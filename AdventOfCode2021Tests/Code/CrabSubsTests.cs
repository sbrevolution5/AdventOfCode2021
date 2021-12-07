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
    public class CrabSubsTests
    {
        [TestMethod()]
        public void FuelForOneLineTest()
        {
            var input = "16,1,2,0,4,2,7,1,2,14";
            var res = CrabSubs.FuelForOneLine(input);
            res.Should().Be(168);
        }
    }
}