using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneScript : MonoBehaviour
{
    #region Public
    [SerializeField] private Image bar1;
    [SerializeField] private Image bar2;
    [SerializeField] private TextMeshProUGUI barValue;
    #endregion

    private void Start()
    {
        #region Start
        Application.targetFrameRate = 60;
        LoadSceneAsync();
        #endregion
    }

    public void LoadSceneAsync()
    {
        StartCoroutine(LoadSceneAsyncCoroutine());
    }

    private IEnumerator LoadSceneAsyncCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level 1");

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Yükleniyor: " + (progress * 100) + "%");


            bar1.fillAmount = progress;
            bar2.fillAmount = progress;
            barValue.text = "%" + Mathf.RoundToInt(progress * 100);

            yield return null;
        }
    }

}
