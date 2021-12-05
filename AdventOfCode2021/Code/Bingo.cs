using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    internal static class Bingo
    {
        public static int GetBingoWinnerScore(string input)
        {
            var game = ParseInput(input);
            return 0;
        }
        private static BingoGame ParseInput(string input)
        {
            var lines = input.Split('\n');
            var nums = lines[0].Split(',').Select(i => int.Parse(i)).ToList();
            var boardlines = lines.Skip(2).ToArray();
            var boards = new List<string[]>();
            while (boardlines.Length >= 5)
            {
                boards.Add(boardlines.Take(5).ToArray());
                boardlines = boardlines.Skip(6).ToArray();
            }
            var game = new BingoGame()
            {
                DrawnNumbers = nums,
            };
            foreach (var boardString in boards)
            {
                var board = new Board();
                for (int j = 0; j < 5; j++)
                {
                    if (!string.IsNullOrEmpty(boardString[j]))
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            board.Numbers[j, k] = int.Parse(boardString[j].Substring(k * 3, 2).Trim());
                        }
                    }
                }
                game.Boards.Add(board);
            }
            return game;
        }
    }

    internal class BingoGame
    {
        public List<int> DrawnNumbers { get; set; } = new List<int>();
        public List<Board> Boards { get; set; } = new List<Board>();
    }

    public class Board
    {
        public int[,] Numbers { get; set; } = new int[5, 5];
        public int Score { get; set; } = 0;
        public bool Winner { get; set; }

    }
}
