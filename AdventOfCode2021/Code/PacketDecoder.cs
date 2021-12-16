using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class PacketDecoder
    {
        internal static List<Packet> allPackets { get; set; } = new List<Packet>();
        public static int GetSumOfVersionNumbers(string input)
        {
            allPackets.Clear();
            var bits = TranslateToByteString(input);


            Packet packet = MakePacket(bits);
            if (packet.IsLiteral)
            {
                return packet.PacketVersion;
            }

            allPackets = allPackets.OrderBy(x => x.PacketData.Length).ToList();
            var versions = allPackets.Select(p => p.PacketVersion);
            return versions.Sum();

            throw new ApplicationException("Input not long enough!");
        }

        //private static List<Packet> MakeHierarchyList(Packet packet)
        //{
        //    var list = new List<Packet>();
        //    //if packet is literal it has no subpackets, add to list, otherwise find its subpackets
        //    if (packet.IsLiteral)
        //    {
        //        list.Add(packet);
        //        return list;
        //    }
        //    else
        //    {
        //        list.Add(packet);
        //        foreach (var p in packet.SubPacketList)
        //        {
        //            list.AddRange(MakeHierarchyList(p));
        //        }
        //        return list;
        //    }

        //}

        private static void ReadLiteralPacket(Packet packet)
        {
            var valueData = packet.PacketData.Substring(6);
            var binString = "";
            var pLength = 6;
            while (valueData[0] != '0')
            {
                var intValue = valueData.Substring(1, 4);
                binString += intValue;
                valueData = valueData.Substring(5);
                pLength += 5;
            }
            var lastIntValue = valueData.Substring(1, 4);
            binString += lastIntValue;
            pLength += 5;
            packet.Value = Convert.ToInt32(binString, 2);
            packet.PacketStringLength = pLength;
            packet.PacketData = packet.PacketData.Substring(pLength);//Set data to only what was read
        }

        private static Packet MakePacket(string bits)
        {
            //Check main packet for type, if its operator, we need to find the sub packets
            var packet = new Packet();
            packet.PacketData = bits;
            packet.IsLiteral = IsPacketLiteral(bits);
            packet.PacketVersion = GetPacketVersion(packet.PacketData);
            if (!packet.IsLiteral)
            {
                packet.SubPacketList.AddRange(SplitOperatorPacket(packet));
                //set packet length to sum of all subpacket lengths
                packet.PacketStringLength = packet.SubPacketList.Select(x => x.PacketStringLength).Sum();
            }
            else
            {
                ReadLiteralPacket(packet);
            }
            allPackets.Add(packet);
            return packet;
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

        private static List<Packet> SplitOperatorPacket(Packet packet)
        {
            //cut out 6 characters for version and type
            var remainingPacketData = packet.PacketData.Substring(6);
            //Find Packet Length
            //if it isn't long enough, return empty list
            if (remainingPacketData.Length < 12)
            {
                return new List<Packet>();
            }
            if (remainingPacketData[0] == '0')
            {
                //For a 0 in length type id
                packet.IsSubPacketNumber = false;
                //next 15 bits is an int that represents total bit length of all sub packets within
                packet.PacketLengthData = remainingPacketData.Substring(1, 15);
                packet.LengthNumber = HowLongIsPacket(packet.PacketLengthData);
                //Parse the subpackets
                //Remove length value and Length type

                var subpacketData = remainingPacketData.Substring(16);
                var parsedLength = 0;
                var subPacketList = new List<Packet>();
                //read next packet
                //do this until we reach sub packet length
                while (parsedLength < packet.LengthNumber && subpacketData.Length >= 6)
                {
                    var newPacket = MakePacket(subpacketData);
                    subPacketList.Add(newPacket);
                    //Remove characters up until new packet's length
                    subpacketData = subpacketData.Substring(newPacket.PacketStringLength);
                    parsedLength += newPacket.PacketStringLength;
                }
                return subPacketList;
            }
            else if (remainingPacketData[0] == '1')
            {
                //for a 1 in length type id
                packet.IsSubPacketNumber = true;
                //next 11 are the number of subpackets contained
                packet.PacketLengthData = remainingPacketData.Substring(1, 11);
                packet.LengthNumber = HowLongIsPacket(packet.PacketLengthData);
                //Parse the subpackets
                //Remove length value and Length type
                var subpacketData = remainingPacketData.Substring(12);
                var packetCount = 0;
                var subPacketList = new List<Packet>();
                //read next packet
                //do this until we reach sub packet length
                while (packetCount < packet.LengthNumber && subpacketData.Length >= 6)
                {
                    var newPacket = MakePacket(subpacketData);
                    subPacketList.Add(newPacket);
                    //Remove parsed packet's length
                    subpacketData = subpacketData.Substring(newPacket.PacketStringLength);
                    packetCount++;
                }
                return subPacketList;
            }
            throw new ApplicationException("INVALID INPUT ERROR!");

        }

        private static int HowLongIsPacket(string? packetLengthData)
        {
            return Convert.ToInt32(packetLengthData, 2);
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
    
}
