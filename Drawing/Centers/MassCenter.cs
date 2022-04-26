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
                Vx = bodies.Sum(req => BodyStates.First(b => b.Id == req.Id).X * req.Mass) / bodies.Sum(req => req.Mass),
                Vy = bodies.Sum(req => BodyStates.First(b => b.Id == req.Id).Y * req.Mass) / bodies.Sum(req => req.Mass)
            };

        public List<BodyState> BodyStates { private get; set; }

        public bool IsScaled => true;

        public MassCenter(List<Body> bodies)
        {
            this.bodies = bodies.ToList();
        }
    }
}
