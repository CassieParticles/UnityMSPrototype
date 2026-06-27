using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.Core
{
    public struct Triangle
    {
        public Triangle(Vector2 a, Vector2 b, Vector2 c)
        {
            A = a;
            B = b;
            C = c;
        }

        public Vector2 A;
        public Vector2 B;
        public Vector2 C;
    }
}