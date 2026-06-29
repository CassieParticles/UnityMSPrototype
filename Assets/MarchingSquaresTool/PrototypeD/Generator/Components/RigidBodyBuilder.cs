using System;
using MarchingSquaresTool.PrototypeD.Core;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.Generator.Components
{
    [RequireComponent(typeof(Rigidbody2D),typeof(ColliderBuilder))]
    public class RigidBodyBuilder: ATriangleBuilder
    {
        private Vector2 sumMassPosition;
        private float sumMass;

        private float density;

        private Rigidbody2D body;

        public void SetDensity(float density)
        {
            this.density = density;
        }

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            sumMassPosition = Vector2.zero;
            sumMass = 0;
            density = 1;
        }

        public override void AddTriangle(Triangle triangle, Vector2Int cellPosition)
        {
             Vector2 AB = triangle.B - triangle.A;
             Vector2 AC = triangle.C - triangle.A;
             
             //p = m / v -> m = p * v
             
             float surfaceArea = Vector3.Cross(AB, AC).magnitude * 0.5f;
             float mass = density *  surfaceArea;
             
             Vector2 centre = (triangle.A + triangle.B + triangle.C) / 3;
             
             sumMassPosition += centre * mass;
             sumMass += mass;
        }
        public override void SetSolid(bool solid)
        {
            body.bodyType = solid ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        }
        public override void Build()
        {
            body.centerOfMass = sumMassPosition / sumMass;
            body.mass = sumMass;
        }
        public override void Clear()
        {
            sumMassPosition = Vector2.zero;
            sumMass = 0;
        }
        
        public override bool IsEditor()
        {
            return false;
        }
        
        public override bool IsGame()
        {
            return true;
        }
    }
}