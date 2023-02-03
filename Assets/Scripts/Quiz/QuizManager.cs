using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAnswer> questionAnswers;
    public GameObject[] option;
    public int currentQuestion;

    public TextMeshProUGUI questionText;
    public string nextLvl;

    // Start is called before the first frame update
    void Start()
    {
        GenereteQuestion();
    }

    public void Correct()
    {
        questionAnswers.RemoveAt(currentQuestion);
        FindObjectOfType<GameManager>().score += 1;
        FindObjectOfType<GameManager>().OpenScene(nextLvl);
        /*GenereteQuestion();*/
    }
    public void InCorrect()
    {
        FindObjectOfType<GameManager>().OpenScene(nextLvl);
    }

    public void SetAnswer()
    {
        for (int i = 0; i < option.Length; i++)
        {
            option[i].GetComponent<AnswerScript>().isCorrect = false;
            option[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questionAnswers[currentQuestion].answers[i];

            if(questionAnswers[currentQuestion].correctAnswers == i)
            {
                option[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    public void GenereteQuestion()
    {
        if(questionAnswers.Count > 0)
        {
            currentQuestion = Random.Range(0, questionAnswers.Count);

            questionText.text = questionAnswers[currentQuestion].question;
            SetAnswer();
        }
        else
        {
            Debug.Log("Out of Question");
        }
    }
}
