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
            long result = Process(raw, 0);

            Console.WriteLine(result);
            Console.WriteLine("XABCABCABCABCABCABCY"); 
            Console.ReadLine();
        }

        private static long Process(string raw, long count)
        {

            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i].Equals('('))
                {
                    var paran = raw.IndexOf(')', i) - 1;
                    var sub = raw.Substring(i + 1, paran - i);
                    var p = sub.Split('x');
                    var numChars = int.Parse(p[0]);
                    var repeat = int.Parse(p[1]);
                    count += ProcessCompresion(raw.Substring(paran + 2, numChars), new Op(numChars, repeat));
                    i = paran + 1 + numChars;
                }
                else
                {
                    count++;
                }
            }
            return count;
        }

        private static long ProcessCompresion(string raw, Op op)
        {
            string sub = raw.Substring(0, op.Chars);
            return Process(sub , 0) * op.Repeat;
        }
    }
}
