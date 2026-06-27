using MarchingSquaresTool.PrototypeD.Core;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.Generator
{
    public interface IBuildTriangles
    {
        public void AddTriangle(Triangle triangle, Vector2Int cellPosition);
        public void SetSolid(bool solid);
        public void Build();
        public void Clear();
    }
}