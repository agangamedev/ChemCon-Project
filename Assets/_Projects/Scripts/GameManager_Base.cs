using UnityEngine;
using TMPro;
using System;

public class GameManager_Base : MonoBehaviour
{
    [SerializeField] private PanelExpression expressionPanel;

    protected string finalAnswer = "";
    protected EnumLevelIndex _lIndex;
    private bool isGameOver = false;

    private void Start()
    {
        SetupQuestion(null);
        SetupTimer();
        GameScore();
    }

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            if (timer > 0)
            {
                if (!isTimerPause)
                    timer -= Time.deltaTime;
            }
            else
            {
                SubmitAnswer();
            }

        }

        UpdateTimer();
    }

    #region Setup Question
    [Header("Question")]
    [SerializeField] private TextMeshProUGUI textQuestion;

    protected void SetupQuestion(string introText)
    {
        Question quest = GetQuestion();

        if (introText == null)
        {
            textQuestion.text = quest.GetQuest();
        }
        else
        {
            textQuestion.text = introText+quest.GetQuest();
        }
        
    }

    protected void SetupQuestion(string introText, string lastText)
    {
        Question quest = GetQuestion();

        if (introText == null)
        {
            textQuestion.text = quest.GetQuest();
        }
        else
        {
            textQuestion.text = introText + quest.GetQuest() + "<br>" + lastText;
        }

    }

    protected void SetupQuestionAdnvance(string _q)
    {
        Question quest = GetQuestion();
        string konf = "";
        string[] arrayKonf;


        if (quest != null)
        {
            konf = quest.KonfElektron;
        }

        arrayKonf = konf.Split(',');
        string newSubShell = "";

        for (int i = 0; i < arrayKonf.Length; i++)
        {
            string subShell = arrayKonf[i].Insert(arrayKonf[i].Length-1, "<sup>");
            subShell = subShell.Insert(subShell.Length, "</sup>");

            newSubShell += subShell+" ";
        }

        string buildQuest = _q + quest.GetQuest() + "\n" + newSubShell.ToLower();
        textQuestion.text = buildQuest;
    }

    protected static Question GetQuestion()
    {
        int qID = GameInstance.Instance.QuestionID;
        Question quest = new Question();

        QuestionBank qBank = FindObjectOfType<QuestionBank>();
        if (qBank != null)
            quest = qBank.q[qID];
        return quest;
    }
    #endregion

    #region Answering

    [Header("Answer Question Manager")]
    [SerializeField] private TextMeshProUGUI textAnswer;
    protected void SetupAnswer(string _answer)
    {
        if (textAnswer)
        {
            textAnswer.text = _answer;
        }
        else
        {
            Debug.Log("Text Answer Does'nt Setup");
        }
    }

    protected void SubmitAnswer()
    {
        isGameOver = true;
        Question quest = GetQuestion();

        switch (_lIndex)
        {
            case EnumLevelIndex._1:
                SubmitedAnswer(finalAnswer, quest.Answer._1);
                break;
            case EnumLevelIndex._2:
                SubmitedAnswer(finalAnswer, quest.Answer._2);
            break;
            case EnumLevelIndex._3:
                SubmitedAnswer(finalAnswer, quest.Answer._3);
            break;
            case EnumLevelIndex._4:
                SubmitedAnswer(finalAnswer, quest.Answer._4);
            break;
            case EnumLevelIndex._5:
                SubmitedAnswer(finalAnswer, quest.Answer._5);
            break;
            default:
                Debug.Log("Level Index Belum di Set");
            break;
        }

        
        GameScore();
    }

    private void SubmitedAnswer(string _answer, string quest)
    {
        quest = quest.Replace(" ", "").ToLower();
        _answer = _answer.Replace(" ", "").ToLower();

        if (_answer == quest)
        {
            expressionPanel.ExpressionAnswer(true);
            GameInstance.Instance.AddScore();
        }
        else
        {
            expressionPanel.ExpressionAnswer(false);
        }

        //Debug.Log(quest);
        //Debug.Log(_answer);
    }

    #endregion

    #region Scoring
    [Header("Scoring")]
    [SerializeField] protected TextMeshProUGUI textScore;
    protected void GameScore()
    {
        textScore.text = GameInstance.Instance.Score.ToString();
    }

    #endregion

    #region Timer
    [Header("Timer")]
    [SerializeField] private float qTime = 60f;
    [SerializeField] private TextMeshProUGUI textTimer;
    private float timer;
    private bool isTimerPause = false;

    protected void SetupTimer()
    {
        timer = qTime;
    }

    protected void UpdateTimer()
    {
        textTimer.text = Mathf.Ceil(timer).ToString();
        if (timer <= 0)
        {
            timer = 0;
        }
    }

    public void TimerPause(bool isPause)
    {
        isTimerPause = isPause;
    }

    #endregion
    
    #region Paneling & Scene
    [Header("Paneling")]
    [SerializeField] private GameObject panelHint;
    [SerializeField] private GameObject panelQuitGame;
    [SerializeField] private GameObject panelConfirmQuit;
    public void CloseAnyPanel()
    {
        panelHint.SetActive(false);
        panelQuitGame.SetActive(false);
        TimerPause(false);
    }

    public void OpenPanelHint()
    {
        panelHint.SetActive(true);
        TimerPause(true);
    }

    public void OpenPanelQuit()
    {
        panelQuitGame.SetActive(true);
        TimerPause(true);
    }

    public void OpenConfirmPanelQuit()
    {
        panelConfirmQuit.SetActive(true);
    }

    public void QuitGame()
    {
        GameInstance.Instance.LoadGameOver();
    }

    public void NextLevel()
    {
        GameInstance.Instance.NextLevel();
    }

    #endregion


    #region GameVolume

    //public void SetVolume(float sliderValue)
    //{
    //    GameInstance.Instance.GameVolume = sliderValue;
    //}

    #endregion
}

