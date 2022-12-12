namespace Day11Csharp;

public class Monkey
{
    public ulong noOfInspections = 0;
    public List<ulong> items = new List<ulong>();
    public string op;
    public string stringValue1;
    public string stringValue2;
    public int divisor = 1;
    public int throwToIfTrue;
    public int throwToIfFalse;

    public void Operate(int itemIndex)
    {
        ulong output = 0;
        ulong value1 = 0;
        ulong value2 = 0;

        if (stringValue1 == "old")
        {
            value1 = items[itemIndex];
        }
        else
        {
            value1 = ulong.Parse(stringValue1);
        }

        if (stringValue2 == "old")
        {
            value2 = items[itemIndex];
        }
        else
        {
            value2 = ulong.Parse(stringValue2);
        }

        switch (op)
        {
            case ("*"):
            {
                output = items[itemIndex];
                output = value1 * value2;
                break;
            }
            case ("+"):
            {
                output = value1 + value2;
                break;
            } 
        }
        items[itemIndex] = output;
    }
    public int ThrowItem(int itemIndex)
    {
        ulong amountOffBy = items[itemIndex]%((ulong)divisor);
        if (items[itemIndex]%((ulong)divisor) == 0)
        {
            items.RemoveAt(itemIndex);
            return throwToIfTrue;
        }
        else
        {
            items.RemoveAt(itemIndex);
            return throwToIfFalse;
        }
    }
}
class Program
{
    static List<List<string>> GetMonkeyInfo()
    {
        List<string> data = File.ReadAllLines("data.txt").ToList<string>();
        List<List<string>> monkeyInfo = new List<List<string>>();
        for (int i = 0; i < data.Count; i += 7)
        {
            monkeyInfo.Add(data.GetRange(i+1,5));
        }
        return monkeyInfo;
    }
    static List<ulong> GetItems(string line)
    {
        List<string> stringItems = line.Replace(",",String.Empty).Split(' ').ToList<string>();
        stringItems.RemoveRange(0,4);
        List<ulong> items = stringItems.ConvertAll(c => ulong.Parse(c));
        return items;
    }
    static List<string> GetOpInfo(string line)
    {
        List<string> lineInfo = line.Split(" ").ToList<string>();
        lineInfo.RemoveRange(0,5);
        return lineInfo;
    }
    static int GetLastInt(string line)
    {
        string[] lineArray = line.Split(" ");
        return int.Parse(lineArray[lineArray.Length-1]);
    }
    static List<Monkey> MonkeySetup()
    {
        List<List<string>> monkeyInfo = GetMonkeyInfo();
        int numOfMonkeys = monkeyInfo.Count;
        List<Monkey> monkeys = new List<Monkey>();

        for (int i=0; i < numOfMonkeys; i++)
        {
            monkeys.Add(new Monkey());
            List<ulong> itemsToAdd = GetItems(monkeyInfo[i][0]);
            foreach (ulong item in itemsToAdd)
            {
                monkeys[i].items.Add(item);
            }
            List<string> lineInfo = GetOpInfo(monkeyInfo[i][1]);

            monkeys[i].stringValue1 = lineInfo[0];
            monkeys[i].op = lineInfo[1];
            monkeys[i].stringValue2 = lineInfo[2];

            monkeys[i].divisor = GetLastInt(monkeyInfo[i][2]);
            monkeys[i].throwToIfTrue = GetLastInt(monkeyInfo[i][3]);
            monkeys[i].throwToIfFalse = GetLastInt(monkeyInfo[i][4]);
        }
        return monkeys;
    }
    static void Main(string[] args)
    {
        List<Monkey> monkeys = MonkeySetup();

        ulong productOfDivisors = 1;
        foreach (Monkey monkey in monkeys)
        {
            productOfDivisors *= (ulong)monkey.divisor;
        }

        for (int round = 1; round <= 10000; round++)
        {
            foreach (Monkey monkey in monkeys)
            {
                monkey.noOfInspections += (ulong)monkey.items.Count;
                while (monkey.items.Count > 0)
                {                    
                    monkey.Operate(0);

                    ulong currentNum = monkey.items[0];
                    int moveTo = monkey.ThrowItem(0);
                    monkeys[moveTo].items.Add(currentNum%productOfDivisors);
                }
            }
        }

        ulong maxInspections1 = 0;
        ulong maxInspections2 = 0;

        foreach (Monkey monkey in monkeys)
        {
            if (monkey.noOfInspections > maxInspections1)
            {
                maxInspections2 = maxInspections1;
                maxInspections1 = monkey.noOfInspections;
            }
            else if (monkey.noOfInspections > maxInspections2)
            {
                maxInspections2 = monkey.noOfInspections;
            }
        }
        Console.WriteLine(maxInspections1*maxInspections2);
    }
}
