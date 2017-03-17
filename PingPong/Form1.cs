using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPong
{
    public partial class Form1 : Form
    {
        private bool addPlayer = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            enableOkCancel();
            disableBtJugador();
            btImg.Enabled = true;
            tbNombreJ.Enabled = true;
            addPlayer = true;

        }
        private void enableOkCancel()
        {
            btOk.Enabled = true;
            btCancel.Enabled = true;
        }
        private void disableOkCancel()
        {
            btOk.Enabled = false;
            btCancel.Enabled = false;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            disableOkCancel();
            enableBtJugador();
            btImg.Enabled = false;
            tbNombreJ.Enabled = false;
            addPlayer = false;
        }
        private void disableBtJugador()
        {
            btDel.Enabled = false;
            btMod.Enabled = false;
            btAdd.Enabled = false;
        }
        private void enableBtJugador()
        {
            btDel.Enabled = true;
            btMod.Enabled = true;
            btAdd.Enabled = true;
        }
    }
}
