using UnityEngine;
using UnityEngine.UI;

public class GM_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelPetunjuk;
    [SerializeField] private GameObject panelKeluar;
    private string urlTutorial = "htpps://youtube.com";

    #region Initial Function

    private void Start()
    {
        panelKeluar.SetActive(false);
        panelPetunjuk.SetActive(false);

        //float _value = GameInstance.Instance.GameVolume;
        float _value = StaticVolume.volume;

        slider.value = _value;
        SetVolume(_value);
    }

    #endregion

    #region Function Paneling

    public void openPanelPetunjuk(bool _active)
    {
        panelPetunjuk.SetActive(_active);
    }

    public void openPanelKeluar(bool _active)
    {
        panelKeluar.SetActive(_active);
    }

    #endregion

    #region Funcion Main


    public void exitGame()
    {
        Application.Quit();
    }

    public void openYoutubeTutorial()
    {
        Application.OpenURL(urlTutorial);
    }

    #endregion

    #region SoundSystem

    [Header("Setup Sound")]
    [SerializeField] private Slider slider;

    public void SetVolume(float sliderValue)
    {
        //GameInstance.Instance.SetVolume(sliderValue);
        StaticVolume.volume = sliderValue;
    }

    #endregion

}
