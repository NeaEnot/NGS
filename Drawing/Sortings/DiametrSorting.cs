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

        List<BodyState> ISorting.Sort(List<BodyState> bodyStates, List<Body> bodies)
        {
            if (order == Order.Ascending)
                return bodyStates.OrderBy(rec => bodies.First(b => b.Id == rec.Id).D).ToList();
            else
                return bodyStates.OrderByDescending(rec => bodies.First(b => b.Id == rec.Id).D).ToList();
        }

        public override string ToString()
        {
            return "По диаметру (" + (order == Order.Ascending ? "прям." : "обр.") + ")";
        }
    }
}
