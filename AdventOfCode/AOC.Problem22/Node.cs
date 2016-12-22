using System;

namespace AOC.Problem22
{
    internal class Node : ICloneable
    {
        public int Avail { get; set; }
        public int Size { get; set; }
        public int Used { get; set; }
        public int Number { get; private set; }
        public Point Location { get; private set; }

        public Node(int v, Point loc, int size, int used, int avail)
        {
            this.Number = v;
            this.Location = loc;
            this.Size = size;
            this.Used = used;
            this.Avail = avail;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Node;
            if (other == null) return false;
            return Number == other.Number && Avail == other.Avail && Size == other.Size;
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode() ^ Avail.GetHashCode() ^ Size.GetHashCode();
        }

        internal bool CanTransferData(Node other)
        {
            return Used <= other.Avail;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        internal int RemoveData()
        {
            var temp = Used;
            Used = 0;
            Avail = Size;
            return temp;
        }

        internal void AddData(int data)
        {
            Used += data;
            Avail = Size - Used;
        }
    }
}