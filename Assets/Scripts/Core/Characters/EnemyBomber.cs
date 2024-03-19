using UnityEngine;
using TD.Managers;
using TD.Characters;

public class EnemyBomber : CharacterBase
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
            Die();
        }
    }
    protected override void Die()
    {
        WaveManager.Instance.RemoveSpawnedUnit(gameObject);
        Destroy(gameObject);
    }

    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent(out AmmunitionBase ammo))
    //    {
    //        TakeDamage(ammo.BaseDamage);
    //        ObjectPool.Instance.ReturnObjectToPool(ammo);
    //    }
    //}
}
