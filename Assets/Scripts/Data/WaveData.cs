using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WaveDatas", menuName = "WaveDatas/EnemyWaveData")]
public class WaveData : ScriptableObject
{
    public List<Wave> WaveList = new List<Wave>();
}

[System.Serializable]
public class Wave
{
    public List<WaveUnit> WaveUnits = new List<WaveUnit>();
}

[System.Serializable]
public class WaveUnit
{
    public GameObject WaveUnitPrefab;
    public int Count;
    public float SpawnDelay;
}
