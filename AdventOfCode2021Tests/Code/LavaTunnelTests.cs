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
    public class LavaTunnelTests
    {
        [TestMethod()]
        public void Find3LargestBasinsTest()
        {
            var input = @"2199943210
3987894921
9856789892
8767896789
9899965678";
            var res = LavaTunnel.Find3LargestBasins(input);
            res.Should().Be(1134);
        }
    }
}