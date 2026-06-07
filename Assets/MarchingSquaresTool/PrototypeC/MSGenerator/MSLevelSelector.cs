using System;
using MarchingSquaresTool.PrototypeC.FileLoader;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.MSGenerator
{
    [RequireComponent(typeof(MSGenerator))]
    public class MSLevelSelector : MonoBehaviour
    {
        [SerializeField] protected string levelName;

        //Safer way of getting generator
        public MSGenerator Generator
        {
            get
            {
                if (_generator)
                {
                    _generator = GetComponent<MSGenerator>();
                }
                return _generator;
            }
        }

        private MSGenerator _generator;

        public void LoadLevel()
        {
            Generator.Grid = LevelFileHandler.LoadLevel(levelName);
        }
    }
}