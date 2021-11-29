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

        public Bitmap GetCurrentFrame(double w, double h, double distance = 1)
        {
            Bitmap bmp = new Bitmap((int)w, (int)h);
            Graphics gr = Graphics.FromImage(bmp);

            ColorConverter converter = new ColorConverter();

            gr.Clear(Color.Black);

            int xStart = (int)(0 - w / 2);
            int yStart = (int)(0 - h / 2);

            foreach (Body body in universe.Bodies)
            {
                double x = body.X / distance;
                double y = body.Y / distance;
                double d = body.D / distance;

                if (Math.Abs(x) > w / 2 + d || Math.Abs(y) > h / 2 + d)
                    continue;

                Brush brush = new SolidBrush((Color)converter.ConvertFromString(body.ColorHex));
                gr.FillEllipse(brush, (int)(x - d / 2 - xStart), (int)(y - d / 2 - yStart), (int)d, (int)d);
            }

            return bmp;
        }
    }
}
