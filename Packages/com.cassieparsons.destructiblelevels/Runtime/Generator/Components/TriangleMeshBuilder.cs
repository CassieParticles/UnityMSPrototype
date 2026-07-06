using System.Collections.Generic;
using Core;
using Generator;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.Generator.Components
{
    [RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
    public class TriangleMeshBuilder: ATriangleBuilder
    {
        private MeshFilter _meshFilter;

        private List<Vector3> _vertices;
        private List<int> _indices;
        private List<Color> _colors;

        [SerializeField]
        private Color solidColour;
        [SerializeField]
        private Color destructibleColour;

        protected new void OnValidate()
        {
            base.OnValidate();
            
            _meshFilter = GetComponent<MeshFilter>();
            _vertices = new List<Vector3>();
            _indices = new List<int>();
            _colors = new List<Color>();
        }

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _vertices = new List<Vector3>();
            _indices = new List<int>();
            _colors = new List<Color>();
        }

        public TriangleMeshBuilder SetSolidColour(Color color)
        {
            solidColour = color;
            return this;
        }

        public TriangleMeshBuilder SetDestructibleColour(Color color)
        {
            destructibleColour = color;
            return this;
        }

        public override void AddTriangle(Triangle triangle, Vector2Int cellPosition, bool solid)
        {
            //Lazy approach, may need to be replaced
            _vertices.Add(triangle.A);
            _vertices.Add(triangle.B);
            _vertices.Add(triangle.C);
            
            _indices.Add(_indices.Count);
            _indices.Add(_indices.Count);
            _indices.Add(_indices.Count);
            
            _colors.Add(solid ? solidColour : destructibleColour);
            _colors.Add(solid ? solidColour : destructibleColour);
            _colors.Add(solid ? solidColour : destructibleColour);
        }
        

        public override void Build()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = _vertices.ToArray();
            mesh.SetIndices(_indices.ToArray(), MeshTopology.Triangles, 0);
            mesh.SetColors(_colors.ToArray());

            _meshFilter.mesh = mesh;
        }

        public override void Clear()
        {
            _meshFilter.mesh = null;
            _vertices.Clear();
            _indices.Clear();
            _colors.Clear();
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