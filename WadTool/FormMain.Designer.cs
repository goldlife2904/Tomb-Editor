﻿namespace WadTool
{
    partial class FormMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.butTest = new DarkUI.Controls.DarkButton();
            this.darkStatusStrip1 = new DarkUI.Controls.DarkStatusStrip();
            this.darkMenuStrip1 = new DarkUI.Controls.DarkMenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDestinationWad2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSourceWADToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveWad2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWad2AsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertWADToWad2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStrip1 = new DarkUI.Controls.DarkToolStrip();
            this.butOpenDestWad2 = new System.Windows.Forms.ToolStripButton();
            this.butOpenSourceWad = new System.Windows.Forms.ToolStripButton();
            this.butSave = new System.Windows.Forms.ToolStripButton();
            this.butSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.darkLabel1 = new DarkUI.Controls.DarkLabel();
            this.darkLabel2 = new DarkUI.Controls.DarkLabel();
            this.treeDestWad = new DarkUI.Controls.DarkTreeView();
            this.treeSourceWad = new DarkUI.Controls.DarkTreeView();
            this.panel3D = new WadTool.Controls.PanelRendering();
            this.openFileDialogWad = new System.Windows.Forms.OpenFileDialog();
            this.butAddObject = new DarkUI.Controls.DarkButton();
            this.butAddObjectToDifferentSlot = new DarkUI.Controls.DarkButton();
            this.butDeleteObject = new DarkUI.Controls.DarkButton();
            this.saveFileDialogWad2 = new System.Windows.Forms.SaveFileDialog();
            this.darkMenuStrip1.SuspendLayout();
            this.darkToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butTest
            // 
            this.butTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butTest.Location = new System.Drawing.Point(719, 625);
            this.butTest.Name = "butTest";
            this.butTest.Padding = new System.Windows.Forms.Padding(5);
            this.butTest.Size = new System.Drawing.Size(75, 23);
            this.butTest.TabIndex = 0;
            this.butTest.Text = "Test";
            this.butTest.Click += new System.EventHandler(this.butTest_Click);
            // 
            // darkStatusStrip1
            // 
            this.darkStatusStrip1.AutoSize = false;
            this.darkStatusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkStatusStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkStatusStrip1.Location = new System.Drawing.Point(0, 728);
            this.darkStatusStrip1.Name = "darkStatusStrip1";
            this.darkStatusStrip1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 3);
            this.darkStatusStrip1.Size = new System.Drawing.Size(1106, 24);
            this.darkStatusStrip1.SizingGrip = false;
            this.darkStatusStrip1.TabIndex = 1;
            this.darkStatusStrip1.Text = "darkStatusStrip1";
            // 
            // darkMenuStrip1
            // 
            this.darkMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkMenuStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.darkMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.darkMenuStrip1.Name = "darkMenuStrip1";
            this.darkMenuStrip1.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.darkMenuStrip1.Size = new System.Drawing.Size(1106, 24);
            this.darkMenuStrip1.TabIndex = 2;
            this.darkMenuStrip1.Text = "darkMenuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDestinationWad2ToolStripMenuItem,
            this.openSourceWADToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveWad2ToolStripMenuItem,
            this.saveWad2AsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openDestinationWad2ToolStripMenuItem
            // 
            this.openDestinationWad2ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.openDestinationWad2ToolStripMenuItem.Image = global::WadTool.Properties.Resources.opened_folder_16;
            this.openDestinationWad2ToolStripMenuItem.Name = "openDestinationWad2ToolStripMenuItem";
            this.openDestinationWad2ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openDestinationWad2ToolStripMenuItem.Text = "Open destination Wad2";
            this.openDestinationWad2ToolStripMenuItem.Click += new System.EventHandler(this.openDestinationWad2ToolStripMenuItem_Click);
            // 
            // openSourceWADToolStripMenuItem
            // 
            this.openSourceWADToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.openSourceWADToolStripMenuItem.Image = global::WadTool.Properties.Resources.import_16;
            this.openSourceWADToolStripMenuItem.Name = "openSourceWADToolStripMenuItem";
            this.openSourceWADToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openSourceWADToolStripMenuItem.Text = "Open source WAD/Wad2";
            this.openSourceWADToolStripMenuItem.Click += new System.EventHandler(this.openSourceWADToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripMenuItem1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(203, 6);
            // 
            // saveWad2ToolStripMenuItem
            // 
            this.saveWad2ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.saveWad2ToolStripMenuItem.Image = global::WadTool.Properties.Resources.save_16;
            this.saveWad2ToolStripMenuItem.Name = "saveWad2ToolStripMenuItem";
            this.saveWad2ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.saveWad2ToolStripMenuItem.Text = "Save Wad2";
            // 
            // saveWad2AsToolStripMenuItem
            // 
            this.saveWad2AsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.saveWad2AsToolStripMenuItem.Image = global::WadTool.Properties.Resources.save_as_16;
            this.saveWad2AsToolStripMenuItem.Name = "saveWad2AsToolStripMenuItem";
            this.saveWad2AsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.saveWad2AsToolStripMenuItem.Text = "Save Wad2 as...";
            this.saveWad2AsToolStripMenuItem.Click += new System.EventHandler(this.saveWad2AsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripMenuItem2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(203, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.exitToolStripMenuItem.Image = global::WadTool.Properties.Resources.door_opened_16;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertWADToWad2ToolStripMenuItem,
            this.soundManagerToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // convertWADToWad2ToolStripMenuItem
            // 
            this.convertWADToWad2ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.convertWADToWad2ToolStripMenuItem.Image = global::WadTool.Properties.Resources.save_as_16;
            this.convertWADToWad2ToolStripMenuItem.Name = "convertWADToWad2ToolStripMenuItem";
            this.convertWADToWad2ToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.convertWADToWad2ToolStripMenuItem.Text = "Convert source WAD to Wad2";
            // 
            // soundManagerToolStripMenuItem
            // 
            this.soundManagerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.soundManagerToolStripMenuItem.Image = global::WadTool.Properties.Resources.volume_up_16;
            this.soundManagerToolStripMenuItem.Name = "soundManagerToolStripMenuItem";
            this.soundManagerToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.soundManagerToolStripMenuItem.Text = "Sound manager";
            // 
            // darkToolStrip1
            // 
            this.darkToolStrip1.AutoSize = false;
            this.darkToolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkToolStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.butOpenDestWad2,
            this.butOpenSourceWad,
            this.butSave,
            this.butSaveAs,
            this.toolStripSeparator1,
            this.toolStripButton4});
            this.darkToolStrip1.Location = new System.Drawing.Point(0, 24);
            this.darkToolStrip1.Name = "darkToolStrip1";
            this.darkToolStrip1.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.darkToolStrip1.Size = new System.Drawing.Size(1106, 28);
            this.darkToolStrip1.TabIndex = 3;
            this.darkToolStrip1.Text = "darkToolStrip1";
            // 
            // butOpenDestWad2
            // 
            this.butOpenDestWad2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.butOpenDestWad2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.butOpenDestWad2.Image = global::WadTool.Properties.Resources.opened_folder_16;
            this.butOpenDestWad2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butOpenDestWad2.Name = "butOpenDestWad2";
            this.butOpenDestWad2.Size = new System.Drawing.Size(23, 25);
            this.butOpenDestWad2.Text = "toolStripButton1";
            this.butOpenDestWad2.Click += new System.EventHandler(this.butOpenDestWad2_Click);
            // 
            // butOpenSourceWad
            // 
            this.butOpenSourceWad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.butOpenSourceWad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.butOpenSourceWad.Image = global::WadTool.Properties.Resources.import_16;
            this.butOpenSourceWad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butOpenSourceWad.Name = "butOpenSourceWad";
            this.butOpenSourceWad.Size = new System.Drawing.Size(23, 25);
            this.butOpenSourceWad.Text = "toolStripButton2";
            this.butOpenSourceWad.Click += new System.EventHandler(this.butOpenSourceWad_Click);
            // 
            // butSave
            // 
            this.butSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.butSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.butSave.Image = global::WadTool.Properties.Resources.save_16;
            this.butSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(23, 25);
            this.butSave.Text = "toolStripButton3";
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // butSaveAs
            // 
            this.butSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.butSaveAs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.butSaveAs.Image = global::WadTool.Properties.Resources.save_as_16;
            this.butSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.butSaveAs.Name = "butSaveAs";
            this.butSaveAs.Size = new System.Drawing.Size(23, 25);
            this.butSaveAs.Text = "toolStripButton5";
            this.butSaveAs.Click += new System.EventHandler(this.butSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripButton4.Image = global::WadTool.Properties.Resources.volume_up_16;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 25);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // darkLabel1
            // 
            this.darkLabel1.AutoSize = true;
            this.darkLabel1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Location = new System.Drawing.Point(12, 59);
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Size = new System.Drawing.Size(79, 13);
            this.darkLabel1.TabIndex = 5;
            this.darkLabel1.Text = "Current Wad2";
            // 
            // darkLabel2
            // 
            this.darkLabel2.AutoSize = true;
            this.darkLabel2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel2.Location = new System.Drawing.Point(813, 59);
            this.darkLabel2.Name = "darkLabel2";
            this.darkLabel2.Size = new System.Drawing.Size(104, 13);
            this.darkLabel2.TabIndex = 6;
            this.darkLabel2.Text = "Source Wad/Wad2";
            // 
            // treeDestWad
            // 
            this.treeDestWad.Location = new System.Drawing.Point(15, 89);
            this.treeDestWad.MaxDragChange = 20;
            this.treeDestWad.Name = "treeDestWad";
            this.treeDestWad.Size = new System.Drawing.Size(279, 485);
            this.treeDestWad.TabIndex = 7;
            this.treeDestWad.Text = "darkTreeView1";
            this.treeDestWad.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeDestWad_MouseClick);
            // 
            // treeSourceWad
            // 
            this.treeSourceWad.Location = new System.Drawing.Point(816, 89);
            this.treeSourceWad.MaxDragChange = 20;
            this.treeSourceWad.Name = "treeSourceWad";
            this.treeSourceWad.Size = new System.Drawing.Size(279, 485);
            this.treeSourceWad.TabIndex = 8;
            this.treeSourceWad.Text = "darkTreeView1";
            this.treeSourceWad.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeSourceWad_MouseClick);
            // 
            // panel3D
            // 
            this.panel3D.Camera = null;
            this.panel3D.CurrentObject = null;
            this.panel3D.CurrentWad = null;
            this.panel3D.Location = new System.Drawing.Point(301, 89);
            this.panel3D.Name = "panel3D";
            this.panel3D.Size = new System.Drawing.Size(509, 514);
            this.panel3D.TabIndex = 9;
            // 
            // butAddObject
            // 
            this.butAddObject.Image = global::WadTool.Properties.Resources.angle_left_16;
            this.butAddObject.Location = new System.Drawing.Point(816, 580);
            this.butAddObject.Name = "butAddObject";
            this.butAddObject.Padding = new System.Windows.Forms.Padding(5);
            this.butAddObject.Size = new System.Drawing.Size(101, 23);
            this.butAddObject.TabIndex = 10;
            this.butAddObject.Text = "Add object";
            this.butAddObject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butAddObject.Click += new System.EventHandler(this.butAddObject_Click);
            // 
            // butAddObjectToDifferentSlot
            // 
            this.butAddObjectToDifferentSlot.Image = global::WadTool.Properties.Resources.angle_left_16;
            this.butAddObjectToDifferentSlot.Location = new System.Drawing.Point(923, 580);
            this.butAddObjectToDifferentSlot.Name = "butAddObjectToDifferentSlot";
            this.butAddObjectToDifferentSlot.Padding = new System.Windows.Forms.Padding(5);
            this.butAddObjectToDifferentSlot.Size = new System.Drawing.Size(172, 23);
            this.butAddObjectToDifferentSlot.TabIndex = 11;
            this.butAddObjectToDifferentSlot.Text = "Add object to different slot";
            this.butAddObjectToDifferentSlot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // butDeleteObject
            // 
            this.butDeleteObject.Image = global::WadTool.Properties.Resources.trash_16;
            this.butDeleteObject.Location = new System.Drawing.Point(15, 580);
            this.butDeleteObject.Name = "butDeleteObject";
            this.butDeleteObject.Padding = new System.Windows.Forms.Padding(5);
            this.butDeleteObject.Size = new System.Drawing.Size(108, 23);
            this.butDeleteObject.TabIndex = 12;
            this.butDeleteObject.Text = "Delete object";
            this.butDeleteObject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butDeleteObject.Click += new System.EventHandler(this.butDeleteObject_Click);
            // 
            // saveFileDialogWad2
            // 
            this.saveFileDialogWad2.Filter = "Tomb Editor Wad2 (*.wad2)|*.wad2";
            this.saveFileDialogWad2.Title = "Save Wad2";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 752);
            this.Controls.Add(this.butDeleteObject);
            this.Controls.Add(this.butAddObjectToDifferentSlot);
            this.Controls.Add(this.butAddObject);
            this.Controls.Add(this.panel3D);
            this.Controls.Add(this.treeSourceWad);
            this.Controls.Add(this.treeDestWad);
            this.Controls.Add(this.darkLabel2);
            this.Controls.Add(this.darkLabel1);
            this.Controls.Add(this.darkToolStrip1);
            this.Controls.Add(this.darkStatusStrip1);
            this.Controls.Add(this.darkMenuStrip1);
            this.Controls.Add(this.butTest);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.darkMenuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Wad Tool";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.darkMenuStrip1.ResumeLayout(false);
            this.darkMenuStrip1.PerformLayout();
            this.darkToolStrip1.ResumeLayout(false);
            this.darkToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkUI.Controls.DarkButton butTest;
        private DarkUI.Controls.DarkStatusStrip darkStatusStrip1;
        private DarkUI.Controls.DarkMenuStrip darkMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSourceWADToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDestinationWad2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveWad2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private DarkUI.Controls.DarkToolStrip darkToolStrip1;
        private DarkUI.Controls.DarkLabel darkLabel1;
        private DarkUI.Controls.DarkLabel darkLabel2;
        private DarkUI.Controls.DarkTreeView treeDestWad;
        private DarkUI.Controls.DarkTreeView treeSourceWad;
        private WadTool.Controls.PanelRendering panel3D;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertWADToWad2ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogWad;
        private System.Windows.Forms.ToolStripButton butOpenDestWad2;
        private System.Windows.Forms.ToolStripButton butOpenSourceWad;
        private System.Windows.Forms.ToolStripButton butSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripMenuItem saveWad2AsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soundManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton butSaveAs;
        private DarkUI.Controls.DarkButton butAddObject;
        private DarkUI.Controls.DarkButton butAddObjectToDifferentSlot;
        private DarkUI.Controls.DarkButton butDeleteObject;
        private System.Windows.Forms.SaveFileDialog saveFileDialogWad2;
    }
}

