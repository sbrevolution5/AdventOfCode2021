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
            var x = 0;
            var y = 0;
            foreach (var direction in directions)
            {
                if (direction.Command == Command.fwd)
                {
                    x += direction.Number;
                }
                else if (direction.Command == Command.up)
                {
                    y-= direction.Number;
                }
                else if (direction.Command == Command.down)
                {
                    y+= direction.Number;
                }
            }
            return x * y;
        }
        public static int AimSub(string input)
        {
            var directions = Format(input);
            var x = 0;
            var y = 0;
            var aim = 0;
            foreach (var direction in directions)
            {
                if (direction.Command == Command.fwd)
                {
                    x += direction.Number;
                    y += aim*direction.Number;
                }
                else if (direction.Command == Command.up)
                {
                    aim-= direction.Number;
                }
                else if (direction.Command == Command.down)
                {
                    aim+= direction.Number;
                }
            }
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

                direction.Number = int.Parse(parts[1]);
                list.Add(direction);
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
