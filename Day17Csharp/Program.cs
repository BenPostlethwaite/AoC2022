using System.Runtime.CompilerServices;
using System.Data;
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
        List<State> states = new List<State>();
        states.Add(new State(0,0, 1));
        string jetStream = File.ReadAllText("data.txt");
        List<List<int[]>> rockStartPositions = new List<List<int[]>>();
        List<int[]> fallenRocks = new List<int[]>();
        ulong rocksToFall = 1000000000000;

        rockStartPositions.Add(new List<int[]>(){new int[]{2,5},new int[]{3,5},new int[]{4,5},new int[]{5,5}});
        rockStartPositions.Add(new List<int[]>(){new int[]{2,6},new int[]{3,6},new int[]{4,6},new int[]{3,5}, new int[]{3,7}});
        rockStartPositions.Add(new List<int[]>(){new int[]{2,5},new int[]{3,5},new int[]{4,5},new int[]{4,6}, new int[]{4,7}});
        rockStartPositions.Add(new List<int[]>(){new int[]{2,5},new int[]{2,6},new int[]{2,7},new int[]{2,8}});
        rockStartPositions.Add(new List<int[]>(){new int[]{2,5},new int[]{3,5},new int[]{2,6},new int[]{3,6}});

        List<string> fallenRocksGraph = new List<string>(){"-------"};
        
        //List<List<string>> rockStartPositonsGraph = new List<List<string>>();
        //PopulateRocks(rockStartPositonsGraph);
        List<string> repeatingSection = new List<string>();
        ulong counter = 0;
        ulong rockIndex = 0;
        bool cycleFound = false;
        ulong hiddenRocks = 0;
        ulong hiddenLength = 0;
        ulong hiddenCycles = 0;

        while (rockIndex + hiddenRocks < rocksToFall)
        {
            List<int[]> rockType = rockStartPositions[(Int32)rockIndex%5];
            Rock rock = new Rock(rockType, fallenRocksGraph.Count-1);
            bool landed = false;
            while (landed == false)
            {
                landed = MoveDown(rock, fallenRocksGraph);
                //PrintRocks(fallenRocksGraph);
                if (landed)
                {
                    break;
                }
                //PrintRocks(fallenRocksGraph);
                MoveLateral(jetStream[(Int32)counter], rock, fallenRocksGraph);
                counter++;
                counter = (ulong)((Int32)counter%jetStream.Count());
            }
            rockIndex++;
            // if (fallenRocksGraph.Count % 2 == 1 && fallenRocksGraph.GetRange(1,(fallenRocksGraph.Count-1)/2)
            // .SequenceEqual(fallenRocksGraph.GetRange((fallenRocksGraph.Count-1)/2+1,(fallenRocksGraph.Count-1)/2)))
            // {
            //     int repeat = rockIndex;
            // }
            //PrintRocks(fallenRocksGraph);

            State currentState = new State(counter, rockIndex, (ulong)fallenRocksGraph.Count);
            foreach (State state in states)
            {
                if (state.counter == currentState.counter && state.rockType == currentState.rockType)
                {
                    if (cycleFound == true)
                    {
                        repeatingSection = fallenRocksGraph.GetRange((Int32)state.height, (Int32)currentState.height-(Int32)state.height);
                        
                        hiddenCycles = (rocksToFall-rockIndex)/(currentState.rocksFallen-state.rocksFallen);
                        hiddenLength = ((ulong)repeatingSection.Count)*hiddenCycles;
                        hiddenRocks = hiddenCycles * (currentState.rocksFallen-state.rocksFallen);
                    }
                    cycleFound = true;
                    break;
                }
            }
            if (cycleFound == false)
            {
                states.Add(currentState);
            }
        
        }
        Console.WriteLine((ulong)fallenRocksGraph.Count+hiddenLength-1);
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

class State
{
    public ulong height;
    public ulong rocksFallen;
    public ulong counter;
    public ulong rockType;
    public State(ulong counter, ulong rockIndex, ulong height)
    {
        this.height = height;
        this.rocksFallen = rockIndex;
        this.counter = counter;
        this.rockType = rockIndex % 5;
    }
}
