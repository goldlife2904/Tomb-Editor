﻿namespace TombEditor.Forms.TombEngine
{
    partial class FormSoundSource
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
            this.butOK = new DarkUI.Controls.DarkButton();
            this.butCancel = new DarkUI.Controls.DarkButton();
            this.optionPlaySoundFromWadGroupBox = new DarkUI.Controls.DarkGroupBox();
            this.butSearch = new DarkUI.Controls.DarkButton();
            this.tbSearch = new DarkUI.Controls.DarkTextBox();
            this.comboPlayMode = new DarkUI.Controls.DarkComboBox();
            this.darkLabel2 = new DarkUI.Controls.DarkLabel();
            this.butPlaySound = new DarkUI.Controls.DarkButton();
            this.lstSounds = new DarkUI.Controls.DarkListView();
            this.tbLuaName = new DarkUI.Controls.DarkTextBox();
            this.labelLuaName = new DarkUI.Controls.DarkLabel();
            this.optionPlaySoundFromWadGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // butOK
            // 
            this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butOK.Checked = false;
            this.butOK.Location = new System.Drawing.Point(306, 572);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(80, 23);
            this.butOK.TabIndex = 0;
            this.butOK.Text = "OK";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Checked = false;
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(392, 572);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(80, 23);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "Cancel";
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // optionPlaySoundFromWadGroupBox
            // 
            this.optionPlaySoundFromWadGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionPlaySoundFromWadGroupBox.Controls.Add(this.butSearch);
            this.optionPlaySoundFromWadGroupBox.Controls.Add(this.tbSearch);
            this.optionPlaySoundFromWadGroupBox.Controls.Add(this.comboPlayMode);
            this.optionPlaySoundFromWadGroupBox.Controls.Add(this.darkLabel2);
            this.optionPlaySoundFromWadGroupBox.Controls.Add(this.butPlaySound);
            this.optionPlaySoundFromWadGroupBox.Controls.Add(this.lstSounds);
            this.optionPlaySoundFromWadGroupBox.Location = new System.Drawing.Point(12, 12);
            this.optionPlaySoundFromWadGroupBox.Name = "optionPlaySoundFromWadGroupBox";
            this.optionPlaySoundFromWadGroupBox.Size = new System.Drawing.Size(460, 526);
            this.optionPlaySoundFromWadGroupBox.TabIndex = 66;
            this.optionPlaySoundFromWadGroupBox.TabStop = false;
            this.optionPlaySoundFromWadGroupBox.Text = "Sound to play";
            // 
            // butSearch
            // 
            this.butSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSearch.Checked = false;
            this.butSearch.Image = global::TombEditor.Properties.Resources.general_search_16;
            this.butSearch.Location = new System.Drawing.Point(428, 21);
            this.butSearch.Name = "butSearch";
            this.butSearch.Selectable = false;
            this.butSearch.Size = new System.Drawing.Size(24, 23);
            this.butSearch.TabIndex = 109;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSearch.Location = new System.Drawing.Point(8, 21);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(421, 23);
            this.tbSearch.TabIndex = 0;
            this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
            // 
            // comboPlayMode
            // 
            this.comboPlayMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboPlayMode.FormattingEnabled = true;
            this.comboPlayMode.Items.AddRange(new object[] {
            "Always",
            "Only when flipmaps are off",
            "Only when flipmaps are on",
            "Auto-decide based on room type"});
            this.comboPlayMode.Location = new System.Drawing.Point(90, 495);
            this.comboPlayMode.Name = "comboPlayMode";
            this.comboPlayMode.Size = new System.Drawing.Size(264, 23);
            this.comboPlayMode.TabIndex = 2;
            // 
            // darkLabel2
            // 
            this.darkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.darkLabel2.AutoSize = true;
            this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel2.Location = new System.Drawing.Point(9, 498);
            this.darkLabel2.Name = "darkLabel2";
            this.darkLabel2.Size = new System.Drawing.Size(62, 13);
            this.darkLabel2.TabIndex = 63;
            this.darkLabel2.Text = "Play mode:";
            // 
            // butPlaySound
            // 
            this.butPlaySound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butPlaySound.Checked = false;
            this.butPlaySound.Image = global::TombEditor.Properties.Resources.actions_play_16;
            this.butPlaySound.Location = new System.Drawing.Point(360, 495);
            this.butPlaySound.Name = "butPlaySound";
            this.butPlaySound.Size = new System.Drawing.Size(92, 23);
            this.butPlaySound.TabIndex = 3;
            this.butPlaySound.Text = "Play sound";
            this.butPlaySound.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butPlaySound.Click += new System.EventHandler(this.butPlay_Click);
            // 
            // lstSounds
            // 
            this.lstSounds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSounds.Location = new System.Drawing.Point(8, 50);
            this.lstSounds.Name = "lstSounds";
            this.lstSounds.Size = new System.Drawing.Size(444, 439);
            this.lstSounds.TabIndex = 1;
            this.lstSounds.Text = "darkListView1";
            this.lstSounds.Click += new System.EventHandler(this.lstSounds_Click);
            this.lstSounds.DoubleClick += new System.EventHandler(this.lstSounds_DoubleClick);
            // 
            // tbLuaName
            // 
            this.tbLuaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLuaName.Location = new System.Drawing.Point(102, 544);
            this.tbLuaName.Name = "tbLuaName";
            this.tbLuaName.Size = new System.Drawing.Size(370, 22);
            this.tbLuaName.TabIndex = 67;
            // 
            // labelLuaName
            // 
            this.labelLuaName.AutoSize = true;
            this.labelLuaName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.labelLuaName.Location = new System.Drawing.Point(21, 546);
            this.labelLuaName.Name = "labelLuaName";
            this.labelLuaName.Size = new System.Drawing.Size(75, 13);
            this.labelLuaName.TabIndex = 68;
            this.labelLuaName.Text = "Lua Name:";
            // 
            // FormSoundSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(484, 607);
            this.Controls.Add(this.tbLuaName);
            this.Controls.Add(this.labelLuaName);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.optionPlaySoundFromWadGroupBox);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 520);
            this.Name = "FormSoundSource";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sound source";
            this.optionPlaySoundFromWadGroupBox.ResumeLayout(false);
            this.optionPlaySoundFromWadGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DarkUI.Controls.DarkButton butOK;
        private DarkUI.Controls.DarkButton butCancel;
        private DarkUI.Controls.DarkGroupBox optionPlaySoundFromWadGroupBox;
        private DarkUI.Controls.DarkButton butPlaySound;
        private DarkUI.Controls.DarkListView lstSounds;
        private DarkUI.Controls.DarkComboBox comboPlayMode;
        private DarkUI.Controls.DarkLabel darkLabel2;
        private DarkUI.Controls.DarkButton butSearch;
        private DarkUI.Controls.DarkTextBox tbSearch;
        private DarkUI.Controls.DarkTextBox tbLuaName;
        private DarkUI.Controls.DarkLabel labelLuaName;
    }
}