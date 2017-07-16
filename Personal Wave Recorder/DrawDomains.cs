#region About
// ~Hats off to~
// Jeff Morton' SoundCatcher::AudioFrame.cs @ http://www.codeproject.com/KB/audio-video/SoundCatcher.aspx
// Arne Elster' WaveIn recorder (vb6) @ http://www.planet-source-code.com/vb/scripts/ShowCode.asp?txtCodeId=65662&lngWId=1
#endregion

#region Directives
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using System.Diagnostics;
using System.Runtime.InteropServices;
#endregion

namespace Personal_Wave_Recorder
{
    public class DrawDomains : IDisposable
    {
        #region Constants
        private const double Pi = 3.14159265358979;
        public const double FFT_MAXAMPLITUDE = 0.2;
        public const double FFT_BANDLOWER = 0.07;
        public const int FFT_BANDS = 22;
        public const int FFT_BANDSPACE = 1;
        public const int FFT_BANDWIDTH = 3;
        public const int FFT_STARTINDEX = 1;
        public const int FFT_SAMPLES = 512;
        public const int DRW_BARXOFF = 1;
        public const int DRW_BARYOFF = 2;
        public const int DRW_BARWIDTH = 3;
        public const int DRW_BARSPACE = 1;
        private double[] _dBands;
        
        #endregion

        #region Enums
        public enum PenStyle
        {
            PS_SOLID = 0,
            PS_DASH = 1,
            PS_DOT = 2,
            PS_DASHDOT = 3,
            PS_DASHDOTDOT = 4,
            PS_NULL = 5,
            PS_INSIDEFRAME = 6,
            PS_USERSTYLE = 7,
            PS_ALTERNATE = 8,
            PS_STYLE_MASK = 0x0000000F,
            PS_ENDCAP_ROUND = 0x00000000,
            PS_ENDCAP_SQUARE = 0x00000100,
            PS_ENDCAP_FLAT = 0x00000200,
            PS_ENDCAP_MASK = 0x00000F00,
            PS_JOIN_ROUND = 0x00000000,
            PS_JOIN_BEVEL = 0x00001000,
            PS_JOIN_MITER = 0x00002000,
            PS_JOIN_MASK = 0x0000F000,
            PS_COSMETIC = 0x00000000,
            PS_GEOMETRIC = 0x00010000,
            PS_TYPE_MASK = 0x000F0000
        };
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public RECT(int X, int Y, int Width, int Height)
            {
                this.Left = X;
                this.Top = Y;
                this.Right = Width;
                this.Bottom = Height;
            }
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PICTUREBOX
        {
            public PICTUREBOX(IntPtr handle, int width, int height)
            {
                this.Hwnd = handle;
                this.Width = width;
                this.Height = height;
            }
            public IntPtr Hwnd;
            public int Width;
            public int Height;
        }
        #endregion

