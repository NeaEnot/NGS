using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static List<string> GenerateIds(uint count)
        {
            List<string> ids = new List<string>();

            StringBuilder id = new StringBuilder($"{alphabet[0]}");

            for (uint i = 0; i < count; i++)
            {
                ids.Add(id.ToString());
                Increment(id, id.Length - 1);
            }

            return ids;
        }

        private static void Increment(StringBuilder id, int index)
        {
            if (index < 0)
            {
                id.Insert(0, alphabet[0]);
                return;
            }

            if (id[index] != alphabet[alphabet.Length - 1])
            {
                int k = alphabet.IndexOf(id[index]);
                id[index] = alphabet[k + 1];
            }
            else
            {
                id[index] = alphabet[0];
                Increment(id, index - 1);
            }
        }
    }
}
