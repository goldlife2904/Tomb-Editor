﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TombLib.IO;
using TombLib.Utils;

namespace TombLib.LevelData.Compilers.TombEngine
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TombEngine_sprite_texture
    {
        public int Tile;
        public float X1;
        public float Y1;
        public float X2;
        public float Y2;
        public float X3;
        public float Y3;
        public float X4;
        public float Y4;
    }

    public enum TombEngine_polygon_shape : int
    {
        Quad,
        Triangle
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TombEngine_atlas
    {
        public ImageC ColorMap;
        public ImageC NormalMap;
        public bool HasNormalMap;
        public bool CustomNormalMap;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TombEngine_collision_info
    {
        public float SplitAngle;
        public int[] Portals;
        public Vector3[] Planes;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TombEngine_room_sector
    {
        public int FloorDataIndex;
        public int BoxIndex;
        public int StepSound;
        public int Stopper;
        public int RoomBelow;
        public int Floor;
        public int RoomAbove;
        public int Ceiling;
        public TombEngine_collision_info FloorCollision;
        public TombEngine_collision_info CeilingCollision;
        public int WallPortal;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TombEngine_polygon
    {
        public TombEngine_polygon_shape Shape;
        public List<int> Indices = new List<int>();
        public List<int> VerticesIds = new List<int>();
        public List<Vector2> TextureCoordinates = new List<Vector2>();
        public List<Vector3> Normals = new List<Vector3>();
        public List<Vector3> Tangents = new List<Vector3>();
        public List<Vector3> Bitangents = new List<Vector3>();
        public int TextureId;
        public byte BlendMode;
        public bool Animated;
        public Vector3 Normal;
        public Vector3 Tangent;
        public Vector3 Bitangent;
        public int AnimatedSequence;
        public int AnimatedFrame;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TombEngine_room_staticmesh
    {
        public int X;
        public int Y;
        public int Z;
        public ushort Rotation;
        public ushort Flags;
        public Vector4 Color;
        public ushort ObjectID;
        public short HitPoints;
    }

    public class NormalHelper
    {
        public TombEngine_polygon Polygon;
        public bool Smooth;

        public NormalHelper(TombEngine_polygon poly)
        {
            Polygon = poly;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TombEngine_vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoords;
        public Vector3 Color;
        public Vector3 Tangent;
        public Vector3 Bitangent;
        public int Bone;
        public int Effects;
        public int IndexInPoly;
        public int OriginalIndex;

        public List<NormalHelper> Polygons = new List<NormalHelper>();
        public bool IsOnPortal;

        // Custom implementation of these because default implementation is *insanely* slow.
        // Its not just a quite a bit slow, it really is *insanely* *crazy* slow so we need those functions :/
        public static bool operator ==(TombEngine_vertex first, TombEngine_vertex second)
        {
            return first.Position.X == second.Position.X && first.Position.Y == second.Position.Y && first.Position.Z == second.Position.Z;
        }

        public static bool operator !=(TombEngine_vertex first, TombEngine_vertex second)
        {
            return !(first == second);
        }

        public bool Equals(TombEngine_vertex other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TombEngine_vertex))
                return false;
            return this == (TombEngine_vertex)obj;
        }

        public override int GetHashCode()
        {
            return unchecked((int)Position.X + (int)Position.Y * 695504311 + (int)Position.Z * 550048883);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TombEngine_material
    {
        public class TombEngineMaterialComparer : IEqualityComparer<TombEngine_material>
        {
            public bool Equals(TombEngine_material x, TombEngine_material y)
            {
                return (x.Texture == y.Texture && x.BlendMode == y.BlendMode && x.Animated == y.Animated && x.NormalMapping == y.NormalMapping && 
                    x.AnimatedSequence == y.AnimatedSequence);
            }

            public int GetHashCode(TombEngine_material obj)
            {
                unchecked
                {
                    int hash = 17;
                    hash = hash * 23 + obj.Texture.GetHashCode();
                    hash = hash * 23 + obj.BlendMode.GetHashCode();
                    hash = hash * 23 + obj.Animated.GetHashCode();
                    hash = hash * 23 + obj.NormalMapping.GetHashCode();
                    hash = hash * 23 + obj.AnimatedSequence.GetHashCode();
                    return hash;
                }
            }
        }

        public int Texture;
        public byte BlendMode;
        public bool Animated;
        public bool NormalMapping;
        public int AnimatedSequence;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TombEngine_bucket
    {
        public TombEngine_material Material;
        public List<TombEngine_polygon> Polygons;

        public TombEngine_bucket()
        {
            Polygons = new List<TombEngine_polygon>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TombEngine_room
    {
        public tr_room_info Info;
        public int NumDataWords;
        public List<Vector3> Positions = new List<Vector3>();
        public List<Vector3> Normals = new List<Vector3>();
        public List<Vector3> Tangents = new List<Vector3>();
        public List<Vector3> Bitangents = new List<Vector3>();
        public List<Vector3> Colors = new List<Vector3>();
        public List<TombEngine_vertex> Vertices = new List<TombEngine_vertex>();
        public Dictionary<TombEngine_material, TombEngine_bucket> Buckets;
        public List<tr_room_portal> Portals;
        public int NumZSectors;
        public int NumXSectors;
        public TombEngine_room_sector[] Sectors;
        public Vector3 AmbientLight;
        public List<TombEngine_room_light> Lights;
        public List<TombEngine_room_staticmesh> StaticMeshes;
        public int AlternateRoom;
        public int Flags;
        public int WaterScheme;
        public int ReverbInfo;
        public int AlternateGroup;

        // Helper data
        public List<TombEngine_polygon> Polygons;
        public TrSectorAux[,] AuxSectors;
        public AlternateKind AlternateKind;
        public List<Room> ReachableRooms;
        public bool Visited;
        public bool Flipped;
        public Room FlippedRoom;
        public Room BaseRoom;
        public Room OriginalRoom;

        public void Write(BinaryWriterEx writer)
        {
            writer.WriteBlock(Info);

            writer.Write(Positions.Count);
            foreach (var p in Positions)
                writer.Write(p);
            foreach (var n in Normals)
                writer.Write(n);
            foreach (var c in Colors)
                writer.Write(c);

            writer.Write(Buckets.Count);
            foreach (var bucket in Buckets.Values)
            {
                writer.Write(bucket.Material.Texture);
                writer.Write(bucket.Material.BlendMode);
                writer.Write(bucket.Material.Animated);
                writer.Write(bucket.Polygons.Count);
                foreach (var poly in bucket.Polygons)
                {
                    writer.Write((int)poly.Shape);
                    writer.Write((int)poly.AnimatedSequence);
                    writer.Write((int)poly.AnimatedFrame);
                    foreach (int index in poly.Indices)
                        writer.Write(index);
                    foreach (var uv in poly.TextureCoordinates)
                        writer.Write(uv);
                    foreach (var n in poly.Normals)
                        writer.Write(n);
                    foreach (var t in poly.Tangents)
                        writer.Write(t);
                    foreach (var bt in poly.Bitangents)
                        writer.Write(bt);
                }
            }

            // Write portals
            writer.WriteBlock(Portals.Count);
            if (Portals.Count != 0)
                writer.WriteBlockArray(Portals);

            // Write sectors
            writer.Write(NumZSectors);
            writer.Write(NumXSectors);
            foreach (var s in Sectors)
            {
                writer.Write(s.FloorDataIndex);
                writer.Write(s.BoxIndex);
                writer.Write(s.StepSound);
                writer.Write(s.Stopper);
                writer.Write(s.RoomBelow);
                writer.Write(s.Floor);
                writer.Write(s.RoomAbove);
                writer.Write(s.Ceiling);
                writer.Write(s.FloorCollision.SplitAngle);
                writer.Write(s.FloorCollision.Portals[0]);
                writer.Write(s.FloorCollision.Portals[1]);
                writer.Write(s.FloorCollision.Planes[0]);
                writer.Write(s.FloorCollision.Planes[1]);
                writer.Write(s.CeilingCollision.SplitAngle);
                writer.Write(s.CeilingCollision.Portals[0]);
                writer.Write(s.CeilingCollision.Portals[1]);
                writer.Write(s.CeilingCollision.Planes[0]);
                writer.Write(s.CeilingCollision.Planes[1]);
                writer.Write(s.WallPortal);
            }

            // Write room color
            writer.Write(AmbientLight.X);
            writer.Write(AmbientLight.Y);
            writer.Write(AmbientLight.Z);

            // Write lights
            writer.WriteBlock(Lights.Count);
            foreach (var light in Lights)
            {
                writer.Write((int)light.Position.X);
                writer.Write((int)light.Position.Y);
                writer.Write((int)light.Position.Z);
                writer.Write(light.Direction.X);
                writer.Write(light.Direction.Y);
                writer.Write(light.Direction.Z);
                writer.Write(light.Color.X);
                writer.Write(light.Color.Y);
                writer.Write(light.Color.Z);
                writer.Write(light.Intensity);
                writer.Write((float)(light.LightType == 2 ? Math.Acos(light.In) * 2.0f : light.In));
                writer.Write((float)(light.LightType == 2 ? Math.Acos(light.Out) * 2.0f : light.Out));
                writer.Write(light.Length);
                writer.Write(light.CutOff);
                writer.Write(light.LightType);
                writer.Write((byte)(light.CastShadows ? 1 : 0));
            }

            // Write static meshes
            writer.WriteBlock(StaticMeshes.Count);
            if (StaticMeshes.Count != 0)
                writer.WriteBlockArray(StaticMeshes);

            // Write final data
            writer.Write(AlternateRoom);
            writer.Write(Flags);
            writer.Write(WaterScheme);
            writer.Write(ReverbInfo);
            writer.Write(AlternateGroup);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TombEngine_room_light
    {
        public VectorInt3 Position;
        public Vector3 Direction;
        public Vector3 Color;
        public float Intensity;
        public float In;
        public float Out;
        public float Length;
        public float CutOff;
        public byte LightType;
        public bool CastShadows;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TombEngine_mesh
    {
        public BoundingSphere Sphere;
        public List<Vector3> Positions = new List<Vector3>();
        public List<Vector3> Normals = new List<Vector3>();
        public List<Vector3> Colors = new List<Vector3>();
        public List<int> Bones = new List<int>();
        public List<TombEngine_polygon> Polygons = new List<TombEngine_polygon>();
        public Dictionary<TombEngine_material, TombEngine_bucket> Buckets = new Dictionary<TombEngine_material, TombEngine_bucket>();
        public List<TombEngine_vertex> Vertices = new List<TombEngine_vertex>();
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TombEngine_box
    {
        public int Zmin;
        public int Zmax;
        public int Xmin;
        public int Xmax;
        public int TrueFloor;
        public int OverlapIndex;
        public int Flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TombEngine_overlap
    {
        public int Box;
        public int Flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TombEngine_zone
    {
        public int GroundZone1_Normal;
        public int GroundZone2_Normal;
        public int GroundZone3_Normal;
        public int GroundZone4_Normal;
        public int GroundZone5_Normal;
        public int FlyZone_Normal;
        public int GroundZone1_Alternate;
        public int GroundZone2_Alternate;
        public int GroundZone3_Alternate;
        public int GroundZone4_Alternate;
        public int GroundZone5_Alternate;
        public int FlyZone_Alternate;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TombEngine_camera
    {
        public int X;
        public int Y;
        public int Z;
        public int Room;
        public int Flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TombEngine_sound_source
    {
        public int X;
        public int Y;
        public int Z;
        public int SoundID;
        public int Flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TombEngine_staticmesh
    {
        public int ObjectID;
        public int Mesh;
        public TombEngine_bounding_box VisibilityBox;
        public TombEngine_bounding_box CollisionBox;
        public ushort Flags;
        public short ShatterType;
        public short ShatterDamage;
        public short ShatterSound;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TombEngine_moveable
    {
        public int ObjectID;
        public short NumMeshes;
        public short StartingMesh;
        public int MeshTree;
        public int FrameOffset;
        public short Animation;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TombEngine_animation
    {
        public int FrameOffset;
        public short FrameRate;
        public ushort StateID;
        public int Speed;
        public int Accel;
        public int SpeedLateral;
        public int AccelLateral;
        public ushort FrameStart;
        public ushort FrameEnd;
        public ushort NextAnimation;
        public ushort NextFrame;
        public ushort NumStateChanges;
        public ushort StateChangeOffset;
        public ushort NumAnimCommands;
        public ushort AnimCommand;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TombEngine_keyframe
    {
        public TombEngine_bounding_box BoundingBox;
        public Vector3 Offset;
        public List<Quaternion> Angles;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TombEngine_bounding_box
    {
        public short X1;
        public short X2;
        public short Y1;
        public short Y2;
        public short Z1;
        public short Z2;
    }
}
