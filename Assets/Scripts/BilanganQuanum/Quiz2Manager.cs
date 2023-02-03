using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz2Manager : MonoBehaviour
{
    public List<Question2Answer> question2Answers;
    public GameObject[] option;
    public GameObject[] option2;
    public GameObject[] option3;
    public GameObject[] option4;
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
        if (pointLevel == 4)
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
            option[i].GetComponent<Answer2Script>().isCorrect = false;
            option[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question2Answers[currentQuestion].answers[i];

            if (question2Answers[currentQuestion].correctAnswers == i)
            {
                option[i].GetComponent<Answer2Script>().isCorrect = true;
            }
        }
    }
    public void SetAnswer2()
    {
        for (int i = 0; i < option2.Length; i++)
        {
            option2[i].GetComponent<Answer2Script>().isCorrect = false;
            option2[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question2Answers[currentQuestion].answers2[i];

            if (question2Answers[currentQuestion].correctAnswers2 == i)
            {
                option2[i].GetComponent<Answer2Script>().isCorrect = true;
            }
        }
    }

    public void SetAnswer3()
    {
        for (int i = 0; i < option3.Length; i++)
        {
            option3[i].GetComponent<Answer2Script>().isCorrect = false;
            option3[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question2Answers[currentQuestion].answers3[i];

            if (question2Answers[currentQuestion].correctAnswers3 == i)
            {
                option3[i].GetComponent<Answer2Script>().isCorrect = true;
            }
        }
    }

    public void SetAnswer4()
    {
        for (int i = 0; i < option4.Length; i++)
        {
            option4[i].GetComponent<Answer2Script>().isCorrect = false;
            option4[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question2Answers[currentQuestion].answers4[i];

            if (question2Answers[currentQuestion].correctAnswers4 == i)
            {
                option4[i].GetComponent<Answer2Script>().isCorrect = true;
            }
        }
    }

    public void GenereteQuestion()
    {
        if (question2Answers.Count > 0)
        {
            currentQuestion = Random.Range(0, question2Answers.Count);

            questionText.text = question2Answers[currentQuestion].question;
            SetAnswer();
            SetAnswer2();
            SetAnswer3();
            SetAnswer4();
        }
        else
        {
            Debug.Log("Out of Question");
        }
    }
}
