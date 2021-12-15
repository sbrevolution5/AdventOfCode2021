using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class RiskyPath
    {
        private static Dictionary<(int, int), Node> CreateTileGrid(Dictionary<(int, int), Node> graph, int sqMax)
        {
            //Every repeat, based on distance from start, values tick up, then if any are > 9, they become 1
            var graphResult = new Dictionary<(int, int), Node>();
            List<Dictionary<(int, int), Node>> dicts = new List<Dictionary<(int, int),Node>>();
            dicts.Add(graph);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    //dont duplicate initial
                    if (!(i == 0 && j == 0))
                    {

                        var points = CreateTickCopy(graph, i, j, sqMax);
                        dicts.Add(points);
                    }
                }
            }
            return dicts.SelectMany(p => p).ToDictionary(p => p.Key, p => p.Value);
        }

        private static Dictionary<(int, int), Node> CreateTickCopy(Dictionary<(int, int), Node> graph, int x, int y, int sqMax)
        {
            var newGraph = new Dictionary<(int, int), Node>();
            foreach (var key in graph.Keys)
            {
                var newRisk = graph[key].Risk + x + y;
                if (newRisk > 9)
                {
                    newRisk -= 9;
                }
                var newX = graph[key].X + x * sqMax;
                var newY = graph[key].Y + y * sqMax;
                newGraph[(newX, newY)] = new Node(newX, newY, newRisk);
            }
            return newGraph;
        }

        //Static function returning adjacent tuple array
        private static (int, int)[] Adjacent()
        {
            return new[] { (-1, 0), (0, -1), (1, 0), (0, 1) };
        }
        //function that returns neighbor nodes from graph
        static IEnumerable<Node> GetNeighbors(Dictionary<(int, int), Node> graph, Node current)
        {
            foreach ((int i, int j) in Adjacent())
            {
                var key = (current.X + i, current.Y + j);
                if (graph.ContainsKey(key) && !graph[key].Visited)
                {
                    yield return graph[key];
                }
            }
        }
        public static int LeastRiskyValue(string input)
        {
            var Graph = new Dictionary<(int, int), Node>();

            //Read input to dictionary with tuple and risk value
            var graph = input.Split('\n')
                .SelectMany((line, y) =>
                    line.Trim().Select((c, x) => ((x, y), new Node(x, y, c - '0'))))
                .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
            var width = (int)Math.Sqrt(graph.Count);
            return Dijkstra(graph, graph[(width - 1, width - 1)]);
        }
        public static int LeastRiskyValueBigCave(string input)
        {
            var Graph = new Dictionary<(int, int), Node>();

            //Read input to dictionary with tuple and risk value
            var graph = input.Split('\n')
                .SelectMany((line, y) =>
                    line.Trim().Select((c, x) => ((x, y), new Node(x, y, c - '0'))))
                .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
            var width = (int)Math.Sqrt(graph.Count);
            var newGrid = CreateTileGrid(graph, width);
            var gridString = "";
            for (int i = 0; i < width*5; i++)
            {
                for (int j = 0; j < width*5; j++)
                {
                    var outp = " ";
                    if (newGrid.ContainsKey((i,j)))
                    {
                        outp = newGrid[(i, j)].Risk.ToString();
                    }
                    gridString += outp;
                }
                gridString += '\n';
            }
            return Dijkstra(newGrid, newGrid[(width*5 - 1, width*5 - 1)]);
        }

        //Dijkstra, takes dictionary and target
        private static int Dijkstra(Dictionary<(int, int), Node> graph, Node end)
        {


            //Priority queue of next nodes and distance of node
            var queue = new PriorityQueue<Node, int>();
            //Set start to 0
            graph[(0, 0)].Distance = 0;
            //add starting to queue
            queue.Enqueue(graph[(0, 0)], 0);
            //while there are still nodes in queue
            while (queue.Count > 0)
            {

                //dequeue the next element
                var current = queue.Dequeue();
                //If we were already here, then loop over the next one
                if (current.Visited)
                {
                    continue;
                }
                //mark current as visited
                current.Visited = true;
                //get neighbors and loop over them
                var neighbors = GetNeighbors(graph, current);
                foreach (var next in neighbors)
                {
                    //check our current distance plus next distance
                    var prospective = current.Distance + next.Risk;
                    if (prospective < next.Distance)
                    {
                        //if prospective distance is less than the neighbor's distance, set the neighbor's distance to the prospective distance
                        next.Distance = prospective;
                        //otherwise it will stay at max value
                    }
                    //if the neighbor's distance isn't default, add it to the queue With its potential distance
                    if (next.Distance != int.MaxValue)
                    {
                        queue.Enqueue(next, next.Distance);
                    }
                }



                //at the end return the end node's distance
            }
            return end.Distance;
            throw new NotImplementedException();
        }
    }

    //node class with x,y, dist, risk, and visited
    internal class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Risk { get; set; }
        public int Distance { get; set; } = int.MaxValue;
        public bool Visited = false;
        public Node(int x, int y, int v)
        {
            X = x;
            Y = y;
            Risk = v;
        }
    }
}