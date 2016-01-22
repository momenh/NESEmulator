using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    class Branches
    {
        /// <summary>
        /// If the carry flag is clear then add the relative displacement to the program counter to cause a branch to a new location.
        /// 
        /// Affects no flags
        /// </summary>
        static public class BCC
        {
            static public class Relative
            {
                public const byte OPcode = 144; //0x90
                public const int Bytes = 2;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// If the carry flag is set then add the relative displacement to the program counter to cause a branch to a new location.
        /// 
        /// Affects no flags
        /// </summary>
        static public class BCS
        {
            static public class Relative
            {
                public const byte OPcode = 176; //0xB0
                public const int Bytes = 2;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// If the zero flag is set then add the relative displacement to the program counter to cause a branch to a new location.
        /// 
        /// Affects no flags
        /// </summary>
        static public class BEQ
        {
            static public class Relative
            {
                public const byte OPcode = 240; //0xF0
                public const int Bytes = 2;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// If the zero flag is clear then add the relative displacement to the program counter to cause a branch to a new location.
        /// 
        /// Affects no flags
        /// </summary>
        static public class BNE
        {
            static public class Relative
            {
                public const byte OPcode = 208; //0xD0
                public const int Bytes = 2;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// If the negative flag is clear then add the relative displacement to the program counter to cause a branch to a new location.
        /// 
        /// Affects no flags
        /// </summary>
        static public class BPL 
        {
            static public class Relative
            {
                public const byte OPcode = 16; //0x10
                public const int Bytes = 2;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// If the negative flag is set then add the relative displacement to the program counter to cause a branch to a new location.
        /// 
        /// Affects no flags
        /// </summary>
        static public class BMI 
        {
            static public class Relative
            {
                public const byte OPcode = 48; //0x30
                public const int Bytes = 2;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// If the overflow flag is clear then add the relative displacement to the program counter to cause a branch to a new location.
        /// 
        /// Affects no flags
        /// </summary>
        static public class BVC
        {
            static public class Relative
            {
                public const byte OPcode = 80; //0x50
                public const int Bytes = 2;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// If the overflow flag is set then add the relative displacement to the program counter to cause a branch to a new location.
        /// 
        /// Affects no flags
        /// </summary>
        static public class BVS
        {
            static public class Relative
            {
                public const byte OPcode = 112; //0x70
                public const int Bytes = 2;
                public const int Cycles = 2;
            }
        }
    }
}
