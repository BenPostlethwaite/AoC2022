using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day13Csharp
{
    internal class Program
    {
        static List<string[]> GetData(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            List<string[]> data = new List<string[]>();
            for (int i = 0; i < lines.Length-2; i+=2)
            {
                string[] toComp = {lines[i],lines[i+1]};
                data.Add(toComp);
            }
            return data;
        }

        static void Main(string[] args)
        {
            List<string[]> data = GetData("test.txt");
            foreach (string[] pair in data)
            {
                string line1String = pair[0];
                string line2String = pair[1];


            }
        }
}
