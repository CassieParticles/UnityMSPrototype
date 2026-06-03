using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MarchingSquaresTool.PrototypeC.MSGenerator
{
    [CustomEditor(typeof(MSGenerator))]
    [Serializable]
    public class MSInspector : Editor
    {
        private TextField _levelName;
        private SerializedProperty _level;

        private void PreReload()
        {
            _level.stringValue = _levelName.text;
            serializedObject.ApplyModifiedProperties();
        }

        private void PostReload()
        {
            _levelName.value = _level.stringValue;
        }

        private void OnEnable()
        {
            _level = serializedObject.FindProperty("levelName");
            _levelName = new TextField("Level Name") {value = _level.stringValue};
            
            AssemblyReloadEvents.beforeAssemblyReload += PreReload;
            AssemblyReloadEvents.afterAssemblyReload += PostReload;

            AssemblyReloadEvents.beforeAssemblyReload += SaveLevel;
        }

        private void OnDisable()
        {
            _level.stringValue = _levelName.text;
            serializedObject.ApplyModifiedProperties();
            
            AssemblyReloadEvents.beforeAssemblyReload -= PreReload;
            AssemblyReloadEvents.afterAssemblyReload -= PostReload;
            
            AssemblyReloadEvents.beforeAssemblyReload -= SaveLevel;
        }

        public void LoadLevel()
        {
            ((MSGenerator)target).LoadLevel();
        }

        private void SaveLevel()
        {
            ((MSGenerator)target).SaveLevel();
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = new VisualElement();
            
            inspector.Add(_levelName);
            
            inspector.Add(new Button(LoadLevel) {text = "Load Level"});
            inspector.Add(new Button(SaveLevel) {text = "Save Level"});
            
            return inspector;
        }
    }
}