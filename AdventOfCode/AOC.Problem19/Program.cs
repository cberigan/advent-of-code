using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem19
{
    class Program
    {
        static void Main(string[] args)
        {
            int numElfs = 3005290;
            

            List<Elf> elfs = new List<Elf>();

            Elf first = new Elf(1, 1);
            Elf current = first;
            for (int i = 1; i < numElfs; i++)
            {
                current.Left = new Elf(i+1, 1);
                var before = current;
                current = current.Left;
                current.Right = before;
                if(i == numElfs - 1)
                {
                    current.Left = first;
                    first.Right = current;
                }
            }
            current = first;
            while(current.Presents != numElfs)
            {
                current.TakePresentsAcross();
                current = current.Left;
            }
            Console.WriteLine(current.Num);
            Console.ReadLine();
        }

        private static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }

    public class Elf
    {
        public static Elf elfTracking = null;
        public static bool traverseMode = false;
        public static int track = 0;
        private static int elfs = 3005290;
        public int Num { get; private set; }
        public int Presents { get; set; }
        public Elf Left { get; set; }
        public Elf Right { get; set; }

        public Elf(int num, int presents)
        {
            Num = num;
            Presents = presents;
        }

        public void TakePresentsLeft()
        {
            this.Presents += Left.Presents;
            //relink elf to the left of the next
            this.Left = Left.Left;
        }

        public decimal GetDegree()
        {
            return (decimal)360.0 / elfs;
        }

        public void TakePresentsAcross()
        {
            if(elfs== 5)
            {
                ;
            }
            //traverse list around left
            Elf current = null;
            int count = 0;
            if (elfTracking == null || traverseMode)
            {
                
                current = this;
                
                
            }
            else
            {
                
                //next time elf moves to left and one has been removed so count from current should be track - 2;
                count = track - 2;
                if (count < 100)
                {
                    traverseMode = true;
                }
                //traverse from track
                current = elfTracking;
            }
            decimal d = GetDegree();
            while ((decimal)count * GetDegree() <= 180.0m)
            {
                var d2 = count * GetDegree();
                count++;
                current = current.Left;
            }
            track = count;
            elfTracking = current;
            current = current.Right;//switch back to the left of 180
            this.Presents += current.Presents;
            //link both left and right of current
            current.Right.Left = current.Left;
            current.Left.Right = current.Right;
            elfs--;
            if (elfs == 2)
            {
                ;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Elf;
            if (other == null) return false;
            return other.Num == this.Num;
        }
        public override int GetHashCode()
        {
            return Num.GetHashCode();
        }
    }
}
