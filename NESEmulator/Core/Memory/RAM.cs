using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.Memory
{
    /// <summary>
    /// Handle all RAM memory operation
    /// 
    /// Zero Page refers to addresses in the range $0000-$00FF, that is the first page in memory 
    /// and is used by certain addressing modes to allow quicker execution . Memory locations 
    /// $0000-$07FF are mirrored three times at $0800-$1FFF. This means that, for example, any 
    /// data written to $0000 will also be written to $0800, $1000 and $1800. 
    ///
    /// this is the how RAM part in NES CPU memory look
    /// 
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
    class RAM : MemoryComponant
    {
        #region Constants

        public const int MemoryRegionStartIndex = 0;
        public const int MemoryRegionEndIndex = 8191;
        public const int MemoryRegionLength = 8192;

        public const int ZeroPageStartIndex = 0;
        public const int ZeroPageEndIndex = 255;
        public const int ZeroPageLength = 256;

        public const int StackPageStartIndex = 256;
        public const int StackPageEndIndex = 511;
        public const int StackPageLength = 256;

        public const int RAMPageStartIndex = 512;
        public const int RAMPageEndIndex = 2047;
        public const int RAMPageLength = 1536;

        public const int MirrorZeroPageStartIndex = 0;
        public const int MirrorZeroPageEndIndex = 2047;
        public const int MirrorZeroPageLength = 2048;

        public const int Mirror1stPageStartIndex = 2048;
        public const int Mirror1stPageEndIndex = 4095;
        public const int Mirror1stPageLength = MirrorZeroPageLength;

        public const int Mirror2ndPageStartIndex = 4096;
        public const int Mirror2ndPageEndIndex = 6143;
        public const int Mirror2ndPageLength = MirrorZeroPageLength;

        public const int Mirror3rdPageStartIndex = 6144;
        public const int Mirror3rdPageEndIndex = 8191;
        public const int Mirror3rdPageLength = MirrorZeroPageLength;

        #endregion

        #region members

        override public void writeMemory(int address,byte value)
        {
            //adjust address to be in the zero mirror
            while (address > MirrorZeroPageEndIndex)
                address -= MirrorZeroPageLength;

            //write to zero page
            m_MainMemoryArray[address] = value;

            //write to 1st mirror
            address += MirrorZeroPageLength;
            m_MainMemoryArray[address] = value;

            //write to 2nd mirror
            address += MirrorZeroPageLength;
            m_MainMemoryArray[address] = value;

            //write to 3rd mirror
            address += MirrorZeroPageLength;
            m_MainMemoryArray[address] = value;
        }

        #endregion
    }
}
