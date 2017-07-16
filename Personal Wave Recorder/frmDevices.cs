#region Directives
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.Drawing.Drawing2D;
using WaveLib;
using RCML;
#endregion

namespace Personal_Wave_Recorder
{
    public partial class frmDevices : Form
    {
        #region Fields
        private RCM _cRcm;
        #endregion

        #region Events/Delegates
        public delegate void ChangedDelegate();
        public event ChangedDelegate PropertyChanged;
        public delegate void ClosingDelegate();
        public event ClosingDelegate DeviceClosing;
        #endregion

        #region Properties
        private int InputDevice { get; set; }
        private int OutputDevice { get; set; }
        #endregion

        #region Form
        public frmDevices()
        {
            InitializeComponent();
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            LoadFrame();
            _cRcm.Start();
            pnlFill.Top = -2;
            pnlFill.Left = -1;
            pnlFill.Width = this.Width;
            pnlFill.Height = this.Height;
            lblInput.Parent = pnlFill;
            lblOutput.Parent = pnlFill;
            this.InputDevice = Properties.Settings.Default.SettingAudioInputDevice;
            this.OutputDevice = Properties.Settings.Default.SettingAudioOutputDevice;
            GetOutputDevices();
            GetInputDevices();
        }

        private void frmOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DeviceClosing != null)
                this.DeviceClosing();
            this.Owner.Focus();
        }
        #endregion

        #region Methods
        private void GetOutputDevices()
        {
            WaveOutPlayer wp = new WaveOutPlayer();

            uint dv = wp.GetDeviceCount();
            if (dv != 0)
            {
                string prod = "";
                for (uint x = 0; x < dv; x++)
                {
                    wp.GetOutputDeviceName(x, ref prod);
                    cbOutputDevices.Items.Add(prod);
                }
                cbOutputDevices.SelectedIndex = 0;
                wp.GetOutputDeviceName((uint)this.OutputDevice, ref prod);
                cbOutputDevices.Text = prod;
            }
            wp.Dispose();
        }

        private void GetInputDevices()
        {
            WaveInRecorder wr = new WaveInRecorder();
            int dv = wr.GetDeviceCount();
            if (dv != 0)
            {
                string prod = "";
                for (uint x = 0; x < dv; x++)
                {
                    wr.GetInputDeviceName(x, ref prod);
                    cbInputDevices.Items.Add(prod);
                }
                cbInputDevices.SelectedIndex = 0;
                wr.GetInputDeviceName((uint)this.InputDevice, ref prod);
                cbInputDevices.Text = prod;
            }
            wr.Dispose();
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
        #endregion

        #region Event Handlers
        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SettingAudioInputDevice = this.InputDevice;
            Properties.Settings.Default.SettingAudioOutputDevice = this.OutputDevice;
            btnSave.Enabled = false;
            btnCancel.Text = "Close";
            if (this.PropertyChanged != null)
                this.PropertyChanged();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            Button bt = (Button)sender;
            bt.BackgroundImage = Properties.Resources.btpress;
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            bt.BackgroundImage = Properties.Resources.btfocused;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            bt.BackgroundImage = Properties.Resources.btnorm;
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            Button bt = (Button)sender;
            bt.BackgroundImage = Properties.Resources.btnorm;
        }

        private void cbOutputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OutputDevice = cbOutputDevices.SelectedIndex;
            btnSave.Enabled = true;
            btnCancel.Text = "Cancel";
        }

        private void cbInputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InputDevice = cbInputDevices.SelectedIndex;
            btnSave.Enabled = true;
            btnCancel.Text = "Cancel";
        }
        #endregion
    }
}
