﻿using DarkUI.Controls;
using DarkUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TombLib.Wad;
using TombLib.Wad.Catalog;

namespace WadTool
{
    public partial class FormAnimCommandsEditor : DarkForm
    {
        private readonly WadToolClass _tool;
        public IEnumerable<WadAnimCommand> AnimCommands => lstCommands.Items.Select(item => item.Tag).OfType<WadAnimCommand>();
        private bool _currentlyDoingCommandSelection = false;
        private List<int> _sounds;

        private WadAnimCommand _selectedCommand => lstCommands.Items.Count == 0 || lstCommands.SelectedIndices.Count == 0 ? null : lstCommands.Items[lstCommands.SelectedIndices[0]]?.Tag as WadAnimCommand;

        public FormAnimCommandsEditor(WadToolClass tool, List<WadAnimCommand> animCommands, WadAnimCommand startupCommand = null)
        {
            InitializeComponent();
            _tool = tool;

            ReloadSounds();

            foreach (var cmd in animCommands)
                lstCommands.Items.Add(new DarkListItem(cmd.ToString()) { Tag = cmd.Clone() });

            if (startupCommand != null)
            {
                if (!animCommands.Contains(startupCommand))
                    lstCommands.Items.Add(new DarkListItem(startupCommand.ToString()) { Tag = startupCommand.Clone() });
                SelectCommand(startupCommand);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (lstCommands.SelectedIndices.Count > 0)
                lstCommands.EnsureVisible();
        }

        private void SelectCommand(WadAnimCommand cmd, bool selectInTree = true)
        {
            if (_currentlyDoingCommandSelection)
                return;

            if (cmd == null)
            {
                comboCommandType.Enabled = false;
                commandControls.Visible = false;
                return;
            }
            else
            {
                comboCommandType.Enabled = true;
                commandControls.Visible = true;
            }

            try
            {
                _currentlyDoingCommandSelection = true;

                if (selectInTree)
                {
                    for (int i = 0; i < lstCommands.Items.Count; i++)
                        if (lstCommands.Items[i].Text == cmd.ToString()) lstCommands.SelectItem(i);
                    lstCommands.EnsureVisible();
                }

                comboCommandType.SelectedIndex = (int)(cmd.Type) - 1;

                switch (cmd.Type)
                {
                    case WadAnimCommandType.SetPosition:
                        commandControls.Visible = true;
                        commandControls.SelectedTab = tabSetPosition;

                        tbPosX.Value = cmd.Parameter1;
                        tbPosY.Value = cmd.Parameter2;
                        tbPosZ.Value = cmd.Parameter3;
                        break;

                    case WadAnimCommandType.SetJumpDistance:
                        commandControls.Visible = true;
                        commandControls.SelectedTab = tabSetJumpDistance;

                        tbHorizontal.Value = cmd.Parameter1;
                        tbVertical.Value = cmd.Parameter2;
                        break;

                    case WadAnimCommandType.EmptyHands:
                    case WadAnimCommandType.KillEntity:
                        commandControls.Visible = false;
                        break;

                    case WadAnimCommandType.PlaySound:
                        commandControls.Visible = true;
                        commandControls.SelectedTab = tabPlaySound;

                        tbPlaySoundFrame.Value = cmd.Parameter1;
                        comboSound.SelectedIndex = _sounds.IndexOf(cmd.Parameter2 & 0x3FFF);

                        switch (cmd.Parameter2 & 0xC000)
                        {
                            default:
                                comboPlaySoundConditions.SelectedIndex = 0;
                                break;
                            case 0x4000:
                                comboPlaySoundConditions.SelectedIndex = 1;
                                break;
                            case 0x8000:
                                comboPlaySoundConditions.SelectedIndex = 2;
                                break;
                        }
                        break;

                    case WadAnimCommandType.FlipEffect:
                        commandControls.Visible = true;
                        commandControls.SelectedTab = tabFlipeffect;

                        tbFlipEffectFrame.Value = cmd.Parameter1;
                        tbFlipEffect.Value = cmd.Parameter2;
                        break;
                }
            }
            finally
            {
                _currentlyDoingCommandSelection = false;
            }
        }

        private void DeleteCommand()
        {
            if (lstCommands.SelectedIndices.Count == 0)
                return;

            for (int i = lstCommands.Items.Count - 1; i >= 0; i--)
                if (lstCommands.SelectedIndices.Contains(i))
                    lstCommands.Items.RemoveAt(i);

            SelectCommand(lstCommands.Items.FirstOrDefault()?.Tag as WadAnimCommand);
        }

        private void MoveCommand(bool down)
        {
            if (lstCommands.Items.Count <= 1)
                return;

            int index = lstCommands.SelectedIndices[0];
            int newIndex = down ? index + 1 : index - 1;
            var item = lstCommands.Items[index];

            if (down && index >= lstCommands.Items.Count - 1) return;
            if (!down && index <= 0) return;

            lstCommands.Items.RemoveAt(index);
            lstCommands.Items.Insert((newIndex), item);
            lstCommands.SelectItem(newIndex);
            lstCommands.EnsureVisible();
        }

        private void ReloadSounds()
        {
            _sounds = new List<int>();
            comboSound.Items.Clear();
            foreach (var sound in TrCatalog.GetAllSounds(_tool.DestinationWad.SuggestedGameVersion))
            {
                _sounds.Add((int)sound.Key);
                comboSound.Items.Add(sound.Key.ToString().PadLeft(4, '0') + ": " + sound.Value);
            }
            comboSound.SelectedIndex = 0;
        }

        public void UpdateSelectedItemText() => lstCommands.Items[lstCommands.SelectedIndices[0]].Text = lstCommands.Items[lstCommands.SelectedIndices[0]].Tag.ToString();

        private void butCommandUp_Click(object sender, EventArgs e) => MoveCommand(false);
        private void butCommandDown_Click(object sender, EventArgs e) => MoveCommand(true);
        private void butDeleteEffect_Click(object sender, EventArgs e) => DeleteCommand();

        private void butAddEffect_Click(object sender, EventArgs e)
        {
            var newCmd = new WadAnimCommand() { Type = WadAnimCommandType.SetPosition };
            lstCommands.Items.Add(new DarkListItem(newCmd.ToString()) { Tag = newCmd });
            SelectCommand(newCmd);
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lstCommands_SelectedIndicesChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null)
                return;
            SelectCommand(_selectedCommand, false);
        }

