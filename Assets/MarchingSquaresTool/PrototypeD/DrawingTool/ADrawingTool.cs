using System;
using MarchingSquaresTool.PrototypeD.Core;
using MarchingSquaresTool.PrototypeD.LevelManagement;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.DrawingTool
{
    public abstract class ADrawingTool : EditorTool
    {
        LevelEditor Target;

        private bool mouseDown = false;

        private void OnEnable()
        {
            Target =  (LevelEditor)target;
        }

        public override void OnToolGUI(EditorWindow window)
        {
            if (target == null)
            {
                return;
            }
            
            if (!(window is SceneView))
            {
                return;
            }
            SceneView scene =  (SceneView)window;
            //Ensure object isn't deselected on left click
            int id = GUIUtility.GetControlID(FocusType.Passive);
            HandleUtility.AddDefaultControl(id);

            //User left click down
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                mouseDown = true;
            }
            //User left click up
            if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
            {
                EditorUtility.SetDirty(Target.Generator);
                mouseDown = false;
            }

            if (mouseDown)
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

                Draw(worldSpace, Target.Generator.grid);
            }
        }

        protected abstract void Draw(Vector2 mousePosition, BodyGrid grid);
    }
}