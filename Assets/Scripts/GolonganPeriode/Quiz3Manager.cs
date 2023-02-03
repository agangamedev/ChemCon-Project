using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz3Manager : MonoBehaviour
{
    public List<Question3Answer> question3Answers;
    public GameObject[] option;
    public GameObject[] option2;
    public GameObject[] option3;
    public int currentQuestion;

    public TextMeshProUGUI questionText;
    public string nextLvl;
    public int pointLevel;

    // Start is called before the first frame update
    void Start()
    {
        GenereteQuestion();
    }
    private void Update()
    {
        if (pointLevel == 3)
        {
            FindObjectOfType<GameManager>().score += 1;
            FindObjectOfType<GameManager>().OpenScene(nextLvl);
        }
    }

    public void Correct()
    {
        pointLevel += 1;
    }
    public void InCorrect()
    {
        FindObjectOfType<GameManager>().OpenScene(nextLvl);
    }

    public void SetAnswer()
    {
        for (int i = 0; i < option.Length; i++)
        {
            option[i].GetComponent<Answer3Script>().isCorrect = false;
            option[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question3Answers[currentQuestion].answers[i];

            if (question3Answers[currentQuestion].correctAnswers == i)
            {
                option[i].GetComponent<Answer3Script>().isCorrect = true;
            }
        }
    }
    public void SetAnswer2()
    {
        for (int i = 0; i < option2.Length; i++)
        {
            option2[i].GetComponent<Answer3Script>().isCorrect = false;
            option2[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question3Answers[currentQuestion].answers2[i];

            if (question3Answers[currentQuestion].correctAnswers2 == i)
            {
                option2[i].GetComponent<Answer3Script>().isCorrect = true;
            }
        }
    }

    public void SetAnswer3()
    {
        for (int i = 0; i < option3.Length; i++)
        {
            option3[i].GetComponent<Answer3Script>().isCorrect = false;
            option3[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question3Answers[currentQuestion].answers3[i];

            if (question3Answers[currentQuestion].correctAnswers3 == i)
            {
                option3[i].GetComponent<Answer3Script>().isCorrect = true;
            }
        }
    }

    public void GenereteQuestion()
    {
        if (question3Answers.Count > 0)
        {
            currentQuestion = Random.Range(0, question3Answers.Count);

            questionText.text = question3Answers[currentQuestion].question;
            SetAnswer();
            SetAnswer2();
            SetAnswer3();
        }
        else
        {
            Debug.Log("Out of Question");
        }
    }
}
