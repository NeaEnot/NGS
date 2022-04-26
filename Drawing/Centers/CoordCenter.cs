using Physics;
using System.Collections.Generic;

namespace Drawing.Centers
{
    public class CoordCenter : ICenter
    {
        private double x;
        private double y;

        public Vector Center => new Vector { Vx = x, Vy = y };

        public List<BodyState> BodyStates { private get; set; }

        public bool IsScaled => false;

        public CoordCenter(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
