using UnityEngine;

namespace MarchingSquaresTool.Scripts
{
    public static class MSHelperFunctions
    {
        public static int[,] triangleIndexTable = {
            {8,8,8,8,8,8,8,8,8,8,8,8,8},//0
            {0,7,1,8,8,8,8,8,8,8,8,8,8},
            {1,3,2,8,8,8,8,8,8,8,8,8,8},
            {0,7,3,0,3,2,8,8,8,8,8,8,8},
            {3,5,4,8,8,8,8,8,8,8,8,8,8},//4
            {0,7,1,1,7,5,1,5,3,3,5,4,8},
            {1,4,2,1,5,4,8,8,8,8,8,8,8},
            {0,7,2,2,7,5,2,5,4,8,8,8,8},
            {5,7,6,8,8,8,8,8,8,8,8,8,8},//8
            {0,5,1,0,6,5,8,8,8,8,8,8,8},
            {1,3,2,1,5,3,1,7,5,5,7,6,8},
            {0,3,2,0,5,3,0,6,5,8,8,8,8},
            {3,7,4,4,7,6,8,8,8,8,8,8,8},//12
            {0,6,1,1,6,3,3,6,4,8,8,8,8},
            {1,4,2,1,7,4,4,7,6,8,8,8,8},
            {0,4,2,0,6,4,8,8,8,8,8,8,8}
        };
        public static int[,] edgeIndexTable = {
            {8,8,8,8,8},    //0
            {7,1,8,8,8},
            {1,3,8,8,8},
            {7,3,8,8,8},
            {3,5,8,8,8},    //4
            {7,5,3,1,8},
            {1,5,8,8,8},
            {7,5,8,8,8},
            {5,7,8,8,8},    //8
            {5,1,8,8,8},
            {1,7,5,3,8},
            {5,3,8,8,8},
            {3,7,8,8,8},    //12
            {3,1,8,8,8},
            {1,7,8,8,8},
            {8,8,8,8,8}
        };

        public static float normalise(float a, float b, float v)
        {
            return (v - a) / (b - a);
        }

        public static Vector2 findMidpointX(Vector2 a, Vector2 b, float aVal, float bVal, float isoValue = 0)
        {
            return new Vector2(a.x + normalise(aVal, bVal, isoValue) * (b.x - a.x), a.y);
        }

        public static Vector2 findMidpointY(Vector2 a, Vector2 b, float aVal, float bVal, float isoValue = 0)
        {
            return new Vector2(a.x, a.y + normalise(aVal, bVal, isoValue) * (b.y - a.y));
        }
    }
}