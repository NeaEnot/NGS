using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public class BodyState
    {
        public string Id { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public Vector Velocity { get; set; }
    }
}
