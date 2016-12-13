using System;
using System.Collections.Generic;

namespace AOC.Problem11
{
    internal class PairComparer : IComparer<Pair>
    {
        public int Compare(Pair x, Pair y)
        {
            int x1 = x.GenFloor;
            int y1 = y.GenFloor;
            int x2 = x.MicFloor;
            int y2 = y.MicFloor;
            if (x1 < y1) return -1;
            else if (y1 > x1) return 1;
            else
            {
                if (x2 < y2) return -1;
                else if (y2 > x2) return 1;
                else return 0;
            }
        }
    }
}