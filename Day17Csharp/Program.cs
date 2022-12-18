using System.Net.WebSockets;
using System;
using System.Reflection;
namespace Day17Csharp;
class Program
{
    static void PrintRocks(List<string> fallenRockGraph)
    {
        Console.WriteLine("\n");
        for (int i = fallenRockGraph.Count-1; i >=0; i--)
        {
            Console.WriteLine(fallenRockGraph[i]);
        }
    }
    static void PopulateRocks(List<List<string>> rockStartPositonsGraph)
    {
        
        List<string> fourByOne = new List<string>(){
        "|..@@@@.|",
        "|.......|",
        "|.......|",
        "|.......|"};
        fourByOne.Reverse();
        rockStartPositonsGraph.Add(fourByOne);
    
        List<string> cross = new List<string>(){
        "|...@...|",
        "|..@@@..|",
        "|...@...|",
        "|.......|",
        "|.......|",
        "|.......|"};
        cross.Reverse();
        rockStartPositonsGraph.Add(cross);

        List<string> L = new List<string>(){
        "|....@..|",
        "|....@..|",
        "|..@@@..|",
        "|.......|",
        "|.......|",
        "|.......|"};
        L.Reverse();
        rockStartPositonsGraph.Add(L);

        List<string> oneByFour = new List<string>(){
        "|..@....|",
        "|..@....|",
        "|..@....|",
        "|..@....|",
        "|.......|",
        "|.......|",
        "|.......|"};
        oneByFour.Reverse();
        rockStartPositonsGraph.Add(oneByFour);

        List<string> square = new List<string>(){
        "|..@@...|",
        "|..@@...|",
        "|.......|",
        "|.......|",
        "|.......|"};
        square.Reverse();
        rockStartPositonsGraph.Add(square);
    }
    static void MoveLateral(char c, Rock rock, List<string> fallenRockGraph)
    {
        int toAdd;
        if (c == '>'){toAdd = 1;}
        else{toAdd = -1;}
        bool hitWall = false;
        List<int[]> positionsCopy = new List<int[]>();
        foreach (int[] coord in rock.positions)
        {
            int[] coordCopy = new int[]{coord[0]+toAdd,coord[1]};            
            if (0 > coordCopy[0] || coordCopy[0]>6 || (coordCopy[1] < fallenRockGraph.Count && fallenRockGraph[coordCopy[1]][coordCopy[0]] != '.'))
            {
                hitWall = true;
                break;
            }
            else
            {
                positionsCopy.Add(coordCopy);
            }
        }
        if (hitWall == false)
        {
            rock.positions = positionsCopy;
        }
    }
static bool MoveDown(Rock rock, List<string> fallenRockGraph)
    {
        bool landed = false;
        //fallenRockGraph.Concat(rockStartPositonsGraph);
        List<int[]> positionsCopy = new List<int[]>();
        foreach (int[] coord in rock.positions)
        {
            int[] coordCopy = new int[]{coord[0],coord[1]-1};
            if (coordCopy[1] < fallenRockGraph.Count && fallenRockGraph[coordCopy[1]][coordCopy[0]] != '.')
            {
                landed = true;
                break;
            }
            else
            {
                positionsCopy.Add(coordCopy);
            }
        }
        if (landed == false)
        {
            rock.positions = positionsCopy;
            return false;
        }
        else
        {
            foreach (int[] coord in rock.positions)
            {
                if (coord[1] >= fallenRockGraph.Count)
                {
                    while (fallenRockGraph.Count-1 < coord[1])
                    {
                        fallenRockGraph.Add(".......");
                    }
                }
                System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(fallenRockGraph[coord[1]]);
                strBuilder[coord[0]] = '#';
                fallenRockGraph[coord[1]]=strBuilder.ToString();
            }
            return true;
        }
    }
    static void Main(string[] args)
    {
        string jetStream = File.ReadAllText("test.txt");
        List<List<int[]>> rockStartPositions = new List<List<int[]>>();
        List<int[]> fallenRocks = new List<int[]>();


        rockStartPositions.Add(new List<int[]>(){new int[]{2,5},new int[]{3,5},new int[]{4,5},new int[]{5,5}});
        rockStartPositions.Add(new List<int[]>(){new int[]{2,6},new int[]{3,6},new int[]{4,6},new int[]{3,5}, new int[]{3,7}});
        rockStartPositions.Add(new List<int[]>(){new int[]{2,5},new int[]{3,5},new int[]{4,5},new int[]{4,6}, new int[]{4,7}});
        rockStartPositions.Add(new List<int[]>(){new int[]{2,5},new int[]{2,6},new int[]{2,7},new int[]{2,8}});
        rockStartPositions.Add(new List<int[]>(){new int[]{2,5},new int[]{3,5},new int[]{2,6},new int[]{3,6}});

        List<string> fallenRocksGraph = new List<string>(){"-------"};
        
        //List<List<string>> rockStartPositonsGraph = new List<List<string>>();
        //PopulateRocks(rockStartPositonsGraph);

        int counter = 0;
        int rockIndex = 0;
        while (rockIndex < 2022)
        {
            List<int[]> rockType = rockStartPositions[rockIndex%5];
            Rock rock = new Rock(rockType, fallenRocksGraph.Count-1);
            bool landed = false;
            while (landed == false)
            {
                //problem with L shape overwriteing
                landed = MoveDown(rock, fallenRocksGraph);
                //PrintRocks(fallenRocksGraph);
                if (landed)
                {
                    break;
                }
                //PrintRocks(fallenRocksGraph);
                MoveLateral(jetStream[counter], rock, fallenRocksGraph);
                counter++;
                counter = counter%jetStream.Count();
            }
            rockIndex++;
        }
        Console.WriteLine(fallenRocksGraph.Count-1);
    }
}

class Rock
{
    public List<int[]> positions = new List<int[]>();
    public Rock(List<int[]> positions, int lowestY)
    {
        for(int i = 0; i < positions.Count; i++)
        {
            this.positions.Add(new int[] {positions[i][0], positions[i][1]+lowestY});
        }
    }
}
