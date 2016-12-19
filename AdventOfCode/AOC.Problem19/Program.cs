using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem19
{
    class Program
    {
        static int numElfs = 3005290;
        static void Main(string[] args)
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        static void Part1()
        {
            Elf current = BuildCircle();
            while (current.Presents != numElfs)
            {
                current.TakePresentsLeft();
                current = current.Left;
            }
            Console.WriteLine("Part 1: Elf " + current.Num + " has all the presents.");
        }

        static void Part2()
        {
            Elf.SetElfNumber(numElfs);
            Elf current = BuildCircle();
            while (current.Presents != numElfs)
            {
                current.TakePresentsAcross();
                current = current.Left;
            }
            Console.WriteLine("Part 2: Elf " + current.Num + " has all the presents.");
        }

        static Elf BuildCircle()
        {
            Elf first = new Elf(1, 1);
            Elf current = first;
            for (int i = 1; i < numElfs; i++)
            {
                var newElf = new Elf(i + 1, 1);
                current.Left = newElf;
                var before = current;
                current = current.Left;
                current.Right = before;
                if (i == numElfs / 2) Elf.SetElfTracking(newElf);
                if (i == numElfs - 1)
                {
                    current.Left = first;
                    first.Right = current;
                }
            }
            return first;
        }
    }

    
}
