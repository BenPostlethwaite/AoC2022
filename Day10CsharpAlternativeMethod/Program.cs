using System.IO.Enumeration;
namespace Day10CsharpAlternativeMethod;

class Directory
{
    public string fileName;
    public int fileSize;
    public Directory parent;
    public List<Directory> children = new List<Directory>();
    public List<int> files = new List<int>();
    public Directory(Directory parent, string fileName)
    {
        this.parent = parent;
        this.fileName = fileName;
    }
}
class Program
{
    static List<Directory> directoriesBigEnough = new List<Directory>();

    static List<string[]> GetData(string fileName)
    {
        List<string[]> data = new List<string[]>();
        string[] lines = File.ReadAllLines(fileName);
        foreach (string line in lines)
        {
            string[] lineData = line.Split(" ");
            data.Add(lineData);
        }
        return data;

    }
    static void MakeDir(Directory openDir, List<string[]> data)
    {

        for (int i = 1; i < data.Count; i++)
        {
            string[] line = data[i];
            switch (line[0])
            {
                case "dir":
                {
                    openDir.children.Add(new Directory(openDir, line[1]));
                    break;
                }
                case "$":
                {
                    if (line[1] == "cd")
                    {
                        if (line[2] == "..")
                        {
                            openDir = openDir.parent;
                        }
                        else
                        {
                            foreach (Directory child in openDir.children)
                            {
                                if (child.fileName == line[2])
                                {
                                    openDir = child;
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
                default:
                {
                    openDir.files.Add(int.Parse(line[0]));
                    break;
                }
            }
        }
    }
    static int GetFileSize(Directory directory)
    {
        int fileSize = 0;
        fileSize += directory.files.Sum();
        foreach (Directory dir in directory.children)
        {
            fileSize += GetFileSize(dir);
        }
        if (fileSize <= 100000)
        {
            directoriesBigEnough.Add(directory);
        }
        directory.fileSize = fileSize;
        return fileSize;
    }
    static void Main(string[] args)
    {
        List<string[]> data = GetData("data.txt");

        Directory wholeThing = new Directory(null, "/");

        MakeDir(wholeThing, data);
        GetFileSize(wholeThing);
        int sum = 0;
        foreach (Directory dir in directoriesBigEnough)
        {
            sum += dir.fileSize;
            Console.WriteLine(dir.fileSize);
        }
        Console.WriteLine("\n");
        Console.WriteLine($"Sum of file sizes over 100000 = {sum}");
    }
}