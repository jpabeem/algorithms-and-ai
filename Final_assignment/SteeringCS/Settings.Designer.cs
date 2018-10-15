namespace SteeringCS
{
    partial class Settings
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
            this.trackBarAlignment = new System.Windows.Forms.TrackBar();
            this.trackBarCohesion = new System.Windows.Forms.TrackBar();
            this.trackBarSeperation = new System.Windows.Forms.TrackBar();
            this.lblAlignment = new System.Windows.Forms.Label();
            this.txtBoxAlignment = new System.Windows.Forms.TextBox();
            this.txtBoxSeperation = new System.Windows.Forms.TextBox();
            this.txtBoxCohesion = new System.Windows.Forms.TextBox();
            this.lblSeperation = new System.Windows.Forms.Label();
            this.lblCohesion = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.txtBoxDistance = new System.Windows.Forms.TextBox();
            this.trackBarDistance = new System.Windows.Forms.TrackBar();
            this.chkBoxSprites = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAlignment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCohesion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSeperation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarAlignment
            // 
            this.trackBarAlignment.LargeChange = 1;
            this.trackBarAlignment.Location = new System.Drawing.Point(148, 19);
            this.trackBarAlignment.Maximum = 100;
            this.trackBarAlignment.Name = "trackBarAlignment";
            this.trackBarAlignment.Size = new System.Drawing.Size(104, 45);
            this.trackBarAlignment.TabIndex = 0;
            this.trackBarAlignment.Value = 1;
            this.trackBarAlignment.ValueChanged += new System.EventHandler(this.trackBarAlignment_ValueChanged);
            // 
            // trackBarCohesion
            // 
            this.trackBarCohesion.LargeChange = 1;
            this.trackBarCohesion.Location = new System.Drawing.Point(148, 121);
            this.trackBarCohesion.Maximum = 100;
            this.trackBarCohesion.Name = "trackBarCohesion";
            this.trackBarCohesion.Size = new System.Drawing.Size(104, 45);
            this.trackBarCohesion.TabIndex = 1;
            this.trackBarCohesion.Value = 1;
            this.trackBarCohesion.ValueChanged += new System.EventHandler(this.trackBarCohesion_ValueChanged);
            // 
            // trackBarSeperation
            // 
            this.trackBarSeperation.LargeChange = 1;
            this.trackBarSeperation.Location = new System.Drawing.Point(148, 70);
            this.trackBarSeperation.Maximum = 100;
            this.trackBarSeperation.Name = "trackBarSeperation";
            this.trackBarSeperation.Size = new System.Drawing.Size(104, 45);
            this.trackBarSeperation.TabIndex = 2;
            this.trackBarSeperation.Value = 1;
            this.trackBarSeperation.ValueChanged += new System.EventHandler(this.trackBarSeperation_ValueChanged);
            // 
            // lblAlignment
            // 
            this.lblAlignment.AutoSize = true;
            this.lblAlignment.Location = new System.Drawing.Point(86, 19);
            this.lblAlignment.Name = "lblAlignment";
            this.lblAlignment.Size = new System.Drawing.Size(56, 13);
            this.lblAlignment.TabIndex = 3;
            this.lblAlignment.Text = "Alignment:";
            // 
            // txtBoxAlignment
            // 
            this.txtBoxAlignment.Location = new System.Drawing.Point(258, 19);
            this.txtBoxAlignment.Name = "txtBoxAlignment";
            this.txtBoxAlignment.ReadOnly = true;
            this.txtBoxAlignment.Size = new System.Drawing.Size(53, 20);
            this.txtBoxAlignment.TabIndex = 4;
            this.txtBoxAlignment.Text = "<value>";
            this.txtBoxAlignment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxSeperation
            // 
            this.txtBoxSeperation.Location = new System.Drawing.Point(258, 70);
            this.txtBoxSeperation.Name = "txtBoxSeperation";
            this.txtBoxSeperation.ReadOnly = true;
            this.txtBoxSeperation.Size = new System.Drawing.Size(53, 20);
            this.txtBoxSeperation.TabIndex = 5;
            this.txtBoxSeperation.Text = "<value>";
            this.txtBoxSeperation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxCohesion
            // 
            this.txtBoxCohesion.Location = new System.Drawing.Point(258, 121);
            this.txtBoxCohesion.Name = "txtBoxCohesion";
            this.txtBoxCohesion.ReadOnly = true;
            this.txtBoxCohesion.Size = new System.Drawing.Size(53, 20);
            this.txtBoxCohesion.TabIndex = 6;
            this.txtBoxCohesion.Text = "<value>";
            this.txtBoxCohesion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSeperation
            // 
            this.lblSeperation.AutoSize = true;
            this.lblSeperation.Location = new System.Drawing.Point(81, 70);
            this.lblSeperation.Name = "lblSeperation";
            this.lblSeperation.Size = new System.Drawing.Size(61, 13);
            this.lblSeperation.TabIndex = 7;
            this.lblSeperation.Text = "Seperation:";
            // 
            // lblCohesion
            // 
            this.lblCohesion.AutoSize = true;
            this.lblCohesion.Location = new System.Drawing.Point(88, 124);
            this.lblCohesion.Name = "lblCohesion";
            this.lblCohesion.Size = new System.Drawing.Size(54, 13);
            this.lblCohesion.TabIndex = 8;
            this.lblCohesion.Text = "Cohesion:";
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(90, 169);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(52, 13);
            this.lblDistance.TabIndex = 11;
            this.lblDistance.Text = "Distance:";
            // 
            // txtBoxDistance
            // 
            this.txtBoxDistance.Location = new System.Drawing.Point(258, 166);
            this.txtBoxDistance.Name = "txtBoxDistance";
            this.txtBoxDistance.ReadOnly = true;
            this.txtBoxDistance.Size = new System.Drawing.Size(53, 20);
            this.txtBoxDistance.TabIndex = 10;
            this.txtBoxDistance.Text = "<value>";
            this.txtBoxDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trackBarDistance
            // 
            this.trackBarDistance.LargeChange = 100;
            this.trackBarDistance.Location = new System.Drawing.Point(148, 166);
            this.trackBarDistance.Maximum = 10000;
            this.trackBarDistance.Name = "trackBarDistance";
            this.trackBarDistance.Size = new System.Drawing.Size(104, 45);
            this.trackBarDistance.SmallChange = 10;
            this.trackBarDistance.TabIndex = 9;
            this.trackBarDistance.Value = 500;
            this.trackBarDistance.ValueChanged += new System.EventHandler(this.trackBarDistance_ValueChanged);
            // 
            // chkBoxSprites
            // 
            this.chkBoxSprites.AutoSize = true;
            this.chkBoxSprites.Location = new System.Drawing.Point(89, 194);
            this.chkBoxSprites.Name = "chkBoxSprites";
            this.chkBoxSprites.Size = new System.Drawing.Size(92, 17);
            this.chkBoxSprites.TabIndex = 12;
            this.chkBoxSprites.Text = "Enable sprites";
            this.chkBoxSprites.UseVisualStyleBackColor = true;
            this.chkBoxSprites.Click += new System.EventHandler(this.chkBoxSprites_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 231);
            this.Controls.Add(this.chkBoxSprites);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.txtBoxDistance);
            this.Controls.Add(this.trackBarDistance);
            this.Controls.Add(this.lblCohesion);
            this.Controls.Add(this.lblSeperation);
            this.Controls.Add(this.txtBoxCohesion);
            this.Controls.Add(this.txtBoxSeperation);
            this.Controls.Add(this.txtBoxAlignment);
            this.Controls.Add(this.lblAlignment);
            this.Controls.Add(this.trackBarSeperation);
            this.Controls.Add(this.trackBarCohesion);
            this.Controls.Add(this.trackBarAlignment);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAlignment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCohesion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSeperation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarAlignment;
        private System.Windows.Forms.TrackBar trackBarCohesion;
        private System.Windows.Forms.TrackBar trackBarSeperation;
        private System.Windows.Forms.Label lblAlignment;
        private System.Windows.Forms.TextBox txtBoxAlignment;
        private System.Windows.Forms.TextBox txtBoxSeperation;
        private System.Windows.Forms.TextBox txtBoxCohesion;
        private System.Windows.Forms.Label lblSeperation;
        private System.Windows.Forms.Label lblCohesion;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.TextBox txtBoxDistance;
        private System.Windows.Forms.TrackBar trackBarDistance;
        private System.Windows.Forms.CheckBox chkBoxSprites;
    }
}