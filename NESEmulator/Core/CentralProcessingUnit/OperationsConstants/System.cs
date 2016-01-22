using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    class SystemFunctions
    {
        /// <summary>
        /// The BRK instruction forces the generation of an interrupt request. 
        /// The program counter and processor status are pushed on the stack then 
        /// the IRQ interrupt vector at $FFFE/F is loaded into the PC and 
        /// the break flag in the status set to one.
        /// 
        /// Affects :
        /// break flag
        /// </summary>
        static public class BRK
        {
            static public class Implicit
            {
                public const byte OPcode = 0; //0x00
                public const int Bytes = 1;
                public const int Cycles = 7;
            }
        }

        /// <summary>
        /// The NOP instruction causes no changes to the processor other than 
        /// the normal incrementing of the program counter to the next instruction
        /// 
        /// Affects no flags
        /// </summary>
        static public class NOP
        {
            static public class Implicit
            {
                public const byte OPcode = 234; //0xEA
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// The RTI instruction is used at the end of an interrupt processing routine.
        /// It pulls the processor flags from the stack followed by the program counter.
        /// 
        /// Affects all flags
        /// </summary>
        static public class RTI
        {
            static public class Implicit
            {
                public const byte OPcode = 64; //0x40
                public const int Bytes = 1;
                public const int Cycles = 6;
            }
        }
    }
}
