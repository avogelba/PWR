namespace Personal_Wave_Recorder
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlSliders = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.msRightFreq6400 = new MediaSlider.MediaSlider();
            this.msRightFreq3200 = new MediaSlider.MediaSlider();
            this.msRightFreq1600 = new MediaSlider.MediaSlider();
            this.msRightFreq800 = new MediaSlider.MediaSlider();
            this.msRightFreq400 = new MediaSlider.MediaSlider();
            this.msRightFreq200 = new MediaSlider.MediaSlider();
            this.msRightFreq100 = new MediaSlider.MediaSlider();
            this.lblFq = new System.Windows.Forms.Label();
            this.lblDb3 = new System.Windows.Forms.Label();
            this.lblDb1 = new System.Windows.Forms.Label();
            this.lblDb2 = new System.Windows.Forms.Label();
            this.lblFq7 = new System.Windows.Forms.Label();
            this.lblFq6 = new System.Windows.Forms.Label();
            this.lblFq5 = new System.Windows.Forms.Label();
            this.lblFq4 = new System.Windows.Forms.Label();
            this.lblFq3 = new System.Windows.Forms.Label();
            this.lblFq2 = new System.Windows.Forms.Label();
            this.lblFq1 = new System.Windows.Forms.Label();
            this.msLeftFreq6400 = new MediaSlider.MediaSlider();
            this.msLeftFreq3200 = new MediaSlider.MediaSlider();
            this.msLeftFreq1600 = new MediaSlider.MediaSlider();
            this.msLeftFreq800 = new MediaSlider.MediaSlider();
            this.msLeftFreq400 = new MediaSlider.MediaSlider();
            this.msLeftFreq200 = new MediaSlider.MediaSlider();
            this.msLeftFreq100 = new MediaSlider.MediaSlider();
            this.mnuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemEqualizer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemHighPass = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemLowPass = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemRepeat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemMute = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemDevices = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.pnlEq = new System.Windows.Forms.Panel();
            this.msVolume = new MediaSlider.MediaSlider();
            this.btnMute = new GlowButton.GlowButton();
            this.btnStop = new GlowButton.GlowButton();
            this.btnPlay = new GlowButton.GlowButton();
            this.btnRecord = new GlowButton.GlowButton();
            this.btnFastForward = new GlowButton.GlowButton();
            this.btnRewind = new GlowButton.GlowButton();
            this.msPosition = new MediaSlider.MediaSlider();
            this.btnEqRollup = new GlowButton.GlowButton();
            this.lblHighPass = new System.Windows.Forms.Label();
            this.lblLowPass = new System.Windows.Forms.Label();
            this.lblRepeat = new System.Windows.Forms.Label();
            this.lblEq = new System.Windows.Forms.Label();
            this.lblChannels = new System.Windows.Forms.Label();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.lblBitRate = new System.Windows.Forms.Label();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.pbLeftGraph = new System.Windows.Forms.PictureBox();
            this.pbRightGraph = new System.Windows.Forms.PictureBox();
            this.pnlSliders.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.pnlEq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSliders
            // 
            this.pnlSliders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pnlSliders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSliders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSliders.Controls.Add(this.label1);
            this.pnlSliders.Controls.Add(this.label2);
            this.pnlSliders.Controls.Add(this.label3);
            this.pnlSliders.Controls.Add(this.label4);
            this.pnlSliders.Controls.Add(this.label5);
            this.pnlSliders.Controls.Add(this.label6);
            this.pnlSliders.Controls.Add(this.label7);
            this.pnlSliders.Controls.Add(this.label8);
            this.pnlSliders.Controls.Add(this.label9);
            this.pnlSliders.Controls.Add(this.label10);
            this.pnlSliders.Controls.Add(this.label11);
            this.pnlSliders.Controls.Add(this.msRightFreq6400);
            this.pnlSliders.Controls.Add(this.msRightFreq3200);
            this.pnlSliders.Controls.Add(this.msRightFreq1600);
            this.pnlSliders.Controls.Add(this.msRightFreq800);
            this.pnlSliders.Controls.Add(this.msRightFreq400);
            this.pnlSliders.Controls.Add(this.msRightFreq200);
            this.pnlSliders.Controls.Add(this.msRightFreq100);
            this.pnlSliders.Controls.Add(this.lblFq);
            this.pnlSliders.Controls.Add(this.lblDb3);
            this.pnlSliders.Controls.Add(this.lblDb1);
            this.pnlSliders.Controls.Add(this.lblDb2);
            this.pnlSliders.Controls.Add(this.lblFq7);
            this.pnlSliders.Controls.Add(this.lblFq6);
            this.pnlSliders.Controls.Add(this.lblFq5);
            this.pnlSliders.Controls.Add(this.lblFq4);
            this.pnlSliders.Controls.Add(this.lblFq3);
            this.pnlSliders.Controls.Add(this.lblFq2);
            this.pnlSliders.Controls.Add(this.lblFq1);
            this.pnlSliders.Controls.Add(this.msLeftFreq6400);
            this.pnlSliders.Controls.Add(this.msLeftFreq3200);
            this.pnlSliders.Controls.Add(this.msLeftFreq1600);
            this.pnlSliders.Controls.Add(this.msLeftFreq800);
            this.pnlSliders.Controls.Add(this.msLeftFreq400);
            this.pnlSliders.Controls.Add(this.msLeftFreq200);
            this.pnlSliders.Controls.Add(this.msLeftFreq100);
            this.pnlSliders.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSliders.Location = new System.Drawing.Point(0, 31);
            this.pnlSliders.Name = "pnlSliders";
            this.pnlSliders.Size = new System.Drawing.Size(341, 94);
            this.pnlSliders.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label1.Location = new System.Drawing.Point(167, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 9);
            this.label1.TabIndex = 50;
            this.label1.Text = "Fq: Hz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label2.Location = new System.Drawing.Point(168, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 9);
            this.label2.TabIndex = 49;
            this.label2.Text = "-8db";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label3.Location = new System.Drawing.Point(168, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 9);
            this.label3.TabIndex = 48;
            this.label3.Text = "+8db";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label4.Location = new System.Drawing.Point(177, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(9, 9);
            this.label4.TabIndex = 47;
            this.label4.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label5.Location = new System.Drawing.Point(302, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 9);
            this.label5.TabIndex = 46;
            this.label5.Text = "6.4k";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label6.Location = new System.Drawing.Point(283, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 9);
            this.label6.TabIndex = 45;
            this.label6.Text = "3.2k";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label7.Location = new System.Drawing.Point(263, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 9);
            this.label7.TabIndex = 44;
            this.label7.Text = "1.6k";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label8.Location = new System.Drawing.Point(247, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 9);
            this.label8.TabIndex = 43;
            this.label8.Text = "800";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label9.Location = new System.Drawing.Point(228, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 9);
            this.label9.TabIndex = 42;
            this.label9.Text = "400";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label10.Location = new System.Drawing.Point(210, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 9);
            this.label10.TabIndex = 41;
            this.label10.Text = "200";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label11.Location = new System.Drawing.Point(191, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 9);
            this.label11.TabIndex = 40;
            this.label11.Text = "100";
            // 
            // msRightFreq6400
            // 
            this.msRightFreq6400.Animated = false;
            this.msRightFreq6400.AnimationSize = 0.2F;
            this.msRightFreq6400.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msRightFreq6400.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msRightFreq6400.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msRightFreq6400.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq6400.BackGroundImage = null;
            this.msRightFreq6400.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq6400.ButtonBorderColor = System.Drawing.Color.Black;
            this.msRightFreq6400.ButtonColor = System.Drawing.Color.Black;
            this.msRightFreq6400.ButtonCornerRadius = ((uint)(6u));
            this.msRightFreq6400.ButtonSize = new System.Drawing.Size(8, 12);
            this.msRightFreq6400.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msRightFreq6400.ContextMenuStrip = null;
            this.msRightFreq6400.LargeChange = 2;
            this.msRightFreq6400.Location = new System.Drawing.Point(306, 21);
            this.msRightFreq6400.Margin = new System.Windows.Forms.Padding(0);
            this.msRightFreq6400.Maximum = 8;
            this.msRightFreq6400.Minimum = -8;
            this.msRightFreq6400.Name = "msRightFreq6400";
            this.msRightFreq6400.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msRightFreq6400.ShowButtonOnHover = false;
            this.msRightFreq6400.Size = new System.Drawing.Size(10, 61);
            this.msRightFreq6400.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msRightFreq6400.SmallChange = 1;
            this.msRightFreq6400.SmoothScrolling = false;
            this.msRightFreq6400.TabIndex = 39;
            this.msRightFreq6400.TickColor = System.Drawing.Color.DarkGray;
            this.msRightFreq6400.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msRightFreq6400.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msRightFreq6400.TrackBorderColor = System.Drawing.Color.Gray;
            this.msRightFreq6400.TrackDepth = 4;
            this.msRightFreq6400.TrackFillColor = System.Drawing.Color.Transparent;
            this.msRightFreq6400.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msRightFreq6400.TrackShadow = true;
            this.msRightFreq6400.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msRightFreq6400.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msRightFreq6400.Value = 5;
            this.msRightFreq6400.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msRightFreq6400_ValueChanged);
            // 
            // msRightFreq3200
            // 
            this.msRightFreq3200.Animated = false;
            this.msRightFreq3200.AnimationSize = 0.2F;
            this.msRightFreq3200.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msRightFreq3200.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msRightFreq3200.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msRightFreq3200.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq3200.BackGroundImage = null;
            this.msRightFreq3200.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq3200.ButtonBorderColor = System.Drawing.Color.Black;
            this.msRightFreq3200.ButtonColor = System.Drawing.Color.Black;
            this.msRightFreq3200.ButtonCornerRadius = ((uint)(6u));
            this.msRightFreq3200.ButtonSize = new System.Drawing.Size(8, 12);
            this.msRightFreq3200.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msRightFreq3200.ContextMenuStrip = null;
            this.msRightFreq3200.LargeChange = 2;
            this.msRightFreq3200.Location = new System.Drawing.Point(287, 21);
            this.msRightFreq3200.Margin = new System.Windows.Forms.Padding(0);
            this.msRightFreq3200.Maximum = 8;
            this.msRightFreq3200.Minimum = -8;
            this.msRightFreq3200.Name = "msRightFreq3200";
            this.msRightFreq3200.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msRightFreq3200.ShowButtonOnHover = false;
            this.msRightFreq3200.Size = new System.Drawing.Size(10, 61);
            this.msRightFreq3200.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msRightFreq3200.SmallChange = 1;
            this.msRightFreq3200.SmoothScrolling = false;
            this.msRightFreq3200.TabIndex = 38;
            this.msRightFreq3200.TickColor = System.Drawing.Color.DarkGray;
            this.msRightFreq3200.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msRightFreq3200.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msRightFreq3200.TrackBorderColor = System.Drawing.Color.Gray;
            this.msRightFreq3200.TrackDepth = 4;
            this.msRightFreq3200.TrackFillColor = System.Drawing.Color.Transparent;
            this.msRightFreq3200.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msRightFreq3200.TrackShadow = true;
            this.msRightFreq3200.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msRightFreq3200.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msRightFreq3200.Value = 7;
            this.msRightFreq3200.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msRightFreq3200_ValueChanged);
            // 
            // msRightFreq1600
            // 
            this.msRightFreq1600.Animated = false;
            this.msRightFreq1600.AnimationSize = 0.2F;
            this.msRightFreq1600.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msRightFreq1600.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msRightFreq1600.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msRightFreq1600.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq1600.BackGroundImage = null;
            this.msRightFreq1600.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq1600.ButtonBorderColor = System.Drawing.Color.Black;
            this.msRightFreq1600.ButtonColor = System.Drawing.Color.Black;
            this.msRightFreq1600.ButtonCornerRadius = ((uint)(6u));
            this.msRightFreq1600.ButtonSize = new System.Drawing.Size(8, 12);
            this.msRightFreq1600.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msRightFreq1600.ContextMenuStrip = null;
            this.msRightFreq1600.LargeChange = 2;
            this.msRightFreq1600.Location = new System.Drawing.Point(269, 21);
            this.msRightFreq1600.Margin = new System.Windows.Forms.Padding(0);
            this.msRightFreq1600.Maximum = 8;
            this.msRightFreq1600.Minimum = -8;
            this.msRightFreq1600.Name = "msRightFreq1600";
            this.msRightFreq1600.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msRightFreq1600.ShowButtonOnHover = false;
            this.msRightFreq1600.Size = new System.Drawing.Size(10, 61);
            this.msRightFreq1600.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msRightFreq1600.SmallChange = 1;
            this.msRightFreq1600.SmoothScrolling = false;
            this.msRightFreq1600.TabIndex = 37;
            this.msRightFreq1600.TickColor = System.Drawing.Color.DarkGray;
            this.msRightFreq1600.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msRightFreq1600.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msRightFreq1600.TrackBorderColor = System.Drawing.Color.Gray;
            this.msRightFreq1600.TrackDepth = 4;
            this.msRightFreq1600.TrackFillColor = System.Drawing.Color.Transparent;
            this.msRightFreq1600.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msRightFreq1600.TrackShadow = true;
            this.msRightFreq1600.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msRightFreq1600.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msRightFreq1600.Value = 5;
            this.msRightFreq1600.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msRightFreq1600_ValueChanged);
            // 
            // msRightFreq800
            // 
            this.msRightFreq800.Animated = false;
            this.msRightFreq800.AnimationSize = 0.2F;
            this.msRightFreq800.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msRightFreq800.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msRightFreq800.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msRightFreq800.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq800.BackGroundImage = null;
            this.msRightFreq800.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq800.ButtonBorderColor = System.Drawing.Color.Black;
            this.msRightFreq800.ButtonColor = System.Drawing.Color.Black;
            this.msRightFreq800.ButtonCornerRadius = ((uint)(6u));
            this.msRightFreq800.ButtonSize = new System.Drawing.Size(8, 12);
            this.msRightFreq800.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msRightFreq800.ContextMenuStrip = null;
            this.msRightFreq800.LargeChange = 2;
            this.msRightFreq800.Location = new System.Drawing.Point(250, 21);
            this.msRightFreq800.Margin = new System.Windows.Forms.Padding(0);
            this.msRightFreq800.Maximum = 8;
            this.msRightFreq800.Minimum = -8;
            this.msRightFreq800.Name = "msRightFreq800";
            this.msRightFreq800.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msRightFreq800.ShowButtonOnHover = false;
            this.msRightFreq800.Size = new System.Drawing.Size(10, 61);
            this.msRightFreq800.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msRightFreq800.SmallChange = 1;
            this.msRightFreq800.SmoothScrolling = false;
            this.msRightFreq800.TabIndex = 36;
            this.msRightFreq800.TickColor = System.Drawing.Color.DarkGray;
            this.msRightFreq800.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msRightFreq800.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msRightFreq800.TrackBorderColor = System.Drawing.Color.Gray;
            this.msRightFreq800.TrackDepth = 4;
            this.msRightFreq800.TrackFillColor = System.Drawing.Color.Transparent;
            this.msRightFreq800.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msRightFreq800.TrackShadow = true;
            this.msRightFreq800.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msRightFreq800.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msRightFreq800.Value = 7;
            this.msRightFreq800.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msRightFreq800_ValueChanged);
            // 
            // msRightFreq400
            // 
            this.msRightFreq400.Animated = false;
            this.msRightFreq400.AnimationSize = 0.2F;
            this.msRightFreq400.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msRightFreq400.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msRightFreq400.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msRightFreq400.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq400.BackGroundImage = null;
            this.msRightFreq400.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq400.ButtonBorderColor = System.Drawing.Color.Black;
            this.msRightFreq400.ButtonColor = System.Drawing.Color.Black;
            this.msRightFreq400.ButtonCornerRadius = ((uint)(6u));
            this.msRightFreq400.ButtonSize = new System.Drawing.Size(8, 12);
            this.msRightFreq400.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msRightFreq400.ContextMenuStrip = null;
            this.msRightFreq400.LargeChange = 2;
            this.msRightFreq400.Location = new System.Drawing.Point(232, 21);
            this.msRightFreq400.Margin = new System.Windows.Forms.Padding(0);
            this.msRightFreq400.Maximum = 8;
            this.msRightFreq400.Minimum = -8;
            this.msRightFreq400.Name = "msRightFreq400";
            this.msRightFreq400.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msRightFreq400.ShowButtonOnHover = false;
            this.msRightFreq400.Size = new System.Drawing.Size(10, 61);
            this.msRightFreq400.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msRightFreq400.SmallChange = 1;
            this.msRightFreq400.SmoothScrolling = false;
            this.msRightFreq400.TabIndex = 35;
            this.msRightFreq400.TickColor = System.Drawing.Color.DarkGray;
            this.msRightFreq400.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msRightFreq400.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msRightFreq400.TrackBorderColor = System.Drawing.Color.Gray;
            this.msRightFreq400.TrackDepth = 4;
            this.msRightFreq400.TrackFillColor = System.Drawing.Color.Transparent;
            this.msRightFreq400.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msRightFreq400.TrackShadow = true;
            this.msRightFreq400.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msRightFreq400.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msRightFreq400.Value = 5;
            this.msRightFreq400.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msRightFreq400_ValueChanged);
            // 
            // msRightFreq200
            // 
            this.msRightFreq200.Animated = false;
            this.msRightFreq200.AnimationSize = 0.2F;
            this.msRightFreq200.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msRightFreq200.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msRightFreq200.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msRightFreq200.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq200.BackGroundImage = null;
            this.msRightFreq200.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq200.ButtonBorderColor = System.Drawing.Color.Black;
            this.msRightFreq200.ButtonColor = System.Drawing.Color.Black;
            this.msRightFreq200.ButtonCornerRadius = ((uint)(6u));
            this.msRightFreq200.ButtonSize = new System.Drawing.Size(8, 12);
            this.msRightFreq200.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msRightFreq200.ContextMenuStrip = null;
            this.msRightFreq200.LargeChange = 2;
            this.msRightFreq200.Location = new System.Drawing.Point(214, 21);
            this.msRightFreq200.Margin = new System.Windows.Forms.Padding(0);
            this.msRightFreq200.Maximum = 8;
            this.msRightFreq200.Minimum = -8;
            this.msRightFreq200.Name = "msRightFreq200";
            this.msRightFreq200.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msRightFreq200.ShowButtonOnHover = false;
            this.msRightFreq200.Size = new System.Drawing.Size(10, 61);
            this.msRightFreq200.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msRightFreq200.SmallChange = 1;
            this.msRightFreq200.SmoothScrolling = false;
            this.msRightFreq200.TabIndex = 34;
            this.msRightFreq200.TickColor = System.Drawing.Color.DarkGray;
            this.msRightFreq200.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msRightFreq200.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msRightFreq200.TrackBorderColor = System.Drawing.Color.Gray;
            this.msRightFreq200.TrackDepth = 4;
            this.msRightFreq200.TrackFillColor = System.Drawing.Color.Transparent;
            this.msRightFreq200.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msRightFreq200.TrackShadow = true;
            this.msRightFreq200.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msRightFreq200.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msRightFreq200.Value = 7;
            this.msRightFreq200.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msRightFreq200_ValueChanged);
            // 
            // msRightFreq100
            // 
            this.msRightFreq100.Animated = false;
            this.msRightFreq100.AnimationSize = 0.2F;
            this.msRightFreq100.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msRightFreq100.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msRightFreq100.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msRightFreq100.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq100.BackGroundImage = null;
            this.msRightFreq100.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msRightFreq100.ButtonBorderColor = System.Drawing.Color.Black;
            this.msRightFreq100.ButtonColor = System.Drawing.Color.Black;
            this.msRightFreq100.ButtonCornerRadius = ((uint)(6u));
            this.msRightFreq100.ButtonSize = new System.Drawing.Size(8, 12);
            this.msRightFreq100.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msRightFreq100.ContextMenuStrip = null;
            this.msRightFreq100.LargeChange = 2;
            this.msRightFreq100.Location = new System.Drawing.Point(189, 21);
            this.msRightFreq100.Margin = new System.Windows.Forms.Padding(0);
            this.msRightFreq100.Maximum = 8;
            this.msRightFreq100.Minimum = -8;
            this.msRightFreq100.Name = "msRightFreq100";
            this.msRightFreq100.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msRightFreq100.ShowButtonOnHover = false;
            this.msRightFreq100.Size = new System.Drawing.Size(18, 61);
            this.msRightFreq100.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msRightFreq100.SmallChange = 1;
            this.msRightFreq100.SmoothScrolling = false;
            this.msRightFreq100.TabIndex = 33;
            this.msRightFreq100.TickColor = System.Drawing.Color.DarkGray;
            this.msRightFreq100.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.msRightFreq100.TickType = MediaSlider.MediaSlider.TickMode.Composite;
            this.msRightFreq100.TrackBorderColor = System.Drawing.Color.Gray;
            this.msRightFreq100.TrackDepth = 4;
            this.msRightFreq100.TrackFillColor = System.Drawing.Color.Transparent;
            this.msRightFreq100.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msRightFreq100.TrackShadow = true;
            this.msRightFreq100.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msRightFreq100.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msRightFreq100.Value = 5;
            this.msRightFreq100.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msRightFreq100_ValueChanged);
            // 
            // lblFq
            // 
            this.lblFq.AutoSize = true;
            this.lblFq.BackColor = System.Drawing.Color.Transparent;
            this.lblFq.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblFq.Location = new System.Drawing.Point(6, 6);
            this.lblFq.Name = "lblFq";
            this.lblFq.Size = new System.Drawing.Size(28, 9);
            this.lblFq.TabIndex = 32;
            this.lblFq.Text = "Fq: Hz";
            // 
            // lblDb3
            // 
            this.lblDb3.AutoSize = true;
            this.lblDb3.BackColor = System.Drawing.Color.Transparent;
            this.lblDb3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDb3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblDb3.Location = new System.Drawing.Point(7, 72);
            this.lblDb3.Name = "lblDb3";
            this.lblDb3.Size = new System.Drawing.Size(20, 9);
            this.lblDb3.TabIndex = 31;
            this.lblDb3.Text = "-8db";
            // 
            // lblDb1
            // 
            this.lblDb1.AutoSize = true;
            this.lblDb1.BackColor = System.Drawing.Color.Transparent;
            this.lblDb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDb1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblDb1.Location = new System.Drawing.Point(7, 20);
            this.lblDb1.Name = "lblDb1";
            this.lblDb1.Size = new System.Drawing.Size(22, 9);
            this.lblDb1.TabIndex = 30;
            this.lblDb1.Text = "+8db";
            // 
            // lblDb2
            // 
            this.lblDb2.AutoSize = true;
            this.lblDb2.BackColor = System.Drawing.Color.Transparent;
            this.lblDb2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDb2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblDb2.Location = new System.Drawing.Point(16, 47);
            this.lblDb2.Name = "lblDb2";
            this.lblDb2.Size = new System.Drawing.Size(9, 9);
            this.lblDb2.TabIndex = 29;
            this.lblDb2.Text = "0";
            // 
            // lblFq7
            // 
            this.lblFq7.AutoSize = true;
            this.lblFq7.BackColor = System.Drawing.Color.Transparent;
            this.lblFq7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFq7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblFq7.Location = new System.Drawing.Point(141, 7);
            this.lblFq7.Name = "lblFq7";
            this.lblFq7.Size = new System.Drawing.Size(19, 9);
            this.lblFq7.TabIndex = 28;
            this.lblFq7.Text = "6.4k";
            // 
            // lblFq6
            // 
            this.lblFq6.AutoSize = true;
            this.lblFq6.BackColor = System.Drawing.Color.Transparent;
            this.lblFq6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFq6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblFq6.Location = new System.Drawing.Point(122, 7);
            this.lblFq6.Name = "lblFq6";
            this.lblFq6.Size = new System.Drawing.Size(19, 9);
            this.lblFq6.TabIndex = 27;
            this.lblFq6.Text = "3.2k";
            // 
            // lblFq5
            // 
            this.lblFq5.AutoSize = true;
            this.lblFq5.BackColor = System.Drawing.Color.Transparent;
            this.lblFq5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFq5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblFq5.Location = new System.Drawing.Point(102, 7);
            this.lblFq5.Name = "lblFq5";
            this.lblFq5.Size = new System.Drawing.Size(19, 9);
            this.lblFq5.TabIndex = 26;
            this.lblFq5.Text = "1.6k";
            // 
            // lblFq4
            // 
            this.lblFq4.AutoSize = true;
            this.lblFq4.BackColor = System.Drawing.Color.Transparent;
            this.lblFq4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFq4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblFq4.Location = new System.Drawing.Point(86, 7);
            this.lblFq4.Name = "lblFq4";
            this.lblFq4.Size = new System.Drawing.Size(17, 9);
            this.lblFq4.TabIndex = 25;
            this.lblFq4.Text = "800";
            // 
            // lblFq3
            // 
            this.lblFq3.AutoSize = true;
            this.lblFq3.BackColor = System.Drawing.Color.Transparent;
            this.lblFq3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFq3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblFq3.Location = new System.Drawing.Point(67, 7);
            this.lblFq3.Name = "lblFq3";
            this.lblFq3.Size = new System.Drawing.Size(17, 9);
            this.lblFq3.TabIndex = 24;
            this.lblFq3.Text = "400";
            // 
            // lblFq2
            // 
            this.lblFq2.AutoSize = true;
            this.lblFq2.BackColor = System.Drawing.Color.Transparent;
            this.lblFq2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFq2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblFq2.Location = new System.Drawing.Point(49, 7);
            this.lblFq2.Name = "lblFq2";
            this.lblFq2.Size = new System.Drawing.Size(17, 9);
            this.lblFq2.TabIndex = 23;
            this.lblFq2.Text = "200";
            // 
            // lblFq1
            // 
            this.lblFq1.AutoSize = true;
            this.lblFq1.BackColor = System.Drawing.Color.Transparent;
            this.lblFq1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFq1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblFq1.Location = new System.Drawing.Point(30, 7);
            this.lblFq1.Name = "lblFq1";
            this.lblFq1.Size = new System.Drawing.Size(17, 9);
            this.lblFq1.TabIndex = 22;
            this.lblFq1.Text = "100";
            // 
            // msLeftFreq6400
            // 
            this.msLeftFreq6400.Animated = false;
            this.msLeftFreq6400.AnimationSize = 0.2F;
            this.msLeftFreq6400.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msLeftFreq6400.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msLeftFreq6400.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msLeftFreq6400.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq6400.BackGroundImage = null;
            this.msLeftFreq6400.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq6400.ButtonBorderColor = System.Drawing.Color.Black;
            this.msLeftFreq6400.ButtonColor = System.Drawing.Color.Black;
            this.msLeftFreq6400.ButtonCornerRadius = ((uint)(6u));
            this.msLeftFreq6400.ButtonSize = new System.Drawing.Size(8, 12);
            this.msLeftFreq6400.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msLeftFreq6400.ContextMenuStrip = null;
            this.msLeftFreq6400.LargeChange = 2;
            this.msLeftFreq6400.Location = new System.Drawing.Point(145, 21);
            this.msLeftFreq6400.Margin = new System.Windows.Forms.Padding(0);
            this.msLeftFreq6400.Maximum = 8;
            this.msLeftFreq6400.Minimum = -8;
            this.msLeftFreq6400.Name = "msLeftFreq6400";
            this.msLeftFreq6400.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msLeftFreq6400.ShowButtonOnHover = false;
            this.msLeftFreq6400.Size = new System.Drawing.Size(10, 61);
            this.msLeftFreq6400.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msLeftFreq6400.SmallChange = 1;
            this.msLeftFreq6400.SmoothScrolling = false;
            this.msLeftFreq6400.TabIndex = 21;
            this.msLeftFreq6400.TickColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq6400.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msLeftFreq6400.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msLeftFreq6400.TrackBorderColor = System.Drawing.Color.Gray;
            this.msLeftFreq6400.TrackDepth = 4;
            this.msLeftFreq6400.TrackFillColor = System.Drawing.Color.Transparent;
            this.msLeftFreq6400.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msLeftFreq6400.TrackShadow = true;
            this.msLeftFreq6400.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq6400.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msLeftFreq6400.Value = 8;
            this.msLeftFreq6400.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msLeftFreq6400_ValueChanged);
            // 
            // msLeftFreq3200
            // 
            this.msLeftFreq3200.Animated = false;
            this.msLeftFreq3200.AnimationSize = 0.2F;
            this.msLeftFreq3200.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msLeftFreq3200.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msLeftFreq3200.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msLeftFreq3200.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq3200.BackGroundImage = null;
            this.msLeftFreq3200.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq3200.ButtonBorderColor = System.Drawing.Color.Black;
            this.msLeftFreq3200.ButtonColor = System.Drawing.Color.Black;
            this.msLeftFreq3200.ButtonCornerRadius = ((uint)(6u));
            this.msLeftFreq3200.ButtonSize = new System.Drawing.Size(8, 12);
            this.msLeftFreq3200.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msLeftFreq3200.ContextMenuStrip = null;
            this.msLeftFreq3200.LargeChange = 2;
            this.msLeftFreq3200.Location = new System.Drawing.Point(126, 21);
            this.msLeftFreq3200.Margin = new System.Windows.Forms.Padding(0);
            this.msLeftFreq3200.Maximum = 8;
            this.msLeftFreq3200.Minimum = -8;
            this.msLeftFreq3200.Name = "msLeftFreq3200";
            this.msLeftFreq3200.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msLeftFreq3200.ShowButtonOnHover = false;
            this.msLeftFreq3200.Size = new System.Drawing.Size(10, 61);
            this.msLeftFreq3200.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msLeftFreq3200.SmallChange = 1;
            this.msLeftFreq3200.SmoothScrolling = false;
            this.msLeftFreq3200.TabIndex = 20;
            this.msLeftFreq3200.TickColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq3200.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msLeftFreq3200.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msLeftFreq3200.TrackBorderColor = System.Drawing.Color.Gray;
            this.msLeftFreq3200.TrackDepth = 4;
            this.msLeftFreq3200.TrackFillColor = System.Drawing.Color.Transparent;
            this.msLeftFreq3200.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msLeftFreq3200.TrackShadow = true;
            this.msLeftFreq3200.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq3200.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msLeftFreq3200.Value = 5;
            this.msLeftFreq3200.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msLeftFreq3200_ValueChanged);
            // 
            // msLeftFreq1600
            // 
            this.msLeftFreq1600.Animated = false;
            this.msLeftFreq1600.AnimationSize = 0.2F;
            this.msLeftFreq1600.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msLeftFreq1600.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msLeftFreq1600.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msLeftFreq1600.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq1600.BackGroundImage = null;
            this.msLeftFreq1600.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq1600.ButtonBorderColor = System.Drawing.Color.Black;
            this.msLeftFreq1600.ButtonColor = System.Drawing.Color.Black;
            this.msLeftFreq1600.ButtonCornerRadius = ((uint)(6u));
            this.msLeftFreq1600.ButtonSize = new System.Drawing.Size(8, 12);
            this.msLeftFreq1600.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msLeftFreq1600.ContextMenuStrip = null;
            this.msLeftFreq1600.LargeChange = 2;
            this.msLeftFreq1600.Location = new System.Drawing.Point(108, 21);
            this.msLeftFreq1600.Margin = new System.Windows.Forms.Padding(0);
            this.msLeftFreq1600.Maximum = 8;
            this.msLeftFreq1600.Minimum = -8;
            this.msLeftFreq1600.Name = "msLeftFreq1600";
            this.msLeftFreq1600.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msLeftFreq1600.ShowButtonOnHover = false;
            this.msLeftFreq1600.Size = new System.Drawing.Size(10, 61);
            this.msLeftFreq1600.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msLeftFreq1600.SmallChange = 1;
            this.msLeftFreq1600.SmoothScrolling = false;
            this.msLeftFreq1600.TabIndex = 19;
            this.msLeftFreq1600.TickColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq1600.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msLeftFreq1600.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msLeftFreq1600.TrackBorderColor = System.Drawing.Color.Gray;
            this.msLeftFreq1600.TrackDepth = 4;
            this.msLeftFreq1600.TrackFillColor = System.Drawing.Color.Transparent;
            this.msLeftFreq1600.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msLeftFreq1600.TrackShadow = true;
            this.msLeftFreq1600.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq1600.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msLeftFreq1600.Value = 7;
            this.msLeftFreq1600.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msLeftFreq1600_ValueChanged);
            // 
            // msLeftFreq800
            // 
            this.msLeftFreq800.Animated = false;
            this.msLeftFreq800.AnimationSize = 0.2F;
            this.msLeftFreq800.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msLeftFreq800.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msLeftFreq800.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msLeftFreq800.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq800.BackGroundImage = null;
            this.msLeftFreq800.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq800.ButtonBorderColor = System.Drawing.Color.Black;
            this.msLeftFreq800.ButtonColor = System.Drawing.Color.Black;
            this.msLeftFreq800.ButtonCornerRadius = ((uint)(6u));
            this.msLeftFreq800.ButtonSize = new System.Drawing.Size(8, 12);
            this.msLeftFreq800.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msLeftFreq800.ContextMenuStrip = null;
            this.msLeftFreq800.LargeChange = 2;
            this.msLeftFreq800.Location = new System.Drawing.Point(89, 21);
            this.msLeftFreq800.Margin = new System.Windows.Forms.Padding(0);
            this.msLeftFreq800.Maximum = 8;
            this.msLeftFreq800.Minimum = -8;
            this.msLeftFreq800.Name = "msLeftFreq800";
            this.msLeftFreq800.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msLeftFreq800.ShowButtonOnHover = false;
            this.msLeftFreq800.Size = new System.Drawing.Size(10, 61);
            this.msLeftFreq800.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msLeftFreq800.SmallChange = 1;
            this.msLeftFreq800.SmoothScrolling = false;
            this.msLeftFreq800.TabIndex = 18;
            this.msLeftFreq800.TickColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq800.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msLeftFreq800.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msLeftFreq800.TrackBorderColor = System.Drawing.Color.Gray;
            this.msLeftFreq800.TrackDepth = 4;
            this.msLeftFreq800.TrackFillColor = System.Drawing.Color.Transparent;
            this.msLeftFreq800.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msLeftFreq800.TrackShadow = true;
            this.msLeftFreq800.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq800.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msLeftFreq800.Value = 5;
            this.msLeftFreq800.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msLeftFreq800_ValueChanged);
            // 
            // msLeftFreq400
            // 
            this.msLeftFreq400.Animated = false;
            this.msLeftFreq400.AnimationSize = 0.2F;
            this.msLeftFreq400.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msLeftFreq400.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msLeftFreq400.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msLeftFreq400.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq400.BackGroundImage = null;
            this.msLeftFreq400.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq400.ButtonBorderColor = System.Drawing.Color.Black;
            this.msLeftFreq400.ButtonColor = System.Drawing.Color.Black;
            this.msLeftFreq400.ButtonCornerRadius = ((uint)(6u));
            this.msLeftFreq400.ButtonSize = new System.Drawing.Size(8, 12);
            this.msLeftFreq400.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msLeftFreq400.ContextMenuStrip = null;
            this.msLeftFreq400.LargeChange = 2;
            this.msLeftFreq400.Location = new System.Drawing.Point(71, 21);
            this.msLeftFreq400.Margin = new System.Windows.Forms.Padding(0);
            this.msLeftFreq400.Maximum = 8;
            this.msLeftFreq400.Minimum = -8;
            this.msLeftFreq400.Name = "msLeftFreq400";
            this.msLeftFreq400.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msLeftFreq400.ShowButtonOnHover = false;
            this.msLeftFreq400.Size = new System.Drawing.Size(10, 61);
            this.msLeftFreq400.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msLeftFreq400.SmallChange = 1;
            this.msLeftFreq400.SmoothScrolling = false;
            this.msLeftFreq400.TabIndex = 17;
            this.msLeftFreq400.TickColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq400.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msLeftFreq400.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msLeftFreq400.TrackBorderColor = System.Drawing.Color.Gray;
            this.msLeftFreq400.TrackDepth = 4;
            this.msLeftFreq400.TrackFillColor = System.Drawing.Color.Transparent;
            this.msLeftFreq400.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msLeftFreq400.TrackShadow = true;
            this.msLeftFreq400.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq400.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msLeftFreq400.Value = 7;
            this.msLeftFreq400.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msLeftFreq400_ValueChanged);
            // 
            // msLeftFreq200
            // 
            this.msLeftFreq200.Animated = false;
            this.msLeftFreq200.AnimationSize = 0.2F;
            this.msLeftFreq200.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msLeftFreq200.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msLeftFreq200.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msLeftFreq200.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq200.BackGroundImage = null;
            this.msLeftFreq200.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq200.ButtonBorderColor = System.Drawing.Color.Black;
            this.msLeftFreq200.ButtonColor = System.Drawing.Color.Black;
            this.msLeftFreq200.ButtonCornerRadius = ((uint)(6u));
            this.msLeftFreq200.ButtonSize = new System.Drawing.Size(8, 12);
            this.msLeftFreq200.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msLeftFreq200.ContextMenuStrip = null;
            this.msLeftFreq200.LargeChange = 2;
            this.msLeftFreq200.Location = new System.Drawing.Point(53, 21);
            this.msLeftFreq200.Margin = new System.Windows.Forms.Padding(0);
            this.msLeftFreq200.Maximum = 8;
            this.msLeftFreq200.Minimum = -8;
            this.msLeftFreq200.Name = "msLeftFreq200";
            this.msLeftFreq200.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msLeftFreq200.ShowButtonOnHover = false;
            this.msLeftFreq200.Size = new System.Drawing.Size(10, 61);
            this.msLeftFreq200.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msLeftFreq200.SmallChange = 1;
            this.msLeftFreq200.SmoothScrolling = false;
            this.msLeftFreq200.TabIndex = 16;
            this.msLeftFreq200.TickColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq200.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msLeftFreq200.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msLeftFreq200.TrackBorderColor = System.Drawing.Color.Gray;
            this.msLeftFreq200.TrackDepth = 4;
            this.msLeftFreq200.TrackFillColor = System.Drawing.Color.Transparent;
            this.msLeftFreq200.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msLeftFreq200.TrackShadow = true;
            this.msLeftFreq200.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq200.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msLeftFreq200.Value = 5;
            this.msLeftFreq200.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msLeftFreq200_ValueChanged);
            // 
            // msLeftFreq100
            // 
            this.msLeftFreq100.Animated = false;
            this.msLeftFreq100.AnimationSize = 0.2F;
            this.msLeftFreq100.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msLeftFreq100.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msLeftFreq100.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msLeftFreq100.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq100.BackGroundImage = null;
            this.msLeftFreq100.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msLeftFreq100.ButtonBorderColor = System.Drawing.Color.Black;
            this.msLeftFreq100.ButtonColor = System.Drawing.Color.Black;
            this.msLeftFreq100.ButtonCornerRadius = ((uint)(6u));
            this.msLeftFreq100.ButtonSize = new System.Drawing.Size(8, 12);
            this.msLeftFreq100.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msLeftFreq100.ContextMenuStrip = null;
            this.msLeftFreq100.LargeChange = 2;
            this.msLeftFreq100.Location = new System.Drawing.Point(28, 21);
            this.msLeftFreq100.Margin = new System.Windows.Forms.Padding(0);
            this.msLeftFreq100.Maximum = 8;
            this.msLeftFreq100.Minimum = -8;
            this.msLeftFreq100.Name = "msLeftFreq100";
            this.msLeftFreq100.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.msLeftFreq100.ShowButtonOnHover = false;
            this.msLeftFreq100.Size = new System.Drawing.Size(18, 61);
            this.msLeftFreq100.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msLeftFreq100.SmallChange = 1;
            this.msLeftFreq100.SmoothScrolling = false;
            this.msLeftFreq100.TabIndex = 15;
            this.msLeftFreq100.TickColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq100.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.msLeftFreq100.TickType = MediaSlider.MediaSlider.TickMode.Composite;
            this.msLeftFreq100.TrackBorderColor = System.Drawing.Color.Gray;
            this.msLeftFreq100.TrackDepth = 4;
            this.msLeftFreq100.TrackFillColor = System.Drawing.Color.Transparent;
            this.msLeftFreq100.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msLeftFreq100.TrackShadow = true;
            this.msLeftFreq100.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msLeftFreq100.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msLeftFreq100.Value = 5;
            this.msLeftFreq100.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msLeftFreq100_ValueChanged);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemOpen,
            this.mnuItemSave,
            this.mnuItemSaveAs,
            this.toolStripMenuItem2,
            this.mnuItemEqualizer,
            this.mnuItemHighPass,
            this.mnuItemLowPass,
            this.toolStripMenuItem1,
            this.mnuItemRepeat,
            this.mnuItemMute,
            this.toolStripMenuItem3,
            this.mnuItemDevices,
            this.toolStripMenuItem4,
            this.mnuItemExit});
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mnuMain.Size = new System.Drawing.Size(124, 248);
            // 
            // mnuItemOpen
            // 
            this.mnuItemOpen.Name = "mnuItemOpen";
            this.mnuItemOpen.Size = new System.Drawing.Size(123, 22);
            this.mnuItemOpen.Text = "Open";
            this.mnuItemOpen.Click += new System.EventHandler(this.mnuItemOpen_Click);
            // 
            // mnuItemSave
            // 
            this.mnuItemSave.Enabled = false;
            this.mnuItemSave.Name = "mnuItemSave";
            this.mnuItemSave.Size = new System.Drawing.Size(123, 22);
            this.mnuItemSave.Text = "Save";
            this.mnuItemSave.Click += new System.EventHandler(this.mnuItemSave_Click);
            // 
            // mnuItemSaveAs
            // 
            this.mnuItemSaveAs.Enabled = false;
            this.mnuItemSaveAs.Name = "mnuItemSaveAs";
            this.mnuItemSaveAs.Size = new System.Drawing.Size(123, 22);
            this.mnuItemSaveAs.Text = "Save As";
            this.mnuItemSaveAs.Click += new System.EventHandler(this.mnuItemSaveAs_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(120, 6);
            // 
            // mnuItemEqualizer
            // 
            this.mnuItemEqualizer.Name = "mnuItemEqualizer";
            this.mnuItemEqualizer.Size = new System.Drawing.Size(123, 22);
            this.mnuItemEqualizer.Text = "Equalizer";
            this.mnuItemEqualizer.Click += new System.EventHandler(this.mnuItemEqualizer_Click);
            // 
            // mnuItemHighPass
            // 
            this.mnuItemHighPass.Name = "mnuItemHighPass";
            this.mnuItemHighPass.Size = new System.Drawing.Size(123, 22);
            this.mnuItemHighPass.Text = "HighPass";
            this.mnuItemHighPass.Click += new System.EventHandler(this.mnuItemHighPass_Click);
            // 
            // mnuItemLowPass
            // 
            this.mnuItemLowPass.Name = "mnuItemLowPass";
            this.mnuItemLowPass.Size = new System.Drawing.Size(123, 22);
            this.mnuItemLowPass.Text = "LowPass";
            this.mnuItemLowPass.Click += new System.EventHandler(this.mnuItemLowPass_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(120, 6);
            // 
            // mnuItemRepeat
            // 
            this.mnuItemRepeat.Name = "mnuItemRepeat";
            this.mnuItemRepeat.Size = new System.Drawing.Size(123, 22);
            this.mnuItemRepeat.Text = "Repeat";
            this.mnuItemRepeat.Click += new System.EventHandler(this.mnuItemRepeat_Click);
            // 
            // mnuItemMute
            // 
            this.mnuItemMute.Name = "mnuItemMute";
            this.mnuItemMute.Size = new System.Drawing.Size(123, 22);
            this.mnuItemMute.Text = "Mute";
            this.mnuItemMute.Click += new System.EventHandler(this.mnuItemMute_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(120, 6);
            // 
            // mnuItemDevices
            // 
            this.mnuItemDevices.Name = "mnuItemDevices";
            this.mnuItemDevices.Size = new System.Drawing.Size(123, 22);
            this.mnuItemDevices.Text = "Devices";
            this.mnuItemDevices.Click += new System.EventHandler(this.mnuItemOptions_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(120, 6);
            // 
            // mnuItemExit
            // 
            this.mnuItemExit.Name = "mnuItemExit";
            this.mnuItemExit.Size = new System.Drawing.Size(123, 22);
            this.mnuItemExit.Text = "Exit";
            this.mnuItemExit.Click += new System.EventHandler(this.mnuItemExit_Click);
            // 
            // pnlEq
            // 
            this.pnlEq.BackColor = System.Drawing.Color.Transparent;
            this.pnlEq.BackgroundImage = global::Personal_Wave_Recorder.Properties.Resources.sliderbgvert;
            this.pnlEq.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlEq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEq.Controls.Add(this.msVolume);
            this.pnlEq.Controls.Add(this.btnMute);
            this.pnlEq.Controls.Add(this.btnStop);
            this.pnlEq.Controls.Add(this.btnPlay);
            this.pnlEq.Controls.Add(this.btnRecord);
            this.pnlEq.Controls.Add(this.btnFastForward);
            this.pnlEq.Controls.Add(this.btnRewind);
            this.pnlEq.Controls.Add(this.msPosition);
            this.pnlEq.Controls.Add(this.btnEqRollup);
            this.pnlEq.Controls.Add(this.lblHighPass);
            this.pnlEq.Controls.Add(this.lblLowPass);
            this.pnlEq.Controls.Add(this.lblRepeat);
            this.pnlEq.Controls.Add(this.lblEq);
            this.pnlEq.Controls.Add(this.lblChannels);
            this.pnlEq.Controls.Add(this.lblFrequency);
            this.pnlEq.Controls.Add(this.lblBitRate);
            this.pnlEq.Controls.Add(this.lblPosition);
            this.pnlEq.Controls.Add(this.lblDuration);
            this.pnlEq.Controls.Add(this.lblTime);
            this.pnlEq.Controls.Add(this.pbLeftGraph);
            this.pnlEq.Controls.Add(this.pbRightGraph);
            this.pnlEq.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEq.Location = new System.Drawing.Point(0, 0);
            this.pnlEq.Name = "pnlEq";
            this.pnlEq.Size = new System.Drawing.Size(341, 120);
            this.pnlEq.TabIndex = 16;
            this.pnlEq.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlEq_MouseClick);
            // 
            // msVolume
            // 
            this.msVolume.Animated = false;
            this.msVolume.AnimationSize = 0.2F;
            this.msVolume.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msVolume.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msVolume.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msVolume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.msVolume.BackGroundImage = null;
            this.msVolume.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.msVolume.ButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msVolume.ButtonColor = System.Drawing.Color.Black;
            this.msVolume.ButtonCornerRadius = ((uint)(4u));
            this.msVolume.ButtonSize = new System.Drawing.Size(12, 12);
            this.msVolume.ButtonStyle = MediaSlider.MediaSlider.ButtonType.Round;
            this.msVolume.ContextMenuStrip = null;
            this.msVolume.LargeChange = 2;
            this.msVolume.Location = new System.Drawing.Point(224, 82);
            this.msVolume.Margin = new System.Windows.Forms.Padding(0);
            this.msVolume.Maximum = 255;
            this.msVolume.Minimum = 0;
            this.msVolume.Name = "msVolume";
            this.msVolume.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.msVolume.ShowButtonOnHover = false;
            this.msVolume.Size = new System.Drawing.Size(75, 24);
            this.msVolume.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msVolume.SmallChange = 1;
            this.msVolume.SmoothScrolling = true;
            this.msVolume.TabIndex = 53;
            this.msVolume.TickColor = System.Drawing.Color.DarkGray;
            this.msVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msVolume.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msVolume.TrackBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.msVolume.TrackDepth = 4;
            this.msVolume.TrackFillColor = System.Drawing.Color.Transparent;
            this.msVolume.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msVolume.TrackShadow = true;
            this.msVolume.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msVolume.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msVolume.Value = 127;
            this.msVolume.MouseLeave += new System.EventHandler(this.msVolume_MouseLeave);
            this.msVolume.ValueChanged += new MediaSlider.MediaSlider.ValueChangedDelegate(this.msVolume_ValueChanged);
            this.msVolume.MouseHover += new System.EventHandler(this.msVolume_MouseHover);
            // 
            // btnMute
            // 
            this.btnMute.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.btnMute.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.btnMute.AutoSize = true;
            this.btnMute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnMute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMute.BackgroundImage")));
            this.btnMute.Checked = false;
            this.btnMute.CheckedBorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnMute.CheckStyle = GlowButton.GlowButton.CheckedStyle.ColorChange;
            this.btnMute.ContextMenuStrip = null;
            this.btnMute.FocusedMask = false;
            this.btnMute.FocusOnHover = false;
            this.btnMute.Image = global::Personal_Wave_Recorder.Properties.Resources.mute;
            this.btnMute.ImageCheckedColor = System.Drawing.Color.Firebrick;
            this.btnMute.ImageDisabledColor = System.Drawing.Color.Transparent;
            this.btnMute.ImageFocusedColor = System.Drawing.Color.SkyBlue;
            this.btnMute.ImageGlowColor = System.Drawing.Color.SteelBlue;
            this.btnMute.ImageGlowFactor = 2;
            this.btnMute.ImageHoverColor = System.Drawing.Color.LightSkyBlue;
            this.btnMute.ImageMirror = true;
            this.btnMute.ImagePressedColor = System.Drawing.Color.SteelBlue;
            this.btnMute.Location = new System.Drawing.Point(205, 83);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(20, 31);
            this.btnMute.TabIndex = 60;
            this.btnMute.Tag = "Mute";
            this.btnMute.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnMute.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnMute_MouseClick);
            this.btnMute.MouseHover += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnStop
            // 
            this.btnStop.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.btnStop.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.btnStop.AutoSize = true;
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStop.BackgroundImage")));
            this.btnStop.Checked = false;
            this.btnStop.CheckedBorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnStop.CheckStyle = GlowButton.GlowButton.CheckedStyle.None;
            this.btnStop.ContextMenuStrip = null;
            this.btnStop.Enabled = false;
            this.btnStop.FocusedMask = false;
            this.btnStop.FocusOnHover = false;
            this.btnStop.Image = global::Personal_Wave_Recorder.Properties.Resources.stop;
            this.btnStop.ImageCheckedColor = System.Drawing.Color.SteelBlue;
            this.btnStop.ImageDisabledColor = System.Drawing.Color.Transparent;
            this.btnStop.ImageFocusedColor = System.Drawing.Color.SkyBlue;
            this.btnStop.ImageGlowColor = System.Drawing.Color.SteelBlue;
            this.btnStop.ImageGlowFactor = 2;
            this.btnStop.ImageHoverColor = System.Drawing.Color.LightSkyBlue;
            this.btnStop.ImageMirror = true;
            this.btnStop.ImagePressedColor = System.Drawing.Color.SteelBlue;
            this.btnStop.Location = new System.Drawing.Point(97, 87);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(15, 22);
            this.btnStop.TabIndex = 59;
            this.btnStop.Tag = "Stop";
            this.btnStop.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnStop.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnStop_MouseClick);
            this.btnStop.MouseHover += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnPlay
            // 
            this.btnPlay.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.btnPlay.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.btnPlay.AutoSize = true;
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPlay.BackgroundImage")));
            this.btnPlay.Checked = false;
            this.btnPlay.CheckedBorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnPlay.CheckStyle = GlowButton.GlowButton.CheckedStyle.None;
            this.btnPlay.ContextMenuStrip = null;
            this.btnPlay.FocusedMask = false;
            this.btnPlay.FocusOnHover = false;
            this.btnPlay.Image = global::Personal_Wave_Recorder.Properties.Resources.play;
            this.btnPlay.ImageCheckedColor = System.Drawing.Color.SteelBlue;
            this.btnPlay.ImageDisabledColor = System.Drawing.Color.Transparent;
            this.btnPlay.ImageFocusedColor = System.Drawing.Color.SkyBlue;
            this.btnPlay.ImageGlowColor = System.Drawing.Color.SteelBlue;
            this.btnPlay.ImageGlowFactor = 2;
            this.btnPlay.ImageHoverColor = System.Drawing.Color.LightSkyBlue;
            this.btnPlay.ImageMirror = true;
            this.btnPlay.ImagePressedColor = System.Drawing.Color.SteelBlue;
            this.btnPlay.Location = new System.Drawing.Point(38, 83);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(20, 31);
            this.btnPlay.TabIndex = 58;
            this.btnPlay.Tag = "Play/Pause";
            this.btnPlay.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnPlay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnPlay_MouseClick);
            this.btnPlay.MouseHover += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnRecord
            // 
            this.btnRecord.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.btnRecord.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.btnRecord.AutoSize = true;
            this.btnRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRecord.BackgroundImage")));
            this.btnRecord.Checked = false;
            this.btnRecord.CheckedBorderColor = System.Drawing.Color.IndianRed;
            this.btnRecord.CheckStyle = GlowButton.GlowButton.CheckedStyle.ColorChange;
            this.btnRecord.ContextMenuStrip = null;
            this.btnRecord.FocusedMask = false;
            this.btnRecord.FocusOnHover = false;
            this.btnRecord.Image = global::Personal_Wave_Recorder.Properties.Resources.record;
            this.btnRecord.ImageCheckedColor = System.Drawing.Color.Firebrick;
            this.btnRecord.ImageDisabledColor = System.Drawing.Color.Transparent;
            this.btnRecord.ImageFocusedColor = System.Drawing.Color.IndianRed;
            this.btnRecord.ImageGlowColor = System.Drawing.Color.Firebrick;
            this.btnRecord.ImageGlowFactor = 2;
            this.btnRecord.ImageHoverColor = System.Drawing.Color.LightCoral;
            this.btnRecord.ImageMirror = true;
            this.btnRecord.ImagePressedColor = System.Drawing.Color.Firebrick;
            this.btnRecord.Location = new System.Drawing.Point(124, 85);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(18, 27);
            this.btnRecord.TabIndex = 57;
            this.btnRecord.Tag = "Record";
            this.btnRecord.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnRecord.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnRecord_MouseClick);
            this.btnRecord.MouseHover += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnFastForward
            // 
            this.btnFastForward.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.btnFastForward.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.btnFastForward.AutoSize = true;
            this.btnFastForward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnFastForward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFastForward.BackgroundImage")));
            this.btnFastForward.Checked = false;
            this.btnFastForward.CheckedBorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnFastForward.CheckStyle = GlowButton.GlowButton.CheckedStyle.None;
            this.btnFastForward.ContextMenuStrip = null;
            this.btnFastForward.Enabled = false;
            this.btnFastForward.FocusedMask = false;
            this.btnFastForward.FocusOnHover = false;
            this.btnFastForward.Image = global::Personal_Wave_Recorder.Properties.Resources.ff;
            this.btnFastForward.ImageCheckedColor = System.Drawing.Color.SteelBlue;
            this.btnFastForward.ImageDisabledColor = System.Drawing.Color.Transparent;
            this.btnFastForward.ImageFocusedColor = System.Drawing.Color.SkyBlue;
            this.btnFastForward.ImageGlowColor = System.Drawing.Color.SteelBlue;
            this.btnFastForward.ImageGlowFactor = 2;
            this.btnFastForward.ImageHoverColor = System.Drawing.Color.LightSkyBlue;
            this.btnFastForward.ImageMirror = true;
            this.btnFastForward.ImagePressedColor = System.Drawing.Color.SteelBlue;
            this.btnFastForward.Location = new System.Drawing.Point(66, 84);
            this.btnFastForward.Name = "btnFastForward";
            this.btnFastForward.Size = new System.Drawing.Size(19, 29);
            this.btnFastForward.TabIndex = 56;
            this.btnFastForward.Tag = "Fast Forward";
            this.btnFastForward.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnFastForward.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnFastForward_MouseClick);
            this.btnFastForward.MouseHover += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnRewind
            // 
            this.btnRewind.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.btnRewind.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.btnRewind.AutoSize = true;
            this.btnRewind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnRewind.BackgroundImage = global::Personal_Wave_Recorder.Properties.Resources.btnbg1;
            this.btnRewind.Checked = false;
            this.btnRewind.CheckedBorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnRewind.CheckStyle = GlowButton.GlowButton.CheckedStyle.None;
            this.btnRewind.ContextMenuStrip = null;
            this.btnRewind.Enabled = false;
            this.btnRewind.FocusedMask = false;
            this.btnRewind.FocusOnHover = false;
            this.btnRewind.Image = global::Personal_Wave_Recorder.Properties.Resources.fr;
            this.btnRewind.ImageCheckedColor = System.Drawing.Color.SteelBlue;
            this.btnRewind.ImageDisabledColor = System.Drawing.Color.Transparent;
            this.btnRewind.ImageFocusedColor = System.Drawing.Color.SkyBlue;
            this.btnRewind.ImageGlowColor = System.Drawing.Color.SteelBlue;
            this.btnRewind.ImageGlowFactor = 2;
            this.btnRewind.ImageHoverColor = System.Drawing.Color.LightSkyBlue;
            this.btnRewind.ImageMirror = true;
            this.btnRewind.ImagePressedColor = System.Drawing.Color.SteelBlue;
            this.btnRewind.Location = new System.Drawing.Point(11, 84);
            this.btnRewind.Name = "btnRewind";
            this.btnRewind.Size = new System.Drawing.Size(19, 29);
            this.btnRewind.TabIndex = 55;
            this.btnRewind.Tag = "Reverse";
            this.btnRewind.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnRewind.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnRewind_MouseClick);
            this.btnRewind.MouseHover += new System.EventHandler(this.Button_MouseEnter);
            // 
            // msPosition
            // 
            this.msPosition.Animated = false;
            this.msPosition.AnimationSize = 0.2F;
            this.msPosition.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msPosition.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msPosition.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msPosition.BackColor = System.Drawing.Color.Transparent;
            this.msPosition.BackGroundImage = null;
            this.msPosition.ButtonAccentColor = System.Drawing.Color.LightSkyBlue;
            this.msPosition.ButtonBorderColor = System.Drawing.Color.SteelBlue;
            this.msPosition.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(131)))), ((int)(((byte)(235)))));
            this.msPosition.ButtonCornerRadius = ((uint)(6u));
            this.msPosition.ButtonSize = new System.Drawing.Size(14, 8);
            this.msPosition.ButtonStyle = MediaSlider.MediaSlider.ButtonType.RoundedRectInline;
            this.msPosition.ContextMenuStrip = null;
            this.msPosition.LargeChange = 2;
            this.msPosition.Location = new System.Drawing.Point(11, 72);
            this.msPosition.Margin = new System.Windows.Forms.Padding(0);
            this.msPosition.Maximum = 10;
            this.msPosition.Minimum = 0;
            this.msPosition.Name = "msPosition";
            this.msPosition.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.msPosition.ShowButtonOnHover = false;
            this.msPosition.Size = new System.Drawing.Size(306, 11);
            this.msPosition.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msPosition.SmallChange = 1;
            this.msPosition.SmoothScrolling = true;
            this.msPosition.TabIndex = 54;
            this.msPosition.TickColor = System.Drawing.Color.DarkGray;
            this.msPosition.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msPosition.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msPosition.TrackBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.msPosition.TrackDepth = 4;
            this.msPosition.TrackFillColor = System.Drawing.Color.Transparent;
            this.msPosition.TrackProgressColor = System.Drawing.Color.LightSkyBlue;
            this.msPosition.TrackShadow = true;
            this.msPosition.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.msPosition.TrackStyle = MediaSlider.MediaSlider.TrackType.Progress;
            this.msPosition.Value = 0;
            this.msPosition.Visible = false;
            this.msPosition.MouseLeave += new System.EventHandler(this.msPosition_MouseLeave);
            this.msPosition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.msPosition_MouseDown);
            this.msPosition.MouseHover += new System.EventHandler(this.msPosition_MouseHover);
            this.msPosition.MouseUp += new System.Windows.Forms.MouseEventHandler(this.msPosition_MouseUp);
            this.msPosition.MouseMove += new System.Windows.Forms.MouseEventHandler(this.msPosition_MouseMove);
            // 
            // btnEqRollup
            // 
            this.btnEqRollup.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.btnEqRollup.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.btnEqRollup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnEqRollup.BackgroundImage = global::Personal_Wave_Recorder.Properties.Resources.btnbg1;
            this.btnEqRollup.Checked = false;
            this.btnEqRollup.CheckedBorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnEqRollup.CheckStyle = GlowButton.GlowButton.CheckedStyle.ColorChange;
            this.btnEqRollup.ContextMenuStrip = null;
            this.btnEqRollup.FocusedMask = false;
            this.btnEqRollup.FocusOnHover = false;
            this.btnEqRollup.Image = global::Personal_Wave_Recorder.Properties.Resources.eq;
            this.btnEqRollup.ImageCheckedColor = System.Drawing.Color.SteelBlue;
            this.btnEqRollup.ImageDisabledColor = System.Drawing.Color.Transparent;
            this.btnEqRollup.ImageFocusedColor = System.Drawing.Color.SkyBlue;
            this.btnEqRollup.ImageGlowColor = System.Drawing.Color.WhiteSmoke;
            this.btnEqRollup.ImageGlowFactor = 2;
            this.btnEqRollup.ImageHoverColor = System.Drawing.Color.LightSkyBlue;
            this.btnEqRollup.ImageMirror = true;
            this.btnEqRollup.ImagePressedColor = System.Drawing.Color.SteelBlue;
            this.btnEqRollup.Location = new System.Drawing.Point(302, 91);
            this.btnEqRollup.Name = "btnEqRollup";
            this.btnEqRollup.Size = new System.Drawing.Size(17, 14);
            this.btnEqRollup.TabIndex = 52;
            this.btnEqRollup.Tag = "Show Equalizer";
            this.btnEqRollup.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnEqRollup.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnEqRollup_MouseClick);
            this.btnEqRollup.MouseHover += new System.EventHandler(this.Button_MouseEnter);
            // 
            // lblHighPass
            // 
            this.lblHighPass.BackColor = System.Drawing.Color.Transparent;
            this.lblHighPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHighPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblHighPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblHighPass.Location = new System.Drawing.Point(263, 46);
            this.lblHighPass.Name = "lblHighPass";
            this.lblHighPass.Size = new System.Drawing.Size(54, 17);
            this.lblHighPass.TabIndex = 12;
            this.lblHighPass.Tag = "High Band Boost : Off";
            this.lblHighPass.Text = "High Boost";
            this.lblHighPass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHighPass.MouseLeave += new System.EventHandler(this.Label_MouseLeave);
            this.lblHighPass.Click += new System.EventHandler(this.lblHighPass_Click);
            this.lblHighPass.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this.lblHighPass.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            // 
            // lblLowPass
            // 
            this.lblLowPass.BackColor = System.Drawing.Color.Transparent;
            this.lblLowPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLowPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblLowPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblLowPass.Location = new System.Drawing.Point(211, 46);
            this.lblLowPass.Name = "lblLowPass";
            this.lblLowPass.Size = new System.Drawing.Size(51, 17);
            this.lblLowPass.TabIndex = 11;
            this.lblLowPass.Tag = "Low Pass Filter : Off";
            this.lblLowPass.Text = "Low Pass";
            this.lblLowPass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLowPass.MouseLeave += new System.EventHandler(this.Label_MouseLeave);
            this.lblLowPass.Click += new System.EventHandler(this.lblLowPass_Click);
            this.lblLowPass.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this.lblLowPass.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            // 
            // lblRepeat
            // 
            this.lblRepeat.BackColor = System.Drawing.Color.Transparent;
            this.lblRepeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRepeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblRepeat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblRepeat.Location = new System.Drawing.Point(172, 46);
            this.lblRepeat.Name = "lblRepeat";
            this.lblRepeat.Size = new System.Drawing.Size(38, 17);
            this.lblRepeat.TabIndex = 10;
            this.lblRepeat.Tag = "Repeat Selection : Off";
            this.lblRepeat.Text = "Repeat";
            this.lblRepeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRepeat.MouseLeave += new System.EventHandler(this.Label_MouseLeave);
            this.lblRepeat.Click += new System.EventHandler(this.lblRepeat_Click);
            this.lblRepeat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this.lblRepeat.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            // 
            // lblEq
            // 
            this.lblEq.BackColor = System.Drawing.Color.Transparent;
            this.lblEq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEq.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblEq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblEq.Location = new System.Drawing.Point(149, 46);
            this.lblEq.Name = "lblEq";
            this.lblEq.Size = new System.Drawing.Size(22, 17);
            this.lblEq.TabIndex = 9;
            this.lblEq.Tag = "Equalizer : Off";
            this.lblEq.Text = "EQ";
            this.lblEq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEq.MouseLeave += new System.EventHandler(this.Label_MouseLeave);
            this.lblEq.Click += new System.EventHandler(this.lblEq_Click);
            this.lblEq.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this.lblEq.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            // 
            // lblChannels
            // 
            this.lblChannels.BackColor = System.Drawing.Color.Transparent;
            this.lblChannels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblChannels.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblChannels.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblChannels.Location = new System.Drawing.Point(98, 47);
            this.lblChannels.Name = "lblChannels";
            this.lblChannels.Size = new System.Drawing.Size(36, 16);
            this.lblChannels.TabIndex = 8;
            this.lblChannels.Text = "Stereo";
            this.lblChannels.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFrequency
            // 
            this.lblFrequency.BackColor = System.Drawing.Color.Transparent;
            this.lblFrequency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblFrequency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblFrequency.Location = new System.Drawing.Point(44, 47);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(53, 16);
            this.lblFrequency.TabIndex = 7;
            this.lblFrequency.Text = "44.0 Khz";
            this.lblFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBitRate
            // 
            this.lblBitRate.BackColor = System.Drawing.Color.Transparent;
            this.lblBitRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBitRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblBitRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblBitRate.Location = new System.Drawing.Point(11, 47);
            this.lblBitRate.Name = "lblBitRate";
            this.lblBitRate.Size = new System.Drawing.Size(32, 16);
            this.lblBitRate.TabIndex = 6;
            this.lblBitRate.Text = "16 bit";
            this.lblBitRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPosition
            // 
            this.lblPosition.BackColor = System.Drawing.Color.Transparent;
            this.lblPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblPosition.Location = new System.Drawing.Point(98, 27);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(36, 19);
            this.lblPosition.TabIndex = 5;
            this.lblPosition.Text = "00:00";
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDuration
            // 
            this.lblDuration.BackColor = System.Drawing.Color.Transparent;
            this.lblDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F);
            this.lblDuration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblDuration.Location = new System.Drawing.Point(98, 7);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(36, 19);
            this.lblDuration.TabIndex = 4;
            this.lblDuration.Text = "00:00";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTime.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblTime.Location = new System.Drawing.Point(11, 7);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(86, 39);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "00:00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbLeftGraph
            // 
            this.pbLeftGraph.BackColor = System.Drawing.Color.Transparent;
            this.pbLeftGraph.BackgroundImage = global::Personal_Wave_Recorder.Properties.Resources.eqbg;
            this.pbLeftGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLeftGraph.Location = new System.Drawing.Point(149, 7);
            this.pbLeftGraph.Name = "pbLeftGraph";
            this.pbLeftGraph.Size = new System.Drawing.Size(83, 38);
            this.pbLeftGraph.TabIndex = 1;
            this.pbLeftGraph.TabStop = false;
            this.pbLeftGraph.Tag = "Time Domain: click to change";
            this.pbLeftGraph.MouseLeave += new System.EventHandler(this.PictureBox_MouseLeave);
            this.pbLeftGraph.Click += new System.EventHandler(this.PictureBox_Click);
            this.pbLeftGraph.MouseHover += new System.EventHandler(this.PictureBox_MouseHover);
            // 
            // pbRightGraph
            // 
            this.pbRightGraph.BackColor = System.Drawing.Color.Transparent;
            this.pbRightGraph.BackgroundImage = global::Personal_Wave_Recorder.Properties.Resources.eqbg;
            this.pbRightGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbRightGraph.Location = new System.Drawing.Point(234, 7);
            this.pbRightGraph.Name = "pbRightGraph";
            this.pbRightGraph.Size = new System.Drawing.Size(83, 38);
            this.pbRightGraph.TabIndex = 0;
            this.pbRightGraph.TabStop = false;
            this.pbRightGraph.Tag = "Time Domain: click to change";
            this.pbRightGraph.MouseLeave += new System.EventHandler(this.PictureBox_MouseLeave);
            this.pbRightGraph.Click += new System.EventHandler(this.PictureBox_Click);
            this.pbRightGraph.MouseHover += new System.EventHandler(this.PictureBox_MouseHover);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(341, 125);
            this.Controls.Add(this.pnlEq);
            this.Controls.Add(this.pnlSliders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PWR";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.pnlSliders.ResumeLayout(false);
            this.pnlSliders.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.pnlEq.ResumeLayout(false);
            this.pnlEq.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSliders;
        private System.Windows.Forms.Label lblFq;
        private System.Windows.Forms.Label lblDb3;
        private System.Windows.Forms.Label lblDb1;
        private System.Windows.Forms.Label lblDb2;
        private System.Windows.Forms.Label lblFq7;
        private System.Windows.Forms.Label lblFq6;
        private System.Windows.Forms.Label lblFq5;
        private System.Windows.Forms.Label lblFq4;
        private System.Windows.Forms.Label lblFq3;
        private System.Windows.Forms.Label lblFq2;
        private System.Windows.Forms.Label lblFq1;
        private MediaSlider.MediaSlider msLeftFreq6400;
        private MediaSlider.MediaSlider msLeftFreq3200;
        private MediaSlider.MediaSlider msLeftFreq1600;
        private MediaSlider.MediaSlider msLeftFreq800;
        private MediaSlider.MediaSlider msLeftFreq400;
        private MediaSlider.MediaSlider msLeftFreq200;
        private MediaSlider.MediaSlider msLeftFreq100;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private MediaSlider.MediaSlider msRightFreq6400;
        private MediaSlider.MediaSlider msRightFreq3200;
        private MediaSlider.MediaSlider msRightFreq1600;
        private MediaSlider.MediaSlider msRightFreq800;
        private MediaSlider.MediaSlider msRightFreq400;
        private MediaSlider.MediaSlider msRightFreq200;
        private MediaSlider.MediaSlider msRightFreq100;
        private System.Windows.Forms.Panel pnlEq;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.PictureBox pbLeftGraph;
        private System.Windows.Forms.PictureBox pbRightGraph;
        private System.Windows.Forms.Label lblBitRate;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblChannels;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.Label lblLowPass;
        private System.Windows.Forms.Label lblRepeat;
        private System.Windows.Forms.Label lblEq;
        private System.Windows.Forms.Label lblHighPass;
        private GlowButton.GlowButton btnEqRollup;
        private GlowButton.GlowButton btnMute;
        private GlowButton.GlowButton btnStop;
        private GlowButton.GlowButton btnPlay;
        private GlowButton.GlowButton btnRecord;
        private GlowButton.GlowButton btnFastForward;
        private GlowButton.GlowButton btnRewind;
        private MediaSlider.MediaSlider msPosition;
        private MediaSlider.MediaSlider msVolume;
        private System.Windows.Forms.ContextMenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuItemSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuItemExit;
        private System.Windows.Forms.ToolStripMenuItem mnuItemEqualizer;
        private System.Windows.Forms.ToolStripMenuItem mnuItemHighPass;
        private System.Windows.Forms.ToolStripMenuItem mnuItemLowPass;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuItemRepeat;
        private System.Windows.Forms.ToolStripMenuItem mnuItemMute;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuItemDevices;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.OpenFileDialog ofdFile;
        private System.Windows.Forms.ToolStripMenuItem mnuItemSave;
        private System.Windows.Forms.SaveFileDialog sfdSave;















    }
}

