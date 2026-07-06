using Core;
using LevelManagement;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace Editor.DrawingTool
{
    [EditorTool("Eraser",typeof(LevelEditor),toolPriority = 1)]
    [Icon("Packages/com.cassieparsons.destructiblelevels/Editor/Icons/Eraser.png")]
    public class EraserTool: ADrawingTool
    {
        
        protected override void Draw(Vector2 mousePosition, BodyGrid grid, bool solid)
        {
            for (int x = (int)(mousePosition.x - Size); x < mousePosition.x + Size; x++)
            {
                for (int y = (int)(mousePosition.y - Size); y < mousePosition.y + Size; y++)
                {
                    float distanceSqr = (x - mousePosition.x) * (x - mousePosition.x) + (y - mousePosition.y) * (y - mousePosition.y);
                    
                    if (distanceSqr > Size * Size)
                    {
                        continue;
                    }
                    float scalar =  1 - (distanceSqr / (Size * Size));
                    
                    grid.Remove(x,y,scalar * Time.fixedDeltaTime * 5.0f);
                }
            }
        }
    }
}