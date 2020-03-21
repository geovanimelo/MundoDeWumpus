using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;


namespace MundoDeWumpus
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private string pathAgent, pathBreeze, pathBreezeAndStench, pathGolden, pathPIT, pathStench, pathWumpus;

        private int posWumpus, posPIT1, posPIT2, posPIT3, posGolden;


        private string GerarPath(string nomeArquivo) 
        {
            string path = Path.Combine(Environment.CurrentDirectory, nomeArquivo);
            return path;
        }


        private void GerandoDesenho(string PathImage, int PointX, int PointY, int op)
        {
            if (PointX >= 45 && PointX <= 249 && PointY >= 16 && PointY <= 220)
            { 
                PictureBox imageAgent = new PictureBox();
                imageAgent.Image = Image.FromFile(PathImage);
                imageAgent.Height = 62;
                imageAgent.Width = 62;
                imageAgent.Location = new Point(PointX, PointY);

                if (op == 1)
                {
                    Controls.Add(imageAgent);
                    Controls.SetChildIndex(imageAgent, 0);
                }
                else { Controls.Add(imageAgent); }
            }
        }
        
        private void GerandoAgent(int PointX, int PointY) 
        {
            GerandoDesenho(pathAgent, PointX, PointY, 1);
        }
        private void GerandoBreeze(int PointX, int PointY)
        {
            GerandoDesenho(pathBreeze, PointX, PointY, 0);
        }

        private void GerandoBreezeAndStench(int PointX, int PointY)
        {
            GerandoDesenho(pathBreezeAndStench, PointX, PointY, 0);
        }

        private void GerandoGolden(int PointX, int PointY)
        {
            GerandoDesenho(pathGolden, PointX, PointY, 0);
        }

        private void GerandoPIT(int PointX, int PointY)
        {
            GerandoDesenho(pathPIT, PointX, PointY, 0);
        }
        private void GerandoStench(int PointX, int PointY)
        {
            GerandoDesenho(pathStench, PointX, PointY, 0);
        }
        private void GerandoWumpus(int PointX, int PointY)
        {
            GerandoDesenho(pathWumpus, PointX, PointY, 0);
        }
        private void MovendoAgent(int NewPointX, int NewPointY) 
        {
            Controls.RemoveAt(0);
            GerandoAgent(NewPointX, NewPointY);
        }

        private string TransformandoNumToPos(int numero)
        {
            int posxx = 45;
            int posyy = 220;
            int cont = 1;
            int num = numero;
            string Pos = "";

            for (int i = 1; i <= 16; i++)
            {
                posxx += 68;
                cont += 1;

                if (i % 4 == 0) 
                {
                    posxx = 45;
                    posyy -= 68;
                }

                if (cont == num) { break; }
            }
            Pos = posxx.ToString() + "," + posyy.ToString();
            return Pos;
        }

            
        private void Form1_Load(object sender, EventArgs e)
        { 
            pathAgent = GerarPath("Agent.png");
            pathWumpus = GerarPath("Wumpus.png");
            pathBreeze = GerarPath("Breeze.png");
            pathBreezeAndStench = GerarPath("BreezeAndStench.png");
            pathGolden = GerarPath("Golden.png");
            pathPIT = GerarPath("PIT.png");
            pathStench = GerarPath("Stench.png");
            
            GerandoAgent(45, 220);


            lblPosX.Text = "45";
            lblPosY.Text = "220";
            lblPosNum.Text = "1";

            Random rnd = new Random();

            List<int> listNumbers = new List<int>();
            do
            {
                int numbers = rnd.Next(2, 12);
                if (!listNumbers.Contains(numbers))
                {
                    listNumbers.Add(numbers);
                }
            } while (listNumbers.Count < 5);

            
            Array.Sort(listNumbers.ToArray());

            posPIT1 = listNumbers[0];
            posPIT2 = listNumbers[1];
            posPIT3 = listNumbers[2];
            posWumpus = listNumbers[3];
            posGolden = rnd.Next(15, 16);


            //MessageBox.Show(posWumpus.ToString() + " " + posPIT1.ToString() + " " + posPIT2.ToString() + " " + posPIT3.ToString() + " " + posGolden.ToString());

            int x = 45;
            int y = 220;

            int[] vectorWumpus = new int[4] { posWumpus + 4, posWumpus - 4, posWumpus + 1, posWumpus - 1 };
            int[] vectorPIT1 = new int[4]  { posPIT1 + 4, posPIT1 - 4, posPIT1 + 1, posPIT1 - 1 };
            int[] vectorPIT2 = new int[4]  { posPIT2 + 4, posPIT2 - 4, posPIT2 + 1, posPIT2 - 1 };
            int[] vectorPIT3 = new int[4]  { posPIT3 + 4, posPIT3 - 4, posPIT3 + 1, posPIT3 - 1 };

            if (posWumpus > 12) { vectorWumpus = vectorWumpus.Where(val => val != (posWumpus + 4)).ToArray(); }
            if (posWumpus < 5) { vectorWumpus = vectorWumpus.Where(val => val != (posWumpus - 4)).ToArray(); }
            if (posWumpus % 4 == 0) { vectorWumpus = vectorWumpus.Where(val => val != (posWumpus + 1)).ToArray(); }
            if (posWumpus % 4 == 1) { vectorWumpus = vectorWumpus.Where(val => val != (posWumpus - 1)).ToArray(); }

            if (posPIT1 > 12) { vectorPIT1 = vectorPIT1.Where(val => val != (posPIT1 + 4)).ToArray(); }
            if (posPIT1 < 5) { vectorPIT1 = vectorPIT1.Where(val => val != (posPIT1 - 4)).ToArray(); }
            if (posPIT1 % 4 == 0) { vectorPIT1 = vectorPIT1.Where(val => val != (posPIT1 + 1)).ToArray(); }
            if (posPIT1 % 4 == 1) { vectorPIT1 = vectorPIT1.Where(val => val != (posPIT1 - 1)).ToArray(); }

            if (posPIT2 > 12) { vectorPIT2 = vectorPIT2.Where(val => val != (posPIT2 + 4)).ToArray(); }
            if (posPIT2 < 5) { vectorPIT2 = vectorPIT2.Where(val => val != (posPIT2 - 4)).ToArray(); }
            if (posPIT2 % 4 == 0) { vectorPIT2 = vectorPIT2.Where(val => val != (posPIT2 + 1)).ToArray(); }
            if (posPIT2 % 4 == 1) { vectorPIT2 = vectorPIT2.Where(val => val != (posPIT2 - 1)).ToArray(); }

            if (posPIT3 > 12) { vectorPIT3 = vectorPIT3.Where(val => val != (posPIT3 + 4)).ToArray(); }
            if (posPIT3 < 5) { vectorPIT3 = vectorPIT3.Where(val => val != (posPIT3 - 4)).ToArray(); }
            if (posPIT3 % 4 == 0) { vectorPIT3 = vectorPIT3.Where(val => val != (posPIT3 + 1)).ToArray(); }
            if (posPIT3 % 4 == 1) { vectorPIT3 = vectorPIT3.Where(val => val != (posPIT3 - 1)).ToArray(); }


            for (int j = 0; j <= 3; j++)
            {
                for (int k = 0; k <= 3; k++)
                {

                    if (posWumpus == 4 * j + k + 1)
                    { GerandoWumpus(x, y); }

                    else if (posPIT1 == 4 * j + k + 1)
                    { GerandoPIT(x, y); }

                    else if (posPIT2 == 4 * j + k + 1)
                    { GerandoPIT(x, y); }

                    else if (posPIT3 == 4 * j + k + 1)
                    { GerandoPIT(x, y); }

                    else if (posGolden == 4 * j + k + 1)
                    { GerandoGolden(x, y); }


                    x += 68;
                }
                x = 45;
                y -= 68;
            }

            int[] vectorBreezeAndStench;
            int[] vectorBreeze;

            vectorBreezeAndStench = vectorWumpus.Intersect(vectorPIT1).ToArray().Union(vectorWumpus.Intersect(vectorPIT2).ToArray()).ToArray().Union(vectorWumpus.Intersect(vectorPIT3).ToArray()).ToArray();

            vectorBreeze = vectorPIT1.Intersect(vectorPIT2).ToArray().Union(vectorPIT1.Intersect(vectorPIT3)).ToArray();


            for (int el1 = 0; el1 < vectorBreezeAndStench.Length; el1++)
            {
                vectorWumpus = vectorWumpus.Where(val => val != vectorBreezeAndStench[el1]).ToArray();
                vectorPIT1 = vectorPIT1.Where(val => val != vectorBreezeAndStench[el1]).ToArray();
                vectorPIT2 = vectorPIT2.Where(val => val != vectorBreezeAndStench[el1]).ToArray();
                vectorPIT3 = vectorPIT3.Where(val => val != vectorBreezeAndStench[el1]).ToArray();
            }

            for (int el2 = 0; el2 < vectorBreeze.Length; el2++)
            {
                vectorPIT1 = vectorPIT1.Where(val => val != vectorBreeze[el2]).ToArray();
                vectorPIT2 = vectorPIT2.Where(val => val != vectorBreeze[el2]).ToArray();
                vectorPIT3 = vectorPIT3.Where(val => val != vectorBreeze[el2]).ToArray();
            }


            for(int res1 = 0; res1 < vectorWumpus.Length; res1++) 
            {

                int posx = int.Parse(TransformandoNumToPos(vectorWumpus[res1]).Split(',')[0]);
                int posy = int.Parse(TransformandoNumToPos(vectorWumpus[res1]).Split(',')[1]);
                
                GerandoStench(posx, posy);

            }

            for (int res2 = 0; res2 < vectorPIT1.Length; res2++)
            {

                int posx = int.Parse(TransformandoNumToPos(vectorPIT1[res2]).Split(',')[0]);
                int posy = int.Parse(TransformandoNumToPos(vectorPIT1[res2]).Split(',')[1]);

                GerandoBreeze(posx, posy);

            }

            for (int res3 = 0; res3 < vectorPIT2.Length; res3++)
            {

                int posx = int.Parse(TransformandoNumToPos(vectorPIT2[res3]).Split(',')[0]);
                int posy = int.Parse(TransformandoNumToPos(vectorPIT2[res3]).Split(',')[1]);

                GerandoBreeze(posx, posy);

            }

            for (int res4 = 0; res4 < vectorPIT3.Length; res4++)
            {

                int posx = int.Parse(TransformandoNumToPos(vectorPIT3[res4]).Split(',')[0]);
                int posy = int.Parse(TransformandoNumToPos(vectorPIT3[res4]).Split(',')[1]);

                GerandoBreeze(posx, posy);

            }


            for (int res5 = 0; res5 < vectorBreeze.Length; res5++)
            {

                int posx = int.Parse(TransformandoNumToPos(vectorBreeze[res5]).Split(',')[0]);
                int posy = int.Parse(TransformandoNumToPos(vectorBreeze[res5]).Split(',')[1]);

                GerandoBreeze(posx, posy);

            }

            for (int res6 = 0; res6 < vectorBreezeAndStench.Length; res6++)
            {

                int posx = int.Parse(TransformandoNumToPos(vectorBreezeAndStench[res6]).Split(',')[0]);
                int posy = int.Parse(TransformandoNumToPos(vectorBreezeAndStench[res6]).Split(',')[1]);

                GerandoBreezeAndStench(posx, posy);

            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            string pathAgent = GerarPath("Agent.png");

            if (e.KeyCode == Keys.Right)
            {
                if (int.Parse(lblPosX.Text) >= 45 && int.Parse(lblPosX.Text) < 249)
                {

                    lblPosX.Text = (int.Parse(lblPosX.Text) + 68).ToString();
                    lblPosNum.Text = (int.Parse(lblPosNum.Text) + 1).ToString();

                    MovendoAgent(int.Parse(lblPosX.Text), int.Parse(lblPosY.Text));
                }
            }

            if (e.KeyCode == Keys.Left)
            {
                if (int.Parse(lblPosX.Text) > 45 && int.Parse(lblPosX.Text) <= 249)
                {
                    lblPosX.Text = (int.Parse(lblPosX.Text) - 68).ToString();
                    lblPosNum.Text = (int.Parse(lblPosNum.Text) - 1).ToString();

                    MovendoAgent(int.Parse(lblPosX.Text), int.Parse(lblPosY.Text));
                }
            }

            else if (e.KeyCode == Keys.Up)
            {
                if (int.Parse(lblPosY.Text) > 16 && int.Parse(lblPosY.Text) <= 220)
                {

                    lblPosY.Text = (int.Parse(lblPosY.Text) - 68).ToString();
                    lblPosNum.Text = (int.Parse(lblPosNum.Text) + 4).ToString();

                    MovendoAgent(int.Parse(lblPosX.Text), int.Parse(lblPosY.Text));
                    
                }
            }

            else if (e.KeyCode == Keys.Down)
            {
                if (int.Parse(lblPosY.Text) >= 16 && int.Parse(lblPosY.Text) < 220)
                {

                    lblPosY.Text = (int.Parse(lblPosY.Text) + 68).ToString();
                    lblPosNum.Text = (int.Parse(lblPosNum.Text) - 4).ToString();

                    MovendoAgent(int.Parse(lblPosX.Text), int.Parse(lblPosY.Text));
                }
            }

        }
    }
}
