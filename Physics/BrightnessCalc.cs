using System.Collections.Generic;

namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="BrightnessCalc"]/BrightnessCalc/*'/>
    public static class BrightnessCalc
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
    }
}
