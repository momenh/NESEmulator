using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NESEmulator.UI
{
    public partial class MemoryDebugger : UserControl
    {
        private byte[] MemoryToDebug;
        private int StartIndex;
        private int Length;
        private string formaterOfNumbers; 
        private string formaterOfNumbersShort;
        private bool initilaizeView = false;

        public MemoryDebugger()
        {
            InitializeComponent();
        }

        public void initilaizeMemoryDebugger(byte[] MemoryToDebug, int StartIndex, int Length)
        {
            try
            {
                initilaizeView = true;

                this.MemoryToDebug = MemoryToDebug;
                this.StartIndex = StartIndex;
                this.Length = Length;
                formaterOfNumbers = "X2";
                formaterOfNumbersShort = "X4";

                //set by default to 16
                cmbViewMode.SelectedItem = cmbViewMode.Items[0];
                txtStartIndex.Text = StartIndex.ToString();
                txtLength.Text = Length.ToString();
                txtEndIndex.Text = (StartIndex + Length - 1).ToString();

                //build memoryView
                buildMemoryView();

                initilaizeView = false;
            }
            catch (Exception ex)
            {
                int i = 0;
            }
        }

        private void cmbViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(initilaizeView == false)
                buildMemoryView();
        }

        private void buildMemoryView()
        {
            //construct view based on current selected view mode
            MemoryDiscriptor[] dataToDisplay = constructDataDiscritors();
            crearMemoryGridView();
            memoryGridViewColumnsBuild();
            memoryGridViewDataBind(dataToDisplay);
        }

        private MemoryDiscriptor[] constructDataDiscritors()
        {
            MemoryDiscriptor[] result = new MemoryDiscriptor[Length];
            for (int i = 0; i < Length; i++)
            {
                result[i] = new MemoryDiscriptor(i + StartIndex, MemoryToDebug[i + StartIndex]);
            }

            return result;
        }

        private void crearMemoryGridView()
        {
            dgvMemoryGrid.Columns.Clear();
            dgvMemoryGrid.DataSource = null;
        }

        private void memoryGridViewColumnsBuild()
        {
            //create default cell template
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            
            //create address base column
            DataGridViewTextBoxColumn addressColumn = new DataGridViewTextBoxColumn();
            addressColumn.CellTemplate = cell;
            addressColumn.Name = "AddressBase";
            addressColumn.HeaderText = "Base";
            addressColumn.ReadOnly = true;
            addressColumn.DefaultCellStyle.BackColor = Color.Gray;

            //add base column 
            dgvMemoryGrid.Columns.Add(addressColumn);
            //dgvMemoryGrid.Columns.Add("AddressBase","Base");

            //base on view mode create the other columns data
            int viewMode = cmbViewMode.SelectedIndex == 0 ? 16 : 10;
            for (int i = 0; i < viewMode; i++)
            {
                //create address base column
                DataGridViewTextBoxColumn dataColumn = new DataGridViewTextBoxColumn();
                dataColumn.CellTemplate = cell;
                dataColumn.Name = i.ToString(formaterOfNumbers);
                dataColumn.HeaderText = i.ToString(formaterOfNumbers);

                //add column
                dgvMemoryGrid.Columns.Add(dataColumn);
                //dgvMemoryGrid.Columns.Add("AddressSubBase" + i.ToString(formaterOfNumbers), i.ToString(formaterOfNumbers));
            }
        }

        private void memoryGridViewDataBind(MemoryDiscriptor[] dataToBind)
        {
            int viewMode = dgvMemoryGrid.Columns.Count - 1;
            int dataRowsCount = dataToBind.Length / viewMode;
            if (dataToBind.Length % viewMode != 0)
                dataRowsCount++;

            //loop over row by row
            for (int i = 0; i < dataRowsCount; i++)
            {
                //construct data
                string[] data = new string[viewMode+1];
                
                //1 - add the address start index
                data[0] = dataToBind[i * viewMode].LocationIndex.ToString(formaterOfNumbersShort);
                
                //2 - loop to add data
                for (int j = 1; j < data.Length; j++)
                {
                    int cellIndexToExtractData = i * viewMode + (j - 1);

                    if (cellIndexToExtractData < dataToBind.Length)
                        data[j] = dataToBind[cellIndexToExtractData].DataValue.ToString(formaterOfNumbers);
                    else
                        data[j] = "  "; //add empty data to mark that no data was found for that cell [normaly will be at the last line]
                }

                //3 - build row of data
                dgvMemoryGrid.Rows.Add(data);
            }
        }

        private class MemoryDiscriptor
        {
            public int LocationIndex {get;set;}
            public byte DataValue {get;set;}

            public MemoryDiscriptor(int LocationIndex,byte DataValue)
            { this.LocationIndex = LocationIndex; this.DataValue = DataValue; }
        }
    }
}
