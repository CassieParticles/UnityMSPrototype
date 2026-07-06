using Generator;
using UnityEditor;
using UnityEngine;

namespace LevelManagement
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
            Generator.Generate();
            EditorUtility.SetDirty(Generator);
        }

        public void SaveLevel()
        {
            LevelFileHelper.SaveLevel(levelName, Generator.grid);
        }
    }
}