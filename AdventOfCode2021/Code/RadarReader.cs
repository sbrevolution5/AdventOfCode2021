using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    internal static class RadarReader
    {
        public static int HowManyIncreases(string input)
        {
            var radarList = ReadInput(input);
            var previous = radarList.FirstOrDefault();
            radarList = radarList.Skip(1).ToList();
            var count = 0;
            foreach (var reading in radarList)
            {
                if (reading > previous)
                {
                    count++;
                }
                previous = reading;
            }
            return count;
        }
        private static List<int> ReadInput(string input)
        {
            var stringlist = input.Split('\n').ToList();
            return stringlist.Select(s=>int.Parse(s)).ToList();
        }
    }
}
