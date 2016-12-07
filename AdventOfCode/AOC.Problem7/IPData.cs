using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem7
{
    class IPData
    {
        public List<string> Supernet { get; private set; }
        public List<string> Hypernet { get; private set; }

        public IPData()
        {
            Supernet = new List<string>();
            Hypernet = new List<string>();
        }
    }
}
