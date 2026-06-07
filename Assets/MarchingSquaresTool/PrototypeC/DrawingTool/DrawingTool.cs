using System;
using MarchingSquaresTool.PrototypeC.Core;
using MarchingSquaresTool.PrototypeC.MSGenerator;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.DrawingTool
{
    [EditorTool("Draw Terrain",typeof(MSEditor))]
    public class DrawingTool: EditorTool
    {
        private MSEditor _target;

        private void OnEnable()
        {
            _target = (MSEditor)target;
        }

        private void OnDisable()
        {
            _target = null;
        }

        public override void OnToolGUI(EditorWindow window)
        {
            if (window is SceneView scene)
            {
                //Ensure object isn't deselected on left click
                int id = GUIUtility.GetControlID(FocusType.Passive);
                HandleUtility.AddDefaultControl(id);

                //Mouse down
                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                {
                    //Get position of cursor
                    Vector2 screenPos = Event.current.mousePosition;
                    screenPos.y = scene.camera.pixelHeight - screenPos.y;
                    Vector2 worldSpace = scene.camera.ScreenToWorldPoint(screenPos);
                    
                    Vector2Int nearestPoint = new Vector2Int((int)worldSpace.x, (int)worldSpace.y);
                    if (Mathf.Abs(screenPos.x + 1.0f) < 0.05f || Mathf.Abs(screenPos.y + 1.0f) < 0.05f)
                    {
                        return;
                    }

                    _target.generator.Grid.DrawPoint(nearestPoint.x,nearestPoint.y).Terrain = 1.0f;
                }
            }
        }
    }
}