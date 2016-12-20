using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem20
{
    class Program
    {
        static List<Range> bl = new List<Range>();
        static void Main(string[] args)
        {
            long part1 = -1;
            long part2 = 0;
            long maxGlobal = 4294967295;
            string[] raw = File.ReadAllLines("data.txt");
            

            //ingest ranges and consolidate overlapping ranges

            foreach (var line in raw)
            {
                var r = line.Split('-');
                var min = long.Parse(r[0]);
                var max = long.Parse(r[1]);
                AddRangeToBlacklist(new Range(min, max));
            }

            //re-add all ranges again
            var copy = bl.ToList();
            foreach (var r in copy)
            {
                AddRangeToBlacklist(r);
            }

            for (int i = 0; i < bl.Count - 1; i++)
            {
                Console.WriteLine("{0}-{1} _ {2}-{3} diff: {4}", bl[i].Min, bl[i].Max, bl[i + 1].Min, bl[i + 1].Max, bl[i + 1].Min - bl[i].Max - 1);
                part2 += (bl[i + 1].Min - bl[i].Max - 1);
                if (part1 < 0 && !bl[i + 1].Contains(bl[i].Max + 1)) part1 = bl[i].Max + 1;
            }
            //last one 
            part2 += (maxGlobal - bl[bl.Count - 1].Max);
            Console.WriteLine("Part 1: " + part1);
            Console.WriteLine("Part 2: " + part2);
            Console.ReadLine();
        }

        private static void AddRangeToBlacklist(Range r)
        {
            bl.Remove(r);
            int index = bl.FindIndex(b => b.Contains(r.Min));
            int index2 = bl.FindIndex(b => b.Contains(r.Max));
            if (index == index2 && index != -1) { }//no op
            else if (index > -1) bl[index].Max = r.Max;
            else if (index2 > -1) bl[index2].Min = r.Min;
            else bl.Add(r); 
            bl.Sort(new MinComparer());
        }
    }
}
