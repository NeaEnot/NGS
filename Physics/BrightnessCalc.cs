using System.Collections.Generic;

namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="BrightnessCalc"]/BrightnessCalc/*'/>
    public class BrightnessCalc
    {
        private static BrightnessCalc instance;

        private static Dictionary<char, byte> shades;

        /// <include file='Documentation.xml' path='documentation/members[@name="BrightnessCalc"]/Instance/*'/>
        public static BrightnessCalc Instance
        {
            get
            {
                if (instance == null)
                    instance = new BrightnessCalc();

                return instance;
            }
        }

        private BrightnessCalc()
        {
            if (shades == null)
            {
                shades = new Dictionary<char, byte>();
                shades.Add('0', 0);
                shades.Add('1', 1);
                shades.Add('2', 2);
                shades.Add('3', 3);
                shades.Add('4', 4);
                shades.Add('5', 5);
                shades.Add('6', 6);
                shades.Add('7', 7);
                shades.Add('8', 8);
                shades.Add('9', 9);
                shades.Add('a', 10);
                shades.Add('b', 11);
                shades.Add('c', 12);
                shades.Add('d', 13);
                shades.Add('e', 14);
                shades.Add('f', 15);
            }
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="BrightnessCalc"]/Calc/*'/>
        public ushort Calc(string colorHex)
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
