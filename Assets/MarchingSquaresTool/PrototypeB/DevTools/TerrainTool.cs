using PrototypeB.MarchingSquaresTool.PrototypeB.Components;
using PrototypeB.MarchingSquaresTool.PrototypeB.Voxel;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace PrototypeB.MarchingSquaresTool.PrototypeB.DevTools
{
    [EditorTool(("Terrain tool"),typeof(VoxelComponent))]
    public class TerrainTool: EditorTool, IDrawSelectedHandles
    {
        private VoxelGridObject targetGrid;
        private Vector2 pos;

        private bool mouseDown;

        private void OnEnable()
        {
            //Get the grid being targeted
            targetGrid = ((VoxelComponent)target).Grid;
        }

        private void OnDisable()
        {
            targetGrid = null;
        }

        public override void OnToolGUI(EditorWindow window)
        {
            if (!(window is SceneView))
            {
                return;
            }

            SceneView view = (SceneView)window;
            
            //Ensure object isn't deselected on left click
            int id = GUIUtility.GetControlID(FocusType.Passive);
            HandleUtility.AddDefaultControl(id);

            //Get if mouse is down
            if (Event.current.type == EventType.MouseDown && Event.current.button==0)
            {
                mouseDown = true;
            }

            if (Event.current.type == EventType.MouseUp && Event.current.button==0)
            {
                mouseDown = false;
            }

            if (mouseDown)
            {
                //Get mouse position
                Vector2 mousePosition = Event.current.mousePosition;
                mousePosition.y = view.camera.pixelHeight - mousePosition.y;
                Vector2 mouseWorldSpace = view.camera.ScreenToWorldPoint(mousePosition);
                
                Vector2Int nearestPoint = new Vector2Int((int)mouseWorldSpace.x, (int)mouseWorldSpace.y);
                if (Mathf.Abs(mousePosition.x + 1.0f) < 0.05f || Mathf.Abs(mousePosition.y + 1.0f) < 0.05f)
                {
                    return;
                }

                targetGrid[nearestPoint.x, nearestPoint.y] = 1.0f;
            }
        }

        public void OnDrawHandles()
        {
            Vector2Int position  = targetGrid.Position;
            Vector2Int size = targetGrid.Size;

            for (int y = position.y; y < position.y + size.y; ++y)
            {
                for (int x = position.x; x < position.x + size.x; ++x)
                {
                    float value = targetGrid[x, y];
                    Handles.color = new Color(value / 2.0f + 0.5f,0,0);
                    Handles.DrawSolidDisc(new Vector3(x,y,0),Vector3.forward,0.5f);
                }
            }
        }
    }
}