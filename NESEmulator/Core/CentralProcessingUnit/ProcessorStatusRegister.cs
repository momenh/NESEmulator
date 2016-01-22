using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.CentralProcessingUnit
{
    /// <summary>
    /// The status register contains a number of single bit flags which are set or cleared when 
    /// instructions are executed
    /// 
    /// the status register layout is 
    /// 
    /// |7||6||5||4||3||2||1||0|
    /// [N][V][ ][B][D][I][Z][C]
    /// 
    /// for more info about every bit refer to Status Flags Section below
    /// </summary>
    class ProcessorStatusRegister
    {
        #region Status Flags
            
        /// <summary>
        /// it is the 1st bit of the register - index 0.
        /// it can be represented by 1 (Decimal) = 00000001 (Binary)
        /// 
        /// The carry flag is set if the last instruction resulted in an overflow from bit 
        /// 7 or an underflow from bit 0. For example performing 255 + 1 would result in an answer 
        /// of 0 with the carry bit set. 
        /// 
        /// This allows the system to perform calculations on numbers longer than 8-bits by 
        /// performing the calculation on the first byte, storing the carry and then using that 
        /// carry when performing the calculation on the second byte. 
        /// 
        /// The carry flag can be set by the SEC (Set Carry Flag) instruction
        /// and cleared by the CLC (Clear Carry Flag) instruction. 
        /// </summary>
        public static byte CarryFlag_C = 1;

        /// <summary>
        /// it is the 2nd bit of the register - index 1.
        /// it can be represented by 2 (Decimal) = 00000010 (Binary)
        /// 
        /// The zero flag is set if the result of the last instruction was zero. So for 
        /// example 128 - 127 does not set the zero flag, whereas 128 - 128 does. 
        /// </summary>
        public static byte ZeroFlag_Z = 2;

        /// <summary>
        /// it is the 3rd bit of the register - index 2.
        /// it can be represented by 4 (Decimal) = 00000100 (Binary)
        /// 
        /// The interrupt disable flag can be used to prevent the system responding to IRQs.
        /// 
        /// It is set by the SEI (Set Interrupt Disable) instruction and IRQs will 
        /// then be ignored until execution of a CLI (Clear Interrupt Disable) instruction. 
        /// </summary>
        public static byte InterruptDisableFlag_I = 4;

        /// <summary>
        /// it is the 4th bit of the register - index 3.
        /// it can be represented by 8 (Decimal) = 00001000 (Binary)
        /// 
        /// The decimal mode flag is used to switch the 6502 into BCD mode. 
        ///
        /// However the 2A03 does not support BCD mode so although the flag can be set, its value 
        /// will be ignored. 
        /// 
        /// This flag can be set SED (Set Decimal Flag) instruction and 
        /// cleared by CLD (Clear Decimal Flag). 
        /// </summary>
        public static byte DecimalModeFlag_D = 8;

        /// <summary>
        /// it is the 5th bit of the register - index 4.
        /// it can be represented by 16 (Decimal) = 00010000 (Binary)
        /// 
        /// The break command flag is used to indicate that a BRK (Break) 
        /// instruction has been executed, causing an IRQ. 
        /// </summary>
        public static byte BreakCommandFlag_B = 16;


        /// <summary>
        /// it is the 6th bit of the register - index 5.
        /// it can be represented by 32 (Decimal) = 00100000 (Binary)
        /// 
        /// it is ignored flag and supposed to be allways 1
        /// </summary>
        public static byte IgnoredFlag_ = 32;

        /// <summary>
        /// it is the 7th bit of the register - index 6.
        /// it can be represented by 64 (Decimal) = 01000000 (Binary)
        /// 
        /// The overflow flag is set if an invalid two’s complement result was 
        /// obtained by the previous instruction. This means that a negative result has been obtained 
        /// when a positive one was expected or vice versa. 
        /// 
        /// For example, adding two positive numbers should give a positive answer. 
        /// However 64 + 64 gives the result -128 due to the sign bit. 
        /// Therefore the overflow flag would be set. 
        /// 
        /// The overflow flag is determined by taking the exclusive-or of the carry 
        /// from between bits 6 and 7 and between bit 7 and the carry flag 
        /// </summary>
        public static byte OverflowFlag_V = 64;

        /// <summary>
        /// it is the 8th bit of the register - index 7.
        /// it can be represented by 128 (Decimal) = 10000000 (Binary)
        /// 
        /// Bit 7 of a byte represents the sign of that byte, with 0 being positive 
        /// and 1 being negative. 
        /// 
        /// The negative flag (also known as the sign flag) is set if this sign bit is 1
        /// </summary>
        public static byte NegativeFlag_N = 128;

        #endregion

        #region members

        /// <summary>
        /// it is public for read purposes only
        /// 
        /// please use test methods to test if any flag is on or not
        /// also use set and clear methods for seting flags on and off
        /// </summary>
        public byte StatusRegister;

        public ProcessorStatusRegister()
        {
            //set status regiter to inital state
            StatusRegister = 32; //set to 00100000 
        }

        private void setFlagOn(byte flag)
        {
            StatusRegister = (byte)(StatusRegister | flag);
        }

        private void setFlagOff(byte flag)
        {
            StatusRegister = (byte)(StatusRegister & ~flag);
        }

        private bool testFlag(byte flag)
        {
            return (StatusRegister & flag) == flag;
        }

        #endregion

        #region set and test flags

        public bool testCarryFlag()
        {
            return testFlag(CarryFlag_C);
        }

        public void setCarryFlagOn()
        {
            setFlagOn(CarryFlag_C);
        }

        public void setCarryFlagOff()
        {
            setFlagOff(CarryFlag_C);
        }

        public bool testZeroFlag()
        {
            return testFlag(ZeroFlag_Z);
        }

        public void setZeroFlagOn()
        {
            setFlagOn(ZeroFlag_Z);
        }

        public void setZeroFlagOff()
        {
            setFlagOff(ZeroFlag_Z);
        }

        public bool testInterruptFlag()
        {
            return testFlag(InterruptDisableFlag_I);
        }

        public void setInterruptFlagOn()
        {
            setFlagOn(InterruptDisableFlag_I);
        }

        public void setInterruptFlagOff()
        {
            setFlagOff(InterruptDisableFlag_I);
        }

        public bool testDecimalFlag()
        {
            return testFlag(DecimalModeFlag_D);
        }

        public void setDecimalFlagOn()
        {
            setFlagOn(DecimalModeFlag_D);
        }

        public void setDecimalFlagOff()
        {
            setFlagOff(DecimalModeFlag_D);
        }

        public bool testBreakFlag()
        {
            return testFlag(BreakCommandFlag_B);
        }

        public void setBreakFlagOn()
        {
            setFlagOn(BreakCommandFlag_B);
        }

        public void setBreakFlagOff()
        {
            setFlagOff(BreakCommandFlag_B);
        }

        public bool testOverflowFlag()
        {
            return testFlag(OverflowFlag_V);
        }

        public void setOverflowFlagOn()
        {
            setFlagOn(OverflowFlag_V);
        }

        public void setOverflowFlagOff()
        {
            setFlagOff(OverflowFlag_V);
        }

        public bool testNegativeFlag()
        {
            return testFlag(NegativeFlag_N);
        }

        public void setNegativeFlagOn()
        {
            setFlagOn(NegativeFlag_N);
        }

        public void setNegativeFlagOff()
        {
            setFlagOff(NegativeFlag_N);
        }

        public bool testIgnoredFlag()
        {
            return testFlag(IgnoredFlag_);
        }

        public void setIgnoredFlagOn()
        {
            setFlagOn(IgnoredFlag_);
        }

        public void setIgnoredFlagOff()
        {
            setFlagOff(IgnoredFlag_);
        }
        #endregion
    }
}
