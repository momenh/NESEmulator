using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    class Load
    {
        /// <summary>
        /// Loads a byte of memory into the accumulator setting the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class LDA
        {
            static public class Immediat
            {
                public const byte OPcode = 169; //0xA9
                public const int Bytes = 2;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 165; //0xA5
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 181; //0xB5
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 173; //0xAD
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 189; //0xBD
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteY
            {
                public const byte OPcode = 185; //0xB9
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class IndirectX
            {
                public const byte OPcode = 161; //0xA1
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class IndirectY
            {
                public const byte OPcode = 177; //0xB1
                public const int Bytes = 2;
                public const int Cycles = 5;
            }
        }

        /// <summary>
        /// Loads a byte of memory into the x register setting the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class LDX
        {
            static public class Immediat
            {
                public const byte OPcode = 162; //0xA2
                public const int Bytes = 2;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 166; //0xA6
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageY
            {
                public const byte OPcode = 182; //0xB6
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 174; //0xAE
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteY
            {
                public const byte OPcode = 190; //0xBE
                public const int Bytes = 3;
                public const int Cycles = 4;
            }
        }

        /// <summary>
        /// Loads a byte of memory into the y register setting the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class LDY
        {
            static public class Immediat
            {
                public const byte OPcode = 160; //0xA0
                public const int Bytes = 2;
                public const int Cycles = 2;
            }

            static public class ZeroPage
            {
                public const byte OPcode = 164; //0xA4
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 180; //0xB4
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 172; //0xAC
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 188; //0xBC
                public const int Bytes = 3;
                public const int Cycles = 4;
            }
        }
    }
}
