using System.Threading.Tasks.Dataflow;
using System.Security;
using System;
using System.Runtime.CompilerServices;
using System.Globalization;
namespace Day24Csharp;
class Program
{
    static void Main(string[] args)
    {
        List<Blizzard> blizzards = new List<Blizzard>();
        (int, int) gridSize = GetData("data.txt", blizzards);

        int there = (BFS(blizzards, gridSize, 0, (-1,0), gridSize));
        int back = (BFS(blizzards, gridSize, 0, (gridSize.Item1+1, gridSize.Item2), (0,0)))+1;
        int thereAgain = (BFS(blizzards, gridSize, 0, (-1,0), gridSize))+1;

        Console.WriteLine(there+back+thereAgain);
    }
    static int BFS(List<Blizzard> blizzards, (int, int) gridSize, int round, (int, int) start, (int, int) destination)
    {
        // round1
        
        // foreach (Blizzard blizzard in blizzards)
        // {
        //     blizzard.Move(gridSize);
        // }
        // round += 1;
        //PrintBlizzards(blizzards);

        HashSet<(int, int)> queue = new HashSet<(int,int)>();
        queue.Add(start);

        while (queue.Count != 0)
        {
            round++;
            foreach (Blizzard blizzard in blizzards){blizzard.Move(gridSize);}
            HashSet<(int,int)> newQueue = new HashSet<(int, int)>();
            foreach ((int,int) currentCoord in queue)
            {
                if (currentCoord == destination)
                {
                    //PrintBlizzards(blizzards);
                    foreach (Blizzard blizzard in blizzards){blizzard.Move(gridSize);}
                    return (round);
                }

                bool rightBlocked = false;
                bool downBlocked = false;
                bool leftBlocked = false;
                bool upBlocked = false;
                bool nomoveBlocked = false;
                foreach (Blizzard blizzard in blizzards)
                {   
                    if (rightBlocked == false && (currentCoord.Item2+1 > gridSize.Item2) || (blizzard.coord.Item1 == currentCoord.Item1 && blizzard.coord.Item2 == currentCoord.Item2+1))
                    {
                        rightBlocked = true;                    
                    }
                    else if (downBlocked == false && (currentCoord.Item1+1 > gridSize.Item1) || (blizzard.coord.Item1 == currentCoord.Item1+1 && blizzard.coord.Item2 == currentCoord.Item2))
                    {
                        downBlocked = true;
                    }
                    if ((currentCoord.Item2-1 < 0) || (blizzard.coord.Item1 == currentCoord.Item1 && blizzard.coord.Item2 == currentCoord.Item2-1))
                    {
                        leftBlocked = true;                    
                    }
                    if (((currentCoord.Item1-1 < 0) || blizzard.coord.Item1 == currentCoord.Item1-1 && blizzard.coord.Item2 == currentCoord.Item2))
                    {
                        upBlocked = true;
                    }
                    if (blizzard.coord.Item1 == currentCoord.Item1 && blizzard.coord.Item2 == currentCoord.Item2)
                    {
                        nomoveBlocked = true;
                    }
                }
                if (rightBlocked == false && currentCoord != start)
                {
                    newQueue.Add((currentCoord.Item1, currentCoord.Item2+1));
                }
                if (downBlocked == false)
                {
                    newQueue.Add((currentCoord.Item1+1, currentCoord.Item2));
                }
                if (leftBlocked == false && currentCoord != start)
                {
                    newQueue.Add((currentCoord.Item1, currentCoord.Item2-1));
                }
                if (upBlocked == false)
                {
                    newQueue.Add((currentCoord.Item1-1, currentCoord.Item2));
                }
                if (nomoveBlocked == false  || currentCoord == start)
                {
                    newQueue.Add((currentCoord.Item1, currentCoord.Item2));
                }
            }
            queue = new HashSet<(int,int)>(newQueue);
        }
        return (0);
    }
    static (int, int) GetData(string fileName, List<Blizzard> blizzards)
    {
        List<string> rawData = File.ReadAllLines(fileName).ToList();
        List<string> data = new List<string>();
        rawData.RemoveAt(0);
        rawData.RemoveAt(rawData.Count-1);
        foreach (string line in rawData)
        {
            data.Add(line.Substring(1, line.Length-2));
        }
        (int, int) finishCoord = (data.Count-1, data[0].Length-1);
        for (int y = 0; y < data.Count; y++)
        {
            for (int x = 0; x < data[y].Length; x++)
            {
                if (data[y][x] == '>')
                {
                    blizzards.Add(new Blizzard((y,x), (0,1)));
                }
                else if (data[y][x] == '<')
                {
                    blizzards.Add(new Blizzard((y,x), (0,-1)));
                }
                else if (data[y][x] == '^')
                {
                    blizzards.Add(new Blizzard((y,x), (-1,0)));
                }
                else if (data[y][x] == 'v')
                {
                    blizzards.Add(new Blizzard((y,x), (1,0)));
                }
            }
        }
        return (finishCoord);
    }
    static void PrintBlizzards(List<Blizzard> blizzards)
    {
        Console.WriteLine("");
        foreach (Blizzard blizzard in blizzards)
        {
            Console.WriteLine($"{blizzard.coord.Item1}, {blizzard.coord.Item2}");
        }
    }
}

class Blizzard
{
    public (int, int) coord;
    public (int, int) direction;
    public Blizzard((int, int) coord, (int, int) direction)
    {
        this.coord = coord;
        this.direction = direction;
    }
    public void Move((int, int) gridSize)
    {      
        coord.Item1 += direction.Item1;
        if (coord.Item1 > gridSize.Item1)
        {
            coord.Item1 = 0;
        }
        else if (coord.Item1 < 0)
        {
            coord.Item1 = gridSize.Item1;
        }

        coord.Item2 += direction.Item2;
        if (coord.Item2 > gridSize.Item2)
        {
            coord.Item2 = 0;
        }
        else if (coord.Item2 < 0)
        {
            coord.Item2 = gridSize.Item2;
        }
    }
}
