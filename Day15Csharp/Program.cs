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


    static void Main(string[] args)
    {
        List<List<int[]>> allCoords = GetCoords("test.txt");
        int yToTest = 10;
        List<int> noBeaconHere = new List<int>();
        foreach (List<int[]> coords in allCoords)
        {
            int disToBeacon = Math.Abs(coords[1][0]-coords[0][0])+Math.Abs(coords[1][1]-coords[0][1]);
            int maxWidth = 2*disToBeacon+1;
            int widthAtToTest = disToBeacon - Math.Abs(2*(yToTest-coords[0][0]));
            if (Math.Abs(yToTest - coords[0][0]) <= disToBeacon)
            {
                for (int i = coords[0][1]-(widthAtToTest/2); i <= widthAtToTest; i++)
                {
                    noBeaconHere.Add(i);
                }
            }
        }
    }
}
