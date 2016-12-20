using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem20
{
    class Range
    {
        public long Min {get; set;}
        public long Max { get; set; }

        public Range(long min, long max)
        {
            Min = min;
            Max = max;
        }

        public bool Contains(long val)
        {
            return Min <= val && Max >= val;
        }
    }
}
