using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer3Script : MonoBehaviour
{
    public bool isCorrect = false;
    public Quiz3Manager quiz3Manager;

    public void Answer()
    {
        if (!isCorrect)
        {
            Debug.Log("Salah");
            quiz3Manager.InCorrect();
            GetComponent<Button>().interactable = false;
        }
        else
        {
            Debug.Log("Betul");
            quiz3Manager.Correct();
            GetComponent<Button>().interactable = false;
        }
    }
}
