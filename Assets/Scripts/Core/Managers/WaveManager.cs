using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD.Characters;

namespace TD.Managers
{
    public class WaveManager : Singleton<WaveManager>
    {
        public WaveData waves;
        public int LastWaveIndex = 0;


        [SerializeField] private Transform[] spawnPoints = new Transform[5];
        [SerializeField] private Transform tower;

        private List<GameObject> spawnedUnits = new List<GameObject>();
        private UIManager uiManager;

        void Start()
        {
            uiManager = UIManager.Instance;
        }

        public void StartNextWave()
        {
            if (LastWaveIndex < waves.WaveList.Count)
            {
                StartCoroutine(SpawnWave());
            }
        }

        IEnumerator SpawnWave()
        {
            for (int j = LastWaveIndex; j < waves.WaveList.Count; j++)
            {
                Wave wave = waves.WaveList[j];
                UpdateUI(waves, wave);
                DataManager.Instance.SaveData();
                foreach (var unit in wave.WaveUnits)
                {
                    for (int i = 0; i < unit.Count; i++)
                    {
                        Vector3 spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                        GameObject spawnedUnit = Instantiate(unit.WaveUnitPrefab, spawnPos, Quaternion.identity);
                        spawnedUnit.GetComponent<CharacterBase>().Target = tower;
                        spawnedUnits.Add(spawnedUnit);
                        yield return new WaitForSeconds(unit.SpawnDelay);
                    }
                }
                while (spawnedUnits.Count > 0)
                {
                    yield return null;
                }
                LastWaveIndex++;
                yield return new WaitForSeconds(5f);
            }

            StartNextWave();
        }

        private void UpdateUI(WaveData waveData, Wave wave)
        {
            uiManager.UpdateWaveText(waveData.WaveList.IndexOf(wave), waveData.WaveList.Count);
            uiManager.MaxEnemyCount = MaxEnemyCount(wave);
            uiManager.UpdateWaveBar(MaxEnemyCount(wave));
        }

        private int MaxEnemyCount(Wave Data)
        {
            int total = 0;
            foreach (var item in Data.WaveUnits)
            {
                total += item.Count;
            }
            return total;
        }

        public void RemoveSpawnedUnit(GameObject unit)
        {
            spawnedUnits.Remove(unit);
            uiManager.UpdateWaveBar(spawnedUnits.Count);
        }
    }
}