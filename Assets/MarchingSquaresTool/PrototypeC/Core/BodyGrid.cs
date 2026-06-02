using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.Core
{
    public class BodyGrid
    {
        public Cell[] Grid { get; private set; }
        private Vector2Int _origin;
        private Vector2Int _size;
        
        public Vector2Int Origin => _origin;
        public Vector2Int Size => _size;

        private bool _resized;
        private readonly Stack<Vector2Int> _changes;

        public BodyGrid()
        {
            Grid = new Cell[1]{new Cell()};
            _origin = Vector2Int.zero;
            _size = Vector2Int.one;

            _resized = false;
            _changes = new Stack<Vector2Int>();
        }

        public BodyGrid(Cell[] cells, Vector2Int size, Vector2Int origin)
        {
            
        }
        
        public Cell this[int x, int y]
        {
            get => Grid[GetIndex(x, y)];
            set
            {
                Grid[GetIndex(x, y)] = value;
                _changes.Push(new Vector2Int(x, y));
            }
        }
        
        public bool Resized
        {
            get
            {
                bool ret = _resized;
                _resized = false;
                return ret;
            }
        }

        public Vector2Int? UpdatedCell
        {
            get
            {
                if (_changes.Count == 0)
                {
                    return null;
                }
                return _changes.Pop();
            }
        }

        public void AddRowTop()
        {
            Cell[] newGrid = new Cell[_size.x * (_size.y + 1)];
            //Add new row
            for (int i = 0; i < _size.x; ++i)
            {
                newGrid[i] = new Cell();
            }
            //Copy old array
            Array.Copy(Grid,0,newGrid,_size.x,Grid.Length);

            _size.y++;
            _origin.y++;
            Grid = newGrid;
            
            _resized = true;
        }

        public void AddRowBottom()
        {
            Cell[] newGrid = new Cell[_size.x * (_size.y + 1)];
            //Copy old array
            Array.Copy(Grid,0,newGrid,0,Grid.Length);
            //Add new row
            for (int i = 0; i < _size.x; ++i)
            {
                newGrid[i + Grid.Length] = new Cell();
            }

            _size.y++;
            Grid = newGrid;
            _resized = true;
        }

        public void AddColumnLeft()
        {
            Cell[] newGrid = new Cell[(_size.x + 1) * _size.y];
            for (int i = 0; i < _size.y; ++i)
            {
                //Add new column
                newGrid[i * (_size.x) + 1] = new Cell();
                //Copy old row
                Array.Copy(Grid,i*_size.x, newGrid,i * (_size.x + 1) + 1, _size.x);
            }

            _size.x++;
            _origin.x++;
            Grid = newGrid;

            _resized = true;
        }

        public void AddColumnRight()
        {
            Cell[] newGrid = new Cell[(_size.x + 1) * _size.y];
            for (int i = 0; i < _size.y; ++i)
            {
                //Copy old row
                Array.Copy(Grid,i*_size.x,newGrid,i * (_size.x + 1),_size.x);
                //Add new column
                newGrid[(i + 1) * (_size.x + 1) - 1] = new Cell();
            }

            _size.x++;
            Grid = newGrid;

            _resized = true;
        }
        
        private int GetIndex(int x, int y)
        {
            return ((y + _origin.y) * _size.x) + (x + _origin.x);
        }
    }
}