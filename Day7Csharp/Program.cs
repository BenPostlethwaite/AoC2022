namespace AoC2022Day7;

class Program
{
    public static int amountToRemove = 44_795_677 - 40_000_000;
    public static int minToRemove= 1000000000;

    static int GetSize(string folderPath)
    {
        var dir = new DirectoryInfo(folderPath);
        int total = 0;

        string[] dirPaths = Directory.GetDirectories(folderPath);

        foreach (string dirPath in dirPaths)
        {
            total += GetSize(dirPath);                
        }

        string[] filePaths = Directory.GetFiles(folderPath);

        foreach (string filePath in filePaths)
        {
            total += int.Parse(File.ReadAllText(filePath));
        }
        if (total >= amountToRemove)
        {
            if (total < minToRemove)
            {
                minToRemove = total;
            }
        }
        return total;
    }

    static void Main(string[] args)
    {
        string[] commands = File.ReadAllLines("data.txt");


    

        //if code is commenteded out it is is so I can run without overwriting files
        //if running on a windows computer change all "/" to "\\" and change sourceDir to the relevent directory.
        
        string sourceDir = "/Users/Ben/Desktop/AoC2022/Day7Csharp/datafolder";
        string openFile = "";
         
        Directory.CreateDirectory(sourceDir);
        for (int i = 1; i < commands.Length; i++)
        {
            string command = commands[i];
            string[] commandInfo = command.Split(' ');
            if (commandInfo[0] == "dir")
            {
                Directory.CreateDirectory(sourceDir + openFile + "/" + commandInfo[1]);
            }
            else if (commandInfo[1] == "cd")
            {
                if (commandInfo[2] == "..")
                {
                    List<string> openFileList = openFile.Split('/').ToList<string>();
                    openFileList.RemoveAt(openFileList.Count - 1);
                    openFile = openFileList[0];
                    for (int j = 1; j < openFileList.Count; j++)
                    {
                        openFile += "/" + openFileList[j];
                    }
                }
                else
                {
                    openFile += "/" + commandInfo[2];
                }
            }
            else if (commandInfo[1] == "ls")
            {
            }
            else
            {
                string fileName = commandInfo[1] + ".txt";
                using (StreamWriter sw = File.CreateText(sourceDir + openFile + "/" + fileName))
                {
                    sw.WriteLine(commandInfo[0]);
                }
            }
        }

        Console.WriteLine(GetSize(sourceDir));
        Console.WriteLine(minToRemove);
    }
}