using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class G4_MC:MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool isTrueAnswer = false;
    [SerializeField] private TextMeshProUGUI textMC;

    private string charMC = "";

    public void SetTrueAnswer(bool _isTrueAnswer)
    {
        this.isTrueAnswer = _isTrueAnswer;
    }

    public void SetTextMC(string _text)
    {
        charMC = _text;
        textMC.text = _text;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        G4_GameManager.Instance.BuildAnswer(charMC, isTrueAnswer);
    }
}
