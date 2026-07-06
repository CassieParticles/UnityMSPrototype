using Core;
using LevelManagement;
using UnityEditor;
using UnityEditor.Actions;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.DrawingTool
{
    public abstract class ADrawingTool : EditorTool, IDrawSelectedHandles
    {
        LevelEditor Target;

        private bool shouldDraw = false;
        private bool drawSolid = false;

        protected Vector3 MousePosition;

        protected static float Size = 3.0f;

        [Shortcut("Increase pen size", typeof(SceneView), KeyCode.UpArrow,ShortcutModifiers.Control)]
        private static void IncreaseSize()
        {
            Size++;
        }
        [Shortcut("Decrease pen size", typeof(SceneView), KeyCode.DownArrow,ShortcutModifiers.Control)]
        private static void DecreaseSize()
        {
            Size--;
        }

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
                shouldDraw = true;
            }
            //User left click up
            if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
            {
                EditorUtility.SetDirty(Target.Generator);
                shouldDraw = false;
            }
            //User left shift down
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.LeftShift)
            {
                drawSolid = true;
            }
            //User left shift up
            if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.LeftShift)
            {
                drawSolid = false;
            }

            if (shouldDraw)
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

                //Random offset cause it was offset the other way and idk why
                Draw(worldSpace - (Vector2)Target.transform.position, Target.Generator.grid, drawSolid);
                
                MousePosition  = worldSpace;
                
                Target.Generator.Clear();
                Target.Generator.Generate();
            }
        }

        protected abstract void Draw(Vector2 mousePosition, BodyGrid grid, bool solid);
        
        
        public virtual void OnDrawHandles()
        {
            Handles.DrawSolidDisc(MousePosition, Vector3.forward, 0.1f);
        }
    }
}