using UnityEngine;
using TD.Managers;

namespace TD.Items
{
    public abstract class TurretBase : MonoBehaviour
    {
        public int TurretLevel = 0;
        public int levelUpRequiredCurrenct;
        [SerializeField] private TurretDataSO turretData;
        [SerializeField] protected GameObject TurretMeshes;
        [SerializeField] protected Transform turretBarrel;
        [SerializeField] protected Transform turretFirePoint;

        protected float range;
        protected float fireRate;
        protected AmmunitionType ammunitionType;

        public void Initialize()
        {
            if (TurretLevel < 1)
                return;

            Debug.Log(TurretLevel);
            TurretMeshes.SetActive(true);

            range = turretData.TurretLevels[TurretLevel].Range;
            fireRate = turretData.TurretLevels[TurretLevel].FireRate;
            ammunitionType = turretData.TurretLevels[TurretLevel].AmmunitionType;
            levelUpRequiredCurrenct = turretData.TurretLevels[TurretLevel].LevelUpRequiredCurrenct;
        }
        public void UpdateLevel()
        {
            TurretLevel++;
            Initialize();
        }

        public void OnMouseEnter()
        {
            UIManager.Instance.OpenUpdatePanel(this);
        }

        protected abstract void Fire();

        protected abstract void LookEnemy();

        protected abstract void FindEnemy();
    } 
}
