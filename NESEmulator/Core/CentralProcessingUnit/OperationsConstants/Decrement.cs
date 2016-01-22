using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    class Decrement
    {
        /// <summary>
        /// Subtracts one from the value held at a specified memory location setting the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class DEC
        {
            static public class ZeroPage
            {
                public const byte OPcode = 198; //0xC6
                public const int Bytes = 2;
                public const int Cycles = 5;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 214; //0xD6
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class Absolute
            {
                public const byte OPcode = 206; //0xCE
                public const int Bytes = 3;
                public const int Cycles = 6;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 222; //0xDE
                public const int Bytes = 3;
                public const int Cycles = 7;
            }
        }

        /// <summary>
        /// Subtracts one from the X register setting the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class DEX
        {
            static public class Implicit
            {
                public const byte OPcode = 202; //0xCA
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Subtracts one from the Y register setting the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class DEY
        {
            static public class Implicit
            {
                public const byte OPcode = 136; //0x88
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }
    }
}
