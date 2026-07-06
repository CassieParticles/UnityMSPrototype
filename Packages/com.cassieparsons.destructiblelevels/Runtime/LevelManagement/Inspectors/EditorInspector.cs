using UnityEditor;
using UnityEngine.UIElements;

namespace LevelManagement.Inspectors
{
    [CustomEditor(typeof(LevelEditor))]
    public class EditorInspector : Editor
    {
        private TextField levelName;
        private Button loadLevel;
        private Button saveLevel;

        private LevelEditor Target => (LevelEditor)target;

        private void Awake()
        {
            loadLevel = new Button(Target.LoadLevel) { text = "Load Level" };
            saveLevel = new Button(Target.SaveLevel) { text = "Save Level" };
            levelName = new TextField(){value = Target.levelName};
        }

        public void OnSceneGUI()
        {
            if (levelName == null)
            {
                return;
            }
            Target.levelName = levelName.text;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = new VisualElement();
            
            inspector.Add(levelName);
            inspector.Add(loadLevel);
            inspector.Add(saveLevel);
            
            return inspector;
        }
    }
}