using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class G3_GameManager : GameManager_Base
{
    [Header("Pengantar Pertanyaan")]
    [SerializeField] private string introQuest = "Bagaimana diagram orbital dari ";

    [Header("Answer Panel")]
    [SerializeField] private List<G3_PanelScript> panelScripts = new List<G3_PanelScript>();

    public static G3_GameManager Instance { get; private set; } = null;

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
        _lIndex = EnumLevelIndex._3;
        GameInstance.Instance.levelIndex = _lIndex;

        SetupQuestionAdnvance(introQuest);
        SetupTimer();
        GameScore();

        ClosePanelScript();
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

        for(int i=0; i<arrayKonf.Length; i++)
        {
            string subShell = arrayKonf[i].Insert(arrayKonf[i].Length - 1, "<sup>");
            subShell = subShell.Insert(subShell.Length, "</sup>");

            panelScripts[i].SetTextQuest(subShell);

            panelScripts[i].gameObject.SetActive(true);
        }
    }

    #endregion

    #region Build Answer

    public void ClosePanelScript()
    {
        foreach(G3_PanelScript ps in panelScripts) { 
            ps.gameObject.SetActive(false);
        }
    }

    public void BuildAnswer()
    {
        finalAnswer = "";

        foreach (G3_PanelScript ps in panelScripts)
        {
            finalAnswer += ps.GetPanelString();
        }
    }

    public void CheckAnswer()
    {
        TimerPause(true);
        SubmitAnswer();
    }

    #endregion
}
