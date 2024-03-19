using System.Collections.Generic;
using UnityEngine;

namespace TD.Managers
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        public int PoolCount = 10;
        [SerializeField] private List<AmmunitionBase> ammoPrefabs;
        private Dictionary<AmmunitionType, Queue<AmmunitionBase>> pools = new Dictionary<AmmunitionType, Queue<AmmunitionBase>>();

        protected override void AwakeSingleton()
        {
            foreach (var ammoPrefab in ammoPrefabs)
            {
                CreatePool(ammoPrefab);
            }
        }

        private void CreatePool(AmmunitionBase ammoPrefab)
        {
            var queue = new Queue<AmmunitionBase>();

            for (int i = 0; i < PoolCount; i++)
            {
                AmmunitionBase clone = Instantiate(ammoPrefab, transform);
                clone.gameObject.SetActive(false);
                queue.Enqueue(clone);
            }

            pools[ammoPrefab.Type] = queue;
        }

        public AmmunitionBase GetPooledObject(AmmunitionType type)
        {
            if (pools.TryGetValue(type, out var queue) && queue.Count > 0)
            {
                var ammo = queue.Dequeue();
                ammo.gameObject.SetActive(true);
                return ammo;
            }
            else
            {
                return null;
            }
        }

        public void ReturnObjectToPool(AmmunitionBase ammunitionBase)
        {
            ammunitionBase.gameObject.SetActive(false);
            if (pools.TryGetValue(ammunitionBase.Type, out var queue))
            {
                queue.Enqueue(ammunitionBase);
            }
        }
    } 
}
