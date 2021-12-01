using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class RadarReader
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
        public static int HowManySlidingIncreases(string input)
        {
            //get window of 3 values,  
            var radarList = ReadInput(input);
            var slidingList = new List<int>();
            while (radarList.Count >= 3)
            {
                slidingList.Add(radarList.Take(3).Sum());
                radarList.RemoveAt(0);
            }
            var previous = slidingList.FirstOrDefault();
            slidingList = slidingList.Skip(1).ToList();
            var count = 0;
            foreach (var reading in slidingList)
            {
                if (reading > previous)
                {
                    count++;
                }
                previous = reading;
            }
            return count;
            //process those into list
            //run list through initial method
        }
        private static List<int> ReadInput(string input)
        {
            var stringlist = input.Split('\n').ToList();
            return stringlist.Select(s=>int.Parse(s)).ToList();
        }
    }
}
