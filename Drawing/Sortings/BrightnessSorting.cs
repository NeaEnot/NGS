using Physics;
using System.Collections.Generic;
using System.Linq;

namespace Drawing.Sortings
{
    public class BrightnessSorting : ISorting
    {
        private Order order;

        public BrightnessSorting(Order order)
        {
            this.order = order;
        }

        List<BodyState> ISorting.Sort(List<BodyState> bodyStates, List<Body> bodies)
        {
            if (order == Order.Ascending)
                return bodyStates.OrderBy(rec => BrightnessCalc.Calc(bodies.First(b => b.Id == rec.Id).ColorHex)).ToList();
            else
                return bodyStates.OrderByDescending(rec => BrightnessCalc.Calc(bodies.First(b => b.Id == rec.Id).ColorHex)).ToList();
        }

        public override string ToString()
        {
            return "По освещению (" + (order == Order.Ascending ? "прям." : "обр.") + ")";
        }
    }
}
