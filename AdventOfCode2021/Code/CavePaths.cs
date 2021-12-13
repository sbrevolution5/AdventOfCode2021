using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    internal static class CavePaths
    {
        public static int HowManyPaths(string input)
        {
            List<Cave> caves = ParseCaves(input);
            var start = caves.Where(c => c.Name == "start").First();
            var end = caves.Where(c=>c.Name == "end").First();
            throw new NotImplementedException();


        }

        private static List<Cave> ParseCaves(string input)
        {
            var lines = input.Split('\n');
            var result = new List<Cave>();
            foreach (var line in lines)
            {
                var names = line.Split('-');
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
                var names = line.Split('-');

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
