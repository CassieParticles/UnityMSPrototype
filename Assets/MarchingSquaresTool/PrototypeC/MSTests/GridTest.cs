using System.Collections;
using MarchingSquaresTool.PrototypeC.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MarchingSquaresTool.PrototypeC.MSTests
{
    public class GridTest
    {
        [Test]
        public void TestGridResize()
        {
            BodyGrid grid = new BodyGrid();
            Assert.True(grid.Size == new Vector2Int(1,1));
            Assert.True(grid.Origin == new Vector2Int(0,0));
            
            grid.AddColumnLeft();
            grid.AddColumnLeft();
            
            Assert.True(grid.Size == new Vector2Int(3,1));
            Assert.True(grid.Origin == new Vector2Int(2,0));
            
            grid.AddColumnRight();
            grid.AddColumnRight();
            
            Assert.True(grid.Size == new Vector2Int(5,1));
            Assert.True(grid.Origin == new Vector2Int(2,0));
            
            grid.AddRowBottom();
            grid.AddRowBottom();
            
            Assert.True(grid.Size == new Vector2Int(5,3));
            Assert.True(grid.Origin == new Vector2Int(2,0));
            
            grid.AddRowTop();
            grid.AddRowTop();
            
            Assert.True(grid.Size == new Vector2Int(5,5));
            Assert.True(grid.Origin == new Vector2Int(2,2));
        }
    }
}