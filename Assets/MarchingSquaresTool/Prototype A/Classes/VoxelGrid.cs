using System;
using UnityEngine;

namespace MarchingSquaresTool.Scripts
{
    public class VoxelGrid
    {
                
        private float[] grid;
        private Vector2Int size;
        private Vector2Int position;
        public VoxelGrid(int width = 1, int height = 1)
        {
            grid = new float[width * height];
            size = new Vector2Int(width, height);
            position =  new Vector2Int(0, 0);
        }

        public VoxelGrid(float[] data, int width, int height)
        {
            this.grid = data;
            size.x = width;
            size.y = height;
            position =  new Vector2Int(0, 0);
        }

        public float this[int i]
        {
            get => grid[i];
            set => grid[i] = value;
        }

        public float this[int x, int y]
        {
            get => grid[(y) * size.x + (x)];
            set => grid[(y) * size.x + (x)] = value;
        }

        public void AddBorder(float defaultValue = 0)
        {
            AddRowTop(defaultValue);
            AddRowBottom(defaultValue);
            AddColumnLeft(defaultValue);
            AddColumnRight(defaultValue);
        }

        private void AddRowTop(float defaultValue = 0)
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

        private void AddRowBottom(float defaultValue = 0)
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

        private void AddColumnLeft(float defaultValue = 0)
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

        private void AddColumnRight(float defaultValue = 0)
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
        
        public Vector2Int GetSize()
        {
            return size;
        }

        public Vector2Int GetPosition()
        {
            return position;
        }



    }
}