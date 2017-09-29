﻿using DarkUI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TombLib.Wad;

namespace WadTool
{
    public partial class FormSoundEditor : DarkForm
    {
        private WadToolClass _tool;
        private int _currentSound = -1;

        public FormSoundEditor()
        {
            InitializeComponent();

            _tool = WadToolClass.Instance;
        }

        private void FormSoundEditor_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 370; i++)
                comboId.Items.Add(i.ToString());

            ReloadSoundInfos();
        }

        private void ReloadSoundInfos()
        {
            lstSoundInfos.Items.Clear();

            foreach (var soundInfo in _tool.DestinationWad.SoundInfo)
            {
                var item = new DarkUI.Controls.DarkListItem(soundInfo.Key + ": " + soundInfo.Value.Name);
                item.Tag = soundInfo.Key;
                lstSoundInfos.Items.Add(item);
            }
        }

        private void butAddNewSound_Click(object sender, EventArgs e)
        {
            ushort newSoundId = _tool.DestinationWad.GetFirstFreeSoundSlot();
            if (newSoundId == ushort.MaxValue)
            {
                DarkMessageBox.Show(this, "You soundmap is already full", "Error", MessageBoxIcon.Error);
                return;
            }

            _currentSound = -1;
            comboId.SelectedIndex = newSoundId;

            tbName.Text = "NEW_SOUND";
            tbChance.Text = "0";
            tbVolume.Text = "0";
            tbRange.Text = "8";
            tbPitch.Text = "0";
            cbFlagN.Checked = false;
            cbRandomizeGain.Checked = false;
            cbRandomizePitch.Checked = false;

            lstWaves.Items.Clear();

            butSaveChanges.Visible = true;
        }

        private void lstSoundInfos_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstSoundInfos.SelectedIndices.Count == 0) return;

            // Get the selected sound info
            var item = lstSoundInfos.Items[lstSoundInfos.SelectedIndices[0]];
            var soundInfo = _tool.DestinationWad.SoundInfo[(ushort)item.Tag];

            // Fill the UI
            tbName.Text = soundInfo.Name;
            tbChance.Text = soundInfo.Chance.ToString();
            tbVolume.Text = soundInfo.Volume.ToString();
            tbRange.Text = soundInfo.Range.ToString();
            tbPitch.Text = soundInfo.Pitch.ToString();
            cbFlagN.Checked = soundInfo.FlagN;
            comboLoop.SelectedIndex = (byte)soundInfo.Loop;
            cbRandomizeGain.Checked = soundInfo.RandomizeGain;
            cbRandomizePitch.Checked = soundInfo.RandomizePitch;
            comboId.SelectedIndex = (ushort)item.Tag;

            lstWaves.Items.Clear();
            foreach (var wave in soundInfo.WaveSounds)
            {
                var itemWave = new DarkUI.Controls.DarkListItem(wave.Name);
                itemWave.Tag = wave;
                lstWaves.Items.Add(itemWave);
            }

            _currentSound = (ushort)item.Tag;

            butSaveChanges.Visible = true;
        }

        private void butSaveChanges_Click(object sender, EventArgs e)
        {
            // I can't overwrite other sounds
            if (comboId.SelectedIndex != _currentSound &&
                _tool.DestinationWad.SoundInfo.ContainsKey((ushort)comboId.SelectedIndex))
            {
                DarkMessageBox.Show(this, "The selected slot is already assigned to another sound", "Error", MessageBoxIcon.Error);
                return;
            }

            if (lstWaves.Items.Count == 0)
            {
                DarkMessageBox.Show(this, "You must add at least one WAV sample", "Error", MessageBoxIcon.Error);
                return;
            }

            // Remap anim commands
            var oldSoundId = _currentSound;
            var newSoundId = (ushort)comboId.SelectedIndex;

            if (oldSoundId != -1)
            {
                foreach (var moveable in _tool.DestinationWad.Moveables)
                {
                    foreach (var animation in moveable.Value.Animations)
                    {
                        foreach (var command in animation.AnimCommands)
                        {
                            if (command.Type == TombLib.Wad.WadAnimCommandType.PlaySound)
                            {
                                ushort soundId = (ushort)(command.Parameter2 & 0x3fff);
                                if (soundId == oldSoundId)
                                {
                                    // Remap current sound
                                    command.Parameter2 = (ushort)((command.Parameter2 & 0xc000) + newSoundId);
                                }
                            }
                        }
                    }
                }
            }

            // Get the current sound info
            WadSoundInfo soundInfo;

            if (oldSoundId == -1)
                soundInfo = new WadSoundInfo();
            else
                soundInfo = _tool.DestinationWad.SoundInfo[(ushort)oldSoundId];

            // Save changes
            soundInfo.Chance = Byte.Parse(tbChance.Text);
            soundInfo.Volume = Byte.Parse(tbVolume.Text);
            soundInfo.Range = Byte.Parse(tbRange.Text);
            soundInfo.Pitch = Byte.Parse(tbPitch.Text);
            soundInfo.Loop = (WadSoundLoopType)comboLoop.SelectedIndex;
            soundInfo.FlagN = cbFlagN.Checked;
            soundInfo.RandomizeGain = cbRandomizeGain.Checked;
            soundInfo.RandomizePitch = cbRandomizePitch.Checked;
            soundInfo.Name = tbName.Text;

            soundInfo.WaveSounds.Clear();
            foreach (var item in lstWaves.Items)
                soundInfo.WaveSounds.Add((WadSound)item.Tag);

            if (oldSoundId == -1)
            {
                _tool.DestinationWad.SoundInfo.Add(newSoundId, soundInfo);

                ReloadSoundInfos();
            }
            else
            {
                if (oldSoundId != newSoundId)
                {
                    _tool.DestinationWad.SoundInfo.Remove((ushort)oldSoundId);
                    _tool.DestinationWad.SoundInfo.Add(newSoundId, soundInfo);

                    ReloadSoundInfos();
                }
                else
                {
                    lstSoundInfos.Items[lstSoundInfos.SelectedIndices[0]].Text = oldSoundId + ": " + tbName.Text;
                }
            }
        }

        private void butAddNewWave_Click(object sender, EventArgs e)
        {
            var form = new FormSelectWave();
            if (form.ShowDialog() == DialogResult.Cancel) return;

            // Search for already existing sample
            for (int i = 0; i < lstWaves.Items.Count; i++)
            {
                var item = lstWaves.Items[i];
                var wave = (WadSound)item.Tag;
                if (wave.Hash == form.SelectedWave.Hash)
                {
                    DarkMessageBox.Show(this, "This WAV sample is already present in this sound info", "Error", MessageBoxIcon.Error);
                    return;
                }
            }

            // Add the new WAV sample
            var newItem = new DarkUI.Controls.DarkListItem(form.SelectedWave.Name);
            newItem.Tag = form.SelectedWave;
            lstWaves.Items.Add(newItem);
        }

        private void butDeleteWave_Click(object sender, EventArgs e)
        {
            if (lstWaves.SelectedIndices.Count == 0) return;

            var item = lstWaves.Items[lstWaves.SelectedIndices[0]];
            var wave = (WadSound)item.Tag;

            // Ask to the user the permission to delete WAV
            if (DarkMessageBox.Show(this,
                   "Are you really sure to delete '" + wave.Name + "'? The WAV sample will be removed from this sound but not from Wad2 file until some sound is referencing it",
                   "Delete WAV", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            lstWaves.Items.Remove(item);
        }

        private void butPlaySound_Click(object sender, EventArgs e)
        {
            if (lstWaves.SelectedIndices.Count == 0) return;

            var item = lstWaves.Items[lstWaves.SelectedIndices[0]];
            var wave = (WadSound)item.Tag;

            wave.Play();
        }

        private void butDeleteSound_Click(object sender, EventArgs e)
        {
            if (lstSoundInfos.SelectedIndices.Count == 0) return;

            var item = lstSoundInfos.Items[lstSoundInfos.SelectedIndices[0]];
            var soundInfo = _tool.DestinationWad.SoundInfo[(ushort)item.Tag];
            var soundIdToRemove = (ushort)item.Tag;

            // Get all moveables that are using this sound
            var moveables = _tool.DestinationWad.GetAllMoveablesReferencingSound(soundIdToRemove);

            if (moveables.Count != 0)
            {
                var stringMoveables = "";
                foreach (var mov in moveables)
                    stringMoveables += mov.ToString() + Environment.NewLine;

                // Ask to the user the permission to delete WAV
                if (DarkMessageBox.Show(this,
                       "Are you really sure to delete '" + soundInfo.Name + "'? The following moveables are referincing " +
                       "this sound and their animation commands will be remapped to the first available sound: " + Environment.NewLine +
                       stringMoveables,
                       "Delete sound", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            else
            {
                // Ask to the user the permission to delete WAV
                if (DarkMessageBox.Show(this,
                       "Are you really sure to delete '" + soundInfo.Name + "'?",
                       "Delete sound", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            // Delete the sound and remap anim commands
            _tool.DestinationWad.DeleteSound(soundIdToRemove);

            ReloadSoundInfos();
            _currentSound = -1;
        }
    }
}
