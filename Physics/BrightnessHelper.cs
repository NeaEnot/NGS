using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="BrightnessCalc"]/BrightnessCalc/*'/>
    public static class BrightnessHelper
    {
        private static Dictionary<char, byte> shades = new Dictionary<char, byte>
        {
            { '0', 0 },
            { '1', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'a', 10 },
            { 'b', 11 },
            { 'c', 12 },
            { 'd', 13 },
            { 'e', 14 },
            { 'f', 15 }
        };

        private static string alphabet = "0123456789abcdef";

        /// <include file='Documentation.xml' path='documentation/members[@name="BrightnessCalc"]/Calc/*'/>
        public static ushort Calc(string colorHex)
        {
            ushort answer = 0;

            for (byte i = 1; i < colorHex.Length; i++)
            {
                byte k = shades[colorHex[i]];
                if (i % 2 == 1)
                    answer += (ushort)(k * 16);
                else
                    answer += k;
            }

            return answer;
        }

        public static List<string> GenerateColors(ushort brightnessMin, ushort brightnessMax)
        {
            List<string> answer = new List<string>();

            StringBuilder color = new StringBuilder("#000000");

            while (true)
            {
                ushort brightness = BrightnessHelper.Calc(color.ToString());
                if (brightness >= brightnessMin && brightness <= brightnessMax)
                    answer.Add(color.ToString());

                if (color.Equals("#ffffff"))
                    break;
                else
                    Increment(color);
            }

            return answer;
        }

        private static void Increment(StringBuilder color, int index = 6)
        {
            if (color[index] != 'f')
            {
                int k = alphabet.IndexOf(color[index]);
                color[index] = alphabet[k + 1];
            }
            else
            {
                color[index] = '0';
                Increment(color, index - 1);
            }
        }
    }
}
