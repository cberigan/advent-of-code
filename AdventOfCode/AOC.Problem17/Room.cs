using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem17
{
    public class Room
    {
        //grid from (0,0),(3,0),(3,-3),(0,-3)
        private int min_x = 0;
        private int max_x = 3;
        private int min_y = -3;
        private int max_y = 0;
        private static MD5 md5 = MD5.Create();
        static HashSet<char> open = new HashSet<char> { 'b', 'c', 'd', 'e', 'f' };

        public int X { get; private set; }
        public int Y { get; private set; }
        public string HashState { get; private set; }

        public Room(int x, int y, string hash)
        {
            X = x;
            Y = y;
            HashState = hash;
        }

        public List<Room> GenerateNextStates()
        {
            List<Room> rooms = new List<Room>();
            var hash = GetHash(HashState);
            if (open.Contains(hash[0]) && Y + 1 <= max_y) rooms.Add(new Room(X, Y + 1, HashState + "U"));
            if (open.Contains(hash[1]) && Y - 1 >= min_y) rooms.Add(new Room(X, Y - 1, HashState + "D"));
            if (open.Contains(hash[2]) && X - 1 >= min_x ) rooms.Add(new Room(X - 1, Y, HashState + "L"));
            if (open.Contains(hash[3]) && X + 1 <= max_x) rooms.Add(new Room(X + 1, Y, HashState + "R"));
            return rooms;
        }

        internal bool IsGoal()
        {
            return X == 3 && Y == -3;
        }

        public string GetPath()
        {
            return HashState.Remove(0, 8);
        }

        private string GetHash(string val)
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
