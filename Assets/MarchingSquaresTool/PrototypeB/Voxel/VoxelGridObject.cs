using System;
using UnityEngine;

namespace PrototypeB.MarchingSquaresTool.PrototypeB.Voxel
{
    [CreateAssetMenu(fileName = "VoxelGrid", menuName = "MarchingSquares/VoxelGrid", order = 0)]
    public class VoxelGridObject : ScriptableObject
    {
        private float[] grid;        
        private Vector2Int size;
        private Vector2Int position;

        public Vector2Int Size
        {
            get => size;
        }

        public Vector2Int Position
        {
            get => position;
        }


        private void Awake()
        {
            //Initialize the grid
            size = Vector2Int.one;
            position = Vector2Int.zero;
            grid = new float[]{-1.0f};
        }

        public float this[Vector2Int pos]
        {
            get=>this[pos.x, pos.y];
            set=>this[pos.x, pos.y] = value;
        }

        public float this[int x, int y]
        {
            get => grid[GetIndex(x, y)];
            set
            {
                //If position is off grid, expand the grid
                while (x <= position.x)
                {
                    AddColumnLeft();
                }
                while (x >= position.x + size.x)
                {
                    AddColumnRight();
                }
                while (y <= position.y)
                {
                    AddRowTop();
                }
                while (y >= position.y + size.y)
                {
                    AddRowBottom();
                }

                //Set the value
                grid[GetIndex(x, y)] = value;   
            }
        }
        
        private int GetIndex(int x, int y)
        {
            return (y - position.y) * size.x + (x - position.x);
        }
        
        
        //RESIZING
        public void AddBorder(float defaultValue = -1.0f)
        {
            AddRowTop(defaultValue);
            AddRowBottom(defaultValue);
            AddColumnLeft(defaultValue);
            AddColumnRight(defaultValue);
        }
        
        public void AddRowTop(float defaultValue = -1.0f)
        {
            //Add new row
            float[] newGrid =  new float[size.x * (size.y + 1)];
            for (int i = 0; i < size.x; ++i)
            {
                newGrid[i] = defaultValue;
            }
            //Copy old array
            Array.Copy(grid, 0, newGrid, size.x, grid.Length);
            size.y++;
            position.y--;
            grid = newGrid;
        }

        public void AddRowBottom(float defaultValue = -1.0f)
        {
            float[] newGrid =  new float[size.x * (size.y + 1)];
            Array.Copy(grid, newGrid, grid.Length);
            for(int i=0;i<size.x;++i)
            {
                newGrid[(grid.Length)+i] = defaultValue;
            }
            size.y++;
            grid = newGrid;
        }

        public void AddColumnLeft(float defaultValue = -1.0f)
        {
            float[] newGrid = new float[(size.x + 1) * size.y];
            for (int i = 0; i < size.y; ++i)
            {
                newGrid[i * (size.x + 1)] = defaultValue;
                Array.Copy(grid,i * size.x,newGrid,i * (size.x + 1) + 1, size.x);
            }
            grid = newGrid;
            size.x++;
            position.x--;
        }

        public void AddColumnRight(float defaultValue = -1.0f)
        {
            float[] newGrid = new float[(size.x + 1) * size.y];
            for (int i = 0; i < size.y; ++i)
            {
                Array.Copy(grid,i * size.x,newGrid,i * (size.x + 1),size.x);
                newGrid[(i + 1) * (size.x + 1) - 1] = defaultValue; 
            }
            size.x++;
            grid = newGrid;
        }
    }
}