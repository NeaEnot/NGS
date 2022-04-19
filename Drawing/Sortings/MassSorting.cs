using Physics;
using System.Collections.Generic;
using System.Linq;

namespace Drawing.Sortings
{
    public class MassSorting : ISorting
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

        public override string ToString()
        {
            return "По массе (" + (order == Order.Ascending ? "прям." : "обр.") + ")";
        }
    }
}
