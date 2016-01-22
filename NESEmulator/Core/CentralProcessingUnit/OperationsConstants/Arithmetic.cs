using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    class Arithmetic
    {
        /// <summary>
        /// This instruction adds the contents of a memory location to the accumulator together with the carry bit. 
        /// If overflow occurs the carry bit is set, this enables multiple byte addition to be performed.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// Overflow Flag
        /// CarryFlag
        /// </summary>
        static public class ADC
        {
            static public class Immediat
            {
                public const byte OPcode = 105; //0x69
                public const int Bytes = 2;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 101; //0x65
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 117; //0x75
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 109; //0x6D
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 125; //0x7D
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteY
            {
                public const byte OPcode = 121; //0x79
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class IndirectX
            {
                public const byte OPcode = 97; //0x61
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class IndirectY
            {
                public const byte OPcode = 113; //0x71
                public const int Bytes = 2;
                public const int Cycles = 5;
            }
        }

        /// <summary>
        /// This instruction subtracts the contents of a memory location to the accumulator together with the not of the carry bit. 
        /// If overflow occurs the carry bit is clear, this enables multiple byte subtraction to be performed.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// Overflow Flag
        /// CarryFlag
        /// </summary>
        static public class SBC
        {
            static public class Immediat
            {
                public const byte OPcode = 233; //0xE9
                public const int Bytes = 2;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 229; //0xE5
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 245; //0xF5
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 237; //0xED
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 253; //0xFD
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteY
            {
                public const byte OPcode = 249; //0xF9
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class IndirectX
            {
                public const byte OPcode = 225; //0xE1
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class IndirectY
            {
                public const byte OPcode = 241; //0xF1
                public const int Bytes = 2;
                public const int Cycles = 5;
            }
        }

        /// <summary>
        /// This instruction compares the contents of the accumulator 
        /// with another memory held value and sets the zero and carry flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// CarryFlag
        /// </summary>
        static public class CMP
        {
            static public class Immediat
            {
                public const byte OPcode = 201; //0xC9
                public const int Bytes = 2;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 197; //0xC5
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 213; //0xD5
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 205; //0xCD
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 221; //0xDD
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteY
            {
                public const byte OPcode = 217; //0xD9
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class IndirectX
            {
                public const byte OPcode = 193; //0xC1
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class IndirectY
            {
                public const byte OPcode = 209; //0xD1
                public const int Bytes = 2;
                public const int Cycles = 5;
            }
        }

        /// <summary>
        /// This instruction compares the contents of the X register 
        /// with another memory held value and sets the zero and carry flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// CarryFlag
        /// </summary>
        static public class CPX
        {
            static public class Immediat
            {
                public const byte OPcode = 224; //0xE0
                public const int Bytes = 2;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 228; //0xE4
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class Absolute
            {
                public const byte OPcode = 236; //0xEC
                public const int Bytes = 3;
                public const int Cycles = 4;
            }
        }

        /// <summary>
        /// This instruction compares the contents of the Y register 
        /// with another memory held value and sets the zero and carry flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// CarryFlag
        /// </summary>
        static public class CPY
        {
            static public class Immediat
            {
                public const byte OPcode = 192; //0xC0
                public const int Bytes = 2;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 196; //0xC4
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class Absolute
            {
                public const byte OPcode = 204; //0xCC
                public const int Bytes = 3;
                public const int Cycles = 4;
            }
        }
    }
}
