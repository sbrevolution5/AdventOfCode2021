using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class SyntaxFixer
    {
        public static int FindSyntaxErrorScore(string input)
        {
            List<SyntaxLine> syntaxLines = ParseInput(input);
            var totalScore = 0;
            foreach (var line in syntaxLines)
            {
                line.ErrorScore = GetErrorScore(line);
            }
            totalScore = syntaxLines.Sum(l => l.ErrorScore);
            return totalScore;
        }
        public static long FindMedianIncompleteScore(string input)
        {
            List<SyntaxLine> syntaxLines = ParseInput(input);
            List<long> Scores= new List<long>();
            foreach (var line in syntaxLines)
            {
                line.ErrorScore = GetErrorScore(line);
            }
            List<SyntaxLine> incompleteLines = syntaxLines.Where(s => !s.Corrupted).ToList();
            foreach (var line in incompleteLines)
            {
                Scores.Add(GetIncompleteScore(line));
            }
            Scores.Sort();
            var c = Scores.Count();
            var half = c / 2;
            return Scores[half];
        }

        private static long GetIncompleteScore(SyntaxLine line)
        {
            Stack<char> syntax = new Stack<char>();
            for (int i = 0; i < line.Characters.Count; i++)
            {
                switch (line.Characters[i])
                {
                    case '(':
                    case '{':
                    case '[':
                    case '<':
                        syntax.Push(line.Characters[i]);
                        break;
                    case ')':
                    case '>':
                    case '}':
                    case ']':
                        syntax.Pop();
                        break;
                    default:
                        break;
                }
            }
            long incompleteScore = 0;
            while (syntax.Count > 0)
            {
                switch (syntax.Pop())
                {
                    case '(':
                        incompleteScore *= 5;
                        incompleteScore++;
                        break;
                    case '[':
                        incompleteScore *= 5;
                        incompleteScore += 2;
                        break;
                    case '{':
                        incompleteScore *= 5;
                        incompleteScore += 3;
                        break;
                    case '<':
                        incompleteScore *= 5;
                        incompleteScore += 4;
                        break;
                }
            }
            return incompleteScore;
        }

        private static int GetErrorScore(SyntaxLine line)
        {
            Stack<char> syntax = new Stack<char>();
            for (int i = 0; i < line.Characters.Count; i++)
            {
                char prev;
                switch (line.Characters[i])
                {
                    case '(':
                    case '{':
                    case '[':
                    case '<':
                        syntax.Push(line.Characters[i]);
                        break;
                    case ')':
                        prev = syntax.Pop();
                        if (prev != '(')
                        {
                            line.Corrupted = true;
                            return 3;
                        }
                        break;
                    case '>':
                        prev = syntax.Pop();
                        if (prev != '<')
                        {
                            line.Corrupted = true;
                            return 25137;
                        }
                        break;
                    case '}':
                        prev = syntax.Pop();
                        if (prev != '{')
                        {
                            line.Corrupted = true;
                            return 1197;
                        }
                        break;
                    case ']':
                        prev = syntax.Pop();
                        if (prev != '[')
                        {
                            line.Corrupted = true;
                            return 57;
                        }
                        break;
                    default:
                        break;
                }
            }
            return 0;
        }

        private static List<SyntaxLine> ParseInput(string input)
        {
            var lines = input.Split('\n');
            var result = new List<SyntaxLine>();
            foreach (var line in lines)
            {
                result.Add(new()
                {
                    Characters = line.ToCharArray().ToList(),
                });
            }
            return result;
        }
    }
    internal class SyntaxLine
    {
        public List<char> Characters { get; set; }
        public bool Corrupted { get; set; }
        public int ErrorScore { get; set; }
    }
}
