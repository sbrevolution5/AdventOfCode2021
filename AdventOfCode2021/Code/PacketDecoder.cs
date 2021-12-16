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
            //if it isn't, just get the version number and return
            
            //Determine packet hierarchy
            Packet packet = new Packet();
            packet.PacketData = bits;
            //Find packet type
            if (!packet.IsLiteral)
            {
                List<Packet> subpackets = SplitOperatorPacket(packet);
            }
            //find version number of each packet
            FindVersionNumber(packet);
            //add version numbers together
            GetPacketValue(packet);
            throw new NotImplementedException();
        }

        private static List<Packet> SplitOperatorPacket(Packet packet)
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
