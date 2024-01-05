using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Universe/*'/>
    public class Universe
    {
        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Name/*'/>
        public string Name { get; set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/G/*'/>
        public double G { get; set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Bodies/*'/>
        public List<Body> Bodies { get; private set; }

        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Constructor/*'/>
        public Universe()
        {
            Bodies = new List<Body>();
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/ToState/*'/>
        public void ToState(UniverseState state)
        {
            foreach (BodyState bodyState in state.BodyStates)
            {
                Body body = Bodies.First(req => req.Id == bodyState.Id);
                body.X = bodyState.X;
                body.Y = bodyState.Y;
                body.Velocity = bodyState.Velocity;
            }
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Update/*'/>
        public void Update()
        {
            MoveBodies();
            UpdateVelocities();
        }

        private void MoveBodies()
        {
            double centerX = Bodies.Sum(x => x.X);
            double centerY = Bodies.Sum(x => x.Y);

            foreach (Body body in Bodies)
            {
                body.X += body.Velocity.Vx - centerX;
                body.Y += body.Velocity.Vy - centerY;
            }
        }

        private void UpdateVelocities()
        {
            for (int i = 0; i < Bodies.Count; i++)
            {
                Body current = Bodies[i];

                for (int j = i + 1; j < Bodies.Count; j++)
                {
                    Body body = Bodies[j];

                    if (body == current)
                        continue;

                    double rx = current.X - body.X;
                    double ry = current.Y - body.Y;
                    double r = Math.Sqrt(rx * rx + ry * ry);

                    double sin = ry / r;
                    double cos = rx / r;

                    Vector a = new Vector
                    {
                        Vx = G * body.Mass / Math.Sqrt(r) * -cos,
                        Vy = G * body.Mass / Math.Sqrt(r) * -sin
                    };
                    Vector b = new Vector
                    {
                        Vx = G * current.Mass / Math.Sqrt(r) * cos,
                        Vy = G * current.Mass / Math.Sqrt(r) * sin
                    };

                    current.Velocity += a;
                    body.Velocity += b;
                }
            }
        }
    }
}
