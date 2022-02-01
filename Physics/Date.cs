using System;

namespace Physics
{
    public class Date
    {
        private static uint maxDays = 365;
        private static uint maxYears = 1000;
        private static uint maxMilleniums = 1000;

        private uint day;
        private uint year;
        private uint millenium;
        private uint leodr;

        public Date(uint day = 0, uint year = 0, uint millenium = 0, uint leodr = 0)
        {
            if (day > maxDays || year > maxYears || millenium > maxMilleniums)
                throw new Exception($"Неверные значения. Должно быть:\n day < {maxDays}\n year < {maxYears}\n millenium < {maxMilleniums}");

            this.day = day;
            this.year = year;
            this.millenium = millenium;
            this.leodr = leodr;
        }

        public Date NextDay()
        {
            uint day = this.day;
            uint year = this.year;
            uint millenium = this.millenium;
            uint leodr = this.leodr;

            day++;

            if (day == maxDays)
            {
                day = 0;
                year++;
            }

            if (year == maxYears)
            {
                year = 0;
                millenium++;
            }

            if (millenium == maxMilleniums)
            {
                millenium = 0;
                leodr++;
            }

            return new Date(day, year, millenium, leodr);
        }

        public override string ToString()
        {
            return leodr.ToString("00000000") + millenium.ToString("000") + year.ToString("000") + day.ToString("000");
        }

        public static Date operator +(Date d1, Date d2)
        {
            uint day = d1.day + d2.day;
            uint year = d1.year + d2.year;
            uint millenium = d1.millenium + d2.millenium;
            uint leodr = d1.leodr + d2.leodr;

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

        public static bool operator >(Date d1, Date d2)
        {
            if (d1.leodr == d2.leodr)
            {
                if (d1.millenium == d2.millenium)
                {
                    if (d1.year == d2.year)
                    {
                        return d1.day > d2.day;
                    }
                    else
                    {
                        return d1.year > d2.year;
                    }
                }
                else
                {
                    return d1.millenium > d2.millenium;
                }
            }
            else
            {
                return d1.leodr > d2.leodr;
            }
        }

        public static bool operator <(Date d1, Date d2)
        {
            if (d1.leodr == d2.leodr)
            {
                if (d1.millenium == d2.millenium)
                {
                    if (d1.year == d2.year)
                    {
                        return d1.day < d2.day;
                    }
                    else
                    {
                        return d1.year < d2.year;
                    }
                }
                else
                {
                    return d1.millenium < d2.millenium;
                }
            }
            else
            {
                return d1.leodr < d2.leodr;
            }
        }

        public static bool operator ==(Date d1, Date d2)
        {
            return
                d1.leodr == d2.leodr &&
                d1.millenium == d2.millenium &&
                d1.year == d2.year &&
                d1.day == d2.day;
        }

        public static bool operator !=(Date d1, Date d2)
        {
            return !(
                d1.leodr == d2.leodr &&
                d1.millenium == d2.millenium &&
                d1.year == d2.year &&
                d1.day == d2.day
                );
        }
    }
}
