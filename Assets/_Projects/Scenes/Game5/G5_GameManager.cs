using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class G5_GameManager : GameManager_Base
{
    [Header("Pengantar Pertanyaan")]
    [SerializeField] private string introQuest = "Tentukan nilai bilangan kuantum dari ";

    [Header("Setup")]
    [SerializeField] private G5_Question qPanel;
    [SerializeField] private TextMeshProUGUI buttonTextLastGame;
    public static G5_GameManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Found duplicate GameInstance on {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }

    private void Start()
    {
        _lIndex = EnumLevelIndex._5;
        GameInstance.Instance.levelIndex = _lIndex;

        if(qPanel!= null)
        {
            qPanel = FindObjectOfType<G5_Question>();
        }

        SetupTimer();
        GameScore();

        SetQuestion();
    }

    #region Build Question

    public void SetQuestion()
    {
        Question q = GetQuestion();
        string konf = "";
        string[] arrayKonf;

        if (q != null)
        {
            konf = q.KonfElektron;
        }

        arrayKonf = konf.Split(',');

        string lastKonf = arrayKonf[arrayKonf.Length - 1].ToUpper();
        int totalElektron = int.Parse(lastKonf.Substring(lastKonf.Length - 1, 1));
        qPanel.BuildDiagram(lastKonf, totalElektron, true);

        string subShell = lastKonf.Insert(lastKonf.Length - 1, "<sup>");
        SetupQuestion(introQuest, subShell);

    }

    #endregion

    #region Build Answer

    private string ann = "";
    private string anl = "";
    private string anm = "";
    private string ans = "";

    [Header("Answer Text")]
    [SerializeField] private TextMeshProUGUI t_ann;
    [SerializeField] private TextMeshProUGUI t_anl;
    [SerializeField] private TextMeshProUGUI t_anm;
    [SerializeField] private TextMeshProUGUI t_ans;

    public void SetAnswerN(string _answer)
    {
        ann = _answer;
        t_ann.text= ann;
        BuildAnswer();
    }

    public void SetAnswerL(string _answer)
    {
        anl = _answer;
        t_anl.text = anl ;
        BuildAnswer();
    }

    public void SetAnswerM(string _answer)
    {
        anm = _answer;
        t_anm.text = anm;
        BuildAnswer();
    }

    public void SetAnswerS(string _answer)
    {
        ans = _answer;
        t_ans.text = ans;
        BuildAnswer();
    }

    public void BuildAnswer()
    {
        finalAnswer = ann+anl+anm+ans;
    }

    public void CheckAnswer()
    {
        TimerPause(true);
        SubmitAnswer();
        buttonTextLastGame.text = "Selesai";
    }

    #endregion
}
