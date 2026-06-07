using System;
using MarchingSquaresTool.PrototypeC.Core;
using MarchingSquaresTool.PrototypeC.MSGenerator;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.DrawingTool
{
    public abstract class ADrawingTool: EditorTool, IDrawSelectedHandles
    {
        protected MSLevelEditor Target { get; private set; }

        private bool _mouseDown;
        private bool _shiftDown;

        private void OnEnable()
        {
            Target = (MSLevelEditor)target;
            _mouseDown = false;
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

            //Get mouse button events
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                _mouseDown = true;
            }
            if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
            {
                _mouseDown = false;
            }
            //Get key events
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.LeftShift)
            {
                _shiftDown = true;
            }
            if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.LeftShift)
            {
                _shiftDown = false;
            }
            
            if (_mouseDown)
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
                
                Draw(Target.Generator.Grid,worldSpace,_shiftDown);
                
                //Update marching squares
                Target.Generator.Clear();
                Target.Generator.Generate();
            }
        }

        public void OnDrawHandles()
        {
            if (Target == null)
            {
                return;
            }
            Vector2Int offset = Target.Generator.Grid.Origin;
            Vector2Int size = Target.Generator.Grid.Size;
            for (int x = -offset.x; x < size.x - offset.x; x++)
            {
                for (int y = -offset.y; y < size.y - offset.y; y++)
                {
                    if (Target.Generator.Grid[x, y].Terrain >= 0)
                    {
                        //Get the colour of that point
                        Color color = Target.Generator.Grid[x, y].Solid ?  Color.red : Color.blue;
                        color *= Target.Generator.Grid[x, y].Terrain;
                        
                        Handles.color = color;
                        Handles.DrawSolidDisc(Target.transform.position + new Vector3(x,y),Vector3.forward,0.2f);
                    }
                }
            }
        }
    }
}