namespace Personal_Wave_Recorder
{
    partial class frmDevices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDevices));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.cbOutputDevices = new System.Windows.Forms.ComboBox();
            this.lblInput = new System.Windows.Forms.Label();
            this.cbInputDevices = new System.Windows.Forms.ComboBox();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnCancel.BackgroundImage = global::Personal_Wave_Recorder.Properties.Resources.btnorm;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(137, 117);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 21);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            this.btnCancel.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnSave.BackgroundImage = global::Personal_Wave_Recorder.Properties.Resources.btnorm;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(71, 117);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 21);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            this.btnSave.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.BackColor = System.Drawing.Color.Transparent;
            this.lblOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutput.ForeColor = System.Drawing.Color.White;
            this.lblOutput.Location = new System.Drawing.Point(12, 59);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(83, 15);
            this.lblOutput.TabIndex = 12;
            this.lblOutput.Text = "Output Device";
            // 
            // cbOutputDevices
            // 
            this.cbOutputDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutputDevices.FormattingEnabled = true;
            this.cbOutputDevices.Location = new System.Drawing.Point(15, 78);
            this.cbOutputDevices.Name = "cbOutputDevices";
            this.cbOutputDevices.Size = new System.Drawing.Size(182, 21);
            this.cbOutputDevices.TabIndex = 11;
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.BackColor = System.Drawing.Color.Transparent;
            this.lblInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInput.ForeColor = System.Drawing.Color.White;
            this.lblInput.Location = new System.Drawing.Point(12, 11);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(74, 15);
            this.lblInput.TabIndex = 14;
            this.lblInput.Text = "Input Device";
            // 
            // cbInputDevices
            // 
            this.cbInputDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInputDevices.FormattingEnabled = true;
            this.cbInputDevices.ItemHeight = 13;
            this.cbInputDevices.Location = new System.Drawing.Point(15, 30);
            this.cbInputDevices.Name = "cbInputDevices";
            this.cbInputDevices.Size = new System.Drawing.Size(182, 21);
            this.cbInputDevices.TabIndex = 13;
            // 
            // pnlFill
            // 
            this.pnlFill.BackgroundImage = global::Personal_Wave_Recorder.Properties.Resources.sliderbgvert;
            this.pnlFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFill.Location = new System.Drawing.Point(2, 1);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(220, 152);
            this.pnlFill.TabIndex = 15;
            // 
            // frmDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(221, 154);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.cbInputDevices);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.cbOutputDevices);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlFill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDevices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Devices";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOptions_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.ComboBox cbOutputDevices;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.ComboBox cbInputDevices;
        private System.Windows.Forms.Panel pnlFill;
    }
}