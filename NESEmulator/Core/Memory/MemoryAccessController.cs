using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NESEmulator.Core.Memory;

namespace NESEmulator.Core.Memory
{
    /// <summary>
    /// - contains the main memory buffer of the system
    /// - handle all memory access of the system
    /// 
    /// the memory structure look like the below discription , for more details please look at te desired class
    /// 
    ///  ______________________________  $10000
    /// |                              |
    /// |   PRG-ROM Upper Bank         |
    /// |                              |
    /// |------------------------------| SC000
    /// |                              |
    /// |   PRG-ROM Lower Bank         |
    /// |______________________________| $8000
    /// |                              |
    /// |         Save ROM             |
    /// |______________________________| $6000
    /// |                              |
    /// |         Expansion ROM        |
    /// |______________________________| $4020
    /// |                              |
    /// | Sound + Input  I/O Registers |
    /// |------------------------------| $4000
    /// |                              |
    /// |         Mirrors              |
    /// |      $2000 - $2007           |
    /// |------------------------------| S2008
    /// |                              |
    /// |   PPU  I/O Registers         |
    /// |______________________________| $2000
    /// |                              |
    /// |         Mirrors              |
    /// |      $0000 - $07FF           |
    /// |------------------------------| $0800
    /// |                              |
    /// |           RAM                |
    /// |------------------------------| $0200
    /// |                              |
    /// |          Stack               |
    /// |------------------------------| S0100
    /// |                              |
    /// |        Zero Page             |
    /// |______________________________| $0000
    /// </summary>
    class MemoryAccessController
    {
        #region Constants
        /// <summary>
        /// 64 KByte of memory
        /// </summary>
        public static int MemoryTotalSizeInBytes = 64 * 1024; //64KB memory buffer

        public const int RAMMemoryRegionStartIndex = RAM.MemoryRegionStartIndex;
        public const int RAMMemoryRegionEndIndex = RAM.MemoryRegionEndIndex;
        public const int RAMMemoryRegionLength = RAM.MemoryRegionLength;

        public const int IORegistersRegionStartIndex = IORegisters.MemoryRegionStartIndex;
        public const int IORegistersRegionEndIndex = IORegisters.MemoryRegionEndIndex;
        public const int IORegistersRegionLength = IORegisters.MemoryRegionLength;

        public const int EROMRegionStartIndex = ExpansionROM.MemoryRegionStartIndex;
        public const int EROMRegionEndIndex = ExpansionROM.MemoryRegionEndIndex;
        public const int EROMRegionLength = ExpansionROM.MemoryRegionLength;

        public const int SRAMRegionStartIndex = SaveRAM.MemoryRegionStartIndex;
        public const int SRAMRegionEndIndex = SaveRAM.MemoryRegionEndIndex;
        public const int SRAMRegionLength = SaveRAM.MemoryRegionLength;

        public const int ROMRegionStartIndex = ROM.MemoryRegionStartIndex;
        public const int ROMRegionEndIndex = ROM.MemoryRegionEndIndex;
        public const int ROMRegionLength = ROM.MemoryRegionLength;

        #endregion

        #region members

        /// <summary>
        /// all memory buffer of the system
        /// </summary>
        byte[] m_MainMemoryBuffer;

        /// <summary>
        /// RAM memory componant
        /// </summary>
        RAM m_ram;

        /// <summary>
        /// I/O Registers memory componant
        /// </summary>
        IORegisters m_ioRegisters;

        /// <summary>
        /// Expansion ROM memory componant
        /// </summary>
        ExpansionROM m_expansionROM;

        /// <summary>
        /// Save/Write RAM memory componant
        /// </summary>
        SaveRAM m_saveRAM;

        /// <summary>
        /// ROM memory componant
        /// </summary>
        ROM m_rom;

        public MemoryAccessController(RAM ram, IORegisters ioRegisters,
                                      ExpansionROM eROM,SaveRAM sRAM,ROM rom)
        {
            //initialize
            m_MainMemoryBuffer = new byte[MemoryTotalSizeInBytes];

            m_ram = ram;
            m_ioRegisters = ioRegisters;
            m_expansionROM = eROM;
            m_saveRAM = sRAM;
            m_rom = rom;

            m_ram.initailzie(m_MainMemoryBuffer);
            m_ioRegisters.initailzie(m_MainMemoryBuffer);
            m_expansionROM.initailzie(m_MainMemoryBuffer);
            m_saveRAM.initailzie(m_MainMemoryBuffer);
            m_rom.initailzie(m_MainMemoryBuffer);
        }

        /// <summary>
        /// read from the desired memory componant base on the address
        /// </summary>
        public byte readOperation(int address)
        {
            if (address <= RAMMemoryRegionEndIndex)
                return m_ram.readMemory(address);
            else if (address <= IORegistersRegionEndIndex)
                return m_ioRegisters.readMemory(address);
            else if (address <= EROMRegionEndIndex)
                return m_expansionROM.readMemory(address);
            else if (address <= SRAMRegionEndIndex)
                return m_saveRAM.readMemory(address);
            else if (address <= ROMRegionEndIndex)
                return m_rom.readMemory(address);
            else
                throw new Exception("not allowed memory locations - Location : " + address.ToString());
        }

        /// <summary>
        /// write to the desired memory componant base on the address
        /// </summary>
        public void writeOperation(int address,byte value)
        {
            if (address <= RAMMemoryRegionEndIndex)
                m_ram.writeMemory(address, value);
            else if (address <= IORegistersRegionStartIndex)
                m_ioRegisters.writeMemory(address, value);
            else if (address <= EROMRegionStartIndex)
                m_expansionROM.writeMemory(address, value);
            else if (address <= SRAMRegionStartIndex)
                m_saveRAM.writeMemory(address, value);
            else if (address <= ROMRegionStartIndex)
                m_rom.writeMemory(address, value);
            else
                throw new Exception("not allowed memory locations - Location : " + address.ToString());
        }

        public byte[] getInternalMemoryBuffer()
        {
            return m_MainMemoryBuffer;
        }
        #endregion
    }
}
