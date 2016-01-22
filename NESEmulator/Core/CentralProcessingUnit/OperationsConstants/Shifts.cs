using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    class Shifts
    {
        /// <summary>
        /// This operation shifts all the bits of the accumulator or memory contents one bit left. 
        /// Bit 0 is set to 0 and bit 7 is placed in the carry flag. 
        /// The effect of this operation is to multiply the memory contents by 2 (ignoring 2's complement considerations), 
        /// setting the carry if the result will not fit in 8 bits.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class ASL
        {
            static public class Accumelator
            {
                public const byte OPcode = 10; //0x0A
                public const int Bytes = 1;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 6; //0x06
                public const int Bytes = 2;
                public const int Cycles = 5;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 22; //0x16
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class Absolute
            {
                public const byte OPcode = 14; //0x0E
                public const int Bytes = 3;
                public const int Cycles = 6;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 30; //0x1E
                public const int Bytes = 3;
                public const int Cycles = 7;
            }
        }

        /// <summary>
        /// This operation shifts all the bits of the accumulator or memory contents one bit left. 
        /// Bit 0 is set to 0 and bit 7 is placed in the carry flag. 
        /// The effect of this operation is to multiply the memory contents by 2 (ignoring 2's complement considerations), 
        /// setting the carry if the result will not fit in 8 bits.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class LSR
        {
            static public class Accumelator
            {
                public const byte OPcode = 74; //0x4A
                public const int Bytes = 1;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 70; //0x46
                public const int Bytes = 2;
                public const int Cycles = 5;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 86; //0x56
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class Absolute
            {
                public const byte OPcode = 78; //0x4E
                public const int Bytes = 3;
                public const int Cycles = 6;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 94; //0x5E
                public const int Bytes = 3;
                public const int Cycles = 7;
            }
        }

        /// <summary>
        /// Move each of the bits in either A or M one place to the left. 
        /// Bit 0 is filled with the current value of the carry flag whilst the old bit 7 becomes the new carry flag value.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class ROL
        {
            static public class Accumelator
            {
                public const byte OPcode = 42; //0x2A
                public const int Bytes = 1;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 38; //0x26
                public const int Bytes = 2;
                public const int Cycles = 5;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 54; //0x36
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class Absolute
            {
                public const byte OPcode = 46; //0x2E
                public const int Bytes = 3;
                public const int Cycles = 6;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 62; //0x3E
                public const int Bytes = 3;
                public const int Cycles = 7;
            }
        }

        /// <summary>
        /// Move each of the bits in either A or M one place to the right. 
        /// Bit 7 is filled with the current value of the carry flag whilst the old bit 0 becomes the new carry flag value.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class ROR
        {
            static public class Accumelator
            {
                public const byte OPcode = 106; //0x6A
                public const int Bytes = 1;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 102; //0x66
                public const int Bytes = 2;
                public const int Cycles = 5;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 118; //0x76
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class Absolute
            {
                public const byte OPcode = 110; //0x6E
                public const int Bytes = 3;
                public const int Cycles = 6;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 126; //0x7E
                public const int Bytes = 3;
                public const int Cycles = 7;
            }
        }
    }
}
