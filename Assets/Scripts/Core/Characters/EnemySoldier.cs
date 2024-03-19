using UnityEngine;
using TD.Managers;

namespace TD.Characters
{
    public class EnemySoldier : CharacterBase
    {
        public override void OnFixedUpdated()
        {
            if (IsTargetInRange(Target))
            {
                Attack();
            }
            else
            {
                Move();
            }
        }
        public override void Attack()
        {
            time += Time.deltaTime; // Her FixedUpdate çaðrýsýnda geçen zamaný hesapla

            if (time >= fireRate)
            {
                if (Target != null)
                {
                    if (Target.TryGetComponent(out CharacterBase characterBase))
                    {
                        characterBase.TakeDamage(damage);
                        if (Target == null)
                        {
                            //AreaManager.Instance.GetNewEnemy();
                        }
                    }
                    else if (Target.TryGetComponent(out Tower tower))
                    {
                        tower.TakeDamage(damage);
                    }
                }

                time = 0f;
            }
        }
        protected override void Die()
        {
            WaveManager.Instance.RemoveSpawnedUnit(gameObject);
            base.Die();
        }

        public override void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
        }
    }
}
