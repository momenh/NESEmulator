using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.Memory
{
    class MemoryUtility
    {
        /// <summary>
        /// convert 2 bytes to ushort base on the current cpu architecture
        /// 
        /// NOTE:
        /// - The 6502 is a little endian processor.
        /// </summary>
        /// <param name="LSB">least significant byte</param>
        /// <param name="MSB">most significant byte</param>
        /// <returns></returns>
        public static ushort readUShort(byte LSB, byte MSB)
        {
            byte[] data = new byte[2];
            
            if (BitConverter.IsLittleEndian)
            {
                data[0] = LSB;
                data[1] = MSB;
            }
            else
            {
                data[0] = MSB;
                data[1] = LSB;
            }

            return BitConverter.ToUInt16(data, 0);
        }

        /// <summary>
        /// split usign short to it's 2 bytes based on the current cpu architecture
        /// 
        /// NOTE:
        /// - The 6502 is a little endian processor.
        /// </summary>
        /// <param name="value">value to split</param>
        /// <param name="LSB">least significant byte</param>
        /// <param name="MSB">most significant byte</param>
        public static void splitUShort(ushort value,out byte LSB, out byte MSB)
        {
            byte[] data = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                LSB = data[0];
                MSB = data[1];
            }
            else
            {
                MSB = data[0];
                LSB = data[1];
            }
        }
    }
}
