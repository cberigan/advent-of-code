using System;
using System.Collections.Generic;

namespace AOC.Problem13
{
    internal class HeuristicComparer : IComparer<Space>
    {
        private Space goal;

        public HeuristicComparer(Space goal)
        {
            this.goal = goal;
        }

        /// <summary>
        /// lowest heuristic should be considered higher
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Space x, Space y)
        {
            int hx = x.GetDistance(goal);
            int hy = y.GetDistance(goal);

            if (hx > hy) return -1;
            else if (hy > hx) return 1;
            else return 0;
        }
    }
}