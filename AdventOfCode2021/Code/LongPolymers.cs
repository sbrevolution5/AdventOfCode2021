using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class LongPolymers
    {
        public static long GetDifferenceAfter40Steps(string input)
        {
            string template = GetPolymerTemplate(input);
            Dictionary<string, string> ruleDict = GetRuleDict(input);
            //Log each pair in dictionary
            var pairs = new Dictionary<string, long>();
            //log each single letter count in dictionary
            var singles = new Dictionary<string, long>();
            var ruleHits = new Dictionary<string, long>();
            foreach (var c in template)
            {
                DictIncrement(singles, c.ToString().Trim());
            }
            for (int i = 0; i < template.Length - 1; i++)
            {
                DictIncrement(pairs, template.Substring(i, 2).Trim());
            }
            for (int i = 0; i < 40; i++)
            {
                ruleHits.Clear();
                //find all hits
                foreach (var pair in pairs.Keys)
                {
                    if (ruleDict[pair] != null)
                    {
                        DictIncrement(ruleHits, pair, pairs[pair]);
                    }
                }
                //apply hits
                foreach (var hit in ruleHits)
                {
                    var charToInsert = ruleDict[hit.Key];
                    //letter count goes up
                    DictIncrement(singles, charToInsert, hit.Value);
                    //pair count drops, as it was replaced
                    DictIncrement(pairs, hit.Key, -hit.Value);
                    //add new pairs
                    var l = hit.Key[0] + charToInsert;
                    var r = charToInsert + hit.Key[1];
                    DictIncrement(pairs, l, hit.Value);
                    DictIncrement(pairs, r, hit.Value);
                }
            }
            long most = singles.OrderByDescending(s => s.Value).FirstOrDefault().Value;
            long least = singles.OrderBy(s => s.Value).FirstOrDefault().Value;
            return most - least;
        }

        private static Dictionary<string, string> GetRuleDict(string input)
        {
            var dict = new Dictionary<string, string>();
            var lines = input.Split('\n').Skip(2);
            foreach (var line in lines)
            {
                var parts = line.Trim().Split(" -> ");
                dict[parts[0]] = parts[1];
            }
            return dict;
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
        public static void DictIncrement<T>(Dictionary<T, long> d, T key, long num = 1)
        {
            if (d.Keys.Contains(key)) d[key] += num;
            else d[key] = num;
        }
    }


}

