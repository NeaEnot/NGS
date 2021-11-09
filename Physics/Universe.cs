using System.Collections.Generic;

namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Universe/*'/>
    public class Universe
    {
        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Id/*'/>
        public string Id { get; set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Name/*'/>
        public string Name { get; set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Bodies/*'/>
        public List<Body> Bodies { get; private set; }

        private double g;

        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Constructor/*'/>
        public Universe(double g, string name)
        {
            Bodies = new List<Body>();
            this.g = g;
            Name = name;
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Update/*'/>
        public void Update()
        {
            MoveBodies();
            UpdateVelocities();
        }

        private void MoveBodies()
        {
            foreach (Body body in Bodies)
            {
                body.X += (long)body.Velocity.Vx;
                body.Y += (long)body.Velocity.Vy;
            }
        }

        private void UpdateVelocities()
        {
            foreach (Body current in Bodies)
            {
                foreach (Body body in Bodies)
                {
                    if (body == current)
                        continue;

                    long rx = current.X - body.X;
                    long ry = current.Y - body.Y;

                    Vector a = new Vector
                    {
                        Vx = g * body.Mass / (rx * rx) * Sign(rx),
                        Vy = g * body.Mass / (ry * ry) * Sign(rx)
                    };

                    current.Velocity += a;
                }
            }
        }

        private int Sign(long n)
        {
            if (n >= 0)
                return 1;
            else 
                return -1;
        }
    }
}
