using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Net;
using System;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.IO;

namespace Day14Csharp;
class Program
{
    static List<List<int[]>> GetData(string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);
        List<List<int[]>> allCoords = new List<List<int[]>>();
        foreach (string line in lines)
        {
            string[] splitLine = line.Split(" ");    
            List<int[]> coords = new List<int[]>();     
            for (int i = 0; i < splitLine.Length; i+=2)
            {
                string[] stringCoord = splitLine[i].Split(',');
                int[] coord = Array.ConvertAll(stringCoord, c => int.Parse(c));
                coords.Add(coord);
            }
            allCoords.Add(coords);
        }
        return allCoords;
    }
    static void GetDrawing(List<List<char>> drawing)
    {
        for (int y = 0; y < 100000; y ++)
        {
            drawing.Add(new List<char>());
            for (int x = 0; x < 10000; x++)
            {
                drawing[y].Add('.');
            }            
        }
    }
    static void Draw(List<List<char>> drawing)
    {
        foreach (List<char> line in drawing)
        {
            Console.WriteLine(string.Join("", line));
        }
        Console.WriteLine("\n");
    }
    static void GetRocks(List<List<char>> drawing, List<List<int[]>> coords)
    {
                foreach (List<int[]> coordLine in coords)
        {
            for (int i = 0; i < coordLine.Count-1; i++)
            {
                if (coordLine[i][0] == coordLine[i+1][0])
                {
                    int x = coordLine[i][0];
                    int y = coordLine[i][1];
                    while (y != coordLine[i+1][1])
                    {
                        drawing[y][x] = '#';
                        y += (coordLine[i+1][1]-coordLine[i][1])/Math.Abs(coordLine[i+1][1]-coordLine[i][1]);
                    }
                    drawing[y][x] = '#';
                    //Draw(drawing);
                }
                else if (coordLine[i][1] == coordLine[i+1][1])
                {
                    int x = coordLine[i][0];
                    int y = coordLine[i][1];
                    while (x != coordLine[i+1][0])
                    {
                        drawing[y][x] = '#';
                        x += (coordLine[i+1][0]-coordLine[i][0])/Math.Abs(coordLine[i+1][0]-coordLine[i][0]);
                    }
                    drawing[y][x] = '#';
                    //Draw(drawing);
                }
            }
        }
    }
    static bool FallSand(List<List<char>> drawing)
    {
        int maxLevel = 5000;
        int currentY = 0;
        int currentX = 500;
        bool landed = false;
        while (landed == false)
        {
            if (drawing[currentY+1][currentX] == '.')
            {
                currentY = currentY+1;
            }
            else if (drawing[currentY+1][currentX-1] == '.')
            {
                currentX = currentX-1;
                currentY = currentY+1;
            }
            else if (drawing[currentY+1][currentX+1] == '.')
            {
                currentX = currentX+1;
                currentY = currentY+1;
            }
            else{landed = true;}
        }
        if (currentY == 0 && currentX == 500)
        {
            return true;
        }
        drawing[currentY][currentX] = 'o';
        return false;
    }
    static void Main(string[] args)
    {

        List<List<int[]>> coords = GetData("data.txt");
        List<List<char>> drawing = new List<List<char>>();
        GetDrawing(drawing);

        GetRocks(drawing, coords);
        int maxY = 0;
        foreach (List<int[]> line in coords)
        {
            foreach (int[] coord in line)
            {
                if (coord[1] > maxY)
                {
                    maxY = coord[1];
                }
            }
        }
        int[] coord1 = {0, maxY+2};
        int[] coord2 = {9999, maxY+2};
        List<int[]> coordList = new List<int[]>();
        coordList.Add(coord1);
        coordList.Add(coord2);

        coords.Add(coordList);
        GetRocks(drawing, coords);

        bool done = false;
        int counter = 0;
        while (done == false)
        {
            done = FallSand(drawing);
            //Draw(drawing);
            counter++;
        }
        Console.WriteLine(counter);
    }
}
