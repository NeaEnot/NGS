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

        List<BodyState> ISorting.Sort(List<BodyState> bodyStates, List<Body> bodies)
        {
            if (order == Order.Ascending)
                return bodyStates.OrderBy(rec => bodies.First(b => b.Id == rec.Id).Mass).ToList();
            else
                return bodyStates.OrderByDescending(rec => bodies.First(b => b.Id == rec.Id).Mass).ToList();
        }

        public override string ToString()
        {
            return "По массе (" + (order == Order.Ascending ? "прям." : "обр.") + ")";
        }
    }
}
