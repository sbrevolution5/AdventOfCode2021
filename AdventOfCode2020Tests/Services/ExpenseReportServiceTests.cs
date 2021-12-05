using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2020.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace AdventOfCode2020.Services.Tests
{
    [TestClass()]
    public class ExpenseReportServiceTests
    {
        [TestMethod()]
        public void Get2020ProductTest()
        {
            //arrange
            var input = @"1721
979
366
299
675
1456";
            //act
            var res = ExpenseReportService.Get2020Product(input);
            //assert
            res.Should().Be(241861950);
        }
    }
}