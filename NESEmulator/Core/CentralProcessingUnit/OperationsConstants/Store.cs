using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    class Store
    {
        /// <summary>
        /// Stores the contents of the accumulator into memory.
        /// 
        /// Affects no flags 
        /// </summary>
        static public class STA
        {
            static public class ZeroPage
            {
                public const byte OPcode = 133; //0x85
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 149; //0x95
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 141; //0x8D
                public const int Bytes = 3;
                public const int Cycles = 4;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 157; //0x9D
                public const int Bytes = 3;
                public const int Cycles = 5;
            }

            static public class AbsoluteY
            {
                public const byte OPcode = 153; //0x99
                public const int Bytes = 3;
                public const int Cycles = 5;
            }

            static public class IndirectX
            {
                public const byte OPcode = 129; //0x81
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class IndirectY
            {
                public const byte OPcode = 145; //0x91
                public const int Bytes = 2;
                public const int Cycles = 6;
            }
        }

        /// <summary>
        /// Stores the contents of the X register into memory.
        /// 
        /// Affects no flags 
        /// </summary>
        static public class STX
        {
            static public class ZeroPage
            {
                public const byte OPcode = 134; //0x86
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageY
            {
                public const byte OPcode = 150; //0x96
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 142; //0x8E
                public const int Bytes = 3;
                public const int Cycles = 4;
            }
        }

        /// <summary>
        /// Stores the contents of the Y register into memory.
        /// 
        /// Affects No Flag
        /// </summary>
        static public class STY
        {
            static public class ZeroPage
            {
                public const byte OPcode = 132; //0x84
                public const int Bytes = 2;
                public const int Cycles = 3;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 148; //0x94
                public const int Bytes = 2;
                public const int Cycles = 4;
            }

            static public class Absolute
            {
                public const byte OPcode = 140; //0x8C
                public const int Bytes = 3;
                public const int Cycles = 4;
            }
        }
    }
}
