using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem24
{
    class Program
    {
        static char[,] grid;

        static void Main(string[] args)
        {
            string[] raw = File.ReadAllLines("data.txt");

            grid = new char[raw.Length, raw[0].Length];

            
            HashSet<Place> places = new HashSet<Place>();
            
            //load grid and places
            for (int i = 0; i < raw.Length; i++)
            {
                for (int j = 0; j < raw[i].Length; j++)
                {
                    if (char.IsNumber(raw[i][j])) places.Add(new Place(new Point(i, j), raw[i][j]));
                    grid[i, j] = raw[i][j];
                }
            }


            //get all unique pairs of places, find shortest path using BFS and add connections to each place.
            var listOfPlaces = places.ToList();
            for (int i = 0; i < listOfPlaces.Count; i++)
            {
                var start = listOfPlaces[i];
                for (int j = i + 1; j < listOfPlaces.Count; j++)
                {
                    var target = listOfPlaces[j];
                    int shortestPath = BFS(start.Loc, target.Loc);
                    foreach (var p in places)
                    {
                        if (p.Equals(start)) p.AddConnection(target, shortestPath);
                        else if (p.Equals(target)) p.AddConnection(start, shortestPath);
                    }
                }
            }

            //find all permutations of places that begin with starting point 0
            var paths = places.Permute().Where(p=> p.ElementAt(0).Name.Equals('0')).ToList();
            int part1Shortest = int.MaxValue;
            int part2Shortest = int.MaxValue;

            foreach (var p in paths)
            {
                var path = p.ToList();

                int part1 = 0;
                int part2 = 0;
                for (int i = 0; i < path.Count - 1; i++)
                {
                    part1 += path[i].Connections[path[i + 1]];
                }

                path.Add(path[0]); // add route back for part 2

                for (int i = 0; i < path.Count - 1; i++)
                {
                    part2 += path[i].Connections[path[i + 1]];
                }
                if (part1 < part1Shortest) part1Shortest = part1;
                if (part2 < part2Shortest) part2Shortest = part2;
            }
            //generate all different combos of places starting at zero
            Console.WriteLine("Part 1: " + part1Shortest);
            Console.WriteLine("Part 2: " + part2Shortest);
            Console.ReadLine();
        }

        static int BFS(Point start, Point goal)
        {
            Console.WriteLine("Finding shortest path bewteen {0} and {1} using BFS...", start, goal);
            HashSet<Point> closed = new HashSet<Point>();
            Queue<Point> open = new Queue<Point>();
            open.Enqueue(start);
            Point found = null;
            while (open.Count > 0)
            {
                var current = open.Dequeue();
                if (current.Equals(goal))
                {
                    found = current;
                    break;
                }
                closed.Add(current);
                List<Point> points = current.GetPointsAround();
                foreach (var p in points)
                {
                    if (closed.Contains(p)) continue;
                    else if (IsWall(p)) closed.Add(p);
                    else {
                        closed.Add(p);
                        open.Enqueue(p);
                    }
                }
            }
            return found.GetStepsFromStart();
        }

        static bool IsWall(Point p)
        {
            return grid[p.X, p.Y] == '#';
        }
    }
}
