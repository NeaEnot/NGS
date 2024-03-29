﻿using Drawing.Centers;
using Drawing.Sortings;
using Physics;

namespace Drawing
{
    public class DrawingParams
    {
        public double W { get; set; }
        public double H { get; set; }
        public double Distance { get; set; }
        public ICenter Center { get; set; }
        public ISorting Sorting { get; set; }
        public UniverseState UniverseState { get; set; }
    }
}
