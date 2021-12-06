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
    public class LanternfishTests
    {
        [TestMethod()]
        public void HowManyFishTest()
        {
            var input = "3,4,3,1,2";
            var res = Lanternfish.HowManyFish(input, 18);
            res.Should().Be(26);
        }
    }
}