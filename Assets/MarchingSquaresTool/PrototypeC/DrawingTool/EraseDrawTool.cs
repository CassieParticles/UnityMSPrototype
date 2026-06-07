using MarchingSquaresTool.PrototypeC.Core;
using MarchingSquaresTool.PrototypeC.MSGenerator;
using UnityEditor.EditorTools;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.DrawingTool
{
    [EditorTool("Erase",typeof(MSLevelEditor))]
    public class EraseDrawTool: ADrawingTool
    {
        private float _radius = 3f;
        public override void Draw(BodyGrid grid, Vector2 mousePosition, bool solid)
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
                    
                    Target.Generator.Grid.DrawPoint(x,y).Terrain -= Time.fixedDeltaTime * scalar;
                    Target.Generator.Grid.DrawPoint(x, y).Solid = solid;
                }
            }
        }
    }
}