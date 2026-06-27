using MarchingSquaresTool.PrototypeD.Generator;
using UnityEngine;
using UnityEngine.UI;

namespace MarchingSquaresTool.PrototypeD.LevelManagement
{
    [RequireComponent(typeof(MSGenerator))]
    public class LevelEditor : MonoBehaviour
    {
        private MSGenerator generator = null;
        public MSGenerator Generator => generator ??= GetComponent<MSGenerator>();
        
        public string levelName;

        public void LoadLevel()
        {
            Generator.grid = LevelFileHelper.LoadLevel(levelName);
        }

        public void SaveLevel()
        {
            LevelFileHelper.SaveLevel(levelName, Generator.grid);
        }
    }
}