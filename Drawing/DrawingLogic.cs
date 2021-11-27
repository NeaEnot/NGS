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

        public Bitmap GetCurrentFrame(int w, int h)
        {
            Bitmap bmp = new Bitmap(w, h);
            Graphics gr = Graphics.FromImage(bmp);

            ColorConverter converter = new ColorConverter();

            gr.Clear(Color.Black);

            int cw = w / 2;
            int ch = h / 2;

            long cx = 0;
            long cy = 0;

            foreach (Body body in universe.Bodies)
            {
                if (Math.Abs(body.X) > w / 2 + body.D || Math.Abs(body.Y) > h / 2 + body.D)
                    continue;

                Brush brush = new SolidBrush((Color)converter.ConvertFromString(body.ColorHex));
                gr.FillEllipse(brush, body.X - body.D / 2, body.Y - body.D / 2, body.D, body.D);
            }

            return bmp;
        }
    }
}
