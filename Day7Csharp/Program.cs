namespace AoC2022Day7;

class Program
{
    
    static void Main(string[] args)
    {
        string[] commands = File.ReadAllLines("data.txt");
        /*
        foreach (string command in commands)
        {
            string cmd = command;
            cmd.Replace("dir", "mkdir");
            cmd.Replace(".txt", "");
            cmd.Replace(".dat", "");
            cmd.Replace(".lst", "");
            cmd.Replace(".log", "");
            cmd.Replace(".ext", "");
            
            if (int.TryParse())
            cmd += ".txt";
        }
        */
        string sourceDir = "/Users/Ben/Desktop/AoC2022/AoC2022Day7/datafolder";
        string openFile = "";
        Directory.CreateDirectory(sourceDir);
        for (int i = 1; i < commands.Length; i ++)
        {
            string command = commands[i];
            string[] commandInfo = command.Split(' ');
            if (commandInfo[0] == "dir")
            {
                Directory.CreateDirectory(sourceDir+openFile+"/"+commandInfo[1]);
            }
            else if (commandInfo[1] == "cd")
            {
                if (commandInfo[2] == "..")
                {
                    List<string> openFileList = openFile.Split("/").ToList<string>();
                    openFileList.RemoveAt(openFileList.Count-1);
                    openFile = openFileList[0];
                    for (int j = 1; j < openFileList.Count; j++)
                    {                        
                        openFile += "/" + openFileList[j];
                    }
                }
                else
                {
                    openFile+="/"+commandInfo[2];
                }
            }
            else if (commandInfo[1]=="ls")
            {                
            }
            else
            {
                string fileName = commandInfo[1]+".txt";
                using (StreamWriter sw = File.CreateText(sourceDir+openFile+"/"+fileName))
                {
                    sw.WriteLine(commandInfo[0]);
                }
            }
        }
/*
        openFile = "";
        Directory.GetFiles(txtFolderPath.Text, "*ProfileHandler.cs",SearchOption.AllDirectories)
*/
    }
}
