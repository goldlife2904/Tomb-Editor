﻿using DarkUI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TombEditor.Geometry;

namespace TombEditor
{
    public partial class FormTrigger : DarkForm
    {
        private Level _level;
        private TriggerInstance _trigger;
        private List<int> _items;

        public FormTrigger(Level level, TriggerInstance trigger)
        {
            _level = level;
            _trigger = trigger;
            InitializeComponent();
        }

        private void FormTrigger_Load(object sender, EventArgs e)
        {
            comboType.SelectedIndex = (int)_trigger.TriggerType;
            comboTargetType.SelectedIndex = (int)_trigger.TargetType;
            cbBit1.Checked = (_trigger.CodeBits & (1 << 0)) != 0;
            cbBit2.Checked = (_trigger.CodeBits & (1 << 1)) != 0;
            cbBit3.Checked = (_trigger.CodeBits & (1 << 2)) != 0;
            cbBit4.Checked = (_trigger.CodeBits & (1 << 3)) != 0;
            cbBit5.Checked = (_trigger.CodeBits & (1 << 4)) != 0;
            cbOneShot.Checked = _trigger.OneShot;
            tbTimer.Text = _trigger.Timer.ToString();
        }

        private void comboTargetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TriggerTargetType triggerTargetType = (TriggerTargetType)comboTargetType.SelectedIndex;

            switch (triggerTargetType)
            {
                case TriggerTargetType.Object:
                    tbParameter.Visible = false;
                    comboParameter.Visible = true;
                    FindAndAddObjects(ObjectInstanceType.Moveable);
                    break;

                case TriggerTargetType.Camera:
                    tbParameter.Visible = false;
                    comboParameter.Visible = true;
                    FindAndAddObjects(ObjectInstanceType.Camera);
                    break;

                case TriggerTargetType.Sink:
                    tbParameter.Visible = false;
                    comboParameter.Visible = true;
                    FindAndAddObjects(ObjectInstanceType.Sink);
                    break;

                case TriggerTargetType.FlipEffect:
                    tbParameter.Visible = true;
                    comboParameter.Visible = false;
                    tbParameter.Text = _trigger.Target.ToString();
                    break;

                case TriggerTargetType.FlipOn:
                    tbParameter.Visible = true;
                    comboParameter.Visible = false;
                    tbParameter.Text = _trigger.Target.ToString();
                    break;

                case TriggerTargetType.FlipOff:
                    tbParameter.Visible = true;
                    comboParameter.Visible = false;
                    tbParameter.Text = _trigger.Target.ToString();
                    break;

                case TriggerTargetType.Target:
                    tbParameter.Visible = false;
                    comboParameter.Visible = true;

                    // Actually it is possible to not only target Target objects, but all movables.
                    // This is also useful: It makes sense to target egg a trap or an enemy.
                    FindAndAddObjects(ObjectInstanceType.Moveable);
                    break;

                case TriggerTargetType.FlipMap:
                    tbParameter.Visible = true;
                    comboParameter.Visible = false;
                    tbParameter.Text = _trigger.Target.ToString();
                    break;

                case TriggerTargetType.FinishLevel:
                    tbParameter.Visible = true;
                    comboParameter.Visible = false;
                    tbParameter.Text = _trigger.Target.ToString();
                    break;

                case TriggerTargetType.Secret:
                    tbParameter.Visible = true;
                    comboParameter.Visible = false;
                    tbParameter.Text = _trigger.Target.ToString();
                    break;

                case TriggerTargetType.PlayAudio:
                    tbParameter.Visible = true;
                    comboParameter.Visible = false;
                    tbParameter.Text = _trigger.Target.ToString();
                    break;

                case TriggerTargetType.FlyByCamera:
                    tbParameter.Visible = false;
                    comboParameter.Visible = true;
                    FindAndAddObjects(ObjectInstanceType.FlyByCamera);
                    break;

                case TriggerTargetType.Fmv:
                    tbParameter.Visible = true;
                    comboParameter.Visible = false;
                    tbParameter.Text = _trigger.Target.ToString();
                    break;
            }
        }

        private void FindAndAddObjects(ObjectInstanceType type)
        {
            comboParameter.Items.Clear();
            _items = new List<int>();

            foreach (ObjectInstance instance in _level.Objects.Values)
                if (instance.Type == type)
                {
                    _items.Add(instance.Id);
                    comboParameter.Items.Add(instance.ToString());
                    if (_trigger.Target == instance.Id)
                        comboParameter.SelectedIndex = comboParameter.Items.Count - 1;
                }

            if (comboParameter.Items.Count != 0 && comboParameter.SelectedIndex == -1)
                comboParameter.SelectedIndex = 0;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            _trigger.TriggerType = (TriggerType)comboType.SelectedIndex;
            _trigger.TargetType = (TriggerTargetType)comboTargetType.SelectedIndex;
            _trigger.Timer = Int16.Parse(tbTimer.Text);
            byte codeBits = 0;
            codeBits |= (byte)(cbBit1.Checked ? (1 << 0) : 0);
            codeBits |= (byte)(cbBit2.Checked ? (1 << 1) : 0);
            codeBits |= (byte)(cbBit3.Checked ? (1 << 2) : 0);
            codeBits |= (byte)(cbBit4.Checked ? (1 << 3) : 0);
            codeBits |= (byte)(cbBit5.Checked ? (1 << 4) : 0);
            _trigger.CodeBits = codeBits;
            _trigger.OneShot = cbOneShot.Checked;

            if (_trigger.TargetType == TriggerTargetType.Object || _trigger.TargetType == TriggerTargetType.Camera ||
                _trigger.TargetType == TriggerTargetType.Target || _trigger.TargetType == TriggerTargetType.FlyByCamera ||
                _trigger.TargetType == TriggerTargetType.Sink)
            {
                if (comboParameter.SelectedIndex == -1)
                {
                    DarkUI.Forms.DarkMessageBox.ShowWarning("You don't have in your project any object of the type that you have required",
                                                            "Save trigger", DarkUI.Forms.DarkDialogButton.Ok);
                    return;
                }

                _trigger.Target = _items[comboParameter.SelectedIndex];
            }
            else
            {
                _trigger.Target = Int16.Parse(tbParameter.Text);
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
