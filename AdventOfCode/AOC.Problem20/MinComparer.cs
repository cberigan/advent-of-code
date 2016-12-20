using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem20
{
    class MinComparer : IComparer<Range>
    {
        public int Compare(Range x, Range y)
        {
            if (x.Min < y.Min) return -1;
            else if (x.Min == y.Min) return 0;
            else return 1;
        }
    }
}
