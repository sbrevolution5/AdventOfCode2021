using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class LavaTunnel
    {
        public static int FindSumOfLowRiskLevel(string input)
        {
            var points = ParseInput(input);
            var lowest = FindLowestValues(points);
            return lowest.Sum(i => i + 1);
        }
        public static int Find3LargestBasins(string input)
        {
            var points = ParseInput(input);
            var lowest = FindLowestValues(points);
            var basins = FindBasinSizes(lowest);
            var largest3 = basins.OrderByDescending(b=>b).Take(3);
            var prod = 1;
            foreach (var b in largest3)
            {
                prod *= b;
            }
            return prod;
        }

        private static List<int> FindBasinSizes(List<int> lowest)
        {
            
        }

        private static List<int> FindLowestValues(List<List<int>> points)
        {
            List<int> lowest = new List<int>();
            int[][] arrays = points.Select(a => a.ToArray()).ToArray();
            for (int i = 0; i < arrays.Length; i++)
            {
                for (int j = 0; j < arrays[i].Length; j++)
                {
                    if (IsLowestNeighbor(arrays, i, j))
                    {
                        lowest.Add(arrays[i][j]);
                    }
                }
            }
            return lowest;
        }
        private static List<Tuple<int,int>> FindLowestPoints(List<List<int>> points)
        {
            List<Tuple<int,int>> lowest = new List<Tuple<int,int>>();
            int[][] arrays = points.Select(a => a.ToArray()).ToArray();
            for (int i = 0; i < arrays.Length; i++)
            {
                for (int j = 0; j < arrays[i].Length; j++)
                {
                    if (IsLowestNeighbor(arrays, i, j))
                    {
                        lowest.Add(new(i,j));
                    }
                }
            }
            return lowest;
        }

        private static bool IsLowestNeighbor(int[][] arrays, int i, int j)
        {
            return CheckLeft(i, j, arrays) && CheckRight(i, j, arrays) && CheckUp(i, j, arrays) && CheckDown(i, j, arrays);
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
            if (i+1 == arr.Length)
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
            if (j+1 == arr[i].Length)
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
