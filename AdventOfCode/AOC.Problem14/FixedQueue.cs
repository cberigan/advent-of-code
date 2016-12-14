using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem14
{
    public class FixedQueue<T> : Queue<T>
    {
        public int Size { get; private set; }

        public FixedQueue(int size)
        {
            Size = size;
        }

        public new void Enqueue(T obj)
        {
            base.Enqueue(obj);
            while (base.Count > Size)
            {
                base.Dequeue();
            }
        }
    }
}
