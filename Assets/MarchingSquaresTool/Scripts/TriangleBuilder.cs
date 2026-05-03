using UnityEngine;

namespace MarchingSquaresTool.Scripts
{
    public struct Triangle
    {
        public Vector2 A;
        public Vector2 B;
        public Vector2 C;
    }

    public struct Edge
    {
        public Vector2 A;
        public Vector2 B;
    }
    
    public interface IBuildTriangles
    {
        public void AddTriangle(Triangle triangle, Vector2 cellPosition);
        public void Build();
    }

    public interface IBuildEdges
    {
        public void AddEdge(Edge edge, Vector2 cellPosition);
        public void Build();
    }
}