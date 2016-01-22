using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.Memory
{
    /// <summary>
    /// Handle all Save RAM memory operation
    ///
    /// SRAM (WRAM) is the Save RAM, the addresses used to access RAM in the cartridges for storing save 
    /// games.
    /// 
    /// if it is battary backed it must write it's contents to disk to save (NOT IMPLEMENETED YET)
    ///
    /// this is the how Save RAM part in NES CPU memory look
    /// 
    /// |______________________________| $8000
    /// |                              |
    /// |         Save ROM             |
    /// |______________________________| $6000
    /// </summary>
    class SaveRAM : MemoryComponant
    {
         #region Constants

        public const int MemoryRegionStartIndex = 24576;
        public const int MemoryRegionEndIndex = 32767;
        public const int MemoryRegionLength = 8192;

        #endregion

        #region members

        override public void writeMemory(int address,byte value)
        {
            base.writeMemory(address,value);

            //if battary saved also save to disk file !!!!
        }

        #endregion
    }
}
