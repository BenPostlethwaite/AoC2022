namespace AoCDay62022;
class Program
{
    static void Main(string[] args)
    {
        string line = File.ReadAllText("data.txt");
        int packetLength = 14;
        for (int i = packetLength-1; i < line.Count(); i++)
        {
            string subString = line.Substring(i-(packetLength-1),packetLength);
            bool repeated = false;
            foreach (char c in subString)
            {
                    
                if (subString.Count(m => m == c) != 1)
                {
                    repeated = true;
                    break;
                }
            }
            if (repeated == false)
            {
                Console.WriteLine(i+1);
                break;
            }
        }
        
    }
}
