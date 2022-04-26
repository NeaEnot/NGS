using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="IdHelper"]/IdHelper/*'/>
    public class IdHelper
    {
        private static string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private Universe universe;

        /// <include file='Documentation.xml' path='documentation/members[@name="IdHelper"]/Constructor/*'/>
        public IdHelper(Universe universe)
        {
            this.universe = universe;
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="IdHelper"]/GetId/*'/>
        public string GetId()
        {
            string answer = "";
            int i = 2;
            Random random = new Random();

            while (true)
            {
                StringBuilder builder = new StringBuilder();

                alphabet
                    .ToArray()
                    .OrderBy(e => Guid.NewGuid())
                    .Take(random.Next(1, i))
                    .ToList().ForEach(e => builder.Append(e));

                answer = builder.ToString();

                if (universe.Bodies.Count(req => req.Id == answer) == 0)
                    break;

                i++;
            }

            return answer;
        }
    }
}
