using System;
using MarchingSquaresTool.PrototypeC.Core;
using MarchingSquaresTool.PrototypeC.MSGenerator;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.DrawingTool
{
    public abstract class ADrawingTool: EditorTool
    {
        protected MSEditor Target { get; private set; }

        private void OnEnable()
        {
            Target = (MSEditor)target;
        }

        private void OnDisable()
        {
            Target = null;
        }

        public abstract void Draw(BodyGrid grid, Vector2 mousePosition, bool solid);

        public override void OnToolGUI(EditorWindow window)
        {
            if (!(window is SceneView))
            {
                return;
            }
            
            SceneView scene =  (SceneView)window;
            
            //Ensure object isn't deselected on left click
            int id = GUIUtility.GetControlID(FocusType.Passive);
            HandleUtility.AddDefaultControl(id);

            if (Event.current.type == EventType.MouseDown)
            {
                //Get mouse position of cursor
                Vector2 screenPos = Event.current.mousePosition;
                
                //If screenpos is -1, then just exit
                if (Mathf.Abs(screenPos.x + 1.0f) < 0.05f || Mathf.Abs(screenPos.y + 1.0f) < 0.05f)
                {
                    return;
                }
                
                screenPos.y = scene.camera.pixelHeight - screenPos.y;
                Vector2 worldSpace = scene.camera.ScreenToWorldPoint(screenPos);
                
                Draw(Target.generator.Grid,worldSpace,Event.current.button == 1);
            }
        }
    }
}