using Drawing.Centers;
using Physics;
using System;
using System.Drawing;

namespace Drawing
{
    public class DrawingLogic
    {
        private Universe universe;

        private int imprintIntensity;
        private Bitmap prevBmp;

        private Color background = Color.Black;

        public DrawingLogic(Universe universe, int imprintIntensity = 250)
        {
            this.universe = universe;
            this.imprintIntensity = imprintIntensity;
        }

        public Bitmap GetCurrentFrame(double w, double h, ICenter center, double distance = 1)
        {
            Bitmap bmp = new Bitmap((int)w, (int)h);
            Graphics gr = Graphics.FromImage(bmp);

            ColorConverter converter = new ColorConverter();

            Vector c = center.Center;
            int xStart = (int)(c.Vx / (center.IsScaled ? distance : 1) - w / 2);
            int yStart = (int)(c.Vy / (center.IsScaled ? distance : 1) - h / 2);

            gr.Clear(background);

            if (prevBmp != null)
                gr.DrawImage(prevBmp, 0, 0);

            foreach (Body body in universe.Bodies)
            {
                double x = body.X / distance;
                double y = body.Y / distance;
                double d = body.D / distance;
                d = d >= 1 ? d : 1;

                if (Math.Abs(x - c.Vx / distance) > w / 2 + d || Math.Abs(y - c.Vy / distance) > h / 2 + d)
                    continue;

                int px = (int)(x - d / 2 - xStart);
                int py = (int)(y - d / 2 - yStart);

                Brush brush = new SolidBrush((Color)converter.ConvertFromString(body.ColorHex));
                gr.FillEllipse(brush, px, py, (int)d, (int)d);
            }

            prevBmp = Impose(bmp);

            return bmp;
        }

        private Bitmap Impose(Bitmap bmp)
        {
            bmp = new Bitmap(bmp);

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color tmp = bmp.GetPixel(x, y);
                    tmp = Color.FromArgb(imprintIntensity, tmp);
                    bmp.SetPixel(x, y, RemoveAlpha(tmp));
                }
            }

            return bmp;
        }

        public Color RemoveAlpha(Color foreground)
        {
            if (foreground.A == 255)
                return foreground;

            var alpha = foreground.A / 255.0;
            var diff = 1.0 - alpha;
            return Color.FromArgb(255,
                (byte)(foreground.R * alpha + background.R * diff),
                (byte)(foreground.G * alpha + background.G * diff),
                (byte)(foreground.B * alpha + background.B * diff));
        }
    }
}
