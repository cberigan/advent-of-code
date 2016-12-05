using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem5
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] result = new char[8];
            for (int k = 0; k < result.Length; k++)
            {
                result[k] = '-';
            }
            string code = "ffykfhsq";
            List<object> l = new List<object>();
            string[] lines = File.ReadAllLines("data.txt");
            int i = 0;
            while (result.Contains('-'))
            {
                string newCode = code + i;
                MD5 md5 = MD5.Create();

                byte[] inputBytes = Encoding.ASCII.GetBytes(newCode);
                byte[] hash = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < hash.Length; j++)
                {
                    sb.Append(hash[j].ToString("X2"));
                }
                string hash2 = sb.ToString();
                if (hash2.StartsWith("00000"))
                {
                    int pos = -1;
                    if (int.TryParse(hash2[5].ToString(), out pos)){

                        if (pos < 8 && result[pos].Equals('-'))
                        {
                            char value = hash2[6];
                            result[pos] = value;
                        }
                    }
                }
                i++;
            }
            Console.WriteLine(new string(result));
            Console.ReadLine();
        }
    }
}
