using MarchingSquaresTool.PrototypeC.FileLoader;
using NUnit.Framework;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.MSTests
{
    public class FileLoaderTest
    {
        [Test]
        public void Level1x1()
        {
            Cell[] cellArray = new Cell[]
            {
                new Cell(1,false)
            };
            string levelName = "TestLevel1x1";
        
            //Attempt to store the level (THE IMPORTANT PART)
            LevelFileHandler.StoreLevel(cellArray,1,1,levelName);

            uint width = 0;
            uint height = 0;
        
            Cell[] loadedLevel = LevelFileHandler.LoadLevel(levelName,ref width,ref height);

            Assert.AreEqual(1,width,"Width is accurate");
            Assert.AreEqual(1,height,"Height is accurate");
        
            for (int i = 0; i < cellArray.Length; ++i)
            {
                Assert.AreEqual(cellArray[i],loadedLevel[i]);
            }
        
            LevelFileHandler.DeleteLevel(levelName);
        }

        [Test]
        public void Level8x8()
        {
            //Set up
            uint width = 8;
            uint height = 8;
            string levelName = "TestLevel8x8";
        
            Cell[] cellArray = new Cell[]
            {
                new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),
                new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),
                new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),
                new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),
                new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),
                new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),
                new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),
                new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),new Cell(-1,true),new Cell(1,false),
            };
            
            LevelFileHandler.StoreLevel(cellArray, 8, 8, levelName);
        
            uint fetchedWidth = 0;
            uint fetchedHeight = 0;
        
            Cell[] loadedLevel = LevelFileHandler.LoadLevel(levelName,ref fetchedWidth,ref fetchedHeight);
        
            //Ensure loaded level is identical to stored one
            Assert.AreEqual(width,fetchedWidth,"Width is accurate");
            Assert.AreEqual(height,fetchedHeight,"Height is accurate");

            for (int i = 0; i < cellArray.Length; ++i)
            {
                Assert.True(cellArray[i].Equals(loadedLevel[i]));
            }
        
            LevelFileHandler.DeleteLevel(levelName);
        }
    }
}
