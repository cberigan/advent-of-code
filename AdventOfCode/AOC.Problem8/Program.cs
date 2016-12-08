using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] raw = File.ReadAllLines("data.txt");
            char[,] screen = InitScreen();

            foreach (var l in raw)
            {
                if (l.StartsWith("rect"))
                {
                    var dim = l.Substring(5).Split('x');
                    var wide = int.Parse(dim[0]);
                    var tall = int.Parse(dim[1]);
                    CreateRect(screen, wide, tall);
                }
                else if (l.StartsWith("rotate row"))
                {
                    var dim = l.Substring(l.IndexOf('=')+1).Split(' ');
                    var row = int.Parse(dim[0]);
                    var pixels = int.Parse(dim[2]);
                    RotateRow(screen, row, pixels);
                }
                else if (l.StartsWith("rotate column"))
                {
                    var dim = l.Substring(l.IndexOf('=')+1).Split(' ');
                    var col = int.Parse(dim[0]);
                    var pixels = int.Parse(dim[2]);
                    RotateCol(screen, col, pixels);
                }
            }
            

            
            PrintScreen(screen);
            Console.WriteLine(CountPixels(screen));
            Console.ReadLine();
        }

        private static int CountPixels(char[,] screen)
        {
            int rowLength = screen.GetLength(0);
            int colLength = screen.GetLength(1);
            int count = 0;
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (screen[i, j].Equals('#')) count++; 
                }
            }
            return count;
        }

        private static void RotateCol(char[,] screen, int col, int pixels)
        {
            var rowLength = screen.GetLength(0);
            var shift = pixels % rowLength;

            char[] rowCopy = new char[rowLength];
            for (int i = 0; i < rowLength; i++)
            {
                rowCopy[i] = screen[i,col];
            }
            for (int i = 0; i < rowLength; i++)
            {
                screen[i, col] = rowCopy[mod(i - shift, rowLength)];
            }
        }

        private static void RotateRow(char[,] screen, int row, int pixels)
        {
            var colLength = screen.GetLength(1);
            var shift = pixels % colLength;

            char[] rowCopy = new char[colLength];
            for (int i = 0; i < colLength; i++)
            {
                rowCopy[i] = screen[row, i];
            }

            for (int i = 0; i < colLength; i++)
            {
                screen[row, i] = rowCopy[mod(i - shift, colLength)];
            }
        }

        private static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        private static char[,] InitScreen()
        {
            char[,] s = new char[6,50];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j <50; j++)
                {
                    s[i,j] = '.';
                }
            }
            return s;
        }

        private static void CreateRect(char[,] s, int wide, int tall)
        {
            for (int i = 0; i < tall; i++)
            {
                for (int j = 0; j < wide; j++)
                {
                    s[i, j] = '#';
                }
            }
        }

        private static void PrintScreen(char[,] screen)
        {
            int rowLength = screen.GetLength(0);
            int colLength = screen.GetLength(1);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", screen[i,j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
