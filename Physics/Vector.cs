namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="Vector"]/Vector/*'/>
    public class Vector
    {
        /// <include file='Documentation.xml' path='documentation/members[@name="Vector"]/Vx/*'/>
        public double Vx { get; set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Vector"]/Vy/*'/>
        public double Vy { get; set; }

        /// <include file='Documentation.xml' path='documentation/members[@name="Vector"]/operatorPlus/*'/>
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector
            {
                Vx = v1.Vx + v2.Vx,
                Vy = v1.Vy + v2.Vy
            };
        }
    }
}
