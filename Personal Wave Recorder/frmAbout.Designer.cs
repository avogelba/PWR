namespace Personal_Wave_Recorder
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCopy = new System.Windows.Forms.Label();
            this.lblMain = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlFill = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.pnlFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.Color.Transparent;
            this.pbImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbImage.Image = ((System.Drawing.Image)(resources.GetObject("pbImage.Image")));
            this.pbImage.Location = new System.Drawing.Point(1, 35);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(290, 150);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbImage.TabIndex = 17;
            this.pbImage.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitle.Location = new System.Drawing.Point(10, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(165, 15);
            this.lblTitle.TabIndex = 24;
            this.lblTitle.Text = "Personal Wave Recorder";
            // 
            // lblCopy
            // 
            this.lblCopy.AutoSize = true;
            this.lblCopy.BackColor = System.Drawing.Color.Transparent;
            this.lblCopy.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCopy.Location = new System.Drawing.Point(10, 196);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(152, 13);
            this.lblCopy.TabIndex = 23;
            this.lblCopy.Text = "VTDev.com, all rights reserved";
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.BackColor = System.Drawing.Color.Transparent;
            this.lblMain.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblMain.Location = new System.Drawing.Point(10, 20);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(245, 13);
            this.lblMain.TabIndex = 22;
            this.lblMain.Text = "Personal Wave Recorder courtesy of: VTDev.com";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnClose.BackgroundImage = global::Personal_Wave_Recorder.Properties.Resources.btnorm;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(212, 193);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 21);
            this.btnClose.TabIndex = 26;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            // 
            // pnlFill
            // 
            this.pnlFill.BackgroundImage = global::Personal_Wave_Recorder.Properties.Resources.sliderbgvert;
            this.pnlFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFill.Controls.Add(this.btnClose);
            this.pnlFill.Controls.Add(this.pbImage);
            this.pnlFill.Controls.Add(this.lblTitle);
            this.pnlFill.Controls.Add(this.lblCopy);
            this.pnlFill.Controls.Add(this.lblMain);
            this.pnlFill.Location = new System.Drawing.Point(1, 0);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(294, 226);
            this.pnlFill.TabIndex = 27;
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(295, 225);
            this.Controls.Add(this.pnlFill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About PWR";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAbout_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.pnlFill.ResumeLayout(false);
            this.pnlFill.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.Label lblMain;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlFill;

    }
}