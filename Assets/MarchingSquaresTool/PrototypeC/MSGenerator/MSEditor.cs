using System;
using MarchingSquaresTool.PrototypeC.FileLoader;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.MSGenerator
{
    [RequireComponent(typeof(MSGenerator))]
    public class MSEditor: MonoBehaviour
    {
        public MSGenerator generator;
        [SerializeField] private string levelName;
        
        private void OnValidate()
        {
            generator = GetComponent<MSGenerator>();
        }

        private void Awake()
        {
            LoadLevel();
        }

        public void SaveLevel()
        {
            LevelFileHandler.StoreLevel(generator.Grid, levelName);
        }

        public void LoadLevel()
        {
            generator.Grid = LevelFileHandler.LoadLevel(levelName);
        }
    }
}