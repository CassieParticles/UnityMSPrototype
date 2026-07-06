using UnityEngine;

namespace Core
{
    public struct Edge
    {
        public Edge(Vector2 a, Vector2 b)
        {
            A = a;
            B = b;
        }

        public Vector2 A;
        public Vector2 B;
    }
}