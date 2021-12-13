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
    public class CavePathsTests
    {
        [TestMethod()]
        public void HowManyPathsTest()
        {
            var input = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";
            var res = CavePaths.HowManyPaths(input);
            res.Should().Be(36);
        }
        [TestMethod()]
        public void HowManySmallTest()
        {
            var input = @"start-a
start-b
a-c 
b-c 
c-end";
            var res = CavePaths.HowManyPaths(input);
            res.Should().Be(2);
        }
    }
}