using System;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.Core
{
    [Serializable]
    public class BodyGrid
    {
        [HideInInspector]
        [SerializeField]
        public float[] grid;

        [HideInInspector]
        [SerializeField]
        private Vector2Int size;
        //Position refers to the position of the top left corner
        [HideInInspector]
        [SerializeField]
        private Vector2Int position;
        public Vector2Int Size => size;
        public Vector2Int Position => position;

        public BodyGrid()
        {
            this.grid = new float[1]{-1.0f};
            size = Vector2Int.one;
            position = Vector2Int.zero;
        }

        //Used for file handling
        public BodyGrid(Vector2Int size, Vector2Int position)
        {
            this.grid = new float[size.x * size.y];
            this.size = size;
            this.position = position;
        }

        public float this[int x, int y]
        {
            get => grid[GetIndex(x, y)];
            set
            {
                grid[GetIndex(x, y)] = Mathf.Clamp(value,-1.0f,1.0f);
            }
        }

        public float Set(int x, int y, float f)
        {
            ExpandToPosition(x,y);
            return  grid[GetIndex(x, y)] = f;
        }

        public float Get(int x, int y)
        {
            return grid[GetIndex(x, y)];
        }
        
        public float Add(int x, int y, float f)
        {
            ExpandToPosition(x,y);
            return grid[GetIndex(x, y)] = Mathf.Clamp(grid[GetIndex(x, y)] + f,-1.0f,1.0f);
        }

        public float Remove(int x, int y, float f)
        {
            return Add(x, y, -f);
        }



        //Expand the grid size to include the position passed in
        public void ExpandToPosition(int x, int y)
        {
            //expand left to meet point
            while (x < +position.x)
            {
                AddColumnLeft();
            }
            //expand right to meet point
            while (x >= size.x + position.x)
            {
                AddColumnRight();
            }
            //expand up to meet point
            while (y < +position.y)
            {
                AddRowTop();
            }
            //expand down to meet point
            while (y >= size.y + position.y)
            {
                AddRowBottom();
            }
        }

        //Expand grid in all 4 directions, to make a -ve border
        public void AddBorder()
        {
            AddRowTop();
            AddRowBottom();
            AddColumnLeft();
            AddColumnRight();
        }
        
        private void AddRowTop()
        {
            float[] newGrid = new float[size.x * (size.y + 1)];
            //Add new row
            for (int i = 0; i < size.x; ++i)
            {
                newGrid[i] = -1.0f;
            }
            //Copy old array
            Array.Copy(grid,0,newGrid,size.x,grid.Length);

            size.y++;
            position.y--;
            grid = newGrid;
        }

        private void AddRowBottom()
        {
            float[] newGrid = new float[size.x * (size.y + 1)];
            //Copy old array
            Array.Copy(grid,0,newGrid,0,grid.Length);
            //Add new row
            for (int i = 0; i < size.x; ++i)
            {
                newGrid[i + grid.Length] = -1.0f;
            }

            size.y++;
            grid = newGrid;
        }

        private void AddColumnLeft()
        {
            float[] newGrid = new float[(size.x + 1) * size.y];
            for (int i = 0; i < size.y; ++i)
            {
                //Add new column
                newGrid[i * (size.x + 1)] = -1.0f;
                //Copy old row
                Array.Copy(grid,i*size.x, newGrid,i * (size.x + 1) + 1, size.x);
            }

            size.x++;
            position.x--;
            grid = newGrid;
        }

        private void AddColumnRight()
        {
            float[] newGrid = new float[(size.x + 1) * size.y];
            for (int i = 0; i < size.y; ++i)
            {
                //Copy old row
                Array.Copy(grid,i*size.x,newGrid,i * (size.x + 1),size.x);
                //Add new column
                newGrid[(i + 1) * (size.x + 1) - 1] = -1.0f;
            }

            size.x++;
            grid = newGrid;
        }

        public int GetIndex(int x, int y)
        {
            return ((y - position.y) * size.x + (x - position.x));
        }
    }
}