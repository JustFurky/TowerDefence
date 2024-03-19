using TD.Managers;
using TD.Characters;
using System.Threading.Tasks;
using UnityEngine;

namespace TD.Items
{
    public class Bomb : AmmunitionBase
    {
        public LayerMask layerMask;
        public override void Fire(Transform spawnPos, Transform targetPoint)
        {
            transform.position = spawnPos.position;
            transform.LookAt(targetPoint);
            target = targetPoint;
            ReturnPoolWithDelay();
        }

        protected override void MoveToDirection()
        {
            transform.position += transform.forward * MovementSpeed;
        }

        private async void ReturnPoolWithDelay()
        {
            await Task.Delay(3000);
            ObjectPool.Instance.ReturnObjectToPool(this);
        }
        private void OnTriggerEnter(Collider other)
        {
            GiveDamage();
        }

        private void GiveDamage()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 3, layerMask);
            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent(out CharacterBase chaBase))
                {
                    chaBase.TakeDamage(BaseDamage);
                }
            }
            ObjectPool.Instance.ReturnObjectToPool(this);
        }
    }

}