using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class Origami
    {
        public static int HowManyDotsAfter1Fold(string input)
        {
            Paper paper = ParseInput(input);
            var fold = paper.Folds.First();
            if (!fold.Vert)
            {
                FoldLeft(paper,fold);
            }
            else
            {
                FoldUp(paper,fold);
            }
            return paper.Points.Count;
        }
        public static void PrintCapitalResult(string input)
        {
            Paper paper = ParseInput(input);
            while (paper.Folds.Count > 0)
            {
                var fold = paper.Folds.First();
                paper.Folds.Remove(fold);
                if (!fold.Vert)
                {
                    FoldLeft(paper, fold);
                }
                else
                {
                    FoldUp(paper, fold);
                }
            }
            ShowGrid(paper);
        }

        private static void ShowGrid(Paper paper)
        {
            bool[,] grid = new bool[paper.Width,paper.Height];
            foreach (var point in paper.Points)
            {
                grid[point.X,point.Y] = true;
            }
            for (int i = 0; i < paper.Width; i++)
            {
                for (int j = 0; j < paper.Height; j++)
                {
                    if (grid[i,j] == true)
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.Write('\n');
            }
        }

        private static void FoldLeft(Paper paper, Fold fold)
        {
            var max = fold.Value;
            var oldPaper = paper;
            paper.Width = max;
            foreach (var pt in oldPaper.Points)
            {
                if (pt.X > max)
                {
                    //is below fold
                    var change = pt.X - max;
                    pt.X = max - change;
                }
            }
            RemoveDuplicatePoints(paper);
        }

        private static void FoldUp(Paper paper, Fold fold)
        {
            var max = fold.Value;
            var oldPaper = paper;
            paper.Height = max;
            foreach (var pt in oldPaper.Points)
            {
                if (pt.Y >max)
                {
                    //is below fold
                    var change = pt.Y - max;
                    pt.Y = max - change;
                }
            }
            RemoveDuplicatePoints(paper);
        }

        private static void RemoveDuplicatePoints(Paper paper)
        {
            var points = paper.Points.ToList();
            foreach (var pt in points)
            {
                if (paper.Points.Where(p=>p.X == pt.X && p.Y ==pt.Y).Count()> 1)
                {
                    paper.Points.Remove(pt);
                }
            }
        }

        private static Paper ParseInput(string input)
        {
            var halfs = input.Split("\n\r\n");
            var lines = halfs[0].Split('\n');
            var foldlines = halfs[1].Split('\n');
            var res = new Paper();
            var points = new List<PaperPoint>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }
                var pt = line.Split(',');
                points.Add(new()
                {
                    Y = int.Parse(pt[0]),
                    X = int.Parse(pt[1])
                });
            }
            var folds = new List<Fold>();
            foreach (var item in foldlines)
            {
                var words = item.Split(' ');
                var foldPart=words[2].Split('=');
                folds.Add(new Fold()
                {
                    Value = int.Parse(foldPart[1]),
                    Vert = foldPart[0] == "x" ? true : false
                });
            }
            res.Folds = folds;
            res.Points = points;
            res.Width = points.Select(p=>p.Y).Max();
            res.Height = points.Select(p=>p.X).Max();
            return res;
        }
    }

    internal class Paper
    {
        public List<PaperPoint> Points { get; set; } = new List<PaperPoint>();
        public List<Fold> Folds { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
    internal class PaperPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Fold
    {
        public bool Vert { get; set; }
        public int Value { get; set; }
    }
}
