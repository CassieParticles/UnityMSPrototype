using UnityEngine;

namespace GameObjects.WeaponSystem.Bullet
{
    public class CircleExplosion:AExplosion
    {
        protected override void ReadyCollider(float explosionSize)
        {
            CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
            
            circleCollider.radius = explosionSize;
        }
    }
}