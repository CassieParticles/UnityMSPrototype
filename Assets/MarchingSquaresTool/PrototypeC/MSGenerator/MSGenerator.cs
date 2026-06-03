using System;
using System.Runtime.CompilerServices;
using MarchingSquaresTool.PrototypeC.Core;
using MarchingSquaresTool.PrototypeC.FileLoader;
using MarchingSquaresTool.PrototypeC.MSGenerator.EdgeComp;
using MarchingSquaresTool.PrototypeC.MSGenerator.TriangleComp;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.MSGenerator
{
    public class MSGenerator : MonoBehaviour
    {
        private BodyGrid _grid;
        
        private IBuildTriangles[] _triangleBuilders;
        private IBuildEdges[] _edgeBuilders;

        //Level editing
        private void OnValidate()
        {
            _triangleBuilders = GetComponents<IBuildTriangles>();
            _edgeBuilders = GetComponents<IBuildEdges>();

            _grid = new BodyGrid();
        }
        
        public void SaveLevel(string name)
        {
            LevelFileHandler.StoreLevel(_grid, name);
        }

        public void LoadLevel(string name)
        {
            _grid = LevelFileHandler.LoadLevel(name);
        }
    }
}