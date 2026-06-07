using System;
using MarchingSquaresTool.PrototypeC.Core;
using MarchingSquaresTool.PrototypeC.FileLoader;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.MSGenerator
{
    [RequireComponent(typeof(MSGenerator))]
    public class MSLevelSelector : MonoBehaviour
    {
        [SerializeField] protected string levelName = "Example Level";

        //Safer way of getting generator
        private MSGenerator _generator;
        public MSGenerator Generator
        {
            get
            {
                if (_generator == null)
                {
                    _generator = GetComponent<MSGenerator>();
                }
                return _generator;
            }
        }

        private void Awake()
        {
            LoadLevel();
        }

        public void LoadLevel()
        {
            Generator.Grid = LevelFileHandler.LoadLevel(levelName);
        }
    }
}