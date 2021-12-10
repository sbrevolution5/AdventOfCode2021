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
                totalScore++;
            }
            totalScore = syntaxLines.Sum(l => l.ErrorScore);
            return totalScore;
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
                            return 3;
                        }
                        break;
                    case '>':
                        prev = syntax.Pop();
                        if (prev != '<')
                        {
                            return 25137;
                        }
                        break;
                    case '}':
                        prev = syntax.Pop();
                        if (prev != '{')
                        {
                            return 1197;
                        }
                        break;
                    case ']':
                        prev = syntax.Pop();
                        if (prev != '[')
                        {
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
