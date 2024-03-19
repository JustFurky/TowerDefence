using UnityEngine;

[CreateAssetMenu(fileName = "AmmunitionDatas", menuName = "ObjectDatas/Ammunition")]
public class AmmunitionData : ScriptableObject
{
    public string Name;
    public float MovementSpeed;
    public int BaseDamage;
    public AmmunitionType Type;
}

public enum AmmunitionType
{
    Bullet = 0,
    Bomb = 1,
    Rocket = 2
}
