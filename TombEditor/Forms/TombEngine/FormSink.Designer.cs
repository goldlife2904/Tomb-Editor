﻿using DarkUI.Controls;

namespace TombEditor.Forms.TombEngine
{
    partial class FormSink
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
            this.label5 = new DarkUI.Controls.DarkLabel();
            this.butCancel = new DarkUI.Controls.DarkButton();
            this.butOK = new DarkUI.Controls.DarkButton();
            this.nudStrength = new DarkUI.Controls.DarkNumericUpDown();
            this.tbLuaName = new DarkUI.Controls.DarkTextBox();
            this.labelLuaName = new DarkUI.Controls.DarkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrength)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label5.Location = new System.Drawing.Point(6, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Current strength:";
            // 
            // butCancel
            // 
            this.butCancel.Checked = false;
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(161, 85);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(80, 23);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "Cancel";
            this.butCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Checked = false;
            this.butOK.Location = new System.Drawing.Point(75, 85);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(80, 23);
            this.butOK.TabIndex = 0;
            this.butOK.Text = "OK";
            this.butOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // nudStrength
            // 
            this.nudStrength.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.nudStrength.Location = new System.Drawing.Point(108, 13);
            this.nudStrength.LoopValues = false;
            this.nudStrength.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudStrength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStrength.Name = "nudStrength";
            this.nudStrength.Size = new System.Drawing.Size(67, 22);
            this.nudStrength.TabIndex = 23;
            this.nudStrength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tbLuaName
            // 
            this.tbLuaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLuaName.Location = new System.Drawing.Point(108, 41);
            this.tbLuaName.Name = "tbLuaName";
            this.tbLuaName.Size = new System.Drawing.Size(203, 22);
            this.tbLuaName.TabIndex = 24;
            // 
            // labelLuaName
            // 
            this.labelLuaName.AutoSize = true;
            this.labelLuaName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.labelLuaName.Location = new System.Drawing.Point(6, 43);
            this.labelLuaName.Name = "labelLuaName";
            this.labelLuaName.Size = new System.Drawing.Size(75, 13);
            this.labelLuaName.TabIndex = 25;
            this.labelLuaName.Text = "Lua Name:";
            // 
            // FormSink
            // 
            this.AcceptButton = this.butOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(323, 117);
            this.Controls.Add(this.tbLuaName);
            this.Controls.Add(this.labelLuaName);
            this.Controls.Add(this.nudStrength);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSink";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sink";
            this.Load += new System.EventHandler(this.FormSink_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudStrength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkButton butOK;
        private DarkButton butCancel;
        private DarkLabel label5;
        private DarkNumericUpDown nudStrength;
        private DarkTextBox tbLuaName;
        private DarkLabel labelLuaName;
    }
}