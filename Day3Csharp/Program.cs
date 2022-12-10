using System.IO;

namespace _2022AoCDay3;

class Program
{
    static char getDupe(List<List<char>> group)
    {
        foreach (char c in group[0])
            {
                if (group[1].Contains(c))
                {
                    if (group[2].Contains(c))
                    {
                        return c;
                    }
                }
            }
        return ' ';
    }

    static void Main(string[] args)
    {
        Console.WriteLine("running...");
        string[] data = File.ReadAllLines("data.txt");
        List<char> doubleList = new List<char>();

        List<List<List<char>>> groupData = new List<List<List<char>>>();

        for (int g=0; g < data.Count()/3; g++)
        {
            List<List<char>> group = new List<List<char>>();
            for(int i=0; i < 3;i++)
            {
                group.Add(data[i+(g*3)].ToCharArray().ToList());
            }
            groupData.Add(group);
        }

        foreach (List<List<char>> group in groupData)
        {
            doubleList.Add(getDupe(group));
        }

        int total = 0;
        List<int> numList = new List<int>();
        foreach (char c in doubleList)
        {
            int numValue;
            if (char.IsLower(c))
            {
                numValue = c-96;
            }
            else
            {
                numValue = c-38;
            }
            total += numValue;
            numList.Add(numValue);
        }
    }
}
