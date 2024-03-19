using System.IO;
using UnityEngine;
using TD.Items;

namespace TD.Managers
{
    public class DataManager : Singleton<DataManager>
    {
        private string SavePath;
        [SerializeField] private int currency;// Editörden currency alýnabilmesi için serialize edildi
        [SerializeField] private TurretBase[] turrets = new TurretBase[3];


        private void Start()
        {
            SavePath = Application.dataPath + "/saveData.json";
            LoadData();
        }

        public void SaveData()
        {
            SaveDataModel saveData = new SaveDataModel();
            saveData.Currency = currency;
            saveData.WaveIndex = WaveManager.Instance.LastWaveIndex;
            saveData.TurretLevels = new int[turrets.Length];
            for (int i = 0; i < turrets.Length; i++)
            {
                saveData.TurretLevels[i] = turrets[i].TurretLevel;
            }
            string jsonData = JsonUtility.ToJson(saveData);
            File.WriteAllText(SavePath, jsonData);
            
        }

        public void LoadData()
        {
            if (File.Exists(SavePath))
            {
                string jsonData = File.ReadAllText(SavePath);
                SaveDataModel saveData = JsonUtility.FromJson<SaveDataModel>(jsonData);

                currency = saveData.Currency;
                TurretInitialization(saveData);
                WaveManagerStarter(saveData);
            }
            else
            {
                Debug.LogWarning("Save file not found.");
                WaveManager.Instance.StartNextWave();
            }
        }

        private void WaveManagerStarter(SaveDataModel saveData)
        {
            WaveManager.Instance.LastWaveIndex = saveData.WaveIndex;
            WaveManager.Instance.StartNextWave();
        }

        private void TurretInitialization(SaveDataModel saveData)
        {
            for (int i = 0; i < turrets.Length; i++)
            {
                TurretBase turret = turrets[i];
                turret.TurretLevel = saveData.TurretLevels[i];
                turret.Initialize();
            }
        }

        public bool CanAfford(int amount)
        {
            return amount <= currency;
        }

        public int GetCurrency()
        {
            return currency;
        }

        public void SetCurrency(int amount)
        {
            currency += amount;
        }
    }

    [System.Serializable]
    public class SaveDataModel
    {
        public int Currency;
        public int WaveIndex;
        public int[] TurretLevels;
    }
}

