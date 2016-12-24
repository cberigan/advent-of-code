using System.Collections.Generic;

namespace AOC.Problem24
{
    internal class Place
    {
        public Point Loc { get; private set; }
        public char Name { get; private set; }
        public Dictionary<Place, int> Connections { get; private set; }
        public Place(Point loc, char name)
        {
            Loc = loc;
            Name = name;
            Connections = new Dictionary<Place, int>();
        }

        public void AddConnection(Place loc, int steps)
        {
            Connections.Add(loc, steps);
        }
    }
}