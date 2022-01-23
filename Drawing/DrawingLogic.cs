using Drawing.Centers;
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

        public Bitmap GetCurrentFrame(double w, double h, ICenter center, double distance = 1)
        {
            Bitmap bmp = new Bitmap((int)w, (int)h);
            Graphics gr = Graphics.FromImage(bmp);

            ColorConverter converter = new ColorConverter();

            Vector c = center.Center;
            int xStart = (int)(c.Vx / distance - w / 2);
            int yStart = (int)(c.Vy / distance - h / 2);

            gr.Clear(Color.Black);

            foreach (Body body in universe.Bodies)
            {
                double x = body.X / distance;
                double y = body.Y / distance;
                double d = body.D / distance;

                if (Math.Abs(x - c.Vx / distance) > w / 2 + d || Math.Abs(y - c.Vy / distance) > h / 2 + d)
                    continue;

                int px = (int)(x - d / 2 - xStart);
                int py = (int)(y - d / 2 - yStart);

                Brush brush = new SolidBrush((Color)converter.ConvertFromString(body.ColorHex));
                gr.FillEllipse(brush, px, py, (int)d, (int)d);
            }

            return bmp;
        }
    }
}
