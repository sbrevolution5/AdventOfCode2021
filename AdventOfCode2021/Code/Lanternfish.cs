using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    internal static class Lanternfish
    {
        public static int HowManyFish(string input, int days)
        {
            var fish = ParseInput(input);
        }

        private static List<int> ParseInput(string input)
        {
            return input.Split(',').Select(i => int.Parse(i)).ToList();
        }
    }
}
