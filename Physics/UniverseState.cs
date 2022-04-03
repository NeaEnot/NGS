using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="UniverseState"]/UniverseState/*'/>
    public class UniverseState
    {
        /// <include file='Documentation.xml' path='documentation/members[@name="UniverseState"]/BodyStates/*'/>
        public List<BodyState> BodyStates { get; set; }

        /// <include file='Documentation.xml' path='documentation/members[@name="UniverseState"]/Constructor0/*'/>
        public UniverseState() { }

        /// <include file='Documentation.xml' path='documentation/members[@name="UniverseState"]/Constructor1/*'/>
        public UniverseState(Universe universe)
        {
            BodyStates = 
                universe.Bodies
                .Select(req => new BodyState
                {
                    Id = req.Id,
                    X = req.X,
                    Y = req.Y,
                    Velocity = req.Velocity
                })
                .ToList();
        }
    }
}
