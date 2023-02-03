using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelExpression : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private TextMeshProUGUI textExpression;
    [SerializeField] private Image imageExpression;
    [SerializeField] private Transform panelExpression;

    [Header("Fill")]
    [SerializeField] private List<string> textTrueExpression = new List<string>();
    [SerializeField] private List<Sprite> spriteTrueExpression = new List<Sprite>();

    public void ExpressionAnswer(bool isTrue)
    {
        if (isTrue)
        {
            textExpression.text = textTrueExpression[0];
            imageExpression.sprite = spriteTrueExpression[0];
        }
        else
        {
            textExpression.text = textTrueExpression[1];
            imageExpression.sprite = spriteTrueExpression[1];
        }

        panelExpression.gameObject.SetActive(true);
    }
}
