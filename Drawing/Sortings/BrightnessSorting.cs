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

        List<Body> ISorting.Sort(List<Body> bodies)
        {
            if (order == Order.Ascending)
                return bodies.OrderBy(rec => BrightnessCalc.Calc(rec.ColorHex)).ToList();
            else
                return bodies.OrderByDescending(rec => BrightnessCalc.Calc(rec.ColorHex)).ToList();
        }

        public override string ToString()
        {
            return "По освещению (" + (order == Order.Ascending ? "прям." : "обр.") + ")";
        }
    }
}
