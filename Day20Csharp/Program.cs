using System.Collections.Specialized;
namespace Day20Csharp;
class Num
{
    public long value;
    public long startIndex;
    public long currentIndex;
    public Num(long value, long startIndex, long currentIndex)
    {
        this.value = value;
        this.startIndex = startIndex;
        this.currentIndex = currentIndex;
    }
}
class Program
{
    static long Mod(long num1, long num2)
    {
        return (num1%num2 + num2)%num2;
    }
    static void PrintNums(List<Num> numList)
    {
        Console.WriteLine("");
        foreach (Num num in numList)
        {
            Console.Write(((num.value)).ToString());
            Console.Write(" ");
        }
        Console.WriteLine("");
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Running...");
        List<long> originalNums = File.ReadAllLines("data.txt")
        .ToList()
        .ConvertAll(c => (long)(long.Parse(c)));

        long count = originalNums.Count;
        List<Num> originalNumList = new List<Num>();
        List<Num> numList = new List<Num>();
        for (long i = 0; i < originalNums.Count; i++)
        {
            originalNumList.Add(new Num(originalNums[(Int32)i]*(811589153%(count*(count-1))),i,i));
        }
        
        foreach (Num n in originalNumList)
        {
            numList.Add(n);
        }

        //PrintNums(numList);
        for (long round = 0; round < 10; round++)
        {
            foreach (Num numToMove in originalNumList)
            {
                long clicks = Mod(numToMove.value,count-1);
                long indexToReach = Mod((numToMove.currentIndex+clicks),(count));
                if (clicks != 0)
                {
                    long i = numToMove.currentIndex;
                    while (i != indexToReach)
                    {
                        numList[(Int32)i] = numList[(Int32)Mod((i+1),count)];
                        numList[(Int32)i].currentIndex = i;
                        i++;
                        i = Mod(i,count);
                    }
                    numToMove.currentIndex = indexToReach;
                    numList[(Int32)numToMove.currentIndex] = numToMove;
                }
                //PrintNums(numList);
            }
            //PrintNums(numList);
        }



        long indexOfZero = originalNumList[originalNums.IndexOf(0)].currentIndex;
        List<long> toSum = new List<long>();
        for (long i = 1000; i <= 3000; i+= 1000)
        {
            toSum.Add((long)numList[(Int32)(indexOfZero+i)%(Int32)(count)].value/((long)811589153%(count*(count-1)))*811589153);
        }
        long total = 0;
        foreach (long num in toSum)
        {
            total += num;
        }
        Console.WriteLine(total);
    }
}
