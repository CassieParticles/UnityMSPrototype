using System;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace MarchingSquaresTool.PrototypeC.FileLoader
{
    public static class LevelFileHandler
    {
        public static readonly string LevelPath = "/Levels/";

        public static void StoreLevel(Cell[] cells, uint width, uint height, string title)
        {
            //16 bytes, level size, (width * height * 5), level data
            byte[] levelData = new byte[4 * 2 + width * height * 5];

            //Copy level size
            uint[] size = new uint[2] { width, height };
            Buffer.BlockCopy(size,0,levelData,0,8);

            float[] terrainBuffer = new float[1];
            bool[] solidBuffer = new bool[1];
            
            //Copy level
            for (int i = 0; i < cells.Length; i++)
            {
                terrainBuffer[0] = cells[i].Terrain;
                solidBuffer[0] = cells[i].Solid;
                
                Buffer.BlockCopy(terrainBuffer,0,levelData,8 + (i * 5),4);
                Buffer.BlockCopy(solidBuffer,0,levelData,8 + (i * 5) + 4,1);
            }
            
            //Write the data
            File.WriteAllBytes(Application.dataPath + LevelPath + title + ".lvl", levelData);
        }
        
        public static Cell[] LoadLevel(string title,ref uint width,ref uint height)
        {
            //Get the size of the level
            byte[] levelData = File.ReadAllBytes(Application.dataPath + LevelPath + title + ".lvl");
            uint[] size = new uint[2];
            
            Buffer.BlockCopy(levelData,0,size,0,8);

            width = size[0];
            height = size[1];
            
            Cell[] cells = new Cell[width * height];

            float[] terrainBuffer = new float[1];
            bool[] solidBuffer = new bool[1];
            
            //Decode the level data
            for (int i = 0; i < cells.Length; i++)
            {
                Buffer.BlockCopy(levelData,8 + (i * 5), terrainBuffer, 0, 4);
                Buffer.BlockCopy(levelData,8 + (i * 5) + 4, solidBuffer, 0, 1);
                
                cells[i] = new Cell(terrainBuffer[0], solidBuffer[0]);
            }
            
            return cells;
        }

        public static void DeleteLevel(string title)
        {
            File.Delete(Application.dataPath + LevelPath + title + ".lvl");
        }
    }
}