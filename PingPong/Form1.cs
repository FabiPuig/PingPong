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
        private String imgUrl;
        private Jugador jugador;
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

        private void btCancel_Click(object sender, EventArgs e)
        {
            disableOkCancel();
            enableBtJugador();
            btImg.Enabled = false;
            tbNombreJ.Enabled = false;
            addPlayer = false;
        }

        private void btImg_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imgUrl = openFileDialog1.FileName;
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (addPlayer)
            {
                if (string.IsNullOrEmpty(tbNombreJ.Text))
                {
                    MessageBox.Show("Debes introducir un nombre");
                }
                else if (string.IsNullOrEmpty(imgUrl))
                {
                    MessageBox.Show("Debes introducir una imagen");
                }
                else
                {
                    jugador = new Jugador(tbNombreJ.Text, imgUrl);
                    MessageBox.Show(jugador.Nombre + " - " + jugador.Image);
                    addPlayer = false;
                    disableOkCancel();
                    enableBtJugador();
                    btImg.Enabled = false;
                }
            }
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
