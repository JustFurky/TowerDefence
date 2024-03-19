using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "ObjectDatas/Turret")]
public class TurretDataSO : ScriptableObject
{
    public List<TurretData> TurretLevels = new List<TurretData>();
}
[System.Serializable]
public class TurretData
{
    public int TurretLevel;
    public float Range;
    public float FireRate;
    public int LevelUpRequiredCurrenct;
    public AmmunitionType AmmunitionType;
}
