using System;
using GameObjects.WeaponSystem;
using Generator;
using UnityEngine;

namespace GameObjects.TerrainDestruction
{
    [RequireComponent(typeof(MSGenerator))]
    public class HittableTerrain : MonoBehaviour,IHittable
    {
        private MSGenerator generator;
        private void Awake()
        {
            generator = GetComponent<MSGenerator>();
        }
        
        public void DirectHit()
        {
            
        }
        
        public void ExplosionHit(Collider2D explosiveCollider)
        {
            Bounds bounds = explosiveCollider.bounds;

            for (float x = -bounds.extents.x; x < bounds.extents.x; x++)
            {
                for (float y = -bounds.extents.y; y < bounds.extents.y; y++)
                {
                    Vector3 position = new Vector3(x + bounds.center.x, y + bounds.center.y,0);
                    
                    float distanceSqr = (bounds.center - position).sqrMagnitude;
                    float maxRange = bounds.extents.x * bounds.extents.x;
                    
                    float scaledDistance = distanceSqr / maxRange;
                    float explosivePower = 1 - scaledDistance;

                    if (explosivePower < 0)
                    {
                        continue;
                    }
                    Vector3 localPosition = transform.InverseTransformPoint(position);
                    
                    generator.grid.Remove((int)localPosition.x, (int)localPosition.y, explosivePower * 3.0f);
                }
            }
            
            generator.Rebuild();
        }
    }
}