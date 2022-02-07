using Physics;
using System.Collections.Generic;

namespace Drawing.Sortings
{
    public interface ISorting
    {
        internal List<Body> Sort(List<Body> bodies);
    }
}
