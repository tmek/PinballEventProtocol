using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using VPinMAMELib;
using COMAdmin;

namespace VPinMAMETest
{
    public partial class Form1 : Form
    {
        const int LampMatrixColumns = 8;
        const int LampMatrixRows = 8;
        const int SwitchMatrixColumns = 11;
        const int SwitchMatrixRows = 8;
        Label[,] LampMatrix = new Label[LampMatrixColumns + 1, LampMatrixRows + 1];
        CheckBox[,] SwitchMatrix = new CheckBox[SwitchMatrixColumns + 1, SwitchMatrixRows + 1];
        Color LightOnColor = Color.DarkOrange;
        Color LightOffColor = Color.Gray;

        public Form1()
        {
            InitializeComponent();

            var size = 40;
            var margin = 3;

            // init switch buttons
            for (int c = 0; c <= SwitchMatrixColumns; c++)
                for (int r = 1; r <= SwitchMatrixRows; r++)
                {
                    var button = new CheckBox();
                    button.Appearance = Appearance.Button;
                    button.Width = size;
                    button.Height = size;
                    button.Text = string.Format("{0},{1}", c, r);

                    button.Top = (r - 1) * (size + margin);
                    button.Left = (c) * (size + margin);

                    button.Click += Button_Click;

                    SwitchMatrix[c, r] = button;

                    switchesPanel.Controls.Add(button);
                }

            // init lamp labels
            for (int c = 1; c <= LampMatrixColumns; c++)
                for (int r = 1; r <= LampMatrixRows; r++)
                {
                    var lamp = new Label();
                    lamp.Width = size;
                    lamp.Height = size;
                    lamp.Text = string.Format("{0},{1}", c, r);
                    lamp.TextAlign = ContentAlignment.MiddleCenter;
                    lamp.ForeColor = Color.White;

                    lamp.Top = (r - 1) * (size + margin);
                    lamp.Left = (c - 1) * (size + margin);

                    lamp.BackColor = LightOffColor;
                    LampMatrix[c, r] = lamp;

                    lampsPanel.Controls.Add(lamp);
                }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = sender as CheckBox;
            var indecies = button.Text.Split(',');
            var col = int.Parse(indecies[0]);
            var row = int.Parse(indecies[1]);
            int index = col * 10 + row;

            vpm.Switch[index] = button.Checked;
        }

        VPinMAMELib.Controller vpm;

        private void Log(string text)
        {
            logTextBox.AppendText(text + System.Environment.NewLine);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            vpm = new Controller();
            vpm.OnStateChange += Vpm_OnStateChange;

            vpm.GameName = "ft_l5";
            //vpm.GameName = "flight2k";

            //vpm.HandleKeyboard = true;
            vpm.ShowDMDOnly = false;

            /*
            vpm.ShowFrame = false;
            vpm.ShowTitle = false;
            vpm.DoubleSize = false;
            vpm.LockDisplay = true;
            vpm.ShowDMDOnly = true;
            vpm.Hidden = true;
            */

            this.Text = vpm.Version;

            //vpm.Dip[17] = 1;
            vpm.Run((int)this.Handle);
            //vpm.Dip[17] = 1;


            Log("SampleRate:" + vpm.SampleRate);

            Log("DMD Width: " + vpm.RawDmdWidth);
            Log("DMD Height: " + vpm.RawDmdHeight);

            //DumpCOMInfo();

        }

        private void DumpCOMInfo()
        {
            COMAdminCatalog catalog;
            COMAdminCatalogCollection applications;

            catalog = new COMAdminCatalog();

            // COM+ Applications
            applications = (COMAdminCatalogCollection)catalog.GetCollection("Applications");
            applications.Populate();

            foreach (COMAdminCatalogObject application in applications)
            {
                Log("\nApplication Name: " + application.Name);

                COMAdminCatalogCollection components;
                components = (COMAdminCatalogCollection)applications.GetCollection("Components", application.Key);
                components.Populate(); // Forgot to call this
                foreach (COMAdminCatalogObject component in components)
                {
                    Log("Component Name: " + component.Name);
                    Log("Constructor String: " + component.get_Value("ConstructorString"));
                }
            }
        }


        private void Vpm_OnStateChange(int nState)
        {
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            frameTimer.Stop();
            vpm.Stop();
            vpm = null;
        }


