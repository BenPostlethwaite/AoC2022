using System.Text;
namespace AoC2022Day10;
class Program
{
    static string GetCRT(int CRTrow, string spritePos)
    {
        CRTrow = CRTrow%40;
        if (spritePos[CRTrow] == '#')
        {
            return "#";
        }
        else
        {
            return ".";
        }
    }

    static string GetSpritePos(int regX)
    {
        string spritePos = "........................................";
        System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(spritePos);
        try
        {
            strBuilder[regX-1] = '#';
        }
        catch{}
        
        try
        {
            strBuilder[regX] = '#';
        }
        catch{}

        try
        {
            strBuilder[regX+1] = '#';
        }
        catch{}
        return strBuilder.ToString();
    }

    static void Main(string[] args)
    {
        string[] data = File.ReadAllLines("data.txt");      
        int cycle = 1;
        int regX = 1;
        string CRT = "";
        List<int> signalStrengths = new List<int>();
        foreach (string line in data)
        {
            string spritePos = GetSpritePos(regX);
            string[] command = line.Split(' ');            

            if (command[0] == "addx")
            {
                int value = int.Parse(command[1]);

                for (int i = 1; i <= 2; i++)
                {
                    if ((cycle + 20) % 40 == 0)
                    {
                        signalStrengths.Add(regX*cycle);
                    }
                    CRT += GetCRT(cycle-1, spritePos);   
                    if (i == 2)
                    {
                        regX += value;
                    }                          
                    cycle++;
                }
            }
            else
            {
                if ((cycle + 20) % 40 == 0)
                    {
                        signalStrengths.Add(regX*cycle);
                    }
                CRT += GetCRT(cycle-1, spritePos);
                cycle++;
            }
        }

        for (int i = 0; i < 6; i ++)
        {
            Console.WriteLine(CRT.Substring(40*i,40));
        }
    }
}
