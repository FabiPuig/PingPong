using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;


namespace PingPong
{
    public partial class Form1 : Form
    {
        private bool addPlayer = false;
        private String imgUrl;
        private Jugador jugador;
        private List<Jugador> players;

        public Form1()
        {
            InitializeComponent();
            getJugadoresFB();
            
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            enableOkCancel();
            disableBtJugador();
            addPlayer = true;

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            disableOkCancel();
            enableBtJugador();
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
                    setJugadorFB();
                    MessageBox.Show("Jugador guardado correctamente");
                    addPlayer = false;
                    disableOkCancel();
                    enableBtJugador();
                }
            }
        }

        private void enableOkCancel()
        {
            btOk.Enabled = true;
            btCancel.Enabled = true;
            tbNombreJ.Enabled = true;
            btImg.Enabled = true;
        }
        private void disableOkCancel()
        {
            btOk.Enabled = false;
            btCancel.Enabled = false;
            tbNombreJ.Enabled = false;
            btImg.Enabled = false;
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

        private async Task setJugadorFB()
        {
            var client = new FirebaseClient("https://pingpong-24930.firebaseio.com/");
            var child = client.Child("jugadors/");

            var p1 = await child.PostAsync(jugador);
            jugador.Id = p1.Key;
        }

        private async Task getJugadoresFB()
        {
            var firebase = new FirebaseClient("https://pingpong-24930.firebaseio.com/");

            var jugadores = await firebase.Child("jugadors").OnceAsync<Jugador>();


            string msg = "";

            players = new List<Jugador>();

            foreach ( var p1 in jugadores)
            {
                Jugador j = p1.Object;
                players.Add(j);
                msg += j.Nombre + "\n" ;
            }
            lvJugador.View = View.List;
            for (int i = 0; i < players.Count; i++)
            {
                lvJugador.Items.Add( players[i].Nombre );
            }
        }

        private void lvJugador_ItemActivate(object sender, EventArgs e)
        {

            int i = lvJugador.SelectedIndices[0];
            Jugador j = players[i];
            tbNombreJ.Text = j.Nombre;
        }
    }
}
