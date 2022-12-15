namespace Day15Csharp;
class Program
{
static List<List<int[]>> GetCoords(string fileName)
{
    List<string> data = File.ReadAllLines(fileName).ToList();
    List<List<int[]>> allCoords = new List<List<int[]>>();
    foreach (string line in data)
    {
        string[] splitLine = line.Split(' ');
        List<int[]> coords = new List<int[]>();

        int[] sensor = {
            int.Parse(splitLine[2].Replace("x=", "").Replace(",", "")),
            int.Parse(splitLine[3].Replace("y=", "").Replace(":", ""))
        };
        coords.Add(sensor);

        int[] beacon = {
            int.Parse(splitLine[8].Replace("x=", "").Replace(",", "")),
            int.Parse(splitLine[9].Replace("y=", ""))
        };
        coords.Add(beacon);
        allCoords.Add(coords);
        
    }
    return allCoords;
}
static List<List<char>> GetGrid(List<List<int[]>> allCoords)
{
    List<List<char>> grid = new List<List<char>>();
    int? minX = null;
    int? maxX = null;
    int? minY = null;
    int? maxY = null;

    foreach (List<int[]> coords in allCoords)
    {
        foreach (int[] coord in coords)
        {
            if ((coord[0] < minX) || minX == null)
            {
                minX = coord[0];
            }
            if ((coord[0] > maxX) || maxX == null)
            {
                maxX = coord[0];
            }
            if ((coord[1] < minY) || minY == null)
            {
                minY = coord[1];
            }
            if ((coord[1] > maxY) || maxY == null)
            {
                maxY = coord[1];
            }
        }
    }

    
    for (int yRow = (int)minY; yRow <= (int)maxY; yRow++)
    {
        grid.Add(new List<char>());
        for (int xRow = (int)minX; xRow <= (int)maxX; xRow++)
        {
            grid[yRow].Add('.');
        }
    }

    foreach (List<int[]> coords in allCoords)
    {
        grid[coords[0][1]][coords[0][0]] = 'S';
        int disToBeacon = Math.Abs(coords[1][0]-coords[0][0])+Math.Abs(coords[1][1]-coords[0][1]);
        for (int y = coords[0][1]-disToBeacon-(int)minY; y <= coords[0][1]+disToBeacon-(int)minY; y++)
        {
            if (y >= 0)
            {
                for (int x = coords[0][0]-disToBeacon-(int)minX; x <= coords[0][0]+disToBeacon-(int)minX; x++)
                {
                    if (x >= 0)
                    {
                        grid[y][x] = '#';
                    }
                }
            }
        }
        grid[(coords[1][1])-(((int)minY))][(coords[1][0])-((int)minX)] = 'B';
        PrintGrid(grid);

    }
    return grid;
}
static void PrintGrid(List<List<char>> grid)
{
    Console.Write("\n");
    foreach (List<char> line in grid)
    {
        Console.WriteLine(string.Join(" ", line));
    }
}
    static void Main(string[] args)
    {
        List<List<int[]>> coords = GetCoords("test.txt");
        List<List<char>> grid = GetGrid(coords);
        PrintGrid(grid);
    }
}
