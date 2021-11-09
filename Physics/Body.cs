﻿namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/Body/*'/>
    public class Body
    {
        /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/Id/*'/>
        public string Id { get; set; }

        /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/X/*'/>
        public long X { get; set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/Y/*'/>
        public long Y { get; set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Body"]/D/*'/>
        public byte D { get; set; }

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
    }
}