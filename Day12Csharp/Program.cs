using System.Collections.Generic;
using System.Net.Sockets;
namespace Day12Csharp;

public class Node
{
    public bool isMinimum = false;
    public int stepsTo;
    public bool assigned = false;
}

class Program
{
    static List<List<int>> getData()
    {
        string[] textFile = File.ReadAllLines("test.txt");
        List<string> list1d = new List<string>();
        List<List<int>> data = new List<List<int>>();

        list1d = textFile.ToList();
        foreach (string line in list1d)
        {
            List<int> intLine = new List<int>();            
            foreach (char ch in line)
            {
                int num = (int)ch;

                intLine.Add(num);
            }
            data.Add(intLine);
        }
        
        return data;
    }

    static void GetMinValues(List<List<int>> heights, List<List<Node>> minValues)
    {
        for (int y = 0; y < heights.Count; y++)
        {
            List<Node> row = new List<Node>();

            for (int x = 0; x < heights[0].Count; x++)
            {
                row.Add(new Node());
            }

            minValues.Add(row);
        }
    }

    static void PrintMinValues(List<List<Node>> minValues)
    {
        foreach (List<Node> row in minValues)
        {
            foreach (Node node in row)
            {
                Console.Write(node.stepsTo.ToString()+ " ");
            }
            Console.WriteLine();
        }
    }

    static bool TestIfAllAssigned(List<List<Node>> minValues)
    {
        bool allAssigned = true;
        foreach(List<Node> row in minValues)
        {
            foreach(Node node in row)
            {
                if (node.isMinimum == false)
                {
                    allAssigned = false;
                }
            }
        }
        return allAssigned;
    }

    static void AssignMinValue(List<List<Node>> minValues)
    {
        int min = 10000000;
        int minX = 0;
        int minY = 0;

        for (int y = 0; y < minValues.Count; y++)
        {
            for (int x = 0; x < minValues[y].Count; x++)
            {
                if (minValues[y][x].assigned && (minValues[y][x].isMinimum == false) && minValues[y][x].stepsTo < min)
                {
                    min = minValues[y][x].stepsTo;
                    minX = x;
                    minY = y;
                }
            }
        }
        minValues[minY][minX].isMinimum = true;
    }

    static void Main(string[] args)
    {
        List<List<int>> heights = getData();

        List<List<Node>> minValues = new List<List<Node>>();
        GetMinValues(heights, minValues);

        minValues[0][0].stepsTo = 0;
        minValues[0][0].isMinimum = true;
        minValues[0][0].assigned = true;
        
        heights[0][0] = (int)'a';
        heights[2][5]= (int)'z';

        while (TestIfAllAssigned(minValues) == false)
        {
            //PrintMinValues(minValues);
            //Console.WriteLine("");
            for (int y = 0; y < minValues.Count; y++)
            {

                for (int x = 0; x < minValues[0].Count; x++)
                {
                    if (minValues[y][x].isMinimum)
                    {
                        if (x > 0)
                        {
                            if (heights[y][x-1]-1<=heights[y][x])
                            {
                                if (minValues[y][x-1].assigned == false)
                                {
                                    minValues[y][x - 1].stepsTo = minValues[y][x].stepsTo + 1;
                                    minValues[y][x - 1].assigned = true;                                                                                           
                                }
                                else
                                {
                                    if (minValues[y][x].stepsTo < minValues[y][x - 1].stepsTo)
                                    {
                                        minValues[y][x - 1].stepsTo = minValues[y][x].stepsTo + 1;
                                    }                                     
                                }                                
                            }
                        }
                        if (y > 0)
                        {
                            if (heights[y-1][x]-1<=heights[y][x])
                            {
                                if (minValues[y-1][x].assigned == false)
                                {
                                    minValues[y-1][x].stepsTo = minValues[y][x].stepsTo + 1;
                                    minValues[y-1][x].assigned = true;                                                                                           
                                }
                                else
                                {
                                    if (minValues[y][x].stepsTo < minValues[y-1][x].stepsTo)
                                    {
                                        minValues[y-1][x].stepsTo = minValues[y][x].stepsTo + 1;
                                    }                                     
                                }                                
                            }
                        }
                        if (x < (heights.Count - 1))
                        {
                            if (heights[y][x+1]-1<=heights[y][x])
                            {
                                if (minValues[y][x+1].assigned == false)
                                {
                                    minValues[y][x+1].stepsTo = minValues[y][x].stepsTo + 1;
                                    minValues[y][x+1].assigned = true;                                                                                           
                                }
                                else
                                {
                                    if (minValues[y][x].stepsTo < minValues[y][x+1].stepsTo)
                                    {
                                        minValues[y][x+1].stepsTo = minValues[y][x].stepsTo + 1;
                                    }                                     
                                }                                
                            }
                        }
                        if (y < (heights.Count - 1))
                        {
                            if (heights[y+1][x]-1<=heights[y][x])
                            {
                                if (minValues[y+1][x].assigned == false)
                                {
                                    minValues[y+1][x].stepsTo = minValues[y][x].stepsTo + 1;
                                    minValues[y+1][x].assigned = true;                                                                                           
                                }
                                else
                                {
                                    if (minValues[y][x].stepsTo < minValues[y+1][x].stepsTo)
                                    {
                                        minValues[y+1][x].stepsTo = minValues[y][x].stepsTo + 1;
                                    }                                     
                                }                                
                            }
                        }
                    }
                }
            }
            AssignMinValue(minValues);
        }

        PrintMinValues(minValues);
        foreach (var line in heights)
        {
            foreach ( var num in line)
            {
                Console.Write(num);
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        Console.ReadKey();
    }
}