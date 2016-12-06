using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] raw = File.ReadAllLines("data.txt");
            Dictionary<char, int>[] counts = new Dictionary<char, int>[8];

            char[] r1 = "--------".ToCharArray();
            char[] r2 = "--------".ToCharArray();

            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = new Dictionary<char, int>();
            }

            foreach (var line in raw)
            {
                char[] data = line.ToCharArray();
                for (int i = 0; i < 8; i++)
                {
                    if (counts[i].ContainsKey(data[i])) counts[i][data[i]]++;
                    else counts[i].Add(data[i], 1);
                }
            }

            for (int i = 0; i < counts.Length; i++)
            {
                r1[i] = counts[i].FirstOrDefault(x => x.Value == counts[i].Values.Max()).Key;
                r2[i] = counts[i].FirstOrDefault(x => x.Value == counts[i].Values.Min()).Key;
            }
            
            Console.WriteLine(new string (r1));
            Console.WriteLine(new string(r2));
            Console.ReadLine();
        }
    }
}
