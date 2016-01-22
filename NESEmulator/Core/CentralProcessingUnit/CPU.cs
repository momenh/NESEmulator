using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NESEmulator.Core.CentralProcessingUnit.OperationsConstants;

namespace NESEmulator.Core.CentralProcessingUnit
{
    /// <summary>
    /// The CPU 2A03.
    /// 
    /// Ricoh produced an NMOS processor based on the 6502, the 2A03.
    /// 
    /// The chip differed from a standard 6502 in that it had the ability to handle sound, 
    /// serving as pAPU (pseudo-Audio Processing Unit) as well as CPU.
    /// 
    /// NOTE :
    /// - it lacked a Binary Coded Decimal (BCD) mode which allowed representing each digit using 4 bits
    /// - the 2A03 uses the same instruction set as the standard 6502
    /// - The 6502 is a little endian processor which means that addresses are stored in memory least 
    ///   significant byte first, for example the address $1234 would be stored in memory as $34 at 
    ///   memory location x and $12 at memory location (x + 1). 
    /// </summary>
    class CPU
    {
        #region registers

        /// <summary>
        /// The program counter is a 16-bit register which holds the address of the next instruction to be 
        /// executed. As instructions are executed, the value of the program counter is updated, usually 
        /// moving on to the next instruction in the sequence. The value can be affected by branch and 
        /// jump instructions, procedure calls and interrupts. 
        /// </summary>
        public ushort m_ProgramCounterRegiter;

        /// <summary>
        /// The stack is located at memory locations $0100-$01FF. The stack pointer is an 8-bit register 
        /// which serves as an offset from $0100. The stack works top-down, so when a byte is pushed 
        /// on to the stack, the stack pointer is decremented and when a byte is pulled from the stack, 
        /// the stack pointer is incremented. There is no detection of stack overflow and the stack 
        /// pointer will just wrap around from $00 to $FF. 
        /// </summary>
        public byte m_StackPointerRegiter;

        /// <summary>
        /// The accumulator is an 8-bit register which stores the results of arithmetic and logic 
        /// operations. The accumulator can also be set to a value retrieved from memory. 
        /// </summary>
        public byte m_AccumulatorRegister;

        /// <summary>
        /// The X register is an 8-bit register typically used as a counter or an offset for certain 
        /// addressing modes. The X register can be set to a value retrieved from memory and can be 
        /// used to get or set the value of the stack pointer. 
        /// </summary>
        public byte m_IndexXRegister;

        /// <summary>
        /// The Y register is an 8-bit register which is used in the same way as the X register, as a counter
        ///  or to store an offset. Unlike the X register, the Y register cannot affect the stack pointer. 
        /// </summary>
        public byte m_IndexYRegister;

        public ProcessorStatusRegister m_ProcessorStatusRegister;

        #endregion

        #region members

        /// <summary>
        /// handle all read and write to memory
        /// </summary>
        Memory.MemoryAccessController m_MemoryAccessController;

        /// <summary>
        /// counter for how many cpu cycles is taken for excuting instructions
        /// </summary>
        public int m_CPUCyclesCounter;

        public CPU(Memory.MemoryAccessController memoryAccessController)
        {
            m_MemoryAccessController = memoryAccessController;
            m_ProcessorStatusRegister = new ProcessorStatusRegister();
        }

        #region memory addressing mode reading

        /// <summary>
        /// Immediate addressing allows the programmer to directly specify an 8 bit constant within the instruction.
        /// 
        /// Normaly this value will be fetched as the next operand of the current instruction
        /// </summary>
        /// <returns>readed value</returns>
        byte loadMemoryImmediat()
        {
            return m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);
        }

