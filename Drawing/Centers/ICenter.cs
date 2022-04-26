using Physics;
using System.Collections.Generic;

namespace Drawing.Centers
{
    public interface ICenter
    {
        public Vector Center { get; }
        public bool IsScaled { get; }

        public List<BodyState> BodyStates { set; }
    }
}
