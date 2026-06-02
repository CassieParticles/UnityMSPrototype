using System;
using System.Collections.Generic;

namespace MarchingSquaresTool.PrototypeC.Core
{
    public struct Cell
    {
        public Cell(float terrain = -1.0f, bool solid = true)
        {
            this.Terrain = terrain;
            this.Solid = solid;
        }

        public bool Equals(Cell other)
        {
            return Terrain.Equals(other.Terrain) && Solid == other.Solid;
        }

        public override bool Equals(object obj)
        {
            return obj is Cell other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Terrain, Solid);
        }

        public float Terrain;
        public bool Solid;
    };
}