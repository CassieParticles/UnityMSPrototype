using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarchingSquaresTool.Scripts
{
    [RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
    public class MeshBuilder: MonoBehaviour, IBuildTriangles
    {
        private MeshFilter meshFilter;

        private List<Vector3> vertices;
        private List<int> indices;
        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            
            vertices = new List<Vector3>();
            indices = new List<int>();
        }

        public void AddTriangle(Triangle triangle, Vector2 cellPosition)
        {
            //Lazy approach, may need to be replaced
            vertices.Add(triangle.A);
            vertices.Add(triangle.B);
            vertices.Add(triangle.C);
            
            indices.Add(indices.Count);
            indices.Add(indices.Count);
            indices.Add(indices.Count);
        }
        
        public void Build()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);

            meshFilter.mesh = mesh;
        }
    }
}