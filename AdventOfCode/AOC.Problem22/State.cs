using System.Collections.Generic;

namespace AOC.Problem22
{
    internal class State
    {
        List<Node> Nodes { get; set; }

        public Point DataLocation { get; private set; }
        public Point EmptyNode { get; private set; }
        //private int[,] gridState { get; set; }
        public State Previous { get; private set; }

        public State(List<Node> nodes, Point data, Point empty)
        {
            EmptyNode = empty;
            DataLocation = data;
            Nodes = nodes;
        }

        public State(List<Node> nodes, Point data, Point empty,State previous) : this(nodes, data,empty)
        {
            Previous = previous;
        }

        public bool IsGoal()
        {
            return DataLocation.X == 0 && DataLocation.Y == 0;
        }

        //private int[,] ToGridState(IEnumerable<Node> nodes)
        //{
        //    int[,] arr = new int[grid_x + 1, grid_y + 1];
        //    foreach (var n in nodes)
        //    {
        //        arr[n.Position.X, n.Position.Y] = n.Use;
        //    }
        //    return arr;
        //}
    }
}