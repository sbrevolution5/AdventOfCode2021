using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class PacketDecoder
    {
        public static int GetSumOfVersionNumbers(string input)
        {
            var bits = TranslateToByteString(input);
            //Check main packet for type, if its operator, we need to find the sub packets
            bool pType = IsPacketLiteral(bits);
            var packetList = new List<Packet>();
            if (!pType)
            {
                packetList.AddRange(SplitOperatorPacket(bits));
            }
            //if it isn't, just get the version number and return
            else
            {
                return GetPacketVersion(bits);
            }
            var versions = packetList.Select(p => p.PacketVersion);
            return versions.Sum();
        }

        private static int GetPacketVersion(string bits)
        {
            var version = bits.Substring(0, 3);
            return Convert.ToInt32(version, 2);
        }

        private static bool IsPacketLiteral(string bits)
        {
            var type = bits.Substring(3, 3);
            if (type == "100")//4
            {
                return true;
            }

            return false;
        }

        private static List<Packet> SplitOperatorPacket(string packet)
        {
            throw new NotImplementedException();
        }

        private static void FindVersionNumber(Packet packet)
        {

        }

        private static string TranslateToByteString(string input)
        {
            string binarystring = String.Join(String.Empty,
                input.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                    )
                );
            return binarystring;
        }
    }
    internal class Packet
    {
        public bool IsLiteral { get; set; }
        public int PacketVersion { get; set; }
        public string PacketData { get; set; }
    }
}
