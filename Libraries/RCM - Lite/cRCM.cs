namespace RCML
{
    #region Author/About
    /************************************************************************************
    *  vtRCM   Radical (Non)Client Modification Class -Lite                             *
    *                                                                                   *
    *  Created:     June 16, 2009                                                       *
    *  Modified:    Febuary 21, 2010                                                    *
    *  Purpose:     Form Non Client modifier                                            *
    *  Revision:    1.3                                                                 *
    *  Tested On:   Win7 32bit, Vista 64bit, XP Professional                            *
    *  IDE:         C# 2008 SP1                                                         *
    *  Referenced:  Control Library NSP                                                 *
    *  Author:      John Underhill (Steppenwolfe)                                       *
    *                                                                                   *
    *************************************************************************************

    You can not:
    Sell or redistribute this code or the binary for profit.
    Use this in spyware, malware, or any generally acknowledged form of malicious software.
    Remove or alter the above author accreditation, or this disclaimer.

    You can:
    Use this code in your applications in any way you like.
    Use this in a published program, (a credit to vtdev.com would be nice)

    I will not:
    Except any responsibility for this code whatsoever. 
    There is no guarantee of fitness, nor should you have any expectation of support. 
    I further renounce any and all responsibilities for this code, in every way conceivable, 
    now, and for the rest of time.
    
    Cheers,
    John
    steppenwolfe_2000@yahoo.com
    */
    #endregion

    #region Directives
    using System;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Diagnostics;
    #endregion

    #region Public Enums
    public enum ControlType : int
    {
        Button = 1,
        CheckBox,
        ComboBox,
        ListBox,
        ListView,
        NumericUpDown,
        ProgressBar,
        RadioButton,
        ScrollBar,
        TabControl,
        TrackBar,
        TreeView
    }
    #endregion

    public class RCM : NativeWindow
    {
        #region Constants
        // setbkmode
        private const int TRANSPARENT = 1;
        private const int OPAQUE = 2;
        // drawtext
        private const int DT_TOP = 0x0;
        private const int DT_LEFT = 0x0;
        private const int DT_CENTER = 0x1;
        private const int DT_RIGHT = 0x2;
        private const int DT_VCENTER = 0x4;
        private const int DT_BOTTOM = 0x8;
        private const int DT_WORDBREAK = 0x10;
        private const int DT_SINGLELINE = 0x20;
        private const int DT_EXPANDTABS = 0x40;
        private const int DT_TABSTOP = 0x80;
        private const int DT_NOCLIP = 0x100;
        private const int DT_EXTERNALLEADING = 0x200;
        private const int DT_CALCRECT = 0x400;
        private const int DT_NOPREFIX = 0x800;
        private const int DT_INTERNAL = 0x1000;
        private const int DT_EDITCONTROL = 0x2000;
        private const int DT_PATH_ELLIPSIS = 0x4000;
        private const int DT_END_ELLIPSIS = 0x8000;
        private const int DT_MODIFYSTRING = 0x10000;
        private const int DT_RTLREADING = 0x20000;
        private const int DT_WORD_ELLIPSIS = 0x40000;
        // window messages
        private const int WM_ACTIVATE = 0x6;
        private const int WM_ACTIVATEAPP = 0x001C;
        private const int WM_NCDESTROY = 0x82;
        private const int WM_INITMENU = 0x116;
        private const int WM_INITMENUPOPUP = 0x117;
        private const int WM_EXITMENULOOP = 0x212;
        private const int WM_NCCALCSIZE = 0x83;
        private const int WM_NCHITTEST = 0x84;
        private const int WM_NCPAINT = 0x85;
        private const int WM_NCACTIVATE = 0x86;
        private const int WM_NCMOUSEMOVE = 0xA0;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int WM_NCLBUTTONUP = 0xA2;
        private const int WM_NCLBUTTONDBLCLK = 0xA3;
        private const int WM_NCRBUTTONDOWN = 0xA4;
        private const int WM_NCRBUTTONUP = 0xA5;
        private const int WM_NCRBUTTONDBLCLK = 0xA6;
        private const int WM_SETFOCUS = 0x7;
        private const int WM_KILLFOCUS = 0x8;
        private const int WM_ENABLE = 0xA;
        private const int WM_TIMER = 0x113;
        private const int WM_SIZE = 0x5;
        private const int WM_STYLECHANGED = 0x7D;
        private const int WM_DISPLAYCHANGE = 0x7E;
        private const int WM_SYSCOMMAND = 0x112;
        private const int WM_GETICON = 0x7F;
        private const int WM_SETTEXT = 0xC;
        private const int WM_GETTEXT = 0xD;
        private const int WM_SETCURSOR = 0x0020;
        private const int WM_CREATE = 0x0001;
        // window styles
        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = (-20);
        private const int WS_THICKFRAME = 0x40000;
        private const int WS_DLGFRAME = 0x00400000;
        private const int WS_EX_CLIENTEDGE = 0x200;
        private const int WS_EX_WINDOWEDGE = 0x100;
        private const int WS_EX_STATICEDGE = 0x20000;
        private const int WS_EX_TOOLWINDOW = 0x80;
        private const int SC_RESTORE = 0xF120;
        private const int SC_MAXIMIZE = 0xF030;
        // setwindowpos
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        // size/move
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOREDRAW = 0x0008;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int SWP_FRAMECHANGED = 0x0020;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int SWP_HIDEWINDOW = 0x0080;
        private const int SWP_NOCOPYBITS = 0x0100;
        private const int SWP_NOOWNERZORDER = 0x0200;
        private const int SWP_NOSENDCHANGING = 0x0400;
        // redraw
        private const int RDW_INVALIDATE = 0x0001;
        private const int RDW_INTERNALPAINT = 0x0002;
        private const int RDW_ERASE = 0x0004;
        private const int RDW_VALIDATE = 0x0008;
        private const int RDW_NOINTERNALPAINT = 0x0010;
        private const int RDW_NOERASE = 0x0020;
        private const int RDW_NOCHILDREN = 0x0040;
        private const int RDW_ALLCHILDREN = 0x0080;
        private const int RDW_UPDATENOW = 0x0100;
        private const int RDW_ERASENOW = 0x0200;
        private const int RDW_FRAME = 0x0400;
        private const int RDW_NOFRAME = 0x0800;
        // nc_calcsize return params
        private const int WVR_VALIDRECTS = 0x400;
        private const int WVR_HREDRAW = 0x0100;
        private const int WVR_VREDRAW = 0x0200;
        private const int WVR_REDRAW = (WVR_HREDRAW | WVR_VREDRAW);
        // misc
        private const int VK_LBUTTON = 0x1;
        private const int VK_RBUTTON = 0x2;
        private const int KEY_PRESSED = 0x1000;
        private const int SPI_GETWORKAREA = 0x0030;
        private const int DI_NORMAL = 0x0003;
        // frame
        private const int FRAME_WIDTH = 8;
        private const int CAPTION_HEIGHT = 30;
        static readonly IntPtr MESSAGE_HANDLED = new IntPtr(1);
        static readonly IntPtr MESSAGE_PROCESS = new IntPtr(0);
        #endregion

        #region Private Enums
        [Flags]
        private enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        private enum POST_MESSAGES : int
        {
            SC_ARRANGE = 0xF110,
            SC_CLOSE = 0xF060,
            SC_MAXIMIZE = 0xF030,
            SC_MINIMIZE = 0xF020,
            SC_MOVE = 0xF010,
            SC_NEXTWINDOW = 0xF040,
            SC_PREVWINDOW = 0xF050,
            SC_RESTORE = 0xF120,
            SC_SIZE = 0xF000
        }

        private enum HIT_CONSTANTS : int
        {
            HTERROR = -2,
            HTTRANSPARENT = -1,
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTOBJECT = 19,
            HTCLOSE = 20,
            HTHELP = 21
        }

        [Flags]
        private enum SYSTEM_METRIC : int
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
            SM_CXVSCROLL = 2,
            SM_CYHSCROLL = 3,
            SM_CYCAPTION = 4,
            SM_CXBORDER = 5,
            SM_CYBORDER = 6,
            SM_CXDLGFRAME = 7,
            SM_CYDLGFRAME = 8,
            SM_CYVTHUMB = 9,
            SM_CXHTHUMB = 10,
            SM_CXICON = 11,
            SM_CYICON = 12,
            SM_CXCURSOR = 13,
            SM_CYCURSOR = 14,
            SM_CYMENU = 15,
            SM_CXFULLSCREEN = 16,
            SM_CYFULLSCREEN = 17,
            SM_CYKANJIWINDOW = 18,
            SM_MOUSEPRESENT = 19,
            SM_CYVSCROLL = 20,
            SM_CXHSCROLL = 21,
            SM_DEBUG = 22,
            SM_SWAPBUTTON = 23,
            SM_RESERVED1 = 24,
            SM_RESERVED2 = 25,
            SM_RESERVED3 = 26,
            SM_RESERVED4 = 27,
            SM_CXMIN = 28,
            SM_CYMIN = 29,
            SM_CXSIZE = 30,
            SM_CYSIZE = 31,
            SM_CXFRAME = 32,
            SM_CYFRAME = 33,
            SM_CXMINTRACK = 34,
            SM_CYMINTRACK = 35,
            SM_CXDOUBLECLK = 36,
            SM_CYDOUBLECLK = 37,
            SM_CXICONSPACING = 38,
            SM_CYICONSPACING = 39,
            SM_MENUDROPALIGNMENT = 40,
            SM_PENWINDOWS = 41,
            SM_DBCSENABLED = 42,
            SM_CMOUSEBUTTONS = 43,
            SM_SECURE = 44,
            SM_CXEDGE = 45,
            SM_CYEDGE = 46,
            SM_CXMINSPACING = 47,
            SM_CYMINSPACING = 48,
            SM_CXSMICON = 49,
            SM_CYSMICON = 50,
            SM_CYSMCAPTION = 51,
            SM_CXSMSIZE = 52,
            SM_CYSMSIZE = 53,
            SM_CXMENUSIZE = 54,
            SM_CYMENUSIZE = 55,
            SM_ARRANGE = 56,
            SM_CXMINIMIZED = 57,
            SM_CYMINIMIZED = 58,
            SM_CXMAXTRACK = 59,
            SM_CYMAXTRACK = 60,
            SM_CXMAXIMIZED = 61,
            SM_CYMAXIMIZED = 62,
            SM_NETWORK = 63,
            SM_CLEANBOOT = 67,
            SM_CXDRAG = 68,
            SM_CYDRAG = 69,
            SM_SHOWSOUNDS = 70,
            SM_CXMENUCHECK = 71,
            SM_CYMENUCHECK = 72,
            SM_SLOWMACHINE = 73,
            SM_MIDEASTENABLED = 74,
            SM_MOUSEWHEELPRESENT = 75,
            SM_XVIRTUALSCREEN = 76,
            SM_YVIRTUALSCREEN = 77,
            SM_CXVIRTUALSCREEN = 78,
            SM_CYVIRTUALSCREEN = 79,
            SM_CMONITORS = 80,
            SM_SAMEDISPLAYFORMAT = 81,
            SM_IMMENABLED = 82,
            SM_CXFOCUSBORDER = 83,
            SM_CYFOCUSBORDER = 84,
            SM_TABLETPC = 86,
            SM_MEDIACENTER = 87,
            SM_CMETRICS_OTHER = 76,
            SM_CMETRICS_2000 = 83,
            SM_CMETRICS_NT = 88,
            SM_REMOTESESSION = 0x1000,
            SM_SHUTTINGDOWN = 0x2000,
            SM_REMOTECONTROL = 0x2001,
        }
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            internal int X;
            internal int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            internal RECT(int X, int Y, int Width, int Height)
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

        [StructLayout(LayoutKind.Sequential)]
        private struct NCCALCSIZE_PARAMS
        {
            internal RECT rect0, rect1, rect2;
            internal IntPtr lppos;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WINDOWPOS
        {
            internal IntPtr hwnd;
            internal IntPtr hwndInsertAfter;
            internal int x;
            internal int y;
            internal int cx;
            internal int cy;
            internal int flags;
        }
        #endregion

        #region API
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PtInRect([In] ref RECT lprc, Point pt);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

        [DllImport("user32.dll")]
        private static extern int ScreenToClient(IntPtr hwnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int x, int y, int cx, int cy, uint flags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetClientRect(IntPtr hWnd, ref RECT r);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr handle, IntPtr hdc);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(SYSTEM_METRIC smIndex);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool StretchBlt(IntPtr hDest, int X, int Y, int nWidth, int nHeight, IntPtr hdcSrc,
        int sX, int sY, int nWidthSrc, int nHeightSrc, int dwRop);

        [DllImport("user32.dll")]
        private extern static int OffsetRect(ref RECT lpRect, int x, int y);

        [DllImport("user32.dll")]
        private static extern IntPtr SetTimer(IntPtr hWnd, int nIDEvent, uint uElapse, IntPtr lpTimerFunc);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool KillTimer(IntPtr hWnd, uint uIDEvent);

        [DllImport("gdi32.dll")]
        private extern static int ExcludeClipRect(IntPtr hdc, int x1, int y1, int x2, int y2);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RedrawWindow(IntPtr hWnd, ref RECT lprcUpdate, IntPtr hrgnUpdate, uint flags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr DefWindowProc(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(int uAction, uint uParam, ref RECT lpvParam, uint fuWinIni);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight,
            int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int DrawText(IntPtr hdc, string lpStr, int nCount, ref RECT lpRect, int wFormat);

        [DllImport("gdi32.dll")]
        private static extern int SetTextColor(IntPtr hdc, int crColor);

        [DllImport("gdi32.dll")]
        private static extern int SetBkMode(IntPtr hdc, int iBkMode);

        [DllImport("user32.dll")]
        private extern static int InflateRect(ref RECT lpRect, int x, int y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);


        [DllImport("uxtheme", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private extern static bool IsAppThemed();

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);
        #endregion

        #region Fields
        private bool _bWindowMaximized = false;
        private bool _bFontRightLeading = false;
        private bool _bCenterTitle = false;
        private bool _bSupressHelpTip = false;
        private bool _bFirstHit = false;
        private bool _bStoreSize = false;
        private bool _bResetSize = false;
        private bool _bIsSizeable = false;
        private bool _bIsFrameInvalid = false;
        private int _iIconSize = 0;
        private int _iButtonOffsetX = 8;
        private int _iButtonOffsetY = 0;
        private int _iIconOffsetX = 0;
        private int _iIconOffsetY = 0;
        private int _iTitleOffsetX = 0;
        private int _iTitleOffsetY = 0;
        private int _iExcludeLeftStart = 0;
        private int _iExcludeLeftEnd = 0;
        private int _iExcludeRightStart = 0;
        private int _iExcludeRightEnd = 0;
        private int _buttonTimer = 0;
        private int _iFrameHeight = FRAME_WIDTH;
        private int _iFrameWidth = FRAME_WIDTH;
        private int _iCaptionBoxStart = 0;
        private int _iCaptionHeight = CAPTION_HEIGHT;
        private Size _szStoreSize = new Size();
        private IntPtr _hParentWnd = IntPtr.Zero;
        private Color _oForeColor = Color.White;
        private Font _oTitleFont = new Font("Segoe UI", 9);
        // frame
        private cStoreDc _cCaptionBarDc = new cStoreDc();
        private Bitmap _oCaptionBarBitmap;
        private cStoreDc _cLeftFrameDc = new cStoreDc();
        private Bitmap _oLeftFrameBitmap;
        private cStoreDc _cRightFrameDc = new cStoreDc();
        private Bitmap _oRightFrameBitmap;
        private cStoreDc _cBottomFrameDc = new cStoreDc();
        private Bitmap _oBottomFrameBitmap;
        // buttons
        private cStoreDc _cMinimizeButtonDc = new cStoreDc();
        private Bitmap _oMinimizeButtonBitmap;
        private cStoreDc _cMaximizeButtonDc = new cStoreDc();
        private Bitmap _oMaximizeButtonBitmap;
        private cStoreDc _cRestoreButtonDc = new cStoreDc();
        private Bitmap _oRestoreButtonBitmap;
        private cStoreDc _cCloseButtonDc = new cStoreDc();
        private Bitmap _oCloseButtonBitmap;
        private cStoreDc _cHelpButtonDc = new cStoreDc();
        private Bitmap _oHelpButtonBitmap;
        private RECT _tRestoreRect = new RECT();
        private RECT[] _aButtonRect = new RECT[4];
        private HIT_CONSTANTS _eLastWindowHit = HIT_CONSTANTS.HTNOWHERE;
        private HIT_CONSTANTS _eLastButtonHit = HIT_CONSTANTS.HTNOWHERE;
        private HIT_CONSTANTS _eLastCursorHit = HIT_CONSTANTS.HTNOWHERE;
        #endregion

        #region Events and Delegates
        public delegate void HelpClickedDelegate(Object sender, EventArgs e);
        public event HelpClickedDelegate HelpClicked;
        #endregion

        #region Constructor
        public RCM(IntPtr handle)
        {
            _hParentWnd = handle;
            //RemoveTheme();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Get/Set Caption buttons offset from right.
        /// </summary>
        public int ButtonOffsetX
        {
            get { return _iButtonOffsetX; }
            set { _iButtonOffsetX = value; }
        }

        /// <summary>
        /// Get/Set Caption Buttons offset from center.
        /// </summary>
        public int ButtonOffsetY
        {
            get { return _iButtonOffsetY; }
            set { _iButtonOffsetY = value; }
        }

        /// <summary>
        /// Get/Set Center the forms title in the caption bar.
        /// </summary>
        public bool CenterTitle
        {
            get { return _bCenterTitle; }
            set { _bCenterTitle = value; }
        }

        /// <summary>
        /// Get/Set Exclude tiling of left caption area starting position.
        /// </summary>
        public int ExcludeLeftStart
        {
            get { return _iExcludeLeftStart; }
            set { _iExcludeLeftStart = value; }
        }

        /// <summary>
        /// Get/Set Exclude tiling of left caption area ending position.
        /// </summary>
        public int ExcludeLeftEnd
        {
            get { return _iExcludeLeftEnd; }
            set { _iExcludeLeftEnd = value; }
        }

        /// <summary>
        /// Get/Set Exclude tiling of left caption area starting position.
        /// </summary>
        public int ExcludeRightStart
        {
            get { return _iExcludeRightStart; }
            set { _iExcludeRightStart = value; }
        }

        /// <summary>
        /// Get/Set Exclude tiling of right caption area ending position.
        /// </summary>
        public int ExcludeRightEnd
        {
            get { return _iExcludeRightEnd; }
            set { _iExcludeRightEnd = value; }
        }

        /// <summary>
        /// Get Forms focus state.
        /// </summary>
        public bool Focused
        {
            get { return (GetForegroundWindow() == this.ParentWnd); }
        }

        /// <summary>
        /// Get/Set Use right aligned text in the caption bar.
        /// </summary>
        public bool FontRightLeading
        {
            get { return _bFontRightLeading; }
            set { _bFontRightLeading = value; }
        }

        /// <summary>
        /// Get/Set The caption title forecolor.
        /// </summary>
        public Color ForeColor
        {
            get { return _oForeColor; }
            set { _oForeColor = value; }
        }

        /// <summary>
        /// Get/Set Icon offset from left.
        /// </summary>
        public int IconOffsetX
        {
            get { return _iIconOffsetX; }
            set { _iIconOffsetX = value; }
        }

        /// <summary>
        /// Get/Set Icon offset from center.
        /// </summary>
        public int IconOffsetY
        {
            get { return _iIconOffsetY; }
            set { _iIconOffsetY = value; }
        }

        /// <summary>
        /// Get Parent window handle.
        /// </summary>
        public IntPtr ParentWnd
        {
            get { return _hParentWnd; }
        }

        /// <summary>
        /// Get/Set Supress tooltip on optional help button.
        /// </summary>
        public bool SupressHelpTip
        {
            get { return _bSupressHelpTip; }
            set { _bSupressHelpTip = value; }
        }

        /// <summary>
        /// Get/Set Caption offset from left.
        /// </summary>
        public int TitleOffsetX
        {
            get { return _iTitleOffsetX; }
            set { _iTitleOffsetX = value; }
        }

        /// <summary>
        /// Get/Set Caption offset from center.
        /// </summary>
        public int TitleOffsetY
        {
            get { return _iTitleOffsetY; }
            set { _iTitleOffsetY = value; }
        }

        /// <summary>
        /// Get/Set The caption font.
        /// </summary>
        public Font TitleFont
        {
            get { return _oTitleFont; }
            set { _oTitleFont = value; }
        }

        #region Images
        public Bitmap CaptionBarGraphic
        {
            get { return _oCaptionBarBitmap; }
            set
            {
                _oCaptionBarBitmap = value;
                if (_cCaptionBarDc.Hdc != IntPtr.Zero)
                {
                    _cCaptionBarDc.Dispose();
                    _cCaptionBarDc = new cStoreDc();
                }
                _cCaptionBarDc.Width = _oCaptionBarBitmap.Width;
                _cCaptionBarDc.Height = _oCaptionBarBitmap.Height;
                _cCaptionBarDc.SelectImage(_oCaptionBarBitmap);
            }
        }

        public Bitmap LeftFrameGraphic
        {
            get { return _oLeftFrameBitmap; }
            set
            {
                _oLeftFrameBitmap = value;
                if (_cLeftFrameDc.Hdc != IntPtr.Zero)
                {
                    _cLeftFrameDc.Dispose();
                    _cLeftFrameDc = new cStoreDc();
                }
                _cLeftFrameDc.Width = _oLeftFrameBitmap.Width;
                _cLeftFrameDc.Height = _oLeftFrameBitmap.Height;
                _cLeftFrameDc.SelectImage(_oLeftFrameBitmap);
            }
        }

        public Bitmap RightFrameGraphic
        {
            get { return _oRightFrameBitmap; }
            set
            {
                _oRightFrameBitmap = value;
                if (_cRightFrameDc.Hdc != IntPtr.Zero)
                {
                    _cRightFrameDc.Dispose();
                    _cRightFrameDc = new cStoreDc();
                }
                _cRightFrameDc.Width = _oRightFrameBitmap.Width;
                _cRightFrameDc.Height = _oRightFrameBitmap.Height;
                _cRightFrameDc.SelectImage(_oRightFrameBitmap);
            }
        }

        public Bitmap BottomFrameGraphic
        {
            get { return _oBottomFrameBitmap; }
            set
            {
                _oBottomFrameBitmap = value;
                if (_cBottomFrameDc.Hdc != IntPtr.Zero)
                {
                    _cBottomFrameDc.Dispose();
                    _cBottomFrameDc = new cStoreDc();
                }
                _cBottomFrameDc.Width = _oBottomFrameBitmap.Width;
                _cBottomFrameDc.Height = _oBottomFrameBitmap.Height;
                _cBottomFrameDc.SelectImage(_oBottomFrameBitmap);
            }
        }

        public Bitmap MinimizeButtonGraphic
        {
            get { return _oMinimizeButtonBitmap; }
            set
            {
                _oMinimizeButtonBitmap = value;
                if (_cMinimizeButtonDc.Hdc != IntPtr.Zero)
                {
                    _cMinimizeButtonDc.Dispose();
                    _cMinimizeButtonDc = new cStoreDc();
                }
                _cMinimizeButtonDc.Width = _oMinimizeButtonBitmap.Width;
                _cMinimizeButtonDc.Height = _oMinimizeButtonBitmap.Height;
                _cMinimizeButtonDc.SelectImage(_oMinimizeButtonBitmap);
            }
        }

        public Bitmap MaximizeButtonGraphic
        {
            get { return _oMaximizeButtonBitmap; }
            set
            {
                _oMaximizeButtonBitmap = value;
                if (_cMaximizeButtonDc.Hdc != IntPtr.Zero)
                {
                    _cMaximizeButtonDc.Dispose();
                    _cMaximizeButtonDc = new cStoreDc();
                }
                _cMaximizeButtonDc.Width = _oMaximizeButtonBitmap.Width;
                _cMaximizeButtonDc.Height = _oMaximizeButtonBitmap.Height;
                _cMaximizeButtonDc.SelectImage(_oMaximizeButtonBitmap);
            }
        }

        public Bitmap RestoreButtonGraphic
        {
            get { return _oRestoreButtonBitmap; }
            set
            {
                _oRestoreButtonBitmap = value;
                if (_cRestoreButtonDc.Hdc != IntPtr.Zero)
                {
                    _cRestoreButtonDc.Dispose();
                    _cRestoreButtonDc = new cStoreDc();
                }
                _cRestoreButtonDc.Width = _oRestoreButtonBitmap.Width;
                _cRestoreButtonDc.Height = _oRestoreButtonBitmap.Height;
                _cRestoreButtonDc.SelectImage(_oRestoreButtonBitmap);
            }
        }

        public Bitmap CloseButtonGraphic
        {
            get { return _oCloseButtonBitmap; }
            set
            {
                _oCloseButtonBitmap = value;
                if (_cCloseButtonDc.Hdc != IntPtr.Zero)
                {
                    _cCloseButtonDc.Dispose();
                    _cCloseButtonDc = new cStoreDc();
                }
                _cCloseButtonDc.Width = _oCloseButtonBitmap.Width;
                _cCloseButtonDc.Height = _oCloseButtonBitmap.Height;
                _cCloseButtonDc.SelectImage(_oCloseButtonBitmap);
            }
        }

        public Bitmap HelpButtonGraphic
        {
            get { return _oHelpButtonBitmap; }
            set
            {
                _oHelpButtonBitmap = value;
                if (_cHelpButtonDc.Hdc != IntPtr.Zero)
                {
                    _cHelpButtonDc.Dispose();
                    _cHelpButtonDc = new cStoreDc();
                }
                _cHelpButtonDc.Width = _oHelpButtonBitmap.Width;
                _cHelpButtonDc.Height = _oHelpButtonBitmap.Height;
                _cHelpButtonDc.SelectImage(_oHelpButtonBitmap);
            }
        }
        #endregion
        #endregion

        #region Private Properties
        private bool CanClose
        {
            get { return ((GetWindowLong(this.ParentWnd, GWL_STYLE) & 0x80000) == 0x80000); }
        }

        private bool CanHelp
        {
            get { return (_oHelpButtonBitmap != null); }
        }

        private bool CanMinimize
        {
            get
            {
                Form f = (Form)Form.FromHandle(this.ParentWnd);
                return f.MinimizeBox;
            }
        }

        private bool CanSize
        {
            get
            {
                Form f = (Form)Form.FromHandle(this.ParentWnd);
                return f.MaximizeBox;
            }
        }

        private bool WindowMaximized
        {
            get { return _bWindowMaximized; }
            set { _bWindowMaximized = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the skinning engine.
        /// </summary>
        /// <param name="Handle">Target window handle.</param>
        public void Start()
        {
            if (!ImageCheck())
                throw new Exception("Required image(s) are either missing or invalid.");

            if ((_hParentWnd == IntPtr.Zero) || (!IsWindow(_hParentWnd)))
                throw new Exception("The window handle is invalid.");
            this.WindowMaximized = IsZoomed(_hParentWnd);
            this.AssignHandle(_hParentWnd);
            // redraw
            InvalidateWindow(true);
            // force NCCALCSIZE message
            RecalculateSize();
            // redraw
            InvalidateWindow(true);
        }

        /// <summary>
        /// Force a complete repaint.
        /// </summary>
        public void ForcedRefresh()
        {
            RedrawWindow(this.ParentWnd, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_UPDATENOW | RDW_INVALIDATE | RDW_INTERNALPAINT | RDW_ALLCHILDREN);
        }

        /// <summary>
        /// Removes theming
        /// </summary>
        public void RemoveTheme()
        {
            if (Environment.OSVersion.Version.Major > 4)
            {
                try
                {
                    if (IsAppThemed())
                        SetWindowTheme(this.ParentWnd, "", "");
                }
                catch { }
            }
        }

        /// <summary>
        /// Disposes this class. Done automatically when the form is closed.
        /// </summary>
        public void Dispose()
        {
            // reset frame to original size
            _bResetSize = true;
            RecalculateSize();
            _bResetSize = false;
            this.ReleaseHandle();
            _bStoreSize = false;
            _bFirstHit = false;
            // graphics
            if (_oCaptionBarBitmap != null)
                _oCaptionBarBitmap.Dispose();
            if (_oLeftFrameBitmap != null)
                _oLeftFrameBitmap.Dispose();
            if (_oRightFrameBitmap != null)
                _oRightFrameBitmap.Dispose();
            if (_oBottomFrameBitmap != null)
                _oBottomFrameBitmap.Dispose();
            if (_oMinimizeButtonBitmap != null)
                _oMinimizeButtonBitmap.Dispose();
            if (_oMaximizeButtonBitmap != null)
                _oMaximizeButtonBitmap.Dispose();
            if (_oRestoreButtonBitmap != null)
                _oRestoreButtonBitmap.Dispose();
            if (_oCloseButtonBitmap != null)
                _oCloseButtonBitmap.Dispose();
            if (_oHelpButtonBitmap != null)
                _oHelpButtonBitmap.Dispose();
            // temp dc
            if (_cCaptionBarDc != null)
                _cCaptionBarDc.Dispose();
            if (_cLeftFrameDc != null)
                _cLeftFrameDc.Dispose();
            if (_cRightFrameDc != null)
                _cRightFrameDc.Dispose();
            if (_cBottomFrameDc != null)
                _cBottomFrameDc.Dispose();
            if (_cMinimizeButtonDc != null)
                _cMinimizeButtonDc.Dispose();
            if (_cMaximizeButtonDc != null)
                _cMaximizeButtonDc.Dispose();
            if (_cRestoreButtonDc != null)
                _cRestoreButtonDc.Dispose();
            if (_cCloseButtonDc != null)
                _cRestoreButtonDc.Dispose();
            if (_cHelpButtonDc != null)
                _cHelpButtonDc.Dispose();
            // resources
            if (_oTitleFont != null)
                _oTitleFont.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Private Methods
        #region Helpers
        private RECT CalculateFrameSize(int x, int y, int cx, int cy)
        {
            RECT windowRect = new RECT(x, y, x + cx, y + cy);
            // subtract original frame size
            windowRect.Left -= _iFrameWidth;
            windowRect.Right += _iFrameWidth;
            windowRect.Top -= _iCaptionHeight;
            windowRect.Bottom += _iFrameHeight;
            // reset client area with new size
            windowRect.Left += (_oLeftFrameBitmap.Width / 2);
            windowRect.Right -= (_oRightFrameBitmap.Width / 2);
            windowRect.Bottom -= (_oBottomFrameBitmap.Height / 2);
            windowRect.Top += (_oCaptionBarBitmap.Height / 2);
            return windowRect;
        }

        private void FrameChanged()
        {
            RECT rcClient = new RECT();
            GetWindowRect(this.Handle, ref rcClient);
            // force a calc size message
            SetWindowPos(this.Handle,
                         IntPtr.Zero,
                         rcClient.Left, rcClient.Top,
                         rcClient.Right - rcClient.Left, rcClient.Bottom - rcClient.Top,
                         SWP_FRAMECHANGED);
        }

        private HIT_CONSTANTS HitTest()
        {
            RECT windowRect = new RECT();
            Point cursorPoint = new Point();

            GetCursorPos(ref cursorPoint);
            GetWindowRect(this.ParentWnd, ref windowRect);
            cursorPoint.X -= windowRect.Left;
            cursorPoint.Y -= windowRect.Top;

            if (this.CanClose)
            {
                if (PtInRect(ref _aButtonRect[2], cursorPoint))
                    return HIT_CONSTANTS.HTCLOSE;
                _iCaptionBoxStart = _aButtonRect[2].Left;
            }
            if (this.CanSize)
            {
                if (PtInRect(ref _aButtonRect[1], cursorPoint))
                    return HIT_CONSTANTS.HTMAXBUTTON;
                _iCaptionBoxStart = _aButtonRect[1].Left;
            }
            if (this.CanMinimize)
            {
                if (PtInRect(ref _aButtonRect[0], cursorPoint))
                    return HIT_CONSTANTS.HTMINBUTTON;
                _iCaptionBoxStart = _aButtonRect[0].Left;
            }
            if (this.CanHelp)
            {
                if (PtInRect(ref _aButtonRect[3], cursorPoint))
                    return HIT_CONSTANTS.HTHELP;
                _iCaptionBoxStart = _aButtonRect[3].Left;
            }
            windowRect = new RECT(_iCaptionBoxStart, _iFrameHeight, (windowRect.Right - windowRect.Left) - _iFrameWidth, _iCaptionHeight);
            if (PtInRect(ref windowRect, cursorPoint))
                return HIT_CONSTANTS.HTNOWHERE;

            return HIT_CONSTANTS.HTCLIENT;
        }

        private bool ImageCheck()
        {
            if (_oCaptionBarBitmap == null)
                return false;
            if (_oLeftFrameBitmap == null)
                return false;
            if (_oRightFrameBitmap == null)
                return false;
            if (_oBottomFrameBitmap == null)
                return false;
            if (this.CanMinimize)
            {
                if (_oMinimizeButtonBitmap == null)
                    return false;
            }
            if (this.CanSize)
            {
                if (_oMaximizeButtonBitmap == null)
                    return false;
                if (_oRestoreButtonBitmap == null)
                    return false;
            }
            if (this.CanClose)
            {
                if (_oCloseButtonBitmap == null)
                    return false;
            }
            return true;
        }

        private void InvalidateWindow(bool force)
        {
            if (!_bIsFrameInvalid || force)
            {
                RECT rc = new RECT();
                GetWindowRect(this.ParentWnd, ref rc);
                rc.Bottom -= rc.Top;
                rc.Top = 0;
                rc.Right -= rc.Left;
                rc.Left = 0;

                RedrawWindow(this.ParentWnd, ref rc, IntPtr.Zero, RDW_FRAME | RDW_UPDATENOW | RDW_INVALIDATE | RDW_NOCHILDREN);
                _bIsFrameInvalid = true;
            }
        }

        private void InvalidateClient()
        {
            RECT rc = new RECT();
            GetWindowRect(this.ParentWnd, ref rc);
            rc.Bottom =_iCaptionHeight + 4;
            rc.Top = 0;
            rc.Right -= rc.Left;
            rc.Left = 0;

            RedrawWindow(this.ParentWnd, ref rc, IntPtr.Zero, RDW_INVALIDATE | RDW_ALLCHILDREN);
        }

        private void EraseRect(IntPtr hWnd, RECT tRect, bool Erase)
        {
            IntPtr pStrct = Marshal.AllocHGlobal(Marshal.SizeOf(tRect));
            try
            {
                Marshal.StructureToPtr(tRect, pStrct, true);
                InvalidateRect(hWnd, pStrct, Erase);
            }
            finally
            {
                Marshal.FreeHGlobal(pStrct);
            }
        }

        private void LeftClick()
        {
            // required on older os
            if (Environment.OSVersion.Version.Major < 6)
            {
                RECT rc = new RECT();
                GetWindowRect(this.ParentWnd, ref rc);
                Point store = Cursor.Position;
                Cursor.Position = new System.Drawing.Point(rc.Left + 24, rc.Top + 5);
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
                Cursor.Position = store;
            }
        }

        private bool LeftKeyPressed()
        {
            if (MouseButtonsSwitched())
                return (GetKeyState(VK_RBUTTON) < 0);
            else
                return (GetKeyState(VK_LBUTTON) < 0);
        }

        private void MaximizeWindow()
        {
            if (this.WindowMaximized)
            {
                //windowMessage(POST_MESSAGES.SC_RESTORE);
                SetWindowPos(this.ParentWnd, IntPtr.Zero,
                    _tRestoreRect.Left, _tRestoreRect.Top,
                    _tRestoreRect.Right - _tRestoreRect.Left, _tRestoreRect.Bottom - _tRestoreRect.Top,
                    SWP_NOACTIVATE | SWP_NOOWNERZORDER);
                this.WindowMaximized = false;
            }
            else
            {
                RECT screenRect = new RECT();
                GetWindowRect(this.ParentWnd, ref _tRestoreRect);
                screenRect = ScreenSize();
                if (screenRect.Right > 0)
                {
                    InflateRect(ref screenRect, 8, 8);
                    OffsetRect(ref screenRect, -4, 4);
                    SetWindowPos(this.ParentWnd, IntPtr.Zero,
                        -4, -4,
                        screenRect.Right + 8, screenRect.Bottom,
                        SWP_NOACTIVATE | SWP_NOOWNERZORDER);
                }
                //windowMessage(POST_MESSAGES.SC_MAXIMIZE);
                this.WindowMaximized = true;
            }
            InvalidateWindow(true);
        }

        private bool MouseButtonsSwitched()
        {
            return (GetSystemMetrics(SYSTEM_METRIC.SM_SWAPBUTTON) != 0);
        }

        private RECT ScreenSize()
        {
            RECT windowRect = new RECT();
            try
            {
                SystemParametersInfo(SPI_GETWORKAREA, 0, ref windowRect, 0);
            }

            catch { }
            return windowRect;
        }

        private void RecalculateSize()
        {
            SetWindowPos(this.ParentWnd, IntPtr.Zero,
                0, 0, 0, 0,
                SWP_FRAMECHANGED | SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER);
        }

        private void StartTimer()
        {
            if (_buttonTimer > 0)
                StopTimer();
            SetTimer(this.ParentWnd, 66, 100, IntPtr.Zero);
        }

        private void StopTimer()
        {
            if (_buttonTimer > 0)
            {
                KillTimer(this.ParentWnd, 66);
                _buttonTimer = 0;
            }
        }

        private void WindowMessage(POST_MESSAGES message)
        {
            PostMessage(this.ParentWnd, (uint)WM_SYSCOMMAND, (IntPtr)message, IntPtr.Zero);
        }
        #endregion

        #region Drawing
        private void DrawWindow()
        {
            int imageWidth = 0;
            int imageHeight = 0;
            int drawWidth = 0;
            int drawHeight = 0;

            RECT windowRect = new RECT();
            GetWindowRect(this.ParentWnd, ref windowRect);
            OffsetRect(ref windowRect, -windowRect.Left, -windowRect.Top);
            int offset = this.Focused ? 0 : 1;
            cStoreDc windowDc = new cStoreDc();
            windowDc.Height = windowRect.Bottom;
            windowDc.Width = windowRect.Right;

            using (StretchMode mode = new StretchMode(windowDc.Hdc, StretchModeEnum.STRETCH_HALFTONE))
            {
                if ((this.ExcludeLeftEnd == 0) && (this.ExcludeRightEnd == 0))
                {
                    // top
                    imageWidth = _cCaptionBarDc.Width;
                    imageHeight = (_cCaptionBarDc.Height / 2);
                    drawWidth = windowRect.Right - ((_cLeftFrameDc.Width / 2) + (_cRightFrameDc.Width / 2));
                    drawHeight = (_cCaptionBarDc.Height / 2);
                    StretchBlt(windowDc.Hdc, (_cLeftFrameDc.Width / 2), 0, drawWidth, drawHeight, _cCaptionBarDc.Hdc, 0, (imageHeight * offset), imageWidth, imageHeight, 0xCC0020);
                }
                else
                {
                    int pieceOffset = 0;
                    int centerLength = windowDc.Width - ((_cLeftFrameDc.Width / 2) + (_cRightFrameDc.Width / 2));
                    // left exclusion area
                    if (this.ExcludeLeftEnd > 0)
                    {
                        // draw the start
                        if (this.ExcludeLeftStart > 0)
                        {
                            drawWidth = this.ExcludeLeftStart;
                            drawHeight = (_cCaptionBarDc.Height / 2);
                            pieceOffset = (_cLeftFrameDc.Width / 2);
                            StretchBlt(windowDc.Hdc, pieceOffset, 0, drawWidth, drawHeight, _cCaptionBarDc.Hdc, 0, (imageHeight * offset), drawWidth, drawHeight, 0xCC0020);
                        }
                        imageWidth = this.ExcludeLeftEnd - this.ExcludeLeftStart;
                        imageHeight = (_cCaptionBarDc.Height / 2);
                        pieceOffset = (_cLeftFrameDc.Width / 2) + this.ExcludeLeftStart;
                        BitBlt(windowDc.Hdc, pieceOffset, 0, imageWidth, imageHeight, _cCaptionBarDc.Hdc, this.ExcludeLeftStart, (imageHeight * offset), 0xCC0020);
                        centerLength -= this.ExcludeLeftEnd;
                    }
                    // right exclusion area
                    if (this.ExcludeRightEnd > 0)
                    {
                        imageWidth = this.ExcludeRightEnd - this.ExcludeRightStart;
                        imageHeight = (_cCaptionBarDc.Height / 2);
                        pieceOffset = windowDc.Width - ((_cRightFrameDc.Width / 2) + (_cCaptionBarDc.Width - this.ExcludeRightStart));
                        //if (pieceOffset > 0)
                        BitBlt(windowDc.Hdc, pieceOffset, 0, imageWidth, imageHeight, _cCaptionBarDc.Hdc, this.ExcludeRightStart, (imageHeight * offset), 0xCC0020);
                        centerLength -= imageWidth;
                    }
                    // draw the end
                    if (this.ExcludeRightEnd < _cCaptionBarDc.Width)
                    {
                        drawWidth = _cCaptionBarDc.Width - this.ExcludeRightEnd;
                        drawHeight = (_cCaptionBarDc.Height / 2);
                        pieceOffset += (this.ExcludeRightEnd - this.ExcludeRightStart);
                        StretchBlt(windowDc.Hdc, pieceOffset, 0, drawWidth, drawHeight, _cCaptionBarDc.Hdc, 0, (imageHeight * offset), drawWidth, drawHeight, 0xCC0020);
                        centerLength -= drawWidth;
                    }
                    // draw the center
                    if (this.ExcludeRightStart > 0)
                        imageWidth = _cCaptionBarDc.Width - (this.ExcludeRightStart - this.ExcludeLeftEnd);
                    else
                        imageWidth = _cCaptionBarDc.Width - this.ExcludeLeftEnd;

                    imageHeight = (_cCaptionBarDc.Height / 2);
                    pieceOffset = (_cLeftFrameDc.Width / 2) + this.ExcludeLeftEnd;
                    drawWidth = centerLength;
                    drawHeight = (_cCaptionBarDc.Height / 2);
                    StretchBlt(windowDc.Hdc, pieceOffset, 0, drawWidth, drawHeight, _cCaptionBarDc.Hdc, this.ExcludeLeftEnd, (imageHeight * offset), imageWidth, imageHeight, 0xCC0020);
                }

                // left
                imageWidth = (_cLeftFrameDc.Width / 2);
                imageHeight = _cLeftFrameDc.Height - ((_cCaptionBarDc.Height / 2) + (_cBottomFrameDc.Height / 2));
                drawWidth = (_cLeftFrameDc.Width / 2);
                drawHeight = windowRect.Bottom - ((_cCaptionBarDc.Height / 2) + (_cBottomFrameDc.Height / 2));
                StretchBlt(windowDc.Hdc, 0, (_cCaptionBarDc.Height / 2), drawWidth, drawHeight, _cLeftFrameDc.Hdc, (imageWidth * offset), (_cCaptionBarDc.Height / 2), imageWidth, imageHeight, 0xCC0020);
                //right
                imageWidth = (_cRightFrameDc.Width / 2);
                imageHeight = _cRightFrameDc.Height - ((_cCaptionBarDc.Height / 2) + (_cBottomFrameDc.Height / 2));
                drawWidth = (_cRightFrameDc.Width / 2);
                drawHeight = windowRect.Bottom - ((_cCaptionBarDc.Height / 2) + (_cBottomFrameDc.Height / 2));
                StretchBlt(windowDc.Hdc, (windowRect.Right - imageWidth), (_cCaptionBarDc.Height / 2), drawWidth, drawHeight, _cRightFrameDc.Hdc, (imageWidth * offset), (_cCaptionBarDc.Height / 2), imageWidth, imageHeight, 0xCC0020);
                // bottom
                imageWidth = _cBottomFrameDc.Width;
                imageHeight = (_cBottomFrameDc.Height / 2);
                drawWidth = windowRect.Right - ((_cLeftFrameDc.Width / 2) + (_cRightFrameDc.Width / 2));
                drawHeight = (_cBottomFrameDc.Height / 2);
                StretchBlt(windowDc.Hdc, (_cLeftFrameDc.Width / 2), windowRect.Bottom - (_cBottomFrameDc.Height / 2), drawWidth, drawHeight, _cBottomFrameDc.Hdc, 0, (imageHeight * offset), imageWidth, imageHeight, 0xCC0020);
            }
            // top left
            imageWidth = (_cLeftFrameDc.Width / 2);
            imageHeight = (_cCaptionBarDc.Height / 2);
            BitBlt(windowDc.Hdc, 0, 0, imageWidth, imageHeight, _cLeftFrameDc.Hdc, (imageWidth * offset), 0, 0xCC0020);
            // top right
            imageWidth = (_cRightFrameDc.Width / 2);
            imageHeight = (_cCaptionBarDc.Height / 2);
            BitBlt(windowDc.Hdc, (windowRect.Right - imageWidth), 0, imageWidth, imageHeight, _cRightFrameDc.Hdc, (imageWidth * offset), 0, 0xCC0020);
            // bottom left
            imageWidth = (_cLeftFrameDc.Width / 2);
            imageHeight = (_cBottomFrameDc.Height / 2);
            BitBlt(windowDc.Hdc, 0, (windowRect.Bottom - imageHeight), imageWidth, imageHeight, _cLeftFrameDc.Hdc, (imageHeight * offset), (_cLeftFrameDc.Height - imageHeight), 0xCC0020);
            // bottom right
            imageWidth = (_cRightFrameDc.Width / 2);
            imageHeight = (_cBottomFrameDc.Height / 2);
            BitBlt(windowDc.Hdc, (windowRect.Right - imageWidth), (windowRect.Bottom - imageHeight), imageWidth, imageHeight, _cRightFrameDc.Hdc, (imageHeight * offset), (_cRightFrameDc.Height - imageHeight), 0xCC0020);

            RECT captionRect = new RECT(0, 0, windowDc.Width, _cCaptionBarDc.Height / 2);

            DrawButtons(windowDc.Hdc, captionRect);
            DrawIcon(windowDc.Hdc, captionRect);
            DrawTitle(windowDc.Hdc, captionRect);

            // exclude client area
            imageWidth = (_cLeftFrameDc.Width / 2);
            imageHeight = (_cCaptionBarDc.Height / 2);
            //drawWidth = windowRect.Right - ((_leftFrameDc.Width / 2) + (_rightFrameDc.Width / 2));
            //drawHeight = windowRect.Bottom - ((_captionBarDc.Height / 2) + (_bottomFrameDc.Height / 2));
            drawWidth = ((_cLeftFrameDc.Width / 2) + (_cRightFrameDc.Width / 2));
            drawHeight = ((_cCaptionBarDc.Height / 2) + (_cBottomFrameDc.Height / 2));
            IntPtr hdc = GetWindowDC(this.ParentWnd);
            RECT clientRect = new RECT();
            //GetClientRect(this.ParentWnd, ref clientRect);
            GetWindowRect(this.ParentWnd, ref clientRect);
            OffsetRect(ref clientRect, -clientRect.Left, -clientRect.Top);
            OffsetRect(ref clientRect, -drawWidth, -drawHeight);
            //ExcludeClipRect(hdc, imageWidth, imageHeight, clientRect.Right + imageWidth, clientRect.Bottom + imageHeight);
            ExcludeClipRect(hdc, imageWidth, imageHeight, clientRect.Right + imageWidth, clientRect.Bottom + imageHeight);
            // blit to window
            BitBlt(hdc, 0, 0, windowRect.Right, windowRect.Bottom, windowDc.Hdc, 0, 0, 0xCC0020);
            // clean up
            ReleaseDC(this.ParentWnd, hdc);
            windowDc.Dispose();
        }

        private void DrawButtons(IntPtr hdc, RECT captionRect)
        {
            int offset = 0;
            int width = 0;
            int height = 0;
            int left = 0;
            HIT_CONSTANTS buttonHit = HitTest();

            if (this.CanClose)
            {
                // close button
                _aButtonRect[2].Right = captionRect.Right - _iButtonOffsetX;
                _aButtonRect[2].Left = _aButtonRect[2].Right - (_cCloseButtonDc.Width / 3);
                _aButtonRect[2].Top = (((_cCaptionBarDc.Height / 2) - _cCloseButtonDc.Height) / 2) + _iButtonOffsetY;
                _aButtonRect[2].Bottom = _aButtonRect[2].Top + _cCloseButtonDc.Height;

                if (buttonHit == HIT_CONSTANTS.HTCLOSE)
                {
                    if (LeftKeyPressed())
                        offset = (_cCloseButtonDc.Width / 3) * 2;
                    else
                        offset = (_cCloseButtonDc.Width / 3);
                }
                width = _aButtonRect[2].Right - _aButtonRect[2].Left;
                height = _cCloseButtonDc.Height;
                BitBlt(hdc, _aButtonRect[2].Left, _aButtonRect[2].Top, width, height, _cCloseButtonDc.Hdc, offset, 0, 0xCC0020);
                left = _aButtonRect[2].Left - width;
            }
            if (this.CanSize)
            {
                // maximize
                _aButtonRect[1].Right = _aButtonRect[2].Left - 1;
                _aButtonRect[1].Left = _aButtonRect[1].Right - (_cMaximizeButtonDc.Width / 3);
                _aButtonRect[1].Top = _aButtonRect[2].Top;
                _aButtonRect[1].Bottom = _aButtonRect[2].Bottom;
                width = _aButtonRect[1].Right - _aButtonRect[1].Left;
                height = _cMaximizeButtonDc.Height;
                offset = 0;
                if (buttonHit == HIT_CONSTANTS.HTMAXBUTTON)
                {
                    if (LeftKeyPressed())
                        offset = (_cMaximizeButtonDc.Width / 3) * 2;
                    else
                        offset = (_cMaximizeButtonDc.Width / 3);
                }
                if (this.WindowMaximized)
                    BitBlt(hdc, _aButtonRect[1].Left, _aButtonRect[1].Top, width, height, _cRestoreButtonDc.Hdc, offset, 0, 0xCC0020);
                else
                    BitBlt(hdc, _aButtonRect[1].Left, _aButtonRect[1].Top, width, height, _cMaximizeButtonDc.Hdc, offset, 0, 0xCC0020);
                left = _aButtonRect[1].Left - width;
            }
            if (this.CanMinimize)
            {
                // minimize
                if (this.CanSize)
                {
                    _aButtonRect[0].Right = _aButtonRect[1].Left - 1;
                    _aButtonRect[0].Left = _aButtonRect[0].Right - (_cMinimizeButtonDc.Width / 3);
                    _aButtonRect[0].Top = _aButtonRect[1].Top;
                    _aButtonRect[0].Bottom = _aButtonRect[1].Bottom;
                    width = _aButtonRect[0].Right - _aButtonRect[0].Left;
                    height = _cMinimizeButtonDc.Height;
                }
                else
                {
                    _aButtonRect[0].Right = _aButtonRect[2].Left - 1;
                    _aButtonRect[0].Left = _aButtonRect[0].Right - (_cMinimizeButtonDc.Width / 3);
                    _aButtonRect[0].Top = _aButtonRect[2].Top;
                    _aButtonRect[0].Bottom = _aButtonRect[2].Bottom;
                    width = _aButtonRect[0].Right - _aButtonRect[0].Left;
                    height = _cMinimizeButtonDc.Height;
                }
                offset = 0;
                if (buttonHit == HIT_CONSTANTS.HTMINBUTTON)
                {
                    if (LeftKeyPressed())
                        offset = (_cMinimizeButtonDc.Width / 3) * 2;
                    else
                        offset = (_cMinimizeButtonDc.Width / 3);
                }
                BitBlt(hdc, _aButtonRect[0].Left, _aButtonRect[0].Top, width, height, _cMinimizeButtonDc.Hdc, offset, 0, 0xCC0020);
                left = _aButtonRect[0].Left - width;
            }
            if (this.CanHelp)
            {
                // help/user button
                _aButtonRect[3].Left = left - 1;
                _aButtonRect[3].Right = _aButtonRect[3].Left + (_cHelpButtonDc.Width / 3);
                _aButtonRect[3].Top = _aButtonRect[2].Top;
                _aButtonRect[3].Bottom = _aButtonRect[2].Bottom;
                width = _aButtonRect[3].Right - _aButtonRect[3].Left;
                height = _cHelpButtonDc.Height;
                offset = 0;
                if (buttonHit == HIT_CONSTANTS.HTHELP)
                {
                    if (LeftKeyPressed())
                        offset = (_cHelpButtonDc.Width / 3) * 2;
                    else
                        offset = (_cHelpButtonDc.Width / 3);
                }
                BitBlt(hdc, _aButtonRect[3].Left, _aButtonRect[3].Top, width, height, _cHelpButtonDc.Hdc, offset, 0, 0xCC0020);
            }
        }

        private void DrawIcon(IntPtr hdc, RECT captionRect)
        {
            IntPtr hIcon = SendMessage(this.ParentWnd, WM_GETICON, IntPtr.Zero, IntPtr.Zero);
            _iIconSize = 0;
            if (hIcon != IntPtr.Zero)
            {
                _iIconSize = GetSystemMetrics(SYSTEM_METRIC.SM_CXSMICON);
                int top = (captionRect.Bottom - _iIconSize) / 2;
                int left = (_cLeftFrameDc.Width / 2);
                top += this.IconOffsetY;
                left += this.IconOffsetX;
                DrawIconEx(hdc, left, top, hIcon, _iIconSize, _iIconSize, 0, IntPtr.Zero, DI_NORMAL);
            }
        }

        private void DrawTitle(IntPtr hdc, RECT captionRect)
        {
            int len = GetWindowTextLength(this.ParentWnd);
            if (len > 0)
            {
                StringBuilder title = new StringBuilder(len + 1);
                GetWindowText(this.ParentWnd, title, title.Capacity);
                if (title.Length > 0)
                {
                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        Rectangle captionBounds = new Rectangle();
                        captionBounds.X = (_cLeftFrameDc.Width / 2) + ((_iIconSize > 0) ? _iIconSize + 4 : 0) + _iIconOffsetX;
                        captionBounds.Width = _iCaptionBoxStart - captionBounds.X;
                        captionBounds.Y = _iTitleOffsetY;
                        captionBounds.Height = captionRect.Bottom;
                        using (StringFormat sf = new StringFormat())
                        {
                            sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
                            sf.FormatFlags = StringFormatFlags.NoWrap;
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Center;
                            using (Brush ht = new SolidBrush(this.ForeColor))
                                g.DrawString(title.ToString(), _oTitleFont, ht, captionBounds, sf);
                        }
                    }
                }
            }

        }
        #endregion
        #endregion

        #region WndProc
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_INITMENU:
                    base.WndProc(ref m);
                    InvalidateWindow(false);
                    break;

                case WM_INITMENUPOPUP:
                case WM_EXITMENULOOP:
                    base.WndProc(ref m);
                    InvalidateWindow(false);
                    break;

                case WM_NCACTIVATE:
                    if (m.WParam == IntPtr.Zero)
                        m.Result = MESSAGE_HANDLED;
                    InvalidateWindow(false);
                    m.LParam = new IntPtr(-1);
                    //base.WndProc(ref m);
                    _bIsFrameInvalid = false;
                    InvalidateWindow(false);
                    InvalidateClient();
                    break;

                case WM_ACTIVATE:
                    if (!_bFirstHit && IsWindowVisible(this.ParentWnd))
                    {
                        _bFirstHit = true;
                        LeftClick();
                    }
                    base.WndProc(ref m);
                    InvalidateWindow(false);
                    InvalidateClient();
                    break;

                case WM_SETCURSOR:
                    m.Result = MESSAGE_PROCESS;
                    if (!this.WindowMaximized)
                        base.WndProc(ref m);
                    HIT_CONSTANTS hitCursor = (HIT_CONSTANTS)DefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam);
                    if (_eLastCursorHit != hitCursor)
                    {
                        _eLastCursorHit = hitCursor;
                        InvalidateWindow(true);
                    }
                    break;

                case WM_NCCALCSIZE:
                    if (m.WParam != IntPtr.Zero)
                    {
                        NCCALCSIZE_PARAMS ncsize = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(NCCALCSIZE_PARAMS));
                        WINDOWPOS wp = (WINDOWPOS)Marshal.PtrToStructure(ncsize.lppos, typeof(WINDOWPOS));
                        // store original frame sizes
                        if (!_bStoreSize)
                        {
                            _bStoreSize = true;
                            _iCaptionHeight = ncsize.rect2.Top - ncsize.rect0.Top;
                            _iFrameHeight = ncsize.rect0.Bottom - ncsize.rect2.Bottom;
                            _iFrameWidth = ncsize.rect2.Left - ncsize.rect0.Left;
                        }
                        if (!_bResetSize)
                        {
                            ncsize.rect0 = CalculateFrameSize(wp.x, wp.y, wp.cx, wp.cy);
                            ncsize.rect1 = ncsize.rect0;
                        }
                        Marshal.StructureToPtr(ncsize, m.LParam, false);
                        m.Result = (IntPtr)WVR_VALIDRECTS;
                    }
                    else
                    {
                        RECT rc = (RECT)m.GetLParam(typeof(RECT));
                        rc = CalculateFrameSize(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top); ;
                        Marshal.StructureToPtr(rc, m.LParam, true);
                        m.Result = MESSAGE_PROCESS;
                    }
                    base.WndProc(ref m);
                    break;

                case WM_SYSCOMMAND:
                    {
                        UInt32 param;
                        if (IntPtr.Size == 4)
                            param = (UInt32)(m.WParam.ToInt32());
                        else
                            param = (UInt32)(m.WParam.ToInt64());
                        Form f = (Form)Form.FromHandle(_hParentWnd);
                        if (f.FormBorderStyle == FormBorderStyle.Sizable || f.FormBorderStyle == FormBorderStyle.SizableToolWindow)
                            _bIsSizeable = true;
                        if ((param & 0xFFF0) == SC_RESTORE)
                        {
                            f.Height = _szStoreSize.Height;
                            f.Width = _szStoreSize.Width;
                            _bFirstHit = false;
                        }
                        else if (f.WindowState == FormWindowState.Normal)
                        {
                            _szStoreSize = new Size(f.Width, f.Height);
                            InvalidateClient();
                            //ForcedRefresh();
                        }
                        base.WndProc(ref m);
                        break;
                    }

                case WM_NCDESTROY:
                    Dispose();
                    base.WndProc(ref m);
                    break;

                case WM_NCHITTEST:
                    _eLastWindowHit = (HIT_CONSTANTS)DefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam);
                    _eLastButtonHit = HitTest();
                    if ((_eLastButtonHit == HIT_CONSTANTS.HTCLOSE) ||
                        (_eLastButtonHit == HIT_CONSTANTS.HTMAXBUTTON) ||
                        (_eLastButtonHit == HIT_CONSTANTS.HTMINBUTTON) ||
                        (_eLastButtonHit == HIT_CONSTANTS.HTHELP) ||
                        (_eLastButtonHit == HIT_CONSTANTS.HTNOWHERE))
                    {
                        if (this.SupressHelpTip && _eLastButtonHit == HIT_CONSTANTS.HTHELP && !LeftKeyPressed())
                            m.Result = (IntPtr)HIT_CONSTANTS.HTCAPTION;
                        else
                            m.Result = (IntPtr)_eLastButtonHit;
                    }
                    else
                    {
                        m.Result = (IntPtr)_eLastWindowHit;
                        base.WndProc(ref m);
                    }
                    break;

                case WM_NCRBUTTONDBLCLK:
                case WM_NCLBUTTONDBLCLK:
                    if (((MouseButtonsSwitched()) && (m.Msg == WM_NCRBUTTONDBLCLK)) ||
                        ((!MouseButtonsSwitched()) && (m.Msg == WM_NCLBUTTONDBLCLK)))
                    {
                        if (_eLastWindowHit == HIT_CONSTANTS.HTCAPTION && _bIsSizeable)
                            MaximizeWindow();
                        else
                            base.WndProc(ref m);
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }
                    break;

                case WM_NCRBUTTONDOWN:
                case WM_NCLBUTTONDOWN:
                    if (((MouseButtonsSwitched()) && (m.Msg == WM_NCRBUTTONDOWN)) ||
                        ((!MouseButtonsSwitched()) && (m.Msg == WM_NCLBUTTONDOWN)))
                    {
                        _eLastButtonHit = HitTest();
                        if ((_eLastButtonHit == HIT_CONSTANTS.HTCLOSE) ||
                            (_eLastButtonHit == HIT_CONSTANTS.HTMAXBUTTON) ||
                            (_eLastButtonHit == HIT_CONSTANTS.HTMINBUTTON) ||
                            (_eLastButtonHit == HIT_CONSTANTS.HTHELP))
                        {
                            m.Result = (IntPtr)MESSAGE_HANDLED;
                        }
                        else
                        {
                            if ((_eLastWindowHit == HIT_CONSTANTS.HTCLOSE) ||
                                (_eLastWindowHit == HIT_CONSTANTS.HTMAXBUTTON) ||
                                (_eLastWindowHit == HIT_CONSTANTS.HTMINBUTTON) ||
                                (_eLastButtonHit == HIT_CONSTANTS.HTHELP))
                            {
                                InvalidateWindow(true);
                            }
                            else
                            {
                                base.WndProc(ref m);
                                InvalidateWindow(true);
                            }
                        }
                    }
                    else
                    {
                        base.WndProc(ref m);
                        InvalidateWindow(true);
                    }
                    break;

                case WM_NCRBUTTONUP:
                case WM_NCLBUTTONUP:
                    if (((MouseButtonsSwitched()) && (m.Msg == WM_NCRBUTTONUP)) ||
                        ((!MouseButtonsSwitched()) && (m.Msg == WM_NCLBUTTONUP)))
                    {
                        if (_eLastButtonHit == HIT_CONSTANTS.HTCLOSE)
                            WindowMessage(POST_MESSAGES.SC_CLOSE);
                        else if (_eLastButtonHit == HIT_CONSTANTS.HTMINBUTTON)
                            WindowMessage(POST_MESSAGES.SC_MINIMIZE);
                        else if (_eLastButtonHit == HIT_CONSTANTS.HTMAXBUTTON)
                            MaximizeWindow();
                        else if (_eLastButtonHit == HIT_CONSTANTS.HTHELP)
                        {
                            if (HelpClicked != null)
                                HelpClicked(this, new EventArgs());
                        }
                        else
                            base.WndProc(ref m);
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }
                    InvalidateWindow(true);
                    break;

                case WM_NCMOUSEMOVE:
                    _eLastButtonHit = HitTest();
                    if ((_eLastButtonHit == HIT_CONSTANTS.HTCLOSE) ||
                        (_eLastButtonHit == HIT_CONSTANTS.HTMAXBUTTON) ||
                        (_eLastButtonHit == HIT_CONSTANTS.HTMINBUTTON) ||
                        (_eLastButtonHit == HIT_CONSTANTS.HTHELP))
                    {
                        StartTimer();
                        InvalidateWindow(true);
                    }
                    base.WndProc(ref m);
                    break;

                case WM_NCPAINT:
                    if (IsWindowVisible(this.ParentWnd))
                        DrawWindow();
                    _bIsFrameInvalid = false;
                    break;

                case WM_TIMER:
                    _buttonTimer += 1;
                    HIT_CONSTANTS hitTimer = HitTest();
                    if ((hitTimer == HIT_CONSTANTS.HTCLOSE) ||
                        (hitTimer == HIT_CONSTANTS.HTMAXBUTTON) ||
                        (hitTimer == HIT_CONSTANTS.HTMINBUTTON) ||
                        (hitTimer == HIT_CONSTANTS.HTHELP))
                    {
                        if (hitTimer != _eLastButtonHit)
                        {
                            StopTimer();
                            InvalidateWindow(true);
                        }
                        else
                        {
                            if (_buttonTimer > 500)
                                StopTimer();
                        }
                    }
                    else
                    {
                        if (!LeftKeyPressed())
                        {
                            StopTimer();
                            InvalidateWindow(true);
                        }
                    }
                    base.WndProc(ref m);
                    break;

                case WM_ACTIVATEAPP:
                case WM_KILLFOCUS:
                case WM_SETFOCUS:
                case WM_DISPLAYCHANGE:
                case WM_STYLECHANGED:
                case WM_SETTEXT:
                    InvalidateWindow(true);
                    InvalidateClient();
                    base.WndProc(ref m);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion
    }
}
