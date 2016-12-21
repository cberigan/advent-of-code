using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem21
{
    class Program
    {
        static Dictionary<int, int> reverseMap = new Dictionary<int, int>();
        static void Main(string[] args)
        {

            reverseMap.Add(1,-1);
            reverseMap.Add(3, -2);
            reverseMap.Add(5, -3);
            reverseMap.Add(7, 4);
            reverseMap.Add(2, -6);
            reverseMap.Add(4, -7);
            reverseMap.Add(6, -8);
            reverseMap.Add(0, -9);

            string testRev = "gbhafcde"; //from part1 answer
            string pw1 = "abcdefgh";
            string pw2 = "fbgdceah";
            string[] raw = File.ReadAllLines("data.txt");

            Console.WriteLine("Part 1: {0}", ProcessCommands(pw1, raw, false));
            Console.WriteLine("Part 2: {0}", ProcessCommands(pw2, raw.Reverse(), true));
            Console.WriteLine("TRev 2: {0}", ProcessCommands(testRev, File.ReadAllLines("data.txt").Reverse(), true));
            Console.ReadLine();
        }

        private static string ProcessCommands(string pw, IEnumerable<string> raw, bool reverse)
        {
            foreach (var cmd in raw)
            {
                var tok = cmd.Split(' ');
                switch (tok[0])
                {

                    case "swap":
                        if (tok[1] == "position") pw = SwapPositions(pw, int.Parse(tok[2]), int.Parse(tok[5]));
                        else pw = SwapLetters(pw, tok[2][0], tok[5][0]);
                        break;
                    case "rotate":
                        if (tok[1] == "based")
                        {
                            if (reverse) pw = Rotate(pw, reverseMap[pw.IndexOf(tok[6][0])]);
                            else pw = RotateBasedOnChar(pw, tok[6][0]);
                        }
                        else
                        {
                            if (tok[1] == "left")
                            {
                                if(reverse) pw = Rotate(pw, int.Parse(tok[2]));
                                else pw = Rotate(pw, int.Parse(tok[2]) * -1);
                            }
                            else
                            {
                                if (reverse) pw = Rotate(pw, int.Parse(tok[2]) * -1);
                                else pw = Rotate(pw, int.Parse(tok[2]));
                            }
                        }
                        break;
                    case "reverse":
                        pw = Reverse(pw, int.Parse(tok[2]), int.Parse(tok[4]));
                        break;
                    case "move":
                        if(reverse) pw = Move(pw, int.Parse(tok[5]), int.Parse(tok[2]));
                        else pw = Move(pw, int.Parse(tok[2]), int.Parse(tok[5]));
                        break;
                    default:
                        break;
                }
            }
            return pw;
        }

        private static string RotateBasedOnChar(string password, char v)
        {
            var index = password.IndexOf(v);
            index = index >= 4 ? index + 1 : index;
            index++;
            return Rotate(password, index);
        }

        static string SwapLetters(string pw, char x, char y)
        {
            return pw.Replace(x, '_').Replace(y, '-').Replace('_', y).Replace('-', x);
        }

        static string SwapPositions(string pw, int a,int b)
        {
            char[] arr = pw.ToArray();
            var temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;

            return new string(arr);
        }
        static string Rotate(string pw, int steps)
        {
            var arr = pw.ToCharArray();
            arr.Rotate(steps);
            return new string(arr);
        }

        static string Reverse(string pw, int x, int y)
        {
            return pw.Substring(0, x) + new string(pw.Substring(x, y - x + 1).Reverse().ToArray()) + pw.Substring(y+1, pw.Length - y-1);
        }

        static string Move(string pw, int x, int y)
        {
            char x1 = pw[x];
            return pw.Remove(x, 1).Insert(y, x1.ToString());
        }
    }
}
