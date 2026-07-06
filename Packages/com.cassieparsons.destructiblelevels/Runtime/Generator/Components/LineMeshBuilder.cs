using System.Collections.Generic;
using Core;
using Generator;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.Generator.Components
{
    [RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
    public class LineMeshBuilder : AEdgeBuilder
    {
        private MeshFilter _meshFilter;

        private List<Vector3> _vertices;
        private List<int> _indices;
        
        protected new void OnValidate()
        {
            base.OnValidate();
            
            _meshFilter = GetComponent<MeshFilter>();
            _vertices = new List<Vector3>();
            _indices = new List<int>();
        }

        protected void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _vertices = new List<Vector3>();
            _indices = new List<int>();
        }
        
        public override void AddEdge(Edge edge, Vector2Int cellPosition, bool solid)
        {
            _vertices.Add(edge.A);
            _vertices.Add(edge.B);
            
            _indices.Add(_indices.Count);
            _indices.Add(_indices.Count);
        }
   
        public override void Build()
        {
            Mesh mesh = new Mesh();
            mesh.SetVertices(_vertices);
            mesh.SetIndices(_indices.ToArray(), MeshTopology.Lines, 0);
            
            _meshFilter.mesh = mesh;
        }
        public override void Clear()
        {
            _meshFilter.mesh = null;
            
            _vertices.Clear();
            _indices.Clear();
        }

        public override bool IsEditor()
        {
            return true;
        }
        public override bool IsGame()
        {
            return true;
        }
    }
}