        private void comboCommandType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null || _currentlyDoingCommandSelection)
                return;

            WadAnimCommandType newType = (WadAnimCommandType)(comboCommandType.SelectedIndex + 1);
            _selectedCommand.Type = newType;

            lstCommands.Items[lstCommands.SelectedIndices[0]].Text = lstCommands.Items[lstCommands.SelectedIndices[0]].Tag.ToString();
            SelectCommand(_selectedCommand, false);
        }

        private void tbPosX_ValueChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null || _selectedCommand.Type != WadAnimCommandType.SetPosition)
                return;
            _selectedCommand.Parameter1 = (short)tbPosX.Value;
            UpdateSelectedItemText();
        }

        private void tbPosY_ValueChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null || _selectedCommand.Type != WadAnimCommandType.SetPosition)
                return;
            _selectedCommand.Parameter2 = (short)tbPosY.Value;
            UpdateSelectedItemText();
        }

        private void tbPosZ_ValueChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null || _selectedCommand.Type != WadAnimCommandType.SetPosition)
                return;
            _selectedCommand.Parameter3 = (short)tbPosZ.Value;
            UpdateSelectedItemText();
        }

        private void tbHorizontal_ValueChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null || _selectedCommand.Type != WadAnimCommandType.SetJumpDistance)
                return;
            _selectedCommand.Parameter1 = (short)tbHorizontal.Value;
            UpdateSelectedItemText();
        }

        private void tbVertical_ValueChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null || _selectedCommand.Type != WadAnimCommandType.SetJumpDistance)
                return;
            _selectedCommand.Parameter2 = (short)tbVertical.Value;
            UpdateSelectedItemText();
        }

        private void tbFlipEffectFrame_ValueChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null || _selectedCommand.Type != WadAnimCommandType.FlipEffect)
                return;
            _selectedCommand.Parameter1 = (short)tbFlipEffectFrame.Value;
            UpdateSelectedItemText();
        }

        private void tbFlipEffect_ValueChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null || _selectedCommand.Type != WadAnimCommandType.FlipEffect)
                return;
            _selectedCommand.Parameter2 = (short)tbFlipEffect.Value;
            UpdateSelectedItemText();
        }

        private void tbPlaySoundFrame_ValueChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null || _selectedCommand.Type != WadAnimCommandType.PlaySound)
                return;
            _selectedCommand.Parameter1 = (short)tbPlaySoundFrame.Value;
            UpdateSelectedItemText();
        }

        private void comboPlaySoundConditions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null || _selectedCommand.Type != WadAnimCommandType.PlaySound)
                return;
            _selectedCommand.Parameter2 &= unchecked((short)~0xC000);
            _selectedCommand.Parameter2 |= (short)(comboPlaySoundConditions.SelectedIndex << 14);
            UpdateSelectedItemText();
        }

        private void comboSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedCommand == null || _selectedCommand.Type != WadAnimCommandType.PlaySound)
                return;

            _selectedCommand.Parameter2 &= unchecked((short)~0x3FFF);
            _selectedCommand.Parameter2 |= (short)(_sounds[comboSound.SelectedIndex]);
            UpdateSelectedItemText();
        }

        private void FormAnimCommandsEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteCommand();
        }
    }
}
