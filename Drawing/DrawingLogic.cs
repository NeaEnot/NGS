using Physics;
using System;
using System.Drawing;

namespace Drawing
{
    public class DrawingLogic
    {
        private Universe universe;

        public DrawingLogic(Universe universe)
        {
            this.universe = universe;
        }

        public Bitmap GetCurrentFrame(double w, double h)
        {
            Bitmap bmp = new Bitmap((int)w, (int)h);
            Graphics gr = Graphics.FromImage(bmp);

            ColorConverter converter = new ColorConverter();

            gr.Clear(Color.Black);

            int xStart = (int)(0 - w / 2);
            int yStart = (int)(0 - h / 2);

            foreach (Body body in universe.Bodies)
            {
                if (Math.Abs(body.X) > w / 2 + body.D || Math.Abs(body.Y) > h / 2 + body.D)
                    continue;

                Brush brush = new SolidBrush((Color)converter.ConvertFromString(body.ColorHex));
                gr.FillEllipse(brush, body.X - body.D / 2 - xStart, body.Y - body.D / 2 - yStart, body.D, body.D);
            }

            return bmp;
        }
    }
}
