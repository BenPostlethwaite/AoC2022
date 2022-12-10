using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
namespace AoC2022day5;
class Program
{
    static List<Stack<string>> GetStackTable(string[] data)
    {
        List<List<string>> dataTable = new List<List<string>>();
        int index = 0;
        int a = 0;
        while (int.TryParse((data[index][1].ToString()), out a) == false)
        {
            List<string> line = new List<string>();
            for (int i = 1; i < data[0].Length; i+=4)
            {
                line.Add(data[index][i].ToString());
            }
            dataTable.Add(line);
            index ++;
        }
        List<Stack<string>> stackTable = new List<Stack<string>>();
        for (int x = 0; x < dataTable[0].Count; x++)
        {
            Stack<string> stack = new Stack<string>();
            for (int y = dataTable.Count-1; y >= 0; y--)
            {
                 stack.Push(dataTable[y][x]);
            }
            stackTable.Add(stack);
        }

        return stackTable;
    }

    static List<List<int>> GetInstructionList(string[] data)
    {
        int a = 0;        
        List<List<int>> instructionList = new List<List<int>>();
        int startLine = 0;
        while (int.TryParse((data[startLine][1].ToString()), out a) == false)
        {
            startLine++;
        }
        startLine+=2;

        for (int i = startLine; i < data.Length; i++)
        {
            string[] instruction = data[i].Split(' ');
            List<int> nums = new List<int>();
            nums.Add(int.Parse(instruction[1]));
            nums.Add(int.Parse(instruction[3]));
            nums.Add(int.Parse(instruction[5]));
            instructionList.Add(nums);
        }
        return instructionList;
    }
    
    static void Main(string[] args)
    {
        string[] data = File.ReadAllLines("test.txt");
        List<Stack<string>> stackTable = GetStackTable(data);
        List<List<int>> instructionList = GetInstructionList(data);

        foreach (List<int> instruction in instructionList)
        {
            Stack<string> tempStack = new Stack<string>();
            for (int i = 0; i < instruction[0]; i++)
            {                
                tempStack.Push(stackTable[instruction[1]-1].Pop());            
            }
            while (tempStack.Count > 0)
            {
                stackTable[instruction[2]-1].Push(tempStack.Pop());
            }
        }
    }
}
