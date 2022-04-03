using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public class UniverseState
    {
        public List<BodyState> BodyStates { get; set; }

        public UniverseState() { }

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
