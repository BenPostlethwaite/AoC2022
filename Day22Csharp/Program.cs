using System.Collections.Generic;
using System.ComponentModel;
namespace Day22Csharp;
class Program
{
    static void Main(string[] args)
    {
        //cube where it's surface can be traversed;
        //and each point on the surface of the cube can be assigned a character
        //and only the surface of the cube can be traversed

        //the cube is a 3d array of characters where only characters on the surface of the cube can be assigned.
        //variable for dimensions of the cube


        int cubeSize = 50;
        //make the cube
        char[,,] cube = new char[cubeSize+2, cubeSize+2, cubeSize+2];

        //read the contents of the text file
        string[] referenceLines = File.ReadAllLines("netReferenceBig.txt");
        int maxLineLength = GetMaxLineLength(referenceLines);

        string[] lines = File.ReadAllLines("data.txt");
        List<string> instructions = GetData("data.txt", maxLineLength);

        //remove last 2 lines from the array
        lines = lines.Take(lines.Count() - 2).ToArray();

        int[,][] coordinates = MakeCoords(referenceLines, maxLineLength, cubeSize);
        PrintCoords(coordinates);

        PopulateDirectionTable();
        PopulateCube(lines, cube, coordinates);

        int[] currentPos = {0,0};
        for (int i = 0; i < lines[0].Count();i ++)
        {
            if (lines[0][i] == '.')
            {
                currentPos = new int[]{0,i};
                break;
            }
        }
        int direction = 0;        


        foreach (string instruction in instructions)
        {
            int num;
            if (int.TryParse(instruction, out num) == true)
            {
                for (int i = 0; i < num; i ++)
                {
                    int testDirection = direction;
                    int[] testPos = new int[]{currentPos[0],currentPos[1]};
                    for (int j = 0; j <2; j++)
                    {
                        testPos[j]+= direction2dTable[direction][j];
                    }
                    if (testPos[1] < 0 || testPos[0] > lines.Count()-1 || testPos[0] < 0 || testPos[1] > lines[testPos[0]].Count()-1 || lines[testPos[0]][testPos[1]] == ' ')
                    {
                        int[] direction2dArray = WrapAround(ref testPos, currentPos, direction, lines, coordinates, cube);   
                        
                        foreach (var item in direction2dTable)
                        {
                            if (item.Value.SequenceEqual(direction2dArray))
                            {
                                testDirection = item.Key;
                                break;
                            }
                        }
                    }

                    if (lines[testPos[0]][testPos[1]] == '#')
                    {     
                        break;                               
                    }
                    else if (lines[testPos[0]][testPos[1]] == '.')
                    {
                        direction = testDirection;
                        currentPos = new int[]{testPos[0], testPos[1]};
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                    Console.WriteLine("Current position: " + (currentPos[0]+1).ToString() + ", " + (currentPos[1]+1).ToString());
                    Console.WriteLine("Current direction: " + direction);
                }
            }
            else
            {
                if (instruction == "R")
                {
                    direction++;
                }
                else
                {
                    direction--;
                }
                direction = (direction+4) %4;
                Console.WriteLine("Direction: " + direction);
            }
        }
        Console.WriteLine("");
        Console.WriteLine("Final position: " + (currentPos[0]+1).ToString() + ", " + (currentPos[1]+1).ToString());
        Console.WriteLine("Final Direction: " + direction);
        Console.WriteLine("Puzzle answer = " + ((currentPos[0]+1)*1000 + (currentPos[1]+1)*4+direction));
    }
    static public Dictionary<int, int[]> direction2dTable = new Dictionary<int, int[]>();
    static List<string> GetData(string fileName, int maxWidth)
    {
        string[] data = File.ReadAllLines(fileName);
        string inputString = data[data.Length-1];
        List<string> input = new List<string>();
        string number = "";
        foreach (char c in inputString)
        {
            if (int.TryParse(c.ToString(), out int n) == true)
            {
                number += c;
            }
            else 
            {
                if (number != "")
                {
                    input.Add(number);
                    number = "";
                }
                input.Add(c.ToString());
            }
        }
        if (number != "")
        {
            input.Add(number);
        }

        return input;
    }
    static int[] WrapAround(ref int[] testPos2d, int[] currentPos2d, int direction, string[] lines, int[,][] coordinates, char[,,] cube)
    {
        int[] currentPos3d = coordinates[currentPos2d[0], currentPos2d[1]];
        int[] previousPos2d = new int[]{currentPos2d[0]-direction2dTable[direction][0], currentPos2d[1]-direction2dTable[direction][1]};
        int[] previousPos3d = coordinates[previousPos2d[0], previousPos2d[1]];

        int[] direction3d = new int[3]{currentPos3d[0]-previousPos3d[0], currentPos3d[1]-previousPos3d[1], currentPos3d[2]-previousPos3d[2]};
               
        int[] testPos3d = new int[]{currentPos3d[0]+direction3d[0], currentPos3d[1]+direction3d[1], currentPos3d[2]+direction3d[2]};
        int[] newDirection2d = new int[2];

        for (int i = 0; i < 6; i++)
        {
            int[] testTestPos = testPos3d.ToArray();
            testTestPos[i/2] += i%2 == 0 ? 1 : -1;           

            //test if x,y,z is in the cube
            if (testTestPos[0] >= 0 && testTestPos[0] < cube.GetLength(0)
            && testTestPos[1] >= 0 && testTestPos[1] < cube.GetLength(1)
            && testTestPos[2] >= 0 && testTestPos[2] < cube.GetLength(2)
            && (testTestPos[0] != currentPos3d[0] || testTestPos[1] != currentPos3d[1] || testTestPos[2] != currentPos3d[2])
            && cube[testTestPos[0], testTestPos[1], testTestPos[2]] != '\0')
            {
                int[] testTestPos2d = Get2dFrom3d(testTestPos, lines, coordinates);

                direction3d = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    direction3d[j] = testTestPos[j] - testPos3d[j];
                }

                int[] directionTestPos3d = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    directionTestPos3d[j] = testTestPos[j] + direction3d[j];
                }

                int[] directionTest2d = Get2dFrom3d(directionTestPos3d, lines, coordinates);

                for (int j = 0; j < 2; j++)
                {
                    newDirection2d[j] = directionTest2d[j] - testTestPos2d[j];
                }

                testPos2d = testTestPos2d;
                break;
            }   
        }
        return newDirection2d;
    }
    static int[] Get2dFrom3d(int[] testTestPos, string[] lines, int[,][] coordinates)
    {
        int[] testTestPos2d = new int[2];
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (coordinates[y,x] != null && coordinates[y,x][0]== testTestPos[0] && coordinates[y,x][1]== testTestPos[1] && coordinates[y,x][2]== testTestPos[2])
                {
                    testTestPos2d = new int[]{y,x};
                    return testTestPos2d;
                }
            }
        }
        return null;
    }
    static void PopulateDirectionTable()
    {
        direction2dTable.Add(0, new int[]{0,1});
        direction2dTable.Add(1, new int[]{1,0});
        direction2dTable.Add(2, new int[]{0,-1});
        direction2dTable.Add(3, new int[]{-1,0});
    }
    static void PopulateCube(string[] lines, char[,,] cube, int[,][] coordinates)
    {
        //loop through string[] lines and assign the characters to the cube
        for (int y2d = 0; y2d < lines.Length; y2d++)
        {
            for (int x2d = 0; x2d < lines[y2d].Length; x2d++)
            {
                if (coordinates[y2d, x2d] != null)
                {
                    cube[coordinates[y2d, x2d][0], coordinates[y2d, x2d][1], coordinates[y2d, x2d][2]] = lines[y2d][x2d];
                }
            }
        }
    }
    static void PrintCoords(int[,][] coordinates)
    {
        //print the coordinates
        for (int y2d = 0; y2d < coordinates.GetLength(0); y2d++)
        {
            for (int x2d = 0; x2d < coordinates.GetLength(1); x2d++)
            {
                if (coordinates[y2d, x2d] != null)
                {
                    Console.Write("("+coordinates[y2d, x2d][0] + "," + coordinates[y2d, x2d][1] + "," + coordinates[y2d, x2d][2] + ") ");
                }
                else
                {
                    Console.Write("        ");
                }
            }
            Console.WriteLine();
        }
    }
    static int GetMaxLineLength(string[] lines)
    {
        //find the length of the longest line in the net
        int maxLineLength = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Length > maxLineLength)
            {
                maxLineLength = lines[i].Length;
            }
        }
        return maxLineLength;
    }
    static int[,][] MakeCoords(string[] lines, int maxLineLength, int cubeSize)
    {
        //the contents of the text file is the net of the cube
        //simmilar characters are on the same face of the cube
        //any a's are on the top face of the cube
        //any b's are on the front face of the cube
        //any c's are on the left face of the cube
        //any d's are on the back face of the cube
        //any e's are on the bottom face of the cube
        //any f's are on the left face of the cube
        int[,][] coordinates = new int[lines.Length, maxLineLength][];

        //loop through the net in test.txt
        for (int y2d = 0; y2d < lines.Length; y2d++)
        {
            //loop through each character in the line
            for (int x2d = 0; x2d < lines[y2d].Length; x2d++)
            {
                //if the character is an a, assign it a 3d coordinate on the top face of the cube
                if (lines[y2d][x2d] == 'a')
                {
                    coordinates[y2d, x2d] = new int[3] {x2d-(cubeSize)+1, cubeSize+1, cubeSize-y2d};
                }
                //if the character is a b, assign it a 3d coordinate on the front face of the cube
                else if (lines[y2d][x2d] == 'b')
                {
                    coordinates[y2d, x2d] = new int[3] {x2d-(cubeSize)+1, (cubeSize*2)-y2d, 0};
                }
                //if the character is a c, assign it a 3d coordinate on the left face of the cube
                else if (lines[y2d][x2d] == 'c')
                {
                    coordinates[y2d, x2d] = new int[3] {0, cubeSize-x2d, y2d-2*(cubeSize)+1};
                }
                //if the character is a d, assign it a 3d coordinate on the back face of the cube
                else if (lines[y2d][x2d] == 'd')
                {
                    coordinates[y2d, x2d] = new int[3] {y2d-(cubeSize*3)+1, cubeSize-x2d, cubeSize+1};
                }
                //if the character is a e, assign it a 3d coordinate on the bottom face of the cube
                else if (lines[y2d][x2d] == 'e')
                {
                    coordinates[y2d, x2d] = new int[3] {x2d-cubeSize+1, 0, y2d-(cubeSize*2)+1};
                }
                //if the character is a f, assign it a 3d coordinate on the right face of the cube
                else if (lines[y2d][x2d] == 'f')
                {
                    coordinates[y2d, x2d] = new int[3] {cubeSize+1, (cubeSize*3)-x2d, cubeSize-y2d};
                }
            }
        }
        return coordinates;
    }
}