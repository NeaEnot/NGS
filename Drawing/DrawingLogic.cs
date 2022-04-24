using Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Drawing
{
    public class DrawingLogic
    {
        private static ColorConverter converter = new ColorConverter();

        private Universe universe;

        private int imprintIntensity;
        private Bitmap prevBmp;
        private Bitmap trajectoryBmp;

        private UniverseState prevState;

        private Color background = Color.Black;

        public DrawingLogic(Universe universe, int imprintIntensity = 250)
        {
            this.universe = universe;
            this.imprintIntensity = imprintIntensity;
        }

        public Bitmap GetCurrentFrame(DrawingParams p)
        {
            Bitmap bmp = new Bitmap((int)p.W, (int)p.H);
            Graphics gr = Graphics.FromImage(bmp);

            Vector c = p.Center.Center;
            int xStart = (int)(c.Vx / (p.Center.IsScaled ? p.Distance : 1) - p.W / 2);
            int yStart = (int)(c.Vy / (p.Center.IsScaled ? p.Distance : 1) - p.H / 2);

            gr.Clear(background);

            if (trajectoryBmp != null)
                gr.DrawImage(DrawTraectory(p), 0, 0);

            List<BodyState> bodyStates = p.Sorting != null ? p.Sorting.Sort(p.UniverseState.BodyStates, universe.Bodies) : p.UniverseState.BodyStates;

            foreach (BodyState bodyState in bodyStates)
            {
                double x = bodyState.X / p.Distance;
                double y = bodyState.Y / p.Distance;
                double d = universe.Bodies.First(req => req.Id == bodyState.Id).D / p.Distance;
                d = d >= 1 ? d : 1;

                if (Math.Abs(x - c.Vx / p.Distance) > p.W / 2 + d || Math.Abs(y - c.Vy / p.Distance) > p.H / 2 + d)
                    continue;

                int px = (int)(x - d / 2 - xStart);
                int py = (int)(y - d / 2 - yStart);

                Brush brush = new SolidBrush((Color)converter.ConvertFromString(universe.Bodies.First(req => req.Id == bodyState.Id).ColorHex));
                gr.FillEllipse(brush, px, py, (int)d, (int)d);
            }

            return bmp;
        }

        private Bitmap DrawTraectory(DrawingParams p)
        {
            Bitmap bmp = Impose(trajectoryBmp);
            Graphics gr = Graphics.FromImage(bmp);

            Vector c = p.Center.Center;
            int xStart = (int)(c.Vx / (p.Center.IsScaled ? p.Distance : 1) - p.W / 2);
            int yStart = (int)(c.Vy / (p.Center.IsScaled ? p.Distance : 1) - p.H / 2);

            foreach (BodyState bodyState in p.UniverseState.BodyStates)
            {
                double x = bodyState.X / p.Distance;
                double y = bodyState.Y / p.Distance;

                if ((Math.Abs(x - c.Vx / p.Distance) > p.W || Math.Abs(y - c.Vy / p.Distance) > p.H) &&
                    (Math.Abs(prevState.BodyStates.First(req => req.Id == bodyState.Id).X - c.Vx / p.Distance) > p.W ||
                    Math.Abs(prevState.BodyStates.First(req => req.Id == bodyState.Id).Y - c.Vy / p.Distance) > p.H))
                    continue;

                int ppx = (int)(prevState.BodyStates.First(req => req.Id == bodyState.Id).X - xStart);
                int ppy = (int)(prevState.BodyStates.First(req => req.Id == bodyState.Id).Y - yStart);
                int pcx = (int)(x - xStart);
                int pcy = (int)(y - yStart);

                Pen pen = new Pen((Color)converter.ConvertFromString(universe.Bodies.First(req => req.Id == bodyState.Id).ColorHex));
                gr.DrawLine(pen, ppx, ppy, pcx, pcy);
            }

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

        private Color RemoveAlpha(Color foreground)
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
