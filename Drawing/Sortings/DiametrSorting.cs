using Physics;
using System.Collections.Generic;
using System.Linq;

namespace Drawing.Sortings
{
    public class DiametrSorting : ISorting
    {
        private Order order;

        public DiametrSorting(Order order)
        {
            this.order = order;
        }

        List<Body> ISorting.Sort(List<Body> bodies)
        {
            if (order == Order.Ascending)
                return bodies.OrderBy(rec => rec.D).ToList();
            else
                return bodies.OrderByDescending(rec => rec.D).ToList();
        }

        public override string ToString()
        {
            return "По диаметру (" + (order == Order.Ascending ? "прям." : "обр.") + ")";
        }
    }
}
