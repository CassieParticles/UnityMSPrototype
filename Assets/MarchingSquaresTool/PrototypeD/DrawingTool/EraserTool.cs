using MarchingSquaresTool.PrototypeD.Core;
using MarchingSquaresTool.PrototypeD.LevelManagement;
using UnityEditor.EditorTools;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.DrawingTool
{
    [EditorTool("Eraser",typeof(LevelEditor))]
    public class EraserTool: ADrawingTool
    {
        private float _radius = 5f;
        protected override void Draw(Vector2 mousePosition, BodyGrid grid)
        {
            for (int x = (int)(mousePosition.x - _radius); x < mousePosition.x + _radius; x++)
            {
                for (int y = (int)(mousePosition.y - _radius); y < mousePosition.y + _radius; y++)
                {
                    float distanceSqr = (x - mousePosition.x) * (x - mousePosition.x) + (y - mousePosition.y) * (y - mousePosition.y);
                    
                    if (distanceSqr > _radius * _radius)
                    {
                        continue;
                    }
                    float scalar =  1 - (distanceSqr / (_radius * _radius));
                    
                    grid.Paint(x,y) -= scalar * Time.fixedDeltaTime;
                }
            }
        }
    }
}