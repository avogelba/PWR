#region Directives
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Drawing.Drawing2D;
#endregion

namespace Personal_Wave_Recorder
{
    public class CustomToolTip : NativeWindow, IDisposable
    {
        #region Constants
        // setwindowpos
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        // size/move
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOREDRAW = 0x0008;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_FRAMECHANGED = 0x0020;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint SWP_HIDEWINDOW = 0x0080;
        private const uint SWP_NOCOPYBITS = 0x0100;
        private const uint SWP_NOOWNERZORDER = 0x0200;
        private const uint SWP_NOSENDCHANGING = 0x0400;
        // styles
        private const int TTS_ALWAYSTIP = 0x01;
        private const int TTS_NOPREFIX = 0x02;
        private const int TTS_NOANIMATE = 0x10;
        private const int TTS_NOFADE = 0x20;
        private const int TTS_BALLOON = 0x40;
        private const int TTS_CLOSE = 0x80;
        private const int TTS_USEVISUALSTYLE = 0x100;
        // window messages
        private const int WM_NOTIFY = 0x4E;
        private const int WM_REFLECT = 0x2000;
        private const int WM_PAINT = 0xF;
        private const int WM_SIZE = 0x5;
        private const int WM_MOVE = 0x3;
        private const int WM_SETFONT = 0x30;
        private const int WM_GETFONT = 0x31;
        private const int WM_SHOWWINDOW = 0x18;
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_MOUSELEAVE = 0x2A3;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_MOUSEWHEEL = 0x20A;
        private const int WM_TIMER = 0x113;
        private const int WM_NCPAINT = 0x85;
        private const int WM_DESTROY = 0x2;
        private const int WM_SETFOCUS = 0x7;
        private const int WM_KILLFOCUS = 0x8;
        private const int WM_IME_NOTIFY = 0x282;
        private const int WM_IME_SETCONTEXT = 0x281;
        private const int WM_ACTIVATE = 0x6;
        private const int WM_NCACTIVATE = 0x86;
        private const int WM_STYLECHANGED = 0x7d;
        private const int WM_STYLECHANGING = 0x7c;
        private const int WM_WINDOWPOSCHANGING = 0x46;
        private const int WM_WINDOWPOSCHANGED = 0x47;
        private const int WM_NCCALCSIZE = 0x83;
        private const int WM_CTLCOLOR = 0x3d8d610;
        // window styles
        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = (-20);
        private const int SS_OWNERDRAW = 0xD;
        private const int WS_OVERLAPPED = 0x0;
        private const int WS_TABSTOP = 0x10000;
        private const int WS_THICKFRAME = 0x40000;
        private const int WS_HSCROLL = 0x100000;
        private const int WS_VSCROLL = 0x200000;
        private const int WS_BORDER = 0x800000;
        private const int WS_CLIPCHILDREN = 0x2000000;
        private const int WS_CLIPSIBLINGS = 0x4000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_CHILD = 0x40000000;
        private const int WS_POPUP = -2147483648;
        // window extended styles
        private const int WS_EX_LTRREADING = 0x0;
        private const int WS_EX_LEFT = 0x0;
        private const int WS_EX_RIGHTSCROLLBAR = 0x0;
        private const int WS_EX_DLGMODALFRAME = 0x1;
        private const int WS_EX_NOPARENTNOTIFY = 0x4;
        private const int WS_EX_TOPMOST = 0x8;
        private const int WS_EX_ACCEPTFILES = 0x10;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_MDICHILD = 0x40;
        private const int WS_EX_TOOLWINDOW = 0x80;
        private const int WS_EX_WINDOWEDGE = 0x100;
        private const int WS_EX_CLIENTEDGE = 0x200;
        private const int WS_EX_CONTEXTHELP = 0x400;
        private const int WS_EX_RIGHT = 0x1000;
        private const int WS_EX_RTLREADING = 0x2000;
        private const int WS_EX_LEFTSCROLLBAR = 0x4000;
        private const int WS_EX_CONTROLPARENT = 0x10000;
        private const int WS_EX_STATICEDGE = 0x20000;
        private const int WS_EX_APPWINDOW = 0x40000;
        private const int WS_EX_NOACTIVATE = 0x8000000;
        private const int WS_EX_LAYERED = 0x80000;
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            private RECT(int X, int Y, int Width, int Height)
            {
                this.Left = X;
                this.Top = Y;
                this.Right = Width;
                this.Bottom = Height;
            }
            internal int Left;
            internal int Top;
            internal int Right;
            internal int Bottom;
        }
        #endregion