        private int MatrixToIndex(int column, int row)
        {
            if (vpm.WPCNumbering)
            {
                return column * 10 + row;
            }
            else
            {
                return (column - 1) * 8 + row;
            }
        }

        private Point IndexToMatrix(int index)
        {
            int column;
            int row;

            if (vpm.WPCNumbering)
            {
                column = index / 10;
                row = index - column * 10;
            }
            else
            {
                // Index numbers sequentially go through each row of column 1, then each row of column 2 etc...
                // Index ids start at 1, not 0. So convert to zero based indexing so we can use / and % 8.
                // then add back 1 to the calculated col and row.
                int zeroBasedIndex = index - 1;
                column = zeroBasedIndex / 8 + 1;
                row = zeroBasedIndex % 8 + 1;
            }

            return new Point(column, row);
        }


        private void PollChangedLamps()
        {
            var changedLamps = vpm.ChangedLamps as object[,];
            if (changedLamps == null) return;

            int count = changedLamps.Length / 2;
            for (int i = 0; i < count; i++)
            {
                int lampID = (int)changedLamps[i, 0];
                bool lampOn = (int)changedLamps[i, 1] == 1 ? true : false;

                Point indecies = IndexToMatrix(lampID);

                LampMatrix[indecies.X, indecies.Y].BackColor = lampOn ? LightOnColor : LightOffColor;
            }
        }

        private void PollChangedSwitches()
        {
            for (int c = 0; c <= SwitchMatrixColumns; c++)
                for (int r = 1; r <= SwitchMatrixRows; r++)
                {
                    int index;
                    if (vpm.WPCNumbering)
                    {
                        index = c * 10 + r;
                    }
                    else
                    {
                        index = c * 8 + r;
                    }


                    if (vpm != null)
                    {
                        SwitchMatrix[c, r].Checked = vpm.Switch[index];
                    }
                }
        }

        private Color DimColor(Color color, float brightness)
        {
            int red = (int)(color.R * brightness);
            int green = (int)(color.G * brightness);
            int blue = (int)(color.B * brightness);

            //int argb = 0xff << 24 | red << 16 | green << 8 | blue;



            return Color.FromArgb(red, green, blue);
        }

        Bitmap dmdbitmap = new Bitmap(256, 64);
        HashSet<int> pixelValues = new HashSet<int>();
        private void UpdateDMD()
        {
            var rawdmdpixels = vpm.RawDmdPixels as object[];
            if (rawdmdpixels == null) return;

            for (int i = 0; i < rawdmdpixels.Length; i++)
            {
                //UInt64 pixel64 = (UInt64)rawdmdpixels[i];
                //Int32 pixel = (Int32)(0x00000000ffffffffUL & pixel64);

                byte pixel = (byte)rawdmdpixels[i];

                if (!pixelValues.Contains(pixel))
                {
                    pixelValues.Add(pixel);
                    Log("Pixel: " + pixel);
                }

                int x = i % 128;
                int y = i / 128;

                //Pixel: 20
                //Pixel: 33
                //Pixel: 67
                //Pixel: 100


                float intensity = (float)pixel / 100.0f;

                Color fullBrightColor = Color.FromArgb(0xFF5820);
                //fullBrightColor = Color.White;
                Color pixelColor = DimColor(fullBrightColor, intensity);

                dmdbitmap.SetPixel(x * 2, y * 2, pixelColor);
            }


            pictureBox1.Image = dmdbitmap;
        }

        private void frameTimer_Tick(object sender, EventArgs e)
        {
            PollChangedLamps();

            //PollChangedSwitches();

            UpdateDMD();
            // poll changed solenoids

            //UpdateLights();
        }

        private void switchesPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lampsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Task.Factory.StartNew(() =>
            {
                vpm.Switch[01] = true;
                Thread.Sleep(100);
                Console.WriteLine("Hello Task library!");
                vpm.Switch[01] = false;
            }
            );

            

        }
    }
}


/*

Private Sub Start_Click()
    vpm.Run Me.hWnd, 99
End Sub

Private Sub Stop_Click()
    vpm.Stop
End Sub

Private Sub Form_Load()
    Set vpm = New VPinMAMELib.Controller
    vpm.GameName = "pinpool"
End Sub

Private Sub Form_Unload(Cancel As Integer)
    Set vpm = Nothing
End Sub
*/
