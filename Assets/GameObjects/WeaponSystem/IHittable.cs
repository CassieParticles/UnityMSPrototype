using UnityEngine;

namespace GameObjects.WeaponSystem
{
    public interface IHittable
    {
        public void DirectHit();
        public void ExplosionHit(Collider2D explosiveCollider);
    }
}