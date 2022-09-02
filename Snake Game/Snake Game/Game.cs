using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game {
    class Game {

        public Keys Direction { get; set; }

        public Keys Arrow { get; set; }


        private Timer Frame { get; set; }

        private Label lblPontos { get; set; }

        private Panel pnTela { get; set; }

        private int pontos = 0;

        private Food Food;
        private Snake Snake;


        private Bitmap offScreenBitmap; /* criando desenhos e pixels na tela */
        private Graphics bitmapGraph;
        private Graphics screenGraph;


        public Game(ref Timer timer, ref Label label, ref Panel panel) {   /*recebendo valores e criando tela*/
            pnTela = panel;
            Frame = timer;
            lblPontos = label;
            offScreenBitmap = new Bitmap(428, 428);
            Snake = new Snake();
            Food = new Food();
            Direction = Keys.Left;
            Arrow = Direction;
        }

        public void StartGame() {
            Snake.Reset();
            Food.CreateFood();
            Direction = Keys.Left;
            bitmapGraph = Graphics.FromImage(offScreenBitmap);
            screenGraph = pnTela.CreateGraphics();
            Frame.Enabled = true;
        }

        public void Tick() {   /* validaçoes de direção da cobrinha , se estiver indo pra um ponto nao pode voltar na direção oposta*/

            if (((Arrow == Keys.Left) && (Direction != Keys.Right)) ||
            ((Arrow == Keys.Right) && (Direction != Keys.Left)) ||
            ((Arrow == Keys.Up) && (Direction != Keys.Down)) ||
            ((Arrow == Keys.Down) && (Direction != Keys.Up))) {
                Direction = Arrow;
            }


            switch (Direction) {    /*direcionando a cobrinha*/
                case Keys.Left:
                    Snake.Left();
                    break;
                case Keys.Right:
                    Snake.Rigth();
                    break;
                case Keys.Up:
                    Snake.Up();
                    break;
                case Keys.Down:
                    Snake.Down();
                    break;
            }

            bitmapGraph.Clear(Color.White);

            /*        imagem maçã                     posição x               pos. Y          tamanho */
            bitmapGraph.DrawImage(Properties.Resources.apple_icon, (Food.Location.X * 15), (Food.Location.Y * 15), 15, 15);
            bool gameOver = false;

            for (int i = 0; i < Snake.Lengt; i++) {  /* criando e colocando cor da cobrinha*/

                if (i == 0) {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#191970")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);
                }
                else {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#0000CD")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);
                }

                if ((Snake.Location[i] == Snake.Location[0]) && (i > 0)) {  /*verificando se ela colide com ela mesma*/
                    gameOver = true;
                }
            }


            screenGraph.DrawImage(offScreenBitmap, 0, 0);
            CheckCollision();
            if (gameOver) {
                GameOver();
            }
        }

        public void CheckCollision() {
            if(Snake.Location[0] == Food.Location) {  /*verificar se comeu a fruta*/
                Snake.Eat();
                Food.CreateFood();
                pontos ++;
                lblPontos.Text = "PONTOS: " + pontos;
            }
        }

        public void GameOver() {
            Frame.Enabled = false;
            bitmapGraph.Dispose();  /*limpando dados da tela*/
            screenGraph.Dispose();
            lblPontos.Text = "PONTOS: 0";
            MessageBox.Show("GAME OVER");
        }


    }

}
