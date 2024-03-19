using UnityEngine;
using TD.Managers;
using System.Linq;

namespace TD.Items
{
    public class MachineGunTurret : TurretBase
    {
        public LayerMask enemyLayer;
        private Transform target;

        private ObjectPool pool;
        private float lastFireTime = 0f;

        private void Start()
        {
            pool = ObjectPool.Instance;
            Initialize();
        }

        void OnUpdated()
        {
            LookEnemy();
        }

        void OnFixedUpdated()
        {
            if (target != null)
            {
                Fire();
            }
            else
            {
                FindEnemy();
            }
        }

        protected override void Fire()
        {
            float timeSinceLastFire = Time.time - lastFireTime;

            if (timeSinceLastFire >= fireRate)
            {
                AmmunitionBase bullet = pool.GetPooledObject(ammunitionType);
                bullet.Fire(turretFirePoint, target);
                lastFireTime = Time.time;
            }
        }

        protected override void LookEnemy()
        {
            turretBarrel.LookAt(target, Vector3.up);
        }

        protected override void FindEnemy()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyLayer);
            if (hits.Length > 0)
            {
                Collider closest = hits.OrderBy(h => (h.transform.position - transform.position).sqrMagnitude).First();
                target = closest.transform;
            }
            else
            {
                target = null;
            }
        }

        private void OnEnable()
        {
            CoreManager.FixedUpdateTick += OnFixedUpdated;
            CoreManager.UpdateTick += OnUpdated;
        }

        private void OnDisable()
        {
            CoreManager.FixedUpdateTick -= OnFixedUpdated;
            CoreManager.UpdateTick -= OnUpdated;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    } 
}
