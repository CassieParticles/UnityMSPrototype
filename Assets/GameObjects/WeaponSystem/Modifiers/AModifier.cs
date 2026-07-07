using UnityEngine;

namespace GameObjects.WeaponSystem.WeaponState
{
    public abstract class AModifier : ScriptableObject
    {
        public abstract void Apply(WeaponState state);
    }
}