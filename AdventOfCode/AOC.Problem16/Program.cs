using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem16
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Part 1: " + Process(new bool[] { true, true, true, false, false, false, true, false, true, true, true, true, true, false, true, false, false }, 272));
            Console.WriteLine("Part 2: " + Process(new bool[] { true, true, true, false, false, false, true, false, true, true, true, true, true, false, true, false, false }, 35651584));
            Console.ReadLine();
        }
        static string Process(bool[] input, int length)
        {
            bool[] result = input;
            while (result.Length <= length)
            {
                var b = (bool[])result.Clone();
                Array.Reverse(b);

                for (int i = 0; i < b.Length; i++)
                {
                    b[i] = !b[i];
                }

                var r = new bool[result.Length * 2 + 1];
                result.CopyTo(r, 0);
                (new bool[] { false }).CopyTo(r, result.Length);
                b.CopyTo(r, result.Length+1);
                result = r;
            }
            var arr = new bool[length];
            Array.Copy(result, arr, arr.Length);
            return Checksum(arr);
        }
        static string Checksum(bool[] disk)
        {
            bool[] cs = new bool[(int)(disk.Length / 2.0)];
            for (int i = 0; i < disk.Length - 1; i += 2)
            {
                if (disk[i] == disk[i + 1]) cs[i / 2] = true;
                else cs[i / 2] = false;
            }
            return cs.Length % 2 == 0 ? Checksum(cs) : BoolTostring(cs);
        }

        public static string BoolTostring(bool[] arr)
        {
            string r = "";
            foreach (var item in arr)
            {
                if (item) r = r + "1";
                else r = r+ "0";
            }
            return r;
        }
    }
}
