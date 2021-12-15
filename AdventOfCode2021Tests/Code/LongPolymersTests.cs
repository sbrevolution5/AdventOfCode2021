using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace AdventOfCode2021.Code.Tests
{
    [TestClass()]
    public class LongPolymersTests
    {
        [TestMethod()]
        public void GetDifferenceAfter40StepsTest()
        {
            var input = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";
            var res = LongPolymers.GetDifferenceAfter40Steps(input);
            res.Should().Be(2188189693529);
        }
    }
}