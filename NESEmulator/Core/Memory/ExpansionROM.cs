using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.Memory
{
    /// <summary>
    /// Handle all Expansion ROM memory operation
    /// 
    ///
    /// this is the how Expansion ROM part in NES CPU memory look
    /// 
    /// |______________________________| $6000
    /// |                              |
    /// |         Expansion ROM        |
    /// |______________________________| $4020
    /// </summary>
    class ExpansionROM : MemoryComponant
    {
        #region Constants

        public const int MemoryRegionStartIndex = 16416;
        public const int MemoryRegionEndIndex = 24575;
        public const int MemoryRegionLength = 8160;

        #endregion

        #region members

        #endregion
    }
}
