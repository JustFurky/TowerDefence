using TD.Managers;
using TD.Characters;
using UnityEngine;

namespace TD.Items
{
    public class Bullet : AmmunitionBase
    {
        public override void Fire(Transform spawnPos, Transform targetPoint)
        {
            transform.position = spawnPos.position;
            transform.LookAt(targetPoint);
            target = targetPoint;
            Invoke("ReturnPoolWithDelay", 3);
        }

        protected override void MoveToDirection()
        {
            transform.position += transform.forward * MovementSpeed;
        }

        private void ReturnPoolWithDelay()
        {
            ObjectPool.Instance.ReturnObjectToPool(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterBase character))
            {
                GiveDamage(character);
            }
        }

        private void GiveDamage(CharacterBase character)
        {
            character.TakeDamage(BaseDamage);
            ObjectPool.Instance.ReturnObjectToPool(this);
        }
    }
}
