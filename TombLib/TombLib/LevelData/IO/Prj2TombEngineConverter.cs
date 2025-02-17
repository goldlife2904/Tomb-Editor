﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using TombLib.Utils;
using TombLib.Wad;
using TombLib.Wad.Catalog;

namespace TombLib.LevelData.IO
{
    public static class Prj2TombEngineConverter
    {
        private static readonly string _tenReferenceWad = Path.Combine(DefaultPaths.ProgramDirectory, "Assets", "Wads", "TombEngine.wad2");

        public static string Start(string fileName, IWin32Window owner, IProgressReporter progressReporter)
        {
            if (!File.Exists(fileName))
            {
                progressReporter.ReportWarn("The specified project doesn't exist.");
                return string.Empty;
            }

            progressReporter.ReportInfo("TombEngine Project Converter");
            progressReporter.ReportInfo(" ");

            string newProject = ConvertProject(fileName, progressReporter);

            if (string.IsNullOrEmpty(newProject))
            {
                throw new Exception("There was an error while converting your project.");
            }

            progressReporter.ReportInfo(" ");
            progressReporter.ReportInfo("Project was converted successfully!");
            progressReporter.ReportInfo("Notes about the conversion:");
            progressReporter.ReportInfo(" ");
            progressReporter.ReportInfo("   - Enemies now have MESHSWAP slots (e.g., BADDY2 -> MESHSWAP_BADDY2), use WadTool for remapping these slots.");
            progressReporter.ReportInfo("   - Some slots are not supported and were added as ANIMATING above 16.");
            progressReporter.ReportInfo("   - TEN uses different slots for every sprite (fire, smoke). They were added from a reference Wad2.");
            progressReporter.ReportInfo("   - Soundmap is similar to WAD130 and should work by default.");
            progressReporter.ReportInfo("   - TRNG triggers are not supported and won't be compiled.");
            progressReporter.ReportInfo("   - TEN requires new LARA object which was copied from reference Wad2.");
            progressReporter.ReportInfo("   - Weapon anims and holsters objects have changed and were also converted.");

            return newProject;
        }

