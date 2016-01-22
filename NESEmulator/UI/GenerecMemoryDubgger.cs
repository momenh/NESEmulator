using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NESEmulator.UI
{
    public partial class GenerecMemoryDubgger : Form
    {
        public GenerecMemoryDubgger()
        {
            InitializeComponent();
        }

        public void initilaizeMemoryDebugger(byte[] data, int startIndex, int length)
        {
            memoryDebugger1.initilaizeMemoryDebugger(data, startIndex, length);
        }
    }
}
