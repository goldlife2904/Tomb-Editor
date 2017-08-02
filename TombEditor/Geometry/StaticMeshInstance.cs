﻿using TombLib.Graphics;

namespace TombEditor.Geometry
{
    public class StaticMeshInstance : ItemInstance
    {
        public StaticModel Model { get; set; }
        public System.Drawing.Color Color { get; set; } = System.Drawing.Color.FromArgb(255, 128, 128, 128);

        public StaticMeshInstance(int id, Room room)
            : base(id, room)
        { }

        public override ObjectInstanceType Type
        {
            get { return ObjectInstanceType.StaticMesh; }
        }

        public override ItemType ItemType
        {
            get { return new ItemType(true, ObjectId); }
        }

        public override ObjectInstance Clone()
        {
            return new StaticMeshInstance(0, Room)
            {
                X = X,
                Y = Y,
                Z = Z,
                Ocb = Ocb,
                Rotation = Rotation,
                Invisible = Invisible,
                ClearBody = ClearBody,
                Bits =
                {
                    [0] = Bits[0],
                    [1] = Bits[1],
                    [2] = Bits[2],
                    [3] = Bits[3],
                    [4] = Bits[4]
                },
                Model = Model,
                ObjectId = ObjectId,
                Color = Color
            };
        }

        public override string ToString()
        {
            return "Static " + ObjectNames.GetStaticName((int)Model.ObjectID) +
                ", ID = " + Id +
                ", Room = " + Room.ToString() +
                ", X = " + Position.X +
                ", Y = " + Position.Y +
                ", Z = " + Position.Z;
        }
    }
}