        private static string ConvertProject(string source, IProgressReporter progressReporter)
        {
            try
            {
                // Load new TombEngine reference Wad2
                Wad2 referenceWad = Wad2Loader.LoadFromFile(_tenReferenceWad, true);

                // Load level and all related resources
                Level level = Path.GetExtension(source).ToLower() == ".prj" ?
                    PrjLoader.LoadFromPrj(source, string.Empty, true, false, null) : Prj2Loader.LoadFromPrj2(source, null);

                if (level == null)
                {
                    progressReporter.ReportWarn("Error while loading level.");
                    return string.Empty;
                }

                if (level.Settings.GameVersion.Native() != TRVersion.Game.TR4 &&
                    level.Settings.GameVersion.Native() != TRVersion.Game.TRNG)
                {
                    progressReporter.ReportWarn("Only TR4 and TRNG projects can be converted to TEN at this time.");
                    return string.Empty;
                }

                // Now convert resources to new format
                List<ReferencedWad> newWads = new List<ReferencedWad>();
                Dictionary<uint, uint> remappedSlots = new Dictionary<uint, uint>();

                string newFileName;
                string newPath;
                bool addedTimex = false;

                foreach (ReferencedWad wadRef in level.Settings.Wads)
                {
                    Wad2 wad = wadRef.Wad;
                    Wad2 newWad = new Wad2 { GameVersion = TRVersion.Game.TombEngine };

                    // Get base remap object which is ANIMATING33 (first animating which doesn't exist in any legacy engines)
                    bool isMoveable;
                    uint remappedObjectIndex = TrCatalog.GetItemIndex(TRVersion.Game.TombEngine, "ANIMATING33", out isMoveable).Value;

                    // Copy all objects to new wad
                    foreach (KeyValuePair<WadMoveableId, WadMoveable> moveable in wad.Moveables)
                    {
                        uint newSlot;
                        string oldId = TrCatalog.GetMoveableName(TRVersion.Game.TR4, moveable.Key.TypeId);
                        string newId = TrCatalog.GetMoveableTombEngineSlot(TRVersion.Game.TR4, moveable.Key.TypeId);

                        if (string.IsNullOrEmpty(newId))
                        {
                            newSlot = remappedObjectIndex;
                            newId = TrCatalog.GetMoveableName(TRVersion.Game.TombEngine, newSlot);
                            progressReporter.ReportWarn("    Slot " + oldId + " is not supported by TombEngine and it will be remapped to " + newId);
                            remappedObjectIndex++;
                        }
                        else
                        {
                            uint? found = TrCatalog.GetItemIndex(TRVersion.Game.TombEngine, newId, out isMoveable);
                            if (!found.HasValue)
                            {
                                continue;
                            }
                            else
                            {
                                newSlot = found.Value;
                            }
                        }

                        string newSlotName = TrCatalog.GetMoveableName(TRVersion.Game.TombEngine, newSlot);

                        progressReporter.ReportInfo(oldId + " → [" + newSlot + "] " + newSlotName);

                        if (newSlotName == "BINOCULAR_GRAPHICS" || newSlotName == "TARGET_GRAPHICS")
                        {
                            progressReporter.ReportInfo("    Skipping unneeded " + newSlotName + " slot");
                            continue;
                        }

                        // We need to copy mesh 14 to 7 for {WEAPON}_ANIM for back weapons
                        if (newSlotName == "SHOTGUN_ANIM" || newSlotName == "CROSSBOW_ANIM" || newSlotName == "HK_ANIM" ||
                            newSlotName == "HARPOON_ANIM" || newSlotName == "GRENADE_ANIM" || newSlotName == "ROCKET_ANIM")
                        {
                            progressReporter.ReportInfo("    Copying mesh #14 to mesh #7 for " + newSlotName);

                            WadMesh mesh = moveable.Value.Bones[14].Mesh.Clone();
                            for (int i = 0; i < mesh.VertexPositions.Count; i++)
                            {
                                Vector3 pos = mesh.VertexPositions[i];
                                pos.Y += 256;
                                mesh.VertexPositions[i] = pos;
                            }
                            moveable.Value.Bones[7].Mesh = mesh;
                            moveable.Value.Meshes[7] = mesh;
                        }

                        // For holsters, we need to put holsters meshes in 1 and 4 and apply the same skeleton from LARA
                        if (newSlotName == "LARA_HOLSTERS" || newSlotName == "LARA_HOLSTERS_PISTOLS" ||
                            newSlotName == "LARA_HOLSTERS_UZIS" || newSlotName == "LARA_HOLSTERS_REVOLVER")
                        {
                            progressReporter.ReportInfo("    Copying holsters meshes for " + newSlotName);

                            WadMoveable laraMoveable = referenceWad.Moveables[new WadMoveableId(0)];
                            List<WadBone> newBones = new List<WadBone>();
                            List<WadMesh> newMeshes = new List<WadMesh>();

                            foreach (WadBone oldBone in laraMoveable.Bones)
                            {
                                newBones.Add(oldBone.Clone());
                            }

                            foreach (WadMesh oldMesh in laraMoveable.Meshes)
                            {
                                newMeshes.Add(oldMesh.Clone());
                            }

                            newBones[1].Mesh = moveable.Value.Bones[4].Mesh.Clone();
                            newMeshes[1] = newBones[1].Mesh;

                            newBones[4].Mesh = moveable.Value.Bones[8].Mesh.Clone();
                            newMeshes[4] = newBones[4].Mesh;

                            moveable.Value.Bones.Clear();
                            moveable.Value.Bones.AddRange(newBones);
                            moveable.Value.Meshes.Clear();
                            moveable.Value.Meshes.AddRange(newMeshes);
                        }

                        if (newSlotName == "TWOBLOCK_PLATFORM" ||
                            newSlotName.ToLower().Contains("falling_block") ||
                            newSlotName.ToLower().Contains("trapdoor"))
                        {
                            progressReporter.ReportInfo("    Adjusting bridge object collision box " + newSlotName);

                            if (moveable.Value.Animations.Count > 0)
                            {
                                WadAnimation anim = moveable.Value.Animations[0];
                                for (int f = 0; f < anim.KeyFrames.Count; f++)
                                {
                                    BoundingBox oldBB = anim.KeyFrames[f].BoundingBox;
                                    oldBB.Maximum = new Vector3(oldBB.Maximum.X, oldBB.Maximum.Y * 0.8f, oldBB.Maximum.Z);
                                    anim.KeyFrames[f].BoundingBox = oldBB;
                                }
                            }
                        }

                        if (newSlotName == "EXPANDING_PLATFORM")
                        {
                            progressReporter.ReportInfo("    Adjusting mesh and pivot point for " + newSlotName);

                            for (int m = 0; m < moveable.Value.Bones.Count; m++)
                            {
                                WadMesh mesh = moveable.Value.Bones[m].Mesh.Clone();

                                for (int i = 0; i < mesh.VertexPositions.Count; i++)
                                {
                                    Vector3 pos = mesh.VertexPositions[i];
                                    pos.Z += 512;
                                    mesh.VertexPositions[i] = pos;
                                }
                                moveable.Value.Bones[m].Mesh = mesh;
                                moveable.Value.Meshes[m] = mesh;
                            }

                            foreach (WadAnimation anim in moveable.Value.Animations)
                            {
                                foreach (WadKeyFrame frame in anim.KeyFrames)
                                {
                                    BoundingBox bb = frame.BoundingBox;
                                    bb.Maximum.Z += 512;
                                    bb.Minimum.Z += 512;
                                    frame.BoundingBox = bb;
                                }
                            }
                        }

                        if (newSlotName == "ANIMATING16")
                        {
                            progressReporter.ReportInfo("    Setting hardcoded collision for " + newSlotName);

                            foreach (WadAnimation anim in moveable.Value.Animations)
                            {
                                foreach (WadKeyFrame frame in anim.KeyFrames)
                                {
                                    frame.BoundingBox = new BoundingBox();
                                }
                            }
                        }

                        if (!addedTimex &&
                            (newSlotName == "MEMCARD_LOAD_INV_ITEM" || newSlotName == "MEMCARD_SAVE_INV_ITEM" ||
                             newSlotName == "PC_LOAD_INV_ITEM" || newSlotName == "PC_SAVE_INV_ITEM"))
                        {
                            progressReporter.ReportInfo("    Adding TIMEX from reference Wad2");

                            uint timexIndex = TrCatalog.GetItemIndex(TRVersion.Game.TombEngine, "TIMEX_ITEM", out isMoveable).Value;
                            newWad.Add(new WadMoveableId(timexIndex), referenceWad.Moveables[new WadMoveableId(timexIndex)]);
                            addedTimex = true;
                        }

                        if (newSlotName == "LARA")
                        {
                            newWad.Add(new WadMoveableId(0), referenceWad.Moveables[new WadMoveableId(0)]);
                        }
                        else
                        {
                            newWad.Add(new WadMoveableId(newSlot), moveable.Value);
                            if (!addedTimex && newSlotName == "TIMEX_ITEM")
                            {
                                addedTimex = true;
                            }
                        }

                        if (!remappedSlots.ContainsKey(moveable.Key.TypeId))
                        {
                            remappedSlots.Add(moveable.Key.TypeId, newSlot);
                        }
                    }

                    // Copy all statics
                    foreach (KeyValuePair<WadStaticId, WadStatic> staticModel in wad.Statics)
                    {
                        newWad.Statics.Add(staticModel.Key, staticModel.Value);
                    }

                    // Copy all sprite sequences
                    foreach (KeyValuePair<WadSpriteSequenceId, WadSpriteSequence> sequence in wad.SpriteSequences)
                    {
                        uint newSlot;
                        string oldId = TrCatalog.GetMoveableName(TRVersion.Game.TR4, sequence.Key.TypeId);
                        string newId = TrCatalog.GetMoveableTombEngineSlot(TRVersion.Game.TR4, sequence.Key.TypeId);

                        if (string.IsNullOrEmpty(newId))
                        {
                            newSlot = remappedObjectIndex;
                            remappedObjectIndex++;
                        }
                        else
                        {
                            uint? found = TrCatalog.GetItemIndex(TRVersion.Game.TombEngine, newId, out isMoveable);
                            if (!found.HasValue)
                            {
                                continue;
                            }
                            else
                            {
                                newSlot = found.Value;
                            }
                        }

                        string newSlotName = TrCatalog.GetSpriteSequenceName(TRVersion.Game.TombEngine, newSlot);

                        progressReporter.ReportInfo(oldId + ": → [" + newSlot + "] " + newSlotName);

                        if (newSlotName == "DEFAULT_SPRITES")
                        {
                            progressReporter.ReportInfo("    DEFAULT_SPRITES found, adding new sprites slots from a reference Wad2");

                            // Open TombEngine sprites Wad2
                            foreach (KeyValuePair<WadSpriteSequenceId, WadSpriteSequence> spr in referenceWad.SpriteSequences)
                            {
                                if (!newWad.Contains(spr.Key))
                                {
                                    progressReporter.ReportInfo("    Adding " + TrCatalog.GetSpriteSequenceName(TRVersion.Game.TombEngine, spr.Key.TypeId));
                                    newWad.Add(spr.Key, spr.Value);
                                }
                            }
                        }
                        else
                        {
                            newWad.Add(new WadSpriteSequenceId(newSlot), sequence.Value);
                        }
                    }

                    // Copy all sounds
                    newWad.Sounds = wad.Sounds;

                    // Save the Wad2 
                    newFileName = Path.GetFileNameWithoutExtension(wad.FileName);
                    newPath = Path.Combine(
                        Path.GetDirectoryName(source),
                        Path.GetFileNameWithoutExtension(wadRef.Path) + "_TombEngine.wad2");

                    progressReporter.ReportInfo("Saving " + wadRef.Path + " to " + newPath);

                    Wad2Writer.SaveToFile(newWad, newPath);

                    newWads.Add(new ReferencedWad(
                        level.Settings,
                        level.Settings.MakeRelative(newPath, VariableType.LevelDirectory)
                        ));
                }

                level.Settings.Wads.Clear();
                level.Settings.Wads.AddRange(newWads);

                foreach (Room room in level.Rooms)
                {
                    if (room != null)
                    {
                        foreach (PositionBasedObjectInstance instance in room.Objects)
                        {
                            if (instance is MoveableInstance)
                            {
                                if (!remappedSlots.ContainsKey(((MoveableInstance)instance).WadObjectId.TypeId))
                                {
                                    progressReporter.ReportWarn("Slot not found! " + ((MoveableInstance)instance).WadObjectId.TypeId);
                                }
                                else
                                {
                                    ((MoveableInstance)instance).WadObjectId = new WadMoveableId(remappedSlots[((MoveableInstance)instance).WadObjectId.TypeId]);
                                }
                            }
                        }
                    }
                }

                level.Settings.GameVersion = TRVersion.Game.TombEngine;

                newPath = Path.Combine(
                    Path.GetDirectoryName(source),
                    Path.GetFileNameWithoutExtension(source) + "_TombEngine.prj2");

                Prj2Writer.SaveToPrj2(newPath, level);

                return newPath;
            }
            catch (Exception ex)
            {
                progressReporter.ReportWarn(ex.Message);
                return string.Empty;
            }
        }
    }
}
