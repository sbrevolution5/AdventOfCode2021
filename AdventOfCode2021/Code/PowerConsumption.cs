using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    internal static class PowerConsumption
    {
        public static int GetLifeSupportRating(string input)
        {
            List<List<char>> splitLines = SplitInput(input);
            return FindOxygenRating(splitLines) * FindCo2Rating(splitLines);
        }

        private static int FindOxygenRating(List<List<char>> splitLines)
        {
            var count = splitLines.First().Count;
            var oxyLines = splitLines;

            for (int i = 0; i < count; i++)
            {
                if (oxyLines.Count == 1)
                {
                    break;
                }
                oxyLines = oxyLines.Where(l => l[i] == GetCommonBit(oxyLines, i, true)).ToList();
            }
            string bin = new string(oxyLines.First().ToArray());
            return Convert.ToInt32(bin,2);
        }

        private static int FindCo2Rating(List<List<char>> splitLines)
        {
            var count = splitLines.First().Count;
            var co2Lines = splitLines;

            for (int i = 0; i < count; i++)
            {
                if (co2Lines.Count == 1)
                {
                    break;
                }
                co2Lines = co2Lines.Where(l => l[i] == GetCommonBit(co2Lines, i, false)).ToList();
            }
            string bin = new string(co2Lines.First().ToArray());
            return Convert.ToInt32(bin, 2);
        }

        public static int GetPowerConsumption(string input)
        {
            List<List<char>> splitLines = SplitInput(input);
            var charnum = splitLines.First().Count;
            return GetPowerInt(splitLines, charnum);
        }

        private static int GetPowerInt(List<List<char>> splitLines, int charnum)
        {
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

        private static List<List<char>> SplitInput(string input)
        {
            var lines = input.Split("\n");
            List<List<char>> splitLines = new();
            foreach (var line in lines)
            {
                splitLines.Add(line.Trim().ToCharArray().ToList());
            }

            return splitLines;
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
        private static char GetCommonBit(List<List<char>> splitLines,int i, bool oxy)
        {
            if (oxy)
            {
                return GetOxyBit(splitLines, i);
            }
            else
            {
                return GetCo2Bit(splitLines, i);
            }
        }

        private static char GetOxyBit(List<List<char>> splitLines, int i)
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

            if (ones >= zeroes)
            {
                return '1';
            }
            else
            {
                return '0';
            }
        }

        private static char GetCo2Bit(List<List<char>> splitLines, int i)
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

            if (ones <= zeroes)
            {
                return '0';
            }
            else
            {
                return '1';
            }
        }
    }
}
