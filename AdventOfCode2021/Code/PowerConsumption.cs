using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    internal static class PowerConsumption
    {
        public static int GetPowerConsumption(string input)
        {
            var lines = input.Split("\n");
            List<List<char>> splitLines = new();
            foreach (var line in lines)
            {
                splitLines.Add(line.Trim().ToCharArray().ToList());
            }
            var charnum = splitLines.First().Count();
            var gammaString = "";
            var epsilonString = "";
            for (int i = 0; i < charnum; i++)
            {
                if (FindCommonBit(splitLines, i))
                {
                    gammaString += "1";
                    epsilonString += "0";
                }
                else
                {
                    gammaString += "0";
                    epsilonString += "1";
                }
            }
            var gamma = Convert.ToInt32(gammaString, 2);
            var epsilon = Convert.ToInt32(epsilonString, 2);
            return gamma * epsilon;
        }

        private static bool FindCommonBit(List<List<char>> splitLines, int i)
        {
            var ones = 0;
            var zeroes = 0;
            foreach (var line in splitLines)
            {
                if (line[i] == '1')
                {
                    ones++;
                }
                else
                {
                    zeroes++;
                }
            }

            if (ones > zeroes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
