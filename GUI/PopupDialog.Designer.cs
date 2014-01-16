namespace LeapMouse.GUI
{
    partial class PopupDialog
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
            this.lTesto = new System.Windows.Forms.Label();
            this.bAnnulla = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Snow;
            this.panel1.Controls.Add(this.lTesto);
            this.panel1.Controls.Add(this.bAnnulla);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 126);
            this.panel1.TabIndex = 0;
            // 
            // lTesto
            // 
            this.lTesto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lTesto.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTesto.ForeColor = System.Drawing.Color.Teal;
            this.lTesto.Location = new System.Drawing.Point(13, 11);
            this.lTesto.Name = "lTesto";
            this.lTesto.Size = new System.Drawing.Size(424, 67);
            this.lTesto.TabIndex = 1;
            this.lTesto.Text = "Blablablablablabla";
            this.lTesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bAnnulla
            // 
            this.bAnnulla.Location = new System.Drawing.Point(162, 81);
            this.bAnnulla.Name = "bAnnulla";
            this.bAnnulla.Size = new System.Drawing.Size(140, 31);
            this.bAnnulla.TabIndex = 0;
            this.bAnnulla.Text = "Annulla";
            this.bAnnulla.UseVisualStyleBackColor = true;
            this.bAnnulla.Click += new System.EventHandler(this.bAnnulla_Click);
            // 
            // PopupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 124);
            this.Controls.Add(this.panel1);
            this.Name = "PopupDialog";
            this.Text = "PopupDialog";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lTesto;
        private System.Windows.Forms.Button bAnnulla;
    }
}