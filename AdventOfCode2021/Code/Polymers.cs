using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class Polymers
    {
        public static int GetLengthOfPolymerAfterSteps(string input, int steps)
        {
            string template = GetPolymerTemplate(input);
            List<PolymerRule> rules = GetRules(input);
            string res = "";
            for (int i = 0; i < steps; i++)
            {
                res = PerformStep(template, rules);
                template = res;
            }
            return template.Length;
        }
        public static string GetStringAfterSteps(string input, int steps)
        {
            string template = GetPolymerTemplate(input);
            List<PolymerRule> rules = GetRules(input);
            string res = "";
            for (int i = 0; i < steps; i++)
            {
                res = PerformStep(template, rules);
                template = res;
            }
            return template;
        }
        public static int DifferenceInMostAndLeastCommon(string input)
        {
            string template = GetPolymerTemplate(input);
            List<PolymerRule> rules = GetRules(input);
            string res = "";
            for (int i = 0; i < 10; i++)
            {
                res = PerformStep(template, rules);
                template = res;
            }
            var most = MostCommonElement(res);
            int least = LeastCommonElement(res);
            return most - least;
        }
        public static long DifferenceInMostAndLeastCommonLong(string input)
        {
            string template = GetPolymerTemplate(input);
            List<PolymerRule> rules = GetRules(input);
            string res = "";
            for (int i = 0; i < 40; i++)
            {
                res = PerformStep(template, rules);
                template = res;
            }
            long most = MostCommonElementLong(res);
            long least = LeastCommonElementLong(res);
            return most - least;
        }

        private static long LeastCommonElementLong(string res)
        {
            var grp = res.GroupBy(c => c);
            return grp.Select(c => c.Count()).Min();
        }
        private static int LeastCommonElement(string res)
        {
            var grp = res.GroupBy(c => c);
            return grp.Select(c => c.Count()).Min();
        }

        private static int MostCommonElement(string res)
        {
            var grp = res.GroupBy(c => c);
            return grp.Select(c => c.Count()).Max();
        }
        private static long MostCommonElementLong(string res)
        {
            var grp = res.GroupBy(c => c);
            return grp.Select(c => c.Count()).Max();
        }

        private static string PerformStep(string template, List<PolymerRule> rules)
        {
            //for each rule,
            List<InsertionInstruction> allInsertions = new List<InsertionInstruction>();
            foreach (var rule in rules)
            {
                //find a match
                List<InsertionInstruction> instructions = GetInsertionCommands(template, rule);
                allInsertions.AddRange(instructions);
            }
            allInsertions = allInsertions.OrderBy(i => i.i).ToList();
            var  insertionPairStrings = allInsertions.Select(i => i.c + i.i.ToString()).ToArray();
            var str = string.Join(" ", insertionPairStrings);
            var polymer = template;
            var insertionCount = 0;
            foreach (var instruction in allInsertions)
            {
                polymer = TransformWithInstruction(polymer, instruction, insertionCount);
                insertionCount++;
            }
            return polymer;

        }

        private static string TransformWithInstruction(string polymer, InsertionInstruction instruction, int insertionCount)
        {
            //The problem is when there are multiple instructions, they aren't being inserted properly
            //Why does it think there is a B at 6?!
            var newPoly = polymer.Insert(instruction.i + insertionCount, instruction.c.ToString());
            return newPoly;
        }
        public static List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length-1)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
        private static List<InsertionInstruction> GetInsertionCommands(string template, PolymerRule rule)
        {
            //We're missing an instruction when there are multiple matches
            var originalTemplate = template;
            var commands = new List<InsertionInstruction>();
            var indexes = AllIndexesOf(template, rule.Original);
            var ct = 0;
            foreach (var pos in indexes)
            {
                commands.Add(new InsertionInstruction()
                {
                    c = rule.Insert,
                    i = pos + 1,
                    Rule = rule,
                });
            }
            //var offset = 1;
            //var removedCharacters = 0;
            //var matchCount = 0;
            ////Find pair in string
            //while (template.IndexOf(rule.Original) != -1)//While we can still find original pair
            //{
            //    matchCount++;
            //    //if its found, record the index we found it at
            //    var currentIndex = template.IndexOf(rule.Original);
            //    //create an instruction, that says "put this character at the index + 1, plus the number of removed characters"
            //    var removalIndex = currentIndex + 1 + removedCharacters;
            //    commands.Add(new()
            //    {
            //        c = rule.Insert,
            //        i = removalIndex //if the pair is found at 0, this should be 1
            //    });
            //    //then remove the beginning of that string, so that we won't find the same pair
            //    //update template to remove everything before the current
            //    template = template.Substring(currentIndex + 1);
            //    removedCharacters = +currentIndex + matchCount;
            //    //Find pair again

            //}
            ////if not found, return the list of commands
            return commands;
        }

        private static List<PolymerRule> GetRules(string input)
        {
            var lines = input.Split('\n');
            var instructions = lines.Skip(2);
            var rules = new List<PolymerRule>();
            foreach (var i in instructions)
            {
                var parts = i.Split(" -> ");
                var rule = new PolymerRule()
                {
                    Original = parts[0],
                    Insert = parts[1].ToCharArray().First(),
                };
                rules.Add(rule);
            }
            return rules;
        }

        private static string GetPolymerTemplate(string input)
        {
            var lines = input.Split('\n');
            return lines[0].Trim();
        }
    }

    internal class PolymerRule
    {
        public string Original { get; set; }
        public char Insert { get; set; }
    }
    internal class InsertionInstruction
    {
        public char c { get; set; }
        public int i { get; set; }
        public PolymerRule Rule { get; set; }
    }
}
