using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Problem10
{
    class Program
    {
        private static Dictionary<int, Bot> bots = new Dictionary<int, Bot>();
        private static HashSet<Bot> readyToSend = new HashSet<Bot>();
        
        private static List<SendCommand> sends = new List<SendCommand>();


        static void Main(string[] args)
        {
            string[] raw = File.ReadAllLines("data.txt");
            List<string> assigns = new List<string>();
            BotEngine eng = new BotEngine(raw);
            eng.RunSimulation();
            
            Console.WriteLine("bot num: " + eng.GetPartOneID());
            Console.WriteLine("part two: " + eng.GetPartTwoValue());
            Console.ReadLine();
        }
    }
}
