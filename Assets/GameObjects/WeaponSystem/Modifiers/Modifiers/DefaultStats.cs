using GameObjects.WeaponSystem.WeaponState;
using UnityEngine;

namespace GameObjects.WeaponSystem.Modifiers.Modifiers
{
    [CreateAssetMenu(fileName = "DefaultStats", menuName = "WeaponModifiers/DefaultStats", order = 0)]
    public class DefaultStats:AModifier
    {
        [SerializeField] private float bulletSpeed;
        [SerializeField] private int bounces;
        [SerializeField] private float gravityScale;
        
        
        [SerializeField] private float bulletSize;
        [SerializeField] private float bulletDamage;
        [SerializeField] private float explosionRadius;
        
        
        public override void Apply(WeaponState.WeaponState state)
        {
            state.bulletSpeed = bulletSpeed;
            state.bounces = bounces;
            state.gravityScale = gravityScale;
            
            state.bulletSize = bulletSize;
            state.bulletDamage = bulletDamage;
            state.explosionRadius = explosionRadius;
            
            
        }
    }
}