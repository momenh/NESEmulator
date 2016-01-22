using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    class Increment
    {
        /// <summary>
        /// Adds one to the value held at a specified memory location setting the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class INC
        {
            static public class ZeroPage
            {
                public const byte OPcode = 230; //0xE6
                public const int Bytes = 2;
                public const int Cycles = 5;
            }

            static public class ZeroPageX
            {
                public const byte OPcode = 246; //0xF6
                public const int Bytes = 2;
                public const int Cycles = 6;
            }

            static public class Absolute
            {
                public const byte OPcode = 238; //0xEE
                public const int Bytes = 3;
                public const int Cycles = 6;
            }

            static public class AbsoluteX
            {
                public const byte OPcode = 254; //0xFE
                public const int Bytes = 3;
                public const int Cycles = 7;
            }
        }

        /// <summary>
        /// Adds one to the X register setting the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class INX
        {
            static public class Implicit
            {
                public const byte OPcode = 232; //0xE8
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Adds one to the Y register setting the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class INY
        {
            static public class Implicit
            {
                public const byte OPcode = 200; //0xC8
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }
    }
}
