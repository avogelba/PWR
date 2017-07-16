using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using RCML;

namespace Personal_Wave_Recorder
{
    public partial class frmError : Form
    {
        private RCM _cRcm;
        public void ErrorText(string caption, string title, string error)
        {
            this.Text = caption;
           // _cRcm.ForcedRefresh();
            lblTitle.Text = title;
            lblMessage.Text = error;
        }

        public frmError()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmError_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Focus();
        }

        private void frmError_Load(object sender, EventArgs e)
        {
            LoadFrame();
            _cRcm.Start();
            pnlFill.Top = -2;
            pnlFill.Left = -1;
            pnlFill.Width = this.Width;
            pnlFill.Height = this.Height;
            lblTitle.Parent = pnlFill;
            lblMessage.Parent = pnlFill;
        }

        private void LoadFrame()
        {
            if (_cRcm != null)
                _cRcm.Dispose();
            _cRcm = new RCM(this.Handle);
            _cRcm.LeftFrameGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_FrameLeft;
            _cRcm.RightFrameGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_FrameRight;
            _cRcm.CaptionBarGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_FrameTop;
            _cRcm.BottomFrameGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_FrameBottom;
            _cRcm.CloseButtonGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_closebutton;
            _cRcm.MaximizeButtonGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_maxbutton;
            _cRcm.RestoreButtonGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_restorebutton;
            _cRcm.MinimizeButtonGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_minbutton;
        }

        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.btpress;
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.btfocused;

        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.btnorm;
        }

        private void btnClose_MouseUp(object sender, MouseEventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.btnorm;
        }
    }
}
