#region Author/About
    /************************************************************************************
    *  PWR -Personal Wav Recorder v1.1                                                  *
    *                                                                                   *
    *  Created:     March 12, 2010                                                      *
    *  Built on:    Win7                                                                *
    *  Purpose:     Wav Player and Recorder                                             *
    *  Revision:    1.1                                                                 *
    *  Tested On:   Win7 32bit, Vista 64bit, XP Professional                            *
    *  IDE:         C# 2008 SP1 FW 3.5                                                  *
    *  Referenced:  VTD Freeware                                                        *
    *  Author:      John Underhill (Steppenwolfe)                                       *
    *                                                                                   *
    *************************************************************************************

    You can not:
    -Sell or redistribute this code or the binary for profit. This is freeware.
    -Use this application, or porions of this code in spyware, malware, or any generally acknowledged form of malicious software.
    -Remove or alter the above author accreditation, or this disclaimer.

    You can:
    -Use this code as a reference basis to build your own applications, (like ex. an MP3 player).
    -Use portions of this code including the controls in your own projects or commercial applications.

    I will not:
    -Except any responsibility for this code whatsoever.
    -Modify on demand.. you have the source code, read it, learn from it, write it.
    -There is no guarantee of fitness, nor should you have any expectation of support. 
    -I further renounce any and all responsibilities for this code, in every way conceivable, 
    now, and for the rest of time.
    
    Cheers,
    John
    steppenwolfe_2000@yahoo.com
    
    changes to v1.1 
    -fixed win7 font issue, (thought arial narrow was native to win7.. darn adobe!)
    -fixed title and stats missing when loading from menu
    -eq reworked for scroll
    -fixed a byte alignment mistake in ProcessEq
    -some graphics changes made (tip/vol hover)
    -changed input buffer size in AcmStreamOut:PreConvert loop to remove empty bytes
    -eq seems to have distortion problems with high energy signal (48k/16bit), 
    may require a max-amplitude cap in IIR, gain throttled on 16bit as a temporary fix
    */
#endregion

#region Directives
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WaveLib;
using RCML;
#endregion

namespace Personal_Wave_Recorder
{
    public partial class frmMain : Form
    {
        #region Enums
        private enum ApplicationState
        {
            Idle,
            Playing,
            PlayPause,
            RecorderLoaded,
            Recording,
            RecordPause,
        }
        #endregion

        #region Constants
        private Color SELECTED_COLOR = Color.FromArgb(96, 96, 96);
        #endregion

        #region Fields
        private byte[] _btPlayBuffer;
        private byte[] _btRecBuffer;
        private int _iBaseHeight = 153;
        private DrawDomains _cDrawDomains = new DrawDomains();
        private WaveOutPlayer _cPlayer;
        private WaveInRecorder _cRecorder;
        private WAVEFORMATEX _cWaveFormat;
        private MemoryStream _cStreamMemory;
        private RCM _cRcm;
        private CustomToolTip  _cCustomTip;
        private ContextMenuRenderer _cMenuRenderer;
        // eq
        private IIR _eqBands = new IIR();
        private unsafe biquad* _bq100Left;
        private unsafe biquad* _bq200Left;
        private unsafe biquad* _bq400Left;
        private unsafe biquad* _bq800Left;
        private unsafe biquad* _bq1600Left;
        private unsafe biquad* _bq3200Left;
        private unsafe biquad* _bq6400Left;
        private unsafe biquad* _bq100Right;
        private unsafe biquad* _bq200Right;
        private unsafe biquad* _bq400Right;
        private unsafe biquad* _bq800Right;
        private unsafe biquad* _bq1600Right;
        private unsafe biquad* _bq3200Right;
        private unsafe biquad* _bq6400Right;
        private unsafe biquad* _bqLPF;
        private unsafe biquad* _bqHPF;
        private int _iLastEqPos = 0;
        #endregion

        #region Properties
        // controls
        private bool IsEqOn { get; set; }
        private bool IsEqShowing { get; set; }
        private bool IsRepeatOn { get; set; }
        private bool IsLowPassOn { get; set; }
        private bool IsHighPassOn { get; set; }
        private bool IsMuted { get; set; }
        private bool IsGraphTimeDomain { get; set; }
        private bool IsPositionChanging { get; set; }
        private bool IsHoverOverPosition { get; set; }
        private bool IsPropertyChanged { get; set; }
        private string FileName { get; set; }
        private ApplicationState State { get; set; }
        // player
        private bool IsPlayerLoaded { get; set; }
        private int AudioOutputDevice { get; set; }
        // common
        private uint AvgBytesPerSecond { get; set; }
        private ushort BitsPerSample { get; set; }
        private ushort Channels { get; set; }
        private int SamplesPerSecond { get; set; }
        private uint BufferCount { get; set; }
        private uint BufferSize { get; set; }
        // recorder
        private bool IsRecorderLoaded { get; set; }
        private int AudioInputDevice { get; set; }
        #endregion

        #region Events/Delegates
        private void _cRcm_HelpClicked(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.Show(this);
        }
        private delegate void SetDrawCallback(byte[] data);
        private delegate void ResetCallback();
        private delegate void PositionChangedCallback();
        private delegate void ProcessCallback(ref byte[] buffer, IntPtr data);
        #endregion

        #region Controls
        #region Buttons
        private void btnEqRollup_MouseClick(object sender, MouseEventArgs e)
        {
            _cCustomTip.Stop();
            if (btnEqRollup.Checked)
                this.Height = _iBaseHeight + 95;
            else
                this.Height = _iBaseHeight;
        }

        private void btnFastForward_MouseClick(object sender, MouseEventArgs e)
        {
            _cCustomTip.Stop();
            msPosition.Value += msPosition.LargeChange;
            if (_cStreamMemory != null && _cPlayer != null)
                _cStreamMemory.Position = msPosition.Value * this.BufferSize;
        }

        private void btnMute_MouseClick(object sender, MouseEventArgs e)
        {
            _cCustomTip.Stop();
            this.IsMuted = !this.IsMuted;
            btnMute.Checked = this.IsMuted;
            mnuItemMute.Checked = this.IsMuted;
            LoadVolume();
        }

        private void btnPlay_MouseClick(object sender, MouseEventArgs e)
        {
            _cCustomTip.Stop();
            if (btnPlay.Enabled)
            {
                if (this.State == ApplicationState.Idle)
                {
                    if (string.IsNullOrEmpty(this.FileName))
                    {
                        string filename = OpenFile();
                        if (filename.Length > 4)
                        {
                            this.FileName = filename;
                            if (PlayerLoad())
                                Play();
                        }
                    }
                    else
                    {
                        if (PlayerLoad())
                            Play();
                    }
                }
                else if (this.State == ApplicationState.Playing)
                {
                    PlayerPause();
                }
                else if (this.State == ApplicationState.PlayPause)
                {
                    PlayerResume();
                }
                if (this.State == ApplicationState.Playing)
                    btnPlay.Image = Personal_Wave_Recorder.Properties.Resources.pause;
                else
                    btnPlay.Image = Personal_Wave_Recorder.Properties.Resources.play;
            }
        }

        private void btnRecord_MouseClick(object sender, MouseEventArgs e)
        {
            _cCustomTip.Stop();
            if (btnRecord.Enabled)
            {
                if (this.State == ApplicationState.Idle)
                {
                    if (RecorderLoad())
                    {
                        Record();
                        btnRecord.Checked = true;
                    }
                    else
                    {
                        //error
                        btnRecord.Checked = false;
                    }

                }
                else if (this.State == ApplicationState.Recording)
                {
                    RecordingPause();
                    btnRecord.Checked = true;
                }
                else if (this.State == ApplicationState.RecordPause)
                {
                    RecordingResume();
                    btnRecord.Checked = true;
                }
                if (this.State == ApplicationState.Recording)
                    btnRecord.Image = Personal_Wave_Recorder.Properties.Resources.pause;
                else
                    btnRecord.Image = Personal_Wave_Recorder.Properties.Resources.record;
            }
        }

