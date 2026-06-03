using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MarchingSquaresTool.PrototypeC.MSGenerator
{
    [CustomEditor(typeof(MSGenerator))]
    public class MSInspector : Editor
    {
        private TextField _levelName;

        private void OnEnable()
        {
            AssemblyReloadEvents.beforeAssemblyReload += SaveLevel;
        }

        private void OnDisable()
        {
            AssemblyReloadEvents.beforeAssemblyReload -= SaveLevel;
        }

        private void LoadLevel()
        {
            Debug.Log("Loading "+ _levelName.text);
            ((MSGenerator)target).LoadLevel(_levelName.text);//sad
        }
        
        private void SaveLevel()
        {
            Debug.Log("Saving "+ _levelName.text);
            ((MSGenerator)target).SaveLevel(_levelName.text);
        }

        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = new VisualElement();

            _levelName = new TextField("Level Name");
            inspector.Add(_levelName);
            
            inspector.Add(new Button(LoadLevel) {text = "Load Level"});
            inspector.Add(new Button(SaveLevel) {text = "Save Level"});
            
            return inspector;
        }
    }
}