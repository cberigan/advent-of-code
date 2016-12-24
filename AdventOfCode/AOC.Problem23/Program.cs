using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem23
{
    class Program
    {
        static Dictionary<string, int> reg = new Dictionary<string, int>() {
            { "a",12 },
            { "b",0 },
            { "c",0 },
            { "d",0 }
        };

        static void Main(string[] args)
        {
            string[] raw = File.ReadAllLines("data.txt");
            string[] map = new string[raw.Length];
            //load current instruction mapping
            for (int i = 0; i < raw.Length; i++)
            {
                var command = raw[i];
                var tok = command.Split(' ');
                var op = tok[0];
                map[i] = op;
            }
            
            for (int i = 0; i < raw.Length; i++)
            {
                var command = raw[i];
                var tok = command.Split(' ');
                var op = map[i];
                
                switch (op)
                {

                    case "cpy":
                        Copy(tok[1], tok[2]);
                        break;
                    case "inc":
                        //Multiply(tok[1]);
                        Increment(tok[1], 1);

                        break;
                    case "dec":
                        Increment(tok[1], -1);
                        break;
                    case "jnz":
                        i += Jump(tok[1], tok[2]);
                        break;
                    case "tgl":
                        Toggle(map, raw, i, tok[1]);
                        break;
                }
            }

            foreach (var r in reg)
            {
                Console.WriteLine("{0}: {1}", r.Key, r.Value);
            }
            Console.ReadLine();
        }

        private static void Multiply(string dest)
        {
            if (reg.Keys.Contains(dest))
            {
                reg[dest] = reg[dest] * 2 - 2;

            }
        }

        private static void Toggle(string[] map, string[] raw, int index, string src)
        {
            var jump = reg[src];
            var instruction = mod(jump + index, map.Length);
            var op = map[instruction];
            var args = raw[instruction].Split(' ').Length - 1;
            if(args == 1)
            {
                map[instruction] = op == "inc" ? "dec" : "inc";
            }
            else
            {
                map[instruction] = op == "jnz" ? "cpy" : "jnz";
            }
            
        }

        private static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        private static int Jump(string src, string jump)
        {
            int source = 0;
            if (reg.ContainsKey(src)) source = reg[src];
            else
            {
                if (!int.TryParse(src, out source))
                {
                    source = reg[src];
                }
            }
            
            if (source != 0)
            {
                if (reg.ContainsKey(jump))
                {
                    return reg[jump] - 1;
                }
                else
                {
                    return int.Parse(jump) - 1;
                }
                
            }
            else return 0;
        }

        private static void Increment(string dest, int val)
        {
            if (reg.Keys.Contains(dest))
            {
                reg[dest] += val;
            }
        }

        private static void Copy(string p1, string dest)
        {
            if (reg.Keys.Contains(dest))
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
}
