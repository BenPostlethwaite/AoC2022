using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Data;
using System.IO;
using System.Collections;
namespace Day23Csharp;
class Program
{
    public static Dictionary<char, int[]> directionTable = new Dictionary<char, int[]>(){
        {'N', new int[]{-1, 0}},
        {'S', new int[]{1, 0}},
        {'W', new int[]{0, -1}},
        {'E', new int[]{0, 1}}
    };

    static void Main(string[] args)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        List<char> directions = new List<char>(){'N', 'S', 'W', 'E'};

        List<Elf> elves = GetElves("data.txt");
        for (int round = 1; round >0; round++)
        {
            List<int[]> posBefore = new List<int[]>();
            foreach (Elf elf in elves)
            {
                posBefore.Add(new int[]{elf.currentCoord[0], elf.currentCoord[1]});
            }
            // List<Elf> elvesBefore = elves.ConvertAll<Elf>(elf => (Elf)elf.Clone());
            GetProposedCoords(elves, directions);
            
            foreach (Elf elf in elves)
            {
                bool otherProposed = false;
                foreach (Elf tempElf in elves)
                {
                    if (elf != tempElf && elf.proposedCoord[0] == tempElf.proposedCoord[0] && elf.proposedCoord[1] == tempElf.proposedCoord[1])
                    {
                        otherProposed = true;
                        break;
                    }
                }
                if (otherProposed == false)
                {
                    elf.currentCoord[0] = elf.proposedCoord[0];
                    elf.currentCoord[1] = elf.proposedCoord[1];
                }
            }
            foreach (Elf elf in elves)
            {
                elf.proposedCoord[0] = elf.currentCoord[0];
                elf.proposedCoord[1] = elf.currentCoord[1];
            }
            //PrintElves(elves);
            for (int i = 0; i < elves.Count; i++)
            {
                if (elves[i].currentCoord[0] != posBefore[i][0] || elves[i].currentCoord[1] != posBefore[i][1])
                {
                    break;
                }
                if (i == elves.Count-1)
                {
                    watch.Stop();
                    Console.WriteLine($"Completed in {watch.ElapsedMilliseconds} ms");
                    Console.WriteLine(round);       
                    string s = Console.ReadLine();         
                }
            }
            RotateDirections(directions);            
        }
        int minX = 999999999;
        int maxX = -999999999;
        int minY = 999999999;
        int maxY = -999999999;

        foreach (Elf elf in elves)
        {
            if (elf.currentCoord[1] < minX)
            {
                minX = elf.currentCoord[1];
            }
            else if (elf.currentCoord[1] > maxX)
            {
                maxX = elf.currentCoord[1];
            }
            if (elf.currentCoord[0] < minY)
            {
                minY = elf.currentCoord[0];
            }
            else if (elf.currentCoord[0] > maxY)
            {
                maxY = elf.currentCoord[0];
            }
        }
        
        Console.WriteLine((1+maxY-minY)*(1+maxX-minX)-elves.Count);
    }
    static List<Elf> GetElves(string fileName)
    {
        List<Elf> elves = new List<Elf>();
        string[] data = File.ReadAllLines(fileName);
        char[][] table = Array.ConvertAll<string, char[]>(data, s => s.ToCharArray());
        for (int y = 0; y <table.Length; y++)
        {
            for (int x = 0; x < table[y].Length; x++)
            {
                if (table[y][x] == '#')
                {
                    elves.Add(new Elf(new int[]{y,x}));
                }
            }
        }
        return elves;
    }
    static void RotateDirections(List<char> directons)
    {
        char temp = directons[0];
        for (int i = 0; i < directons.Count-1; i++)
        {
            directons[i] = directons[i+1];
        }
        directons[directons.Count-1] = temp;
    }
    static bool TestIfOccupied(List<Elf> elves, Elf elf, char direction)
        {
            int[] currentCoord = elf.currentCoord;
            int[] directionInfo = directionTable[direction];
            if (directionInfo[1] == 0)
            {
                foreach (Elf testElf in elves)
                {
                    if (currentCoord[0]+directionInfo[0] == testElf.currentCoord[0] && currentCoord[1]-1 == testElf.currentCoord[1])
                    {
                        return true;
                    }
                    if (currentCoord[0]+directionInfo[0] == testElf.currentCoord[0] && currentCoord[1] == testElf.currentCoord[1])
                    {
                        return true;
                    }
                    if (currentCoord[0]+directionInfo[0] == testElf.currentCoord[0] && currentCoord[1]+1 == testElf.currentCoord[1])
                    {
                        return true;
                    }
                }
                
            }
            else
            {
                foreach (Elf testElf in elves)
                {
                    if (currentCoord[0]-1 == testElf.currentCoord[0] && currentCoord[1]+directionInfo[1] == testElf.currentCoord[1])
                    {
                        return true;
                    }
                    if (currentCoord[0] == testElf.currentCoord[0] && currentCoord[1]+directionInfo[1] == testElf.currentCoord[1])
                    {
                        return true;
                    }
                    if (currentCoord[0]+1 == testElf.currentCoord[0] && currentCoord[1]+directionInfo[1] == testElf.currentCoord[1])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    static void GetProposedCoords(List<Elf> elves, List<char> directions)
    {
        foreach (Elf elf in elves)
        {
            bool foundDirection = false;
            int foundDirections = 0;

            foreach (char direction in directions)
            {
                if (TestIfOccupied(elves, elf, direction) == false)
                {
                    if (foundDirection == false)
                    {
                        elf.proposedCoord = new int[]{elf.currentCoord[0]+directionTable[direction][0], elf.currentCoord[1]+directionTable[direction][1]};
                    }
                    foundDirections++;
                    foundDirection = true;
                    
                }
            }
            if (foundDirection == false || foundDirections == 4)
            {
                elf.proposedCoord = new int[]{elf.currentCoord[0], elf.currentCoord[1]};
            }
        }
    }
    static void PrintElves(List<Elf> elves)
    {
        Console.WriteLine("");
        foreach (Elf elf in elves)
        {
            Console.WriteLine($"{elf.currentCoord[1]}, {elf.currentCoord[0]}");
        }
    }
}
class Elf : ICloneable
{
    public int[] currentCoord;
    public int[] proposedCoord;
    public Elf(int[] currentCoord)
    {
        this.currentCoord = currentCoord;
    }
    public object Clone()
    {
        return MemberwiseClone();
    }
}