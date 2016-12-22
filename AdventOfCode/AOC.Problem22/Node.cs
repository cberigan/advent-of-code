namespace AOC.Problem22
{
    internal class Node
    {
        public int Avail { get; set; }
        public int Size { get; set; }
        public int Used { get; set; }
        public int Use { get; set; }
        public int Number { get; private set; }
        public Point Location { get; private set; }

        public Node(int v, Point loc, int size, int used, int avail, int use)
        {
            this.Number = v;
            this.Location = loc;
            this.Size = size;
            this.Used = used;
            this.Avail = avail;
        }
    }
}