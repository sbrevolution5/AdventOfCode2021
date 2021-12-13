using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class CavePaths
    {
        public static int HowManyPaths(string input)
        {
            List<Cave> caves = ParseCaves(input);

            var start = caves.First(c => c.Name == "start");
            var paths = GetPossiblePaths(caves, start, "start");
            paths = paths.OrderBy(s => s).ToList();
            return paths.Count();

        }
        private static List<string> GetPossiblePaths(List<Cave> caves, Cave current, string path)
        {
            var paths = new List<string>();
            foreach (var neighbor in current.Linked)
            {
                if (neighbor.Name == "end")
                {
                    var tempPath = path + "," + neighbor.Name;
                    paths.Add(tempPath);
                }
                else if (IsNodeAllowed(current, path, neighbor))
                {
                    var tempPath = path + "," + neighbor.Name;
                    paths.AddRange(GetPossiblePaths(caves, neighbor, tempPath));
                }
            }
            return paths;
        }

        private static bool IsNodeAllowed(Cave current, string path, Cave next)
        {
            //Start node is allways not alowed
            if (next.Name == "start")
            {
                return false;
            }
            //if theres already this node in path, and it isn't big, we Check number of previous duplicates
            var splitPath = path.Split(',');
            //if no duplicate at all, then we are fine
            if (DoesPathHaveDuplicate(splitPath))
            {
                //if it does have a duplicate, check validity
                
                    var prosPath = path + ',' + next.Name;

                    if (IsPathValid(prosPath))
                    {
                        return true;
                    }
                    return false;
                
            }
            return true;

        }

        private static bool IsPathValid(string prosPath)
        {
            //Check for either a double duplicate, or a triplicate
            var lowers = prosPath.Split(',').Where(s => s.ToUpper() != s).ToList();
            var g = lowers.GroupBy(s => s);
            //If there is already one duplicate:
            if (g.Where(s => s.Count() == 3).Any())
            {
                return false;
            }
            if (g.Where(s => s.Count() == 2).Count() > 1)
            {
                return false;
            }
            return true;
        }

        private static bool DoesPathHaveDuplicate(string[] splitPath)
        {
            if (splitPath.Length > 2)
            {
                //filter out large Caves and group
                var lowers = splitPath.Where(s => s.ToUpper() != s).ToList();
                var g = lowers.GroupBy(s => s);
                //If there is already one duplicate:
                if (g.Where(s => s.Count() == 2).Count() >= 1)
                {
                    return true;
                }
            }
            return false;
        }

        private static List<Cave> ParseCaves(string input)
        {
            var lines = input.Split('\n');
            var result = new List<Cave>();
            foreach (var line in lines)
            {
                var names = line.Trim().Split('-');
                if (!result.Where(c => c.Name == names[0]).Any())
                {
                    var newCave = new Cave()
                    {
                        Name = names[0]
                    };
                    if (newCave.Name.ToUpper() == names[0])
                    {
                        newCave.IsBig = true;
                    }
                    result.Add(newCave);
                }
                if (!result.Where(c => c.Name == names[1]).Any())
                {
                    var newCave = new Cave()
                    {
                        Name = names[1]
                    };
                    if (newCave.Name.ToUpper() == names[1])
                    {
                        newCave.IsBig = true;
                    }
                    result.Add(newCave);
                }
            }
            foreach (var line in lines)
            {
                var names = line.Trim().Split('-');

                var linkTo = result.Where(c => c.Name == names[1]).First();
                var linkFrom = result.Where(c => c.Name == names[0]).First();
                linkFrom.Linked.Add(linkTo);
                linkTo.Linked.Add(linkFrom);

            }
            return result;
        }
    }
    internal class Cave
    {
        public string Name { get; set; } = "";
        public List<Cave> Linked { get; set; } = new List<Cave>();
        public bool IsBig { get; set; } = false;
        public bool VisitedSmall { get; set; } = false;
        public int VisitCount { get; set; } = 0;
    }
}
