using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    class StatusFlags
    {
        /// <summary>
        /// Set the carry flag to zero.
        /// 
        /// Affects :
        /// Carry flag
        /// </summary>
        static public class CLC
        {
            static public class Implicit
            {
                public const byte OPcode = 24; //0x18
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Set the carry flag to one.
        /// 
        /// Affects :
        /// Carry flag
        /// </summary>
        static public class SEC
        {
            static public class Implicit
            {
                public const byte OPcode = 56; //0x38
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Set the decimal mode flag to zero.
        /// 
        /// Affects :
        /// decimal flag
        /// </summary>
        static public class CLD
        {
            static public class Implicit
            {
                public const byte OPcode = 216; //0xD8
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Set the decimal mode flag to one.
        /// 
        /// Affects :
        /// decimal flag
        /// </summary>
        static public class SED
        {
            static public class Implicit
            {
                public const byte OPcode = 248; //0xF8
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Set the interrupt disable flag to zero.
        /// 
        /// Affects :
        /// interrupt flag
        /// </summary>
        static public class CLI
        {
            static public class Implicit
            {
                public const byte OPcode = 88; //0x58
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Set the interrupt disable flag to one.
        /// 
        /// Affects :
        /// interrupt flag
        /// </summary>
        static public class SEI
        {
            static public class Implicit
            {
                public const byte OPcode = 120; //0x78
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Set the overflow flag to zero.
        /// 
        /// Affects :
        /// overflow flag
        /// </summary>
        static public class CLV
        {
            static public class Implicit
            {
                public const byte OPcode = 184; //0xB8
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }
    }
}
