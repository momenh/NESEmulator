using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    class JumpCalls
    {
        /// <summary>
        /// Sets the program counter to the address specified by the operand.
        /// 
        /// NOTE : An original 6502 has does not correctly fetch the target address if the indirect vector falls on a page boundary
        ///        e.g. $xxFF where xx is and value from $00 to $FF. 
        ///        In this case fetches the LSB from $xxFF as expected but takes the MSB from $xx00. 
        ///        This is fixed in some later chips like the 65SC02 so for compatibility always ensure the indirect vector is not at the end of the page.
        ///        
        ///        In any case it seemes that NES chipset has that problem so indirect jump has to overflow in case of boundary fetching
        /// 
        /// Affects no flags
        /// </summary>
        static public class JMP
        {
            static public class Absolute
            {
                public const byte OPcode = 76; //0x4C
                public const int Bytes = 3;
                public const int Cycles = 3;
            }

            static public class Indirect
            {
                public const byte OPcode = 108; //0x6C
                public const int Bytes = 3;
                public const int Cycles = 5;
            }
        }

        /// <summary>
        /// The JSR instruction pushes the address (minus one) of the return 
        /// point on to the stack and then sets the program counter to the target memory address.
        /// 
        /// Affects no flags
        /// </summary>
        static public class JSR
        {
            static public class Absolute
            {
                public const byte OPcode = 32; //0x20
                public const int Bytes = 3;
                public const int Cycles = 6;
            }
        }

        /// <summary>
        /// The RTS instruction is used at the end of a subroutine to return to the calling routine. 
        /// It pulls the program counter (minus one) from the stack.
        /// 
        /// Affects no flags
        /// </summary>
        static public class RTS
        {
            static public class Implicit
            {
                public const byte OPcode = 96; //0x60
                public const int Bytes = 1;
                public const int Cycles = 6;
            }
        }
    }
}