        private void btnRewind_MouseClick(object sender, MouseEventArgs e)
        {
            _cCustomTip.Stop();
            msPosition.Value -= msPosition.LargeChange;
            if (_cStreamMemory != null && _cPlayer != null)
                _cStreamMemory.Position = msPosition.Value * this.BufferSize;
        }

        private void btnStop_MouseClick(object sender, MouseEventArgs e)
        {
            _cCustomTip.Stop();
            if (this.IsPlayerLoaded)
                PlayerUnLoad();
            if (this.IsRecorderLoaded)
                RecordingStop();
        }
        #endregion

        #region Context Menu
        private void mnuItemEqualizer_Click(object sender, EventArgs e)
        {
            lblEq_Click((object)this.lblEq, new EventArgs());
        }

        private void mnuItemExit_Click(object sender, EventArgs e)
        {
            if (this.IsPlayerLoaded)
                PlayerUnLoad();
            if (this.IsRecorderLoaded)
                RecorderUnLoad();
            Application.Exit();
        }

        private void mnuItemHighPass_Click(object sender, EventArgs e)
        {
            lblHighPass_Click((object)this.lblHighPass, new EventArgs());
        }

        private void mnuItemLowPass_Click(object sender, EventArgs e)
        {
            lblLowPass_Click((object)this.lblLowPass, new EventArgs());
        }

        private void mnuItemMute_Click(object sender, EventArgs e)
        {
            this.IsMuted = !this.IsMuted;
            mnuItemMute.Checked = this.IsMuted;
            btnMute.Checked = this.IsMuted;
            LoadVolume();
        }

        private void mnuItemOpen_Click(object sender, EventArgs e)
        {
            if (this.IsPlayerLoaded)
                PlayerUnLoad();
            if (this.IsRecorderLoaded)
                RecorderUnLoad();
            string filename = OpenFile();
            if (filename.Length > 4)
                this.FileName = filename;
        }

        private void mnuItemOptions_Click(object sender, EventArgs e)
        {
            frmDevices f = new frmDevices();
            f.PropertyChanged += new frmDevices.ChangedDelegate(Device_PropertyChanged);
            f.Show(this);
        }

        private void Device_PropertyChanged()
        {
            this.AudioInputDevice = Properties.Settings.Default.SettingAudioInputDevice;
            this.AudioOutputDevice = Properties.Settings.Default.SettingAudioOutputDevice;
        }

        private void mnuItemRepeat_Click(object sender, EventArgs e)
        {
            lblRepeat_Click((object)this.lblRepeat, new EventArgs());
        }

        private void mnuItemSave_Click(object sender, EventArgs e)
        {
            if (this.IsRecorderLoaded && _cStreamMemory != null && _cStreamMemory.Length > 0)
                RecordingStop();
        }

        private void mnuItemSaveAs_Click(object sender, EventArgs e)
        {
            if (this.IsRecorderLoaded && _cStreamMemory != null && _cStreamMemory.Length > 0)
            {
                this.FileName = "";
                RecordingStop();
            }
        }
        #endregion

        #region Form
        /// <summary>Load Controls</summary>
        public frmMain()
        {
            InitializeComponent();
            // load menu renderer
            _cMenuRenderer = new ContextMenuRenderer();
            _cMenuRenderer.SeperatorInnerColor = Color.FromArgb(64, 64, 64);
            _cMenuRenderer.SeperatorOuterColor = Color.FromArgb(96, 96, 96);
            _cMenuRenderer.MenuImageMarginColor = Color.FromArgb(48, 48, 48);
            mnuMain.Renderer = _cMenuRenderer;
            // load tool tips
            _cCustomTip = new CustomToolTip(this.Handle);
            _cCustomTip.GradientBegin = Color.FromArgb(220, 64, 64, 64);
            _cCustomTip.GradientEnd = Color.FromArgb(220, Color.Black);
            _cCustomTip.ForeColor = Color.LightGray;
            // eq defaults
            LoadEq();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // load rcm
            _cRcm = new RCM(this.Handle);
            _cRcm.HelpClicked += new RCM.HelpClickedDelegate(_cRcm_HelpClicked);
            LoadFrameImages();
            _cRcm.Start();
            CalculateRelativeHeight();
            
            // file open dialog
            ofdFile.Filter = "Wav Files|*.wav";
            ofdFile.InitialDirectory = Application.StartupPath;
            ofdFile.FilterIndex = 1;
            ofdFile.RestoreDirectory = true;
            ofdFile.DefaultExt = ".wav";
            ofdFile.Title = "Select a Wav file";
            // file save dialog
            sfdSave.Title = "Choose a File Name";
            sfdSave.ValidateNames = true;
            sfdSave.InitialDirectory = Application.StartupPath;
            sfdSave.RestoreDirectory = true;
            sfdSave.DefaultExt = ".wav";
            sfdSave.Filter = "Wav Files|*.wav";
            // set up graphs
            _cDrawDomains.LeftPicture(pbLeftGraph.Handle, pbLeftGraph.Width, pbLeftGraph.Height);
            _cDrawDomains.RightPicture(pbRightGraph.Handle, pbRightGraph.Width, pbRightGraph.Height);
            // load user settings
            LoadSettings();
            // for xp
          //  LoadFonts();
            // boost priority
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.AboveNormal;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_cCustomTip != null)
                _cCustomTip.Dispose();
            _cCustomTip = null;
            if (this.IsPlayerLoaded)
                PlayerUnLoad();
            if (this.IsRecorderLoaded)
                RecordingStop();
            UnloadEq();
            SaveSettings();
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
            _cRcm.HelpButtonGraphic = Personal_Wave_Recorder.Properties.Resources.vienna_helpbutton;
        }
        #endregion

        #region Labels
        private void Label_MouseDown(object sender, MouseEventArgs e)
        {
            _cCustomTip.Stop();
        }

        private void lblEq_Click(object sender, EventArgs e)
        {
            this.IsEqOn = !this.IsEqOn;
            mnuItemEqualizer.Checked = this.IsEqOn;
            if (this.IsEqOn)
            {
                lblEq.BackColor = SELECTED_COLOR;
                lblEq.Tag = "Equalizer : On";
            }
            else
            {
                lblEq.BackColor = Color.Transparent;
                lblEq.Tag = "Equalizer : Off";
            }
        }

        private void lblHighPass_Click(object sender, EventArgs e)
        {
            this.IsHighPassOn = !this.IsHighPassOn;
            mnuItemHighPass.Checked = this.IsHighPassOn;
            if (this.IsHighPassOn)
            {
                lblHighPass.BackColor = SELECTED_COLOR;
                lblHighPass.Tag = "High Band Boost : Off";
            }
            else
            {
                lblHighPass.BackColor = Color.Transparent;
                lblHighPass.Tag = "High Band Boost : Off";
            }
        }

        private void lblLowPass_Click(object sender, EventArgs e)
        {
            this.IsLowPassOn = !this.IsLowPassOn;
            mnuItemLowPass.Checked = this.IsLowPassOn;
            if (this.IsLowPassOn)
            {
                lblLowPass.BackColor = SELECTED_COLOR;
                lblLowPass.Tag = "Low Pass Filter : On";
            }
            else
            {
                lblLowPass.BackColor = Color.Transparent;
                lblLowPass.Tag = "Low Pass Filter : Off";
            }
        }

