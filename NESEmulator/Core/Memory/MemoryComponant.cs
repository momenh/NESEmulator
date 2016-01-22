using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESEmulator.Core.Memory
{
    /// <summary>
    /// Any memory componant will be inherted from this class
    /// </summary>
    class MemoryComponant
    {
        #region members

        /// <summary>
        /// pointer to the full memory of the system passed to the class constructor
        /// </summary>
        protected byte[] m_MainMemoryArray;

        public void initailzie(byte[] mainMemoryArray)
        {
            m_MainMemoryArray = mainMemoryArray;
        }

        virtual public byte readMemory(int address)
        {
            return m_MainMemoryArray[address];
        }

        virtual public void writeMemory(int address,byte value)
        {
            m_MainMemoryArray[address] = value;

        }
        #endregion
    }
}
