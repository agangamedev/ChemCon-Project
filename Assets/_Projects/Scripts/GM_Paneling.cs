using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_Paneling : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameManager_Base gameManager;

    [Header("Volume")]
    [SerializeField] private Slider slider;

    private void Awake()
    {
        if (!gameManager)
        {
            gameManager = FindObjectOfType<GameManager_Base>();
        }
    }

    private void Start()
    {
        if (slider)
            slider.value = StaticVolume.volume;
        //slider.value = GameInstance.Instance.GameVolume;
    }

    private void FixedUpdate()
    {
        //slider.value = GameInstance.Instance.GameVolume;
    }

    #region Paneling & Scene

    public void CloseAnyPanel()
    {
        gameManager.CloseAnyPanel();
    }

    public void OpenPanelHint()
    {
        gameManager.OpenPanelHint();
    }

    public void OpenPanelQuit()
    {
        gameManager.OpenPanelQuit();
    }

    public void OpenConfirmPanelQuit()
    {
        gameManager.OpenConfirmPanelQuit();
    }

    public void QuitGame()
    {
        GameInstance.Instance.LoadGameOver();
    }

    public void NextLevel()
    {
        gameManager.NextLevel();
    }

    #endregion



    #region Volume Slider

    public void SetVolume(float sliderValue)
    {
        //GameInstance.Instance.SetVolume(sliderValue);
        StaticVolume.volume = sliderValue;
    }

    #endregion
}
