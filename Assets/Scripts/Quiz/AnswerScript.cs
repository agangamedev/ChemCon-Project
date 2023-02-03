using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public void Answer()
    {
        if (!isCorrect)
        {
            Debug.Log("Salah");
            quizManager.InCorrect();
        }
        else
        {
            Debug.Log("Betul");
            quizManager.Correct();
        }
    }
}
