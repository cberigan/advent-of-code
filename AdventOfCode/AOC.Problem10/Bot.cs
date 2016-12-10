using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem10
{
    public class Bot
    {
        private List<int> values = new List<int>();

        public Bot(int id, int val) : this(id)
        {
            values.Add(val);
        }

        public Bot(int id)
        {
            this.ID = id;
        }
        public int ID { get; set; }
        public bool CanSend { get { return values.Count == 2; } }
        public bool CanReceive { get { return values.Count < 2; } }

        internal void AssignValue(int value)
        {
            if(this.values.Count < 2)
            {
                values.Add(value);
                values.Sort();
            }
            else
            {
                throw new Exception(string.Format("bot {0} has 2 values already", this.ID));
            }
        }

        internal Tuple<int,int> GetValues()
        {
            var low = values[0];
            var high = values[1];
            values.Clear();
            return Tuple.Create<int,int>(low,high);
        }

        internal int CheckHighValue()
        {
            int val = -1;
            if (values.Count == 2) val = values[1];
            return val;
        }

        internal int CheckLowValue()
        {
            int val = -1;
            if (values.Count == 2) val = values[0];
            return val;
        }
    }
}
