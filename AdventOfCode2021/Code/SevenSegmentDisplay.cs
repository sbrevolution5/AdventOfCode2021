﻿using System;
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
        public static int GetSumOfOutputs(string input)
        {
            var signalDisplays = ParseInput(input);
            FindAll1478(signalDisplays);


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
        public Screen Screen { get; set; } = new();
    }

    public class Screen
    {
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
