using Physics;
using System.Collections.Generic;

namespace Drawing.Sortings
{
    public interface ISorting
    {
        internal List<BodyState> Sort(List<BodyState> bodyStates, List<Body> bodies);
    }
}
