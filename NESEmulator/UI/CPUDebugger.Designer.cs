namespace NESEmulator.UI
{
    partial class CPUDebugger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPC = new System.Windows.Forms.Label();
            this.txtPCDCM = new System.Windows.Forms.TextBox();
            this.txtXRegDCM = new System.Windows.Forms.TextBox();
            this.lvlXReg = new System.Windows.Forms.Label();
            this.txtYRegDCM = new System.Windows.Forms.TextBox();
            this.lblYReg = new System.Windows.Forms.Label();
            this.txtAcumDCM = new System.Windows.Forms.TextBox();
            this.lblAcum = new System.Windows.Forms.Label();
            this.txtStackDCM = new System.Windows.Forms.TextBox();
            this.lblStack = new System.Windows.Forms.Label();
            this.txtPStatusDCM = new System.Windows.Forms.TextBox();
            this.lblPStatus = new System.Windows.Forms.Label();
            this.txtNigative = new System.Windows.Forms.TextBox();
            this.txtOverflow = new System.Windows.Forms.TextBox();
            this.txtIgnore = new System.Windows.Forms.TextBox();
            this.txtBreak = new System.Windows.Forms.TextBox();
            this.txtDCM = new System.Windows.Forms.TextBox();
            this.txtInterrubt = new System.Windows.Forms.TextBox();
            this.txtZero = new System.Windows.Forms.TextBox();
            this.txtCarry = new System.Windows.Forms.TextBox();
            this.lblPDetails = new System.Windows.Forms.Label();
            this.lblNigative = new System.Windows.Forms.Label();
            this.lblOverflow = new System.Windows.Forms.Label();
            this.lblIgnore = new System.Windows.Forms.Label();
            this.lblBreak = new System.Windows.Forms.Label();
            this.lblDCM = new System.Windows.Forms.Label();
            this.lblintrurubt = new System.Windows.Forms.Label();
            this.lblZero = new System.Windows.Forms.Label();
            this.lblCarry = new System.Windows.Forms.Label();
            this.txtPStatusHEX = new System.Windows.Forms.TextBox();
            this.txtStackHEX = new System.Windows.Forms.TextBox();
            this.txtAcumHEX = new System.Windows.Forms.TextBox();
            this.txtYRegHEX = new System.Windows.Forms.TextBox();
            this.txtXRegHEX = new System.Windows.Forms.TextBox();
            this.txtPCHEX = new System.Windows.Forms.TextBox();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.txtInstructionHEX = new System.Windows.Forms.TextBox();
            this.txtInstructionDCM = new System.Windows.Forms.TextBox();
            this.txtOperand1HEX = new System.Windows.Forms.TextBox();
            this.txtOperand1DCM = new System.Windows.Forms.TextBox();
            this.lblOperand1 = new System.Windows.Forms.Label();
            this.txtOperand1Signed = new System.Windows.Forms.TextBox();
            this.lblSigned = new System.Windows.Forms.Label();
            this.txtOperand2Signed = new System.Windows.Forms.TextBox();
            this.txtOperand2HEX = new System.Windows.Forms.TextBox();
            this.txtOperand2DCM = new System.Windows.Forms.TextBox();
            this.lblOperand2 = new System.Windows.Forms.Label();
            this.btnZeroPageMemory = new System.Windows.Forms.Button();
            this.btnStackPageMemory = new System.Windows.Forms.Button();
            this.btnFullPageMemory = new System.Windows.Forms.Button();
            this.btnRAMPageMemory = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnUpdateInternal = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.txtCPUCycles = new System.Windows.Forms.TextBox();
            this.lblCPUCycles = new System.Windows.Forms.Label();
            this.txtLogBox = new System.Windows.Forms.TextBox();
            this.lblLogBox = new System.Windows.Forms.Label();
            this.lblLogStatusColor = new System.Windows.Forms.Label();
            this.lblLogBoxIndex = new System.Windows.Forms.Label();
            this.txtLogBoxIndex = new System.Windows.Forms.TextBox();
            this.btnRunTellError = new System.Windows.Forms.Button();
            this.txtLogLineBreak = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chbkLogError = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblPC
            // 
            this.lblPC.AutoSize = true;
            this.lblPC.Location = new System.Drawing.Point(12, 9);
            this.lblPC.Name = "lblPC";
            this.lblPC.Size = new System.Drawing.Size(20, 13);
            this.lblPC.TabIndex = 0;
            this.lblPC.Text = "PC";
            // 
            // txtPCDCM
            // 
            this.txtPCDCM.Location = new System.Drawing.Point(38, 6);
            this.txtPCDCM.Name = "txtPCDCM";
            this.txtPCDCM.Size = new System.Drawing.Size(66, 20);
            this.txtPCDCM.TabIndex = 1;
            // 
            // txtXRegDCM
            // 
            this.txtXRegDCM.Location = new System.Drawing.Point(38, 58);
            this.txtXRegDCM.Name = "txtXRegDCM";
            this.txtXRegDCM.Size = new System.Drawing.Size(66, 20);
            this.txtXRegDCM.TabIndex = 3;
            // 
            // lvlXReg
            // 
            this.lvlXReg.AutoSize = true;
            this.lvlXReg.Location = new System.Drawing.Point(12, 61);
            this.lvlXReg.Name = "lvlXReg";
            this.lvlXReg.Size = new System.Drawing.Size(13, 13);
            this.lvlXReg.TabIndex = 2;
            this.lvlXReg.Text = "X";
            // 
            // txtYRegDCM
            // 
            this.txtYRegDCM.Location = new System.Drawing.Point(38, 84);
            this.txtYRegDCM.Name = "txtYRegDCM";
            this.txtYRegDCM.Size = new System.Drawing.Size(66, 20);
            this.txtYRegDCM.TabIndex = 5;
            // 
            // lblYReg
            // 
            this.lblYReg.AutoSize = true;
            this.lblYReg.Location = new System.Drawing.Point(12, 87);
            this.lblYReg.Name = "lblYReg";
            this.lblYReg.Size = new System.Drawing.Size(13, 13);
            this.lblYReg.TabIndex = 4;
            this.lblYReg.Text = "Y";
            // 
            // txtAcumDCM
            // 
            this.txtAcumDCM.Location = new System.Drawing.Point(38, 32);
            this.txtAcumDCM.Name = "txtAcumDCM";
            this.txtAcumDCM.Size = new System.Drawing.Size(66, 20);
            this.txtAcumDCM.TabIndex = 7;
            // 
            // lblAcum
            // 
            this.lblAcum.AutoSize = true;
            this.lblAcum.Location = new System.Drawing.Point(12, 35);
            this.lblAcum.Name = "lblAcum";
            this.lblAcum.Size = new System.Drawing.Size(14, 13);
            this.lblAcum.TabIndex = 6;
            this.lblAcum.Text = "A";
            // 
            // txtStackDCM
            // 
            this.txtStackDCM.Location = new System.Drawing.Point(38, 110);
            this.txtStackDCM.Name = "txtStackDCM";
            this.txtStackDCM.Size = new System.Drawing.Size(66, 20);
            this.txtStackDCM.TabIndex = 9;
            // 
            // lblStack
            // 
            this.lblStack.AutoSize = true;
            this.lblStack.Location = new System.Drawing.Point(12, 113);
            this.lblStack.Name = "lblStack";
            this.lblStack.Size = new System.Drawing.Size(13, 13);
            this.lblStack.TabIndex = 8;
            this.lblStack.Text = "S";
            // 
            // txtPStatusDCM
            // 
            this.txtPStatusDCM.Location = new System.Drawing.Point(38, 136);
            this.txtPStatusDCM.Name = "txtPStatusDCM";
            this.txtPStatusDCM.Size = new System.Drawing.Size(66, 20);
            this.txtPStatusDCM.TabIndex = 11;
            // 
            // lblPStatus
            // 
            this.lblPStatus.AutoSize = true;
            this.lblPStatus.Location = new System.Drawing.Point(12, 139);
            this.lblPStatus.Name = "lblPStatus";
            this.lblPStatus.Size = new System.Drawing.Size(13, 13);
            this.lblPStatus.TabIndex = 10;
            this.lblPStatus.Text = "P";
            // 
            // txtNigative
            // 
            this.txtNigative.Location = new System.Drawing.Point(38, 175);
            this.txtNigative.Name = "txtNigative";
            this.txtNigative.Size = new System.Drawing.Size(12, 20);
            this.txtNigative.TabIndex = 12;
            this.txtNigative.Text = "0";
            // 
            // txtOverflow
            // 
            this.txtOverflow.Location = new System.Drawing.Point(56, 175);
            this.txtOverflow.Name = "txtOverflow";
            this.txtOverflow.Size = new System.Drawing.Size(12, 20);
            this.txtOverflow.TabIndex = 13;
            this.txtOverflow.Text = "0";
            // 
            // txtIgnore
            // 
            this.txtIgnore.Location = new System.Drawing.Point(74, 175);
            this.txtIgnore.Name = "txtIgnore";
            this.txtIgnore.Size = new System.Drawing.Size(12, 20);
            this.txtIgnore.TabIndex = 14;
            this.txtIgnore.Text = "0";
            // 
            // txtBreak
            // 
            this.txtBreak.Location = new System.Drawing.Point(92, 175);
            this.txtBreak.Name = "txtBreak";
            this.txtBreak.Size = new System.Drawing.Size(12, 20);
            this.txtBreak.TabIndex = 15;
            this.txtBreak.Text = "0";
            // 
            // txtDCM
            // 
            this.txtDCM.Location = new System.Drawing.Point(110, 175);
            this.txtDCM.Name = "txtDCM";
            this.txtDCM.Size = new System.Drawing.Size(12, 20);
            this.txtDCM.TabIndex = 16;
            this.txtDCM.Text = "0";
            // 
            // txtInterrubt
            // 
            this.txtInterrubt.Location = new System.Drawing.Point(128, 175);
            this.txtInterrubt.Name = "txtInterrubt";
            this.txtInterrubt.Size = new System.Drawing.Size(12, 20);
            this.txtInterrubt.TabIndex = 17;
            this.txtInterrubt.Text = "0";
            // 
            // txtZero
            // 
            this.txtZero.Location = new System.Drawing.Point(146, 175);
            this.txtZero.Name = "txtZero";
            this.txtZero.Size = new System.Drawing.Size(12, 20);
            this.txtZero.TabIndex = 18;
            this.txtZero.Text = "0";
            // 
            // txtCarry
            // 
            this.txtCarry.Location = new System.Drawing.Point(164, 175);
            this.txtCarry.Name = "txtCarry";
            this.txtCarry.Size = new System.Drawing.Size(12, 20);
            this.txtCarry.TabIndex = 19;
            this.txtCarry.Text = "0";
            // 
            // lblPDetails
            // 
            this.lblPDetails.AutoSize = true;
            this.lblPDetails.Location = new System.Drawing.Point(12, 159);
            this.lblPDetails.Name = "lblPDetails";
            this.lblPDetails.Size = new System.Drawing.Size(166, 13);
            this.lblPDetails.TabIndex = 20;
            this.lblPDetails.Text = "Processor Status Register Details";
            // 
            // lblNigative
            // 
            this.lblNigative.AutoSize = true;
            this.lblNigative.Location = new System.Drawing.Point(36, 198);
            this.lblNigative.Name = "lblNigative";
            this.lblNigative.Size = new System.Drawing.Size(14, 13);
            this.lblNigative.TabIndex = 21;
            this.lblNigative.Text = "N";
            // 
            // lblOverflow
            // 
            this.lblOverflow.AutoSize = true;
            this.lblOverflow.Location = new System.Drawing.Point(56, 198);
            this.lblOverflow.Name = "lblOverflow";
            this.lblOverflow.Size = new System.Drawing.Size(13, 13);
            this.lblOverflow.TabIndex = 22;
            this.lblOverflow.Text = "V";
            // 
            // lblIgnore
            // 
            this.lblIgnore.AutoSize = true;
            this.lblIgnore.Location = new System.Drawing.Point(73, 198);
            this.lblIgnore.Name = "lblIgnore";
            this.lblIgnore.Size = new System.Drawing.Size(11, 13);
            this.lblIgnore.TabIndex = 23;
            this.lblIgnore.Text = "-";
            // 
            // lblBreak
            // 
            this.lblBreak.AutoSize = true;
            this.lblBreak.Location = new System.Drawing.Point(90, 198);
            this.lblBreak.Name = "lblBreak";
            this.lblBreak.Size = new System.Drawing.Size(13, 13);
            this.lblBreak.TabIndex = 24;
            this.lblBreak.Text = "B";
            // 
            // lblDCM
            // 
            this.lblDCM.AutoSize = true;
            this.lblDCM.Location = new System.Drawing.Point(109, 198);
            this.lblDCM.Name = "lblDCM";
            this.lblDCM.Size = new System.Drawing.Size(14, 13);
            this.lblDCM.TabIndex = 25;
            this.lblDCM.Text = "D";
            // 
            // lblintrurubt
            // 
            this.lblintrurubt.AutoSize = true;
            this.lblintrurubt.Location = new System.Drawing.Point(127, 198);
            this.lblintrurubt.Name = "lblintrurubt";
            this.lblintrurubt.Size = new System.Drawing.Size(11, 13);
            this.lblintrurubt.TabIndex = 26;
            this.lblintrurubt.Text = "I";
            // 
            // lblZero
            // 
            this.lblZero.AutoSize = true;
            this.lblZero.Location = new System.Drawing.Point(145, 198);
            this.lblZero.Name = "lblZero";
            this.lblZero.Size = new System.Drawing.Size(13, 13);
            this.lblZero.TabIndex = 27;
            this.lblZero.Text = "Z";
            // 
            // lblCarry
            // 
            this.lblCarry.AutoSize = true;
            this.lblCarry.Location = new System.Drawing.Point(163, 198);
            this.lblCarry.Name = "lblCarry";
            this.lblCarry.Size = new System.Drawing.Size(14, 13);
            this.lblCarry.TabIndex = 28;
            this.lblCarry.Text = "C";
            // 
            // txtPStatusHEX
            // 
            this.txtPStatusHEX.Location = new System.Drawing.Point(110, 136);
            this.txtPStatusHEX.Name = "txtPStatusHEX";
            this.txtPStatusHEX.Size = new System.Drawing.Size(66, 20);
            this.txtPStatusHEX.TabIndex = 34;
            // 
            // txtStackHEX
            // 
            this.txtStackHEX.Location = new System.Drawing.Point(110, 110);
            this.txtStackHEX.Name = "txtStackHEX";
            this.txtStackHEX.Size = new System.Drawing.Size(66, 20);
            this.txtStackHEX.TabIndex = 33;
            // 
            // txtAcumHEX
            // 
            this.txtAcumHEX.Location = new System.Drawing.Point(110, 32);
            this.txtAcumHEX.Name = "txtAcumHEX";
            this.txtAcumHEX.Size = new System.Drawing.Size(66, 20);
            this.txtAcumHEX.TabIndex = 32;
            // 
            // txtYRegHEX
            // 
            this.txtYRegHEX.Location = new System.Drawing.Point(110, 84);
            this.txtYRegHEX.Name = "txtYRegHEX";
            this.txtYRegHEX.Size = new System.Drawing.Size(66, 20);
            this.txtYRegHEX.TabIndex = 31;
            // 
            // txtXRegHEX
            // 
            this.txtXRegHEX.Location = new System.Drawing.Point(110, 58);
            this.txtXRegHEX.Name = "txtXRegHEX";
            this.txtXRegHEX.Size = new System.Drawing.Size(66, 20);
            this.txtXRegHEX.TabIndex = 30;
            // 
            // txtPCHEX
            // 
            this.txtPCHEX.Location = new System.Drawing.Point(110, 6);
            this.txtPCHEX.Name = "txtPCHEX";
            this.txtPCHEX.Size = new System.Drawing.Size(66, 20);
            this.txtPCHEX.TabIndex = 29;
            // 
            // lblInstruction
            // 
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Location = new System.Drawing.Point(193, 9);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(59, 13);
            this.lblInstruction.TabIndex = 35;
            this.lblInstruction.Text = "Instruction";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(477, 301);
            this.shapeContainer1.TabIndex = 36;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 399;
            this.lineShape2.X2 = 399;
            this.lineShape2.Y1 = 3;
            this.lineShape2.Y2 = 30;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 185;
            this.lineShape1.X2 = 185;
            this.lineShape1.Y1 = 0;
            this.lineShape1.Y2 = 210;
            // 
            // txtInstructionHEX
            // 
            this.txtInstructionHEX.Location = new System.Drawing.Point(330, 6);
            this.txtInstructionHEX.Name = "txtInstructionHEX";
            this.txtInstructionHEX.ReadOnly = true;
            this.txtInstructionHEX.Size = new System.Drawing.Size(66, 20);
            this.txtInstructionHEX.TabIndex = 38;
            // 
            // txtInstructionDCM
            // 
            this.txtInstructionDCM.Location = new System.Drawing.Point(258, 6);
            this.txtInstructionDCM.Name = "txtInstructionDCM";
            this.txtInstructionDCM.ReadOnly = true;
            this.txtInstructionDCM.Size = new System.Drawing.Size(66, 20);
            this.txtInstructionDCM.TabIndex = 37;
            // 
            // txtOperand1HEX
            // 
            this.txtOperand1HEX.Location = new System.Drawing.Point(330, 32);
            this.txtOperand1HEX.Name = "txtOperand1HEX";
            this.txtOperand1HEX.ReadOnly = true;
            this.txtOperand1HEX.Size = new System.Drawing.Size(66, 20);
            this.txtOperand1HEX.TabIndex = 41;
            // 
            // txtOperand1DCM
            // 
            this.txtOperand1DCM.Location = new System.Drawing.Point(258, 32);
            this.txtOperand1DCM.Name = "txtOperand1DCM";
            this.txtOperand1DCM.ReadOnly = true;
            this.txtOperand1DCM.Size = new System.Drawing.Size(66, 20);
            this.txtOperand1DCM.TabIndex = 40;
            // 
            // lblOperand1
            // 
            this.lblOperand1.AutoSize = true;
            this.lblOperand1.Location = new System.Drawing.Point(193, 35);
            this.lblOperand1.Name = "lblOperand1";
            this.lblOperand1.Size = new System.Drawing.Size(58, 13);
            this.lblOperand1.TabIndex = 39;
            this.lblOperand1.Text = "Operand 1";
            // 
            // txtOperand1Signed
            // 
            this.txtOperand1Signed.Location = new System.Drawing.Point(402, 32);
            this.txtOperand1Signed.Name = "txtOperand1Signed";
            this.txtOperand1Signed.ReadOnly = true;
            this.txtOperand1Signed.Size = new System.Drawing.Size(66, 20);
            this.txtOperand1Signed.TabIndex = 42;
            // 
            // lblSigned
            // 
            this.lblSigned.AutoSize = true;
            this.lblSigned.Location = new System.Drawing.Point(402, 9);
            this.lblSigned.Name = "lblSigned";
            this.lblSigned.Size = new System.Drawing.Size(68, 13);
            this.lblSigned.TabIndex = 43;
            this.lblSigned.Text = "Signed Value";
            // 
            // txtOperand2Signed
            // 
            this.txtOperand2Signed.Location = new System.Drawing.Point(402, 58);
            this.txtOperand2Signed.Name = "txtOperand2Signed";
            this.txtOperand2Signed.ReadOnly = true;
            this.txtOperand2Signed.Size = new System.Drawing.Size(66, 20);
            this.txtOperand2Signed.TabIndex = 47;
            // 
            // txtOperand2HEX
            // 
            this.txtOperand2HEX.Location = new System.Drawing.Point(330, 58);
            this.txtOperand2HEX.Name = "txtOperand2HEX";
            this.txtOperand2HEX.ReadOnly = true;
            this.txtOperand2HEX.Size = new System.Drawing.Size(66, 20);
            this.txtOperand2HEX.TabIndex = 46;
            // 
            // txtOperand2DCM
            // 
            this.txtOperand2DCM.Location = new System.Drawing.Point(258, 58);
            this.txtOperand2DCM.Name = "txtOperand2DCM";
            this.txtOperand2DCM.ReadOnly = true;
            this.txtOperand2DCM.Size = new System.Drawing.Size(66, 20);
            this.txtOperand2DCM.TabIndex = 45;
            // 
            // lblOperand2
            // 
            this.lblOperand2.AutoSize = true;
            this.lblOperand2.Location = new System.Drawing.Point(193, 61);
            this.lblOperand2.Name = "lblOperand2";
            this.lblOperand2.Size = new System.Drawing.Size(58, 13);
            this.lblOperand2.TabIndex = 44;
            this.lblOperand2.Text = "Operand 2";
            // 
            // btnZeroPageMemory
            // 
            this.btnZeroPageMemory.Location = new System.Drawing.Point(196, 107);
            this.btnZeroPageMemory.Name = "btnZeroPageMemory";
            this.btnZeroPageMemory.Size = new System.Drawing.Size(272, 23);
            this.btnZeroPageMemory.TabIndex = 49;
            this.btnZeroPageMemory.Text = "Zero Page Memory Debugger - Window";
            this.btnZeroPageMemory.UseVisualStyleBackColor = true;
            this.btnZeroPageMemory.Click += new System.EventHandler(this.btnZeroPageMemory_Click);
            // 
            // btnStackPageMemory
            // 
            this.btnStackPageMemory.Location = new System.Drawing.Point(196, 133);
            this.btnStackPageMemory.Name = "btnStackPageMemory";
            this.btnStackPageMemory.Size = new System.Drawing.Size(272, 23);
            this.btnStackPageMemory.TabIndex = 50;
            this.btnStackPageMemory.Text = "Stack Page Memory Debugger - Window";
            this.btnStackPageMemory.UseVisualStyleBackColor = true;
            this.btnStackPageMemory.Click += new System.EventHandler(this.btnStackPageMemory_Click);
            // 
            // btnFullPageMemory
            // 
            this.btnFullPageMemory.Location = new System.Drawing.Point(196, 186);
            this.btnFullPageMemory.Name = "btnFullPageMemory";
            this.btnFullPageMemory.Size = new System.Drawing.Size(272, 23);
            this.btnFullPageMemory.TabIndex = 51;
            this.btnFullPageMemory.Text = "Full Memory Debugger - Window";
            this.btnFullPageMemory.UseVisualStyleBackColor = true;
            this.btnFullPageMemory.Click += new System.EventHandler(this.btnFullPageMemory_Click);
            // 
            // btnRAMPageMemory
            // 
            this.btnRAMPageMemory.Location = new System.Drawing.Point(196, 159);
            this.btnRAMPageMemory.Name = "btnRAMPageMemory";
            this.btnRAMPageMemory.Size = new System.Drawing.Size(272, 23);
            this.btnRAMPageMemory.TabIndex = 52;
            this.btnRAMPageMemory.Text = "RAM Memory Debugger - Window";
            this.btnRAMPageMemory.UseVisualStyleBackColor = true;
            this.btnRAMPageMemory.Click += new System.EventHandler(this.btnRAMPageMemory_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(5, 216);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(116, 23);
            this.btnReset.TabIndex = 53;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(122, 216);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(116, 23);
            this.btnStep.TabIndex = 54;
            this.btnStep.Text = "Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnUpdateInternal
            // 
            this.btnUpdateInternal.Location = new System.Drawing.Point(239, 216);
            this.btnUpdateInternal.Name = "btnUpdateInternal";
            this.btnUpdateInternal.Size = new System.Drawing.Size(116, 23);
            this.btnUpdateInternal.TabIndex = 55;
            this.btnUpdateInternal.Text = "Update View";
            this.btnUpdateInternal.UseVisualStyleBackColor = true;
            this.btnUpdateInternal.Click += new System.EventHandler(this.btnUpdateInternal_Click);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(356, 216);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(116, 23);
            this.btnReload.TabIndex = 56;
            this.btnReload.Text = "Reload View";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // txtCPUCycles
            // 
            this.txtCPUCycles.Location = new System.Drawing.Point(258, 84);
            this.txtCPUCycles.Name = "txtCPUCycles";
            this.txtCPUCycles.ReadOnly = true;
            this.txtCPUCycles.Size = new System.Drawing.Size(138, 20);
            this.txtCPUCycles.TabIndex = 58;
            // 
            // lblCPUCycles
            // 
            this.lblCPUCycles.AutoSize = true;
            this.lblCPUCycles.Location = new System.Drawing.Point(193, 87);
            this.lblCPUCycles.Name = "lblCPUCycles";
            this.lblCPUCycles.Size = new System.Drawing.Size(61, 13);
            this.lblCPUCycles.TabIndex = 57;
            this.lblCPUCycles.Text = "CPU Cycles";
            // 
            // txtLogBox
            // 
            this.txtLogBox.Location = new System.Drawing.Point(38, 245);
            this.txtLogBox.Name = "txtLogBox";
            this.txtLogBox.Size = new System.Drawing.Size(420, 20);
            this.txtLogBox.TabIndex = 59;
            // 
            // lblLogBox
            // 
            this.lblLogBox.AutoSize = true;
            this.lblLogBox.Location = new System.Drawing.Point(12, 248);
            this.lblLogBox.Name = "lblLogBox";
            this.lblLogBox.Size = new System.Drawing.Size(24, 13);
            this.lblLogBox.TabIndex = 60;
            this.lblLogBox.Text = "Log";
            // 
            // lblLogStatusColor
            // 
            this.lblLogStatusColor.BackColor = System.Drawing.Color.Green;
            this.lblLogStatusColor.Location = new System.Drawing.Point(461, 248);
            this.lblLogStatusColor.Name = "lblLogStatusColor";
            this.lblLogStatusColor.Size = new System.Drawing.Size(13, 13);
            this.lblLogStatusColor.TabIndex = 62;
            // 
            // lblLogBoxIndex
            // 
            this.lblLogBoxIndex.AutoSize = true;
            this.lblLogBoxIndex.Location = new System.Drawing.Point(12, 279);
            this.lblLogBoxIndex.Name = "lblLogBoxIndex";
            this.lblLogBoxIndex.Size = new System.Drawing.Size(55, 13);
            this.lblLogBoxIndex.TabIndex = 63;
            this.lblLogBoxIndex.Text = "Log Index";
            // 
            // txtLogBoxIndex
            // 
            this.txtLogBoxIndex.Location = new System.Drawing.Point(73, 276);
            this.txtLogBoxIndex.Name = "txtLogBoxIndex";
            this.txtLogBoxIndex.Size = new System.Drawing.Size(43, 20);
            this.txtLogBoxIndex.TabIndex = 64;
            // 
            // btnRunTellError
            // 
            this.btnRunTellError.Location = new System.Drawing.Point(122, 273);
            this.btnRunTellError.Name = "btnRunTellError";
            this.btnRunTellError.Size = new System.Drawing.Size(116, 23);
            this.btnRunTellError.TabIndex = 65;
            this.btnRunTellError.Text = "Run To";
            this.btnRunTellError.UseVisualStyleBackColor = true;
            this.btnRunTellError.Click += new System.EventHandler(this.btnRunToCondition_Click);
            // 
            // txtLogLineBreak
            // 
            this.txtLogLineBreak.Location = new System.Drawing.Point(305, 276);
            this.txtLogLineBreak.Name = "txtLogLineBreak";
            this.txtLogLineBreak.Size = new System.Drawing.Size(43, 20);
            this.txtLogLineBreak.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 279);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Log Line";
            // 
            // chbkLogError
            // 
            this.chbkLogError.AutoSize = true;
            this.chbkLogError.Location = new System.Drawing.Point(356, 279);
            this.chbkLogError.Name = "chbkLogError";
            this.chbkLogError.Size = new System.Drawing.Size(114, 17);
            this.chbkLogError.TabIndex = 68;
            this.chbkLogError.Text = "Log InConsistancy";
            this.chbkLogError.UseVisualStyleBackColor = true;
            this.chbkLogError.CheckedChanged += new System.EventHandler(this.chbkLogError_CheckedChanged);
            // 
            // CPUDebugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 301);
            this.Controls.Add(this.chbkLogError);
            this.Controls.Add(this.txtLogLineBreak);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRunTellError);
            this.Controls.Add(this.txtLogBoxIndex);
            this.Controls.Add(this.lblLogBoxIndex);
            this.Controls.Add(this.lblLogStatusColor);
            this.Controls.Add(this.lblLogBox);
            this.Controls.Add(this.txtLogBox);
            this.Controls.Add(this.txtCPUCycles);
            this.Controls.Add(this.lblCPUCycles);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnUpdateInternal);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnRAMPageMemory);
            this.Controls.Add(this.btnFullPageMemory);
            this.Controls.Add(this.btnStackPageMemory);
            this.Controls.Add(this.btnZeroPageMemory);
            this.Controls.Add(this.txtOperand2Signed);
            this.Controls.Add(this.txtOperand2HEX);
            this.Controls.Add(this.txtOperand2DCM);
            this.Controls.Add(this.lblOperand2);
            this.Controls.Add(this.lblSigned);
            this.Controls.Add(this.txtOperand1Signed);
            this.Controls.Add(this.txtOperand1HEX);
            this.Controls.Add(this.txtOperand1DCM);
            this.Controls.Add(this.lblOperand1);
            this.Controls.Add(this.txtInstructionHEX);
            this.Controls.Add(this.txtInstructionDCM);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.txtPStatusHEX);
            this.Controls.Add(this.txtStackHEX);
            this.Controls.Add(this.txtAcumHEX);
            this.Controls.Add(this.txtYRegHEX);
            this.Controls.Add(this.txtXRegHEX);
            this.Controls.Add(this.txtPCHEX);
            this.Controls.Add(this.lblCarry);
            this.Controls.Add(this.lblZero);
            this.Controls.Add(this.lblintrurubt);
            this.Controls.Add(this.lblDCM);
            this.Controls.Add(this.lblBreak);
            this.Controls.Add(this.lblIgnore);
            this.Controls.Add(this.lblOverflow);
            this.Controls.Add(this.lblNigative);
            this.Controls.Add(this.txtCarry);
            this.Controls.Add(this.txtZero);
            this.Controls.Add(this.txtInterrubt);
            this.Controls.Add(this.txtDCM);
            this.Controls.Add(this.txtBreak);
            this.Controls.Add(this.txtIgnore);
            this.Controls.Add(this.txtOverflow);
            this.Controls.Add(this.txtNigative);
            this.Controls.Add(this.txtPStatusDCM);
            this.Controls.Add(this.lblPStatus);
            this.Controls.Add(this.txtStackDCM);
            this.Controls.Add(this.lblStack);
            this.Controls.Add(this.txtAcumDCM);
            this.Controls.Add(this.lblAcum);
            this.Controls.Add(this.txtYRegDCM);
            this.Controls.Add(this.lblYReg);
            this.Controls.Add(this.txtXRegDCM);
            this.Controls.Add(this.lvlXReg);
            this.Controls.Add(this.txtPCDCM);
            this.Controls.Add(this.lblPC);
            this.Controls.Add(this.lblPDetails);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "CPUDebugger";
            this.Text = "CPUDebugger";
            this.Load += new System.EventHandler(this.CPUDebugger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPC;
        private System.Windows.Forms.TextBox txtPCDCM;
        private System.Windows.Forms.TextBox txtXRegDCM;
        private System.Windows.Forms.Label lvlXReg;
        private System.Windows.Forms.TextBox txtYRegDCM;
        private System.Windows.Forms.Label lblYReg;
        private System.Windows.Forms.TextBox txtAcumDCM;
        private System.Windows.Forms.Label lblAcum;
        private System.Windows.Forms.TextBox txtStackDCM;
        private System.Windows.Forms.Label lblStack;
        private System.Windows.Forms.TextBox txtPStatusDCM;
        private System.Windows.Forms.Label lblPStatus;
        private System.Windows.Forms.TextBox txtNigative;
        private System.Windows.Forms.TextBox txtOverflow;
        private System.Windows.Forms.TextBox txtIgnore;
        private System.Windows.Forms.TextBox txtBreak;
        private System.Windows.Forms.TextBox txtDCM;
        private System.Windows.Forms.TextBox txtInterrubt;
        private System.Windows.Forms.TextBox txtZero;
        private System.Windows.Forms.TextBox txtCarry;
        private System.Windows.Forms.Label lblPDetails;
        private System.Windows.Forms.Label lblNigative;
        private System.Windows.Forms.Label lblOverflow;
        private System.Windows.Forms.Label lblIgnore;
        private System.Windows.Forms.Label lblBreak;
        private System.Windows.Forms.Label lblDCM;
        private System.Windows.Forms.Label lblintrurubt;
        private System.Windows.Forms.Label lblZero;
        private System.Windows.Forms.Label lblCarry;
        private System.Windows.Forms.TextBox txtPStatusHEX;
        private System.Windows.Forms.TextBox txtStackHEX;
        private System.Windows.Forms.TextBox txtAcumHEX;
        private System.Windows.Forms.TextBox txtYRegHEX;
        private System.Windows.Forms.TextBox txtXRegHEX;
        private System.Windows.Forms.TextBox txtPCHEX;
        private System.Windows.Forms.Label lblInstruction;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.TextBox txtInstructionHEX;
        private System.Windows.Forms.TextBox txtInstructionDCM;
        private System.Windows.Forms.TextBox txtOperand1HEX;
        private System.Windows.Forms.TextBox txtOperand1DCM;
        private System.Windows.Forms.Label lblOperand1;
        private System.Windows.Forms.TextBox txtOperand1Signed;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private System.Windows.Forms.Label lblSigned;
        private System.Windows.Forms.TextBox txtOperand2Signed;
        private System.Windows.Forms.TextBox txtOperand2HEX;
        private System.Windows.Forms.TextBox txtOperand2DCM;
        private System.Windows.Forms.Label lblOperand2;
        private System.Windows.Forms.Button btnZeroPageMemory;
        private System.Windows.Forms.Button btnStackPageMemory;
        private System.Windows.Forms.Button btnFullPageMemory;
        private System.Windows.Forms.Button btnRAMPageMemory;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnUpdateInternal;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.TextBox txtCPUCycles;
        private System.Windows.Forms.Label lblCPUCycles;
        private System.Windows.Forms.TextBox txtLogBox;
        private System.Windows.Forms.Label lblLogBox;
        private System.Windows.Forms.Label lblLogStatusColor;
        private System.Windows.Forms.Label lblLogBoxIndex;
        private System.Windows.Forms.TextBox txtLogBoxIndex;
        private System.Windows.Forms.Button btnRunTellError;
        private System.Windows.Forms.TextBox txtLogLineBreak;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbkLogError;
    }
}