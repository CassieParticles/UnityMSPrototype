using Core;
using MarchingSquaresTool.PrototypeD.Generator;
using UnityEngine;

namespace Generator
{
    public abstract class ATriangleBuilder: MonoBehaviour
    {
        public  abstract void AddTriangle(Triangle triangle, Vector2Int cellPosition, bool solid);
        public  abstract void Build();
        public  abstract void Clear();
        
        public  abstract bool IsEditor();
        public  abstract bool IsGame();

        protected void OnValidate()
        {
            GetComponent<MSGenerator>().InitializeComponents(true);
        }
    }
}