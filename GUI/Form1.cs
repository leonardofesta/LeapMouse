using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeapMouse.GUI
{

    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        public const int GREEN = 0;
        public const int YELLOW = 1;
        public const int RED = 2;

        public const int BUTTONSTART = 0;
        public const int BUTTONSTOP = 1;

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

        public void setDesktopMargin(int x1, int y1, int x2, int y2) {

            string top = "" + x1;
            string left = "" + y1;
            string bottom = "" + x2;
            string right = "" + y2;
  
            this.lLeft.Text = left;
            this.lRight.Text = right;
            this.lTop.Text = top;
            this.lBottom.Text = bottom;
            
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
                    bStartStop.Text = "Avvia";
                    break;
                case BUTTONSTOP :
                    bStartStop.Text = "Ferma";
                    break;
                default : 
                    throw new IndexOutOfRangeException("Stringa non prevista");
            }

        }

    }
}
