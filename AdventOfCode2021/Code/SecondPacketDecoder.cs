using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class SecondPacketDecoder
    {
        public static int GetOperationResult(string input)
        {
            var packets = DecodeAllPackets(input);

        }
        public static int GetVersionSum(string input)
        {
            var packets = DecodeAllPackets(input);
            return packets.Select(p => p.PacketVersion).Sum();
        }
        public static List<Packet> DecodeAllPackets(string input)
        {

            var bytes = TranslateToByteString(input);
            var top = DecodeTopPacket(bytes).First();
            var allPackets = MakeHierarchy(top);
            return allPackets;
        }

        private static List<Packet> MakeHierarchy(Packet top)
        {
            var res = new List<Packet>();
            var topPacket = top;
            res.Add(topPacket);
            if (topPacket.SubPacketList.Any())
            {
                foreach (var p in topPacket.SubPacketList)
                {

                    res.AddRange(MakeHierarchy(p));
                }
            }
            return res;
        }

        public static List<Packet> DecodeTopPacket(string bytes)
        {
            //If its a literal, make a literal packet,
            var packets = new List<Packet>();
            if (bytes.Substring(3, 3) == "100")
            {
                packets.Add(MakeLiteralPacket(bytes));
            }
            //if its not a literal, We need to decode the packets within
            else
            {
                packets.Add(MakeOpPacket(bytes));
            }
            return packets;
        }
        public static Packet DecodeOnePacket(string bytes)
        {
            //If its a literal, make a literal packet,
            if (bytes.Substring(3, 3) == "100")
            {
                return MakeLiteralPacket(bytes);
            }
            //if its not a literal, We need to decode the packets within
            else
            {
                return MakeOpPacket(bytes);
            }
        }

        private static Packet MakeLiteralPacket(string bytes)
        {
            Packet packet = new Packet();
            packet.TypeId = 4;
            packet.PacketData = bytes;
            packet.IsLiteral = true;
            packet.PacketVersion = Convert.ToInt32(bytes.Substring(0, 3), 2);
            packet.PacketValueData = bytes.Substring(6);
            var remainingData = packet.PacketValueData;
            var byteCount = 1;
            while (remainingData[0] != '0')
            {
                byteCount++;
                remainingData = remainingData.Substring(5);//Remove the indicator and 4 values
            }
            //Process all counted bytes,
            var byteString = "";
            for (int i = 0; i < byteCount; i++)
            {
                byteString += packet.PacketValueData.Substring(i * 5 + 1, 4);
            }
            packet.Value = Convert.ToInt64(byteString, 2);
            packet.ValueString = byteString;
            packet.PacketStringLength = byteString.Length + byteCount + 6;
            packet.PacketData = packet.PacketData.Substring(0, packet.PacketStringLength);
            return packet;
        }

        private static Packet MakeOpPacket(string bytes)
        {
            var packet = new Packet();
            //removes op packet header (version num, type, lengthType, and lengthValue), sets those values and calls DecodePackets on the rest 
            //Which lengthtype do we have
            packet.TypeId = Convert.ToInt32(bytes.Substring(3, 3), 2);
            packet.PacketData = bytes;
            packet.PacketVersion = Convert.ToInt32(bytes.Substring(0, 3), 2);
            var lengthBit = bytes[6];
            var remainingBytes = bytes.Substring(7);
            if (lengthBit == '0')
            {
                int charNum = Convert.ToInt32(remainingBytes.Substring(0, 15), 2);
                remainingBytes = remainingBytes.Substring(15);
                packet.SubPacketList = DecodeCharPackets(remainingBytes, charNum);

                packet.PacketStringLength = 15 + 7 + packet.SubPacketList.Select(x => x.PacketStringLength).Sum();
            }
            else //Lengthtype is count
            {
                int packetNum = Convert.ToInt32(remainingBytes.Substring(0, 11), 2);
                remainingBytes = remainingBytes.Substring(11);
                //Find this many bytes in the rest of the string
                packet.SubPacketList = DecodeNumPackets(remainingBytes, packetNum);
                //Set length of this packet to the sum of 11+any sub packets
                packet.PacketStringLength = 11 +7 + packet.SubPacketList.Select(x => x.PacketStringLength).Sum();
            }
            return packet;
        }

        private static List<Packet> DecodeNumPackets(string currentBytes, int packetNum)
        {
            var res = new List<Packet>();
            for (int i = 0; i < packetNum; i++)
            {
                if (currentBytes.Length > 6)
                {
                    var newPacket = DecodeOnePacket(currentBytes);
                    res.Add(newPacket);
                    currentBytes = currentBytes.Substring(newPacket.PacketStringLength);
                }
            }
            return res;
        }

        private static List<Packet> DecodeCharPackets(string ourBytes, int charNum)
        {
            var res = new List<Packet>();
            var bitsRead = 0;
            ourBytes = ourBytes.Substring(0, charNum);
            while (bitsRead < charNum)
            {
                if (ourBytes.Length > 6)
                {
                    var newPacket = DecodeOnePacket(ourBytes);
                    res.Add(newPacket);
                    bitsRead += newPacket.PacketStringLength;
                    ourBytes = ourBytes.Substring(newPacket.PacketStringLength);
                }
                else
                {
                    break;
                }
            }
            return res;
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
    public class Packet
    {
        /// <summary>
        /// True means literal, false means operation
        /// </summary>
        public bool IsLiteral { get; set; }
        /// <summary>
        /// Number of version
        /// </summary>
        public int PacketVersion { get; set; }
        /// <summary>
        /// Full string of packet
        /// </summary>
        public string PacketData { get; set; } = "";
        public bool IsSubPacketNumber { get; set; }
        public int LengthNumber { get; set; }
        public string PacketLengthData { get; set; } = "";
        public int PacketStringLength { get; set; }
        public long? Value { get; set; }
        public List<Packet> SubPacketList { get; set; } = new List<Packet>();
        public string PacketValueData { get; internal set; }
        public string ValueString { get; internal set; }
        public int TypeId { get; set; }
    }
}
