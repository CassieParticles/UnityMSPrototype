using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class BodyGrid
    {
        [HideInInspector][SerializeField]
        public float[] grid;

        [HideInInspector][SerializeField] 
        public bool[] solid;

        [HideInInspector] [SerializeField]
        private Vector2Int size;
        //Position refers to the position of the top left corner
        [HideInInspector] [SerializeField]
        private Vector2Int position;
        
        public Vector2Int Size => size;
        public Vector2Int Position => position;

        public BodyGrid()
        {
            this.grid = new float[1]{-1.0f};
            this.solid = new bool[1] { false };
            size = Vector2Int.one;
            position = Vector2Int.zero;
        }

        //Used for file handling
        public BodyGrid(Vector2Int size, Vector2Int position)
        {
            this.grid = new float[size.x * size.y];
            this.solid = new bool[size.x * size.y];
            this.size = size;
            this.position = position;
        }

        public float this[int x, int y]
        {
            get => grid[GetIndex(x, y)];
            set
            {
                ExpandToPosition(x,y);
                grid[GetIndex(x, y)] = Mathf.Clamp(value,-1.0f,1.0f);
            }
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

        public bool SetStatic(int x, int y, bool isSolid)
        {
            ExpandToPosition(x,y);
            return solid[GetIndex(x, y)] = isSolid;
        }

        public bool GetStatic(int x, int y)
        {
            return solid[GetIndex(x, y)];
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
            bool[] newSolid = new bool[size.x * (size.y + 1)];
            //Add new row
            for (int i = 0; i < size.x; ++i)
            {
                newGrid[i] = -1.0f;
                newSolid[i] = false;
            }
            //Copy old array
            Array.Copy(grid,0,newGrid,size.x,grid.Length);
            Array.Copy(solid,0,newSolid,size.x,solid.Length);

            size.y++;
            position.y--;
            grid = newGrid;
            solid = newSolid;
        }

        private void AddRowBottom()
        {
            float[] newGrid = new float[size.x * (size.y + 1)];
            bool[] newSolid = new bool[size.x * (size.y + 1)];
            //Copy old array
            Array.Copy(grid,0,newGrid,0,grid.Length);
            Array.Copy(solid,0,newSolid,0,solid.Length);
            //Add new row
            for (int i = 0; i < size.x; ++i)
            {
                newGrid[i + grid.Length] = -1.0f;
                newSolid[i + solid.Length] = false;
            }

            size.y++;
            grid = newGrid;
            solid = newSolid;
        }

        private void AddColumnLeft()
        {
            float[] newGrid = new float[(size.x + 1) * size.y];
            bool[] newSolid = new bool[(size.x + 1) * size.y];
            for (int i = 0; i < size.y; ++i)
            {
                //Add new column
                newGrid[i * (size.x + 1)] = -1.0f;
                newSolid[i * (size.x + 1)] = false;
                //Copy old row
                Array.Copy(grid,i*size.x, newGrid,i * (size.x + 1) + 1, size.x);
                Array.Copy(solid,i * size.x, newSolid, i * (size.x + 1) + 1, size.x);
            }

            size.x++;
            position.x--;
            grid = newGrid;
            solid = newSolid;
        }

        private void AddColumnRight()
        {
            float[] newGrid = new float[(size.x + 1) * size.y];
            bool[] newSolid = new bool[(size.x + 1) * size.y];
            for (int i = 0; i < size.y; ++i)
            {
                //Copy old row
                Array.Copy(grid,i*size.x,newGrid,i * (size.x + 1),size.x);
                Array.Copy(solid, i * size.x, newSolid, i * (size.x + 1), size.x);
                //Add new column
                newGrid[(i + 1) * (size.x + 1) - 1] = -1.0f;
                newSolid[(i + 1) * (size.x + 1) - 1] = false;
            }

            size.x++;
            grid = newGrid;
            solid = newSolid;
        }

        public int GetIndex(int x, int y)
        {
            return ((y - position.y) * size.x + (x - position.x));
        }
    }
}