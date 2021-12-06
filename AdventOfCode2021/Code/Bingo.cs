using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class Bingo
    {
        public static int GetBingoWinnerScore(string input)
        {
            var game = ParseInput(input);
            while (!game.Boards.Where(b => b.Winner).Any())
            {
                //draw number
                var num = DrawNumber(game);
                //mark number on board
                foreach (var board in game.Boards)
                {
                    if (MarkBoardAndCheckWinner(board, num))
                    {
                        board.Winner = true;

                        board.Score = CalculateScore(board, num);
                    }
                }

            }
            return game.Boards.Where(b => b.Winner).First().Score;
        }
        public static int GetBingoLoserScore(string input)
        {
            var game = ParseInput(input);
            while (game.Boards.Where(b => !b.Winner).Count() > 1)
            {
                //draw number
                var num = DrawNumber(game);
                //mark number on board
                var listToRemove = new List<Board>();
                foreach (var board in game.Boards)
                {
                    if (MarkBoardAndCheckWinner(board, num))
                    {
                        board.Winner = true;

                        board.Score = CalculateScore(board, num);
                        //drop boards which have already won to improve performance
                        listToRemove.Add(board);
                    }
                }
                foreach (var board in listToRemove)
                {
                    game.Boards.Remove(board);
                }

            }
            var finalBoard = game.Boards.First();
            while (!finalBoard.Winner)
            {
                var num = DrawNumber(game);
                if (MarkBoardAndCheckWinner(finalBoard, num))
                {
                    finalBoard.Winner = true;

                    finalBoard.Score = CalculateScore(finalBoard, num);
                }
            }
            
            return finalBoard.Score;
        }

        private static int CalculateScore(Board board, int num)
        {
            var score = 0;
            foreach (var list in board.Numbers)
            {
                foreach (var square in list)
                {
                    if (!square.Marked)
                    {
                        score += square.Contains;
                    }
                }
            }
            return score * num;
        }

        private static bool MarkBoardAndCheckWinner(Board board, int num)
        {

            var found = false;
            var i = 0;
            var j = 0;
            while (!found)
            {
                if (board.Numbers[i][j].Contains == num)
                {
                    board.Numbers[i][j].Marked = true;
                    found = true;

                }
                if (!found)
                {

                    if (j == 4)
                    {
                        i++;
                        if (i> 4)
                        {
                            //end of board
                            return false;
                        }
                        j = 0;
                    }
                    else
                    {
                        j++;
                    }
                }
            }
            return CheckForWinner(board, i, j,num);
        }

        private static bool CheckForWinner(Board board, int i, int j,int num)
        {
            for (int k = 0; k < 5; k++)
            {
                if (!board.Numbers[i][k].Marked)
                {
                    break;
                }
                if (k==4)
                {
                    board.Winner = true;
                    return true;
                }
            }
            for (int k = 0; k < 5; k++)
            {
                if (!board.Numbers[k][j].Marked)
                {
                    break;
                }
                if (k == 4)
                {
                    board.Winner = true;
                    return true;
                }
            }
            return false;
        }

        private static int DrawNumber(BingoGame game)
        {
            var num = game.DrawnNumbers.First();
            game.DrawnNumbers.Remove(num);
            return num;
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
                    board.Numbers.Add(new List<Square>());
                    if (!string.IsNullOrEmpty(boardString[j]))
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            board.Numbers[j].Add(new Square()
                            {

                                Contains = int.Parse(boardString[j].Substring(k * 3, 2).Trim())
                            });
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
        public List<List<Square>> Numbers { get; set; } = new List<List<Square>>();
        public int Score { get; set; }
        public bool Winner { get; set; }

    }

    public class Square
    {
        public int Contains { get; set; }
        public bool Marked { get; set; }
    }
}
