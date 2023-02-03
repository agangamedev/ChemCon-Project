using UnityEngine;

public class G1_GameManager : GameManager_Base
{
    public static G1_GameManager Instance { get; private set; } = null;
    private G1_LineController lineControl;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Found duplicate GameInstance on {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        lineControl = FindObjectOfType<G1_LineController>();
    }

    private void Start()
    {
        _lIndex = EnumLevelIndex._1;
        GameInstance.Instance.levelIndex = _lIndex;

        ETouchPhase = EnumTouchPhase.End;
        SetupQuestion(null);
        SetupTimer();
        GameScore();

        //for debug only
        //GameInstance.Instance.levelIndex = ELevelIndex._1;
    }

    #region TouchPhase

    public EnumTouchPhase ETouchPhase { get => eTouchPhase; set => eTouchPhase = value; }

    private EnumTouchPhase eTouchPhase;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            eTouchPhase = EnumTouchPhase.Start;
        }
        if (Input.GetMouseButton(0))
        {
            eTouchPhase = EnumTouchPhase.Drag;
        }
        if (Input.GetMouseButtonUp(0))
        {
            eTouchPhase = EnumTouchPhase.End;
        }
    }

    #endregion

    #region Build Answer
    private string answer = "";

    public void BuildAnswer(string _addNode, Transform node)
    {
        //string addNode = _addNode.Insert(_addNode.Length - 1, "<sup>");
        //addNode = addNode.Insert(addNode.Length, "</sup>");


        answer += _addNode + " ";
        SetupAnswer(answer);
        lineControl.SetUpLine(node);
    }

    public void CheckAnswer()
    {
        TimerPause(true);

        string submitAnswer = answer.Replace(" ", ",").Replace("<sup>","").Replace("</sup>","");
        submitAnswer = submitAnswer.Remove(submitAnswer.Length-1);
        //Debug.Log(submitAnswer);
        finalAnswer = submitAnswer.ToUpper();
        SubmitAnswer();

        answer = "";
    }

    #endregion
}

