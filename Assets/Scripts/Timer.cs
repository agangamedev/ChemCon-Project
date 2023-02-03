using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    GameManager gameManager;
    public TextMeshProUGUI textTimer;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        textTimer.text = gameManager.time.ToString();
    }
}
