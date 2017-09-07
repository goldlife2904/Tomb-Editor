﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TombLib.IO;

namespace TombLib.Wad
{
    public partial class Wad2
    {
        private static byte[] _magicWord = new byte[] { 0x57, 0x41, 0x44, 0x32 };
        
        public static Wad2 LoadFromStream(Stream stream)
        {
            throw new NotImplementedException();
        }

        public static bool SaveToStream(Wad2 wad, Stream stream)
        {
            ushort chunkMagicWord;
            uint chunkSize;

            var texturesList = new List<WadTexture>();
            for (int i = 0; i < wad.Textures.Count; i++)
            {
                var texture = wad.Textures.ElementAt(i).Value;
                texturesList.Add(texture); 
            }

            var meshesList = new List<WadMesh>();
            for (int i = 0; i < wad.Meshes.Count; i++)
            {
                var mesh = wad.Meshes.ElementAt(i).Value;
                meshesList.Add(mesh);
            }

            using (var writer = new BinaryWriterEx(stream))
            {
                // Write magic word
                writer.Write(_magicWord);

                // Store number of textures
                uint numTextures = (uint)wad.Textures.Count;
                writer.Write(numTextures);

                // Write textures
                for (int i = 0; i < numTextures; i++)
                {
                    var texture = wad.Textures.ElementAt(i).Value;

                    writer.Write(texture.Width);
                    writer.Write(texture.Height);
                    writer.Write(texture.Image.ToByteArray());

                    // No more data, in future we can expand the structure using chunks
                    chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                    writer.Write(chunkMagicWord);
                }

                // Store number of meshes
                uint numMeshes = (uint)wad.Meshes.Count;
                writer.Write(numMeshes);

                // Write meshes
                for (int i = 0; i < numMeshes; i++)
                {
                    var mesh = wad.Meshes.ElementAt(i).Value;

                    writer.Write(mesh.BoundingSphere.Center);
                    writer.Write(mesh.BoundingSphere.Radius);

                    uint numVertices = (uint)mesh.VerticesPositions.Count;
                    foreach (var position in mesh.VerticesPositions)
                    {
                        writer.Write(position);
                    }

                    // Has normals or shades?
                    var hasNormalsOrShades = (mesh.VerticesNormals.Count != 0 ? WadMeshNormalsOrShades.Normals :
                                                                                WadMeshNormalsOrShades.Shades);
                    writer.Write((ushort)hasNormalsOrShades);

                    if (hasNormalsOrShades == WadMeshNormalsOrShades.Normals)
                    {
                        foreach (var normal in mesh.VerticesNormals)
                        {
                            writer.Write(normal);
                        }
                    }
                    else
                    {
                        foreach (var shade in mesh.VerticesShades)
                        {
                            writer.Write(shade);
                        }
                    }

                    // Store number of polygons
                    uint numPolygons = (uint)mesh.Polys.Count;
                    foreach (var poly in mesh.Polys)
                    {
                        writer.Write((ushort)poly.Shape);

                        // Write indices
                        writer.Write(poly.Indices[0]);
                        writer.Write(poly.Indices[1]);
                        writer.Write(poly.Indices[2]);
                        if (poly.Shape == WadPolygonShape.Rectangle) writer.Write(poly.Indices[3]);

                        // Write UVs
                        writer.Write(poly.UV[0]);
                        writer.Write(poly.UV[1]);
                        writer.Write(poly.UV[2]);
                        if (poly.Shape == WadPolygonShape.Rectangle) writer.Write(poly.UV[3]);

                        // Store index of texture
                        uint textureIndex = (uint)texturesList.IndexOf(poly.Texture);

                        // Attributes
                        writer.Write(poly.ShineStrength);
                        writer.Write(poly.Transparent);
                        writer.Write(poly.Attributes);

                        // No more data, in future we can expand the structure using chunks
                        chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                        writer.Write(chunkMagicWord);
                    }

                    // No more data, in future we can expand the structure using chunks
                    chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                    writer.Write(chunkMagicWord);
                }

                // Store number of moveables
                uint numMoveables = (uint)wad.Moveables.Count;
                writer.Write(numMoveables);

                for (int i = 0; i < numMoveables; i++)
                {
                    var moveable = wad.Moveables.ElementAt(i).Value;

                    // Store meshes
                    uint numMeshesInThisMoveable = (uint)moveable.Meshes.Count;
                    writer.Write(numMeshesInThisMoveable);

                    foreach (var mesh in moveable.Meshes)
                    {
                        uint meshIndex = (uint)meshesList.IndexOf(mesh);
                        writer.Write(meshIndex);
                    }

                    // Store links
                    foreach (var link in moveable.Links)
                    {
                        writer.Write((ushort)link.Opcode);
                        writer.Write(link.Offset);

                        // No more data, in future we can expand the structure using chunks
                        chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                        writer.Write(chunkMagicWord);
                    }

                    // Store offset
                    writer.Write(moveable.Offset);

                    // Store animations
                    uint numAnimations = (uint)moveable.Animations.Count;
                    writer.Write(numAnimations);

                    foreach (var animation in moveable.Animations)
                    {
                        writer.Write(animation.FrameDuration);
                        writer.Write(animation.StateId);
                        writer.Write(animation.Speed);
                        writer.Write(animation.Acceleration);
                        writer.Write(animation.LateralSpeed);
                        writer.Write(animation.LateralAcceleration);
                        writer.Write(animation.NextAnimation);
                        writer.Write(animation.NextFrame);
                        writer.Write(animation.FrameStart);
                        writer.Write(animation.FrameEnd);

                        // Write keyframes
                        uint numKeyframes = (uint)animation.KeyFrames.Count;
                        writer.Write(numKeyframes);

                        foreach (var keyframe in animation.KeyFrames)
                        {
                            writer.Write(keyframe.BoundingBox);
                            writer.Write(keyframe.Offset);

                            foreach (var angle in keyframe.Angles)
                            {
                                writer.Write((ushort)angle.Axis);
                                writer.Write((ushort)angle.X);
                                writer.Write((ushort)angle.Y);
                                writer.Write((ushort)angle.Z);
                            }

                            // No more data, in future we can expand the structure using chunks
                            chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                            writer.Write(chunkMagicWord);
                        }

                        // Write state changes
                        uint numStateChanges = (uint)animation.StateChanges.Count;
                        writer.Write(numStateChanges);

                        foreach (var stateChange in animation.StateChanges)
                        {
                            writer.Write(stateChange.StateId);
                            writer.Write(stateChange.NumDispatches);

                            // Write dispatches
                            foreach (var dispatch in stateChange.Dispatches)
                            {
                                writer.Write(dispatch.InFrame);
                                writer.Write(dispatch.OutFrame);
                                writer.Write(dispatch.NextAnimation);
                                writer.Write(dispatch.NextFrame);

                                // No more data, in future we can expand the structure using chunks
                                chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                                writer.Write(chunkMagicWord);
                            }

                            // No more data, in future we can expand the structure using chunks
                            chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                            writer.Write(chunkMagicWord);
                        }

                        // Write anim commands
                        uint numAnimCommands = (uint)animation.AnimCommands.Count;
                        writer.Write(numAnimCommands);

                        foreach (var animCommands in animation.AnimCommands)
                        {
                            writer.Write((ushort)animCommands.Type);
                            writer.Write(animCommands.Parameter1);
                            writer.Write(animCommands.Parameter2);
                            writer.Write(animCommands.Parameter3);

                            // No more data, in future we can expand the structure using chunks
                            chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                            writer.Write(chunkMagicWord);
                        }

                        // No more data, in future we can expand the structure using chunks
                        chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                        writer.Write(chunkMagicWord);
                    }
                }

                // Store number of static meshes
                uint numStaticMeshes = (uint)wad.Statics.Count;
                writer.Write(numStaticMeshes);

                for (int i = 0; i < numStaticMeshes; i++)
                {
                    var staticMesh = wad.Statics.ElementAt(i).Value;

                    writer.Write(staticMesh.ObjectID);
                    writer.Write(staticMesh.VisibilityBox);
                    writer.Write(staticMesh.CollisionBox);
                    writer.Write(staticMesh.Flags);

                    uint meshIndex = (uint)meshesList.IndexOf(staticMesh.Mesh);
                    writer.Write(meshIndex);

                    // No more data, in future we can expand the structure using chunks
                    chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                    writer.Write(chunkMagicWord);
                }

                byte[] soundsMagicWord = new byte[] { 0x53, 0x4F, 0x55, 0x4E, 0x44, 0x53 };
                writer.Write(soundsMagicWord);

                // Write sounds
                uint numSounds = (uint)wad.SoundInfo.Count;
                writer.Write(numSounds);

                for (int i = 0; i < wad.SoundInfo.Count; i++)
                {
                    var sound = wad.SoundInfo.ElementAt(i).Value;
                    uint soundId = wad.SoundInfo.ElementAt(i).Key;

                    writer.Write(soundId);
                    writer.Write(sound.Volume);
                    writer.Write(sound.Range);
                    writer.Write(sound.Pitch);
                    writer.Write(sound.Loop);
                    writer.Write(sound.FlagN);
                    writer.Write(sound.RandomizeGain);
                    writer.Write(sound.RandomizePitch);

                    uint numWaves = (uint)sound.WaveSounds.Count;
                    writer.Write(numWaves);

                    foreach (var wave in sound.WaveSounds)
                    {
                        uint waveSize = (uint)wave.WaveData.Length;
                        writer.Write(waveSize);
                        writer.Write(wave.WaveData);
                    }

                    // No more data, in future we can expand the structure using chunks
                    chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                    writer.Write(chunkMagicWord);
                }

                byte[] spritesMagicWord = new byte[] { 0x53, 0x50, 0x52, 0x49, 0x54, 0x45, 0x53 };
                writer.Write(spritesMagicWord);

                // Write sprites
                var spritesList = new List<WadTexture>();

                uint numSpritesTextures = (uint)wad.SpriteTextures.Count;
                writer.Write(numSpritesTextures);

                for (int i = 0; i < wad.SpriteTextures.Count; i++)
                {
                    var texture = wad.SpriteTextures.ElementAt(i).Value;

                    writer.Write(texture.Width);
                    writer.Write(texture.Height);
                    writer.Write(texture.Image.ToByteArray());

                    // No more data, in future we can expand the structure using chunks
                    chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                    writer.Write(chunkMagicWord);

                    spritesList.Add(texture);
                }

                uint numSpritesSequences = (uint)wad.SpriteSequences.Count;
                writer.Write(numSpritesSequences);

                foreach (var sequence in wad.SpriteSequences)
                {
                    writer.Write(sequence.ObjectID);

                    uint numSprites = (uint)sequence.Sprites.Count;
                    writer.Write(numSprites);

                    foreach (var sprite in sequence.Sprites)
                    {
                        uint spriteIndex = (uint)spritesList.IndexOf(sprite);
                        writer.Write(spriteIndex);
                    }

                    // No more data, in future we can expand the structure using chunks
                    chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                    writer.Write(chunkMagicWord);
                }

                // No more data, in future we can expand the structure using chunks
                chunkMagicWord = (ushort)WadChunkType.NoExtraChunk;
                writer.Write(chunkMagicWord);
            }

            return true;
        }
    }
}
