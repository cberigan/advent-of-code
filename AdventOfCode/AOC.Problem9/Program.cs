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
            long method1 = Process(raw, 0, false);
            long method2 = Process(raw, 0, true);

            Console.WriteLine(method1);
            Console.WriteLine(method2);
            Console.ReadLine();
        }

        private static long Process(string raw, long count, bool recurse)
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
                    count += ProcessCompresion(raw.Substring(paran + 2, numChars), numChars, repeat, recurse);
                    i = paran + 1 + numChars;
                }
                else
                {
                    count++;
                }
            }
            return count;
        }

        private static long ProcessCompresion(string raw, int chars, int repeat, bool recurse)
        {
            string sub = raw.Substring(0, chars);
            long temp = recurse ? Process(sub, 0, recurse) : sub.Length;
            return temp * repeat;
        }
    }
}
