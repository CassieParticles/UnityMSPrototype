using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects.WeaponSystem.Bullet
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class AExplosion: MonoBehaviour
    {
        private List<IHittable> hittableObjects = new List<IHittable>();
        
        //Set up the size/shape of the collider
        protected abstract void ReadyCollider(float explosionSize);

        public void ReadyExplosion(float explosionSize)
        {
            ReadyCollider(explosionSize);
            
            Collider2D collider = GetComponent<Collider2D>();
            collider.isTrigger = true;
        }
        
        public void Explode()
        {
            Collider2D collider =  GetComponent<Collider2D>();
            foreach (IHittable hittableObject in hittableObjects)
            {
                //TODO: Probably needs to pass collider in
                hittableObject.ExplosionHit(collider);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IHittable hittable = other.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittableObjects.Add(hittable);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IHittable hittable = other.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittableObjects.Remove(hittable);
            }
        }
    }
}