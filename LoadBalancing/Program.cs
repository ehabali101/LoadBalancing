using System;
using System.Collections.Generic;
using RoundRobin;

public class LoadBalancing
{
    public static void RunSnippet()
    {
        List<Node> servers = new List<Node>();
        servers.Add(new Node("serv", "urlA", 1));
        servers.Add(new Node("serv", "urlB", 1));
        servers.Add(new Node("serv", "urlC", 4));

        Dictionary<string, List<int>> dictLog = new Dictionary<string, List<int>>();

        RoundRobinBalancer balancer = new RoundRobinBalancer(servers);

        for (int i = 1; i <= 100; i++)
        {
            Node node = balancer.DispatchTo();

            if (!dictLog.ContainsKey(node.Url))
                dictLog[node.Url] = new List<int>();
            dictLog[node.Url].Add(i);
        }

        foreach (string key in dictLog.Keys)
        {
            WL("Url:{0} ==> {1}", key, dictLog[key].Count);
        }
    }

    #region Helper methods

    public static void Main()
    {
        try
        {
            RunSnippet();
        }
        catch (Exception e)
        {
            string error = string.Format("---\nThe following error occurred while executing the snippet:\n{0}\n---", e.ToString());
            Console.WriteLine(error);
        }
        finally
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }

    private static void WL(object text, params object[] args)
    {
        Console.WriteLine(text.ToString(), args);
    }

    private static void RL()
    {
        Console.ReadLine();
    }

    private static void Break()
    {
        System.Diagnostics.Debugger.Break();
    }

    #endregion
}

