using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinballEvents
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var switches = new List<PinballDevice>();

            var r = new Random();


            for (int i = 0; i < 64; i++)
            {
                var sw = new PinballDevice(i, "switch#" + i.ToString(), false);
                listBox1.Items.Add(sw);
            }

            toolTip1.SetToolTip(shapeControl1, "Left Feed Frenzy");
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            toolTip1.AutomaticDelay = 0;

            //toolTip1.AutoPopDelay;

            
            toolTip1.UseFading = false;
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            //e.DrawBackground();
            e.Graphics.FillRectangle(Brushes.Black, e.Bounds);

            // Draw mouse over rectangle
            if (mouseIndex > -1 && mouseIndex == e.Index)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(51, 26, 0)), e.Bounds);
            }


            // Define the default color of the brush as black.
            Brush myBrush = new SolidBrush(Color.FromArgb(102, 52, 0));

            // Determine the color of the brush to draw each item based 
            // on the index of the item to draw.

            var sw = (PinballDevice)listBox1.Items[e.Index];

            if (sw.State)
            {
                myBrush = Brushes.Orange;
            }

            // Draw the current item text based on the current Font 
            // and the custom brush settings.
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);

            // If the ListBox has focus, draw a focus rectangle around the selected item.
            //e.DrawFocusRectangle();


        }

        int mouseDownIndex = -1;
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownIndex = listBox1.IndexFromPoint(e.Location);
            if (mouseDownIndex == -1) return;

            var item = (PinballDevice)listBox1.Items[mouseDownIndex];
            item.ToggleState();
            listBox1.Invalidate(listBox1.GetItemRectangle(mouseDownIndex));
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // retoggle on left mouse up
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (mouseDownIndex == -1) return;

                var item = (PinballDevice)listBox1.Items[mouseDownIndex];
                item.ToggleState();
                listBox1.Invalidate(listBox1.GetItemRectangle(mouseDownIndex));
                mouseDownIndex = -1;
            }
        }

        int mouseIndex = -1;
        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);

            if (index != mouseIndex)
            {
                if (mouseIndex > -1)
                {
                    int oldIndex = mouseIndex;
                    mouseIndex = -1;
                    if (oldIndex <= listBox1.Items.Count - 1)
                    {
                        listBox1.Invalidate(listBox1.GetItemRectangle(oldIndex));
                    }
                }
                mouseIndex = index;
                if (mouseIndex > -1)
                {
                    listBox1.Invalidate(listBox1.GetItemRectangle(mouseIndex));
                }
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (shapeControl1.BackColor != Color.Black)
                shapeControl1.BackColor = Color.Black;
            else
                shapeControl1.BackColor = Color.White;
        }

        private void shapeControl1_Click(object sender, EventArgs e)
        {
            ShapeControl sc = (ShapeControl)sender;

            if (sc.Text != "23")
                sc.Text = "23";
            else
            {
                sc.Text = "1";
            }

            sc.Invalidate();
        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }
    }
}
