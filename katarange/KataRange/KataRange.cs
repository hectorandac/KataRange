using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace KataRange
{

    public class KataRange
    {
        private string range = "[0,0]";
        private int lower = 0;
        private int upper = 0;

        public KataRange(string range)
        {
            this.range = range;
            CheckLimits();
            int[] values = DelimitValues();
            lower = values[0];
            upper = values[1];
            if (upper < lower)
            {
                throw new FormatException();
            }
        }

        public int[] EndPoints()
        {
            return new int[] { lower, upper };
        }

        public int GetLower()
        {
            return lower;
        }

        public int GetUpper()
        {
            return upper;
        }

        public bool Contains(int[] set)
        {
            foreach (int number in set)
            {
                if(number < lower || number > upper)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Contains(KataRange range)
        {
            if (lower <= range.GetLower() && upper >= range.GetUpper()) return true;
            return false;
        }

        private int[] DelimitValues()
        {
            int[] values = new int[] { 0, 0 };
            char left = range[0];
            char rigth = range[range.Length - 1];
            values[0] = (left == '(' ? values[0] += 1 : values[0]);
            values[1] = (rigth == ')' ? values[1] -= 1 : values[1]);
            range = range.Replace("(", "").Replace("[", "").Replace(")", "").Replace("]", "");
            string[] ranged = range.Trim().Split(',');
            try
            {
                values[0] += Int32.Parse(ranged[0]);
                values[1] += Int32.Parse(ranged[1]);
            }
            catch
            {
                throw new FormatException();
            }

            return values;
        }

        private void CheckLimits()
        {
            char left = range[0];
            char rigth = range[range.Length - 1];
            if( (left != '[' && left != '(') || (rigth != ']' && rigth != ')'))
            {
                throw new FormatException();
            }
        }

        public int[] getAllPoints()
        {
            int[] points = new int[upper - lower +1];
            for(int i = 0; i < points.Length; i++)
            {
                points[i] = lower + i;
            }
            return points;
        }

        public bool Overlaps(KataRange mainRange)
        {
            if (mainRange.Contains(this) || this.Contains(mainRange)) return false;
            if (mainRange.GetLower() < lower && mainRange.GetUpper() < lower)
            {
                return false;
            }

            if (mainRange.GetLower() > upper && mainRange.GetUpper() > upper)
            {
                return false;
            }
            return true;
        }

        public bool IsEqualTo(KataRange secondRange)
        {
            if (this.upper == secondRange.GetUpper() && this.lower == secondRange.GetLower()) return true;
            return false;
        }
    }
}
