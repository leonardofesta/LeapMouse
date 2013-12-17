namespace GUI
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lBottomRight = new System.Windows.Forms.Label();
            this.lTopLeft = new System.Windows.Forms.Label();
            this.bHide = new System.Windows.Forms.Button();
            this.bStartStop = new System.Windows.Forms.Button();
            this.bCalibrate = new System.Windows.Forms.Button();
            this.LStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bExit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.bExit);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.bHide);
            this.panel1.Controls.Add(this.bStartStop);
            this.panel1.Controls.Add(this.bCalibrate);
            this.panel1.Controls.Add(this.LStatus);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 261);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightBlue;
            this.panel2.Controls.Add(this.lBottomRight);
            this.panel2.Controls.Add(this.lTopLeft);
            this.panel2.Location = new System.Drawing.Point(11, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(261, 116);
            this.panel2.TabIndex = 5;
            // 
            // lBottomRight
            // 
            this.lBottomRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lBottomRight.AutoSize = true;
            this.lBottomRight.Location = new System.Drawing.Point(200, 91);
            this.lBottomRight.Name = "lBottomRight";
            this.lBottomRight.Size = new System.Drawing.Size(58, 13);
            this.lBottomRight.TabIndex = 1;
            this.lBottomRight.Text = "( 400 ; 25 )";
            this.lBottomRight.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lTopLeft
            // 
            this.lTopLeft.AutoSize = true;
            this.lTopLeft.Location = new System.Drawing.Point(3, 15);
            this.lTopLeft.Name = "lTopLeft";
            this.lTopLeft.Size = new System.Drawing.Size(67, 13);
            this.lTopLeft.TabIndex = 0;
            this.lTopLeft.Text = "( -400 ; 800 )";
            // 
            // bHide
            // 
            this.bHide.Location = new System.Drawing.Point(11, 217);
            this.bHide.Name = "bHide";
            this.bHide.Size = new System.Drawing.Size(127, 30);
            this.bHide.TabIndex = 4;
            this.bHide.Text = "Nascondi";
            this.bHide.UseVisualStyleBackColor = true;
            this.bHide.Click += new System.EventHandler(this.bHide_Click);
            // 
            // bStartStop
            // 
            this.bStartStop.Location = new System.Drawing.Point(145, 181);
            this.bStartStop.Name = "bStartStop";
            this.bStartStop.Size = new System.Drawing.Size(127, 30);
            this.bStartStop.TabIndex = 3;
            this.bStartStop.Text = "Avvia";
            this.bStartStop.UseVisualStyleBackColor = true;
            this.bStartStop.Click += new System.EventHandler(this.bStartStop_Click);
            // 
            // bCalibrate
            // 
            this.bCalibrate.Location = new System.Drawing.Point(12, 181);
            this.bCalibrate.Name = "bCalibrate";
            this.bCalibrate.Size = new System.Drawing.Size(127, 30);
            this.bCalibrate.TabIndex = 2;
            this.bCalibrate.Text = "Calibrazione";
            this.bCalibrate.UseVisualStyleBackColor = true;
            this.bCalibrate.Click += new System.EventHandler(this.bCalibrate_Click);
            // 
            // LStatus
            // 
            this.LStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LStatus.AutoSize = true;
            this.LStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LStatus.Font = new System.Drawing.Font("Arial", 12F);
            this.LStatus.ForeColor = System.Drawing.Color.Black;
            this.LStatus.Location = new System.Drawing.Point(114, 22);
            this.LStatus.Name = "LStatus";
            this.LStatus.Size = new System.Drawing.Size(140, 18);
            this.LStatus.TabIndex = 1;
            this.LStatus.Text = "Connessione Leap";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(24, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 22);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // bExit
            // 
            this.bExit.Location = new System.Drawing.Point(145, 217);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(127, 30);
            this.bExit.TabIndex = 6;
            this.bExit.Text = "Esci";
            this.bExit.UseVisualStyleBackColor = true;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "LeapMouse";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bStartStop;
        private System.Windows.Forms.Button bCalibrate;
        private System.Windows.Forms.Label LStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button bHide;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lBottomRight;
        private System.Windows.Forms.Label lTopLeft;
        private System.Windows.Forms.Button bExit;

    }
}

