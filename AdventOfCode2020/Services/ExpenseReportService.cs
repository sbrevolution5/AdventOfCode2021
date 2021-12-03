using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Services
{
    internal static class ExpenseReportService
    {
        public static int Get2020Product(string input)
        {
            List<int> entries = ParseInput(input);
            var count = entries.Count;
            while (entries.Count>1)
            {
                var entry = entries.Take(1).First();
                entries.Remove(entry);
                foreach (var param in entries)
                {
                    if (entry + param == 2020)
                    {
                        return entry * param;
                    }
                }
            }
            return -1;
        }

        private static List<int> ParseInput(string input)
        {
            var lines = input.Split("\n");
            var list = new List<int>();
            foreach (var line in lines)
            {
                list.Add(int.Parse(line));
            }
            return list;
        }
    }
}
