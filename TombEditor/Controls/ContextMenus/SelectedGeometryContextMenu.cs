﻿using System.Windows.Forms;
using TombLib;
using TombLib.LevelData;

namespace TombEditor.Controls.ContextMenus
{
    class SelectedGeometryContextMenu : BaseContextMenu
    {
        public SelectedGeometryContextMenu(Editor editor, IWin32Window owner, Room targetRoom, RectangleInt2 targetArea)
            : base(editor, owner)
        {
            Items.Add(new ToolStripMenuItem("Add trigger", null, (o, e) =>
            {
                EditorActions.AddTrigger(targetRoom, targetArea, this);
            }));

            Items.Add(new ToolStripMenuItem("Add portal", null, (o, e) =>
            {
                EditorActions.AddPortal(targetRoom, targetArea, this);
            }));

            Items.Add(new ToolStripSeparator());

            Items.Add(new ToolStripMenuItem("Crop room", Properties.Resources.general_crop_16, (o, e) =>
            {
                EditorActions.CropRoom(targetRoom, targetArea, this);
            }));

            Items.Add(new ToolStripMenuItem("Split room", Properties.Resources.actions_Split_16, (o, e) =>
            {
                EditorActions.SplitRoom(this);
            }));
        }
    }
}
