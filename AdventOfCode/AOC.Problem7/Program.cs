using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] raw = File.ReadAllLines("data.txt");
            int tsls = 0;
            int ssls = 0;
            foreach (var line in raw)
            {
                IPData data = ExtractIPData(line);

                if (IsTLS(data))
                {
                    tsls++;
                }
                if (IsSSL(data))
                {
                    ssls++;
                }
            }


            Console.WriteLine(tsls);
            Console.WriteLine(ssls);
            Console.ReadLine();

        }

        private static IPData ExtractIPData(string line)
        {
            IPData data = new IPData();
            int index = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '[')
                {
                    data.Supernet.Add(line.Substring(index, i - index));
                    index = i + 1;
                }
                else if (line[i] == ']')
                {
                    data.Hypernet.Add(line.Substring(index, i - index));
                    index = i + 1;
                }
                else if (i == line.Length - 1)
                {
                    data.Supernet.Add(line.Substring(index, i - index + 1));
                }
            }
            return data;
        }

        private static bool IsTLS(IPData data)
        {
            return  data.Supernet.Select(d => TestFOrABBA(d)).Contains(true) && 
                    !data.Hypernet.Select(d => TestFOrABBA(d)).Contains(true);
        }

        private static bool IsSSL(IPData data)
        {
            return CheckForBAB(data, ExtractABA(data));
        }

        private static List<string> ExtractABA(IPData ip)
        {
            List<string> abas = new List<string>();
            foreach (var data in ip.Supernet)
            { 
                for (int i = 0; i < data.Length - 2; i++)
                {
                    var aba = data.Substring(i, 3);
                    if (data[i] == data[i + 2] && aba.Distinct().Count() != 1)
                    {
                        abas.Add(aba);
                    }
                }
            }
            return abas;
        }

        private static bool TestFOrABBA(string data)
        {
            bool results = false;
            for (int i = 0; i < data.Length - 3; i++)
            {
                var chars = data.Substring(i, 4);
                if (data[i] == data[i + 3] && data[i + 1] == data[i + 2] && chars.Distinct().Count() != 1)
                {
                    results = true;
                }
            }
            return results;
        }

        private static bool CheckForBAB(IPData ip, List<string> abas)
        {
            bool results = false;
            List<string> babs = abas.Select(s => new string(new char[] { s[1], s[0], s[1] })).ToList();
            foreach (var data in ip.Hypernet)
            {
                for (int i = 0; i < data.Length - 2; i++)
                {
                    var chars = data.Substring(i, 3);
                    if (data[i] == data[i + 2] && 
                        chars.Distinct().Count() != 1 && 
                        babs.Contains(chars))
                    {
                        results = true;
                    }
                }
            }
            return results;
        }
    }
}
