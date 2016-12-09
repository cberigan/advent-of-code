using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem9
{
    class Program
    {
        static void Main(string[] args)
        {
            string raw = File.ReadAllText("data.txt");
            string result = "";

            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i].Equals('(')) {
                    string uncompressed = "";
                    i = ProcessCompresion(raw,i, out uncompressed);
                    result += uncompressed;
                }
                else
                {
                    result += raw[i];
                }
            }

            Console.WriteLine(result.Length);
            Console.WriteLine("ABBBBBC"); 
            Console.ReadLine();
        }

        private static int ProcessCompresion(string raw, int i, out string result)
        {
            var paran = raw.IndexOf(')', i) - 1;
            var sub = raw.Substring(i + 1, paran - i);
            var p = sub.Split('x');
            var numChars = int.Parse(p[0]);
            var repeat = int.Parse(p[1]);
            

            var value = raw.Substring(paran + 2, numChars);
            result = string.Concat(Enumerable.Repeat(value, repeat));
            return paran + 1 + value.Length;
        }
    }
}
