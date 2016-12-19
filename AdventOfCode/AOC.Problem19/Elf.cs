using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem19
{
    public class Elf
    {
        private static Elf elfTracking = null;
        private static int elfs = 0;
        public int Num { get; private set; }
        public int Presents { get; set; }

        internal static void SetElfNumber(int numElfs)
        {
            elfs = numElfs;
        }

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
            Left.RemoveElf();
        }

        public static void SetElfTracking(Elf elf)
        {
            elfTracking = elf;
        }

        public void RemoveElf()
        {
            this.Right.Left = this.Left;
            this.Left.Right = this.Right;
        }

        public void TakePresentsAcross()
        {
            this.Presents += elfTracking.Presents;
            //link both left and right of current
            elfTracking.RemoveElf();
            elfTracking = elfs % 2 == 1 ? elfTracking.Left.Left : elfTracking.Left;
            elfs--;
        }
    }
}
