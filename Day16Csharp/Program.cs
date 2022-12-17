using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
namespace Day16Csharp;

class Node
{
    public List<string> connections;
    public string name;
    public int flowRate;
    public Node(string name, int flowRate)
    {
        connections = new List<string>();
        this.name = name;
        this.flowRate = flowRate;
    }
}
class Program
{
    static Dictionary<string, Node> nodes = new Dictionary<string, Node>();
    static KeyValuePair<List<string>, int> maxFlow = new KeyValuePair<List<string>, int>(new List<string>(), 0);
    static void MakeNodes(string fileName)
    {
        string[] data = File.ReadAllLines(fileName);
        
        foreach (string line in data)
        {
            string[] splitLine = line.Split(' ');
            string name = splitLine[1];

            char[] toRemove = "rate=;".ToCharArray();
            int flowRate = int.Parse(splitLine[4].Trim(toRemove));
            List<string> connectionString = line.Split(" ").ToList();
            connectionString.RemoveRange(0,9);
            List<string> connections = new List<string>();
            Node node = new Node(name, flowRate);            
            foreach (string connection in connectionString)
            {
                string tempConnection = connection.Trim(',');
                node.connections.Add(tempConnection);
            }      
            nodes.Add(name, node);
        }
    }

    static void MakeConnection(Node currentNode, int minsLeft, int totalFlow, List<string> previousOpenNodes)
    {
        
        foreach (string connection in currentNode.connections)
        {
            currentNode = nodes[connection];
            if (minsLeft > 1)
            {
                if ((currentNode.flowRate != 0) && (previousOpenNodes.Contains(connection) == false))
                {
                    List<string> openNodes = new List<string>();
                    foreach (string item in previousOpenNodes)
                    {
                        openNodes.Add(item);
                    }

                    openNodes.Add(connection);
                    MakeConnection(currentNode, (minsLeft-2), (totalFlow + minsLeft*currentNode.flowRate), openNodes);
                }
                MakeConnection(currentNode, (minsLeft-1), totalFlow, previousOpenNodes);                
            }
            else if (minsLeft == 1)
            {
                totalFlow += minsLeft*currentNode.flowRate;
            }
            if (totalFlow > maxFlow.Value)
            {                     
                maxFlow = new KeyValuePair<List<string>, int>(previousOpenNodes, totalFlow);
            }
        }
    }
    static void Main(string[] args)
    {
        MakeNodes("test.txt");
        Node currentNode = nodes["AA"];
        List<string> visitedNodes = new List<string>();
        int minsLeft = 30;
        int totalFlow = 0;

        MakeConnection(currentNode, minsLeft-1, totalFlow, visitedNodes);

    }
}