        #region API
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CreateWindowEx(int exstyle, string lpClassName, string lpWindowName, int dwStyle,
            int x, int y, int nWidth, int nHeight, IntPtr hwndParent, IntPtr Menu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = false)]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern IntPtr SetTimer(IntPtr hWnd, int nIDEvent, uint uElapse, IntPtr lpTimerFunc);

        [DllImport("user32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool KillTimer(IntPtr hWnd, uint uIDEvent);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int x, int y, int cx, int cy, uint flags);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, ref RECT r);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll")]
        private static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr handle);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr handle, IntPtr hdc);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        #endregion

        #region Fields
        private bool _bTimerActive = false;
        private bool _bTipShowing = false;
        private bool _bTextRightToLeft = false;
        private bool _bUseIcon = false;
        private int _iTimerTick = 0;
        private int _iDelayTime = 1000;
        private int _iVisibleTime = 3000;
        private string _sClientCaption = String.Empty;
        private string _sClientTitle = String.Empty;
        private Color _clrForeColor = Color.Black;
        private Color _clrGradientBegin = Color.White;
        private Color _clrGradientEnd = Color.Silver;
        private IntPtr _hTipWnd = IntPtr.Zero;
        private IntPtr _hInstance = IntPtr.Zero;
        private IntPtr _hParentWnd = IntPtr.Zero;
        private Rectangle _rcClientBounds = new Rectangle();
        private Font _fntTitleFont;
        private Font _fntCaptionFont;
        private Image _imgClientImage = null;
        private cStoreDc _cDrawDc;
        #endregion

        #region Constructor
        public CustomToolTip(IntPtr hParentWnd)
        {
            Type t = typeof(ToolTip);
            Module m = t.Module;
            _hInstance = Marshal.GetHINSTANCE(m);
            _hParentWnd = hParentWnd;
            // create window
            _hTipWnd = CreateWindowEx(WS_EX_TOPMOST | WS_EX_TOOLWINDOW,
                "STATIC", "",
                SS_OWNERDRAW | WS_CHILD | WS_CLIPSIBLINGS | WS_OVERLAPPED,
                0, 0,
                0, 0,
                GetDesktopWindow(),
                IntPtr.Zero, _hInstance, IntPtr.Zero);
            // set starting position
            SetWindowPos(_hTipWnd, HWND_TOP,
                0, 0,
                0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_NOOWNERZORDER);
            this.AssignHandle(_hTipWnd);
        }

        private void Create()
        {
            Destroy();
            _fntTitleFont = new Font("Tahoma", 8, FontStyle.Bold);
            _fntCaptionFont = new Font("Tahoma", 8, FontStyle.Regular);
        }

        private void Destroy()
        {
            if (_fntTitleFont != null)
                _fntTitleFont.Dispose();
            if (_fntCaptionFont != null)
                _fntCaptionFont.Dispose();
        }
        #endregion

        #region Properties
        private Rectangle TipBounds
        {
            get { return _rcClientBounds; }
            set { _rcClientBounds = value; }
        }

        private string Caption
        {
            get { return _sClientCaption; }
            set { _sClientCaption = value; }
        }

        public int DelayTime
        {
            get { return _iDelayTime; }
            set { _iDelayTime = value; }
        }

        public Color ForeColor
        {
            get { return _clrForeColor; }
            set { _clrForeColor = value; }
        }

        public Color GradientBegin
        {
            get { return _clrGradientBegin; }
            set { _clrGradientBegin = value; }
        }

        public Color GradientEnd
        {
            get { return _clrGradientEnd; }
            set { _clrGradientEnd = value; }
        }

        public Image ItemImage
        {
            get { return _imgClientImage; }
            set { _imgClientImage = value; }
        }

        public int MaximumLength
        {
            set { _rcClientBounds.Width = value; }
        }

        public bool TextRightToLeft
        {
            get { return _bTextRightToLeft; }
            set { _bTextRightToLeft = value; }
        }

        private string Title
        {
            get { return _sClientTitle; }
            set { _sClientTitle = value; }
        }

        public bool UseIcon
        {
            get { return _bUseIcon; }
            set { _bUseIcon = value; }
        }

        public int VisibleTime
        {
            get { return _iVisibleTime; }
            set { _iVisibleTime = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>Start the ToolTip timer</summary>
        /// <param name="title">optional title text</param>
        /// <param name="caption">caption text</param>
        /// <param name="image">optional image</param>
        /// <param name="bounds">size and location of tooltip [manual sizing]</param>
        public void Start(string title, string caption, Image image, Rectangle bounds)
        {
            // reset timer
            if (_bTimerActive)
                Stop();
            // create graphics objects
            Create();
            this.Title = title;
            this.Caption = caption;
            this.ItemImage = image;
            this.TipBounds = bounds;
            CreateBuffer(bounds);
            // start the timer
            SetTimer(_hTipWnd, 1, 100, IntPtr.Zero);
        }

        /// <summary>Start the ToolTip timer</summary>
        /// <param name="title">optional title text</param>
        /// <param name="caption">caption text</param>
        /// <param name="image">optional image</param>
        /// <param name="pt">location of tooltip [auto-calculate size]</param>
        public void Start(string title, string caption, Image image, Point pt)
        {
            // reset timer
            if (_bTimerActive)
                Stop();
            // create graphics objects
            Create();
            this.Title = title;
            this.Caption = caption;
            this.ItemImage = image;
            Rectangle bounds = CalculateSize();
            bounds.X = pt.X + 16;
            bounds.Y = pt.Y + bounds.Height + 16;
            this.TipBounds = bounds;
            CreateBuffer(bounds);
            // start the timer
            SetTimer(_hTipWnd, 1, 100, IntPtr.Zero);
        }

        private void CreateBuffer(Rectangle bounds)
        {
            // draw it into a buffer
            if (_cDrawDc != null)
                _cDrawDc.Dispose();
            _cDrawDc = new cStoreDc();
            _cDrawDc.Width = bounds.Width;
            _cDrawDc.Height = bounds.Height;
            bounds.X = 0;
            bounds.Y = 0;
            PositionWindow();
            using (Graphics g = Graphics.FromHdc(_cDrawDc.Hdc))
            {
                CopyBackground(g);
                g.SmoothingMode = SmoothingMode.HighQuality;
                bounds.Inflate(-4, -4);
                DrawDropShadow(g, bounds, 4, 255);
                DrawBackGround(g, bounds);
                DrawBorder(g, bounds);
                DrawCaption(g, bounds);
                DrawTitle(g, bounds);
                DrawIcon(g, bounds);
            }
        }

        /// <summary>Hide the tooltip</summary>
        public void Stop()
        {
            // hide the window
            ShowWindow(false);
            // dispose of graphics
            Destroy();
            // kill the timer
            KillTimer(_hTipWnd, 1);
            // reset properties
            this.Title = String.Empty;
            this.Caption = String.Empty;
            this.ItemImage = null;
            this.TipBounds = Rectangle.Empty;
            // reset timer values
            _iTimerTick = 0;
            _bTipShowing = false;
            _bTimerActive = false;
        }

        /// <summary>Dispose of the tooltip</summary>
        public void Dispose()
        {
            Destroy();
            if (this.ItemImage != null)
            {
                this.ItemImage.Dispose();
                this.ItemImage = null;
            }
            if (_hTipWnd != IntPtr.Zero)
            {
                this.ReleaseHandle();
                DestroyWindow(_hTipWnd);
                _hTipWnd = IntPtr.Zero;
            }
        }
        #endregion

        #region Private Methods
        #region Drawing
        private void Draw()
        {
            if ((this.Caption != String.Empty) && (this.TipBounds != Rectangle.Empty))
            {
                // show the window
                ShowWindow(true);
                // draw the tempdc to the window
                IntPtr hdc = GetDC(_hTipWnd);
                BitBlt(hdc, 0, 0, this.TipBounds.Width, this.TipBounds.Height, _cDrawDc.Hdc, 0, 0, 0xCC0020);
                ReleaseDC(_hTipWnd, hdc);
            }
        }

        private void DrawBorder(Graphics g, Rectangle bounds)
        {
            // draw the frame
            using (GraphicsPath borderPath = CreateRoundRectanglePath(g, bounds, 4f))
            {
                using (Pen borderPen = new Pen(Color.FromArgb(250, 0x44, 0x44, 0x44)))
                    g.DrawPath(borderPen, borderPath);
            }
        }

        private void DrawBackGround(Graphics g, Rectangle bounds)
        {
            // fill the body
            using (GraphicsPath fillPath = CreateRoundRectanglePath(g, bounds, 4f))
            {
                using (LinearGradientBrush shadowBrush = new LinearGradientBrush(bounds, 
                    this.GradientBegin, this.GradientEnd, 
                    LinearGradientMode.Vertical))
                {
                    Blend blend = new Blend();
                    blend.Positions = new float[] { 0f, .4f, .6f, 1f };
                    blend.Factors = new float[] { 0f, .3f, .6f, .8f };
                    shadowBrush.Blend = blend;
                    g.FillPath(shadowBrush, fillPath);
                }
            }
        }

        private void DrawCaption(Graphics g, Rectangle bounds)
        {
            if (!string.IsNullOrEmpty(this.Caption))
            {
                using (StringFormat sF = new StringFormat())
                {
                    int vOffset;
                    int hOffset;

                    if (this.ItemImage != null)
                    {
                        if (!string.IsNullOrEmpty(this.Title))
                        {
                            vOffset = 4;
                            hOffset = (this.ItemImage.Size.Height / 2) + (_fntTitleFont.Height);
                        }
                        else
                        {
                            vOffset = this.ItemImage.Size.Width + 4;
                            hOffset = 5;
                        }
                    }
                    else if (!string.IsNullOrEmpty(this.Title))
                    {
                        vOffset = 4;
                        hOffset = (_fntTitleFont.Height + 8);
                    }
                    else
                    {
                        vOffset = 4;
                        hOffset = 4;
                    }
                    sF.Alignment = StringAlignment.Near;
                    sF.LineAlignment = StringAlignment.Near;
                    if (this.TextRightToLeft)
                        sF.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                    using (Brush captionBrush = new SolidBrush(this.ForeColor))
                        g.DrawString(this.Caption, _fntCaptionFont, captionBrush, new RectangleF(vOffset, hOffset, this.TipBounds.Width - vOffset, this.TipBounds.Height - hOffset), sF);
                }
            }
        }

        private void DrawDropShadow(Graphics g, Rectangle bounds, int depth, int opacity)
        {
            // offset shadow dimensions
            Rectangle shadowBounds = bounds;
            shadowBounds.Offset(depth, depth);

            // create a clipping region
            using (GraphicsPath clipPath = CreateRoundRectanglePath(g, bounds, 4))
            {
                // clip the interior
                using (Region region = new Region(clipPath))
                    g.SetClip(region, CombineMode.Exclude);
            }

            // create a graphics path
            using (GraphicsPath gp = CreateRoundRectanglePath(g, shadowBounds, 4))
            {
                // draw with a path brush
                using (PathGradientBrush borderBrush = new PathGradientBrush(gp))
                {
                    borderBrush.CenterColor = Color.FromArgb(opacity, Color.Black);
                    borderBrush.SurroundColors = new Color[] { Color.Transparent };
                    borderBrush.FocusScales = new PointF(.5f, .5f);
                    g.FillPath(borderBrush, gp);
                    g.ResetClip();
                }
            }
        }

        private void DrawIcon(Graphics g, Rectangle bounds)
        {
            if (this.ItemImage != null)
                g.DrawImage(this.ItemImage, new Rectangle(6, 4, this.ItemImage.Width, this.ItemImage.Height));
        }

        private void DrawTitle(Graphics g, Rectangle bounds)
        {
            if (!string.IsNullOrEmpty(this.Title))
            {
                using (StringFormat sF = new StringFormat())
                {
                    int vOffset;
                    int hOffset;

                    if (this.ItemImage != null)
                    {
                        vOffset = this.ItemImage.Size.Width + 6;
                        hOffset = 4;
                    }
                    else
                    {
                        vOffset = 4;
                        hOffset = 4;
                    }
                    sF.Alignment = StringAlignment.Near;
                    sF.LineAlignment = StringAlignment.Near;
                    sF.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
                    sF.FormatFlags = StringFormatFlags.NoWrap;

                    if (this.TextRightToLeft)
                        sF.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                    using (Brush titleBrush = new SolidBrush(this.ForeColor))
                        g.DrawString(this.Title, _fntTitleFont, titleBrush, new PointF(vOffset, hOffset), sF);
                }
            }
        }
        #endregion

        #region Helpers
        private Rectangle CalculateSize()
        {
            SizeF textSize = new SizeF();
            Rectangle bounds = new Rectangle();

            if (this.ItemImage != null && !string.IsNullOrEmpty(this.Title))
            {
                // image size
                bounds.Height = this.ItemImage.Size.Height + 4;
                bounds.Width = this.ItemImage.Size.Width + 4;
                // title size
                textSize = CalcTextSize(this.Title, _fntTitleFont, 0);
                bounds.Height = Math.Max((int)textSize.Height + 4, bounds.Height);
                bounds.Width += (int)textSize.Width + 4;
                // caption size
                textSize = CalcTextSize(this.Caption, _fntCaptionFont, 150);
                bounds.Height += (int)textSize.Height + 4;
                bounds.Width = Math.Max((int)textSize.Width + 4, bounds.Width);
            }
            else if (this.ItemImage != null && string.IsNullOrEmpty(this.Title))
            {
                // image size
                bounds.Height = this.ItemImage.Size.Height + 4;
                bounds.Width = this.ItemImage.Size.Width + 4;
                // caption size
                textSize = CalcTextSize(this.Caption, _fntCaptionFont, 150);
                bounds.Height = Math.Max((int)textSize.Height + 4, bounds.Height);
                bounds.Width += (int)textSize.Width + 8;
            }
            else if (!string.IsNullOrEmpty(this.Title))
            {
                // title size
                textSize = CalcTextSize(this.Title, _fntTitleFont, 0);
                bounds.Height = (int)textSize.Height + 4;
                bounds.Width = (int)textSize.Width + 4;
                // caption size
                textSize = CalcTextSize(this.Caption, _fntCaptionFont, 150);
                bounds.Height += (int)textSize.Height + 4;
                bounds.Width = Math.Max((int)textSize.Width + 4, bounds.Width);
            }
            else
            {
                // caption size
                textSize = CalcTextSize(this.Caption, _fntCaptionFont, 150);
                bounds.Height = (int)textSize.Height + 4;
                bounds.Width = (int)textSize.Width + 8;
            }
            bounds.Inflate(2, 2);
            return bounds;
        }

        private SizeF CalcTextSize(string text, Font font, int width)
        {
            SizeF sF = new SizeF();
            using (Graphics g = Graphics.FromHwnd(_hTipWnd))
            {
                if (width > 0)
                    sF = g.MeasureString(text, font, width);
                else
                    sF = g.MeasureString(text, font);
            }
            return sF;
        }

        private void CopyBackground(Graphics g)
        {
            RECT windowRect = new RECT();
            GetWindowRect(_hTipWnd, ref windowRect);
            g.CopyFromScreen(windowRect.Left, windowRect.Top, 0, 0, new Size(windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top), CopyPixelOperation.SourceCopy);
        }

        private GraphicsPath CreateRoundRectanglePath(Graphics g, Rectangle bounds, float radius)
        {
            // create a path
            GraphicsPath pathBounds = new GraphicsPath();
            // arc top left
            pathBounds.AddArc(bounds.Left, bounds.Top, radius, radius, 180, 90);
            // line top
            pathBounds.AddLine(bounds.Left + radius, bounds.Top, bounds.Right - radius, bounds.Top);
            // arc top right
            pathBounds.AddArc(bounds.Right - radius, bounds.Top, radius, radius, 270, 90);
            // line right
            pathBounds.AddLine(bounds.Right, bounds.Top + radius, bounds.Right, bounds.Bottom - radius);
            // arc bottom right
            pathBounds.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            // line bottom
            pathBounds.AddLine(bounds.Right - radius, bounds.Bottom, bounds.Left + radius, bounds.Bottom);
            // arc bottom left
            pathBounds.AddArc(bounds.Left, bounds.Bottom - radius, radius, radius, 90, 90);
            // line left
            pathBounds.AddLine(bounds.Left, bounds.Bottom - radius, bounds.Left, bounds.Top + radius);
            pathBounds.CloseFigure();
            return pathBounds;
        }

        private void PositionWindow()
        {
            if (_hTipWnd != IntPtr.Zero)
            {
                // offset with screen position
                RECT windowRect = new RECT();
                GetWindowRect(_hParentWnd, ref windowRect);
                windowRect.Left += this.TipBounds.X;
                windowRect.Top += this.TipBounds.Y;
                // position the window
                SetWindowPos(_hTipWnd, HWND_TOPMOST, windowRect.Left, windowRect.Top, this.TipBounds.Width, this.TipBounds.Height, SWP_SHOWWINDOW | SWP_NOACTIVATE);
            }
        }

        private void ShowWindow(bool show)
        {
            if (show)
                SetWindowPos(_hTipWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_SHOWWINDOW | SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);
            else
                SetWindowPos(_hTipWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_HIDEWINDOW | SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);
        }
        #endregion
        #endregion

        #region WndProc
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_TIMER:
                    _iTimerTick++;
                    if (_iTimerTick > (this.DelayTime / 100))
                    {
                        if (!_bTipShowing)
                        {
                            _bTipShowing = true;
                            Draw();
                        }
                    }
                    if (_iTimerTick > ((this.DelayTime + this.VisibleTime) / 100))
                        Stop();
                    base.WndProc(ref m);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        #region GraphicsMode
        internal class GraphicsMode : IDisposable
        {
            #region Instance Fields
            private Graphics _graphicCopy;
            private SmoothingMode _oldMode;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the class.
            /// </summary>
            /// <param name="g">Graphics instance.</param>
            /// <param name="mode">Desired Smoothing mode.</param>
            public GraphicsMode(Graphics g, SmoothingMode mode)
            {
                _graphicCopy = g;
                _oldMode = _graphicCopy.SmoothingMode;
                _graphicCopy.SmoothingMode = mode;
            }

            /// <summary>
            /// Revert the SmoothingMode to original setting.
            /// </summary>
            public void Dispose()
            {
                _graphicCopy.SmoothingMode = _oldMode;
            }
            #endregion
        }
        #endregion

        #region StoreDc
        /// <summary>DC buffer class</summary>
        private class cStoreDc : IDisposable
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
