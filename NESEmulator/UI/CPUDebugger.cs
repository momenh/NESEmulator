using NESEmulator.Core.CentralProcessingUnit;
using NESEmulator.Core.Memory;
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
    public partial class CPUDebugger : Form
    {
        byte[] memeoryBuffer;
        Core.CentralProcessingUnit.CPU cpu;
        int logLineIndex = 0;
        string[] logBuffer;

        public CPUDebugger()
        {
            InitializeComponent();
        }

        private void CPUDebugger_Load(object sender, EventArgs e)
        {
            //constract system componants
            RAM ram = new RAM();
            IORegisters ioRegisters = new IORegisters();
            ExpansionROM eROM = new ExpansionROM();
            SaveRAM sRAM = new SaveRAM();
            ROM rom = new ROM();

            MemoryAccessController memoryAccessControler = new MemoryAccessController(ram, ioRegisters, eROM, sRAM, rom);

            cpu = new CPU(memoryAccessControler);

            memeoryBuffer = memoryAccessControler.getInternalMemoryBuffer();

            //read rom data from file
            byte[] fileData = System.IO.File.ReadAllBytes("C:\\CPUTests\\nestest.nes");
            for (int i = 0; i < 16384; i++)
            {
                memeoryBuffer[i + 32768] = fileData[i + 16]; // bank one 0x8000
            }

            for (int i = 0; i < 16384; i++)
            {
                memeoryBuffer[i + 49152] = fileData[i + 16]; // bank tow 0xC000
            }

            //read log file
            logBuffer = System.IO.File.ReadAllLines("C:\\CPUTests\\nestest.log");
        }

        private void btnZeroPageMemory_Click(object sender, EventArgs e)
        {
            GenerecMemoryDubgger gmd = new GenerecMemoryDubgger();
            gmd.Show();
            gmd.initilaizeMemoryDebugger(memeoryBuffer, 0, 256);
        }

        private void btnStackPageMemory_Click(object sender, EventArgs e)
        {
            GenerecMemoryDubgger gmd = new GenerecMemoryDubgger();
            gmd.Show();
            gmd.initilaizeMemoryDebugger(memeoryBuffer, 256, 256);
        }

        private void btnRAMPageMemory_Click(object sender, EventArgs e)
        {
            GenerecMemoryDubgger gmd = new GenerecMemoryDubgger();
            gmd.Show();
            gmd.initilaizeMemoryDebugger(memeoryBuffer, 512, 1536);
        }

        private void btnFullPageMemory_Click(object sender, EventArgs e)
        {
            GenerecMemoryDubgger gmd = new GenerecMemoryDubgger();
            gmd.Show();
            gmd.initilaizeMemoryDebugger(memeoryBuffer, 0, memeoryBuffer.Length);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cpu.m_ProgramCounterRegiter = 49152; //start at 0xC000
            cpu.m_AccumulatorRegister = 0;
            cpu.m_IndexXRegister = 0;
            cpu.m_IndexYRegister = 0;
            cpu.m_CPUCyclesCounter = 0;
            cpu.m_ProcessorStatusRegister.StatusRegister = 36;
            cpu.m_StackPointerRegiter = 253;
            logLineIndex = 0;

            btnReload_Click(sender, e);
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            cpu.doCycle();

            //increment log
            logLineIndex++;
            
            btnReload_Click(sender,e);
        }

        private void btnUpdateInternal_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            txtPCDCM.Text = cpu.m_ProgramCounterRegiter.ToString();
            txtPCHEX.Text = cpu.m_ProgramCounterRegiter.ToString("X4");

            txtAcumDCM.Text = cpu.m_AccumulatorRegister.ToString();
            txtAcumHEX.Text = cpu.m_AccumulatorRegister.ToString("X2");

            txtXRegDCM.Text = cpu.m_IndexXRegister.ToString();
            txtXRegHEX.Text = cpu.m_IndexXRegister.ToString("X2");

            txtYRegDCM.Text = cpu.m_IndexYRegister.ToString();
            txtYRegHEX.Text = cpu.m_IndexYRegister.ToString("X2");

            txtStackDCM.Text = cpu.m_StackPointerRegiter.ToString();
            txtStackHEX.Text = cpu.m_StackPointerRegiter.ToString("X2");

            txtPStatusDCM.Text = cpu.m_ProcessorStatusRegister.StatusRegister.ToString();
            txtPStatusHEX.Text = cpu.m_ProcessorStatusRegister.StatusRegister.ToString("X2");


            txtNigative.Text = cpu.m_ProcessorStatusRegister.testNegativeFlag() ? "1" : "0";
            txtOverflow.Text = cpu.m_ProcessorStatusRegister.testOverflowFlag() ? "1" : "0";
            txtIgnore.Text = cpu.m_ProcessorStatusRegister.testIgnoredFlag() ? "1" : "0";
            txtBreak.Text = cpu.m_ProcessorStatusRegister.testBreakFlag() ? "1" : "0";
            txtDCM.Text = cpu.m_ProcessorStatusRegister.testDecimalFlag() ? "1" : "0";
            txtInterrubt.Text = cpu.m_ProcessorStatusRegister.testInterruptFlag() ? "1" : "0";
            txtZero.Text = cpu.m_ProcessorStatusRegister.testZeroFlag() ? "1" : "0";
            txtCarry.Text = cpu.m_ProcessorStatusRegister.testCarryFlag() ? "1" : "0";

            txtCPUCycles.Text = cpu.m_CPUCyclesCounter.ToString();

            //memory reading
            byte instruction = memeoryBuffer[cpu.m_ProgramCounterRegiter];
            byte operand1 = memeoryBuffer[cpu.m_ProgramCounterRegiter+1];
            byte operand2 = memeoryBuffer[cpu.m_ProgramCounterRegiter+2];

            txtInstructionDCM.Text = instruction.ToString();
            txtInstructionHEX.Text = instruction.ToString("X2");

            txtOperand1DCM.Text = operand1.ToString();
            txtOperand1HEX.Text = operand1.ToString("X2");
            txtOperand1Signed.Text = ((sbyte)operand1).ToString();

            txtOperand2DCM.Text = operand2.ToString();
            txtOperand2HEX.Text = operand2.ToString("X2");
            txtOperand2Signed.Text = ((sbyte)operand2).ToString();

            //load log line
            if (logBuffer != null && logLineIndex <= logBuffer.Length - 1)
            {
                txtLogBox.Text = logBuffer[logLineIndex];
                txtLogBoxIndex.Text = logLineIndex.ToString();

                //break down line
                string logPC = txtLogBox.Text.Substring(0, 4);
                string logInst = txtLogBox.Text.Substring(6, 2);
                string logOpe1 = txtLogBox.Text.Substring(9, 2);
                string logOpe2 = txtLogBox.Text.Substring(12, 2);
                string logA = txtLogBox.Text.Substring(50, 2);
                string logX = txtLogBox.Text.Substring(55, 2);
                string logY = txtLogBox.Text.Substring(60, 2);
                string logS = txtLogBox.Text.Substring(71, 2);
                string logP = txtLogBox.Text.Substring(65, 2);

                //if any is not correct falge the color
                bool logAgreeWithResult = true;
                if (!txtPCHEX.Text.Equals(logPC))
                    logAgreeWithResult = false;
                else if (!txtInstructionHEX.Text.Equals(logInst))
                    logAgreeWithResult = false;
                else if (!string.IsNullOrWhiteSpace(logOpe1) && !txtOperand1HEX.Text.Equals(logOpe1))
                    logAgreeWithResult = false;
                else if (!string.IsNullOrWhiteSpace(logOpe2) && !txtOperand2HEX.Text.Equals(logOpe2))
                    logAgreeWithResult = false;
                else if (!txtAcumHEX.Text.Equals(logA))
                    logAgreeWithResult = false;
                else if (!txtXRegHEX.Text.Equals(logX))
                    logAgreeWithResult = false;
                else if (!txtYRegHEX.Text.Equals(logY))
                    logAgreeWithResult = false;
                else if (!txtStackHEX.Text.Equals(logS))
                    logAgreeWithResult = false;
                else if (!txtPStatusHEX.Text.Equals(logP))
                    logAgreeWithResult = false;

                if (logAgreeWithResult)
                    lblLogStatusColor.BackColor = Color.Green;
                else
                    lblLogStatusColor.BackColor = Color.Red;
            }
            else
            {
                txtLogBox.Text = "No Log Loaded";
                txtLogBoxIndex.Text = logLineIndex.ToString();
                lblLogStatusColor.BackColor = Color.Yellow;
            }

            if (lblLogStatusColor.BackColor.Equals(Color.Red))
                MessageBox.Show("Error - Log not consistant");
        }

        private void btnRunToCondition_Click(object sender, EventArgs e)
        {
            int temp;

            if (!int.TryParse(txtLogLineBreak.Text, out temp))
            {
                temp = 0;
                txtLogLineBreak.Text = "0";
            }

            if (chbkLogError.Checked)
            {
                while (lblLogStatusColor.BackColor.Equals(Color.Green))
                {
                    btnStep_Click(sender, e);
                }
            }
            else
            {
                while (logLineIndex < temp)
                {
                    btnStep_Click(sender, e);
                }
            }
        }

        private void chbkLogError_CheckedChanged(object sender, EventArgs e)
        {
            txtLogLineBreak.Enabled = !chbkLogError.Checked;
        }
    }
}
