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
    public class RadarReaderTests
    {
        [TestMethod()]
        public void HowManyIncreasesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void HowManySlidingIncreasesTest()
        {
            //arrange
            var input = @"199
200
208
210
200
207
240
269
260
263";
            //act
            var res = RadarReader.HowManySlidingIncreases(input);
            res.Should().Be(5);
        }
    }
}