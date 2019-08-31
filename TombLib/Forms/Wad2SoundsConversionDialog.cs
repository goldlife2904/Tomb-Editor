﻿using DarkUI.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TombLib.Utils;
using TombLib.Wad;
using TombLib.Wad.Catalog;

namespace TombLib.Forms
{
    public partial class Wad2SoundsConversionDialog : DarkForm
    {
        private readonly WadGameVersion _version;
        private readonly List<FileFormatConversions.SoundInfoConversionRow> _conversionRows;

        public Wad2SoundsConversionDialog(WadGameVersion version, List<FileFormatConversions.SoundInfoConversionRow> conversionRows)
        {
            _version = version;
            _conversionRows = conversionRows;

            InitializeComponent();
        }

        private void Wad2SoundsConversionDialog_Load(object sender, EventArgs e)
        {
            // Add rows
            ReloadSoundInfos();
        }

        private void ReloadSoundInfos()
        {
            dgvSoundInfos.Rows.Clear();

            foreach (var row in _conversionRows)
            {
                dgvSoundInfos.Rows.Add(row.OldName, (row.NewId != -1 ? row.NewId.ToString() : ""), row.NewName, 
                                       row.SaveToXml, row.ExportSamples);
                if (row.NewId != -1)
                    dgvSoundInfos.Rows[dgvSoundInfos.Rows.Count - 1].DefaultCellStyle.BackColor = Color.DarkGreen;
            }

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            var fixedSounds = 0;
            var soundsToSave = 0;
            foreach (var row in _conversionRows)
            {
                if (row.NewId != -1)
                    fixedSounds++;
                if (row.SaveToXml)
                    soundsToSave++;
            }

            var numSounds = _conversionRows.Count;
            var missingSounds = numSounds - fixedSounds;

            statusSamples.Text = "Sounds = " + numSounds + " | Fixed = " + fixedSounds + " | Missing = " + missingSounds;
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSoundInfos.Rows)
            {
                int id;
                string value = row.Cells[1].Value.ToString();

                if (value == "" || !int.TryParse(value, out id))
                {
                    DarkMessageBox.Show(this, "You have not selected a sound Id for sound '" +
                                        row.Cells[0].Value + "'. You must assign all sounds for converting your Wa2 file.",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                    return;
                }
            }

            for (int i = 0; i < _conversionRows.Count; i++)
            {
                DataGridViewRow row = dgvSoundInfos.Rows[i];

                _conversionRows[i].NewId = int.Parse(row.Cells[1].Value.ToString());
                _conversionRows[i].NewName = row.Cells[2].Value.ToString();
                _conversionRows[i].SaveToXml = (bool)row.Cells[3].Value;
                _conversionRows[i].ExportSamples = (bool)row.Cells[4].Value;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void dgvSamples_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 3)
            {
                DataGridViewRow row = dgvSoundInfos.Rows[e.RowIndex];
                row.Cells[e.ColumnIndex].Value = !((bool)row.Cells[e.ColumnIndex].Value);
            }
        }

        private void ButSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSoundInfos.Rows)
                row.Cells[3].Value = true;

            dgvSoundInfos.Invalidate();
        }

        private void ButUnselectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSoundInfos.Rows)
                row.Cells[3].Value = false;

            dgvSoundInfos.Invalidate();
        }

        private void DgvSoundInfos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }

        private void DgvSoundInfos_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 1)
                return;

            DataGridViewRow row = dgvSoundInfos.Rows[e.RowIndex];

            int id;
            if (!int.TryParse(row.Cells[1].Value.ToString(), out id))
            {
                row.DefaultCellStyle.BackColor = dgvSoundInfos.BackColor;
                row.Cells[1].Value = "";
                row.Cells[2].Value = "";
            }
            else
            {
                // Search if this Id was already assigned
                foreach (DataGridViewRow row2 in dgvSoundInfos.Rows)
                {
                    // Ignore the same row
                    if (row2.Index == row.Index)
                        continue;

                    // Ignore empty values
                    int id2;
                    if (!int.TryParse(row2.Cells[1].Value.ToString(), out id2))
                        continue;

                    // If is the same then warn the user
                    if (id == id2)
                    {
                        row.DefaultCellStyle.BackColor = dgvSoundInfos.BackColor;
                        row.Cells[1].Value = "";
                        row.Cells[2].Value = "";

                        DarkMessageBox.Show(this, "The selected Id " + id + " was already assigned to sound '" +
                                            row2.Cells[0].Value + "'", "Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string name = TrCatalog.GetOriginalSoundName(_version, (uint)id);
                if (name == null || name == "")
                {
                    row.DefaultCellStyle.BackColor = dgvSoundInfos.BackColor;
                    row.Cells[1].Value = "";
                    row.Cells[2].Value = "";
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.DarkGreen;
                    row.Cells[2].Value = name;
                }
            }

            dgvSoundInfos.InvalidateRow(e.RowIndex);
            UpdateStatus();
        }
    }
}
