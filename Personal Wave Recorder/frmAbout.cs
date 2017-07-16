using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using RCML;

namespace Personal_Wave_Recorder
{
    public partial class frmAbout : Form
    {
        private RCML.RCM _cRcm;

        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            _cRcm = new RCM(this.Handle);
            LoadFrameImages();
            _cRcm.Start();
            pnlFill.Top = -2;
            pnlFill.Left = -1;
            pnlFill.Width = this.Width;
            pnlFill.Height = this.Height;
            lblCopy.Parent = pnlFill;
            lblMain.Parent = pnlFill;
            lblTitle.Parent = pnlFill;
        }

        private void frmAbout_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Focus();
        }

        private void LoadFrameImages()
        {
            _cRcm.LeftFrameGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_FrameLeft;
            _cRcm.RightFrameGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_FrameRight;
            _cRcm.CaptionBarGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_FrameTop;
            _cRcm.BottomFrameGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_FrameBottom;
            _cRcm.CloseButtonGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_closebutton;
            _cRcm.MaximizeButtonGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_maxbutton;
            _cRcm.RestoreButtonGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_restorebutton;
            _cRcm.MinimizeButtonGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_minbutton;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
