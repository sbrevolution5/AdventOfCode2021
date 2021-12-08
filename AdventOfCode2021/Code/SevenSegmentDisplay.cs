using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class SevenSegmentDisplay
    {
        public static int CountEasyOutputs(string input)
        {
            var signalDisplays = ParseInput(input);
            FindOutput1478(signalDisplays);
            var easy = 0;
            foreach (var display in signalDisplays)
            {
                easy += display.OutputWires.Where(w => w.NumberValue.HasValue).Count();
            }
            return easy;
        }
        public static bool DoesEachLineHaveAll4EasyValues(string input)
        {
            var signalDisplays = ParseInput(input);
            FindAll1478(signalDisplays);
            foreach (var display in signalDisplays)
            {
                if (!DoesHaveEasy(display))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool DoesHaveEasy(Display display)
        {
            if (!display.AllWires.Where(w => w.NumberValue == 1).Any())
            {
                return false;
            }
            if (!display.AllWires.Where(w => w.NumberValue == 4).Any())
            {
                return false;
            }
            if (!display.AllWires.Where(w => w.NumberValue == 7).Any())
            {
                return false;
            }
            if (!display.AllWires.Where(w => w.NumberValue == 8).Any())
            {
                return false;
            }
            return true;
        }

        public static int GetSumOfOutputs(string input)
        {
            var signalDisplays = ParseInput(input);
            FindAll1478(signalDisplays);
            var sum = 0;
            foreach (var display in signalDisplays)
            {
                FindDisplayNodes(display);
                if (display.OutputResult is not null)
                {
                    sum += display.OutputResult.Value;
                }
                else
                {
                    throw new InvalidOperationException("A display number could not be found");
                }
            }
            return sum;
        }

        private static void FindDisplayNodes(Display display)
        {
            while (!display.Screen.Done || display.OutputWires.Where(w => w.NumberValue is null).Any())
            {
                if (display.Screen.Top is null)
                {
                    //only do linq if we have to
                    if (display.AllWires.Where(w => w.NumberValue == 1 || w.NumberValue == 7).Count() >= 2)
                    {
                        FindTop(display);
                    }
                }
                else if (display.Screen.RightB is null)
                {
                    if (display.HasAll6sAnd1)
                    {
                        FindBottomRight(display);
                        if (display.Screen.RightT is null)
                        {
                            FindTopRight(display);
                        }
                    }
                }
                else if (display.Screen.LeftB is null)
                {
                    if (display.Screen.RightT is not null)
                    {
                        FindNumbersOf6(display);
                        FindNumbersOf5(display);
                        FindNumbersOf2(display);
                        FindNumbersOf3(display);
                        FindLeftSide(display);
                        FindZeroAndMiddle(display);

                    }
                }

            }
        }

        private static void FindZeroAndMiddle(Display display)
        {
            FindZero(display);
            FindMiddle(display);
        }

        private static void FindMiddle(Display display)
        {
            var candidates = "abcdefg".ToCharArray();
            var possible = new List<char>();
            var number0 = display.AllWires.First(w => w.NumberValue == 0);
            foreach (var possibleChar in candidates)
            {
                var inAll = true;
                foreach (var six in sixes)
                {
                    if (!six.Contains(possibleChar))
                    {
                        inAll = false;
                    }
                }
                if (inAll == true)
                {
                    possible.Add(possibleChar);
                }
            }
        }

        private static void FindZero(Display display)
        {
            if (display.Screen.LeftT is null || display.Screen.LeftB is null)
            {
                throw new ArgumentNullException("Tried to find 0 without Left Side");
            }
            var sixes = display.AllWires.Where(w => w.StringValue.Length == 6);
            foreach (var six in sixes)
            {
                if (!six.NumberValue.HasValue)
                {
                    six.NumberValue = 0;
                }
            }
        }

        private static void FindLeftSide(Display display)
        {
            var missingFrom3 = FindMissingFrom3(display);
            FindTopLeft(display, missingFrom3);
            FindBotLeft(display, missingFrom3);
        }

        private static void FindBotLeft(Display display, char[] missingFrom3)
        {
            var number5 = display.AllWires.First(w => w.NumberValue == 5).StringValue.ToCharArray();
            foreach (var c in missingFrom3)
            {
                if (number5.Contains(c))
                {
                    display.Screen.LeftT = c;
                }
            }
        }

        private static void FindTopLeft(Display display, char[] missingFrom3)
        {
            var number2 = display.AllWires.First(w => w.NumberValue == 2).StringValue.ToCharArray();
            foreach (var c in missingFrom3)
            {
                if (number2.Contains(c))
                {
                    display.Screen.LeftB = c;
                }
            }
        }

        private static char[] FindMissingFrom3(Display display)
        {
            var possibleChar = "abcdefg".ToCharArray();
            var number3 = display.AllWires.First(w => w.NumberValue == 3).StringValue.ToCharArray();
            List<char> missing = new();
            foreach (var c in possibleChar)
            {
                if (!number3.Contains(c))
                {
                    missing.Add(c);
                    if (missing.Count == 2)
                    {
                        break;
                    }
                }
            }
            return missing.ToArray();
        }

        private static void FindNumbersOf3(Display display)
        {
            var numberThrees = display.AllWires.Where(w => w.StringValue.Length == 5 && w.NumberValue is null);
            foreach (var number in numberThrees)
            {
                number.NumberValue = 3;
            }
        }

        private static void FindNumbersOf2(Display display)
        {
            var fives = display.AllWires.Where(w => w.StringValue.Length == 5);
            foreach (var five in fives)
            {
                if (display.Screen.RightB is null)
                {
                    throw new ArgumentNullException("Tried to find 2 without RightB");
                }
                if (!five.StringValue.ToCharArray().Contains(display.Screen.RightB.Value))
                {
                    five.NumberValue = 2;
                }
            }
        }

        private static void FindNumbersOf6(Display display)
        {
            var sixes = display.AllWires.Where(w => w.StringValue.Length == 6);
            foreach (var six in sixes)
            {
                if (display.Screen.RightT is null)
                {
                    throw new ArgumentNullException("Tried to find 6 without RightT");
                }
                if (!six.StringValue.ToCharArray().Contains(display.Screen.RightT.Value))
                {
                    six.NumberValue = 6;
                }
            }
        }
        private static void FindNumbersOf5(Display display)
        {
            var fives = display.AllWires.Where(w => w.StringValue.Length == 5);
            foreach (var five in fives)
            {
                if (display.Screen.RightT is null)
                {
                    throw new ArgumentNullException("Tried to find 5 without RightT");
                }
                if (!five.StringValue.ToCharArray().Contains(display.Screen.RightT.Value))
                {
                    five.NumberValue = 5;
                }
            }
        }

        private static void FindTopRight(Display display)
        {
            var one = display.AllWires.First(w => w.NumberValue == 1).StringValue.ToCharArray();
            foreach (var c in one)
            {
                if (c != display.Screen.RightB)
                {
                    display.Screen.RightT = c;
                    return;
                }
            }
            throw new InvalidOperationException("Top right not found because bottom right wasn't already found");
        }

        private static void FindBottomRight(Display display)
        {
            var candidates = "abcdefg".ToCharArray();
            var possible = new List<char>();
            var sixes = display.AllWires.Where(w => w.StringValue.Length == 6)
                .DistinctBy(w => w.StringValue)
                .Select(w => w.StringValue.ToCharArray());
            foreach (var possibleChar in candidates)
            {
                var inAll = true;
                foreach (var six in sixes)
                {
                    if (!six.Contains(possibleChar))
                    {
                        inAll = false;
                    }
                }
                if (inAll == true)
                {
                    possible.Add(possibleChar);
                }
            }
            var one = display.AllWires.First(w => w.NumberValue == 1).StringValue.ToCharArray();
            foreach (var possibleChar in possible)
            {
                if (one.Contains(possibleChar))
                {
                    display.Screen.RightB = possibleChar;
                    return;
                }
            }
            throw new InvalidOperationException("Tried to find bottom right with 6 character and confirmed 1, but was unsuccessful");
        }

        private static void FindTop(Display display)
        {
            var one = display.AllWires.First(w => w.NumberValue == 1).StringValue.ToCharArray();
            var seven = display.AllWires.First(w => w.NumberValue == 7).StringValue.ToCharArray();
            foreach (var c in seven)
            {
                if (!one.Contains(c))
                {
                    display.Screen.Top = c;
                }
            }
        }

        private static void FindAll1478(List<Display> signalDisplays)
        {
            FindOutput1478(signalDisplays, true);
            FindOutput1478(signalDisplays, false);
        }

        private static void FindOutput1478(List<Display> displays, bool isOutput = true)
        {
            foreach (var display in displays)
            {
                var wireList = new List<Wire>();
                if (isOutput)
                {
                    wireList = display.OutputWires;
                }
                else
                {
                    wireList = display.InputWires;
                }
                foreach (var wire in wireList)
                {
                    if (wire.StringValue.Length == 3)
                    {
                        wire.NumberValue = 7;
                    }
                    else if (wire.StringValue.Length == 2)
                    {
                        wire.NumberValue = 1;
                    }
                    else if (wire.StringValue.Length == 4)
                    {
                        wire.NumberValue = 4;
                    }
                    else if (wire.StringValue.Length == 7)
                    {
                        wire.NumberValue = 8;
                    }
                }
            }
        }

        private static List<Display> ParseInput(string input)
        {
            var lines = input.Split('\n');
            var res = new List<Display>();
            foreach (var line in lines)
            {
                var halves = line.Split('|');
                var disp = new Display();
                var inputs = halves[0].Split(' ');
                foreach (var i in inputs)
                {
                    if (!string.IsNullOrEmpty(i))
                    {
                        disp.InputWires.Add(new()
                        {
                            StringValue = i
                        });
                    }
                }
                var outputs = halves[1].Split(' ');
                foreach (var i in outputs)
                {
                    if (!string.IsNullOrEmpty(i))
                    {
                        disp.OutputWires.Add(new()
                        {
                            StringValue = i.Trim()
                        });
                    }
                }
                res.Add(disp);
            }
            return res;
        }
    }

    public class Display
    {
        public List<Wire> InputWires { get; set; } = new();
        public List<Wire> OutputWires { get; set; } = new();
        public List<Wire> AllWires
        {
            get
            {
                var res = new List<Wire>();
                res.AddRange(InputWires);
                res.AddRange(OutputWires);
                return res;
            }
        }
        public Screen Screen { get; set; } = new();
        public int? OutputResult { get; internal set; }
        public bool HasAll6sAnd1
        {
            get
            {
                if (AllWires.Where(w => w.StringValue.Length == 6).DistinctBy(w => w.StringValue).Count() == 3)
                {
                    if (AllWires.Where(w => w.NumberValue == 1).Any())
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }

    public class Screen
    {
        public bool Done
        {
            get
            {
                return RightB is not null && LeftB is not null && RightT is not null && LeftT is not null && Top is not null && Bot is not null && Mid is not null;
            }
        }
        public char? Top { get; set; }
        public char? LeftT { get; set; }
        public char? LeftB { get; set; }
        public char? RightT { get; set; }
        public char? RightB { get; set; }
        public char? Bot { get; set; }
        public char? Mid { get; set; }


    }

    public class Wire
    {
        public string StringValue { get; set; }
        public int? NumberValue { get; set; }
    }
}
