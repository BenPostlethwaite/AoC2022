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
        List<List<int[]>> allCoords = GetCoords("data.txt");
        int yToTest = 2000000;
        List<int> noBeaconHere = new List<int>();
        int numBeaconsInRowToTest = 0;
        foreach (List<int[]> coords in allCoords)
        {                
            //Console.WriteLine($"({coords[0][0]},{coords[0][1]}) to ({coords[1][0]},{coords[1][1]})");
            int disToBeacon = Math.Abs(coords[1][0] - coords[0][0]) + Math.Abs(coords[1][1] - coords[0][1]);
            int maxWidth = 2 * disToBeacon + 1;
            int toMeasureWidth = maxWidth - 2*(Math.Abs(yToTest - coords[0][1]));
            if (Math.Abs(yToTest - coords[0][1]) <= disToBeacon)
            {
                int minRange = coords[0][0] - (toMeasureWidth-1)/2;
                int maxRange = coords[0][0] + (toMeasureWidth-1)/2;
                int minIndex = noBeaconHere.BinarySearch(minRange);
                int maxIndex = noBeaconHere.BinarySearch(maxRange);
                if (minIndex < 0){minIndex = ~minIndex;}
                if (maxIndex < 0){maxIndex = ~maxIndex;}
                if (maxIndex < noBeaconHere.Count)
                {
                    noBeaconHere.RemoveRange(minIndex, maxIndex-minIndex+1);
                }
                noBeaconHere.InsertRange(minIndex, Enumerable.Range(minRange, toMeasureWidth));
                /*
                for (int i = coords[0][0] - (toMeasureWidth-1)/2; i <= coords[0][0] + (toMeasureWidth - 1)/2; i++)
                {
                    int index = noBeaconHere.BinarySearch(i);
                    if (index < 0)
                    {
                        noBeaconHere.Insert(~index, i);
                    }
                }
                */
                
            }
        }
        foreach (List<int[]> coords in allCoords)
        {
            int index = noBeaconHere.BinarySearch(coords[1][0]);
            if (coords[1][1] == yToTest && index >= 0)
            {
                noBeaconHere.RemoveAt(index);
            }
        }
    }
}

