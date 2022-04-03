using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="BodyState"]/BodyState/*'/>
    public class BodyState
    {
        /// <include file='Documentation.xml' path='documentation/members[@name="BodyState"]/BodyState/*'/>
        public string Id { get; set; }

        /// <include file='Documentation.xml' path='documentation/members[@name="BodyState"]/X/*'/>
        public double X { get; set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="BodyState"]/Y/*'/>
        public double Y { get; set; }

        /// <include file='Documentation.xml' path='documentation/members[@name="BodyState"]/Velocity/*'/>
        public Vector Velocity { get; set; }
    }
}
