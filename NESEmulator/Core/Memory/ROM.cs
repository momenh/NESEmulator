using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.Memory
{
    /// <summary>
    /// Handle all ROM memory operation
    /// 
    /// From $8000 onwards is the addresses allocated to cartridge PRG-ROM. Games with only 
    /// one 16 KB bank of PRG-ROM will load it into both $8000 and $C000. This is to ensure that 
    /// the vector table is located in the correct addresses. Games with two 16 KB PRG-ROM banks 
    /// will load one into $8000 and the other into $C000. Games with more than two banks use 
    /// memory mappers to determine which banks to load into memory. The memory mapper 
    /// monitors memory writes for a specific address (or range of addresses) and when that 
    /// address is written to, it performs a bank switch.
    ///
    /// this is the how ROM in NES CPU memory look
    /// 
    ///  ______________________________  $10000
    /// |                              |
    /// |   PRG-ROM Upper Bank         |
    /// |                              |
    /// |------------------------------| SC000
    /// |                              |
    /// |   PRG-ROM Lower Bank         |
    /// |______________________________| $8000
    /// </summary>
    class ROM : MemoryComponant
    {
         #region Constants

        public const int MemoryRegionStartIndex = 32768;
        public const int MemoryRegionEndIndex = 65535;
        public const int MemoryRegionLength = 32768;

        public const int ROMLowerBankStartIndex = 32768;
        public const int ROMLowerBankEndIndex = 49151;
        public const int ROMLowerBankLength = 16384;

        public const int ROMUpperBankStartIndex = 49152;
        public const int ROMUpperBankEndIndex = 65535;
        public const int ROMUpperBankLength = 16384;

        #endregion

        #region members

        override public void writeMemory(int address,byte value)
        {
            //Do Nothing ! this is ROM (Read Only Memory)
        }

        #endregion
    }
}
