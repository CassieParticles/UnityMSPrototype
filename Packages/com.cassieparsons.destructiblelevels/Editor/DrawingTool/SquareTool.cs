using Core;
using LevelManagement;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace Editor.DrawingTool
{
    [EditorTool("Square",typeof(LevelEditor),toolPriority = 2)]
    [Icon("Packages/com.cassieparsons.destructiblelevels/Editor/Icons/Rectangle.png")]
    public class SquareTool : ADrawingTool
    {
      
        
        
        protected override void Draw(Vector2 mousePosition, BodyGrid grid, bool solid)
        {
            for (int x = (int)(mousePosition.x - Size); x < mousePosition.x + Size; x++)
            {
                for (int y = (int)(mousePosition.y - Size); y < mousePosition.y + Size; y++)
                {
                    grid[x, y] = 1.0f;
                    grid.SetStatic(x,y,solid);
                }
            }
        }
    }
}
