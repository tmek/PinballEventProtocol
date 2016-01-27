using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinballEvents
{
    public partial class ShapeControl : Control
    {
        public Color ShapeColor { get; set; }

        public ShapeControl()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            InitializeComponent();

            this.ShapeColor = Color.Blue;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;

            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;


            var shapeBrush = new SolidBrush(Color.Blue  );
            g.FillEllipse(shapeBrush, pe.ClipRectangle);
            g.DrawEllipse(Pens.White, pe.ClipRectangle);

            //StringFormat stringFormat = new StringFormat();
            //stringFormat.Alignment = StringAlignment.Center;
            //stringFormat.LineAlignment = StringAlignment.Center;
            //stringFormat.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox | ~StringFormatFlags.word;

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoClipping;

            Font font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);

            TextRenderer.DrawText(g, this.Text, this.Font, new Point(22,10), Color.White, flags);

            
            //g.DrawString("44", Font, Brushes.Red    , pe.ClipRectangle, stringFormat);

        }

        protected void Draw(Graphics g)
        {
        }
    }
}
