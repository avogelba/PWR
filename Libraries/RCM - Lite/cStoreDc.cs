namespace RCML
{
    #region Directives
    using System;
    using System.Runtime.InteropServices;
    using System.Drawing;
    #endregion

    public class cStoreDc
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
}
