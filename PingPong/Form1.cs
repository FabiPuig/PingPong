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
        private bool modPlayer = false;
        private String imgUrl;
        private Jugador jugador;
        private List<Jugador> players;
        private int index;

        public Form1()
        {
            InitializeComponent();
            getJugadoresFB();
            
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            enableOkCancel();
            disableModDel();
            addPlayer = true;
            tbNombreJ.ReadOnly = false;
            btAdd.Enabled = false;
            clearPlayer();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            disableOkCancel();
            btAdd.Enabled = true;
            disableModDel();
            tbNombreJ.Enabled = false;
            modPlayer = false;
            addPlayer = false;
        }

        private void btImg_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imgUrl = openFileDialog1.FileName;
                pictureBox1.Load(openFileDialog1.FileName);

                if ( modPlayer )
                {
                    players[index].Image = imgUrl;
                }

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
                    players.Add(jugador);
                    refreshLv();
                    setJugadorFB();
                    MessageBox.Show("Jugador guardado correctamente");
                    addPlayer = false;
                    disableOkCancel();
                    clearPlayer();
                    btAdd.Enabled = true;
                }
            }else if ( modPlayer )
            {
                players[index].Nombre = tbNombreJ.Text;
                refreshLv();
                modPlayer = false;
                disableOkCancel();
                btAdd.Enabled = true;
                updateJugadorFB();

            }
        }

        private void enableOkCancel()
        {
            btOk.Enabled = true;
            btCancel.Enabled = true;
            btImg.Enabled = true;
        }
        private void disableOkCancel()
        {
            btOk.Enabled = false;
            btCancel.Enabled = false;
            tbNombreJ.ReadOnly = true;
            btImg.Enabled = false;
        }

        private void disableModDel()
        {
            btDel.Enabled = false;
            btMod.Enabled = false;
        }
        private void enableModDel()
        {
            btDel.Enabled = true;
            btMod.Enabled = true;
        }

        private void clearPlayer()
        {
            tbNombreJ.Text = "";
            pictureBox1.Image = null;
        }

        private async Task setJugadorFB()
        {
            var client = new FirebaseClient("https://pingpong-24930.firebaseio.com/");
            var child = client.Child("jugadors/");

            var p1 = await child.PostAsync(jugador);

        }

        private async Task getJugadoresFB()
        {
            var firebase = new FirebaseClient("https://pingpong-24930.firebaseio.com/");

            var jugadores = await firebase.Child("jugadors").OnceAsync<Jugador>();

            players = new List<Jugador>();

            foreach ( var p1 in jugadores)
            {
                Jugador j = p1.Object;
                j.Id = p1.Key;
                players.Add(j);
            }

            resetLv();

            refreshLv();
        }

        private async Task delJugadorFB()
        {
            var client = new FirebaseClient("https://pingpong-24930.firebaseio.com/");
            var child = client.Child("jugadors/" + players[ index ].Id) ;

            await child.DeleteAsync();

            players.RemoveAt( index );

            MessageBox.Show("Borrado correctamente");

        }

        private async Task updateJugadorFB()
        {
            
            var client = new FirebaseClient("https://pingpong-24930.firebaseio.com/");
            var child = client.Child("jugadors/" + players[ index ].Id);

            await child.PutAsync( players[ index ] );

            MessageBox.Show("ok");
            
        }

        private void lvJugador_ItemActivate(object sender, EventArgs e)
        {
            index = lvJugador.SelectedIndices[0];
            Jugador j = players[ index ];
            tbNombreJ.Text = j.Nombre;
            pictureBox1.Load( j.Image );
            btMod.Enabled = true;
            btDel.Enabled = true;
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            delJugadorFB();
            tbNombreJ.Text = "";
            pictureBox1.Image = null;
            players.RemoveAt(index);
            refreshLv();
        }

        private void btMod_Click(object sender, EventArgs e)
        {
            modPlayer = true;
            enableOkCancel();
            tbNombreJ.Enabled = true;
            tbNombreJ.ReadOnly = false;
            btImg.Enabled = true;
        }

        //Refresh del listview( usar cuando se modifique la lista de jugadores)
        private void refreshLv()
        {
            resetLv();
            for (int i = 0; i < players.Count; i++)
            {
                lvJugador.Items.Add(players[i].Nombre);
            }
        }

        //Reset del listview para que no añada la nueva informacion a la que habia
        private void resetLv()
        {
            this.lvJugador.Items.Clear();
            this.lvJugador.Update();
            this.lvJugador.Refresh();
            this.lvJugador.View = View.List;
        }
    }
}
