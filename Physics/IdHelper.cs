using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
    internal class IdHelper
    {
        private static string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private List<string> ids;

        internal IdHelper(List<string> ids)
        {
            this.ids = ids;
        }

        internal string GetId()
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

                if (!ids.Contains(answer))
                    break;

                i++;
            }

            ids.Add(answer);

            return answer;
        }
    }
}
