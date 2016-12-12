using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem11
{
    class Pair
    {
        public string Type { get; private set; }
        public int GenFloor { get; private set; }
        public int MicFloor { get; private set; }

        public Pair(string type, int mic, int gen)
        {
            Type = type;
            GenFloor = gen;
            MicFloor = mic;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Pair;

            if (other == null)
            {
                return false;
            }

            return this.GenFloor.Equals(other.GenFloor) && this.MicFloor.Equals(other.MicFloor);
        }

        public override int GetHashCode()
        {
            return this.GenFloor ^ this.MicFloor;
        }
    }
}
