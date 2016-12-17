using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem17
{
    class Program
    {
        

        static void Main(string[] args)
        {
            
            Console.WriteLine(PartOne().GetPath());
            Console.WriteLine(PartTwo());
            Console.ReadLine();
        }

        static Room PartOne()
        {
            HashSet<Room> explored = new HashSet<Room>();
            Queue<Room> toExplore = new Queue<Room>();

            toExplore.Enqueue(new Room(0, 0, "veumntbg"));

            while (true)
            {
                if (toExplore.Count == 0) return null;
                else
                {
                    Room currentState = toExplore.Dequeue();
                    explored.Add(currentState);
                    var nextStates = currentState.GenerateNextStates();
                    foreach (var s in nextStates)
                    {
                        if (!explored.Contains(s) && !toExplore.Contains(s))
                        {
                            if (s.IsGoal()) return s;
                            else toExplore.Enqueue(s);
                        }
                    }
                }
            }
        }

        static int PartTwo()
        {
            List<Room> rooms = new List<Room>();
            HashSet<Room> explored = new HashSet<Room>();
            Queue<Room> toExplore = new Queue<Room>();

            toExplore.Enqueue(new Room(0, 0, "veumntbg"));

            while (true)
            {
                if (toExplore.Count == 0) break;
                else
                {
                    Room currentState = toExplore.Dequeue();
                    explored.Add(currentState);
                    var nextStates = currentState.GenerateNextStates();
                    foreach (var s in nextStates)
                    {
                        if (!explored.Contains(s) && !toExplore.Contains(s))
                        {
                            if (s.IsGoal()) rooms.Add(s);
                            else toExplore.Enqueue(s);
                        }
                    }
                }
            }
            return rooms.Select(r => r.GetPath().Length).Max();
        }
    }
}
