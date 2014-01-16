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
    public partial class PopupDialog : Form
    {

        public event EventHandler PopupAnnullaEvt;

        public PopupDialog()
        {
            InitializeComponent();
        }

        private void bAnnulla_Click(object sender, EventArgs e)
        {
            PopupAnnullaEvt.Invoke(sender, e);
        }

        public void setText(string s) {
            lTesto.Text = s;
        }

        public void setButtonText(string s) {
            bAnnulla.Text = s;
        }
    }
}
