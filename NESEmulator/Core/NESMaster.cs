using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NESEmulator.Core.CentralProcessingUnit;
using NESEmulator.Core.Memory;

namespace NESEmulator.Core
{
    /// <summary>
    /// This is the master controller of the all parts
    /// it will act like the NES Device At a complete unit 
    /// it will control when to call cpu cycle - ppu cycle and ...etc
    /// </summary>
    class NESMaster
    {

        public void initialize()
        {
            //constract system componants
            RAM ram = new RAM();
            IORegisters ioRegisters = new IORegisters();
            ExpansionROM eROM = new ExpansionROM();
            SaveRAM sRAM = new SaveRAM();
            ROM rom = new ROM();

            MemoryAccessController memoryAccessControler = new MemoryAccessController(ram,ioRegisters,eROM,sRAM,rom);

            CPU cpu = new CPU(memoryAccessControler);
        }
    }
}
