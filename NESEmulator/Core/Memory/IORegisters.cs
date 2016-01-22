using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.Memory
{
    /// <summary>
    /// Handle all Input/Output Registers memory operation
    /// 
    /// The memory mapped I/O registers are located at $2000-$401F. Loc
    /// ations $2000-$2007 are mirrored every 8 bytes in the region 
    /// $2008-$3FFF.
    ///
    /// this is the how I/O part in NES CPU memory look
    /// 
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
    /// </summary>
    class IORegisters : MemoryComponant
    {
        #region Constants

        public const int MemoryRegionStartIndex = 8192;
        public const int MemoryRegionEndIndex = 16415;
        public const int MemoryRegionLength = 8224;

        public const int PPUIORegitersStartIndex = 8192;
        public const int PPUIORegitersEndIndex = 8199;
        public const int PPUIORegitersLength = 8;

        public const int PPUIOMirrorsStartIndex = 8200;
        public const int PPUIOMirrorsEndIndex = 16383;
        public const int PPUIOMirrorsPageLength = 8184;

        public const int SNDINPRegistersStartIndex = 16384;
        public const int SNDINPRegistersEndIndex = 16415;
        public const int SNDINPRegistersLength = 32;

        #endregion

        #region members

        override public void writeMemory(int address,byte value)
        {
            //if PPU register memory address
            if (address < SNDINPRegistersStartIndex)
            {
                //get the base byte index
                int baseAddress = address;
                int offset = 0;
                if (baseAddress > PPUIORegitersEndIndex)
                {
                    offset = baseAddress / PPUIORegitersStartIndex;
                    baseAddress = baseAddress - (offset * PPUIORegitersLength);
                }

                //write value in the base address and all addresses tell end of the mirrors space
                for (int i = baseAddress; i < SNDINPRegistersStartIndex; i += PPUIORegitersLength)
                {
                    m_MainMemoryArray[i] = value;
                }
            }
            else
                m_MainMemoryArray[address] = value;
        }

        #endregion
    }
}
