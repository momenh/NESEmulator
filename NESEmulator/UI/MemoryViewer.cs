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
    public partial class MemoryViewer : Form
    {
        public MemoryViewer()
        {
            InitializeComponent();
        }

        class MemoryInfo
        {
            [DisplayName("Location")]
            public String Location { get; set; }
            [DisplayName("Data")]
            public String Data { get; set; }

            public MemoryInfo(int Location, byte Data)
            {
                this.Location = Location.ToString("X2");
                this.Data = Data.ToString("X2");
            }
        }

        MemoryInfo[] data = new MemoryInfo[10];

        private void MemoryViewer_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                data[i] = new MemoryInfo(i, (byte)(10 - i));
            }

            dataGridView1.DataSource = data;
        }
    }
}
