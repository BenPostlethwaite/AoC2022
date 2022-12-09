namespace AoC2022Day9;
class Program
{
    static void Follow(List<int[]> segmentCoords)
    {
        for (int i=1; i < segmentCoords.Count; i++)
        {            
            if (segmentCoords[i-1][0] == segmentCoords[i][0])
            {                        
                int yDiff = segmentCoords[i-1][1] - segmentCoords[i][1];
                if (Math.Abs(yDiff) >= 2)
                {
                    segmentCoords[i][1] += yDiff/2;
                }
            }
            else if (segmentCoords[i][1] == segmentCoords[i-1][1])
            {
                int xDiff = segmentCoords[i-1][0] - segmentCoords[i][0];
                if (Math.Abs(xDiff) >= 2)
                {
                    segmentCoords[i][0] += xDiff/2;
                }                        
            }
            else
            {
                int xDiff = segmentCoords[i-1][0] - segmentCoords[i][0];
                int yDiff = segmentCoords[i-1][1] - segmentCoords[i][1];
                if (((-1<=xDiff && xDiff<=1)==false) || ((-1<=yDiff && yDiff<=1)) == false)
                {
                    segmentCoords[i][0] += xDiff/Math.Abs(xDiff);
                    segmentCoords[i][1] += yDiff/Math.Abs(yDiff);
                }                    
            }
        }                
    }

    static List<int[]> AddTailCoord(List<int[]> segmentCoords, List<int[]> visitedCoords)
    {
        bool inside = false;
        foreach (int[] coord in visitedCoords)
        {
            if (coord[0] == segmentCoords[segmentCoords.Count-1][0])
            {
                if (coord[1] == segmentCoords[segmentCoords.Count-1][1])
                {
                    inside = true;
                    break;
                }
            }
        }

        if (inside == false)
        {
            int[] toAppend = { segmentCoords[segmentCoords.Count-1][0], segmentCoords[segmentCoords.Count-1][1] };
            visitedCoords.Add(toAppend);
        }
        return visitedCoords;
    }

    static int[] GetMultiplier(string direction)
    {
        int[] multiplier = { 0, 0 };
        switch (direction)
        {
            case ("R"):
                {
                    multiplier[0] = 1;
                    multiplier[1] = 0;
                    break;
                }
            case ("L"):
                {
                    multiplier[0] = -1;
                    multiplier[1] = 0;
                    break;
                }
            case ("U"):
                {
                    multiplier[0] = 0;
                    multiplier[1] = 1;

                    break;
                }
            case ("D"):
                {
                    multiplier[0] = 0;
                    multiplier[1] = -1;
                    break;
                }
            default:
                {
                    break;
                }
        }
        return multiplier;
    }

    static void Main(string[] args)
    {
        List<int[]> visitedCoords = new List<int[]>();
        List<int[]> segmentCoords = new List<int[]>();
        for (int i=0; i < 10; i++) 
        {
            int[] coord = {0,0};
            segmentCoords.Add(coord);
        }

        string[] data = File.ReadAllLines("data.txt");
        visitedCoords = AddTailCoord(segmentCoords, visitedCoords);

        foreach (string line in data)
        {
            string[] command = line.Split(' ');
            string direction = command[0];
            int magnitude = int.Parse(command[1]);
            int[] multiplier = GetMultiplier(direction);
            for (int i = 0; i < magnitude;i++)
            {
                segmentCoords[0][0] += multiplier[0];
                segmentCoords[0][1] += multiplier[1];

                Follow(segmentCoords);
                visitedCoords = AddTailCoord(segmentCoords, visitedCoords);
            }
        }
    }
}
