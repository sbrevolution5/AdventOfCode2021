using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    internal static class SubmarineNavigation
    {
        public static int NavigateSub(string input)
        {
            var directions = Format(input);
            return x * y;
        }

        private static List<Direction> Format(string input)
        {
            var lines = input.Split('\n');
            var list = new List<Direction>();
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                Direction direction = new Direction();
                if (parts[0] == "forward")
                {
                    direction.Command = Command.fwd;
                }
                else if (parts[0] == "down") { direction.Command = Command.down; }
                else if (parts[0] == "up") { direction.Command = Command.up; }
            }
            return list;
        }
    }

    internal class Direction
    {
        public Command Command { get; set; }
        public int Number { get; set; }
    }

    public enum Command
    {
        fwd,
        up,
        down
    }
}
