using MarchingSquaresTool.PrototypeC.Core;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.MSGenerator.TriangleComp
{
    public interface IBuildTriangles
    {
        public void AddTriangle(Triangle triangle, Vector2Int cellPosition);
        public void SetSolid(bool solid);
        public void Build();
    }
}