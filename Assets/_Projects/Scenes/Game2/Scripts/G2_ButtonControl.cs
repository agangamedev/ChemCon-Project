using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class G2_ButtonControl : MonoBehaviour
{
    [Header("Button Collection")]
    [SerializeField] private List<Button> buttonGolonganRomawi;
    [SerializeField] private List<Button> buttonGolonganHuruf;
    [SerializeField] private List<Button> buttonPeriode;

    private string golRomawi = "";
    private string golHuruf = "";
    private string periode = "";

    public void ClickGolRomawi(string _fill)
    {
        golRomawi = _fill;
        CheckGolongan();

        //DisableButton(buttonGolonganRomawi);
    }

    

    public void ClickGolHuruf(string _fill)
    {
        golHuruf = _fill;
        CheckGolongan();

        //DisableButton(buttonGolonganHuruf);
    }

    private void CheckGolongan()
    {
        string gol = golRomawi + golHuruf;
        G2_GameManager.Instance.BuildAnswerGolongan(gol);
    }

    public void ClickPeriode(string _fill)
    {
        periode = _fill;
        G2_GameManager.Instance.BuildAnswerPeriode(periode);

        //DisableButton(buttonPeriode);
    }

    private void DisableButton(List<Button> listButton)
    {
        for (int i = 0; i < listButton.Count; i++)
        {
            listButton[i].interactable = false;
        }
    }
}
