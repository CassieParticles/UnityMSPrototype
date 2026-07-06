using Core;
using MarchingSquaresTool.PrototypeD.Generator;
using UnityEngine;

namespace Generator
{
    [RequireComponent(typeof(MSGenerator))]
    public abstract class AEdgeBuilder: MonoBehaviour
    {
        public abstract void AddEdge(Edge edge, Vector2Int cellPosition, bool solid);
        public abstract void Build();
        public abstract void Clear();
        public abstract bool IsEditor();
        public abstract bool IsGame();
        
        protected void OnValidate()
        {
            GetComponent<MSGenerator>().InitializeComponents(true);
        }
    }
}