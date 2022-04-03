using System;
using System.Collections.Generic;
using System.Linq;

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

        private IdHelper idHelper;

        /// <include file='Documentation.xml' path='documentation/members[@name="Universe"]/Constructor/*'/>
        public Universe()
        {
            Bodies = new List<Body>();
            idHelper = new IdHelper(Bodies.Select(req => req.Id).ToList());
            Body.IdHelper = idHelper;
        }

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
            foreach (Body body in Bodies)
            {
                body.X += body.Velocity.Vx;
                body.Y += body.Velocity.Vy;
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

                    current.Velocity += a;
                }
            }
        }
    }
}
