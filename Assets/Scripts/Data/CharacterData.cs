using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatas", menuName = "CharacterDatas/Character")]
public class CharacterData : ScriptableObject
{
    public float Range;
    public float Speed;
    public float FireRate;
    public int Health;
    public int Damage;
    public CharacterSide Side;
    public int Currency;
}
public enum CharacterSide
{
    Enemy,
    Friend
}
