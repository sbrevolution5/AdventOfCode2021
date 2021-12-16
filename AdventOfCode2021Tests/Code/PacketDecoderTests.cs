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
    public class PacketDecoderTests
    {
        [DataTestMethod()]
        [DataRow("8A004A801A8002F478",16)]
        [DataRow("620080001611562C8802118E34", 12)]
        [DataRow("C0015000016115A2E0802F182340", 23)]
        [DataRow("A0016C880162017C3686B18A3D4780", 31)]
        public void GetSumOfVersionNumbersTest(string input, int expected)
        {
            var res = PacketDecoder.GetSumOfVersionNumbers(input);
            res.Should().Be(expected);
        }
    }
}