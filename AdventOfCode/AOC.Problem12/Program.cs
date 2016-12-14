using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem12
{
    class Program
    {
        static Dictionary<string, int> reg = new Dictionary<string, int>() {
            { "a",0 },
            { "b",0 },
            { "c",1 },
            { "d",0 }
        };

        static void Main(string[] args)
        {
            string[] raw = File.ReadAllLines("data.txt");
            for (int i = 0; i < raw.Length; i++)
            {
                var command = raw[i];
                var tok = command.Split(' ');
                var op = tok[0];
                switch (op)
                {
                    
                    case "cpy":
                        Copy(tok[1], tok[2]);
                        break;
                    case "inc":
                        Increment(tok[1], 1);
                        
                        break;
                    case "dec":
                        Increment(tok[1], -1);
                        break;
                    case "jnz":
                        i += Jump(tok[1], tok[2]);
                        break;
                }
            }

            foreach (var r in reg)
            {
                Console.WriteLine("{0}: {1}", r.Key, r.Value);
            }
            Console.ReadLine();
        }

        private static int Jump(string src, string jump)
        {
            int val = 0;
            if (!int.TryParse(src, out val))
            {
                val = reg[src];
            }
            if (val != 0) return int.Parse(jump)- 1;
            else return 0;
        }

        private static void Increment(string dest, int val)
        {
            reg[dest]+= val;
        }

        private static void Copy(string p1, string dest)
        {
            int val = 0;
            if (int.TryParse(p1, out val))
            {
                reg[dest] = val;
            }
            else
            {
                reg[dest] = reg[p1];
            }
        }
    }
}
