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

            List<Room> rooms = new List<Room>();
            HashSet<Room> explored = new HashSet<Room>();
            Queue<Room> toExplore = new Queue<Room>();

            toExplore.Enqueue(new Room(0, 0, "veumntbg"));

            while (toExplore.Count > 0)
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
            Console.WriteLine("Part 1: " + rooms[0].GetPath());
            Console.WriteLine("Part 2: " + rooms.Select(r => r.GetPath().Length).Max());
            Console.ReadLine();
        }
    }
}
