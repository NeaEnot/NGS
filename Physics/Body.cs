using System;

namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/Body/*'/>
    public class Body
    {
        /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/Id/*'/>
        public string Id { get; set; }

        /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/X/*'/>
        public double X { get; set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/Y/*'/>
        public double Y { get; set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/D/*'/>
        public uint D { get; set; }

        /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/Mass/*'/>
        public uint Mass { get; set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/Velocity/*'/>
        public Vector Velocity { get; set; }

        /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/ColorHex/*'/>
        public string ColorHex { get; set; }

        public Body()
        {
            Velocity = new Vector { Vx = 0, Vy = 0 };
        }

        public override string ToString()
        {
            return $"{Id}, {ColorHex}, {D}:{Mass}";
        }
    }
}
