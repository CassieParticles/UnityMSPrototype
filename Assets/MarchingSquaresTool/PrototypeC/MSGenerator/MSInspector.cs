using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace MarchingSquaresTool.PrototypeC.MSGenerator
{
    [CustomEditor(typeof(MSLevelSelector),true)]
    public class MSInspector : Editor
    {
        private TextField _levelName;
        private Button _loadButton;
        private Button _saveButton;

        private SerializedProperty _levelNameSer;

        private void OnEnable()
        {
            //Get serialized field in level editor, set text box
            _levelNameSer = serializedObject.FindProperty("levelName");
            
            _levelName = new TextField("Level Name") {value = _levelNameSer.stringValue};
            _loadButton = new Button(LoadLevel) { text = "Load Level" };
            _saveButton = new Button(SaveLevel) { text = "Save Level" };
            
            //Ensure name is retained in text box
            AssemblyReloadEvents.beforeAssemblyReload += UpdateName;
            AssemblyReloadEvents.afterAssemblyReload += UpdateTextBox;

            if (target is MSLevelEditor)
            {
                AssemblyReloadEvents.beforeAssemblyReload += SaveLevel;
            }
            AssemblyReloadEvents.afterAssemblyReload += LoadLevel;
        }

        private void OnDisable()
        {
            //Update serialized field in level editor with string in textbox
            UpdateName();
            
            //Ensure name is retained in text box
            AssemblyReloadEvents.beforeAssemblyReload -= UpdateName;
            AssemblyReloadEvents.afterAssemblyReload -= UpdateTextBox;
            
            if (target is MSLevelEditor)
            {
                AssemblyReloadEvents.beforeAssemblyReload -= SaveLevel;
            }
            AssemblyReloadEvents.afterAssemblyReload -= LoadLevel;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = new  VisualElement();

            inspector.Add(_levelName);
            inspector.Add(_loadButton);
            
            //Only level editors support saving
            if (target is MSLevelEditor)
            {
                inspector.Add(_saveButton);
            }
            
            return inspector;
        }

        private void UpdateTextBox()
        {
            _levelName.value = _levelNameSer.stringValue;
        }

        private void UpdateName()
        {
            _levelNameSer.stringValue = _levelName.text;
            serializedObject.ApplyModifiedProperties();
        }

        private void LoadLevel()
        {

            UpdateName();
            
            ((MSLevelSelector)target).LoadLevel();
        }

        private void SaveLevel()
        {
            UpdateName();
            
            ((MSLevelEditor)target).SaveLevel();
        }
    }
}