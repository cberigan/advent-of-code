using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem11
{
    class StateRankComparer : IComparer<State>
    {
        //lowest rank is considered highest so do the oposite here 
        public int Compare(State x, State y)
        {
            int rank1 = x.GetRank();
            int rank2 = y.GetRank();
            if (rank1 > rank2) return -1;
            else if (rank2 < rank1) return 1;
            else return 0;
        }
    }
}