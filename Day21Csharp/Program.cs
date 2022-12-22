using System.Security.AccessControl;
using System;
using System.IO;
namespace Day21Csharp;

class Program
{
    public static Dictionary<string, Monkey> monkeys = GetData("data.txt");

    static void Main(string[] args)
    {
        Monkey monkey = monkeys["root"];
        double[] num1 = MonkeyCalc(monkey.num1);
        double[] num2 = MonkeyCalc(monkey.num2);
    }
    static double[] MonkeyCalc(string monkeyName)
    {
        Monkey monkey = monkeys[monkeyName];
        if (monkey.valueCalculated == false)
        {
            double[] num1 = MonkeyCalc(monkey.num1);
            double[] num2 = MonkeyCalc(monkey.num2);
            switch (monkey.op)
            {            
                case "+":
                {
                    for (int i = 0; i<2; i++)
                    {
                        monkey.value[i] = num1[i]+num2[i];
                    }
                    break;
                }
                case "-":
                {
                    for (int i = 0; i<2; i++)
                    {
                        monkey.value[i] = num1[i]-num2[i];
                    }
                    break;
                }
                case "*":
                {
                    for (int i = 0; i<2; i++)
                    {
                        for (int j = 0; j<2; j++)
                        {
                            monkey.value[i*j] += num1[i]*num2[j];
                        }
                    }
                    break;
                }
                case "/":
                {
                    if (num1[0] == 0 && num2[0] == 0)
                    {
                        monkey.value[1] = num1[1]/num2[1];
                    }
                    else if (num2[0] == 0)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            monkey.value[i] = num1[i]/num2[1];
                        }
                    }
                    else
                    {
                        Console.WriteLine("Panik");
                    }
                    break;
                }                
            }
            monkey.valueCalculated = true;
        }
        
        return (monkey.value);
    }
    static Dictionary<string, Monkey> GetData(string fileName)
    {
        Dictionary<string, Monkey> monkeys = new Dictionary<string, Monkey>();
        List<string[]> lines = File.ReadAllLines(fileName)
        .ToList()
        .ConvertAll(c => c.Split(":"));
        foreach (string[] line in lines)
        {
            monkeys.Add(line[0], new Monkey(line));
        }
        return monkeys;
    }
}

class Monkey
{
    public bool valueCalculated;
    public double[] value = new double[]{0,0};
    public string op;
    public string num1;
    public string num2;
    public Monkey(string[] line)
    {
        if (line[0] != "humn")
        {
            List<string> lineData = line[1].Split(" ").ToList();
            lineData.RemoveAt(0);
            if (lineData.Count == 1)
            {
                this.value = new double[]{0,double.Parse(lineData[0])};
                this.valueCalculated = true;
            }
            else
            {
                this.op = lineData[1];
                this.num1 = lineData[0];
                this.num2 = lineData[2];
                this.valueCalculated = false;
            }
        }
        else
        {
            this.value = new double[]{0,3296135418820};
            this.valueCalculated = true;
        }
    }
}