using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private TextMeshProUGUI textScore;

    private void Start()
    {
        SetScore();
    }

    public void SetScore()
    {
        textScore.text = GameInstance.Instance.Score.ToString();
    }

    public void BackToMainMenu()
    {
        GameInstance.Instance.LoadMainMenu();
    }
}
