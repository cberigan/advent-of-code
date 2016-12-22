using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem22
{
    class Program
    {
        static List<Node> nodes = new List<Node>();
        static int pairs = 0;
        static void Main(string[] args)
        {
            string[] raw = File.ReadAllLines("data.txt");

            for (int i = 2; i < raw.Length; i++)
            {
                var data = raw[i].Split(' ').Where(z=>z.Length > 0).ToArray();
                var node = data[0].Split('/')[3];
                var nel = node.Split('-');
                var x = int.Parse(nel[1].Remove(0, 1));
                var y = int.Parse(nel[2].Remove(0, 1));
                var size = int.Parse(data[1].Remove(data[1].Length - 1, 1));
                var used = int.Parse(data[2].Remove(data[2].Length - 1, 1));
                var avail = int.Parse(data[3].Remove(data[3].Length - 1, 1));
                var use = int.Parse(data[4].Remove(data[4].Length - 1, 1));
                nodes.Add(new Node(i - 1, new Point(x,y), size, used, avail, use));
            }
            for (int i = 0; i <nodes.Count; i++)
            {
                Node n1 = nodes[i];
                for (int j = i+1; j < nodes.Count; j++)
                {
                    Node n2 = nodes[j];

                    if(n1.Used > 0 && n1.Used <= n2.Avail)
                    {
                        pairs++;
                    }
                    else if (n2.Used > 0 && n2.Used <= n1.Avail)
                    {
                        pairs++;
                    }
                }
            }
            Console.WriteLine("Part 1: " + pairs);
            AStar(new State(nodes,))
            Console.ReadLine();
        }

        static void AStar(State intial)
        {
            Console.WriteLine("Finding best path using A*...");
            Space start = new Space(1, 1);
            Space goal = new Space(31, 39);
            HashSet<Space> closed = new HashSet<Space>();
            C5.IntervalHeap<Space> open = new C5.IntervalHeap<Space>(new HeuristicComparer(goal));
            open.Add(start);

            while (open.Count > 0)
            {
                var current = open.DeleteMax();
                if (current.Equals(goal))
                {
                    goal = current;
                    break;
                }
                closed.Add(current);
                List<Space> points = current.GetPointsAround();
                foreach (var p in points)
                {
                    if (closed.Contains(p)) continue;
                    else if (p.IsWall) closed.Add(p);
                    else open.Add(p);
                }
            }

            Console.WriteLine("Steps to goal: {0}", goal.GetStepsFromStart());
        }
    }
}
