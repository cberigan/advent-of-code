using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Problem14
{
    class Program
    {
        static string salt = "zpqevtbw";
        static string saltTest = "abc";
        static MD5 md5 = MD5.Create();
        static FixedQueue<string> prevHashes = new FixedQueue<string>(1000);
        static HashSet<Data> found = new HashSet<Data>();
        static bool stretch = true;
        static void Main(string[] args)
        {
            Regex quint = new Regex("([a-zA-Z0-9])\\1\\1\\1\\1");
            int i = 0;
            while(found.Count < 64)
            {
                var val = salt + i.ToString();
                var hash = GetHash(val);
                hash = stretch ? StretchHash(hash) : hash;
                Match quintMatch = quint.Match(hash);
                if (quintMatch.Success)
                {
                    var t = quintMatch.Value.Substring(0, 3);
                    var subHash = prevHashes.ToArray();
                    Console.WriteLine("quint found: " + i + " value: " + quintMatch.Value + " hash: " + hash);
                    for (int j= 0; j < subHash.Length; j++)
                    {
                        string hashTest = subHash[j];
                        
                        //first trip match should match the index of intended trip
                        if (FindFirstTrip(hashTest).Equals(t))
                        {
                            var d = new Data();
                            d.Hash = hashTest;
                            d.Quint = quintMatch.Value;
                            d.Trip = t;
                            d.IndexFound = i - (subHash.Length - j);
                            found.Add(d);
                            Console.WriteLine("key found: " + hashTest + " index: " + d.IndexFound + " keys: " + found.Count);
                        }
                    }
                    
                }
                prevHashes.Enqueue(hash);
                i++;
            }
            var keysFound = found.OrderBy(x => x.IndexFound).ToList().OrderBy(x => x.IndexFound).ToList();
            Console.WriteLine(keysFound[63].IndexFound);
            Console.ReadLine();
        }

        private static string StretchHash(string hash)
        {
            var temp = hash;
            for (int i = 0; i < 2016; i++)
            {
                temp = GetHash(temp);
            }
            return temp;
        }

        static string FindFirstTrip(string hash)
        {
            for (int i = 0; i < hash.Length - 2; i++)
            {
                if(hash[i] == hash[i+1] && hash[i+1] == hash[i + 2])
                {
                    return hash.Substring(i, 3);
                }
            }
            return "";
        }

        static string GetHash(string val)
        {
            var temp = md5.ComputeHash(Encoding.ASCII.GetBytes(val));
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < temp.Length; j++)
            {
                sb.Append(temp[j].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
