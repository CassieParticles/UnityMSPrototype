using System;
using PrototypeB.MarchingSquaresTool.PrototypeB.Voxel;
using UnityEngine;

namespace PrototypeB.MarchingSquaresTool.PrototypeB.Components
{
    public class VoxelComponent : MonoBehaviour
    {
        [SerializeField]
        private VoxelGridObject grid;

        public VoxelGridObject Grid
        {
            get
            {
                return grid;
            }
        }
        
        
    }
}
