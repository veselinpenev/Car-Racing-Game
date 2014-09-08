using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRacing
{
    public partial class Form1 : Form
    {
        static int row = 16;
        static int col = 9;

        public int startX = 50;
        public int startY = 50;

        public int size = 15;
        public int[,] matrix = new int[row, col];
        
        public int carX=0;
        public int carY=0;
        private Random rand = new Random();

        public int myCarY = 0;

        public int score=0;
        public int level = 1;
        public int countLevel = 1;
        
        public Form1()
        {
            InitializeComponent();
            ResetMatrix();
            DrawCar(0, 0, 1);
            DrawCar(12, myCarY, 2);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics draw=e.Graphics;
            //DrawBoard
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    draw.DrawRectangle(new Pen(Brushes.Black), (startY + c * size), (startX + r * size), size, size);
                    if (matrix[r, c] == 1)
                    {
                        draw.FillRectangle(Brushes.DarkBlue, (startY + c * size), (startX + r * size), size, size);
                    }
                    if (matrix[r, c] == 2)
                    {
                        draw.FillRectangle(Brushes.DarkRed, (startY + c * size), (startX + r * size), size, size);
                    }
                }
            }

        }
        public void ResetMatrix()
        {
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    matrix[r, c] = 0;
                }
            }
        }
        public void DrawCar(int x,int y, int value)
        {
            ValidPoint(x, y+1, value);
            ValidPoint(x+1, y + 1, value);
            ValidPoint(x+2, y + 1, value);
            ValidPoint(x+3, y + 1, value);
            ValidPoint(x+1, y, value);
            ValidPoint(x+1, y + 2, value);
            ValidPoint(x+3, y, value);
            ValidPoint(x+3, y+2, value);
        }
        public void ValidPoint(int x, int y, int value)
        {
            if (x < row && x >= 0 && y < col && y >= 0)
            {
                matrix[x, y] = value;
            }
        }

        private void timerRacing_Tick(object sender, EventArgs e)
        {
            ResetMatrix();
            DrawCar(12, myCarY, 2);
            DrawCar(carX, carY, 1);
            Invalidate();

            carX++;
            if (carX > row)
            {
                carX = 0;
                if (rand.Next() % 3 == 0)
                {
                    carY = 0;
                }
                else if (rand.Next() % 3 == 1)
                {
                    carY = 3;
                }
                else
                {
                    carY = 6;
                }
                score += 10 * level;
                countLevel++;
                if (countLevel == 5)
                {
                    level++;
                    timerRacing.Interval = timerRacing.Interval - 10;
                    countLevel = 1;
                }
            }
            Check();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && myCarY>0)
            {
                myCarY -= 3;
            }
            else if (e.KeyCode == Keys.Right && myCarY<col-3)
            {
                myCarY += 3;
            }
        }

        private void Check()
        {
            if (carX + 3 > 12 && carY == myCarY)
            {
                timerRacing.Enabled = false;
                var again=MessageBox.Show("Reached " + level + " level" +Environment.NewLine +"Your score is " + score + Environment.NewLine +"Try Again?", "Score", MessageBoxButtons.YesNo);
                if (again == DialogResult.Yes)
                {
                    carX = 0;
                    carY = 0;
                    myCarY = 0;
                    score = 0;
                    level = 1;
                    timerRacing.Interval = 100;
                    timerRacing.Enabled = true;
                }
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            carX = 0;
            carY = 0;
            myCarY = 0;
            timerRacing.Enabled = true;
        }

        private void Form1_KeyDown(object sender, KeyPressEventArgs e)
        {

        }
    }
}
