using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Services
{
    public static class ExpenseReportService
    {
        public static int Get2020Product(string input)
        {
            List<int> entries = ParseInput(input);
            return entries.Where(x => 0 < entries.FirstOrDefault(y => entries.Contains(2020 - x - y)))
                .Aggregate(1, (a, b) => a * b);
            while (entries.Count > 1)
            {
                var entry = entries.Take(1).First();
                var param1 = entries.Skip(1).Take(1).First();
                var sum = entry + param1;
                entries.Remove(entry);
                if (entries.Contains(2020 - sum))
                {
                    return entry * param1 * (2020 - sum);
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
