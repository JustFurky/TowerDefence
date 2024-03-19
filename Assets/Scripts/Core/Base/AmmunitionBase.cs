using UnityEngine;
using TD.Managers;

public abstract class AmmunitionBase : MonoBehaviour
{
    public AmmunitionData AmmunitionData;

    public float MovementSpeed;
    public int BaseDamage;
    public AmmunitionType Type;

    protected Transform target;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        MovementSpeed = AmmunitionData.MovementSpeed;
        BaseDamage = AmmunitionData.BaseDamage;
        Type = AmmunitionData.Type;
    }
    public abstract void Fire(Transform spawnPos, Transform targetPoint);

    protected virtual void OnUpdated()
    {
        MoveToDirection();
    }
    protected abstract void MoveToDirection();

    private void OnEnable()
    {
        CoreManager.FixedUpdateTick += OnUpdated;
    }
    private void OnDisable()
    {
        CoreManager.FixedUpdateTick -= OnUpdated;
    }
}
