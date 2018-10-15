namespace Week_1
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.outputTxtBox = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.runMazeBtn = new System.Windows.Forms.Button();
            this.elementNumTxtBox = new System.Windows.Forms.TextBox();
            this.drawBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.wallOptionLblValue = new System.Windows.Forms.Label();
            this.drawAllWallsBtn = new System.Windows.Forms.Button();
            this.drawExperimentalBtn = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.msDelayLbl = new System.Windows.Forms.Label();
            this.drawMazeOutlinesBtn = new System.Windows.Forms.Button();
            this.btnMakeGraph = new System.Windows.Forms.Button();
            this.wallRadioLeft = new System.Windows.Forms.RadioButton();
            this.wallRadioNorth = new System.Windows.Forms.RadioButton();
            this.wallRadioRight = new System.Windows.Forms.RadioButton();
            this.wallRadioSouth = new System.Windows.Forms.RadioButton();
            this.chkBoxMzWalking = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 764);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Set maze";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // outputTxtBox
            // 
            this.outputTxtBox.Location = new System.Drawing.Point(9, 12);
            this.outputTxtBox.Name = "outputTxtBox";
            this.outputTxtBox.ReadOnly = true;
            this.outputTxtBox.Size = new System.Drawing.Size(228, 746);
            this.outputTxtBox.TabIndex = 1;
            this.outputTxtBox.Text = "";
            this.outputTxtBox.TextChanged += new System.EventHandler(this.outputTxtBox_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(366, 765);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(44, 22);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "x";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(416, 765);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(44, 22);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "y";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(285, 764);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Union";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // runMazeBtn
            // 
            this.runMazeBtn.Location = new System.Drawing.Point(9, 793);
            this.runMazeBtn.Name = "runMazeBtn";
            this.runMazeBtn.Size = new System.Drawing.Size(126, 23);
            this.runMazeBtn.TabIndex = 6;
            this.runMazeBtn.Text = "Run maze";
            this.runMazeBtn.UseVisualStyleBackColor = true;
            this.runMazeBtn.Click += new System.EventHandler(this.runMazeBtn_Click);
            // 
            // elementNumTxtBox
            // 
            this.elementNumTxtBox.BackColor = System.Drawing.SystemColors.Window;
            this.elementNumTxtBox.Location = new System.Drawing.Point(285, 796);
            this.elementNumTxtBox.Name = "elementNumTxtBox";
            this.elementNumTxtBox.Size = new System.Drawing.Size(175, 20);
            this.elementNumTxtBox.TabIndex = 7;
            this.elementNumTxtBox.Text = "Enter element #";
            // 
            // drawBtn
            // 
            this.drawBtn.Location = new System.Drawing.Point(9, 822);
            this.drawBtn.Name = "drawBtn";
            this.drawBtn.Size = new System.Drawing.Size(126, 23);
            this.drawBtn.TabIndex = 8;
            this.drawBtn.Text = "Draw element";
            this.drawBtn.UseVisualStyleBackColor = true;
            this.drawBtn.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(246, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(863, 745);
            this.panel1.TabIndex = 9;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.checkBox1.Location = new System.Drawing.Point(285, 826);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(98, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Enable all walls";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.wallRadioLeft);
            this.groupBox1.Controls.Add(this.wallRadioRight);
            this.groupBox1.Controls.Add(this.wallRadioNorth);
            this.groupBox1.Controls.Add(this.wallRadioSouth);
            this.groupBox1.Location = new System.Drawing.Point(476, 765);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 80);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wall options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(282, 846);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Option:";
            // 
            // wallOptionLblValue
            // 
            this.wallOptionLblValue.AutoSize = true;
            this.wallOptionLblValue.Location = new System.Drawing.Point(324, 846);
            this.wallOptionLblValue.Name = "wallOptionLblValue";
            this.wallOptionLblValue.Size = new System.Drawing.Size(57, 13);
            this.wallOptionLblValue.TabIndex = 15;
            this.wallOptionLblValue.Text = "wall option";
            // 
            // drawAllWallsBtn
            // 
            this.drawAllWallsBtn.Location = new System.Drawing.Point(9, 851);
            this.drawAllWallsBtn.Name = "drawAllWallsBtn";
            this.drawAllWallsBtn.Size = new System.Drawing.Size(126, 23);
            this.drawAllWallsBtn.TabIndex = 16;
            this.drawAllWallsBtn.Text = "Draw all walls";
            this.drawAllWallsBtn.UseVisualStyleBackColor = true;
            this.drawAllWallsBtn.Click += new System.EventHandler(this.drawAllWallsBtn_Click);
            // 
            // drawExperimentalBtn
            // 
            this.drawExperimentalBtn.Location = new System.Drawing.Point(141, 822);
            this.drawExperimentalBtn.Name = "drawExperimentalBtn";
            this.drawExperimentalBtn.Size = new System.Drawing.Size(126, 23);
            this.drawExperimentalBtn.TabIndex = 17;
            this.drawExperimentalBtn.Text = "Draw experimental";
            this.drawExperimentalBtn.UseVisualStyleBackColor = true;
            this.drawExperimentalBtn.Click += new System.EventHandler(this.drawExperimentalBtn_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(141, 763);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(126, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "Clear screen";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 50;
            this.trackBar1.Location = new System.Drawing.Point(476, 846);
            this.trackBar1.Maximum = 1000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.SmallChange = 10;
            this.trackBar1.TabIndex = 19;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 861);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "MS delay:";
            // 
            // msDelayLbl
            // 
            this.msDelayLbl.AutoSize = true;
            this.msDelayLbl.Location = new System.Drawing.Point(334, 861);
            this.msDelayLbl.Name = "msDelayLbl";
            this.msDelayLbl.Size = new System.Drawing.Size(20, 13);
            this.msDelayLbl.TabIndex = 21;
            this.msDelayLbl.Text = "ms";
            // 
            // drawMazeOutlinesBtn
            // 
            this.drawMazeOutlinesBtn.Location = new System.Drawing.Point(141, 793);
            this.drawMazeOutlinesBtn.Name = "drawMazeOutlinesBtn";
            this.drawMazeOutlinesBtn.Size = new System.Drawing.Size(126, 23);
            this.drawMazeOutlinesBtn.TabIndex = 22;
            this.drawMazeOutlinesBtn.Text = "Draw maze outlines";
            this.drawMazeOutlinesBtn.UseVisualStyleBackColor = true;
            this.drawMazeOutlinesBtn.Click += new System.EventHandler(this.drawMazeOutlinesBtn_Click);
            // 
            // btnMakeGraph
            // 
            this.btnMakeGraph.Location = new System.Drawing.Point(141, 851);
            this.btnMakeGraph.Name = "btnMakeGraph";
            this.btnMakeGraph.Size = new System.Drawing.Size(126, 23);
            this.btnMakeGraph.TabIndex = 23;
            this.btnMakeGraph.Text = "Make graph";
            this.btnMakeGraph.UseVisualStyleBackColor = true;
            this.btnMakeGraph.Click += new System.EventHandler(this.btnMakeGraph_Click);
            // 
            // wallRadioLeft
            // 
            this.wallRadioLeft.AutoSize = true;
            this.wallRadioLeft.Location = new System.Drawing.Point(7, 44);
            this.wallRadioLeft.Name = "wallRadioLeft";
            this.wallRadioLeft.Size = new System.Drawing.Size(43, 17);
            this.wallRadioLeft.TabIndex = 14;
            this.wallRadioLeft.Text = "Left";
            this.wallRadioLeft.UseVisualStyleBackColor = true;
            this.wallRadioLeft.CheckedChanged += new System.EventHandler(this.wallRadioLeft_CheckedChanged);
            // 
            // wallRadioNorth
            // 
            this.wallRadioNorth.AutoSize = true;
            this.wallRadioNorth.Location = new System.Drawing.Point(6, 21);
            this.wallRadioNorth.Name = "wallRadioNorth";
            this.wallRadioNorth.Size = new System.Drawing.Size(44, 17);
            this.wallRadioNorth.TabIndex = 11;
            this.wallRadioNorth.Text = "Top";
            this.wallRadioNorth.UseVisualStyleBackColor = true;
            this.wallRadioNorth.CheckedChanged += new System.EventHandler(this.wallRadioNorth_CheckedChanged);
            // 
            // wallRadioRight
            // 
            this.wallRadioRight.AutoSize = true;
            this.wallRadioRight.Location = new System.Drawing.Point(56, 44);
            this.wallRadioRight.Name = "wallRadioRight";
            this.wallRadioRight.Size = new System.Drawing.Size(50, 17);
            this.wallRadioRight.TabIndex = 13;
            this.wallRadioRight.Text = "Right";
            this.wallRadioRight.UseVisualStyleBackColor = true;
            this.wallRadioRight.CheckedChanged += new System.EventHandler(this.wallRadioRight_CheckedChanged);
            // 
            // wallRadioSouth
            // 
            this.wallRadioSouth.AutoSize = true;
            this.wallRadioSouth.Location = new System.Drawing.Point(56, 21);
            this.wallRadioSouth.Name = "wallRadioSouth";
            this.wallRadioSouth.Size = new System.Drawing.Size(58, 17);
            this.wallRadioSouth.TabIndex = 12;
            this.wallRadioSouth.Text = "Bottom";
            this.wallRadioSouth.UseVisualStyleBackColor = true;
            this.wallRadioSouth.CheckedChanged += new System.EventHandler(this.wallRadioSouth_CheckedChanged);
            this.wallRadioSouth.EnabledChanged += new System.EventHandler(this.wallRadioSouth_EnabledChanged);
            // 
            // chkBoxMzWalking
            // 
            this.chkBoxMzWalking.AutoSize = true;
            this.chkBoxMzWalking.Location = new System.Drawing.Point(604, 767);
            this.chkBoxMzWalking.Name = "chkBoxMzWalking";
            this.chkBoxMzWalking.Size = new System.Drawing.Size(126, 17);
            this.chkBoxMzWalking.TabIndex = 24;
            this.chkBoxMzWalking.Text = "Enable maze walking";
            this.chkBoxMzWalking.UseVisualStyleBackColor = true;
            this.chkBoxMzWalking.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 898);
            this.Controls.Add(this.chkBoxMzWalking);
            this.Controls.Add(this.btnMakeGraph);
            this.Controls.Add(this.drawMazeOutlinesBtn);
            this.Controls.Add(this.msDelayLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.drawExperimentalBtn);
            this.Controls.Add(this.drawAllWallsBtn);
            this.Controls.Add(this.wallOptionLblValue);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.drawBtn);
            this.Controls.Add(this.elementNumTxtBox);
            this.Controls.Add(this.runMazeBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.outputTxtBox);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.button4_Click);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox outputTxtBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button runMazeBtn;
        private System.Windows.Forms.TextBox elementNumTxtBox;
        private System.Windows.Forms.Button drawBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label wallOptionLblValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button drawAllWallsBtn;
        private System.Windows.Forms.Button drawExperimentalBtn;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label msDelayLbl;
        private System.Windows.Forms.Button drawMazeOutlinesBtn;
        private System.Windows.Forms.Button btnMakeGraph;
        private System.Windows.Forms.RadioButton wallRadioLeft;
        private System.Windows.Forms.RadioButton wallRadioRight;
        private System.Windows.Forms.RadioButton wallRadioNorth;
        private System.Windows.Forms.RadioButton wallRadioSouth;
        private System.Windows.Forms.CheckBox chkBoxMzWalking;
    }
}