        private void lblRepeat_Click(object sender, EventArgs e)
        {
            this.IsRepeatOn = !this.IsRepeatOn;
            mnuItemRepeat.Checked = this.IsRepeatOn;
            if (this.IsRepeatOn)
            {
                lblRepeat.BackColor = SELECTED_COLOR;
                lblRepeat.Tag = "Repeat Selection : On";
            }
            else
            {
                lblRepeat.BackColor = Color.Transparent;
                lblRepeat.Tag = "Repeat Selection : Off";
            }
            _cCustomTip.Stop();
        }

        private void pnlEq_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuMain.Show(pnlEq, e.X, e.Y);
            }
        }
        #endregion

        #region Sliders
        #region Volume and Position
        private void msVolume_MouseHover(object sender, EventArgs e)
        {
            msVolume.ButtonBorderColor = Color.LightSkyBlue;
        }

        private void msVolume_MouseLeave(object sender, EventArgs e)
        {
            msVolume.ButtonBorderColor = Color.FromArgb(64, 64, 64);
        }

        private void msVolume_ValueChanged(object sender, EventArgs e)
        {
            SetVolume(msVolume.Value);
        }

        private void msPosition_MouseDown(object sender, MouseEventArgs e)
        {
            IsPositionChanging = true;
        }

        private void msPosition_MouseUp(object sender, MouseEventArgs e)
        {
            IsPositionChanging = false;
            if (_cStreamMemory != null)
                _cStreamMemory.Position = msPosition.Value * this.BufferSize;
        }

        private void msPosition_MouseHover(object sender, EventArgs e)
        {
            if (_cPlayer != null)
                IsHoverOverPosition = true;
        }

        private void msPosition_MouseLeave(object sender, EventArgs e)
        {
            IsHoverOverPosition = false;
        }

        private void msPosition_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsHoverOverPosition)
                lblPosition.Text = GetPositionTime(msPosition.ValueFromPosition());
        }
        #endregion
        
        #region Equalizer
        private void msLeftFreq100_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq100Left, msLeftFreq100.Value, 100); }
        }

        private void msLeftFreq200_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq200Left, msLeftFreq200.Value, 200); }
        }

        private void msLeftFreq400_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq400Left, msLeftFreq400.Value, 400); }
        }

        private void msLeftFreq800_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq800Left, msLeftFreq800.Value, 800); }
        }

        private void msLeftFreq1600_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq1600Left, msLeftFreq1600.Value, 1600); }
        }

        private void msLeftFreq3200_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq3200Left, msLeftFreq3200.Value, 3200); }
        }

        private void msLeftFreq6400_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq6400Left, msLeftFreq6400.Value, 6400); }
        }

        private void msRightFreq100_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq100Right, msRightFreq100.Value, 100); }
        }

        private void msRightFreq200_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq200Right, msRightFreq200.Value, 200); }
        }

        private void msRightFreq400_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq400Right, msRightFreq400.Value, 400); }
        }

        private void msRightFreq800_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq800Right, msRightFreq800.Value, 800); }
        }

        private void msRightFreq1600_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq1600Right, msRightFreq1600.Value, 1600); }
        }

        private void msRightFreq3200_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq3200Right, msRightFreq3200.Value, 3200); }
        }

        private void msRightFreq6400_ValueChanged(object sender, EventArgs e)
        {
            unsafe { RecalculateBiquadBand(ref _bq6400Right, msRightFreq6400.Value, 6400); }
        }
        #endregion
        #endregion

        #region ToolTips
        private void HideTip()
        {
            _cCustomTip.Stop();
        }

        private void ShowTip(object sender)
        {
            Control ct = (Control)sender;
            Point pt = this.PointToClient(Cursor.Position);
            Image img = null;
            if (ct.GetType() == typeof(GlowButton.GlowButton))
            {
                GlowButton.GlowButton gb = (GlowButton.GlowButton)ct;
                if (gb.Image != null)
                    img = gb.Image;
            }
            if (ct.Tag != null)
                _cCustomTip.Start("", (string)ct.Tag, img, pt);
        }
        #endregion

        #region Global Event Handlers
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            ShowTip(sender);
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            HideTip();
        }

        private void Label_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.ForeColor = Color.LightSkyBlue;
            ShowTip(sender);
        }

        private void Label_MouseLeave(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.ForeColor = Color.FromArgb(240, 240, 240);
            HideTip();
        }

        private void PictureBox_MouseHover(object sender, EventArgs e)
        {
            if (this.State == ApplicationState.Idle)
                ShowTip(sender);
        }

        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            HideTip();
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (this.State == ApplicationState.Idle)
                _cCustomTip.Stop();
            GraphSwitch();
        }
        #endregion
        #endregion

        #region Methods
        #region Player
        /// <summary>gfile name from dialog</summary>
        /// <returns>file name</returns>
        private string OpenFile()
        {
            if (Properties.Settings.Default.SettingInputPath.Length == 0)
                ofdFile.InitialDirectory = Application.StartupPath;
            else
                ofdFile.InitialDirectory = Properties.Settings.Default.SettingInputPath;
            DialogResult res = ofdFile.ShowDialog(this);
            if (res == DialogResult.Cancel || String.IsNullOrEmpty(ofdFile.FileName))
                return "";
            Properties.Settings.Default.SettingInputPath = ofdFile.FileName.Substring(1, ofdFile.FileName.Length - ofdFile.SafeFileName.Length);

            return ofdFile.FileName;
        }

        /// <summary>play the wav file</summary>
        /// <returns>success</returns>
        private bool Play()
        {
            try
            {
                if (_cPlayer.Play(this.FileName) == MMSYSERR.NOERROR)
                {
                    this.State = ApplicationState.Playing;
                    btnPlay.Image = Properties.Resources.pause;
                    return true;
                }
                else
                {
                    ErrorPrompt("UnKnown Format", "File error!", "Unknown format type. The selected file can not be played.");
                    return false;
                }
            }
            catch 
            {
                ErrorPrompt("UnKnown Format", "File error!", "Unknown format type. The selected file can not be played.");
                return false; 
            }
        }

        /// <summary>load a wav file</summary>
        /// <returns>success</returns>
        private bool PlayerLoad()
        {
            try
            {
                if (this.IsPlayerLoaded)
                    PlayerUnLoad();
                if (this.IsRecorderLoaded)
                    RecorderUnLoad();

                // create player
                _cPlayer = new WaveOutPlayer(this.AudioInputDevice, new BufferFillEventHandler(PlayData));
                if (_cStreamMemory != null)
                    _cStreamMemory.Dispose();
                _cStreamMemory = new MemoryStream();
                // force 16
               // _cPlayer.Convert16Bit = true;
                if (_cPlayer.CopyStream(this.FileName, ref _cStreamMemory))
                {
                    // store wav stats
                    this.Channels = _cPlayer.Channels;
                    this.BitsPerSample = _cPlayer.BitsPerSample;
                    this.SamplesPerSecond = (int)_cPlayer.SamplesPerSecond;
                    this.AvgBytesPerSecond = _cPlayer.AvgBytesPerSecond;

                    // set up eq
                    RecalculateBiquads();

                    // calculate ideal buffer size
                    CalculatePlayerBufferSize();
                    _cPlayer.BufferSize = this.BufferSize;
                    _cPlayer.BufferCount = this.BufferCount;

                    // set up visualizer
                    _cDrawDomains.SetSampleRate(this.BitsPerSample);

                    // set up controls
                    msPosition.Value = 0;
                    msPosition.Maximum = (int)(_cStreamMemory.Length / this.BufferSize);
                    msPosition.SmallChange = (int)(msPosition.Maximum * .01f);
                    msPosition.LargeChange = (int)(msPosition.Maximum * .1f);
                    msPosition.Visible = true;
                    msPosition.Enabled = true;

                    msVolume.Visible = true;
                    msVolume.Enabled = true;
                    
                    btnRewind.Enabled = true;
                    btnFastForward.Enabled = true;
                    btnStop.Enabled = true;
                    btnRecord.Enabled = false;

                    // update stats
                    lblTime.Text = "00:00";
                    lblPosition.Text = "00:00";
                    lblDuration.Text = GetDuration();
                    lblBitRate.Text = this.BitsPerSample.ToString() + " bit";
                    lblChannels.Text = this.Channels == 1 ? "Mono" : "Stereo";
                    lblFrequency.Text = String.Format("{0:0.0}", (int)(this.SamplesPerSecond * .001f)) + " Khz";

                    // display file name
                    this.Text = "PWR - " + ofdFile.SafeFileName;
                    _cRcm.ForcedRefresh();

                    // set volume level
                    LoadVolume();
                    this.IsPlayerLoaded = true;
                    return true;
                }
                else
                {
                    ErrorPrompt("UnKnown Format", "File error!", "Unknown format type. The selected file can not be played.");
                    return false;
                }
            }
            catch 
            {
                ErrorPrompt("No Playback Device", "Device error!", "No valid playback device found.");
                return false; 
            }
        }

        /// <summary>pause playback</summary>
        private void PlayerPause()
        {
            if (_cPlayer != null)
            {
                _cPlayer.Pause();
                this.State = ApplicationState.PlayPause;
            }
        }

        /// <summary>reset player</summary>
        private void PlayerRestart()
        {
            if (_cStreamMemory != null)
                _cStreamMemory.Position = 0;
            // reset progress bar
            msPosition.Value = 0;
            // reset labels
            lblTime.Text = "00:00";
            lblPosition.Text = "00:00";
        }

        /// <summary>resume playback</summary>
        private void PlayerResume()
        {
            if (_cPlayer != null)
            {
                _cPlayer.Resume();
                this.State = ApplicationState.Playing;
            }
        }

        /// <summary>unload player</summary>
        private void PlayerUnLoad()
        {
            try 
            {
                if (_cPlayer != null)
                {
                    _cPlayer.Stop();
                    _cPlayer.Dispose();
                }
                ApplicationReset();
                this.IsPlayerLoaded = false;
            }
            finally { _cPlayer = null; }
        }
        #endregion

        #region Recorder
        /// <summary>record a wav file</summary>
        /// <returns>success</returns>
        private bool Record()
        {
            if (_cRecorder != null)
            {
                if (_cRecorder.Record() == MMSYSERR.NOERROR)
                {
                    this.State = ApplicationState.Recording;
                    return true;
                }
            }
            ErrorPrompt("No Recording Device", "Device error!", "No valid recording device found.");
            return false;
        }

        /// <summary>load recorder</summary>
        /// <returns>success</returns>
        private bool RecorderLoad()
        {
            try
            {
                if (this.IsPlayerLoaded)
                    PlayerUnLoad();
                if (this.IsRecorderLoaded)
                    RecorderUnLoad();

                this.FileName = "";
                // reset controls
                msPosition.Visible = false;
                msPosition.Enabled = false;

                msVolume.Visible = false;
                msVolume.Enabled = false;

                btnRewind.Enabled = false;
                btnFastForward.Enabled = false;
                btnStop.Enabled = true;
                btnRecord.Enabled = true;

                mnuItemSave.Enabled = false;
                mnuItemSaveAs.Enabled = true;

                // load user settings
                this.AudioInputDevice = Properties.Settings.Default.SettingAudioInputDevice;
                this.SamplesPerSecond = Properties.Settings.Default.SettingSamplesPerSecond;
                this.BitsPerSample = Properties.Settings.Default.SettingBitsPerSample;
                this.Channels = Properties.Settings.Default.SettingChannels;

                // set up eq
                RecalculateBiquads();

                // create format
                _cWaveFormat = new WAVEFORMATEX();
                _cWaveFormat.nSamplesPerSec = (uint)this.SamplesPerSecond;
                _cWaveFormat.wBitsPerSample = this.BitsPerSample;
                _cWaveFormat.nChannels = this.Channels;

                // prepare destination stream
                if (_cStreamMemory != null)
                    _cStreamMemory.Dispose();
                _cStreamMemory = new MemoryStream();

                // load recorder
                _cRecorder = new WaveInRecorder(this.AudioInputDevice, ref _cWaveFormat, new BufferDoneEventHandler(RecordData));
                this.AvgBytesPerSecond = _cWaveFormat.nAvgBytesPerSec;

                // set up visualizer
                _cDrawDomains.SetSampleRate(this.BitsPerSample);

                // update stats
                lblTime.Text = "00:00";
                lblPosition.Text = "00:00";
                lblDuration.Text = "00:00";
                lblBitRate.Text = this.BitsPerSample.ToString() + " bit";
                lblChannels.Text = this.Channels == 1 ? "Mono" : "Stereo";
                lblFrequency.Text = String.Format("{0:0.0}", (int)(this.SamplesPerSecond * .001f)) + " Khz";

                this.Text = "PWR - Recording";
                _cRcm.ForcedRefresh();

                this.IsRecorderLoaded = true;
                return true;
            }
            catch 
            {
                ErrorPrompt("No Recording Device", "Device error!", "No valid recording device found.");
                return false; 
            }
        }

        /// <summary>pause recording</summary>
        private void RecordingPause()
        {
            if (_cRecorder != null)
            {
                _cRecorder.Pause();
                this.State = ApplicationState.RecordPause;
            }
        }

        /// <summary>resume recording</summary>
        private void RecordingResume()
        {
            if (_cRecorder != null)
            {
                _cRecorder.Resume();
                this.State = ApplicationState.Recording;
            }
        }

        /// <summary>save recording</summary>
        /// <returns>success</returns>
        private bool RecordingSave(bool create)
        {
            try
            {
                Stream sw = _cRecorder.CreateStream(_cStreamMemory, _cWaveFormat);
                byte[] bf = new byte[sw.Length - sw.Position];
                sw.Read(bf, 0, bf.Length);
                sw.Dispose();
                FileStream fs = new FileStream(sfdSave.FileName, create ? FileMode.Create : FileMode.Append);
                fs.Write(bf, 0, bf.Length);
                fs.Close();
                fs.Dispose();
                mnuItemSave.Enabled = true;
                return true;
            }
            catch
            {
                ErrorPrompt("Bad File Name", "Could Not Save File!", "The Recording could not be saved.");
                return false;
            }
        }

        /// <summary>save dialog</summary>
        /// <returns>success</returns>
        private bool RecordingSaveDialog()
        {
            if (Properties.Settings.Default.SettingOutputPath.Length == 0)
                sfdSave.InitialDirectory = Application.StartupPath;
            else
                sfdSave.InitialDirectory = Properties.Settings.Default.SettingOutputPath;
            if (sfdSave.ShowDialog(this) == DialogResult.OK && !String.IsNullOrEmpty(sfdSave.FileName))
            {
                this.FileName = sfdSave.FileName;
                Properties.Settings.Default.SettingOutputPath = sfdSave.FileName.Substring(0, sfdSave.FileName.LastIndexOf(@"\") + 1);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>stop recording</summary>
        private void RecordingStop()
        {
            if (_cRecorder != null)
            {
                // thread suspended
                if (this.State == ApplicationState.RecordPause)
                    _cRecorder.Resume();
                _cRecorder.Stop();
                btnRecord.Checked = false;
                btnRecord.Image = Personal_Wave_Recorder.Properties.Resources.record;
                if (string.IsNullOrEmpty(this.FileName))
                {
                    if (RecordingSaveDialog())
                        RecordingSave(true);
                }
                else
                {
                    RecordingSave(false);
                }
                RecorderUnLoad();
            }
            this.State = ApplicationState.Idle;
        }

        /// <summary>unload recorder</summary>
        private void RecorderUnLoad()
        {
            try
            {
                if (_cRecorder != null)
                {
                    if (this.State == ApplicationState.Recording)
                        _cRecorder.Stop();
                    _cRecorder.Dispose();
                    _cRecorder = null;
                }
                if (_cStreamMemory != null)
                {
                    _cStreamMemory.Dispose();
                    _cStreamMemory = null;
                }
            }
            finally
            {
                ApplicationReset();
                this.IsRecorderLoaded = false;
            }
        }
        #endregion

        #region Helpers
        /// <summary>reset ui cues</summary>
        private void ApplicationReset()
        {
            // reset button states
            btnPlay.Image = Properties.Resources.play;
            btnRewind.Enabled = false;
            btnFastForward.Enabled = false;
            btnStop.Enabled = false;
            btnRecord.Enabled = true;
            mnuItemSave.Enabled = false;
            mnuItemSaveAs.Enabled = false;
            // reset progress bar
            msPosition.Value = 0;
            msPosition.Enabled = false;
            // reset labels
            lblTime.Text = "00:00";
            lblDuration.Text = "00:00";
            lblPosition.Text = "00:00";
            // refresh graphs
            pbLeftGraph.Refresh();
            pbRightGraph.Refresh();
            // reset text
            this.Text = "PWR";
            _cRcm.ForcedRefresh();
            // flag state
            this.State = ApplicationState.Idle;
        }

        /// <summary>calculate playback buffer size</summary>
        private void CalculatePlayerBufferSize()
        {
            this.BufferSize = (uint)this.SamplesPerSecond / this.BitsPerSample;
            this.BufferCount = (uint)this.BitsPerSample;
        }

        /// <summary>get the proper form starting height</summary>
        private void CalculateRelativeHeight()
        {
            _iBaseHeight = pnlEq.Height + ((Properties.Resources.vienna_FrameTop.Height / 2) + (Properties.Resources.vienna_FrameBottom.Height / 2));
        }

        /// <summary>convert interval to time</summary>
        /// <returns>string time</returns>
        private string ConvertToTime(int time)
        {
            DateTime dt = new DateTime(time * 10000);
            return String.Format("{0:mm:ss}", dt);
        }

        /// <summary>pass error message to error dialog</summary>
        private void ErrorPrompt(string caption, string title, string error)
        {
            frmError f = new frmError();
            f.ErrorText(caption, title, error);
            f.Show(this);
        }

        /// <summary>get playback duration</summary>
        /// <returns>string duration</returns>
        private string GetDuration()
        {
            try { return ConvertToTime((int)(_cStreamMemory.Length / this.AvgBytesPerSecond) * 1000); }
            catch { return "00:00"; }
        }

        /// <summary>get playback position</summary>
        /// <returns>string position</returns>
        private string GetPosition()
        {
            try { return ConvertToTime((int)(msPosition.Value / this.AvgBytesPerSecond)); }
            catch { return "00:00"; }
        }

        /// <summary>get playback time from position</summary>
        /// <returns>string position time</returns>
        private string GetPositionTime(int pos)
        {
            try { return ConvertToTime((int)((pos * this.BufferSize) / this.AvgBytesPerSecond) * 1000); }
            catch { return "00:00"; }
        }

        /// <summary>convert player position to time</summary>
        /// <returns>string time</returns>
        private string GetTime()
        {
            try { return ConvertToTime(((msPosition.Value * (int)this.BufferSize) / (int)this.AvgBytesPerSecond) * 1000); }
            catch { return "00:00"; }
        }

        /// <summary>Fonts for Win7 OS (not used)</summary>
        private void LoadFonts()
        {
            if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1)
            {
                lblTime.Font = new Font("Arial Narrow", 24f, FontStyle.Regular);
                lblBitRate.Font = new Font("Arial Narrow", 7f);
                lblFrequency.Font = new Font("Arial Narrow", 7f);
                lblChannels.Font = new Font("Arial Narrow", 7f);
                lblPosition.Font = new Font("Arial Narrow", 7f);
                lblDuration.Font = new Font("Arial Narrow", 7f);
                lblEq.Font = new Font("Arial Narrow", 7f);
                lblRepeat.Font = new Font("Arial Narrow", 7f);
                lblLowPass.Font = new Font("Arial Narrow", 7f);
                lblHighPass.Font = new Font("Arial Narrow", 7f);
            }
        }

        /// <summary>load playback volume</summary>
        private void LoadVolume()
        {
            if (this.IsMuted)
                SetVolume(0);
            else
                SetVolume(msVolume.Value);
        }

        /// <summary>set player volume</summary>
        private void SetVolume(int level)
        {
            if (_cPlayer != null)
                _cPlayer.SetVolume((uint)level, (uint)level);
        }
        #endregion

        #region Graphs
        /// <summary>draw domain graph switch</summary>
        private void DrawGraphs(byte[] samples)
        {
            // effects switch
            if (this.IsGraphTimeDomain)
                _cDrawDomains.ProcessTimeDomain(samples, this.Channels);
            else
                _cDrawDomains.ProcessFrequencyDomain(samples, this.Channels);
        }

        /// <summary>draw domain graph switch</summary>
        private void DrawGraphs(Int16[] samples)
        {
            // effects switch
            if (this.IsGraphTimeDomain)
                _cDrawDomains.ProcessTimeDomain(samples, this.Channels);
            else
                _cDrawDomains.ProcessFrequencyDomain(samples, this.Channels);
        }

        /// <summary>draw domain graph switch ui</summary>
        private void GraphSwitch()
        {
            this.IsGraphTimeDomain = !this.IsGraphTimeDomain;
            if (this.IsGraphTimeDomain)
            {
                pbLeftGraph.Tag = "Time Domain: click to change";
                pbRightGraph.Tag = "Time Domain: click to change";
            }
            else
            {
                pbLeftGraph.Tag = "Frequency Domain";
                pbRightGraph.Tag = "Frequency Domain";
            }
        }
        #endregion

        #region Equalizer
        /// <summary>player sound hub</summary>
        private void ProcessSound(ref byte[] buffer, IntPtr data)
        {
            int len = buffer.Length;

            if (this.BitsPerSample == 16)
            {
                len = buffer.Length / 2;
                Int16[] samples = new Int16[len];
                Buffer.BlockCopy(buffer, 0, samples, 0, buffer.Length);
                // eq
                ProcessEq(ref samples);
                // pass in
                Marshal.Copy(samples, 0, data, len);
                // graph
                DrawGraphs(samples);
            }
            else if (this.BitsPerSample == 8)
            {
                // eq
                ProcessEq(ref buffer);
                // pass in
                Marshal.Copy(buffer, 0, data, len);
                // graph
                DrawGraphs(buffer);
            }
        }

        /// <summary>process dsp chain</summary>
        private void ProcessEq(ref Int16[] samples)
        {
            int len = samples.Length;
            int i = 0;

            unsafe
            {
                if (this.Channels == 1)
                {
                    do
                    {
                        // eq  
                        if (this.IsEqOn)
                        {
                            _eqBands.BiQuad(ref samples[i], _bq100Left);
                            _eqBands.BiQuad(ref samples[i], _bq200Left);
                            _eqBands.BiQuad(ref samples[i], _bq400Left);
                            _eqBands.BiQuad(ref samples[i], _bq800Left);
                            _eqBands.BiQuad(ref samples[i], _bq1600Left);
                            _eqBands.BiQuad(ref samples[i], _bq3200Left);
                            _eqBands.BiQuad(ref samples[i], _bq6400Left);
                        }
                        // filters
                        if (this.IsHighPassOn)
                        {
                            _eqBands.BiQuad(ref samples[i], _bqHPF);
                        }
                        if (this.IsLowPassOn)
                        {
                            _eqBands.BiQuad(ref samples[i], _bqLPF);
                        }
                        i++;
                    } while (i < len);
                }
                else
                {
                    len -= 1;
                    do
                    {
                        // eq 
                        if (this.IsEqOn)
                        {
                            // left channel
                            _eqBands.BiQuad(ref samples[i], _bq100Left);
                            _eqBands.BiQuad(ref samples[i], _bq200Left);
                            _eqBands.BiQuad(ref samples[i], _bq400Left);
                            _eqBands.BiQuad(ref samples[i], _bq800Left);
                            _eqBands.BiQuad(ref samples[i], _bq1600Left);
                            _eqBands.BiQuad(ref samples[i], _bq3200Left);
                            _eqBands.BiQuad(ref samples[i], _bq6400Left);
                            // right channel
                            _eqBands.BiQuad(ref samples[i + 1], _bq100Right);
                            _eqBands.BiQuad(ref samples[i + 1], _bq200Right);
                            _eqBands.BiQuad(ref samples[i + 1], _bq400Right);
                            _eqBands.BiQuad(ref samples[i + 1], _bq800Right);
                            _eqBands.BiQuad(ref samples[i + 1], _bq1600Right);
                            _eqBands.BiQuad(ref samples[i + 1], _bq3200Right);
                            _eqBands.BiQuad(ref samples[i + 1], _bq6400Right);
                        }
                        // filters
                        if (this.IsHighPassOn)
                        {
                            _eqBands.BiQuad(ref samples[i], _bqHPF);
                            _eqBands.BiQuad(ref samples[i + 1], _bqHPF);
                        }
                        if (this.IsLowPassOn)
                        {
                            _eqBands.BiQuad(ref samples[i], _bqLPF);
                            _eqBands.BiQuad(ref samples[i + 1], _bqLPF);
                        }
                        i += 2;
                    } while (i < len);
                }
            }
        }

        /// <summary>process dsp chain</summary>
        private void ProcessEq(ref byte[] buffer)
        {
            int len = buffer.Length;
            int i = 0;

            unsafe
            {
                if (this.Channels == 1)
                {
                    do
                    {
                        // eq  
                        if (this.IsEqOn)
                        {
                            _eqBands.BiQuad(ref buffer[i], _bq100Left);
                            _eqBands.BiQuad(ref buffer[i], _bq200Left);
                            _eqBands.BiQuad(ref buffer[i], _bq400Left);
                            _eqBands.BiQuad(ref buffer[i], _bq800Left);
                            _eqBands.BiQuad(ref buffer[i], _bq1600Left);
                            _eqBands.BiQuad(ref buffer[i], _bq3200Left);
                            _eqBands.BiQuad(ref buffer[i], _bq6400Left);
                        }
                        // filters
                        if (this.IsHighPassOn)
                        {
                            _eqBands.BiQuad(ref buffer[i], _bqHPF);
                        }
                        if (this.IsLowPassOn)
                        {
                            _eqBands.BiQuad(ref buffer[i], _bqLPF);
                        }
                        i++;
                    } while (i < len);
                }
                else
                {
                    len -= 1;
                    i = 0;
                    do
                    {
                        // eq  
                        if (this.IsEqOn)
                        {
                            // left channel
                            _eqBands.BiQuad(ref buffer[i], _bq100Left);
                            _eqBands.BiQuad(ref buffer[i], _bq200Left);
                            _eqBands.BiQuad(ref buffer[i], _bq400Left);
                            _eqBands.BiQuad(ref buffer[i], _bq800Left);
                            _eqBands.BiQuad(ref buffer[i], _bq1600Left);
                            _eqBands.BiQuad(ref buffer[i], _bq3200Left);
                            _eqBands.BiQuad(ref buffer[i], _bq6400Left);
                            // right channel
                            _eqBands.BiQuad(ref buffer[i + 1], _bq100Right);
                            _eqBands.BiQuad(ref buffer[i + 1], _bq200Right);
                            _eqBands.BiQuad(ref buffer[i + 1], _bq400Right);
                            _eqBands.BiQuad(ref buffer[i + 1], _bq800Right);
                            _eqBands.BiQuad(ref buffer[i + 1], _bq1600Right);
                            _eqBands.BiQuad(ref buffer[i + 1], _bq3200Right);
                            _eqBands.BiQuad(ref buffer[i + 1], _bq6400Right);
                        }
                        // filters
                        if (this.IsHighPassOn)
                        {
                            _eqBands.BiQuad(ref buffer[i], _bqHPF);
                            _eqBands.BiQuad(ref buffer[i + 1], _bqHPF);
                        }
                        if (this.IsLowPassOn)
                        {
                            _eqBands.BiQuad(ref buffer[i], _bqLPF);
                            _eqBands.BiQuad(ref buffer[i + 1], _bqLPF);
                        }
                        i += 2;
                    } while (i < len);
                }
            }
        }

        /// <summary>load equalizer settings</summary>
        private unsafe void LoadEq()
        {
            // type(BPF), start freq, cutoff freq, sample rate, banwidth-(BPF, 10, 3200, sps, 1)??
            // A band-pass filter passes a limited range of frequencies.

            // type(HPF), center freq, cutoff freq, sample rate, banwidth-(LPF, 10, 40, sps, 1)??
            // A high-pass filter passes high frequencies fairly well; it is helpful as a filter to cut any unwanted low frequency components. 

            // type(HSH), gain, center freq, rate, bandwidth-(HSH, 4, 4000, sps, 1)
            // A high-shelf filter passes all frequencies, but increasing or reducing frequencies above the cutoff frequency by specified amount. 

            // type(LPF), start freq, cutoff freq, sample rate, banwidth-(LPF, 8000, 10000, sps, 1)
            // A low-pass filter is used to cut unwanted high-frequency signals. 

            // type(LSH), gain, cutoff freq, sample rate, banwidth-(LSH, .5, 80, sps, 1)
            // A low-shelf filter passes all frequencies, but increasing or reducing frequencies below the cutoff frequency by specified amount.

            // type(NOTCH), gain, center freq, rate, bandwidth-(NOTCH, 1, 20, sps, 1)
            // A notch filter or band-rejection filter is a filter that passes most frequencies unaltered, but attenuates those in a specific range to very low levels.

            // type(PEQ), gain, center freq, sample rate, banwidth-(PEQ, 8, 400, sps, 1)
            // A peak EQ filter makes a peak or a dip in the frequency response, commonly used in graphic equalizers. 
            UnloadEq();
            // left channel
            _bq100Left = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msLeftFreq100.Value, 100, this.SamplesPerSecond, 1);
            _bq200Left = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msLeftFreq200.Value, 200, this.SamplesPerSecond, 1);
            _bq400Left = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msLeftFreq400.Value, 400, this.SamplesPerSecond, 1);
            _bq800Left = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msLeftFreq800.Value, 800, this.SamplesPerSecond, 1);
            _bq1600Left = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msLeftFreq1600.Value, 1600, this.SamplesPerSecond, 1);
            _bq3200Left = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msLeftFreq3200.Value, 3200, this.SamplesPerSecond, 1);
            _bq6400Left = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msLeftFreq6400.Value, 6400, this.SamplesPerSecond, 1);
            // right channel
            _bq100Right = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msRightFreq100.Value, 100, this.SamplesPerSecond, 1);
            _bq200Right = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msRightFreq200.Value, 200, this.SamplesPerSecond, 1);
            _bq400Right = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msRightFreq400.Value, 400, this.SamplesPerSecond, 1);
            _bq800Right = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msRightFreq800.Value, 800, this.SamplesPerSecond, 1);
            _bq1600Right = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msRightFreq1600.Value, 1600, this.SamplesPerSecond, 1);
            _bq3200Right = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msRightFreq3200.Value, 3200, this.SamplesPerSecond, 1);
            _bq6400Right = _eqBands.BiQuadFilter(IIR.Filter.PEQ, msRightFreq6400.Value, 6400, this.SamplesPerSecond, 1);
            // filters
            // cut high harmonic
            _bqLPF = _eqBands.BiQuadFilter(IIR.Filter.LPF, 8000, 10000, this.SamplesPerSecond, 1);
            // boost mid harmonic
            _bqHPF = _eqBands.BiQuadFilter(IIR.Filter.HSH, 4, 4000, this.SamplesPerSecond, 1);
        }

        /// <summary>reload equalizer settings</summary>
        private unsafe void RecalculateBiquads()
        {
            // left channel
            RecalculateBiquadBand(ref _bq100Left, msLeftFreq100.Value, 100);
            RecalculateBiquadBand(ref _bq200Left, msLeftFreq200.Value, 200);
            RecalculateBiquadBand(ref _bq400Left, msLeftFreq400.Value, 400);
            RecalculateBiquadBand(ref _bq800Left, msLeftFreq800.Value, 800);
            RecalculateBiquadBand(ref _bq1600Left, msLeftFreq1600.Value, 1600);
            RecalculateBiquadBand(ref _bq3200Left, msLeftFreq3200.Value, 3200);
            RecalculateBiquadBand(ref _bq6400Left, msLeftFreq6400.Value, 6400);
            // right channel
            RecalculateBiquadBand(ref _bq100Right, msRightFreq100.Value, 100);
            RecalculateBiquadBand(ref _bq200Right, msRightFreq200.Value, 200);
            RecalculateBiquadBand(ref _bq400Right, msRightFreq400.Value, 400);
            RecalculateBiquadBand(ref _bq800Right, msRightFreq800.Value, 800);
            RecalculateBiquadBand(ref _bq1600Right, msRightFreq1600.Value, 1600);
            RecalculateBiquadBand(ref _bq3200Right, msRightFreq3200.Value, 3200);
            RecalculateBiquadBand(ref _bq6400Right, msRightFreq6400.Value, 6400);
            RecalculateFilters();
        }

        /// <summary>reload biquad filters</summary>
        private unsafe void RecalculateFilters()
        {
            if (_bqLPF != null)
            {
                _eqBands.FreeMem(_bqLPF);
                _bqLPF = null;
            }
            if (_bqHPF != null)
            {
                _eqBands.FreeMem(_bqHPF);
                _bqHPF = null;
            }
            // cut high harmonic
            _bqLPF = _eqBands.BiQuadFilter(IIR.Filter.LPF, 8000, 10000, this.SamplesPerSecond, 1);
            // boost mid harmonic
            _bqHPF = _eqBands.BiQuadFilter(IIR.Filter.HSH, 4, 4000, this.SamplesPerSecond, 1);
        }

        /// <summary>calculate amplitude change in biquad</summary>
        private unsafe void RecalculateBiquadBand(ref biquad* b, double dbGain, double frequency)
        {
            // reduce the gain thresholds on energetic signals to reduce distortion..
           // if (this.BitsPerSample == 16)
           //     dbGain *= .1f;
            biquad* tmp = _eqBands.BiQuadFilter(IIR.Filter.PEQ, dbGain, frequency, this.SamplesPerSecond, 1);
            b->a0 = tmp->a0;
            b->a1 = tmp->a1;
            b->a2 = tmp->a2;
            b->a3 = tmp->a3;
            b->a4 = tmp->a4;
            _eqBands.FreeMem(tmp);
            tmp = null;
        }

        /// <summary>equalizer cleanup</summary>
        private unsafe void UnloadEq()
        {
            // filters
            if (_bqLPF != null)
                _eqBands.FreeMem(_bqLPF);
            if (_bqHPF != null)
                _eqBands.FreeMem(_bqHPF);
            // left
            if (_bq100Left != null)
                _eqBands.FreeMem(_bq100Left);
            if (_bq200Left != null)
                _eqBands.FreeMem(_bq200Left);
            if (_bq400Left != null)
                _eqBands.FreeMem(_bq400Left);
            if (_bq800Left != null)
                _eqBands.FreeMem(_bq800Left);
            if (_bq1600Left != null)
                _eqBands.FreeMem(_bq1600Left);
            if (_bq3200Left != null)
                _eqBands.FreeMem(_bq3200Left);
            if (_bq6400Left != null)
                _eqBands.FreeMem(_bq6400Left);
            // right
            if (_bq100Right != null)
                _eqBands.FreeMem(_bq100Right);
            if (_bq200Right != null)
                _eqBands.FreeMem(_bq200Right);
            if (_bq400Right != null)
                _eqBands.FreeMem(_bq400Right);
            if (_bq800Right != null)
                _eqBands.FreeMem(_bq800Right);
            if (_bq1600Right != null)
                _eqBands.FreeMem(_bq1600Right);
            if (_bq3200Right != null)
                _eqBands.FreeMem(_bq3200Right);
            if (_bq6400Right != null)
                _eqBands.FreeMem(_bq6400Right);
        }
        #endregion

        #region Callbacks
        /// <summary>player callback</summary>
        private void PlayData(IntPtr data, uint size)
        {
            // size byte buffer
            if (_btPlayBuffer == null)
                _btPlayBuffer = new byte[size];
            else
                Array.Resize(ref _btPlayBuffer, (int)size);

            // read in bytes
            int len = (int)(_cStreamMemory.Length - _cStreamMemory.Position);
            if (len >= size)
            {
                _cStreamMemory.Read(_btPlayBuffer, 0, (int)size);
            }
            else
            {
                _cStreamMemory.Read(_btPlayBuffer, 0, len);
                for (int i = len; i < _btPlayBuffer.Length; i++)
                    _btPlayBuffer[i] = 0;
            }

            // process byte stream
            ProcessCallback ps = new ProcessCallback(ProcessSound);
            this.Invoke(ps, new object[] { _btPlayBuffer, data });

            // move the progress bar
            PositionChangedCallback pc = new PositionChangedCallback(PositionChange);
            this.Invoke(pc, new object[] { });

            // loop/unload
            if (_cStreamMemory.Position == _cStreamMemory.Length)
            {
                if (this.IsRepeatOn)
                {
                    ResetCallback r = new ResetCallback(PlayerRestart);
                    this.Invoke(r);
                }
                else
                {
                    ResetCallback r = new ResetCallback(PlayerUnLoad);
                    this.Invoke(r);
                }
            }
        }

        /// <summary>recorder callback</summary>
        private void RecordData(IntPtr data, uint size)
        {
            if (_btRecBuffer == null)
                _btRecBuffer = new byte[size];
            else
                Array.Resize(ref _btRecBuffer, (int)size);
            System.Runtime.InteropServices.Marshal.Copy(data, _btRecBuffer, 0, (int)size);
            // process byte stream
            ProcessCallback ps = new ProcessCallback(ProcessSound);
            this.Invoke(ps, new object[] { _btRecBuffer, data });
            _cStreamMemory.Write(_btRecBuffer, 0, _btRecBuffer.Length);
            // update labels
            PositionChangedCallback pc = new PositionChangedCallback(PositionChange);
            this.Invoke(pc, new object[] { });
        }

        /// <summary>player position slider change</summary>
        private void PositionChange()
        {
            try 
            {
                if (this.State == ApplicationState.Recording)
                {
                    lblTime.Text = GetDuration();
                    lblDuration.Text = lblTime.Text;
                }
                else if (this.State == ApplicationState.Playing)
                {
                    if (!this.IsPositionChanging)
                        msPosition.Value = (int)(_cStreamMemory.Position / this.BufferSize);
                    lblTime.Text = GetTime();
                    //if not hovering..
                    if (!IsHoverOverPosition)
                        lblPosition.Text = lblTime.Text;
                }
            }
            catch { }
        }
        #endregion

        #region Settings
        /// <summary>load application settings</summary>
        private void LoadSettings()
        {
            // controls
            this.IsGraphTimeDomain = Properties.Settings.Default.SettingGraphClicked;
            this.IsEqOn = Properties.Settings.Default.SettingEqOn;
            mnuItemEqualizer.Checked = this.IsEqOn;
            if (this.IsEqOn)
            {
                lblEq.BackColor = SELECTED_COLOR;
                lblEq.Tag = "Equalizer : On";
            }
            this.IsHighPassOn = Properties.Settings.Default.SettingHighPassOn;
            if (this.IsHighPassOn)
            {
                lblHighPass.BackColor = SELECTED_COLOR;
                lblHighPass.Tag = "High Band Boost : On";
            }
            this.IsLowPassOn = Properties.Settings.Default.SettingLowPassOn;
            if (this.IsLowPassOn)
            {
                lblLowPass.BackColor = SELECTED_COLOR;
                lblLowPass.Tag = "Low Pass Filter : On";
            }
            this.IsRepeatOn = Properties.Settings.Default.SettingRepeatOn;
            if (this.IsRepeatOn)
            {
                lblRepeat.BackColor = SELECTED_COLOR;
                lblRepeat.Tag = "Repeat Selection : On";
            }
            btnEqRollup.Checked = Properties.Settings.Default.SettingEqShowing;
            if (btnEqRollup.Checked)
                this.Height = _iBaseHeight + 95;
            else
                this.Height = _iBaseHeight;

            this.msVolume.Value = Properties.Settings.Default.SettingVolume;
            // player
            this.AudioOutputDevice = Properties.Settings.Default.SettingAudioOutputDevice;
            // recorder
            this.AudioInputDevice = Properties.Settings.Default.SettingAudioInputDevice;
            this.BitsPerSample = Properties.Settings.Default.SettingBitsPerSample;
            this.BufferCount = Properties.Settings.Default.SettingBufferCount;
            this.BufferSize = Properties.Settings.Default.SettingBufferSize;
            this.Channels = Properties.Settings.Default.SettingChannels;
            this.SamplesPerSecond = Properties.Settings.Default.SettingSamplesPerSecond;
            lblBitRate.Text = this.BitsPerSample.ToString() + " bit";
            lblChannels.Text = this.Channels == 1 ? "Mono" : "Stereo";
            lblFrequency.Text = String.Format("{0:0.0}", (int)(this.SamplesPerSecond * .001f)) + " Khz";
            // eq
            msLeftFreq100.Value = Properties.Settings.Default.SettingLeftEq100;
            msLeftFreq200.Value = Properties.Settings.Default.SettingLeftEq200;
            msLeftFreq400.Value = Properties.Settings.Default.SettingLeftEq400;
            msLeftFreq800.Value = Properties.Settings.Default.SettingLeftEq800;
            msLeftFreq1600.Value = Properties.Settings.Default.SettingLeftEq1600;
            msLeftFreq3200.Value = Properties.Settings.Default.SettingLeftEq3200;
            msLeftFreq6400.Value = Properties.Settings.Default.SettingLeftEq6400;
            msRightFreq100.Value = Properties.Settings.Default.SettingRightEq100;
            msRightFreq200.Value = Properties.Settings.Default.SettingRightEq200;
            msRightFreq400.Value = Properties.Settings.Default.SettingRightEq400;
            msRightFreq800.Value = Properties.Settings.Default.SettingRightEq800;
            msRightFreq1600.Value = Properties.Settings.Default.SettingRightEq1600;
            msRightFreq3200.Value = Properties.Settings.Default.SettingRightEq3200;
            msRightFreq6400.Value = Properties.Settings.Default.SettingRightEq6400;
        }

        /// <summary>unload application settings</summary>
        private void SaveSettings()
        {
            // controls
            Properties.Settings.Default.SettingGraphClicked = this.IsGraphTimeDomain;
            Properties.Settings.Default.SettingEqOn = this.IsEqOn;
            Properties.Settings.Default.SettingHighPassOn = this.IsHighPassOn;
            Properties.Settings.Default.SettingLowPassOn = this.IsLowPassOn;
            Properties.Settings.Default.SettingRepeatOn = this.IsRepeatOn;
            Properties.Settings.Default.SettingEqShowing = btnEqRollup.Checked;
            Properties.Settings.Default.SettingVolume = this.msVolume.Value;
            // player
            Properties.Settings.Default.SettingAudioOutputDevice = this.AudioOutputDevice;
            // recorder
            Properties.Settings.Default.SettingAudioInputDevice = this.AudioInputDevice;
            Properties.Settings.Default.SettingBitsPerSample = this.BitsPerSample;
            Properties.Settings.Default.SettingBufferCount = this.BufferCount;
            Properties.Settings.Default.SettingBufferSize = this.BufferSize;
            Properties.Settings.Default.SettingChannels = (byte)this.Channels;
            Properties.Settings.Default.SettingSamplesPerSecond = this.SamplesPerSecond;
            // eq
            Properties.Settings.Default.SettingLeftEq100 = msLeftFreq100.Value;
            Properties.Settings.Default.SettingLeftEq200 = msLeftFreq200.Value;
            Properties.Settings.Default.SettingLeftEq400 = msLeftFreq400.Value;
            Properties.Settings.Default.SettingLeftEq800 = msLeftFreq800.Value;
            Properties.Settings.Default.SettingLeftEq1600 = msLeftFreq1600.Value;
            Properties.Settings.Default.SettingLeftEq3200 = msLeftFreq3200.Value;
            Properties.Settings.Default.SettingLeftEq6400 = msLeftFreq6400.Value;
            Properties.Settings.Default.SettingRightEq100 = msRightFreq100.Value;
            Properties.Settings.Default.SettingRightEq200 = msRightFreq200.Value;
            Properties.Settings.Default.SettingRightEq400 = msRightFreq400.Value;
            Properties.Settings.Default.SettingRightEq800 = msRightFreq800.Value;
            Properties.Settings.Default.SettingRightEq1600 = msRightFreq1600.Value;
            Properties.Settings.Default.SettingRightEq3200 = msRightFreq3200.Value;
            Properties.Settings.Default.SettingRightEq6400 = msRightFreq6400.Value;
            Properties.Settings.Default.Save();
        }
        #endregion
        #endregion
    }
}