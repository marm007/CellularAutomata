using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CellularAutomata
{
    public partial class Form1 : Form
    {
         int CELL_SIZE = 3;

        int SLEEP_TIME = 10;

        int speed = 0;

        bool flagStop = false;

        int offsetX = 0;

        int indexY = 0;

        int iterationNumber = 0;

        int pictureBoxWidth;

        int pictureBoxHeight;

        int numberOfIterations;

        CellularAutomat cellularAutomat = null;

        Rule rule = null;

        private BackgroundWorker automatyPetla = null;

        private System.Drawing.SolidBrush clearBrush = new System.Drawing.SolidBrush(SystemColors.Control);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       

        private void clearButton_Click(object sender, EventArgs e)
        {
            indexY = 0;

            pictureBox1.CreateGraphics().FillRectangle(clearBrush, 0, 0, pictureBoxWidth + 1, pictureBoxHeight + 1);

            widthTextBox.Text = null;
            heightTextBox.Text = null;
            // numberOfIterationsTextBox.Text = null;
            // ruleTextBox.Text = null;

            cellSizeTrackBar.Enabled = true;

            flagStop = false;
            startButton.Enabled = true;
            stopButton.Enabled = false;
            clearButton.Enabled = false;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            flagStop = true;
            automatyPetla.CancelAsync();
            startButton.Enabled = true;
            stopButton.Enabled = false;
            clearButton.Enabled = true;
        }

        private void startButton_Click_1(object sender, EventArgs e)

        {

            automatyPetla = new BackgroundWorker();
            automatyPetla.WorkerSupportsCancellation = true;


            if (flagStop)
            {
                flagStop = false;
            }
            else
            {
                cellSizeTrackBar.Enabled = false;

                pictureBox1.Refresh();

                pictureBoxWidth = this.pictureBox1.Width;
                pictureBoxHeight = this.pictureBox1.Height;


                int maxSizeX = pictureBoxWidth / CELL_SIZE;
                int maxSizeY = pictureBoxHeight / CELL_SIZE;

                int sizeX;
                int sizeY;

                bool success = Int32.TryParse(this.widthTextBox.Text, out sizeX);
                if (!success)
                {
                    sizeX = maxSizeX;
                    widthTextBox.Text = sizeX.ToString();
                }

                if (sizeX > maxSizeX || sizeX <= 0)
                {
                    sizeX = maxSizeX;
                    widthTextBox.Text = sizeX.ToString();

                }

                success = Int32.TryParse(this.heightTextBox.Text, out sizeY);
                if (!success)
                {
                    sizeY = maxSizeY;
                    heightTextBox.Text = sizeY.ToString();

                }

                if (sizeY > maxSizeY || sizeY <= 0)
                {
                    sizeY = maxSizeY;
                    heightTextBox.Text = sizeY.ToString();

                }


                success = Int32.TryParse(this.numberOfIterationsTextBox.Text, out numberOfIterations);

                if (!success)
                {
                    numberOfIterations = 1000;
                    numberOfIterationsTextBox.Text = numberOfIterations.ToString();
                }

                if (numberOfIterations <= 0)
                {
                    numberOfIterations = 1000;
                    numberOfIterationsTextBox.Text = numberOfIterations.ToString();
                }


                int ruleFromText;
                success = Int32.TryParse(this.ruleTextBox.Text, out ruleFromText);

                if (!success)
                {
                    ruleFromText = 30;
                    ruleTextBox.Text = ruleFromText.ToString();

                }

                iterationNumber = 0;

                offsetX = (pictureBoxWidth - sizeX) / 2;

                rule = new Rule(ruleFromText);

                cellularAutomat = new CellularAutomat(sizeX, sizeY);


            }

            automatyPetla.DoWork += new DoWorkEventHandler((state, args) =>
            {

                for (; iterationNumber < numberOfIterations; iterationNumber++)
                {

                    if (automatyPetla.CancellationPending)
                    {
                        break;
                    }

                    cellularAutomat.Simulate(rule.Tab, indexY);
                    cellularAutomat.Display(pictureBox1.CreateGraphics(), CELL_SIZE, indexY);

                    indexY++;

                    int sizeX = cellularAutomat.SizeX;
                    int sizeY = cellularAutomat.SizeY;

                    if (cellularAutomat.ResetY(indexY))
                    {
                        pictureBox1.CreateGraphics().FillRectangle(clearBrush, 0, 0, pictureBoxWidth  + 1, pictureBoxHeight + 1);
                        indexY = 0;

                    }

                    Thread.Sleep(speed * SLEEP_TIME);

                }

            });

            automatyPetla.RunWorkerAsync();
            startButton.Enabled = false;
            stopButton.Enabled = true;
            clearButton.Enabled = false;

        }

        private void speedTrackBar_Scroll(object sender, EventArgs e)
        {
            speed = speedTrackBar.Value;
        }

        private void cellSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            CELL_SIZE = cellSizeTrackBar.Value;
        }
    }
}
