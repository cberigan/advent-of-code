using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem11
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] raw = File.ReadAllLines("data.txt");

            var part1 = ExtractItems(raw);
            var part2 = part1.ToList();
            part2.Add(new Item("elerium", MaterialType.microchip, 1));
            part2.Add(new Item("elerium", MaterialType.generator, 1));
            part2.Add(new Item("dilithium", MaterialType.microchip, 1));
            part2.Add(new Item("dilithium", MaterialType.generator, 1));

            //initial State
            Console.WriteLine("Running Part 1....");
            Console.WriteLine("Part1 steps: " + FindSolutionPath(new State(1, part1)).GetStepsFromStart());
            Console.WriteLine("Running Part 2....");
            Console.WriteLine("Part2 steps: " + FindSolutionPath(new State(1, part2)).GetStepsFromStart());
            Console.ReadLine();
        }

        private static List<Item> ExtractItems(string[] raw)
        {
            List<Item> items = new List<Item>();
            for (int i = 0; i < raw.Length; i++)
            {
                var itemsRaw = raw[i].Replace(".", String.Empty).Split(new string[] { " a " }, StringSplitOptions.None).Skip(1);
                foreach (var ir in itemsRaw)
                {
                    var tokens = ir.Split(' ');
                    var type = tokens[1].Contains("generator") ? MaterialType.generator : MaterialType.microchip;

                    var material = "";
                    if (type.Equals(MaterialType.microchip)) material = tokens[0].Split('-')[0];
                    else material = tokens[0];

                    var item = new Item(material, type, i + 1);
                    items.Add(item);
                }
            }
            return items;
        }

        private static void PrintSolutionPath(State finalState)
        {
            State currentState = finalState;
            while (currentState.PreviousState != null)
            {
                PrintState(currentState);
                var move = currentState.MoveFromPrevious;
                Console.WriteLine("Elevator Moves: {0}", move.ElevatorDirection.ToString());
                var i2 = move.Items.Item2 == null ? "" : move.Items.Item2.ToString();
                Console.WriteLine("Takes Items: {0} {1}", move.Items.Item1.ToString(), i2);
                currentState = currentState.PreviousState;
            }
            PrintState(currentState);
        }

        private static void PrintState(State state)
        {
            Console.WriteLine("===================");
            Console.WriteLine("Elevator Position: {0}", state.ElevatorPos);
            for (int i = 4; i >= 1; i--)
            {
                var itemsOnFloor = state.Items.Where(it => it.Floor == i);
                Console.WriteLine("Floor {0}: {1}", i, string.Join(", ", itemsOnFloor.Select(s => s.ToString()).ToArray()));
            }
            Console.WriteLine("===================");
        }

        public static State FindSolutionPath(State initialState)
        {
            int lowestRank = int.MaxValue;
            HashSet<State> explored = new HashSet<State>();
            Queue<State> toExplore = new Queue<State>();

            toExplore.Enqueue(initialState);

            while (true)
            {
                if(toExplore.Count == 0) return null;
                else
                {
                    State currentState = toExplore.Dequeue();
                    explored.Add(currentState);
                    var currentRank = currentState.GetRank();
                    if(currentRank < lowestRank)
                    {
                        lowestRank = currentRank;
                        Console.WriteLine("Best rank so far: {0}", lowestRank);
                    }
                    var nextStates = currentState.GenerateNextStates();
                    foreach (var s in nextStates)
                    {
                        if(!explored.Contains(s) && !toExplore.Contains(s))
                        {
                            if (s.IsGoal()) return s;
                            else toExplore.Enqueue(s);
                        }
                    }
                }
            }
        }
    }
}
