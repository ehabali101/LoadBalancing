using System;
using System.Collections.Generic;
using System.Text;

namespace RoundRobin
{

    public class Node
    {
        public Node(string serviceName, string url, int weight)
        {
            this.ServiceName = serviceName;
            this.Url = url;
            this.Weight = weight;
        }

        public int Weight { get; private set; }
        public string Url { get; private set; }
        public string ServiceName { get; private set; }
    }

    public class RoundRobinBalancer
    {
        private readonly List<Node> nodes;
        private int i = -1;
        private int cw = 0;

        public RoundRobinBalancer(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public Node DispatchTo()
        {
            while (true)
            {
                i = (i + 1) % nodes.Count;
                if (i == 0)
                {
                    cw = cw - MaxCommonDivisor(nodes);
                    if (cw <= 0)
                    {
                        cw = MaxWeight(nodes);
                        if (cw == 0)
                            return null;
                    }
                }
                if ((nodes[i]).Weight >= cw)
                    return nodes[i];
            }
        }

        private static int MaxCommonDivisor(List<Node> nodes)
        {
            List<int> nums = new List<int>();
            foreach (Node node in nodes)
            {
                nums.Add(node.Weight);
            }
            return max_common_divisor(nums);
        }

        private static int MaxWeight(List<Node> nodes)
        {
            int ret = -1;
            foreach (Node node in nodes)
            {
                if (node.Weight > ret)
                    ret = node.Weight;
            }
            return ret;
        }

        public static int gcd(int n, int m)
        {
            //swap
            if (n < m)
            {
                n = m + n;
                m = n - m;
                n = n - m;
            }
            if (m == 0) return n;
            return gcd(m, n % m);
        }

        public static int max_common_divisor(List<int> several)
        {
            int a = several[0];
            int b = several[1];
            int c = gcd(a, b);
            int i;
            for (i = 2; i < several.Count; i++)
            {
                c = gcd(c, several[i]);
            }
            return c;
        }
    }
}