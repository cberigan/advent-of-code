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
        private static MD5 md5 = MD5.Create();

        static void Main(string[] args)
        {
            string code = "ffykfhsq";
            char[] result = "--------".ToCharArray();
            
            for (int i = 0; result.Contains('-'); i++)
            {
                string newCode = code + i;
                byte[] inputBytes = Encoding.ASCII.GetBytes(newCode);
                byte[] temp = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                
                for (int j = 0; j < temp.Length; j++)
                {
                    sb.Append(temp[j].ToString("X2"));
                }
                
                string hash = sb.ToString();
                
                if (hash.StartsWith("00000"))
                {
                    int pos = -1;
                    if (int.TryParse(hash[5].ToString(), out pos)){

                        if (pos < 8 && result[pos].Equals('-'))
                        {
                            char value = hash[6];
                            result[pos] = value;
                        }
                    }
                }
            }
            Console.WriteLine(new string(result));
            Console.ReadLine();
        }
    }
}
