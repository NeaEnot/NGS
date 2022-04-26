using Physics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Drawing.Centers
{
    public class BodyCenter : ICenter
    {
        private Body body;

        public Vector Center => new Vector { Vx = BodyStates.First(req => req.Id == body.Id).X, Vy = BodyStates.First(req => req.Id == body.Id).Y };

        public List<BodyState> BodyStates { private get; set; }

        public bool IsScaled => true;

        public BodyCenter(Body body)
        {
            if (body == null)
                throw new NullReferenceException();
            this.body = body;
        }
    }
}