        #region API
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject(IntPtr hObject);
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr handle);
        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr handle, IntPtr hdc);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateSolidBrush(int crColor);
        [DllImport("user32.dll")]
        private static extern int FillRect(IntPtr hDC, [In] ref RECT lprc, IntPtr hbr);
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        #endregion

        #region Accessors/Fields
        private bool _bEightBit = false;
        private PICTUREBOX _leftBox;
        private PICTUREBOX _rightBox;
        private Bitmap _bmpGraphBar;
        private cStoreDc _cBufferDc;
        private cStoreDc _cGraphicDc;
        private Fourier _FFT = new Fourier();

        private int SampleScale { get; set; }
        public bool Decibels { get; set; }
        #endregion

        #region Constructor
        public DrawDomains()
        {
            _dBands = new double[21];
        }
        ~DrawDomains()
        {
            Dispose();
        }
        public void Dispose()
        {
            if (_cBufferDc != null)
                _cBufferDc.Dispose();
            if (_cGraphicDc != null)
                _cGraphicDc.Dispose();
            if (_bmpGraphBar != null)
                _bmpGraphBar.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Methods
        #region TimeDomain
        public void ProcessTimeDomain(Int16[] samples, ushort channels)
        {
            if (channels == 1)
            {
                RenderTimeDomain(samples, _leftBox.Hwnd, _leftBox.Width, _leftBox.Height);
            }
            else
            {
                int len = samples.Length;
                Int16[] left = new Int16[len / 2];
                Int16[] right = new Int16[len / 2];
                SplitInt16Array(samples, ref left, ref right);
                RenderTimeDomain(left, _leftBox.Hwnd, _leftBox.Width, _leftBox.Height);
                RenderTimeDomain(right, _rightBox.Hwnd, _rightBox.Width, _rightBox.Height);
            }
        }
        public void ProcessTimeDomain(byte[] samples, ushort channels)
        {
            if (channels == 1)
            {
                RenderTimeDomain(samples, _leftBox.Hwnd, _leftBox.Width, _leftBox.Height);
            }
            else
            {
                int len = samples.Length;
                byte[] left = new byte[len / 2];
                byte[] right = new byte[len / 2];
                SplitByteArray(samples, ref left, ref right);
                RenderTimeDomain(left, _leftBox.Hwnd, _leftBox.Width, _leftBox.Height);
                RenderTimeDomain(right, _rightBox.Hwnd, _rightBox.Width, _rightBox.Height);
            }
        }

        /// <summary>
        /// Render time domain to PictureBox
        /// </summary>
        /// <param name="data"></param>
        /// <param name="handle"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void RenderTimeDomain(Int16[] data, IntPtr handle, int width, int height)
        {
            double center = height / 2;
            int x = 0, y = 0;
            // Draw channel
            double scale = 0.5 * height / SampleScale;
            int xPrev = 0, yPrev = 0;
            Bitmap canvas = new Bitmap(width, height);

            using (Graphics offScreenDC = Graphics.FromImage(canvas))
            {
                using (Pen pen = new System.Drawing.Pen(Color.Green))
                {
                    for (x = 0; x < width; x++)
                    {
                        y = (int)(center + (data[data.Length / width * x] * scale));
                        if (x == 0)
                        {
                            xPrev = 0;
                            yPrev = y;
                        }
                        else
                        {
                            offScreenDC.DrawLine(pen, xPrev, yPrev, x, y);
                            xPrev = x;
                            yPrev = y;
                        }
                    }
                }
            }
            using (Graphics g = Graphics.FromHwnd(handle))
            {
                using (SolidBrush hb = new SolidBrush(Color.Black))
                    g.FillRectangle(hb, 0, 0, width, height);
                g.DrawImage(canvas, 0, 0);
                canvas.Dispose();
            }
        }
        private void RenderTimeDomain(byte[] data, IntPtr handle, int width, int height)
        {
            double center = height / 2;
            int x = 0, y = 0;
            // Draw channelv 
            double scale = 0.5 * height / (SampleScale / 8);
            int xPrev = 0, yPrev = 0;
            double offset = 0;
            Bitmap canvas = new Bitmap(width, height);

            using (Graphics offScreenDC = Graphics.FromImage(canvas))
            {
                using (Pen pen = new System.Drawing.Pen(Color.Green))
                {
                    for (x = 0; x < width; x++)
                    {
                        offset = data[data.Length / width * x];
                        if (offset == 0)
                            offset = 0;
                        else if (offset > 128)
                            offset -= 128;
                        else if (offset < 128)
                            offset = -(128 - offset);
                        else
                            offset = 0;

                        y = (int)(center + (offset * scale));
                        if (x == 0)
                        {
                            xPrev = 0;
                            yPrev = y;
                        }
                        else
                        {
                            offScreenDC.DrawLine(pen, xPrev, yPrev, x, y);
                            xPrev = x;
                            yPrev = y;
                        }
                    }
                }
            }
            // apply image
            using (Graphics g = Graphics.FromHwnd(handle))
            {
                using (SolidBrush hb = new SolidBrush(Color.FromArgb(56, 56, 56)))
                    g.FillRectangle(hb, 0, 0, width, height);
                g.DrawImage(canvas, 0, 0);
                canvas.Dispose();
            }
        }
        #endregion

        #region Frequency Domain
        private void CreateGraphBar(int width, int height)
        {
            int barwidth = ((width - 4) / 21);
            int barheight = height - 2;

            if (_bmpGraphBar != null)
                _bmpGraphBar.Dispose();
            _bmpGraphBar = new Bitmap(barwidth, barheight);

            Rectangle barRect = new Rectangle(0, 0, barwidth, barheight);

            using (Graphics g = Graphics.FromImage(_bmpGraphBar))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                using (LinearGradientBrush fillBrush = new LinearGradientBrush(barRect, Color.FromArgb(0, 255, 0), Color.FromArgb(255, 0, 0), LinearGradientMode.Vertical))
                {
                    Color[] fillColors = { 
                        Color.FromArgb(255, 0, 0),
                        Color.FromArgb(255, 64, 0),
                        Color.FromArgb(255, 128, 0),
                        Color.FromArgb(255, 196, 0),
                        Color.FromArgb(255, 255, 0),
                        Color.FromArgb(196, 255, 0),
                        Color.FromArgb(128, 255, 0),
                        Color.FromArgb(64, 255, 0),
                        Color.FromArgb(0, 255, 0) };

                    float[] fillPositions = { 0f, .2f, .4f, .5f, .6f, .7f, .8f, .9f, 1f };
                    ColorBlend myBlend = new ColorBlend();
                    myBlend.Colors = fillColors;
                    myBlend.Positions = fillPositions;
                    fillBrush.InterpolationColors = myBlend;
                    g.FillRectangle(fillBrush, barRect);
                }
            }

            _cBufferDc = new cStoreDc();
            _cBufferDc.Height = height;
            _cBufferDc.Width = width;
            _cGraphicDc = new cStoreDc();
            _cGraphicDc.Height = _bmpGraphBar.Height;
            _cGraphicDc.Width = _bmpGraphBar.Width;
            _cGraphicDc.SelectImage(_bmpGraphBar);
        }

        /// <summary>
        /// runthrough comples fft and display freq ampl
        /// </summary>
        /// <param name="samples">samples</param>
        /// <param name="channels">channels</param>
        public void ProcessFrequencyDomain(Int16[] samples, ushort channels)
        {
            _bEightBit = false;
            if (channels == 1)
            {
                DrawFrequencies(samples, _leftBox.Hwnd, _leftBox.Width, _leftBox.Height);
            }
            else
            {
                int len = samples.Length;
                Int16[] left = new Int16[len / 2];
                Int16[] right = new Int16[len / 2];
                SplitInt16Array(samples, ref left, ref right);
                DrawFrequencies(left, _leftBox.Hwnd, _leftBox.Width, _leftBox.Height);
                DrawFrequencies(right, _rightBox.Hwnd, _rightBox.Width, _rightBox.Height);
            }
        }
        public void ProcessFrequencyDomain(byte[] samples, ushort channels)
        {
            _bEightBit = true;
            if (channels == 1)
            {
                int len = samples.Length;
                Int16[] left = new Int16[len / 2];
                Buffer.BlockCopy(samples, 0, left, 0, samples.Length);
                DrawFrequencies(left, _leftBox.Hwnd, _leftBox.Width, _leftBox.Height);
            }
            else
            {
                int len = samples.Length;
                byte[] leftBt = new byte[len / 2];
                byte[] rightBt = new byte[len / 2];
                SplitByteArray(samples, ref leftBt, ref rightBt);

                Int16[] left = new Int16[leftBt.Length / 2];
                Int16[] right = new Int16[rightBt.Length / 2];

                Buffer.BlockCopy(leftBt, 0, left, 0, leftBt.Length);
                Buffer.BlockCopy(rightBt, 0, right, 0, rightBt.Length);

                DrawFrequencies(left, _leftBox.Hwnd, _leftBox.Width, _leftBox.Height);
                DrawFrequencies(right, _rightBox.Hwnd, _rightBox.Width, _rightBox.Height);
            }
        }

        private void DrawFrequencies(Int16[] intSamples, IntPtr handle, int width, int height)
        {
            int i, j;
            int count = FFT_STARTINDEX;
            int barwidth = _bmpGraphBar.Width;
            double[] real = new double[intSamples.Length];
            double complex = 0, band = 0;
            Rectangle rcBand = new Rectangle(0, 0, width, height);

            try
            {
                _FFT.NumberOfSamples(FFT_SAMPLES);
                _FFT.WithTimeWindow(1);
                // load samples
                for (i = 0; i < FFT_SAMPLES; i++)
                    _FFT.RealIn(i, intSamples[i]);

                // normalize values and cut them at FFT_MAXAMPLITUDE
                for (i = 0; i < (FFT_SAMPLES / 2) + 1; i++)
                {
                    complex = _FFT.ComplexOut(i);
                    // normalise
                    real[i] = complex / (FFT_SAMPLES / 4) / 32767;
                    // cut the output to FFT_MAXAMPLITUDE, so
                    // the spectrum doesn't get too small
                    if (real[i] > FFT_MAXAMPLITUDE)
                        real[i] = FFT_MAXAMPLITUDE;
                    real[i] /= FFT_MAXAMPLITUDE;
                }

                for (i = 0; i < FFT_BANDS - 1; i++)
                {
                    // average for the current band
                    for (j = count; j < count + FFT_BANDWIDTH + 1; j++)
                        band += real[j];
                    // boost frequencies in the middle with a hanning window,
                    // because they have less power then the low ones
                    band = (band * (Hanning(i + 3, FFT_BANDS + 3) + 1)) / FFT_BANDWIDTH;
                    _dBands[i] = _bEightBit ? band / 8 : band;
                    if (_dBands[i] > 1)
                        _dBands[i] = 1;
                    // skip some bands
                    count += FFT_BANDSPACE;
                }

                // backfill
                IntPtr brush = CreateSolidBrush(0x565656);
                RECT rc = new RECT(0, 0, width, height);
                FillRect(_cBufferDc.Hdc, ref rc, brush);
                DeleteObject(brush);

                // draw bands to buffer
                for (i = 0; i < _dBands.Length; i++)
                {
                    rcBand.X = (i * barwidth) + (i + 1) * DRW_BARSPACE;
                    rcBand.Width = barwidth;
                    rcBand.Y = (int)(height - (height * _dBands[i]));
                    rcBand.Height = height - (rcBand.Y  + DRW_BARYOFF);
                    if (rcBand.Height + rcBand.Y > height)
                    {
                        rcBand.Height = height - 2;
                        rcBand.Y = 1;
                    }
                    BitBlt(_cBufferDc.Hdc, rcBand.X, rcBand.Y, rcBand.Width, rcBand.Height, _cGraphicDc.Hdc, 0, rcBand.Y, 0xCC0020);
                }
                // blit in buffer
                IntPtr destDc = GetDC(handle);
                BitBlt(destDc, 0, 0, width, height, _cBufferDc.Hdc, 0, 0, 0xCC0020);
                ReleaseDC(handle, destDc);
            }
            catch { }
        }

        private double Hanning(double x, int len)
        {
            return 0.5 * (1 - Math.Cos((2 * Pi * x) / len));
        }
        #endregion

        #region Helpers
        public void LeftPicture(IntPtr handle, int width, int height)
        {
            _leftBox = new PICTUREBOX(handle, width, height);
            CreateGraphBar(width, height);
        }

        public void RightPicture(IntPtr handle, int width, int height)
        {
            _rightBox = new PICTUREBOX(handle, width, height);
        }

        private void SplitByteArray(byte[] data, ref byte[] left, ref byte[] right)
        {
            int len = data.Length - 2;
            int h = 0;
            for (int i = 0; i < len; i += 2)
            {
                left[h] = data[i];
                right[h] = data[i + 2];
                h++;
            }
        }

        private void SplitInt16Array(Int16[] data, ref Int16[] left, ref Int16[] right)
        {
            int len = data.Length - 2;
            int h = 0;
            for (int i = 0; i < len; i += 2)
            {
                left[h] = data[i];
                right[h] = data[i + 2];
                h++;
            }
        }

        public void SetSampleRate(int rate)
        {
            if (rate == 8)
                SampleScale = 256;
            else
                SampleScale = 8192;
        }
        #endregion
        #endregion

        #region StoreDc
        /// <summary>DC buffer class</summary>
        internal class cStoreDc
        {
            #region API
            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateDCA([MarshalAs(UnmanagedType.LPStr)]string lpszDriver, [MarshalAs(UnmanagedType.LPStr)]string lpszDevice, [MarshalAs(UnmanagedType.LPStr)]string lpszOutput, int lpInitData);

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateDCW([MarshalAs(UnmanagedType.LPWStr)]string lpszDriver, [MarshalAs(UnmanagedType.LPWStr)]string lpszDevice, [MarshalAs(UnmanagedType.LPWStr)]string lpszOutput, int lpInitData);

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, int lpInitData);

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

            [DllImport("gdi32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool DeleteDC(IntPtr hdc);

            [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true)]
            private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

            [DllImport("gdi32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool DeleteObject(IntPtr hObject);
            #endregion

            #region Fields
            private int _Height = 0;
            private int _Width = 0;
            private IntPtr _Hdc = IntPtr.Zero;
            private IntPtr _Bmp = IntPtr.Zero;
            private IntPtr _BmpOld = IntPtr.Zero;
            #endregion

            #region Methods
            public IntPtr Hdc
            {
                get { return _Hdc; }
            }

            public IntPtr HBmp
            {
                get { return _Bmp; }
            }

            public int Height
            {
                get { return _Height; }
                set
                {
                    if (_Height != value)
                    {
                        _Height = value;
                        ImageCreate(_Width, _Height);
                    }
                }
            }

            public int Width
            {
                get { return _Width; }
                set
                {
                    if (_Width != value)
                    {
                        _Width = value;
                        ImageCreate(_Width, _Height);
                    }
                }
            }

            public void SelectImage(Bitmap image)
            {
                if (Hdc != IntPtr.Zero && image != null)
                    SelectObject(Hdc, image.GetHbitmap());
            }

            private void ImageCreate(int Width, int Height)
            {
                IntPtr pHdc = IntPtr.Zero;

                ImageDestroy();
                pHdc = CreateDCA("DISPLAY", "", "", 0);
                _Hdc = CreateCompatibleDC(pHdc);
                _Bmp = CreateCompatibleBitmap(pHdc, _Width, _Height);
                _BmpOld = SelectObject(_Hdc, _Bmp);
                if (_BmpOld == IntPtr.Zero)
                {
                    ImageDestroy();
                }
                else
                {
                    _Width = Width;
                    _Height = Height;
                }
                DeleteDC(pHdc);
                pHdc = IntPtr.Zero;
            }

            private void ImageDestroy()
            {
                if (_BmpOld != IntPtr.Zero)
                {
                    SelectObject(_Hdc, _BmpOld);
                    _BmpOld = IntPtr.Zero;
                }
                if (_Bmp != IntPtr.Zero)
                {
                    DeleteObject(_Bmp);
                    _Bmp = IntPtr.Zero;
                }
                if (_Hdc != IntPtr.Zero)
                {
                    DeleteDC(_Hdc);
                    _Hdc = IntPtr.Zero;
                }
            }

            public void Dispose()
            {
                ImageDestroy();
            }
            #endregion
        }
        #endregion
    }
}

