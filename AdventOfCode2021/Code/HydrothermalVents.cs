using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class HydrothermalVents
    {
        public static int CountOverlaps(string input)
        {
            var count = 0;
            Grid grid = ParseInput(input);
            foreach (var line in grid.Lines)
            {
                grid.PlotLine(line);
            }
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (grid.grid[i, j] > 1)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private static Grid ParseInput(string input)
        {
            var lines = input.Split('\n');
            var grid = new Grid();
            foreach (var line in lines)
            {
                var pts = line.Split(" -> ");
                var start = pts[0].Split(',');
                var end = pts[1].Split(',');
                grid.Lines.Add(new()
                {
                    Start = new()
                    {
                        x = int.Parse(start[0]),
                        y = int.Parse(start[1])
                    },
                    End = new()
                    {
                        x = int.Parse(end[0]),
                        y = int.Parse(end[1])
                    }
                });
            }
            return grid;
        }
    }

    internal class Grid
    {
        public List<Line> Lines { get; set; } = new List<Line>();
        public int[,] grid { get; set; } = new int[1000, 1000];

        internal void PlotLine(Line line)
        {
            line.Direction = GetLineDirection(line);
            switch (line.Direction)
            {
                case LineDirection.n:
                case LineDirection.s:
                    PlotLineVertical(line);
                    break;
                case LineDirection.e:
                case LineDirection.w:
                    PlotLineHorizontal(line);
                    break;
                case LineDirection.x:
                    PlotPoint(line);
                    break;
                case LineDirection.d:
                    PlotLineDiagonal(line);
                    break;
                default:
                    break;
            }
        }

        private void PlotLineDiagonal(Line line)
        {
            var x = line.Start.x;
            var y = line.Start.y;
            //rl
            if (line.Start.x > line.End.x)
            {
                //ns
                if (line.Start.y > line.End.y)
                {
                    while (y >= line.End.y)
                    {
                        grid[x, y]++;
                        x--;
                        y--;
                    }
                }
                //sn
                else
                {
                    while (y <= line.End.y)
                    {
                        grid[x, y]++;
                        x--;
                        y++;
                    }
                }
            }
            //lr
            else
            {
                //ns
                if (line.Start.y > line.End.y)
                {
                    while (y >= line.End.y)
                    {
                        grid[x, y]++;
                        x++;
                        y--;
                    }
                }
                //sn
                else
                {
                    while (y <= line.End.y)
                    {
                        grid[x, y]++;
                        x++;
                        y++;
                    }
                }
            }
        }

        private void PlotLineVertical(Line line)
        {
            var low = 0;
            var high = 0;
            if (line.Start.y > line.End.y)
            {
                low = line.End.y;
                high = line.Start.y;
            }
            else
            {
                low = line.Start.y;
                high = line.End.y;
            }
            for (int i = low; i <= high; i++)
            {
                grid[line.Start.x, i]++;
            }
        }

        private void PlotLineHorizontal(Line line)
        {
            var low = 0;
            var high = 0;
            if (line.Start.x > line.End.x)
            {
                low = line.End.x;
                high = line.Start.x;
            }
            else
            {
                low = line.Start.x;
                high = line.End.x;
            }
            for (int i = low; i <= high; i++)
            {
                grid[i, line.Start.y]++;
            }
        }

        private void PlotPoint(Line line)
        {
            grid[line.Start.x, line.Start.y]++;
        }

        private LineDirection GetLineDirection(Line line)
        {
            if (line.Start.x == line.End.x)
            {
                //east or west
                if (line.Start.y > line.End.y)
                {
                    return LineDirection.s;
                }
                else if (line.Start.y < line.End.y)
                {
                    return LineDirection.n;
                }
                else return LineDirection.x;
            }
            else if (line.Start.y == line.End.y)
            {
                if (line.Start.x > line.End.x)
                {
                    return LineDirection.e;
                }
                else if (line.Start.x < line.End.x)
                {
                    return LineDirection.w;
                }
                else return LineDirection.x;
            }
            else
            {
                //is diagonal, do not use
                return LineDirection.d;
            }
        }
    }

    internal enum LineDirection
    {
        n,
        s,
        e,
        w,
        x,
        d
    }

    internal class Point
    {
        public int x { get; set; }
        public int y { get; set; }
    }
    internal class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public LineDirection? Direction { get; set; }
    }
}
