using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem18
{
    class Program
    {

        static void Main(string[] args)
        {
            string input = "^^^^......^...^..^....^^^.^^^.^.^^^^^^..^...^^...^^^.^^....^..^^^.^.^^...^.^...^^.^^^.^^^^.^^.^..^.^";
            List<string> rows = new List<string>();
            rows.Add(input);
            while (rows.Count < 400000)
            {
                var pre = rows[rows.Count - 1];
                var cur = "";
                for (int i = 0; i < pre.Length; i++)
                {
                    var left = i - 1 < 0 ? '.' : pre[i - 1];
                    var center = pre[i];
                    var right = i + 1 == pre.Length ? '.' : pre[i + 1];
                    if ((left == '^' && center == '^' && right == '.') ||
                        (right == '^' && center == '^' && left == '.') ||
                        (left == '^' && center == '.' && right == '.') ||
                        (right == '^' && center == '.' && left == '.'))
                        cur += '^';
                    else
                        cur += '.';
                }
                rows.Add(cur);
            }
            ;
            Console.WriteLine(rows.Select(r => r.ToCharArray().Count(x => x.Equals('.'))).Sum());
            Console.ReadLine();
        }
    }
}