        /// <summary>
        /// Relative addressing mode is used by branch instructions which contain 
        /// a signed 8 bit relative offset (e.g. -128 to +127) which is added to program counter 
        /// if the condition is true. 
        /// 
        /// As the program counter itself is incremented during instruction execution by two the effective 
        /// address range for the target instruction must be with -126 to +129 bytes of the branch.
        /// </summary>
        /// <returns>readed value</returns>
        sbyte loadMemoryRelative()
        {
            return (sbyte)m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);
        }

        /// <summary>
        /// An instruction using zero page addressing mode has only an 8 bit address operand. 
        /// This limits it to addressing only the first 256 bytes of memory (e.g. $0000 to $00FF)
        /// where the most significant byte of the address is always zero.
        /// </summary>
        /// <returns>readed value</returns>
        byte loadMemoryZeroPage()
        { 
            //get zero page memory location
            byte zeroPageAddress = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //read the desired location 
            return m_MemoryAccessController.readOperation(zeroPageAddress);
        }

        /// <summary>
        /// The address to be accessed by an instruction using indexed zero page addressing is calculated by taking 
        /// the 8 bit zero page address from the instruction and adding the current value of the X register to it
        /// 
        /// For example if the X register contains $0F and the instruction LDA $80,X 
        /// is executed then the accumulator will be loaded from $008F (e.g. $80 + $0F => $8F).
        /// 
        /// NOTE : 
        /// The address calculation wraps around if the sum of the base address and the register exceed $FF. 
        /// If we repeat the last example but with $FF in the X register then the value 
        /// will be loaded from $007F (e.g. $80 + $FF => $7F) and not $017F.
        /// </summary>
        /// <returns>readed value</returns>
        byte loadMemoryZeroPageX()
        {
            //get zero page memory location
            byte zeroPageAddress = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //calculate the memeory location to read from
            //we will trubcate the result back to byte for not to cross the page
            byte memoryLocation = (byte)(zeroPageAddress + m_IndexXRegister);

            //read the desired location 
            return m_MemoryAccessController.readOperation(memoryLocation);
        }

        /// <summary>
        /// The address to be accessed by an instruction using indexed zero page addressing is calculated by taking 
        /// the 8 bit zero page address from the instruction and adding the current value of the Y register to it
        /// </summary>
        /// <returns>readed value</returns>
        byte loadMemoryZeroPageY()
        {
            //get zero page memory location
            byte zeroPageAddress = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //calculate the memeory location to read from
            //we will trubcate the result back to byte for not to cross the page
            byte memoryLocation = (byte)(zeroPageAddress + m_IndexYRegister);

            //read the desired location 
            return m_MemoryAccessController.readOperation(memoryLocation);
        }

        /// <summary>
        /// Instructions using absolute addressing contain a full 16 bit address to identify the target location.
        /// </summary>
        /// <returns></returns>
        byte loadMemoryAbsolute()
        {
            //get LSB
            byte LSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //get MSB
            byte MSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 2);

            //constrcut the memeory location to read from
            ushort memoryLocation = Memory.MemoryUtility.readUShort(LSB, MSB);

            //read the desired location 
            return m_MemoryAccessController.readOperation(memoryLocation);
        }

        /// <summary>
        /// The address to be accessed by an instruction using X register indexed absolute addressing 
        /// is computed by taking the 16 bit address from the instruction and added the contents of the X register. 
        /// 
        /// For example if X contains $92 then an STA $2000,X instruction will store the accumulator at $2092 (e.g. $2000 + $92).
        /// 
        /// NOTE : if the adding operation cross the page the instruction take one more cycle from cpu 
        /// </summary>
        /// <returns></returns>
        byte loadMemoryAbsoluteX()
        {
            //get LSB
            byte LSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //get MSB
            byte MSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 2);

            //calculate and construct the memeory location to read from
            int memoryLocation = LSB + m_IndexXRegister;

            //test if page crossed
            if (memoryLocation > 255)
            {
                //register the added cpu cycle
                m_CPUCyclesCounter++;

                //update MSB
                MSB++;
            }

            memoryLocation = Memory.MemoryUtility.readUShort((byte)memoryLocation, MSB);

            //read the desired location 
            return m_MemoryAccessController.readOperation(memoryLocation);
        }

        /// <summary>
        /// The Y register indexed absolute addressing mode is 
        /// the same as the previous mode only with the contents of the 
        /// Y register added to the 16 bit address from the instruction.
        /// </summary>
        /// <returns></returns>
        byte loadMemoryAbsoluteY()
        {
            //get LSB
            byte LSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //get MSB
            byte MSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 2);

            //calculate and construct the memeory location to read from
            int memoryLocation = LSB + m_IndexYRegister;

            //test if page crossed
            if (memoryLocation > 255)
            {
                //register the added cpu cycle
                m_CPUCyclesCounter++;

                //update MSB
                MSB++;
            }

            memoryLocation = Memory.MemoryUtility.readUShort((byte)memoryLocation, MSB);

            //read the desired location 
            return m_MemoryAccessController.readOperation(memoryLocation);
        }

        /// <summary>
        /// Indexed Indirect - (also known as pre-indexed)
        /// 
        /// addressing takes a single byte as an operand and adds the value of 
        /// the X register to it (with wraparound) to give the address of the least 
        /// significant byte of the target address
        /// 
        ///  For example : if the operand + x register is bb,
        ///  00bb is xx and 00bb+1 is yy 
        ///  then the data can be found at yyxx
        /// </summary>
        /// <returns></returns>
        byte loadMemoryIndirectX()
        {
            //get LSB address Location
            byte LSBAddress = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);
            LSBAddress = (byte)(LSBAddress + m_IndexXRegister); //add to x register with wrapping

            //get LSB
            byte LSB = m_MemoryAccessController.readOperation(LSBAddress);

            //get MSB
            byte MSB = m_MemoryAccessController.readOperation((byte)(LSBAddress + 1));

            //calculate and construct the memeory location to read from
            int memoryLocation = Memory.MemoryUtility.readUShort(LSB, MSB);

            //read the desired location 
            return m_MemoryAccessController.readOperation(memoryLocation);
        }

        /// <summary>
        /// Indirect indexed (also known as post-indexed) addressing takes a single operand which 
        /// gives the zero page address of the least significant byte of a 16-bit address which is then 
        /// added to the Y register to give the target address.
        /// 
        /// For example : 
        /// if the operand is bb, 00bb is xx and 00bb+1 is yy
        /// then the data can be found at yyxx + Y register
        /// 
        /// NOTE : if the adding operation cross the page the instruction take one more cycle from cpu 
        /// </summary>
        byte loadMemoryIndirectY()
        {
            //get LSB 1st address Location
            byte LSBAddress = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //get 1st addres LSB
            byte LSB = m_MemoryAccessController.readOperation(LSBAddress);

            //get 1st addres  MSB
            byte MSB = m_MemoryAccessController.readOperation((byte)(LSBAddress + 1));

            //calculate and construct the memory location to read from
            int memoryLocation = LSB + m_IndexYRegister;

            //test if page crossed
            if (memoryLocation > 255)
            {
                //register the added cpu cycle
                m_CPUCyclesCounter++;

                //update MSB
                MSB++;
            }

            memoryLocation = Memory.MemoryUtility.readUShort((byte)memoryLocation, MSB);


            //read the desired location 
            return m_MemoryAccessController.readOperation(memoryLocation);
        }

        #endregion

        #region memory addressing mode writing

        /// <summary>
        /// An instruction using zero page addressing mode has only an 8 bit address operand. 
        /// This limits it to addressing only the first 256 bytes of memory (e.g. $0000 to $00FF)
        /// where the most significant byte of the address is always zero.
        /// </summary>
        void writeMemoryZeroPage(byte value)
        {
            //get zero page memory location
            byte zeroPageAddress = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //write to the desired location 
            m_MemoryAccessController.writeOperation(zeroPageAddress,value);
        }

        /// <summary>
        /// The address to be accessed by an instruction using indexed zero page addressing is calculated by taking 
        /// the 8 bit zero page address from the instruction and adding the current value of the X register to it
        /// 
        /// For example if the X register contains $0F and the instruction LDA $80,X 
        /// is executed then the accumulator will be loaded from $008F (e.g. $80 + $0F => $8F).
        /// 
        /// NOTE : 
        /// The address calculation wraps around if the sum of the base address and the register exceed $FF. 
        /// If we repeat the last example but with $FF in the X register then the value 
        /// will be loaded from $007F (e.g. $80 + $FF => $7F) and not $017F.
        /// </summary>
        void writeMemoryZeroPageX(byte value)
        {
            //get zero page memory location
            byte zeroPageAddress = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //calculate the memeory location to write to
            //we will truncate the result back to byte for not to cross the page
            byte memoryLocation = (byte)(zeroPageAddress + m_IndexXRegister);

            //write to the desired location 
            m_MemoryAccessController.writeOperation(memoryLocation,value);
        }

        /// <summary>
        /// The address to be accessed by an instruction using indexed zero page addressing is calculated by taking 
        /// the 8 bit zero page address from the instruction and adding the current value of the Y register to it
        /// </summary>
        void writeMemoryZeroPageY(byte value)
        {
            //get zero page memory location
            byte zeroPageAddress = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //calculate the memeory location to write to
            //we will truncate the result back to byte for not to cross the page
            byte memoryLocation = (byte)(zeroPageAddress + m_IndexYRegister);

            //read the desired location 
            m_MemoryAccessController.writeOperation(memoryLocation,value);
        }

        /// <summary>
        /// Instructions using absolute addressing contain a full 16 bit address to identify the target location.
        /// </summary>
        void writeMemoryAbsolute(byte value)
        {
            //get LSB
            byte LSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //get MSB
            byte MSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 2);

            //constrcut the memeory location to write to
            ushort memoryLocation = Memory.MemoryUtility.readUShort(LSB, MSB);

            //write to the desired location 
            m_MemoryAccessController.writeOperation(memoryLocation,value);
        }

        /// <summary>
        /// The address to be accessed by an instruction using X register indexed absolute addressing 
        /// is computed by taking the 16 bit address from the instruction and added the contents of the X register. 
        /// 
        /// For example if X contains $92 then an STA $2000,X instruction will store the accumulator at $2092 (e.g. $2000 + $92).
        /// </summary>
        void writeMemoryAbsoluteX(byte value)
        {
            //get LSB
            byte LSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //get MSB
            byte MSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 2);

            //calculate and construct the memeory location to write to
            int memoryLocation = Memory.MemoryUtility.readUShort(LSB,MSB);

            memoryLocation += m_IndexXRegister;

            //write to the desired location 
            m_MemoryAccessController.writeOperation(memoryLocation,value);
        }

        /// <summary>
        /// The Y register indexed absolute addressing mode is 
        /// the same as the previous mode only with the contents of the 
        /// Y register added to the 16 bit address from the instruction.
        /// </summary>
        /// <returns></returns>
        void writeMemoryAbsoluteY(byte value)
        {
            //get LSB
            byte LSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //get MSB
            byte MSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 2);

            //calculate and construct the memeory location to write to
            int memoryLocation = Memory.MemoryUtility.readUShort(LSB, MSB);

            memoryLocation += m_IndexYRegister;

            //write to the desired location 
            m_MemoryAccessController.writeOperation(memoryLocation,value);
        }

        /// <summary>
        /// Indexed Indirect - (also known as pre-indexed)
        /// 
        /// addressing takes a single byte as an operand and adds the value of 
        /// the X register to it (with wraparound) to give the address of the least 
        /// significant byte of the target address
        /// 
        ///  For example : if the operand + x register is bb,
        ///  00bb is xx and 00bb+1 is yy 
        ///  then the data can be found at yyxx
        /// </summary>
        /// <returns></returns>
        void writeMemoryIndirectX(byte value)
        {
            //get LSB address Location
            byte LSBAddress = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);
            LSBAddress = (byte)(LSBAddress + m_IndexXRegister); //add to x register with wrapping

            //get LSB
            byte LSB = m_MemoryAccessController.readOperation(LSBAddress);

            //get MSB
            byte MSB = m_MemoryAccessController.readOperation((byte)(LSBAddress + 1));

            //calculate and construct the memeory location to write to
            int memoryLocation = Memory.MemoryUtility.readUShort(LSB, MSB);

            //write to the desired location 
            m_MemoryAccessController.writeOperation(memoryLocation,value);
        }

        /// <summary>
        /// Indirect indexed (also known as post-indexed) addressing takes a single operand which 
        /// gives the zero page address of the least significant byte of a 16-bit address which is then 
        /// added to the Y register to give the target address.
        /// 
        /// For example : 
        /// if the operand is bb, 00bb is xx and 00bb+1 is yy
        /// then the data can be found at yyxx + Y register
        /// </summary>
        void writeMemoryIndirectY(byte value)
        {
            //get LSB 1st address Location
            byte LSBAddress = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);

            //get 1st addres LSB
            byte LSB = m_MemoryAccessController.readOperation(LSBAddress);

            //get 1st addres  MSB
            byte MSB = m_MemoryAccessController.readOperation((byte)(LSBAddress + 1));

            //calculate and construct the memory location to write to
            int memoryLocation = Memory.MemoryUtility.readUShort(LSB, MSB);

            memoryLocation += m_IndexYRegister;


            //write to the desired location 
            m_MemoryAccessController.writeOperation(memoryLocation,value);
        }

        #endregion

        #region processor status register adjusting

        void updateZeroFlag(byte valueOflastOperation)
        {
            if (valueOflastOperation == 0)
                m_ProcessorStatusRegister.setZeroFlagOn();
            else
                m_ProcessorStatusRegister.setZeroFlagOff();
        }

        void updateNegativeFlag(byte valueOflastOperation)
        {
            if (valueOflastOperation >= 128)
                m_ProcessorStatusRegister.setNegativeFlagOn();
            else
                m_ProcessorStatusRegister.setNegativeFlagOff();
        }

        void updateOverFlowFlag(byte valueOfLastOperation)
        {
            //64 (01000000)
            if ((valueOfLastOperation & 64) == 64) //if 6th bit is on
                m_ProcessorStatusRegister.setOverflowFlagOn();
            else
                m_ProcessorStatusRegister.setOverflowFlagOff();
        }

        #endregion

        #region main cpu cycle

        public void doCycle()
        {
            //1 - fetch operation from memory
            byte OPCode = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter);

            //2 - decode and perform operation
            switch (OPCode)
            {
                #region Load Operations
                #region LDA
                case Load.LDA.Immediat.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = loadMemoryImmediat();

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDA.Immediat.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDA.Immediat.Bytes;
                    break;
                case Load.LDA.ZeroPage.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = loadMemoryZeroPage();

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDA.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDA.ZeroPage.Bytes;
                    break;
                case Load.LDA.ZeroPageX.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = loadMemoryZeroPageX();

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDA.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDA.ZeroPageX.Bytes;
                    break;
                case Load.LDA.Absolute.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = loadMemoryAbsolute();

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDA.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDA.Absolute.Bytes;
                    break;
                case Load.LDA.AbsoluteX.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = loadMemoryAbsoluteX();

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDA.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDA.AbsoluteX.Bytes;
                    break;
                case Load.LDA.AbsoluteY.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = loadMemoryAbsoluteY();

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDA.AbsoluteY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDA.AbsoluteY.Bytes;
                    break;
                case Load.LDA.IndirectX.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = loadMemoryIndirectX();

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDA.IndirectX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDA.IndirectX.Bytes;

                    break;
                case Load.LDA.IndirectY.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = loadMemoryIndirectY();

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDA.IndirectY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDA.IndirectY.Bytes;
                    break;
                #endregion
                #region LDX
                case Load.LDX.Immediat.OPcode:
                    //3 - perform the operation
                    m_IndexXRegister = loadMemoryImmediat();

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexXRegister);
                    updateNegativeFlag(m_IndexXRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDX.Immediat.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDX.Immediat.Bytes;
                    break;
                case Load.LDX.ZeroPage.OPcode:
                    //3 - perform the operation
                    m_IndexXRegister = loadMemoryZeroPage();

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexXRegister);
                    updateNegativeFlag(m_IndexXRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDX.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDX.ZeroPage.Bytes;
                    break;
                case Load.LDX.ZeroPageY.OPcode:
                    //3 - perform the operation
                    m_IndexXRegister = loadMemoryZeroPageY();

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexXRegister);
                    updateNegativeFlag(m_IndexXRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDX.ZeroPageY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDX.ZeroPageY.Bytes;
                    break;
                case Load.LDX.Absolute.OPcode:
                    //3 - perform the operation
                    m_IndexXRegister = loadMemoryAbsolute();

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexXRegister);
                    updateNegativeFlag(m_IndexXRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDX.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDX.Absolute.Bytes;
                    break;
                case Load.LDX.AbsoluteY.OPcode:
                    //3 - perform the operation
                    m_IndexXRegister = loadMemoryAbsoluteY();

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexXRegister);
                    updateNegativeFlag(m_IndexXRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDX.AbsoluteY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDX.AbsoluteY.Bytes;
                    break;
                #endregion
                #region LDY
                case Load.LDY.Immediat.OPcode:
                    //3 - perform the operation
                    m_IndexYRegister = loadMemoryImmediat();

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexYRegister);
                    updateNegativeFlag(m_IndexYRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDY.Immediat.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDY.Immediat.Bytes;
                    break;
                case Load.LDY.ZeroPage.OPcode:
                    //3 - perform the operation
                    m_IndexYRegister = loadMemoryZeroPage();

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexYRegister);
                    updateNegativeFlag(m_IndexYRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDY.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDY.ZeroPage.Bytes;
                    break;
                case Load.LDY.ZeroPageX.OPcode:
                    //3 - perform the operation
                    m_IndexYRegister = loadMemoryZeroPageX();

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexYRegister);
                    updateNegativeFlag(m_IndexYRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDY.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDY.ZeroPageX.Bytes;
                    break;
                case Load.LDY.Absolute.OPcode:
                    //3 - perform the operation
                    m_IndexYRegister = loadMemoryAbsolute();

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexYRegister);
                    updateNegativeFlag(m_IndexYRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDY.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDY.Absolute.Bytes;
                    break;
                case Load.LDY.AbsoluteX.OPcode:
                    //3 - perform the operation
                    m_IndexYRegister = loadMemoryAbsoluteX();

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexYRegister);
                    updateNegativeFlag(m_IndexYRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Load.LDY.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Load.LDY.AbsoluteX.Bytes;
                    break;
                #endregion
                #endregion
                #region Store Operations
                #region STA
                case Store.STA.ZeroPage.OPcode:
                    //3 - perform the operation
                    writeMemoryZeroPage(m_AccumulatorRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STA.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STA.ZeroPage.Bytes;
                    break;
                case Store.STA.ZeroPageX.OPcode:
                    //3 - perform the operation
                    writeMemoryZeroPageX(m_AccumulatorRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STA.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STA.ZeroPageX.Bytes;
                    break;
                case Store.STA.Absolute.OPcode:
                    //3 - perform the operation
                    writeMemoryAbsolute(m_AccumulatorRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STA.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STA.Absolute.Bytes;
                    break;
                case Store.STA.AbsoluteX.OPcode:
                    //3 - perform the operation
                    writeMemoryAbsoluteX(m_AccumulatorRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STA.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STA.AbsoluteX.Bytes;
                    break;
                case Store.STA.AbsoluteY.OPcode:
                    //3 - perform the operation
                    writeMemoryAbsoluteY(m_AccumulatorRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STA.AbsoluteY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STA.AbsoluteY.Bytes;
                    break;
                case Store.STA.IndirectX.OPcode:
                    //3 - perform the operation
                    writeMemoryIndirectX(m_AccumulatorRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STA.IndirectX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STA.IndirectX.Bytes;
                    break;
                case Store.STA.IndirectY.OPcode:
                    //3 - perform the operation
                    writeMemoryIndirectY(m_AccumulatorRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STA.IndirectY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STA.IndirectY.Bytes;
                    break;
                #endregion
                #region STX
                case Store.STX.ZeroPage.OPcode:
                    //3 - perform the operation
                    writeMemoryZeroPage(m_IndexXRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STX.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STX.ZeroPage.Bytes;
                    break;
                case Store.STX.ZeroPageY.OPcode:
                    //3 - perform the operation
                    writeMemoryZeroPageY(m_IndexXRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STX.ZeroPageY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STX.ZeroPageY.Bytes;
                    break;
                case Store.STX.Absolute.OPcode:
                    //3 - perform the operation
                    writeMemoryAbsolute(m_IndexXRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STX.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STX.Absolute.Bytes;
                    break;
                #endregion
                #region STY
                case Store.STY.ZeroPage.OPcode:
                    //3 - perform the operation
                    writeMemoryZeroPage(m_IndexYRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STY.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STY.ZeroPage.Bytes;
                    break;
                case Store.STY.ZeroPageX.OPcode:
                    //3 - perform the operation
                    writeMemoryZeroPageX(m_IndexYRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STY.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STY.ZeroPageX.Bytes;
                    break;
                case Store.STY.Absolute.OPcode:
                    //3 - perform the operation
                    writeMemoryAbsolute(m_IndexYRegister);

                    //4 -update processor status flags
                    // no flags will be affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Store.STY.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Store.STY.Absolute.Bytes;
                    break;
                #endregion
                #endregion
                #region Register Trasfers Operations
                case RegisterTransfers.TAX.Implicit.OPcode:
                    //3 - perform the operation
                    m_IndexXRegister = m_AccumulatorRegister;

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexXRegister);
                    updateNegativeFlag(m_IndexXRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += RegisterTransfers.TAX.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += RegisterTransfers.TAX.Implicit.Bytes;
                    break;
                case RegisterTransfers.TAY.Implicit.OPcode:
                    //3 - perform the operation
                    m_IndexYRegister = m_AccumulatorRegister;

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexYRegister);
                    updateNegativeFlag(m_IndexYRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += RegisterTransfers.TAY.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += RegisterTransfers.TAY.Implicit.Bytes;
                    break;
                case RegisterTransfers.TXA.Implicit.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = m_IndexXRegister;

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += RegisterTransfers.TXA.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += RegisterTransfers.TXA.Implicit.Bytes;
                    break;
                case RegisterTransfers.TYA.Implicit.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = m_IndexYRegister;

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += RegisterTransfers.TYA.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += RegisterTransfers.TYA.Implicit.Bytes;
                    break;
                #endregion
                #region Stack Operations
                case StackOperations.TSX.Implicit.OPcode:
                    //3 - perform the operation
                    m_IndexXRegister = m_StackPointerRegiter;

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexXRegister);
                    updateNegativeFlag(m_IndexXRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StackOperations.TSX.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StackOperations.TSX.Implicit.Bytes;
                    break;
                case StackOperations.TXS.Implicit.OPcode:
                    //3 - perform the operation
                    m_StackPointerRegiter = m_IndexXRegister;

                    //4 -update processor status flags
                    // no flags is affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StackOperations.TXS.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StackOperations.TXS.Implicit.Bytes;
                    break;
                case StackOperations.PHA.Implicit.OPcode:
                    //3 - perform the operation
                    int memoryAddress = m_StackPointerRegiter + 256;
                    m_MemoryAccessController.writeOperation(memoryAddress, m_AccumulatorRegister);

                    m_StackPointerRegiter--;

                    //4 -update processor status flags
                    // no flags is affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StackOperations.PHA.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StackOperations.PHA.Implicit.Bytes;
                    break;
                case StackOperations.PHP.Implicit.OPcode:
                    //3 - perform the operation
                    memoryAddress = m_StackPointerRegiter + 256;
                    
                    //update break falg
                    byte tempStatusRegisterBefoureForcingBreakOn = m_ProcessorStatusRegister.StatusRegister;
                    m_ProcessorStatusRegister.setBreakFlagOn();
                    m_MemoryAccessController.writeOperation(memoryAddress, m_ProcessorStatusRegister.StatusRegister);
                    //reset status flage to original status befoure setting the break flag
                    m_ProcessorStatusRegister.StatusRegister = tempStatusRegisterBefoureForcingBreakOn;

                    //update statck pointer
                    m_StackPointerRegiter--;

                    //4 -update processor status flags
                    // no flags is affected

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StackOperations.PHP.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StackOperations.PHP.Implicit.Bytes;
                    break;
                case StackOperations.PLA.Implicit.OPcode:
                    //3 - perform the operation
                    m_StackPointerRegiter++;

                    memoryAddress = m_StackPointerRegiter + 256;
                    m_AccumulatorRegister = m_MemoryAccessController.readOperation(memoryAddress);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StackOperations.PLA.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StackOperations.PLA.Implicit.Bytes;
                    break;
                case StackOperations.PLP.Implicit.OPcode:
                    //3 - perform the operation
                    m_StackPointerRegiter++;

                    memoryAddress = m_StackPointerRegiter + 256;
                    m_ProcessorStatusRegister.StatusRegister = m_MemoryAccessController.readOperation(memoryAddress);
                    m_ProcessorStatusRegister.setBreakFlagOff();
                    m_ProcessorStatusRegister.setIgnoredFlagOn();

                    //4 -update processor status flags
                    //nothing needed to be done

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StackOperations.PLP.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StackOperations.PLP.Implicit.Bytes;
                    break;
                #endregion
                #region Logical Operations
                #region AND
                case Logical.AND.Immediat.OPcode:
                    //3 - perform the operation
                    byte temp = loadMemoryImmediat();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister & temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.AND.Immediat.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.AND.Immediat.Bytes;
                    break;
                case Logical.AND.ZeroPage.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryZeroPage();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister & temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.AND.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.AND.ZeroPage.Bytes;
                    break;
                case Logical.AND.ZeroPageX.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryZeroPageX();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister & temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.AND.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.AND.ZeroPageX.Bytes;
                    break;
                case Logical.AND.Absolute.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryAbsolute();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister & temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.AND.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.AND.Absolute.Bytes;
                    break;
                case Logical.AND.AbsoluteX.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryAbsoluteX();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister & temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.AND.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.AND.AbsoluteX.Bytes;
                    break;
                case Logical.AND.AbsoluteY.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryAbsoluteY();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister & temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.AND.AbsoluteY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.AND.AbsoluteY.Bytes;
                    break;
                case Logical.AND.IndirectX.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryIndirectX();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister & temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.AND.IndirectX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.AND.IndirectX.Bytes;
                    break;
                case Logical.AND.IndirectY.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryIndirectY();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister & temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.AND.IndirectY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.AND.IndirectY.Bytes;
                    break;
                #endregion
                #region EOR
                case Logical.EOR.Immediat.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryImmediat();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister ^ temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.EOR.Immediat.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.EOR.Immediat.Bytes;
                    break;
                case Logical.EOR.ZeroPage.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryZeroPage();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister ^ temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.EOR.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.EOR.ZeroPage.Bytes;
                    break;
                case Logical.EOR.ZeroPageX.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryZeroPageX();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister ^ temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.EOR.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.EOR.ZeroPageX.Bytes;
                    break;
                case Logical.EOR.Absolute.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryAbsolute();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister ^ temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.EOR.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.EOR.Absolute.Bytes;
                    break;
                case Logical.EOR.AbsoluteX.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryAbsoluteX();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister ^ temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.EOR.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.EOR.AbsoluteX.Bytes;
                    break;
                case Logical.EOR.AbsoluteY.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryAbsoluteY();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister ^ temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.EOR.AbsoluteY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.EOR.AbsoluteY.Bytes;
                    break;
                case Logical.EOR.IndirectX.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryIndirectX();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister ^ temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.EOR.IndirectX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.EOR.IndirectX.Bytes;
                    break;
                case Logical.EOR.IndirectY.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryIndirectY();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister ^ temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.EOR.IndirectY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.EOR.IndirectY.Bytes;
                    break;
                #endregion
                #region ORA
                case Logical.ORA.Immediat.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryImmediat();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister | temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.ORA.Immediat.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.ORA.Immediat.Bytes;
                    break;
                case Logical.ORA.ZeroPage.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryZeroPage();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister | temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.ORA.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.ORA.ZeroPage.Bytes;
                    break;
                case Logical.ORA.ZeroPageX.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryZeroPageX();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister | temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.ORA.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.ORA.ZeroPageX.Bytes;
                    break;
                case Logical.ORA.Absolute.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryAbsolute();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister | temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.ORA.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.ORA.Absolute.Bytes;
                    break;
                case Logical.ORA.AbsoluteX.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryAbsoluteX();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister | temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.ORA.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.ORA.AbsoluteX.Bytes;
                    break;
                case Logical.ORA.AbsoluteY.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryAbsoluteY();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister | temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.ORA.AbsoluteY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.ORA.AbsoluteY.Bytes;
                    break;
                case Logical.ORA.IndirectX.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryIndirectX();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister | temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.ORA.IndirectX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.ORA.IndirectX.Bytes;
                    break;
                case Logical.ORA.IndirectY.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryIndirectY();
                    m_AccumulatorRegister = (byte)(m_AccumulatorRegister | temp);

                    //4 -update processor status flags
                    updateZeroFlag(m_AccumulatorRegister);
                    updateNegativeFlag(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.ORA.IndirectY.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.ORA.IndirectY.Bytes;
                    break;
                #endregion
                #region BIT
                case Logical.BIT.ZeroPage.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryZeroPage();

                    //4 -update processor status flags
                    updateNegativeFlag(temp);
                    updateOverFlowFlag(temp);

                    //perform and operation and set zero page
                    temp = (byte)(m_AccumulatorRegister & temp);
                    updateZeroFlag(temp);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.BIT.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.BIT.ZeroPage.Bytes;
                    break;
                case Logical.BIT.Absolute.OPcode:
                    //3 - perform the operation
                    temp = loadMemoryAbsolute();

                    //4 -update processor status flags
                    updateNegativeFlag(temp);
                    updateOverFlowFlag(temp);

                    //perform and operation and set zero page
                    temp = (byte)(m_AccumulatorRegister & temp);
                    updateZeroFlag(temp);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Logical.BIT.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Logical.BIT.Absolute.Bytes;
                    break;
                #endregion
                #endregion
                #region Arithmetics Operations
                #region ADC
                case Arithmetic.ADC.Immediat.OPcode:
                    //3 - perform the operation
                    byte memoryValue = loadMemoryImmediat();
                    doADC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.ADC.Immediat.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.ADC.Immediat.Bytes;
                    break;
                case Arithmetic.ADC.ZeroPage.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryZeroPage();
                    doADC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.ADC.ZeroPage.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.ADC.ZeroPage.Bytes;
                    break;
                case Arithmetic.ADC.ZeroPageX.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryZeroPageX();
                    doADC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.ADC.ZeroPageX.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.ADC.ZeroPageX.Bytes;
                    break;
                case Arithmetic.ADC.Absolute.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryAbsolute();
                    doADC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.ADC.Absolute.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.ADC.Absolute.Bytes;
                    break;
                case Arithmetic.ADC.AbsoluteX.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryAbsoluteX();
                    doADC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.ADC.AbsoluteX.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.ADC.AbsoluteX.Bytes;
                    break;
                case Arithmetic.ADC.AbsoluteY.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryAbsoluteY();
                    doADC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.ADC.AbsoluteY.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.ADC.AbsoluteY.Bytes;
                    break;
                case Arithmetic.ADC.IndirectX.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryIndirectX();
                    doADC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.ADC.IndirectX.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.ADC.IndirectX.Bytes;
                    break;
                case Arithmetic.ADC.IndirectY.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryIndirectY();
                    doADC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.ADC.IndirectY.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.ADC.IndirectY.Bytes;
                    break;
                #endregion
                #region SBC
                case Arithmetic.SBC.Immediat.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryImmediat();
                    doSBC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.SBC.Immediat.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.SBC.Immediat.Bytes;
                    break;
                case Arithmetic.SBC.ZeroPage.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryZeroPage();
                    doSBC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.SBC.ZeroPage.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.SBC.ZeroPage.Bytes;
                    break;
                case Arithmetic.SBC.ZeroPageX.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryZeroPageX();
                    doSBC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.SBC.ZeroPageX.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.SBC.ZeroPageX.Bytes;
                    break;
                case Arithmetic.SBC.Absolute.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryAbsolute();
                    doSBC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.SBC.Absolute.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.SBC.Absolute.Bytes;
                    break;
                case Arithmetic.SBC.AbsoluteX.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryAbsoluteX();
                    doSBC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.SBC.AbsoluteX.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.SBC.AbsoluteX.Bytes;
                    break;
                case Arithmetic.SBC.AbsoluteY.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryAbsoluteY();
                    doSBC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.SBC.AbsoluteY.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.SBC.AbsoluteY.Bytes;
                    break;
                case Arithmetic.SBC.IndirectX.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryIndirectX();
                    doSBC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.SBC.IndirectX.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.SBC.IndirectX.Bytes;
                    break;
                case Arithmetic.SBC.IndirectY.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryIndirectY();
                    doSBC(memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.SBC.IndirectY.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.SBC.IndirectY.Bytes;
                    break;
                #endregion
                #region CMP
                case Arithmetic.CMP.Immediat.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryImmediat();
                    doCMP(m_AccumulatorRegister,memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CMP.Immediat.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CMP.Immediat.Bytes;
                    break;
                case Arithmetic.CMP.ZeroPage.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryZeroPage();
                    doCMP(m_AccumulatorRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CMP.ZeroPage.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CMP.ZeroPage.Bytes;
                    break;
                case Arithmetic.CMP.ZeroPageX.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryZeroPageX();
                    doCMP(m_AccumulatorRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CMP.ZeroPageX.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CMP.ZeroPageX.Bytes;
                    break;
                case Arithmetic.CMP.Absolute.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryAbsolute();
                    doCMP(m_AccumulatorRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CMP.Absolute.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CMP.Absolute.Bytes;
                    break;
                case Arithmetic.CMP.AbsoluteX.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryAbsoluteX();
                    doCMP(m_AccumulatorRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CMP.AbsoluteX.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CMP.AbsoluteX.Bytes;
                    break;
                case Arithmetic.CMP.AbsoluteY.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryAbsoluteY();
                    doCMP(m_AccumulatorRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CMP.AbsoluteY.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CMP.AbsoluteY.Bytes;
                    break;
                case Arithmetic.CMP.IndirectX.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryIndirectX();
                    doCMP(m_AccumulatorRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CMP.IndirectX.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CMP.IndirectX.Bytes;
                    break;
                case Arithmetic.CMP.IndirectY.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryIndirectY();
                    doCMP(m_AccumulatorRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CMP.IndirectY.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CMP.IndirectY.Bytes;
                    break;
                #endregion
                #region CPX
                case Arithmetic.CPX.Immediat.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryImmediat();
                    doCMP(m_IndexXRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CPX.Immediat.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CPX.Immediat.Bytes;
                    break;
                case Arithmetic.CPX.ZeroPage.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryZeroPage();
                    doCMP(m_IndexXRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CPX.ZeroPage.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CPX.ZeroPage.Bytes;
                    break;
                case Arithmetic.CPX.Absolute.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryAbsolute();
                    doCMP(m_IndexXRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CPX.Absolute.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CPX.Absolute.Bytes;
                    break;
                #endregion
                #region CPY
                case Arithmetic.CPY.Immediat.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryImmediat();
                    doCMP(m_IndexYRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CPY.Immediat.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CPY.Immediat.Bytes;
                    break;
                case Arithmetic.CPY.ZeroPage.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryZeroPage();
                    doCMP(m_IndexYRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CPY.ZeroPage.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CPY.ZeroPage.Bytes;
                    break;
                case Arithmetic.CPY.Absolute.OPcode:
                    //3 - perform the operation
                    memoryValue = loadMemoryAbsolute();
                    doCMP(m_IndexYRegister, memoryValue);

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Arithmetic.CPY.Absolute.Cycles;

                    //5 -update PC
                    m_ProgramCounterRegiter += Arithmetic.CPY.Absolute.Bytes;
                    break;
                #endregion
                #endregion
                #region Icrement Operations
                #region INC
                case Increment.INC.ZeroPage.OPcode:
                    //3 - perform the operation
                    byte tempValue = loadMemoryZeroPage();
                    tempValue++;
                    writeMemoryZeroPage(tempValue);

                    //4 -update processor status flags
                    updateZeroFlag(tempValue);
                    updateNegativeFlag(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Increment.INC.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Increment.INC.ZeroPage.Bytes;
                    break;
                case Increment.INC.ZeroPageX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryZeroPageX();
                    tempValue++;
                    writeMemoryZeroPageX(tempValue);

                    //4 -update processor status flags
                    updateZeroFlag(tempValue);
                    updateNegativeFlag(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Increment.INC.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Increment.INC.ZeroPageX.Bytes;
                    break;
                case Increment.INC.Absolute.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsolute();
                    tempValue++;
                    writeMemoryAbsolute(tempValue);

                    //4 -update processor status flags
                    updateZeroFlag(tempValue);
                    updateNegativeFlag(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Increment.INC.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Increment.INC.Absolute.Bytes;
                    break;
                case Increment.INC.AbsoluteX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsoluteX();
                    tempValue++;
                    writeMemoryAbsoluteX(tempValue);

                    //4 -update processor status flags
                    updateZeroFlag(tempValue);
                    updateNegativeFlag(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Increment.INC.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Increment.INC.AbsoluteX.Bytes;
                    break;
                #endregion
                case Increment.INX.Implicit.OPcode:
                    //3 - perform the operation
                    m_IndexXRegister++;

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexXRegister);
                    updateNegativeFlag(m_IndexXRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Increment.INX.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Increment.INX.Implicit.Bytes;
                    break;
                case Increment.INY.Implicit.OPcode:
                    //3 - perform the operation
                    m_IndexYRegister++;

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexYRegister);
                    updateNegativeFlag(m_IndexYRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Increment.INY.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Increment.INY.Implicit.Bytes;
                    break;
                #endregion
                #region Decrement Operations
                #region DEC
                case Decrement.DEC.ZeroPage.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryZeroPage();
                    tempValue--;
                    writeMemoryZeroPage(tempValue);

                    //4 -update processor status flags
                    updateZeroFlag(tempValue);
                    updateNegativeFlag(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Decrement.DEC.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Decrement.DEC.ZeroPage.Bytes;
                    break;
                case Decrement.DEC.ZeroPageX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryZeroPageX();
                    tempValue--;
                    writeMemoryZeroPageX(tempValue);

                    //4 -update processor status flags
                    updateZeroFlag(tempValue);
                    updateNegativeFlag(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Decrement.DEC.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Decrement.DEC.ZeroPageX.Bytes;
                    break;
                case Decrement.DEC.Absolute.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsolute();
                    tempValue--;
                    writeMemoryAbsolute(tempValue);

                    //4 -update processor status flags
                    updateZeroFlag(tempValue);
                    updateNegativeFlag(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Decrement.DEC.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Decrement.DEC.Absolute.Bytes;
                    break;
                case Decrement.DEC.AbsoluteX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsoluteX();
                    tempValue--;
                    writeMemoryAbsoluteX(tempValue);

                    //4 -update processor status flags
                    updateZeroFlag(tempValue);
                    updateNegativeFlag(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Decrement.DEC.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Decrement.DEC.AbsoluteX.Bytes;
                    break;
                #endregion
                case Decrement.DEX.Implicit.OPcode:
                    //3 - perform the operation
                    m_IndexXRegister--;

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexXRegister);
                    updateNegativeFlag(m_IndexXRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Decrement.DEX.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Decrement.DEX.Implicit.Bytes;
                    break;
                case Decrement.DEY.Implicit.OPcode:
                    //3 - perform the operation
                    m_IndexYRegister--;

                    //4 -update processor status flags
                    updateZeroFlag(m_IndexYRegister);
                    updateNegativeFlag(m_IndexYRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Decrement.DEY.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Decrement.DEY.Implicit.Bytes;
                    break;
                #endregion
                #region Shifts Operations
                #region ASL
                case Shifts.ASL.Accumelator.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = doASL(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ASL.Accumelator.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ASL.Accumelator.Bytes;
                    break;
                case Shifts.ASL.ZeroPage.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryZeroPage();
                    tempValue = doASL(tempValue);
                    writeMemoryZeroPage(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ASL.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ASL.ZeroPage.Bytes;
                    break;
                case Shifts.ASL.ZeroPageX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryZeroPageX();
                    tempValue = doASL(tempValue);
                    writeMemoryZeroPageX(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ASL.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ASL.ZeroPageX.Bytes;
                    break;
                case Shifts.ASL.Absolute.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsolute();
                    tempValue = doASL(tempValue);
                    writeMemoryAbsolute(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ASL.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ASL.Absolute.Bytes;
                    break;
                case Shifts.ASL.AbsoluteX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsoluteX();
                    tempValue = doASL(tempValue);
                    writeMemoryAbsoluteX(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ASL.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ASL.AbsoluteX.Bytes;
                    break;
                #endregion
                #region LSR
                case Shifts.LSR.Accumelator.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = doLSR(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.LSR.Accumelator.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.LSR.Accumelator.Bytes;
                    break;
                case Shifts.LSR.ZeroPage.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryZeroPage();
                    tempValue = doLSR(tempValue);
                    writeMemoryZeroPage(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.LSR.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.LSR.ZeroPage.Bytes;
                    break;
                case Shifts.LSR.ZeroPageX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryZeroPageX();
                    tempValue = doLSR(tempValue);
                    writeMemoryZeroPageX(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.LSR.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.LSR.ZeroPageX.Bytes;
                    break;
                case Shifts.LSR.Absolute.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsolute();
                    tempValue = doLSR(tempValue);
                    writeMemoryAbsolute(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.LSR.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.LSR.Absolute.Bytes;
                    break;
                case Shifts.LSR.AbsoluteX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsoluteX();
                    tempValue = doLSR(tempValue);
                    writeMemoryAbsoluteX(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.LSR.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.LSR.AbsoluteX.Bytes;
                    break;
                #endregion
                #region ROL
                case Shifts.ROL.Accumelator.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = doROL(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ROL.Accumelator.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ROL.Accumelator.Bytes;
                    break;
                case Shifts.ROL.ZeroPage.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryZeroPage();
                    tempValue = doROL(tempValue);
                    writeMemoryZeroPage(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ROL.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ROL.ZeroPage.Bytes;
                    break;
                case Shifts.ROL.ZeroPageX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryZeroPageX();
                    tempValue = doROL(tempValue);
                    writeMemoryZeroPageX(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ROL.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ROL.ZeroPageX.Bytes;
                    break;
                case Shifts.ROL.Absolute.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsolute();
                    tempValue = doROL(tempValue);
                    writeMemoryAbsolute(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ROL.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ROL.Absolute.Bytes;
                    break;
                case Shifts.ROL.AbsoluteX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsoluteX();
                    tempValue = doROL(tempValue);
                    writeMemoryAbsoluteX(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ROL.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ROL.AbsoluteX.Bytes;
                    break;
                #endregion
                #region ROR
                case Shifts.ROR.Accumelator.OPcode:
                    //3 - perform the operation
                    m_AccumulatorRegister = doROR(m_AccumulatorRegister);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ROR.Accumelator.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ROR.Accumelator.Bytes;
                    break;
                case Shifts.ROR.ZeroPage.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryZeroPage();
                    tempValue = doROR(tempValue);
                    writeMemoryZeroPage(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ROR.ZeroPage.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ROR.ZeroPage.Bytes;
                    break;
                case Shifts.ROR.ZeroPageX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryZeroPageX();
                    tempValue = doROR(tempValue);
                    writeMemoryZeroPageX(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ROR.ZeroPageX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ROR.ZeroPageX.Bytes;
                    break;
                case Shifts.ROR.Absolute.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsolute();
                    tempValue = doROR(tempValue);
                    writeMemoryAbsolute(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ROR.Absolute.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ROR.Absolute.Bytes;
                    break;
                case Shifts.ROR.AbsoluteX.OPcode:
                    //3 - perform the operation
                    tempValue = loadMemoryAbsoluteX();
                    tempValue = doROR(tempValue);
                    writeMemoryAbsoluteX(tempValue);

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += Shifts.ROR.AbsoluteX.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += Shifts.ROR.AbsoluteX.Bytes;
                    break;
                #endregion
                #endregion
                #region Jump And Calls Operations
                #region JMP
                case JumpCalls.JMP.Absolute.OPcode:
                    //3 - perform the operation
                    byte LSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);
                    byte MSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 2);

                    ushort newMemoryLocation = Memory.MemoryUtility.readUShort(LSB, MSB);

                    m_ProgramCounterRegiter = newMemoryLocation;

                    //4 -no flags to update

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += JumpCalls.JMP.Absolute.Cycles;

                    //6 -no PC update - this instruction alter PC
                    break;
                case JumpCalls.JMP.Indirect.OPcode:
                    //3 - perform the operation
                    byte LocationMemoryValueLSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);
                    byte LocationMemoryValueMSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 2);
                    ushort LocationMemoryValue = Memory.MemoryUtility.readUShort(LocationMemoryValueLSB,LocationMemoryValueMSB);
                    
                    LSB = m_MemoryAccessController.readOperation(LocationMemoryValue);
                    //do not cross page boundary as disscussed on JMP Note
                    if (LocationMemoryValueLSB != 255)
                    {
                        MSB = m_MemoryAccessController.readOperation(LocationMemoryValue + 1);
                    }
                    else
                    {
                        MSB = m_MemoryAccessController.readOperation(Memory.MemoryUtility.readUShort(0,LocationMemoryValueMSB));
                    }

                    newMemoryLocation = Memory.MemoryUtility.readUShort(LSB, MSB);

                    m_ProgramCounterRegiter = newMemoryLocation;

                    //4 -no flags to update

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += JumpCalls.JMP.Indirect.Cycles;

                    //6 -no PC update - this instruction alter PC
                    break;
                #endregion
                case JumpCalls.JSR.Absolute.OPcode:
                    //3 - perform the operation
                    //calculate return point
                    ushort returnAddress = (ushort)(m_ProgramCounterRegiter + JumpCalls.JSR.Absolute.Bytes - 1); //return point - 1 ?? because RTS will increment it
                    
                    //split to LSB and MSB
                    Memory.MemoryUtility.splitUShort(returnAddress,out LSB,out MSB);
                    
                    //push MSB then LSB to stack and update stack pointer
                    memoryAddress = m_StackPointerRegiter + 256;
                    m_MemoryAccessController.writeOperation(memoryAddress, MSB);
                    m_MemoryAccessController.writeOperation(memoryAddress-1, LSB);
                    m_StackPointerRegiter -= 2;

                    //load the new PC
                    LSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 1);
                    MSB = m_MemoryAccessController.readOperation(m_ProgramCounterRegiter + 2);

                    newMemoryLocation = Memory.MemoryUtility.readUShort(LSB, MSB);

                    m_ProgramCounterRegiter = newMemoryLocation;
                    
                    //4 -no flags to update

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += JumpCalls.JSR.Absolute.Cycles;

                    //6 -no PC update - this instruction alter PC
                    break;
                case JumpCalls.RTS.Implicit.OPcode:
                    //3 - perform the operation
                    
                    //pull LSB then MSB from stack and update stack pointer
                    memoryAddress = m_StackPointerRegiter + 256;
                    LSB = m_MemoryAccessController.readOperation(memoryAddress + 1);
                    MSB = m_MemoryAccessController.readOperation(memoryAddress + 2);
                    m_StackPointerRegiter += 2;

                    //adjust PC
                    m_ProgramCounterRegiter = Memory.MemoryUtility.readUShort(LSB, MSB);
                    m_ProgramCounterRegiter++;

                    //4 -no flags to update

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += JumpCalls.RTS.Implicit.Cycles;

                    //6 -no PC update - this instruction alter PC
                    break;
                #endregion
                #region Branches Operations
                case Branches.BCC.Relative.OPcode:
                    //3 - perform the operation
                    sbyte relativeAddress = loadMemoryRelative();

                    if (!m_ProcessorStatusRegister.testCarryFlag())
                    {
                        //1 more cycles if branch succeeds
                        m_CPUCyclesCounter++;

                        //update program counter
                        byte LSBOld;
                        byte MSBOld;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter,out LSBOld,out MSBOld);
                        m_ProgramCounterRegiter = (ushort)(m_ProgramCounterRegiter + relativeAddress + 2);

                        //2 more cycles if go to onther page
                        byte LSBNew;
                        byte MSBNew;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter,out LSBNew,out MSBNew);
                        if(MSBNew != MSBOld)
                            m_CPUCyclesCounter+=2;
                    }
                    else
                    {
                        //update program counter
                        m_ProgramCounterRegiter += Branches.BCC.Relative.Bytes;
                    }

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Branches.BCC.Relative.Cycles;
                    break;
                case Branches.BCS.Relative.OPcode:
                    //3 - perform the operation
                    relativeAddress = loadMemoryRelative();

                    if (m_ProcessorStatusRegister.testCarryFlag())
                    {
                        //1 more cycles if branch succeeds
                        m_CPUCyclesCounter++;

                        //update program counter
                        byte LSBOld;
                        byte MSBOld;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBOld, out MSBOld);
                        m_ProgramCounterRegiter = (ushort)(m_ProgramCounterRegiter + relativeAddress + 2);

                        //2 more cycles if go to onther page
                        byte LSBNew;
                        byte MSBNew;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBNew, out MSBNew);
                        if (MSBNew != MSBOld)
                            m_CPUCyclesCounter += 2;
                    }
                    else
                    {
                        //update program counter
                        m_ProgramCounterRegiter += Branches.BCS.Relative.Bytes;
                    }

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Branches.BCS.Relative.Cycles;
                    break;
                case Branches.BNE.Relative.OPcode:
                    //3 - perform the operation
                    relativeAddress = loadMemoryRelative();

                    if (!m_ProcessorStatusRegister.testZeroFlag())
                    {
                        //1 more cycles if branch succeeds
                        m_CPUCyclesCounter++;

                        //update program counter
                        byte LSBOld;
                        byte MSBOld;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBOld, out MSBOld);
                        m_ProgramCounterRegiter = (ushort)(m_ProgramCounterRegiter + relativeAddress + 2);

                        //2 more cycles if go to onther page
                        byte LSBNew;
                        byte MSBNew;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBNew, out MSBNew);
                        if (MSBNew != MSBOld)
                            m_CPUCyclesCounter += 2;
                    }
                    else
                    {
                        //update program counter
                        m_ProgramCounterRegiter += Branches.BNE.Relative.Bytes;
                    }

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Branches.BNE.Relative.Cycles;
                    break;
                case Branches.BEQ.Relative.OPcode:
                    //3 - perform the operation
                    relativeAddress = loadMemoryRelative();

                    if (m_ProcessorStatusRegister.testZeroFlag())
                    {
                        //1 more cycles if branch succeeds
                        m_CPUCyclesCounter++;

                        //update program counter
                        byte LSBOld;
                        byte MSBOld;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBOld, out MSBOld);
                        m_ProgramCounterRegiter = (ushort)(m_ProgramCounterRegiter + relativeAddress + 2);

                        //2 more cycles if go to onther page
                        byte LSBNew;
                        byte MSBNew;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBNew, out MSBNew);
                        if (MSBNew != MSBOld)
                            m_CPUCyclesCounter += 2;
                    }
                    else
                    {
                        //update program counter
                        m_ProgramCounterRegiter += Branches.BEQ.Relative.Bytes;
                    }

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Branches.BEQ.Relative.Cycles;
                    break;
                case Branches.BPL.Relative.OPcode:
                    //3 - perform the operation
                    relativeAddress = loadMemoryRelative();

                    if (!m_ProcessorStatusRegister.testNegativeFlag())
                    {
                        //1 more cycles if branch succeeds
                        m_CPUCyclesCounter++;

                        //update program counter
                        byte LSBOld;
                        byte MSBOld;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBOld, out MSBOld);
                        m_ProgramCounterRegiter = (ushort)(m_ProgramCounterRegiter + relativeAddress + 2);

                        //2 more cycles if go to onther page
                        byte LSBNew;
                        byte MSBNew;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBNew, out MSBNew);
                        if (MSBNew != MSBOld)
                            m_CPUCyclesCounter += 2;
                    }
                    else
                    {
                        //update program counter
                        m_ProgramCounterRegiter += Branches.BPL.Relative.Bytes;
                    }

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Branches.BPL.Relative.Cycles;
                    break;
                case Branches.BMI.Relative.OPcode:
                    //3 - perform the operation
                    relativeAddress = loadMemoryRelative();

                    if (m_ProcessorStatusRegister.testNegativeFlag())
                    {
                        //1 more cycles if branch succeeds
                        m_CPUCyclesCounter++;

                        //update program counter
                        byte LSBOld;
                        byte MSBOld;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBOld, out MSBOld);
                        m_ProgramCounterRegiter = (ushort)(m_ProgramCounterRegiter + relativeAddress + 2);

                        //2 more cycles if go to onther page
                        byte LSBNew;
                        byte MSBNew;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBNew, out MSBNew);
                        if (MSBNew != MSBOld)
                            m_CPUCyclesCounter += 2;
                    }
                    else
                    {
                        //update program counter
                        m_ProgramCounterRegiter += Branches.BMI.Relative.Bytes;
                    }

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Branches.BMI.Relative.Cycles;
                    break;
                case Branches.BVC.Relative.OPcode:
                    //3 - perform the operation
                    relativeAddress = loadMemoryRelative();

                    if (!m_ProcessorStatusRegister.testOverflowFlag())
                    {
                        //1 more cycles if branch succeeds
                        m_CPUCyclesCounter++;

                        //update program counter
                        byte LSBOld;
                        byte MSBOld;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBOld, out MSBOld);
                        m_ProgramCounterRegiter = (ushort)(m_ProgramCounterRegiter + relativeAddress + 2);

                        //2 more cycles if go to onther page
                        byte LSBNew;
                        byte MSBNew;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBNew, out MSBNew);
                        if (MSBNew != MSBOld)
                            m_CPUCyclesCounter += 2;
                    }
                    else
                    {
                        //update program counter
                        m_ProgramCounterRegiter += Branches.BVC.Relative.Bytes;
                    }

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Branches.BVC.Relative.Cycles;
                    break;
                case Branches.BVS.Relative.OPcode:
                    //3 - perform the operation
                    relativeAddress = loadMemoryRelative();

                    if (m_ProcessorStatusRegister.testOverflowFlag())
                    {
                        //1 more cycles if branch succeeds
                        m_CPUCyclesCounter++;

                        //update program counter
                        byte LSBOld;
                        byte MSBOld;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBOld, out MSBOld);
                        m_ProgramCounterRegiter = (ushort)(m_ProgramCounterRegiter + relativeAddress + 2);

                        //2 more cycles if go to onther page
                        byte LSBNew;
                        byte MSBNew;
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSBNew, out MSBNew);
                        if (MSBNew != MSBOld)
                            m_CPUCyclesCounter += 2;
                    }
                    else
                    {
                        //update program counter
                        m_ProgramCounterRegiter += Branches.BVS.Relative.Bytes;
                    }

                    //4 -update cpu cycles    
                    m_CPUCyclesCounter += Branches.BVS.Relative.Cycles;
                    break;
                #endregion
                #region Status Flag Changes Operations
                case StatusFlags.CLC.Implicit.OPcode:
                    //3 - perform the operation
                    m_ProcessorStatusRegister.setCarryFlagOff();

                    //4 -no other flags updated

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StatusFlags.CLC.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StatusFlags.CLC.Implicit.Bytes;
                    break;
                case StatusFlags.SEC.Implicit.OPcode:
                    //3 - perform the operation
                    m_ProcessorStatusRegister.setCarryFlagOn();

                    //4 -no other flags updated

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StatusFlags.SEC.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StatusFlags.SEC.Implicit.Bytes;
                    break;
                case StatusFlags.CLD.Implicit.OPcode:
                    //3 - perform the operation
                    m_ProcessorStatusRegister.setDecimalFlagOff();

                    //4 -no other flags updated

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StatusFlags.CLD.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StatusFlags.CLD.Implicit.Bytes;
                    break;
                case StatusFlags.SED.Implicit.OPcode:
                    //3 - perform the operation
                    m_ProcessorStatusRegister.setDecimalFlagOn();

                    //4 -no other flags updated

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StatusFlags.SED.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StatusFlags.SED.Implicit.Bytes;
                    break;
                case StatusFlags.CLI.Implicit.OPcode:
                    //3 - perform the operation
                    m_ProcessorStatusRegister.setInterruptFlagOff();

                    //4 -no other flags updated

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StatusFlags.CLI.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StatusFlags.CLI.Implicit.Bytes;
                    break;
                case StatusFlags.SEI.Implicit.OPcode:
                    //3 - perform the operation
                    m_ProcessorStatusRegister.setInterruptFlagOn();

                    //4 -no other flags updated

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StatusFlags.SEI.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StatusFlags.SEI.Implicit.Bytes;
                    break;
                case StatusFlags.CLV.Implicit.OPcode:
                    //3 - perform the operation
                    m_ProcessorStatusRegister.setOverflowFlagOff();

                    //4 -no other flags updated

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += StatusFlags.CLV.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += StatusFlags.CLV.Implicit.Bytes;
                    break;
                #endregion
                #region System Function Operations
                case SystemFunctions.BRK.Implicit.OPcode:
                    if (!m_ProcessorStatusRegister.testInterruptFlag())
                    {
                        //3 -update PC for the return to start after the BRK request
                        m_ProgramCounterRegiter += SystemFunctions.BRK.Implicit.Bytes;

                        //4 - perform the operation
                            //a - push program counter and processor status to stack
                                //split to LSB and MSB
                        Memory.MemoryUtility.splitUShort(m_ProgramCounterRegiter, out LSB, out MSB);

                                //push MSB then LSB to stack and update stack pointer
                        memoryAddress = m_StackPointerRegiter + 256;
                        m_MemoryAccessController.writeOperation(memoryAddress, MSB);
                        m_MemoryAccessController.writeOperation(memoryAddress - 1, LSB);
                                //push status register
                        m_MemoryAccessController.writeOperation(memoryAddress - 2, m_ProcessorStatusRegister.StatusRegister);
                                //update stack pointer
                        m_StackPointerRegiter -= 3;

                            //b - set interrupt disable flag on
                        m_ProcessorStatusRegister.setInterruptFlagOn();

                            //c - set PC to the IRQ inturrupt vector (0xFFFE/F)
                                //load the new PC
                        LSB = m_MemoryAccessController.readOperation(65534); //0xFFFE
                        MSB = m_MemoryAccessController.readOperation(65535); //0xFFFF

                        newMemoryLocation = Memory.MemoryUtility.readUShort(LSB, MSB);
                        m_ProgramCounterRegiter = newMemoryLocation;

                        //5 - update processor status flags
                        m_ProcessorStatusRegister.setBreakFlagOn();

                        //6 -update cpu cycles    
                        m_CPUCyclesCounter += SystemFunctions.BRK.Implicit.Cycles;
                    }
                    else
                    {
                        //ignore request 

                        //just update PC
                        m_ProgramCounterRegiter += SystemFunctions.BRK.Implicit.Bytes;
                    }
                    break;
                case SystemFunctions.NOP.Implicit.OPcode:
                    //3 - nothing to perform

                    //4 -no flags to update

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += SystemFunctions.NOP.Implicit.Cycles;

                    //6 -update PC
                    m_ProgramCounterRegiter += SystemFunctions.NOP.Implicit.Bytes;
                    break;
                case SystemFunctions.RTI.Implicit.OPcode:
                    //3 - perform the operation
                        //a - pull status register from stack
                    memoryAddress = m_StackPointerRegiter + 256;
                    m_ProcessorStatusRegister.StatusRegister = m_MemoryAccessController.readOperation(memoryAddress + 1);
                    m_ProcessorStatusRegister.setIgnoredFlagOn();
                        //b - pull LSB then MSB from stack
                    LSB = m_MemoryAccessController.readOperation(memoryAddress + 2);
                    MSB = m_MemoryAccessController.readOperation(memoryAddress + 3);

                        //c - update stack pointer
                    m_StackPointerRegiter += 3;

                        //d - set old PC
                    newMemoryLocation = Memory.MemoryUtility.readUShort(LSB, MSB);
                    m_ProgramCounterRegiter = newMemoryLocation;

                    //5 -update cpu cycles    
                    m_CPUCyclesCounter += SystemFunctions.BRK.Implicit.Cycles;
                    break;
                #endregion
                default:
                    throw new Exception("Instruction Not Supported - Operation Code : " + OPCode.ToString() + " | 0x"+ OPCode.ToString("X2"));
            }
        }

        public void doADC(byte value)
        {
            int tempValue;

            if (m_ProcessorStatusRegister.testCarryFlag())
                tempValue = m_AccumulatorRegister + value + 1;
            else
                tempValue = m_AccumulatorRegister + value;

            //update carry flage status
            if (tempValue > 255)
                m_ProcessorStatusRegister.setCarryFlagOn();
            else
                m_ProcessorStatusRegister.setCarryFlagOff();

            //save tremed value back to accumelator
            byte tempValueAsByte = (byte)tempValue;

            //update overflow flag
            if ((value < 128 && m_AccumulatorRegister < 128 && tempValueAsByte >= 128) ||
               (value >= 128 && m_AccumulatorRegister >= 128 && tempValueAsByte < 128))
                m_ProcessorStatusRegister.setOverflowFlagOn();
            else
                m_ProcessorStatusRegister.setOverflowFlagOff();

            m_AccumulatorRegister = tempValueAsByte;

            //update processor status flags
            updateZeroFlag(m_AccumulatorRegister);
            updateNegativeFlag(m_AccumulatorRegister);
        }

        public void doSBC(byte value)
        {
            int tempValue;

            if (m_ProcessorStatusRegister.testCarryFlag())
                tempValue = m_AccumulatorRegister - value;
            else
                tempValue = m_AccumulatorRegister - value - 1;

            //update carry flage status
            if (tempValue >= 0)
                m_ProcessorStatusRegister.setCarryFlagOn();
            else
                m_ProcessorStatusRegister.setCarryFlagOff();

            //save tremed value back to accumelator
            byte tempValueAsByte = (byte)tempValue;

            //update overflow flag
            if ((value >= 128 && m_AccumulatorRegister < 128 && tempValueAsByte >= 128) ||
               (value < 128 && m_AccumulatorRegister >= 128 && tempValueAsByte < 128))
                m_ProcessorStatusRegister.setOverflowFlagOn();
            else
                m_ProcessorStatusRegister.setOverflowFlagOff();

            m_AccumulatorRegister = tempValueAsByte;

            //update processor status flags
            updateZeroFlag(m_AccumulatorRegister);
            updateNegativeFlag(m_AccumulatorRegister);
        }

        public void doCMP(byte register,byte value)
        {
            byte tempValue = (byte)(register - value);

            //update carry flage status
            if (register >= value)
                m_ProcessorStatusRegister.setCarryFlagOn();
            else
                m_ProcessorStatusRegister.setCarryFlagOff();

            //update zero flags
            if (tempValue == 0)
                m_ProcessorStatusRegister.setZeroFlagOn();
            else
                m_ProcessorStatusRegister.setZeroFlagOff();

            //update nigative
            if (tempValue >= 128)
                m_ProcessorStatusRegister.setNegativeFlagOn();
            else
                m_ProcessorStatusRegister.setNegativeFlagOff();
        }

        public byte doASL(byte value)
        {
            byte newValue = (byte)(value << 1);

            //update carry flag
            if (value > 127)
                m_ProcessorStatusRegister.setCarryFlagOn();
            else
                m_ProcessorStatusRegister.setCarryFlagOff();

            //4 -update processor status flags
            updateZeroFlag(newValue);
            updateNegativeFlag(newValue);

            return newValue;
        }

        public byte doLSR(byte value)
        {
            byte newValue = (byte)(value >> 1);

            //update carry flag
            if ((value & 0x01) == 0x01) //if lest significan bit is 1 
                m_ProcessorStatusRegister.setCarryFlagOn();
            else
                m_ProcessorStatusRegister.setCarryFlagOff();

            //4 -update processor status flags
            updateZeroFlag(newValue);
            updateNegativeFlag(newValue);

            return newValue;
        }

        public byte doROL(byte value)
        {
            byte newValue = (byte)(value << 1);

            //adjust lest significan bit with the carry flag
            if (m_ProcessorStatusRegister.testCarryFlag())
                newValue = (byte)(newValue | 0x01);//we can simply add 1 - it will have the same effect ??

            //update carry flag
            if (value > 127)
                m_ProcessorStatusRegister.setCarryFlagOn();
            else
                m_ProcessorStatusRegister.setCarryFlagOff();

            //4 -update processor status flags
            updateZeroFlag(newValue);
            updateNegativeFlag(newValue);

            return newValue;
        }

        public byte doROR(byte value)
        {
            byte newValue = (byte)(value >> 1);

            //adjust most significan bit with the carry flag
            if (m_ProcessorStatusRegister.testCarryFlag())
                newValue = (byte)(newValue | 0x80);

            //update carry flag
            if ((value & 0x01) == 0x01) //if lest significan bit is 1 
                m_ProcessorStatusRegister.setCarryFlagOn();
            else
                m_ProcessorStatusRegister.setCarryFlagOff();

            //4 -update processor status flags
            updateZeroFlag(newValue);
            updateNegativeFlag(newValue);

            return newValue;
        }
        #endregion

        #endregion
    }
}
