using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class Polymers
    {
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

        private static int LeastCommonElement(string res)
        {
            throw new NotImplementedException();
        }

        private static int MostCommonElement(string res)
        {
            throw new NotImplementedException();
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
            polymer.Insert(instruction.i+insertionCount,instruction.c.ToString());
            return polymer;
        }

        private static List<InsertionInstruction> GetInsertionCommands(string template, PolymerRule rule)
        {
            var commands = new List<InsertionInstruction>();
            var offset = 0;
            while (template.IndexOf(rule.Original) != -1)
            {
                var currentIndex = template.IndexOf(rule.Original);
                commands.Add(new()
                {
                    c = rule.Insert,
                    i = currentIndex + offset
                });
                offset += currentIndex;
                template = template.Substring(offset);
            }
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
    }
}
