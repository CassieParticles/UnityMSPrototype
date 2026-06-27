using MarchingSquaresTool.PrototypeD.Core;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.Generator
{
    public abstract class ATriangleBuilder: MonoBehaviour
    {
        public  abstract void AddTriangle(Triangle triangle, Vector2Int cellPosition);
        public  abstract void SetSolid(bool solid);
        public  abstract void Build();
        public  abstract void Clear();
        
        public  abstract bool IsEditor();
        public  abstract bool IsGame();
    }
}