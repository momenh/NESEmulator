using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    /// <summary>
    /// The instructions perform logical operations on the contents of the accumulator and another value held in memory. 
    /// The BIT instruction performs a logical AND to test the presence of bits in the memory value to set the
    /// flags but does not keep the result.
    /// </summary>
    class Logical
    {
        /// <summary>
        /// A logical AND is performed, bit by bit, on the accumulator contents using the contents of a byte of memory.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class AND
        {
            static public class Immediat
            {
                public const byte OPcode = 41; //0x29
                public const int Bytes = 2;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 37; //0x25
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 53; //0x35
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 45; //0x2D
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 61; //0x3D
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteY
            {
                public const byte OPcode = 57; //0x39
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class IndirectX
            {
                public const byte OPcode = 33; //0x21
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class IndirectY
            {
                public const byte OPcode = 49; //0x31
                public const int Bytes = 2;
                public const int Cycles = 5;
            }
        }

        /// <summary>
        /// An exclusive OR is performed, bit by bit, on the accumulator contents using the contents of a byte of memory.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class EOR
        {
            static public class Immediat
            {
                public const byte OPcode = 73; //0x49
                public const int Bytes = 2;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 69; //0x45
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 85; //0x55
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 77; //0x4D
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 93; //0x5D
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteY
            {
                public const byte OPcode = 89; //0x59
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class IndirectX
            {
                public const byte OPcode = 65; //0x41
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class IndirectY
            {
                public const byte OPcode = 81; //0x51
                public const int Bytes = 2;
                public const int Cycles = 5;
            }
        }

        /// <summary>
        /// An inclusive OR is performed, bit by bit, on the accumulator contents using the contents of a byte of memory.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class ORA
        {
            static public class Immediat
            {
                public const byte OPcode = 9; //0x09
                public const int Bytes = 2;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 5; //0x05
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 21; //0x15
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 13; //0x0D
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 29; //0x1D
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteY
            {
                public const byte OPcode = 25; //0x19
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class IndirectX
            {
                public const byte OPcode = 1; //0x01
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class IndirectY
            {
                public const byte OPcode = 17; //0x11
                public const int Bytes = 2;
                public const int Cycles = 5;
            }
        }

        /// <summary>
        /// This instructions is used to test if one or more bits are set in a target memory location. 
        /// The mask pattern in A is ANDed with the value in memory to set or clear the zero flag, 
        /// but the result is not kept. Bits 7 and 6 of the value from memory are copied into the N and V flags.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// Overflow Flag
        /// </summary>
        static public class BIT
        {
            static public class ZeroPage
            {
                public const byte OPcode = 36; //0x24
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class Absolute
            {
                public const byte OPcode = 44; //0x2C
                public const int Bytes = 3;
                public const int Cycles = 4;
            }
        }
    }
}
