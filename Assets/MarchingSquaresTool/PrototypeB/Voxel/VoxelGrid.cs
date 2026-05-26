using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeB.MarchingSquaresTool.PrototypeB.Voxel
{
    public class VoxelGrid
    {
        private float[] grid;
        private Vector2Int size;
        private Vector2Int position;

        private bool resized;
        private Stack<Vector2Int> modifiedCell;
        
        public VoxelGrid(int width = 1, int height = 1)
        {
            grid = new float[width * height];
            size = new Vector2Int(width, height);
            position =  new Vector2Int(0, 0);

            resized = false;
            modifiedCell = new Stack<Vector2Int>();
        }

        public VoxelGrid(float[] data, int width, int height)
        {
            this.grid = data;
            size.x = width;
            size.y = height;
            position =  new Vector2Int(0, 0);
            
            resized = false;
            modifiedCell = new Stack<Vector2Int>();
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

        public void AddBorder(float defaultValue = -1.0f)
        {
            AddRowTop(defaultValue);
            AddRowBottom(defaultValue);
            AddColumnLeft(defaultValue);
            AddColumnRight(defaultValue);
        }

        private void AddRowTop(float defaultValue = -1.0f)
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
            resized = true;
        }

        private void AddRowBottom(float defaultValue = -1.0f)
        {
            float[] newGrid =  new float[size.x * (size.y + 1)];
            Array.Copy(grid, newGrid, grid.Length);
            for(int i=0;i<size.x;++i)
            {
                newGrid[(grid.Length)+i] = defaultValue;
            }
            size.y++;
            grid = newGrid;
            resized = true;
        }

        private void AddColumnLeft(float defaultValue = -1.0f)
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
            resized = true;
        }

        private void AddColumnRight(float defaultValue = -1.0f)
        {
            float[] newGrid = new float[(size.x + 1) * size.y];
            for (int i = 0; i < size.y; ++i)
            {
                Array.Copy(grid,i * size.x,newGrid,i * (size.x + 1),size.x);
                newGrid[(i + 1) * (size.x + 1) - 1] = defaultValue; 
            }
            size.x++;
            grid = newGrid;
            resized = true;
        }

        private int GetIndex(int x, int y)
        {
            return (y - position.y) * size.x + (x - position.x);
        }
        
        public Vector2Int GetSize()
        {
            return size;
        }

        public Vector2Int GetPosition()
        {
            return position;
        }

        public bool GetResized()
        {
            bool resize = resized;
            resized = false;
            return resize;
        }

        public Vector2Int GetModifiedCell()
        {
            if (modifiedCell.Count == 0)
            {
                return Vector2Int.left;
            }

            return modifiedCell.Pop();
        }
    }
}