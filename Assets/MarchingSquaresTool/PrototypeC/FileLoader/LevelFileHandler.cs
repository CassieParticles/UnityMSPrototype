using System;
using System.IO;
using log4net.Core;
using MarchingSquaresTool.PrototypeC.Core;
using PlasticPipe.Client;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.FileLoader
{
    public static class LevelFileHandler
    {
        public static readonly string LevelPath = "/Levels/";

        public static void StoreLevel(BodyGrid level, string title)
        {
            StoreLevel(level.Grid,(uint)level.Size.x,(uint)level.Size.y,(uint)level.Origin.x,(uint)level.Origin.y,title);
        }

        public static void StoreLevel(Cell[] cells, uint width, uint height, uint originX, uint originY, string title)
        {
            //Level Size (8 bytes), Level Origin (8 bytes), Level (width * height * 5 bytes)
            byte[] levelData = new byte[8 + 8 + width * height * 5];

            //Copy level size
            uint[] size = new uint[2] { width, height };
            Buffer.BlockCopy(size,0,levelData,0,8);
            
            //Copy level origin
            uint[] origin = new uint[2] { originX, originY };
            Buffer.BlockCopy(origin,0,levelData,8,8);

            float[] terrainBuffer = new float[1];
            bool[] solidBuffer = new bool[1];
            
            //Copy level
            for (int i = 0; i < cells.Length; i++)
            {
                terrainBuffer[0] = cells[i].Terrain;
                solidBuffer[0] = cells[i].Solid;
                
                Buffer.BlockCopy(terrainBuffer,0,levelData,16 + (i * 5),4);
                Buffer.BlockCopy(solidBuffer,0,levelData,16 + (i * 5) + 4,1);
            }
            
            //Write the data
            File.WriteAllBytes(Application.dataPath + LevelPath + title + ".lvl", levelData);
        }

        public static BodyGrid LoadLevel(string title)
        {
            Cell[] cells = LoadLevel(title, out uint width, out uint height, out uint originX, out uint originY);

            return new BodyGrid(cells, new Vector2Int((int)width, (int)height), new Vector2Int((int)originX, (int)originY));
        }
        
        public static Cell[] LoadLevel(string title, out uint width, out uint height, out uint originX,
            out uint originY)
        {
            //Get the size of the level
            byte[] levelData = File.ReadAllBytes(Application.dataPath + LevelPath + title + ".lvl");
            
            //Get level size
            uint[] size = new uint[2];
            Buffer.BlockCopy(levelData,0,size,0,8);
            width = size[0];
            height = size[1];

            //Get level origin
            uint[] origin = new uint[2];
            Buffer.BlockCopy(levelData,8,origin,0,8);
            originX = origin[0];
            originY = origin[1];
            
            //Get level
            Cell[] cells = new Cell[width * height];

            float[] terrainBuffer = new float[1];
            bool[] solidBuffer = new bool[1];
            
            //Decode the level data
            for (int i = 0; i < cells.Length; i++)
            {
                Buffer.BlockCopy(levelData,16 + (i * 5), terrainBuffer, 0, 4);
                Buffer.BlockCopy(levelData,16 + (i * 5) + 4, solidBuffer, 0, 1);
                
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