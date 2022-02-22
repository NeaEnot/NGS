using Physics;
using System.Collections.Generic;
using System.Linq;

namespace Drawing.Centers
{
    public class MassCenter : ICenter
    {
        private List<Body> bodies;

        public Vector Center =>
            new Vector
            {
                Vx = bodies.Sum(req => req.X * req.Mass) / bodies.Sum(req => req.X),
                Vy = bodies.Sum(req => req.Y * req.Mass) / bodies.Sum(req => req.Y)
            };

        public bool IsScaled => true;

        public MassCenter(List<Body> bodies)
        {
            this.bodies = bodies.ToList();
        }
    }
}
