namespace AoCday4actual2022;
class Program
{

    static int FindNumberInside(int[] coord1, int[] coord2)
    {
        if (coord1[0] >= coord2[0])
        {
            int[] tempCoord = coord2;
            coord2 = coord1;
            coord1 = tempCoord;
        }
        if (coord1[0] == coord2[0])
        {
            return 1;
        }
        if (coord1[1] >= coord2[0])
        {
            return 1;
        }
        else
        { 
        return 0;
        }
        
   }

    static void Main(string[] args)
    {
        int total = 0;
        string[] data = File.ReadAllLines("data.txt");
        foreach (string line in data)
        {
            string[] coords = line.Split(",");
            int[] coord1 = Array.ConvertAll(coords[0].Split("-"), i => int.Parse(i));
            int[] coord2 = Array.ConvertAll(coords[1].Split("-"), i => int.Parse(i));
            if(FindNumberInside(coord1, coord2) > 0)
            {
                total++;
            }
            
        }
    }
}