#region Unused
/*
        [DllImport("gdi32.dll")]
        static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);
        [DllImport("gdi32.dll")]
        static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreatePen(PenStyle nPenStyle, int nWidth, int crColor);
        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);
        [DllImport("gdi32.dll")]
        private static extern bool GdiGradientFill(IntPtr hdc, TRIVERTEX[] pVertex, uint dwNumVertex, GRADIENT_RECT[] pMesh, uint dwNumMesh, uint dwMode);
 * 
        [StructLayout(LayoutKind.Sequential)]
        public struct TRIVERTEX
        {
            public int x;
            public int y;
            public ushort Red;
            public ushort Green;
            public ushort Blue;
            public ushort Alpha;
            public TRIVERTEX(
                int x, int y,
                ushort red, ushort green, ushort blue,
                ushort alpha)
            {
                this.x = x;
                this.y = y;
                this.Red = (ushort)(red << 8);
                this.Green = (ushort)(green << 8);
                this.Blue = (ushort)(blue << 8);
                this.Alpha = (ushort)(alpha << 8);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GRADIENT_RECT
        {
            public uint UpperLeft;
            public uint LowerRight;

            public GRADIENT_RECT(uint upLeft, uint lowRight)
            {
                UpperLeft = upLeft;
                LowerRight = lowRight;
            }
        }
 * 
        private int RGB(byte R, byte G, byte B)
        {
            return (R << 16) + (G << 8) + (B);
        }
 * 
        private void DrawPeaks(IntPtr hdc, int start, int color, ref RECT rc)
        {
            TRIVERTEX[] tva = new TRIVERTEX[2];
            tva[0] = new TRIVERTEX(rc.Left, rc.Top, 255, 0, 0, 255);
            tva[1] = new TRIVERTEX(rc.Right, start, 255, 255, 0, 255);
            GRADIENT_RECT[] gRect = new GRADIENT_RECT[] { new GRADIENT_RECT(0, 1) };
            GdiGradientFill(hdc, tva, 2, gRect, 1, 1);
            rc.Top = start;
        }
 * 
        public double CalculateGoertzel(byte[] sample, double frequency, int samplerate)
        {
            double Skn, Skn1, Skn2;
            Skn = Skn1 = Skn2 = 0;
            for (int i = 0; i < sample.Length; i++)
            {
                Skn2 = Skn1;
                Skn1 = Skn;
                Skn = 2 * Math.Cos(2 * Math.PI * frequency / samplerate) * Skn1 - Skn2 + sample[i];
            }
            double WNk = Math.Exp(-2 * Math.PI * frequency / samplerate);

            return 20 * Math.Log10(Math.Abs((Skn - WNk * Skn1)));
        }
 * 
        private void ConvertByteToInt16(ref Int16[] intArr, byte[] btArr)
        {
            Buffer.BlockCopy(btArr, 0, intArr, 0, btArr.Length);
        }
 * 
        /// <summary>
        /// Get color in the range of 0-255 for amplitude sample
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="range"></param>
        /// <param name="amplitude"></param>
        /// <returns></returns>
        private static int GetColor(double min, double max, double range, double amplitude)
        {
            double color;
            if (min != double.NegativeInfinity && min != double.MaxValue & max != double.PositiveInfinity && max != double.MinValue && range != 0)
            {
                if (min < 0 || max < 0)
                    if (min < 0 && max < 0)
                        color = (255 / range) * (Math.Abs(min) - Math.Abs(amplitude));
                    else
                        if (amplitude < 0)
                            color = (255 / range) * (Math.Abs(min) - Math.Abs(amplitude));
                        else
                            color = (255 / range) * (amplitude + Math.Abs(min));
                else
                    color = (255 / range) * (amplitude - min);
            }
            else
                color = 0;
            return (int)color;
        }
 * 
        -DrawFrequencies...
        using (Graphics offScreenDC = Graphics.FromImage(canvas))// too much flicker
        {
            // draw bands
            for (i = 0; i < _dBands.Length; i++)
            {
                rcBand.X = (i * barwidth) + (i + 1) * DRW_BARSPACE;
                rcBand.Width = barwidth;
                rcBand.Y = (int)(height - (height * _dBands[i]));
                rcBand.Height = height - (rcBand.Y  + DRW_BARYOFF);
                imgBounds.Y = rcBand.Y;
                imgBounds.Height = rcBand.Height;
                offScreenDC.DrawImage(_bmpGraphBar, rcBand, imgBounds, GraphicsUnit.Pixel);
                //using (Graphics g = Graphics.FromHdc(hdc))
                //    g.DrawImage(_bmpGraphBar, rcBand.Left, rcBand.Top, rcBand.Right - rcBand.Left, rcBand.Bottom - rcBand.Top);
                //BitBlt(hdc, rcBand.Left, rcBand.Top, rcBand.Right - rcBand.Left, rcBand.Bottom - rcBand.Top, srcDc, 0, rcBand.Top, 0xCC0020);
            }
        }
        // apply image
        using (Graphics g = Graphics.FromHwnd(handle))
        {
            using (SolidBrush hb = new SolidBrush(Color.FromArgb(56, 56, 56)))
                g.FillRectangle(hb, 0, 0, width, height);
            g.DrawImage(canvas, 0, 0);
            canvas.Dispose();
        }
 */
#endregion