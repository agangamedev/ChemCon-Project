using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer2Script : MonoBehaviour
{
    public bool isCorrect = false;
    public Quiz2Manager quiz2Manager;

    public void Answer()
    {
        if (!isCorrect)
        {
            Debug.Log("Salah");
            quiz2Manager.InCorrect();
            GetComponent<Button>().interactable = false;
        }
        else
        {
            Debug.Log("Betul");
            quiz2Manager.Correct();
            GetComponent<Button>().interactable = false;
        }
    }
}
