using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC.Problem22
{
    internal class State
    {
        public Dictionary<Point, Node> LocationLookup { get; private set; }
        public HashSet<Node> NodeSet { get; private set; }
        public Point DataLocation { get; private set; }
        public Point EmptyNode { get; private set; }
        //private int[,] gridState { get; set; }
        public State Previous { get; private set; }

        private static int max_x = 0;
        private static int max_y = 0;

        public State(List<Node> nodes, Point data, Point empty)
        {
            EmptyNode = empty;
            DataLocation = data;
            LocationLookup = ConvertLookup(nodes);
            NodeSet = new HashSet<Node>(nodes);
        }

        internal static List<Node> LoadRawNodes(string path)
        {
            string[] raw = File.ReadAllLines(path);
            List<Node> nodes = new List<Node>();
            for (int i = 2; i < raw.Length; i++)
            {
                var data = raw[i].Split(' ').Where(z => z.Length > 0).ToArray();
                var node = data[0].Split('/')[3];
                var nel = node.Split('-');
                var x = int.Parse(nel[1].Remove(0, 1));
                var y = int.Parse(nel[2].Remove(0, 1));
                var size = int.Parse(data[1].Remove(data[1].Length - 1, 1));
                var used = int.Parse(data[2].Remove(data[2].Length - 1, 1));
                var avail = int.Parse(data[3].Remove(data[3].Length - 1, 1));
                var use = int.Parse(data[4].Remove(data[4].Length - 1, 1));
                if (x > max_x) max_x = x;
                if (y > max_y) max_y = y;
                nodes.Add(new Node(i - 1, new Point(x, y), size, used, avail));
            }
            return nodes;
        }

        internal int CountViablePairs()
        {
            int pairs = 0;
            var nodes = LocationLookup.Values.ToList();
            for (int i = 0; i < nodes.Count; i++)
            {
                Node n1 = nodes[i];
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    Node n2 = nodes[j];
                    if (n1.Used > 0 && n1.Used <= n2.Avail)
                    {
                        pairs++;
                    }
                    else if (n2.Used > 0 && n2.Used <= n1.Avail)
                    {
                        pairs++;
                    }
                }
            }
            return pairs;
        }

        public State(List<Node> nodes, Point data, Point empty,State previous) : this(nodes, data,empty)
        {
            Previous = previous;
        }

        private Dictionary<Point,Node> ConvertLookup(List<Node> nodes)
        {
            Dictionary<Point, Node> dict = new Dictionary<Point, Node>();
            foreach (var n in nodes)
            {
                dict.Add(n.Location, n);
            }
            return dict;
        }

        public bool IsGoal()
        {
            return DataLocation.X == 0 && DataLocation.Y == 0;
        }

        internal List<State> GetSuccessors()
        {
            List<State> succ = new List<State>();
            Node empty = LocationLookup[EmptyNode];
            List<Node> nodesAroundEmpty = GetNodesAround(empty.Location);
            foreach (var n in nodesAroundEmpty)
            {
                if (n.CanTransferData(empty))
                {
                    succ.Add(ExcuteTransfer(n,empty));
                }
            }
            //optimization
            return succ;
        }

        public void PrintPath()
        {
            Console.WriteLine("== Path to solution ==");
            State state = this;
            while (state != null)
            {
                Console.WriteLine(state.ToString());
                Console.WriteLine("====================");
                state = state.Previous;
            }
        }
        private State ExcuteTransfer(Node from, Node to)
        {
            Node fromClone = (Node)from.Clone();
            Node toClone = (Node)to.Clone();

            int data = fromClone.RemoveData();
            toClone.AddData(data);
            HashSet<Node> nodes = new HashSet<Node>(LocationLookup.Values);
            nodes.Remove(from);
            nodes.Remove(to);
            nodes.Add(fromClone);
            nodes.Add(toClone);
            var dataLocation = fromClone.Location.Equals(DataLocation) ? toClone.Location : DataLocation;
            return new State(nodes.ToList(), dataLocation, fromClone.Location,this);
        }

        private List<Node> GetNodesAround(Point location)
        {
            List<Point> points = location.GetPointsAround();
            List<Node> around = new List<Node>();

            foreach (var p in points)
            {
                Node node = null;
                if(LocationLookup.TryGetValue(p, out node))
                {
                    around.Add(node);
                }
            }
            return around;
        }

        internal int GetStepsFromStart()
        {
            if (Previous == null) return 0;
            return Previous.GetStepsFromStart() + 1;
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

        public override bool Equals(object obj)
        {
            var other = obj as State;
            if (other == null) return false;
            return DataLocation.Equals(other.DataLocation) && EmptyNode.Equals(other.EmptyNode);
        }

        public override int GetHashCode()
        {
            return DataLocation.GetHashCode() ^ EmptyNode.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            char[,] grid = new char[max_x+1, max_y+1];
            foreach (var n in NodeSet)
            {
                grid[n.Location.X, n.Location.Y] =  n.Location.Equals(new Point(0,0)) ? '*' : n.Location.Equals(DataLocation) ? 'G' : 
                                                    n.Location.Equals(EmptyNode) ? '_' :  
                                                    n.Used > 300 ? '#' : '.';
            }
            int rowLength = grid.GetLength(0);
            int colLength = grid.GetLength(1);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    sb.Append(string.Format("{0} ", grid[i, j]));
                }
                sb.Append(Environment.NewLine + Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}