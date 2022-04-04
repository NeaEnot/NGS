using System;
using System.Linq;

namespace Physics
{
    /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/Date/*'/>
    public class Date
    {
        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/maxDays/*'/>
        public readonly static uint maxDays = 365;
        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/maxYears/*'/>
        public readonly static uint maxYears = 1000;
        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/maxMilleniums/*'/>
        public readonly static uint maxMilleniums = 1000;

        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/Day/*'/>
        public uint Day { get; private set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/Year/*'/>
        public uint Year { get; private set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/Millenium/*'/>
        public uint Millenium { get; private set; }
        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/Leodr/*'/>
        public uint Leodr { get; private set; }

        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/Constructor/*'/>
        public Date(uint day = 0, uint year = 0, uint millenium = 0, uint leodr = 0)
        {
            if (day > maxDays || year > maxYears || millenium > maxMilleniums)
                throw new Exception($"Неверные значения. Должно быть:\n day < {maxDays}\n year < {maxYears}\n millenium < {maxMilleniums}");

            this.Day = day;
            this.Year = year;
            this.Millenium = millenium;
            this.Leodr = leodr;
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/NextDay/*'/>
        public Date NextDay()
        {
            uint day = this.Day;
            uint year = this.Year;
            uint millenium = this.Millenium;
            uint leodr = this.Leodr;

            day++;

            if (day == maxDays)
            {
                day = 0;
                year++;

                if (year == maxYears)
                {
                    year = 0;
                    millenium++;

                    if (millenium == maxMilleniums)
                    {
                        millenium = 0;
                        leodr++;
                    }
                }
            }

            return new Date(day, year, millenium, leodr);
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/ToString/*'/>
        public override string ToString()
        {
            return 
                Leodr.ToString("00000000") + "." +
                Millenium.ToString("000") + "." +
                Year.ToString("000") + "." +
                Day.ToString("000");
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/Parse/*'/>
        public static Date Parse(string str)
        {
            uint[] comps = str.Split('.').Select(req => uint.Parse(req)).ToArray();

            Date date = new Date(comps[3], comps[2], comps[1], comps[0]);

            return date;
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/operatorPlus/*'/>
        public static Date operator +(Date d1, Date d2)
        {
            uint day = d1.Day + d2.Day;
            uint year = d1.Year + d2.Year;
            uint millenium = d1.Millenium + d2.Millenium;
            uint leodr = d1.Leodr + d2.Leodr;

            if (day >= maxDays)
            {
                day -= maxDays;
                year++;
            }

            if (year >= maxYears)
            {
                year -= maxYears;
                millenium++;
            }

            if (millenium >= maxMilleniums)
            {
                millenium -= maxMilleniums;
                leodr++;
            }

            return new Date(day, year, millenium, leodr);
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/operatorMore/*'/>
        public static bool operator >(Date d1, Date d2)
        {
            if (d1.Leodr == d2.Leodr)
            {
                if (d1.Millenium == d2.Millenium)
                {
                    if (d1.Year == d2.Year)
                    {
                        return d1.Day > d2.Day;
                    }
                    else
                    {
                        return d1.Year > d2.Year;
                    }
                }
                else
                {
                    return d1.Millenium > d2.Millenium;
                }
            }
            else
            {
                return d1.Leodr > d2.Leodr;
            }
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/operatorLess/*'/>
        public static bool operator <(Date d1, Date d2)
        {
            if (d1.Leodr == d2.Leodr)
            {
                if (d1.Millenium == d2.Millenium)
                {
                    if (d1.Year == d2.Year)
                    {
                        return d1.Day < d2.Day;
                    }
                    else
                    {
                        return d1.Year < d2.Year;
                    }
                }
                else
                {
                    return d1.Millenium < d2.Millenium;
                }
            }
            else
            {
                return d1.Leodr < d2.Leodr;
            }
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/operatorEquality/*'/>
        public static bool operator ==(Date d1, Date d2)
        {
            return
                d1.Leodr == d2.Leodr &&
                d1.Millenium == d2.Millenium &&
                d1.Year == d2.Year &&
                d1.Day == d2.Day;
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="Date"]/operatorInequality/*'/>
        public static bool operator !=(Date d1, Date d2)
        {
            return !(
                d1.Leodr == d2.Leodr &&
                d1.Millenium == d2.Millenium &&
                d1.Year == d2.Year &&
                d1.Day == d2.Day
                );
        }
    }
}
