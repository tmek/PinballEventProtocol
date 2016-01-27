namespace Pipes
{
    partial class ClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.lblReceived = new System.Windows.Forms.Label();
            this.tbReceived = new System.Windows.Forms.TextBox();
            this.btnSwitchOn = new System.Windows.Forms.Button();
            this.lblSend = new System.Windows.Forms.Label();
            this.tbSend = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblPipeName = new System.Windows.Forms.Label();
            this.tbPipeName = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numSwitchId = new System.Windows.Forms.NumericUpDown();
            this.btnSwtichOff = new System.Windows.Forms.Button();
            this.btnSwitchPulse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwitchId)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReceived
            // 
            this.lblReceived.AutoSize = true;
            this.lblReceived.Location = new System.Drawing.Point(12, 195);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(107, 13);
            this.lblReceived.TabIndex = 15;
            this.lblReceived.Text = "Received Messages:";
            // 
            // tbReceived
            // 
            this.tbReceived.Location = new System.Drawing.Point(12, 211);
            this.tbReceived.Multiline = true;
            this.tbReceived.Name = "tbReceived";
            this.tbReceived.ReadOnly = true;
            this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbReceived.Size = new System.Drawing.Size(298, 86);
            this.tbReceived.TabIndex = 14;
            // 
            // btnSwitchOn
            // 
            this.btnSwitchOn.Location = new System.Drawing.Point(236, 176);
            this.btnSwitchOn.Name = "btnSwitchOn";
            this.btnSwitchOn.Size = new System.Drawing.Size(34, 23);
            this.btnSwitchOn.TabIndex = 13;
            this.btnSwitchOn.Text = "On";
            this.btnSwitchOn.UseVisualStyleBackColor = true;
            this.btnSwitchOn.Click += new System.EventHandler(this.btnSwitchOn_Click);
            // 
            // lblSend
            // 
            this.lblSend.AutoSize = true;
            this.lblSend.Location = new System.Drawing.Point(12, 68);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(81, 13);
            this.lblSend.TabIndex = 12;
            this.lblSend.Text = "Send Message:";
            // 
            // tbSend
            // 
            this.tbSend.Location = new System.Drawing.Point(12, 84);
            this.tbSend.Multiline = true;
            this.tbSend.Name = "tbSend";
            this.tbSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSend.Size = new System.Drawing.Size(298, 86);
            this.tbSend.TabIndex = 11;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(235, 23);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "Connect";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblPipeName
            // 
            this.lblPipeName.AutoSize = true;
            this.lblPipeName.Location = new System.Drawing.Point(12, 9);
            this.lblPipeName.Name = "lblPipeName";
            this.lblPipeName.Size = new System.Drawing.Size(62, 13);
            this.lblPipeName.TabIndex = 9;
            this.lblPipeName.Text = "Pipe Name:";
            // 
            // tbPipeName
            // 
            this.tbPipeName.Location = new System.Drawing.Point(12, 25);
            this.tbPipeName.Name = "tbPipeName";
            this.tbPipeName.Size = new System.Drawing.Size(217, 20);
            this.tbPipeName.TabIndex = 8;
            this.tbPipeName.Text = "\\\\.\\pipe\\PinballEvents";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(235, 303);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 22);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(235, 52);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 17;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(34, 346);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // numSwitchId
            // 
            this.numSwitchId.Location = new System.Drawing.Point(184, 176);
            this.numSwitchId.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSwitchId.Name = "numSwitchId";
            this.numSwitchId.Size = new System.Drawing.Size(46, 20);
            this.numSwitchId.TabIndex = 19;
            // 
            // btnSwtichOff
            // 
            this.btnSwtichOff.Location = new System.Drawing.Point(276, 176);
            this.btnSwtichOff.Name = "btnSwtichOff";
            this.btnSwtichOff.Size = new System.Drawing.Size(34, 23);
            this.btnSwtichOff.TabIndex = 13;
            this.btnSwtichOff.Text = "Off";
            this.btnSwtichOff.UseVisualStyleBackColor = true;
            this.btnSwtichOff.Click += new System.EventHandler(this.btnSwtichOff_Click);
            // 
            // btnSwitchPulse
            // 
            this.btnSwitchPulse.Location = new System.Drawing.Point(125, 176);
            this.btnSwitchPulse.Name = "btnSwitchPulse";
            this.btnSwitchPulse.Size = new System.Drawing.Size(53, 23);
            this.btnSwitchPulse.TabIndex = 13;
            this.btnSwitchPulse.Text = "Pulse";
            this.btnSwitchPulse.UseVisualStyleBackColor = true;
            this.btnSwitchPulse.Click += new System.EventHandler(this.btnSwitchPulse_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 434);
            this.Controls.Add(this.numSwitchId);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblReceived);
            this.Controls.Add(this.tbReceived);
            this.Controls.Add(this.btnSwtichOff);
            this.Controls.Add(this.btnSwitchPulse);
            this.Controls.Add(this.btnSwitchOn);
            this.Controls.Add(this.lblSend);
            this.Controls.Add(this.tbSend);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblPipeName);
            this.Controls.Add(this.tbPipeName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClientForm";
            this.Text = "PEP Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSwitchId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReceived;
        private System.Windows.Forms.TextBox tbReceived;
        private System.Windows.Forms.Button btnSwitchOn;
        private System.Windows.Forms.Label lblSend;
        private System.Windows.Forms.TextBox tbSend;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblPipeName;
        private System.Windows.Forms.TextBox tbPipeName;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown numSwitchId;
        private System.Windows.Forms.Button btnSwtichOff;
        private System.Windows.Forms.Button btnSwitchPulse;

    }
}

