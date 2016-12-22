﻿using System;
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
        static void Main(string[] args)
        {
            nodes = State.LoadRawNodes("data.txt");
            
            Point dataLocation = nodes.Select(n => n.Location).Where(n => n.Y == 0).OrderBy(n => n.X).Last();
            Point emptyLocation = nodes.First(n => n.Used == 0).Location;
            State initialState = new State(nodes, dataLocation, emptyLocation);
            int pairs = initialState.CountViablePairs();
            
            Console.WriteLine("Part 1: " + pairs);

            Console.WriteLine(initialState.ToString());
            AStar(initialState);
            Console.ReadLine();
        }

        static State AStar(State initial)
        {
            Console.WriteLine("Finding best path using A*...");
            HashSet<State> closed = new HashSet<State>();
            C5.IntervalHeap<State> open = new C5.IntervalHeap<State>(new HeuristicComparer(new Point(0,0)));
            open.Add(initial);
            State goal = null;
            while (open.Count > 0)
            {
                var current = open.DeleteMax();
                if (current.IsGoal())
                {
                    goal = current;
                    break;
                }
                closed.Add(current);
                List<State> nextStates = current.GetSuccessors();
                foreach (var next in nextStates)
                {
                    if (closed.Contains(next)) continue;
                    else open.Add(next);
                }
            }

            Console.WriteLine("Steps to goal: {0}", goal.GetStepsFromStart());
            return goal;
        }
    }
}
