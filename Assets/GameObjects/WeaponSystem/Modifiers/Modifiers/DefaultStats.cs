using System;
using UnityEngine;

namespace GameObjects.WeaponSystem.WeaponState.Modifiers
{
    [CreateAssetMenu(fileName = "DefaultStats", menuName = "WeaponModifiers/DefaultStats", order = 0)]
    public class DefaultStats:AModifier
    {
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletSize;
        [SerializeField] private float bulletDamage;
        [SerializeField] private float explosionRadius;
        
        
        public override void Apply(WeaponState state)
        {
            state.bulletSpeed = bulletSpeed;
            state.bulletSize = bulletSize;
            state.bulletDamage = bulletDamage;
            state.explosionRadius = explosionRadius;
        }
    }
}