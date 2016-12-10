using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Problem10
{
    class BotEngine
    {
        Dictionary<int,Bot> bots = new Dictionary<int, Bot>();
        Dictionary<int, int> output = new Dictionary<int, int>();
        List<SendCommand> commands = new List<SendCommand>();

        Bot part1 = new Bot(-1);

        public BotEngine(string[] rawCommands)
        {
            foreach (var l in rawCommands)
            {
                if (l.StartsWith("value"))
                {
                    var matches = Regex.Matches(l, @"\d+");
                    int value = int.Parse(matches[0].Value);
                    int id = int.Parse(matches[1].Value);

                    if (bots.Keys.Contains(id)) bots[id].AssignValue(value);
                    else bots.Add(id, new Bot(id, value));
                }
                else
                {
                    //generate send commands and create any referenced bins and bots with no values yet
                    var send = new SendCommand();
                    var tokens = l.Split(' ');
                    send.BotID = int.Parse(tokens[1]);
                    send.LowBinType = tokens[5];
                    send.LowID = int.Parse(tokens[6]);
                    send.HighBinType = tokens[10];
                    send.HighID = int.Parse(tokens[11]);
                    
                    //create named bots and output bins
                    CreateBin(send.HighBinType, send.HighID);
                    CreateBin(send.LowBinType, send.LowID);
                    commands.Add(send);

                }
            }
        }

        internal int GetPartOneID()
        {
            return part1.ID;
        }

        internal int GetPartTwoValue()
        {
            return output[0] * output[1] * output[2];
        }

        private void CreateBin(string binType, int id)
        {
            if (binType.Equals("bot") && !bots.ContainsKey(id)) bots.Add(id, new Bot(id));
            else if (!output.ContainsKey(id)) output.Add(id, -1);
        }

        public void RunSimulation()
        {
            while (bots.Values.Count(b => b.CanSend == true) > 0)
            {
                //get bots that can send values
                foreach (var b in bots.Values.Where(b => b.CanSend).ToList())
                {
                    //get bot next command set
                    var s = commands.Where(c => c.BotID == b.ID).First();
                    var values = b.GetValues();
                    AssignValue(s.LowBinType, s.LowID, values.Item1);
                    AssignValue(s.HighBinType, s.HighID, values.Item2);
                }
            }
        }

        private void AssignValue(string type, int to, int val)
        {
            if (type.Equals("bot"))
            {
                Bot b = bots[to];
                b.AssignValue(val);
                CheckForPartOneBot(b);
            }
            else output[to] = val;
        }

        private void CheckForPartOneBot(Bot b)
        {
            if (b.CheckLowValue() == 17 && b.CheckHighValue() == 61)
            {
                part1 = b;
            }
        }
    }
}
