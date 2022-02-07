using Physics;
using System.Collections.Generic;
using System.Linq;

namespace Drawing.Sortings
{
    class MassSorting : ISorting
    {
        private Order order;

        public MassSorting(Order order)
        {
            this.order = order;
        }

        List<Body> ISorting.Sort(List<Body> bodies)
        {
            if (order == Order.Ascending)
                return bodies.OrderBy(rec => rec.Mass).ToList();
            else
                return bodies.OrderByDescending(rec => rec.Mass).ToList();
        }
    }
}
