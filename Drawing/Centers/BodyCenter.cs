using Physics;
using System;

namespace Drawing.Centers
{
    public class BodyCenter : ICenter
    {
        private Body body;

        public Vector Center => new Vector { Vx = body.X, Vy = body.Y };

        public BodyCenter(Body body)
        {
            if (body == null)
                throw new NullReferenceException();
            this.body = body;
        }
    }
}
