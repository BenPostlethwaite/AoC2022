namespace AoCDay62022;
class Program
{
    static void Main(string[] args)
    {
        string[] data = File.ReadAllLines("test.txt");
        foreach (string line in data)
        {
            for (int i = 3; i < line.Count(); i++)
            {
                string subString = line.Substring(i-3,i);
                bool marker = true;
                foreach (char c in subString)
                {
                     
                    if (subString.Count(m => m == c) != 1)
                    {
                        marker = false;
                    }
                }
                if (marker == true)
                {
                    Console.WriteLine(i+1);
                    break;
                }
            }
        }
    }
}
