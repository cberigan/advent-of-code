using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem9
{
    class Op
    {
        public int Chars { get; set; }
        public int Repeat { get; set; }

        public Op(int c, int r)
        {
            Chars = c;
            Repeat = r;
        }
    }
}
