using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AoC2022Day8
{
    internal class Program
    {
        static void AddTailCoord(int[] tailCoord, List<int[]> visitedCoords)
        {
            if (visitedCoords.Contains(c => Array.Equals(c, tailCoord)) == false)
            {

            }
        }

        static int[] GetMultiplier(string direction)
        {
            int[] multiplier = { 0, 0 };
            switch (direction)
            {
                case ("R"):
                    {
                        multiplier[0] = 1;
                        multiplier[1] = 0;
                        break;
                    }
                case ("L"):
                    {
                        multiplier[0] = -1;
                        multiplier[1] = 0;
                        break;
                    }
                case ("U"):
                    {
                        multiplier[0] = 0;
                        multiplier[1] = 1;

                        break;
                    }
                case ("D"):
                    {
                        multiplier[0] = 0;
                        multiplier[1] = -1;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return multiplier;
        }

        static void Main(string[] args)
        {
            List<int[]> visitedCoords = new List<int[]>();
            int[] headCoord = { 0, 0 };
            int[] tailCoord = { 0, 0 };

            string[] data = File.ReadAllLines("test.txt");
            foreach (string line in data)
            {
                string[] command = line.Split(' ');
                string direction = command[0];
                int magnitude = int.Parse(command[1]);
                int[] multiplier = GetMultiplier(direction);

                for (int i = 0; i < magnitude;i++)
                {
                    AddTailCoord(tailCoord, visitedCoords);

                    headCoord[0] += multiplier[0];
                    headCoord[1] += multiplier[1];

                    if ()
                }
            }
        }
    }
}
