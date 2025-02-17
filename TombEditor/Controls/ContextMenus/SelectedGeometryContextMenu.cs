﻿using System.Windows.Forms;
using TombLib;
using TombLib.LevelData;

namespace TombEditor.Controls.ContextMenus
{
    class SelectedGeometryContextMenu : BaseContextMenu
    {
        public SelectedGeometryContextMenu(Editor editor, IWin32Window owner, Room targetRoom, RectangleInt2 targetArea, VectorInt2 targetBlock)
            : base(editor, owner)
        {
			
            Items.Add(new ToolStripMenuItem("Paste object", Properties.Resources.general_clipboard_16, (o, e) =>
            {
                EditorActions.PasteObject(targetBlock, targetRoom);
            })
            { Enabled = Clipboard.ContainsData(typeof(ObjectClipboardData).FullName) });

            Items.Add(new ToolStripMenuItem("Select objects", null, (o, e) =>
            {
                EditorActions.SelectObjectsInArea(this, editor.SelectedSectors);
            }));

            Items.Add(new ToolStripSeparator());

            Items.Add(new ToolStripMenuItem("Move Lara", null, (o, e) =>
            {
                EditorActions.MoveLara(this, targetRoom, targetBlock);
            }));

			Items.Add(new ToolStripMenuItem("Move Object", Properties.Resources.general_target_16, (o, e) => 
            {
				var obj = editor.SelectedObject as PositionBasedObjectInstance;
				EditorActions.MoveObject(obj, targetRoom, targetBlock);
			}) { Enabled = _editor.SelectedObject is PositionBasedObjectInstance && !(_editor.SelectedObject is ObjectGroup) });
			Items.Add(new ToolStripSeparator());

            Items.Add(new ToolStripMenuItem("Add trigger", null, (o, e) =>
            {
                CommandHandler.GetCommand("AddTrigger").Execute(new CommandArgs { Editor = editor, Window = owner });
            }));

            Items.Add(new ToolStripMenuItem("Add portal", null, (o, e) =>
            {
                CommandHandler.GetCommand("AddPortal").Execute(new CommandArgs { Editor = editor, Window = owner });
            }));
            Items.Add(new ToolStripSeparator());

            Items.Add(new ToolStripMenuItem("Add camera", Properties.Resources.objects_Camera_16, (o, e) =>
            {
                EditorActions.PlaceObject(targetRoom, targetBlock, new CameraInstance());
            }));

            Items.Add(new ToolStripMenuItem("Add flyby camera", Properties.Resources.objects_movie_projector_16, (o, e) =>
            {
                EditorActions.PlaceObject(targetRoom, targetBlock, new FlybyCameraInstance(editor.SelectedObject));
            }));

            Items.Add(new ToolStripMenuItem("Add sink", Properties.Resources.objects_tornado_16, (o, e) =>
            {
                EditorActions.PlaceObject(targetRoom, targetBlock, new SinkInstance());
            }));

            Items.Add(new ToolStripMenuItem("Add sound source", Properties.Resources.objects_speaker_16, (o, e) =>
            {
                EditorActions.PlaceObject(targetRoom, targetBlock, new SoundSourceInstance());
            }));

            Items.Add(new ToolStripMenuItem("Add imported geometry", Properties.Resources.objects_custom_geometry, (o, e) =>
            {
                EditorActions.PlaceObject(targetRoom, targetBlock, new ImportedGeometryInstance());
            }));

            Items.Add(new ToolStripMenuItem("Add memo", Properties.Resources.objects_Memo_16, (o, e) =>
            {
                EditorActions.PlaceObject(targetRoom, targetBlock, new MemoInstance());
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
