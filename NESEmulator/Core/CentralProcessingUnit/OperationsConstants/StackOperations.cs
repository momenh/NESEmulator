using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    /// <summary>
    /// The 6502 microprocessor supports a 256 byte stack fixed between memory locations $0100 and $01FF. 
    /// A special 8-bit register, S, is used to keep track of the next free byte of stack space. 
    /// Pushing a byte on to the stack causes the value to be stored at the current free location (e.g. $0100,S) 
    /// and then the stack pointer is post decremented. Pull operations reverse this procedure.
    /// 
    /// The stack register can only be accessed by transferring its value to or from the X register. 
    /// Its value is automatically modified by push/pull instructions, subroutine calls and returns, 
    /// interrupts and returns from interrupts.
    /// </summary>
    class StackOperations
    {
        /// <summary>
        /// Copies the current contents of the stack register into the X register and sets the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class TSX
        {
            static public class Implicit
            {
                public const byte OPcode = 186; //0xBA
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Copies the current contents of the X register into the stack register.
        /// 
        /// Affects no Flags
        /// </summary>
        static public class TXS
        {
            static public class Implicit
            {
                public const byte OPcode = 154; //0x9A
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Pushes a copy of the accumulator on to the stack.
        /// 
        /// Affects no Flags
        /// </summary>
        static public class PHA
        {
            static public class Implicit
            {
                public const byte OPcode = 72; //0x48
                public const int Bytes = 1;
                public const int Cycles = 3;
            }
        }

        /// <summary>
        /// Pushes a copy of the status flags on to the stack.
        /// 
        /// NOTE : the PHP always pushes the Break (B) flag as a `1' to the stack.
        /// 
        /// Affects no Flags
        /// </summary>
        static public class PHP
        {
            static public class Implicit
            {
                public const byte OPcode = 8; //0x08
                public const int Bytes = 1;
                public const int Cycles = 3;
            }
        }

        /// <summary>
        /// Pulls an 8 bit value from the stack and into the accumulator. The zero and negative flags are set as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class PLA
        {
            static public class Implicit
            {
                public const byte OPcode = 104; //0x68
                public const int Bytes = 1;
                public const int Cycles = 4;
            }
        }
        /// <summary>
        /// Pulls an 8 bit value from the stack and into the processor flags. The flags will take on new states as determined by the value pulled.
        /// 
        /// NOTE : the PHP always pulls the Break (B) flag as a `0' from the stack. 
        /// NOTE : what ever the pulled value is the un-usd flag is alwyes set to '1'
        /// 
        /// Affects all Flags
        /// </summary>
        static public class PLP
        {
            static public class Implicit
            {
                public const byte OPcode = 40; //0x28
                public const int Bytes = 1;
                public const int Cycles = 4;
            }
        }


    }
}
