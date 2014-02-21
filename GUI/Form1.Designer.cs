namespace LeapMouse.GUI
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
            this.bExit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lBottom = new System.Windows.Forms.Label();
            this.lTop = new System.Windows.Forms.Label();
            this.lRight = new System.Windows.Forms.Label();
            this.lLeft = new System.Windows.Forms.Label();
            this.bHide = new System.Windows.Forms.Button();
            this.bStartStop = new System.Windows.Forms.Button();
            this.bCalibrate = new System.Windows.Forms.Button();
            this.LStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lBottom);
            this.panel2.Controls.Add(this.lTop);
            this.panel2.Controls.Add(this.lRight);
            this.panel2.Controls.Add(this.lLeft);
            this.panel2.Location = new System.Drawing.Point(11, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(261, 116);
            this.panel2.TabIndex = 5;
            // 
            // lBottom
            // 
            this.lBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lBottom.AutoSize = true;
            this.lBottom.Location = new System.Drawing.Point(113, 89);
            this.lBottom.Name = "lBottom";
            this.lBottom.Size = new System.Drawing.Size(19, 13);
            this.lBottom.TabIndex = 3;
            this.lBottom.Text = "25";
            this.lBottom.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lTop
            // 
            this.lTop.AutoSize = true;
            this.lTop.Location = new System.Drawing.Point(115, 9);
            this.lTop.Name = "lTop";
            this.lTop.Size = new System.Drawing.Size(25, 13);
            this.lTop.TabIndex = 2;
            this.lTop.Text = "800";
            // 
            // lRight
            // 
            this.lRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lRight.AutoSize = true;
            this.lRight.Location = new System.Drawing.Point(231, 50);
            this.lRight.Name = "lRight";
            this.lRight.Size = new System.Drawing.Size(25, 13);
            this.lRight.TabIndex = 1;
            this.lRight.Text = "400";
            this.lRight.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lLeft
            // 
            this.lLeft.AutoSize = true;
            this.lLeft.Location = new System.Drawing.Point(3, 52);
            this.lLeft.Name = "lLeft";
            this.lLeft.Size = new System.Drawing.Size(31, 13);
            this.lLeft.TabIndex = 0;
            this.lLeft.Text = "-400 ";
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
            this.pictureBox1.Image = global::LeapMouse.GUI.Properties.Resources.green;
            this.pictureBox1.InitialImage = global::LeapMouse.GUI.Properties.Resources.green;
            this.pictureBox1.Location = new System.Drawing.Point(24, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
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
        private System.Windows.Forms.Label lRight;
        private System.Windows.Forms.Label lLeft;
        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.Label lBottom;
        private System.Windows.Forms.Label lTop;

    }
}

