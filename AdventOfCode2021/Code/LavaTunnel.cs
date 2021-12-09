using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    internal static class LavaTunnel
    {
        public static int FindSumOfLowRiskLevel(string input)
        {
            var points = ParseInput(input);
            var lowest = FindLowestPoints(points);
            return lowest.Sum(i => i + 1);
        }

        private static List<int> FindLowestPoints(List<List<int>> points)
        {
            List<int> lowest = new List<int>();
            int[][] arrays = points.Select(a => a.ToArray()).ToArray();
            for (int i = 0; i < arrays.Length; i++)
            {
                for (int j = 0; j < arrays[i].Length; j++)
                {
                    var low = false;
                    if (CheckLeft(i, j, arrays) && CheckRight(i, j, arrays) && CheckUp(i, j, arrays) && CheckDown(i, j, arrays))
                    {
                        lowest.Add(arrays[i][j]);
                    }
                }
            }
            return lowest;
        }

        private static bool CheckUp(int i, int j, int[][] arr)
        {
            if (i == 0)
            {
                return true;

            }
            if (arr[i][j] < arr[i - 1][j])
            {
                return true;
            }
            return false;
        }

        private static bool CheckDown(int i, int j, int[][] arr)
        {
            if (i == arr.Length)
            {
                return true;

            }
            if (arr[i][j] < arr[i + 1][j])
            {
                return true;
            }
            return false;
        }
        private static bool CheckLeft(int i, int j, int[][] arr)
        {
            if (j==0)
            {
                return true;

            }
            if (arr[i][j] < arr[i][j - 1])
            {
                return true;

            }
            return false;
        }
        private static bool CheckRight(int i, int j, int[][] arr)
        {
            if (j == arr[i].Length)
            {
                return true;

            }
            if (arr[i][j] < arr[i][j + 1])
            {
                return true;
            }
            return false;
        }
        private static List<List<int>> ParseInput(string input)
        {
            var lines = input.Split('\n',StringSplitOptions.RemoveEmptyEntries);
            return lines.Select(x => x.Trim().ToCharArray().Select(y => int.Parse(char.ToString(y))).ToList()).ToList();
        }
    }
}
