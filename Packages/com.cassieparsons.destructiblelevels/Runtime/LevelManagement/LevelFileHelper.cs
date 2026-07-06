using System;
using System.IO;
using Core;
using JetBrains.Annotations;
using UnityEngine;

namespace LevelManagement
{
    public static class LevelFileHelper
    {

        /*  Level format structure:
         *      Header: 
         *        8 bytes -> Vector2Int: Level size (width,height)
         *        8 bytes -> Vector2Int: Level position (x,y)
         *      LevelGrid:
         *         4 bytes-> float: cell value
         *      LevelSolid:
         *         1 byte -> bool: solid
         *
         *      Level exists (width * height) for the level, therefore it is important to first collect the size of the level
         *
         *  Level name is the file name
         *  Levels stored as a .lvl file (binary data format doesn't complement json)
         */
        
        private const string LevelsFilePath = "/Levels/";
        
        public static string FolderPath => Application.dataPath + LevelsFilePath;
        public static string GetFilePath(string levelName)
        {
            return FolderPath + levelName + ".lvl";
        }
        
        
        public static void SaveLevel(string levelName, BodyGrid grid)
        {
            WriteLevel(levelName, EncodeLevel(grid));
        }
        
        [CanBeNull]
        public static BodyGrid LoadLevel(string levelName)
        {
            byte[] levelData = ReadLevel(levelName);
            if (levelData == null) { return null;}
            return DecodeLevel(ReadLevel(levelName));
        }

        public static void DeleteLevel(string levelName)
        {
            if (!File.Exists(GetFilePath(levelName)))
            {
                return;
            }
            
            File.Delete(GetFilePath(levelName));
        }
        
        
        private static byte[] EncodeLevel(BodyGrid grid)
        {
            int[] size = new int[] { grid.Size.x, grid.Size.y };
            int[] position = new int[] {grid.Position.x, grid.Position.y};
            
            byte[] levelData = new byte[8 + 8 + 5 * size[0] * size[1]];

            //Copy header data
            Buffer.BlockCopy(size,0,levelData,0,8);
            Buffer.BlockCopy(position,0,levelData,8,8);
            
            //Copy level data
            Buffer.BlockCopy(grid.grid,0,levelData,16,4 * size[0] * size[1]);
            Buffer.BlockCopy(grid.solid,0,levelData,16 + 4 * size[0] * size[1],size[0] * size[1]);
            
            return levelData;
        }

        private static BodyGrid DecodeLevel(byte[] levelData)
        {
            int[] size = new int[2];
            int[] position = new int[2];
            
            //Retrieve header information
            Buffer.BlockCopy(levelData,0,size,0,8);
            Buffer.BlockCopy(levelData,8,position,0,8);
            
            BodyGrid grid = new BodyGrid(new Vector2Int(size[0],size[1]),new Vector2Int(position[0],position[1]));
            //Retrieve level information
            Buffer.BlockCopy(levelData,16,grid.grid,0,4 * size[0] * size[1]);
            Buffer.BlockCopy(levelData,16 + 4 * size[0] * size[1], grid.solid,0,size[0] * size[1]);

            return grid;
        }

        private static void WriteLevel(string levelName, byte[] levelData)
        {
            Directory.CreateDirectory(FolderPath);
            File.WriteAllBytes(GetFilePath(levelName),levelData);
        }

        [CanBeNull]
        private static byte[] ReadLevel(string levelName)
        {
            if (!File.Exists(GetFilePath(levelName)))
            {
                return null;
            }
            
            return File.ReadAllBytes(GetFilePath(levelName));
        }
    }
}