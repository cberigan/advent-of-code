using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem13
{
    class Program
    {

        static void Main(string[] args)
        {
            AStar();
            BFS();
            Console.ReadLine();
        }

        static void BFS()
        {
            Console.WriteLine("Finding all spaces withing 50 steps...");
            int reachable = 0;
            Space start = new Space(1, 1);
            HashSet<Space> closed = new HashSet<Space>();
            Queue<Space> open = new Queue<Space>();
            open.Enqueue(start);

            while (open.Count > 0)
            {
                
                var current = open.Dequeue();
                reachable++;
                closed.Add(current);
                List<Space> points = current.GetPointsAround();
                foreach (var p in points)
                {
                    if (closed.Contains(p)) continue;
                    else if (p.IsWall) closed.Add(p);
                    else if (p.GetStepsFromStart() <= 50)
                    {
                        closed.Add(p);
                        open.Enqueue(p);
                    }
                }
            }

            Console.WriteLine("Reachable spaces at 50 steps: {0}", reachable);
        }

        static void AStar()
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
