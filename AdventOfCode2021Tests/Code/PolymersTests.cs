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
    public class PolymersTests
    {
        [DataTestMethod()]
        [DataRow(1,7)]
        [DataRow(2,13)]
        [DataRow(3,25)]
        [DataRow(4,49)]
        [DataRow(5,97)]
        [DataRow(10,3073)]
        public void TestProperLength(int steps, int expected)
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
            var res = Polymers.GetLengthOfPolymerAfterSteps(input, steps);
            res.Should().Be(expected);
        }
        [DataTestMethod()]
        [DataRow(1, "NCNBCHB")]
        [DataRow(2, "NBCCNBBBCBHCB")]
        [DataRow(3, "NBBBCNCCNBBNBNBBCHBHHBCHB")]
        [DataRow(4, "NBBNBNBBCCNBCNCCNBBNBBNBBBNBBNBBCBHCBHHNHCBBCBHCB")]
        public void TestProperString(int steps, string expected)
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
            var res = Polymers.GetStringAfterSteps(input, steps);
            res.Should().Be(expected);
        }
        [TestMethod()]
        public void DifferenceInMostAndLeastCommonTest()
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
            var res = Polymers.DifferenceInMostAndLeastCommon(input);
            res.Should().Be(1588);
        }
        [TestMethod()]
        public void DoubleMatchTest()
        {
            var input = @"NNCHNN

NN -> B";
            var res = Polymers.DifferenceInMostAndLeastCommon(input);
            res.Should().Be(3);
        }
    }
}