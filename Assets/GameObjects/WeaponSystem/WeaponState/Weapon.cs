using System.Collections.Generic;
using UnityEngine;

namespace GameObjects.WeaponSystem.WeaponState
{
    public class Weapon : MonoBehaviour
    {
        private WeaponState state;

        public void SetModifiers(List<AModifier> modifiers)
        {
            foreach(AModifier modifier in modifiers)
            {
                modifier.Apply(state);
            }
        }
    }
}