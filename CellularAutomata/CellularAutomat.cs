using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{

    class CellularAutomat
    {
        private int[,] tab;
        private int sizeX;
        private int sizeY;

        private System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(Color.Blue);

        public CellularAutomat(int sizeX, int sizeY)
        {
            tab = new int[sizeY, sizeX];

            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    tab[i, j] = 0;
                }
            }

            tab[0, sizeX / 2] = 1;

            this.sizeX = sizeX;
            this.sizeY = sizeY;
        }

        public int[,] Tab { get => tab; set => tab = value; }
        public int SizeX { get => sizeX; set => sizeX = value; }
        public int SizeY { get => sizeY; set => sizeY = value; }

        public void Simulate(int[] rule, int indexY)
        {

         

            int[,] tabTmp = new int[2, sizeX];


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    tabTmp[i, j] = tab[indexY + i, j];
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {

                    int s_l = -200;
                    int s_p = -200;

                    int komorka = tab[indexY + i, j];
                    int wartosc = 0;

                    if (j - 1 < 0)
                    {
                        s_l = tab[indexY + i, sizeX - 1];
                    }
                    else
                    {
                        s_l = tab[indexY + i, j - 1];
                    }

                    if (j + 1 >= sizeX)
                    {
                        s_p = tab[indexY + i, 0];
                    }
                    else
                    {
                        s_p = tab[indexY + i, j + 1];
                    }


                    if (s_l == 0 & s_p == 0 && komorka == 0)
                    {
                        wartosc = rule[0];
                    }
                    else if (s_l == 0 && s_p == 1 && komorka == 0)
                    {
                        wartosc = rule[1];
                    }
                    else if (s_l == 0 && s_p == 0 && komorka == 1)
                    {
                        wartosc = rule[2];
                    }
                    else if (s_l == 0 && s_p == 1 && komorka == 1)
                    {
                        wartosc = rule[3];
                    }
                    else if (s_l == 1 && s_p == 0 && komorka == 0)
                    {
                        wartosc = rule[4];
                    }
                    else if (s_l == 1 && s_p == 1 && komorka == 0)
                    {
                        wartosc = rule[5];
                    }
                    else if (s_l == 1 && s_p == 0 && komorka == 1)
                    {
                        wartosc = rule[6];
                    }
                    else if (s_l == 1 & s_p == 1 && komorka == 1)
                    {
                        wartosc = rule[7];
                    }

                    if (i + 1 < 2)
                    {
                        tabTmp[i + 1, j] = wartosc;
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    tab[indexY + i, j] = tabTmp[i, j];
                }
            }

        }

        public void Display(Graphics g, int cellSize, int indexY)
        {
            int sizeX = this.tab.GetLength(1);

            for (int j = 0; j < sizeX; j++)
            {
                if (tab[indexY, j] == 1)
                {
                    g.FillRectangle(brush, j * cellSize, indexY * cellSize, cellSize, cellSize);
                }


            }

        }

        public bool ResetY(int i)
        {
            if (i == this.sizeY - 1)
            {
                // pictureBox1.Image = null;

                // indexY = 0;

                int[,] tabTmp = new int[this.sizeY, this.sizeX];


                for (int k = 0; k < this.sizeY; k++)
                {
                    for (int j = 0; j < this.sizeX; j++)
                    {
                        tabTmp[k, j] = this.tab[k, j];
                    }
                }

                for (int k = 0; k < this.sizeY; k++)
                {
                    for (int j = 0; j < this.sizeX; j++)
                    {
                        if (k == 0)
                        {
                            tabTmp[k, j] = this.tab[this.sizeY - 1, j];
                        }
                        else
                        {
                            tabTmp[k, j] = 0;
                        }
                    }
                }
                for (int k = 0; k < this.sizeY; k++)
                {
                    for (int j = 0; j < this.sizeX; j++)
                    {
                        this.tab[k, j] = tabTmp[k, j];
                    }
                }

                return true;
            }

            return false;

        }

    }

}
