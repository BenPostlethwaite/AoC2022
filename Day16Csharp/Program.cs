using System.Collections;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using Graphalo;
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
    static DirectedGraph<string> graph = new DirectedGraph<string>();
    static Dictionary<string, int> distances = new Dictionary<string, int>();
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
            graph.AddVertex(name);
            foreach (string connection in connectionString)
            {
                string tempConnection = connection.Trim(',');
                node.connections.Add(tempConnection);
            }      
            nodes.Add(name, node);
        }
    }
    static void MakeConnection(string currentNodeName, int minsLeft, int totalFlow, List<string> previousOpenNodes)
    {
  
        foreach (string vertex in nodes.Keys)
        {
            int localMinsLeft = minsLeft;
            if (vertex != currentNodeName && nodes[vertex].flowRate!= 0)
            {
                Node currentNode  = nodes[vertex];
                int distance = distances[(currentNodeName)+vertex];
                localMinsLeft -= (distance); //traverse to node

                if ((localMinsLeft > 0) && previousOpenNodes.Contains(vertex) == false)
                {
                    localMinsLeft--; //open tap
                    List<string> openNodes = new List<string>();
                    foreach (string item in previousOpenNodes)
                    {
                        openNodes.Add(item);
                    }
                    openNodes.Add(vertex);
                    MakeConnection(vertex, (localMinsLeft), (totalFlow + localMinsLeft*currentNode.flowRate), openNodes);                                   
                }
            }
        }
        if (totalFlow > maxFlow.Value)
        {
            maxFlow = new KeyValuePair<List<string>, int>(previousOpenNodes, totalFlow);
        }
    }
    static void Main(string[] args)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        MakeNodes("data.txt");
        foreach (var nodeDict in nodes)
        {
            foreach (string connection in nodeDict.Value.connections)
            {
                graph.AddEdge(new Edge<string>(nodeDict.Key, connection, 1));
            }
        }

        foreach (string node1 in nodes.Keys)
        {
            foreach (string node2 in nodes.Keys)
            {
                var route = graph.Traverse(Graphalo.Traversal.TraversalKind.Dijkstra, node1, node2);
                int distance = route.Results.Count()-1;
                distances.Add(node1+node2, distance);
            }
        }

        List<string> visitedNodes = new List<string>();
        int minsLeft = 30;
        int totalFlow = 0;

        MakeConnection("AA", minsLeft, totalFlow, visitedNodes);
        var output = maxFlow;
        watch.Start();
        var elapsedMs = watch.ElapsedMilliseconds;
        Console.WriteLine($"Completed in {(double)elapsedMs/1000} ms");
        Console.WriteLine(output.Value);
    }
}