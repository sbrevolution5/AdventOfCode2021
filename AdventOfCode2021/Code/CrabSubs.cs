using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class CrabSubs
    {
        public static int FuelForOneLine(string input)
        {
            var sublist = ParseInput(input);
            var target = 0;
            var leastFuel = 0;
            var max = sublist.Max();
            while (target <= max)
            {
                var currentFuel = 0;
                foreach (var sub in sublist)
                {
                    var steps = 0;
                    if (sub > target)
                    {
                        steps = sub - target;
                    }
                    else
                    {
                        steps = (sub - target) * -1;
                    }
                    var fuel = steps*(steps + 1) / 2;
                    currentFuel += fuel;
                    //don't keep counting if we start using extra
                    //don't break first time around
                    if (currentFuel > leastFuel && target != 0)
                    {
                        break;
                    }
                }
                //first loop we must set leastfuel
                if (currentFuel < leastFuel || target == 0)
                {
                    leastFuel = currentFuel;
                }
                target++;
            }
            return leastFuel;
        }

        private static List<int> ParseInput(string input)
        {
            var res = input.Split(',').Select(i => int.Parse(i)).ToList();
            return res;
        }
    }
}
