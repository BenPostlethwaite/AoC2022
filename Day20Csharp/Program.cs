using System.Collections.Specialized;
namespace Day20Csharp;
class Num : ICloneable
{
    public int value;
    public int startIndex;
    public int currentIndex;
    public Num(int value, int startIndex, int currentIndex)
    {
        this.value = value;
        this.startIndex = startIndex;
        this.currentIndex = currentIndex;
    }
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
class Program
{
    static int Mod(int num1, int num2)
    {
        return (num1%num2 + num2)%num2;
    }
    static void Main(string[] args)
    {
        List<int> originalNums = File.ReadAllLines("test.txt")
        .ToList()
        .ConvertAll(c => int.Parse(c));

        List<Num> originalNumList = new List<Num>();
        List<Num> numList = new List<Num>();
        for (int i = 0; i < originalNums.Count; i++)
        {
            originalNumList.Add(new Num(originalNums[i],i,i));
        }
        int count = originalNumList.Count;
        foreach (Num n in originalNumList)
        {
            numList.Add(n);
        }


        foreach (Num numToMove in originalNumList)
        {
            int clicks = Mod(numToMove.value,count);
            int indexToReach = Mod((numToMove.currentIndex+clicks),(count));
            //Num originalNum = originalNumList[i];
            if (clicks != 0)
            {
                int i = numToMove.currentIndex;
                while (i != indexToReach)
                {
                    numList[i] = numList[Mod((i+1),count)];
                    numList[i].currentIndex = i;
                    i++;
                    i = Mod(i,count);
                }
                numToMove.currentIndex = indexToReach;
                numList[numToMove.currentIndex] = numToMove;
            }
        }
    }
}
