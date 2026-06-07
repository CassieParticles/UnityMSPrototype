using System;
using System.Collections.Generic;
using MarchingSquaresTool.PrototypeC.Core;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.MSGenerator.TriangleComp
{
    [RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
    public class MeshBuilder: MonoBehaviour, IBuildTriangles
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

        public void AddTriangle(Triangle triangle, Vector2Int cellPosition)
        {
            //Lazy approach, may need to be replaced
            _vertices.Add(triangle.A);
            _vertices.Add(triangle.B);
            _vertices.Add(triangle.C);
            
            _indices.Add(_indices.Count);
            _indices.Add(_indices.Count);
            _indices.Add(_indices.Count);
        }

        public void SetSolid(bool solid)
        {
            
        }

        public void Build()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = _vertices.ToArray();
            mesh.SetIndices(_indices.ToArray(), MeshTopology.Triangles, 0);

            _meshFilter.mesh = mesh;
        }

        public void Clear()
        {
            _meshFilter.mesh = null;
            _vertices.Clear();
            _indices.Clear();
        }
    }
}