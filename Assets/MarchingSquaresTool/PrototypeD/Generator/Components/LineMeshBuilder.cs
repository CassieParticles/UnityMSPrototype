using System.Collections.Generic;
using MarchingSquaresTool.PrototypeD.Core;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.Generator.Components
{
    [RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
    public class LineMeshBuilder : MonoBehaviour, IBuildEdges
    {
        private MeshFilter _meshFilter;

        private List<Vector3> _vertices;
        private List<int> _indices;
        
        private void OnValidate()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _vertices = new List<Vector3>();
            _indices = new List<int>();
        }

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _vertices = new List<Vector3>();
            _indices = new List<int>();
        }
        
        public void AddEdge(Edge edge, Vector2Int cellPosition)
        {
            _vertices.Add(edge.A);
            _vertices.Add(edge.B);
            
            _indices.Add(_indices.Count);
            _indices.Add(_indices.Count);
        }
        public void SetSolid(bool solid)
        {
            
        }
        public void Build()
        {
            Mesh mesh = new Mesh();
            mesh.SetVertices(_vertices);
            mesh.SetIndices(_indices.ToArray(), MeshTopology.Lines, 0);
            
            _meshFilter.mesh = mesh;
        }
        public void Clear()
        {
            _meshFilter.mesh = null;
            
            _vertices.Clear();
            _indices.Clear();
        }

        public bool IsEditor()
        {
            return true;
        }
        public bool IsGame()
        {
            return true;
        }
    }
}