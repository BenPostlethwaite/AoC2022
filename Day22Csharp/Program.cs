namespace Day22Csharp;
class Program
{
    public static Dictionary<int, int[]> directionTable = new Dictionary<int, int[]>();
    public static int maxWidth = 0;
    static void Main(string[] args)
    {
        PopulateDirectionTable();
        List<List<char>> graph = new List<List<char>>();
        List<string> input = GetData("data.txt", graph);
        int[] currentPos = {0,0};
        for (int i = 0; i<graph[0].Count;i ++)
        {
            if (graph[0][i] == '.')
            {
                currentPos = new int[]{0,i};
                break;
            }
        }        
        int direction = 0;

        foreach (string instruction in input)
        {
            int num;
            if (int.TryParse(instruction, out num) == true)
            {
                for (int i = 0; i < num; i ++)
                {
                    int[] testPos = new int[]{currentPos[0],currentPos[1]};
                    for (int j = 0; j <2; j++)
                    {
                        testPos[j]+= directionTable[direction][j];
                    }
                    if (testPos[0] < 0 || testPos[0] > graph.Count-1 || testPos[1] < 0 || testPos[1] > graph[testPos[0]].Count-1 || graph[testPos[0]][testPos[1]] == ' ')
                    {
                        testPos = WrapAround(direction, currentPos, graph);
                    }

                    if (graph[testPos[0]][testPos[1]] == '#')
                    {     
                        break;                               
                    }
                    else if (graph[testPos[0]][testPos[1]] == '.')
                    {
                        currentPos = new int[]{testPos[0], testPos[1]};
                    }
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
                    direction --;
                }
                direction = (direction+4) %4;
            }
        }
        Console.WriteLine((currentPos[0]+1)*1000+(currentPos[1]+1)*4+direction);
    }
    static List<string> GetData(string fileName, List<List<char>> graph)
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


        List<string> graphLines = data.ToList();
        graphLines.RemoveRange(graphLines.Count-2,2);

        foreach (string line in graphLines)
        {
            if (line.Length > maxWidth)
            {
                maxWidth = line.Length;
            }
            graph.Add(line.ToCharArray().ToList());
        }
        for (int y = 0; y < graph.Count; y++)
        {
            int currentWidth = graph[y].Count;
            for (int i = 0; i < (maxWidth-currentWidth); i++)
            {
                graph[y].Add(' ');
            }
        }
        return input;
    }
    static void PopulateDirectionTable()
    {
        directionTable.Add(0, new int[]{0,1});
        directionTable.Add(1, new int[]{1,0});
        directionTable.Add(2, new int[]{0,-1});
        directionTable.Add(3, new int[]{-1,0});
    }
    static int[] WrapAround(int direction, int[] currentPos, List<List<char>> graph)
    {
        int[] testPos = new int[]{currentPos[0], currentPos[1]};
        switch (direction)
        {
            case 0:
            {
                for (int c = 0; c < graph[currentPos[0]].Count; c++)
                {
                    if (graph[currentPos[0]][c] != ' ')
                    {
                        testPos[1] = c;
                        break;
                    }
                }
                break;
            }
            case 1:
            {
                for (int c = 0; c < graph.Count; c++)
                {
                    if (graph[c][currentPos[1]] != ' ')
                    {
                        testPos[0] = c;
                        break;
                    }
                }
                break;
            }
            case 2:
            {
                for (int c = graph[currentPos[0]].Count-1; c >= 0; c--)
                {
                    if (graph[currentPos[0]][c] != ' ')
                    {
                        testPos[1] = c;
                        break;
                    }
                }
                break;
            }
            case 3:
            {
                for (int c = graph.Count-1; c >= 0; c--)
                {
                    if (graph[c][currentPos[1]] != ' ')
                    {
                        testPos[0] = c;
                        break;
                    }
                }
                break;
            }
        }
        return testPos;
    }
}
