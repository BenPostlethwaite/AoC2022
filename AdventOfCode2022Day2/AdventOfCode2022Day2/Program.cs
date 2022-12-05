using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AoC_day_5
{
    internal class Program
    {
        static List<int[]> getLine(string[] stringCoord1, string[] stringCoord2)
        {
            List<int[]> line = new List<int[]>();
            int[] coord1 = { int.Parse(stringCoord1[0]), int.Parse(stringCoord1[1]) };
            int[] coord2 = { int.Parse(stringCoord2[0]), int.Parse(stringCoord2[1]) };
            if (coord1[0] == coord2[0])
            {
                if (coord1[1] < coord2[1])
                {

                }
                for (int i = coord1[1]; i <= coord2[1]; i++)
                {
                    int[] point = { coord1[0], i };
                    line.Add(point);
                }
            }

            else if (coord1[1] == coord2[1])
            {
                if (coord1[0] < coord2[0])
                {
                    int[] tempCoord = coord2;
                    coord2 = coord1;
                    coord1 = tempCoord;
                }
                for (int i = coord1[0]; i <= coord2[0]; i++)
                {
                    int[] point = { i, coord1[1] };
                    line.Add(point);
                }
            }

            else if ((coord2[1] - coord1[1]) / (coord2[0] - coord1[0]) == 1)
            {
                if (coord1[0] < coord2[0])
                {
                    int[] tempCoord = coord2;
                    coord2 = coord1;
                    coord1 = tempCoord;
                }
                for (int i = 0; i <= coord2[1] - coord1[1]; i++)
                {
                    int[] point = { coord1[0] + i, coord2[0] + i };
                    line.Add(point);
                }
            }

            else if ((coord2[1] - coord1[1]) / (coord2[0] - coord1[0]) == -1)
            {
                if (coord1[0] < coord2[0])
                {
                    int[] tempCoord = coord2;
                    coord2 = coord1;
                    coord1 = tempCoord;

                    for (int i = 0; i <= coord2[1] - coord1[1]; i++)
                    {
                        int[] point = { coord1[0] + i, coord2[0] - i };
                        line.Add(point);
                    }
                }                
            }
            return line;



            static void Main(string[] args)
            {

                string[] data = File.ReadAllLines("test.txt");
                List<int> xLines = new List<int>();
                List<int> yLines = new List<int>();

                List<string> graph = new List<string>();

                foreach (string row in data)
                {
                    string[] coords = row.Split(' ');
                    string[] coord1 = coords[0].Split(',');
                    string[] coord2 = coords[2].Split(',');


                    List<int[]> line = new List<int[]>(getLine(coord1, coord2));

                    foreach (int[] point in line)
                    {
                        string stringPoint = point[0].ToString() + "," + point[1].ToString();
                        graph.Add(stringPoint);
                    }

                }

                int overlaps = 0;

                while (graph.Count > 1)
                {
                    string point = graph[0];
                    bool found = true;
                    int count = 0;


                    int lenBefore = graph.Count;
                    graph.RemoveAll(item => item == point);
                    int lenAfter = graph.Count;

                    count = lenBefore - lenAfter;

                    Console.WriteLine(point);
                    Console.WriteLine(count);


                    if (count > 1)
                    {
                        overlaps++;
                    }
                }

                Console.WriteLine(overlaps);

                Console.ReadKey();
            }
        }
    }
}
