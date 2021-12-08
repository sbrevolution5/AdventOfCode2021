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
    public class PowerConsumptionTests
    {
        [TestMethod()]
        public void GetLifeSupportRatingTest()
        {
            //arrange
            var input = @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";
            //act
            var res = PowerConsumption.GetLifeSupportRating(input);
            //assert
            res.Should().Be(230);
        }

    }
}