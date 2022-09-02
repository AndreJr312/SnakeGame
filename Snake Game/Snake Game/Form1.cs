using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game {
    public partial class Form1 : Form {

        Game Game;

        public Form1() { 
            InitializeComponent();
            Game = new Game(ref frame, ref lblPontos, ref pnTela);
        }

        private void sairToolStripMenuItem_Click_1(object sender, EventArgs e){
            Application.Exit();
        }

        private void iniciarJogoToolStripMenuItem_Click(object sender, EventArgs e) {
            Game.StartGame();
        }

        private void frame_Tick(object sender, EventArgs e) {
            Game.Tick();
        }

        private void Clicado(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) {
                Game.Arrow = e.KeyCode;
            }
        }
    }
}
