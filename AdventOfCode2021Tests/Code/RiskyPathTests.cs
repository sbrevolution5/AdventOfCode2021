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
    public class RiskyPathTests
    {
        [TestMethod()]
        public void LeastRiskyValueTest()
        {
            var input = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";
            var output = RiskyPath.LeastRiskyValue(input);
            output.Should().Be(40);
        }

        [TestMethod()]
        public void LeastRiskyValueBigCaveTest()
        {
            var input = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";
            var output = RiskyPath.LeastRiskyValueBigCave(input);
            output.Should().Be(315);
        }
        
    }
}