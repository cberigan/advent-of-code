using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem15
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] raw = File.ReadAllLines("data.txt");
            List<Queue<int>> part1 = new List<Queue<int>>();
            List<Queue<int>> part2 = new List<Queue<int>>();
            foreach (var line in raw)
            {
                part1.Add(CreateDisc(line));
                part2.Add(CreateDisc(line));
            }
            part2.Add(CreateDisc("Disc #7 has 11 positions; at time=0, it is at position 0."));
            Console.WriteLine("Part 1: " + GetTime(part1));
            Console.WriteLine("Part 2: " + GetTime(part2));
            Console.ReadLine();

        }

        static int GetTime(List<Queue<int>> discs)
        {
            int time = 0;
            int counter = 0;
            while (true)
            {
                time++;
                discs.ForEach(d => d.Enqueue(d.Dequeue()));
                if (discs[counter].Peek() == 1)
                {
                    //engage counting squence
                    counter++;
                    if (counter == discs.Count) break;

                }
                else counter = 0;
            }
            return time - discs.Count;
        } 

        static Queue<int> CreateDisc(string data)
        {
            var tok = data.Split(' ');
            var positions = int.Parse(tok[3]);
            var place = int.Parse(tok[11].Replace('.', ' '));
            var q = new Queue<int>(positions);
            for (int i = positions - 1; i >= 0; i--)
            {
                if (mod(place - 1, positions) == i) q.Enqueue(1);
                else q.Enqueue(0);
            }
            return q;
        }

        static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
