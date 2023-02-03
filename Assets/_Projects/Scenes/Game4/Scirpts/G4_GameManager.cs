using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G4_GameManager : GameManager_Base
{
    [Header("Pengantar Pertanyaan")]
    [SerializeField] private string introQuest = "Pilih orbital elektron dan diagram orbital yang menjelaskan ";

    [Header("Setup")]
    [SerializeField] private G4_PanelScript[] panelScripts = new G4_PanelScript[5];
    [SerializeField] private Transform parrentPanel;
    

    public static G4_GameManager Instance { get; private set; } = null;

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
        _lIndex = EnumLevelIndex._4;
        GameInstance.Instance.levelIndex = _lIndex;

        SetupQuestion(introQuest);
        SetupTimer();
        GameScore();

        SetQuestion();
    }


    #region Build Question

    private List<string> qMC = new List<string>();
     
    public void SetQuestion()
    {
        qMC.Clear();
        Question q = GetQuestion();
        string konf = "";
        string[] arrayKonf;

        if (q != null)
        {
            konf = q.KonfElektron;
        }

        arrayKonf = konf.Split(',');

        string lastKonf = arrayKonf[arrayKonf.Length-1].ToUpper();
        int totalElektron = int.Parse( lastKonf.Substring(lastKonf.Length - 1,1));

        panelScripts[0].BuildDiagram(lastKonf, totalElektron, true);
        qMC.Add(lastKonf);
        SetAnotherQuestion(lastKonf);
    }

    public void SetAnotherQuestion(string _trueSubShell)
    {
        for(int i=1; i<panelScripts.Length; i++)
        {
            SetSubstring(_trueSubShell, i);
        }

        RandomizeQuestion();
    }

    private void SetSubstring(string _trueSubShell, int i)
    {
        int num = Random.Range(1, 7);
        int numSubShell = Random.Range(0, 2);
        string stringSubShell = "";
        int totalElektron = 0;

        switch (numSubShell)
        {
            case 0: totalElektron = Random.Range(1, 2); stringSubShell = num.ToString() + "S" + totalElektron; break;
            case 1: totalElektron = Random.Range(1, 6); stringSubShell = num.ToString() + "P" + totalElektron; break;
            case 2: totalElektron = Random.Range(1, 10); stringSubShell = num.ToString() + "D" + totalElektron; break;
        }

        if (!qMC.Contains(stringSubShell))
        {
            panelScripts[i].BuildDiagram(stringSubShell, totalElektron, false);
        }
        else
        {
            Debug.Log("Ganti Soal ");
            SetSubstring(_trueSubShell, i);
        }
    }

    public void RandomizeQuestion()
    {
        Shuffle<G4_PanelScript>(panelScripts);

        for(int i=0; i<panelScripts.Length; i++)
        {
            char _mc = (char)(65 + i);
            panelScripts[i].SetMultipleChoice(_mc.ToString());
            panelScripts[i].transform.SetSiblingIndex(i);
        }
    }

    #endregion

    #region Build Answer

    public void BuildAnswer(string _char, bool _answer)
    {
        SetupAnswer(_char);

        if (_answer)
        {
            finalAnswer = "true";
        }
        else
        {
            finalAnswer = "false";
        }
    }

    public void CheckAnswer()
    {
        TimerPause(true);
        SubmitAnswer();
    }

    #endregion

    #region Algorithm

    static void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = n-1; i>0; i--)
        {
            // Use Next on random instance with an argument.
            // ... The argument is an exclusive bound.
            //     So we will not go past the end of the array.
            int r = Random.Range(0, i);
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }

    #endregion
}
