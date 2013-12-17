﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{

    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }
        public static const int GREEN = 0;
        public static const int YELLOW = 1;
        public static const int RED = 2;

        public static const int BUTTONSTART = 0;
        public static const int BUTTONSTOP  = 1;

        public event EventHandler CalibrationClickEvt;
        public event EventHandler StartStopClickEvt;
        public event EventHandler HideClickEvt;
        public event EventHandler ExitClickEvt;

        private void bCalibrate_Click(object sender, EventArgs e)
        {
            CalibrationClickEvt.Invoke(sender, e); 
        }

        private void bHide_Click(object sender, EventArgs e)
        {
            HideClickEvt.Invoke(sender, e);
        }

        private void bStartStop_Click(object sender, EventArgs e)
        {
            StartStopClickEvt.Invoke(sender, e);
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            ExitClickEvt.Invoke(sender, e);
        }

        public void setDesktopMargin(float x1, float y1, float x2, float y2) {

            string l1 = " ( " + x1 + " ; " + y1 + " ) ";
            string l2 = " ( " + x2 + " ; " + y2 + " ) ";

            this.lTopLeft.Text = l1;
            this.lBottomRight.Text = l2;
            
        }

        public void switchColor(int color) {
            switch (color)
            {
                case RED:
                        pictureBox1.Image = Properties.Resources.red;
                        break;
                case YELLOW:
                        pictureBox1.Image = Properties.Resources.yellow;
                        break;
                case GREEN:
                        pictureBox1.Image = Properties.Resources.red;
                        break;
                default:
                        throw new IndexOutOfRangeException("Colore non previsto");
            }
        }


        public void changeStartStopButton(int button) {
            switch (button) { 
                case BUTTONSTART :
                    bHide.Text = "Avvia";
                    break;
                case BUTTONSTOP :
                    bHide.Text = "Ferma";
                    break;
                default : 
                    throw new IndexOutOfRangeException("Stringa non prevista");
            }

        }

    }
}