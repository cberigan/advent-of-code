using System.Collections.Generic;

namespace AOC.Problem22
{
    internal class HeuristicComparer : IComparer<State>
    {
        private Point goal;

        public HeuristicComparer(Point goal)
        {
            this.goal = goal;
        }

        /// <summary>
        /// lowest heuristic should be considered higher
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(State x, State y)
        {
            int hx = x.DataLocation.GetDistance(goal);
            int hy = y.DataLocation.GetDistance(goal);
            int hx2 = x.EmptyNode.GetDistance(x.DataLocation);
            int hy2 = y.EmptyNode.GetDistance(y.DataLocation);
            //int hx3 = x.EmptyNode.GetDistance(goal);
            //int hy3 = y.EmptyNode.GetDistance(goal);

            var x_total = hx + hx2;
            var y_total = hy + hy2;
            if (x_total > y_total) return -1;
            else if (y_total > x_total) return 1;
            else return 0;
        }
    }
}

