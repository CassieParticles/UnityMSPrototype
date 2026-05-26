using System;
using UnityEngine;

namespace MarchingSquaresTool.Scripts
{
    public class VoxelGridComp: MonoBehaviour
    {
        public VoxelGrid grid { get; private set; }

        
        
        private void Awake()
        {
            float[] data = {
                -1.0f,-1.0f,-1.0f,-1.0f,-1.0f,-1.0f,
                -1.0f, 1.0f, 1.0f, 1.0f, 1.0f,-1.0f,
                -1.0f, 1.0f,-1.0f,-1.0f, 1.0f,-1.0f,
                -1.0f, 1.0f,-1.0f,-1.0f, 1.0f,-1.0f,
                -1.0f, 1.0f, 1.0f, 1.0f, 1.0f,-1.0f,
                -1.0f,-1.0f,-1.0f,-1.0f,-1.0f,-1.0f,
            };

            grid = new VoxelGrid(data,6,6);
            grid.AddBorder(-1);
        }
    }
}