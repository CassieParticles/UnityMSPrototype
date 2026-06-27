using MarchingSquaresTool.PrototypeD.Generator;
using UnityEditor;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.LevelManagement
{
    [RequireComponent(typeof(MSGenerator))]
    public class LevelLoader : MonoBehaviour
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
    }
}