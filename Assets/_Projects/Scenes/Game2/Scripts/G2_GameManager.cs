using TMPro;
using UnityEngine;

public class G2_GameManager : GameManager_Base
{
    [Header("Pengantar Pertanyaan")]
    [SerializeField] private string introQuest = "Tentukan Golongan dan Periode dari ";

    public static G2_GameManager Instance { get; private set; } = null;

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

    void Start()
    {
        _lIndex = EnumLevelIndex._2;
        GameInstance.Instance.levelIndex = _lIndex;

        SetupQuestionAdnvance(introQuest);
        SetupTimer();
        GameScore();
    }

    #region Build Answer
    [Header("Build Answer")]
    [SerializeField] private TextMeshProUGUI txAnsGolongan;
    [SerializeField] private TextMeshProUGUI txAnsPeriode;
    private string aGol = "";
    private string aPer = "";

    public void BuildAnswerGolongan(string gol)
    {
        aGol = gol;
        txAnsGolongan.text = gol;
        finalAnswer = aGol + aPer;
        //CheckAnswer();
    }

    public void BuildAnswerPeriode(string per)
    {
        aPer = per;
        txAnsPeriode.text = per;
        finalAnswer = aGol + aPer;
        //CheckAnswer();
    }

    public void CheckAnswer()
    {
        TimerPause(true);
        //string submitAnswer = finalAnswer.Replace(" ", "");
        //finalAnswer = submitAnswer;
        //Debug.Log(submitAnswer);
        SubmitAnswer();

        finalAnswer = "";
    }

    #endregion

}
