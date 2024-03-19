using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TD.Items;

namespace TD.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject updatePanel;
        [SerializeField] private UpdatePanel panel;
        public Image WaveBar;
        public TMP_Text WaveText;
        public int MaxEnemyCount;

        private bool isUpdatePanelOpen = false; // Panelin açýk veya kapalý olduðunu takip etmek için bir deðiþken

        public void UpdateWaveText(int currentWave, int maxWave)
        {
            WaveText.text = $"{currentWave + 1}/{maxWave}";
        }

        public void UpdateWaveBar(int livingEnemy)
        {
            WaveBar.fillAmount = 1 - ((float)livingEnemy / MaxEnemyCount);
        }

        public void ToggleUpdatePanel(TurretBase turret)
        {
            if (!isUpdatePanelOpen)
            {
                OpenUpdatePanel(turret);
            }
            else
            {
                CloseUpdatePanel();
            }
        }

        public void OpenUpdatePanel(TurretBase turret)
        {
            updatePanel.SetActive(true);
            UpdatePanelUI(turret);
            UpdateButtonListener(turret);
            isUpdatePanelOpen = true;
        }

        public void CloseUpdatePanel()
        {
            updatePanel.SetActive(false);
            isUpdatePanelOpen = false;
        }

        private void UpdateButtonListener(TurretBase turret)
        {
            panel.UpdateButton.onClick.RemoveAllListeners();
            panel.UpdateButton.onClick.AddListener(() => TryUpgradeTurret(turret));
        }

        private void TryUpgradeTurret(TurretBase turret)
        {
            DataManager dataManager = DataManager.Instance;
            if (dataManager.CanAfford(turret.levelUpRequiredCurrenct) && turret.TurretLevel < 3)
            {
                turret.UpdateLevel();
                dataManager.SetCurrency(-turret.levelUpRequiredCurrenct);
                dataManager.SaveData();
                UpdatePanelUI(turret);
            }
            else
            {
                Debug.Log("Yetersiz Para veya Max Level");
            }
        }

        private void UpdatePanelUI(TurretBase turret)
        {
            panel.CostText.text = "";
            if (turret.TurretLevel < 1)
            {
                panel.CostText.text = $"Cost: {turret.levelUpRequiredCurrenct}";
                panel.TurretLevelText.text = "Buy Turret";
                panel.ButtonText.text = "Buy!";
            }
            else if (turret.TurretLevel < 3)
            {
                panel.CostText.text = $"Cost: {turret.levelUpRequiredCurrenct}";
                panel.TurretLevelText.text = $"Level {turret.TurretLevel} to Level {turret.TurretLevel + 1}";
                panel.ButtonText.text = "Upgrade!";
            }
            else
            {
                panel.TurretLevelText.text = $"Max Level Reached";
                panel.ButtonText.text = "MAX LEVEL!";
            }
        }
    }
}

[System.Serializable]
public class UpdatePanel
{
    public Button UpdateButton;
    public TMP_Text TurretLevelText;
    public TMP_Text ButtonText;
    public TMP_Text CostText;
}
