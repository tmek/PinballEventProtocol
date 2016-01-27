using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace Pipes
{

    public class DmdBitmap
    {
        public Bitmap Bitmap { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Color LedColor { get; set; }

        public DmdBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bitmap = new Bitmap(width * 2, height * 2); // space out pixels to give that dmd grid look

            ChangeColor();
        }

        public void ChangeColor()
        {
            List<Color> colors = new List<Color>();
            colors.Add(Color.Red);
            colors.Add(Color.Green);
            colors.Add(Color.Blue);
            colors.Add(Color.Yellow);
            colors.Add(Color.Cyan);
            colors.Add(Color.Magenta);
            colors.Add(Color.Purple);
            colors.Add(Color.Orange);



            Random randomGen = new Random();
            LedColor = colors[randomGen.Next(colors.Count)];;
        }

        private Color DimColor(Color color, float brightness)
        {
            int red = (int)(color.R * brightness);
            int green = (int)(color.G * brightness);
            int blue = (int)(color.B * brightness);

            return Color.FromArgb(red, green, blue);
        }

        public void UpdateBitmap(byte[] pixels)
        {
            for (int i = 0; i < pixels.Length; i++)
            {

                byte pixel = pixels[i];

                int x = i % Width;
                int y = i / Width;

                float intensity = (float)pixel / 100.0f;

                //LedColor =  Color.FromArgb(0xFF5820);

                Color pixelColor = DimColor(LedColor, intensity);
                Color offPixelColor = DimColor(LedColor, intensity * 0.25f);

                Bitmap.SetPixel(x * 2, y * 2, pixelColor);
                Bitmap.SetPixel(x * 2 + 1, y * 2 + 1, offPixelColor);
                Bitmap.SetPixel(x * 2 + 0, y * 2 + 1, offPixelColor);
                Bitmap.SetPixel(x * 2 + 1, y * 2 + 0, offPixelColor);
            }
        }
    }
}
