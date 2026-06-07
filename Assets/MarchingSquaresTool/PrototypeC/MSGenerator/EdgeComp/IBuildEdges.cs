using MarchingSquaresTool.PrototypeC.Core;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.MSGenerator.EdgeComp
{
    public interface IBuildEdges
    {
        public void AddEdge(Edge edge, Vector2Int cellPosition);
        public void SetSolid(bool solid);
        public void Build();
        public void Clear();
    }
}