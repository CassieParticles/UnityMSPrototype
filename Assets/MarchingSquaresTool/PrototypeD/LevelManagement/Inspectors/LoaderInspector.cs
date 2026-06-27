using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MarchingSquaresTool.PrototypeD.LevelManagement.Inspectors
{
    [CustomEditor(typeof(LevelLoader))]
    public class LoaderInspector : Editor
    {
        private TextField levelName;
        private Button loadLevel;

        private LevelLoader Target => (LevelLoader)target;

        private void Awake()
        {
            loadLevel = new Button(Target.LoadLevel) { text = "Load Level" };
            levelName = new TextField(){value = Target.levelName};
        }

        public void OnSceneGUI()
        {
            Target.levelName = levelName.text;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = new VisualElement();
            
            inspector.Add(levelName);
            inspector.Add(loadLevel);
            
            return inspector;
        }
    }
}