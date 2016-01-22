using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit.OperationsConstants
{
    class RegisterTransfers
    {
        /// <summary>
        /// Copies the current contents of the accumulator into the X register and sets the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class TAX
        {
            static public class Implicit
            {
                public const byte OPcode = 170; //0xAA
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Copies the current contents of the accumulator into the Y register and sets the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class TAY
        {
            static public class Implicit
            {
                public const byte OPcode = 168; //0xA8
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Copies the current contents of the X register into the accumulator and sets the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class TXA
        {
            static public class Implicit
            {
                public const byte OPcode = 138; //0x8A
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }

        /// <summary>
        /// Copies the current contents of the Y register into the accumulator and sets the zero and negative flags as appropriate.
        /// 
        /// Affects : 
        /// Zero Flag
        /// Negative Flag
        /// </summary>
        static public class TYA
        {
            static public class Implicit
            {
                public const byte OPcode = 152; //0x98
                public const int Bytes = 1;
                public const int Cycles = 2;
            }
        }
    }
}
