using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    #region Instance Management
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    public static void LoadPersistantLevel()
    {
        const string levelName = "PersistantLevel";
        
        for(int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
        {
            if (SceneManager.GetSceneAt(sceneIndex).name == levelName)
                return;
        }

        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
    }

    public static GameInstance Instance { get; private set; } = null;
    private QuestionBank quest;


    #endregion

    public bool isDebug = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError($"Found duplicate GameInstance on {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(Instance);

        if(quest == null)
            quest = FindObjectOfType<QuestionBank>();

        audioSource.clip = bgClip;
        audioSource.loop = true;
        //audioSource.volume = gameVolume;
        audioSource.Play();

        if(isDebug)
            totalRound = quest.q.Length;
    }

    public int QuestionID { get => questionID; set => questionID = value; }

    private int questionID = 0;

    private int totalRound = 5;


    public EnumLevelIndex levelIndex = EnumLevelIndex.None;

    private void FixedUpdate()
    {
        audioSource.volume = StaticVolume.volume;
    }

    #region Scoring
    public int Score { get => score; set => score = value; }

    private int score = 0;

    public void AddScore()
    {
        score += 4;
    }

    public void ResetGame()
    {
        score = 0;
        questionID= 0;
        
        if(!isDebug)
            quest.ReShuffle();
    }

    #endregion

    #region GameBacksound

    //private float gameVolume = 0.3f;
    public AudioClip BgClip { get => bgClip; set => bgClip = value; }
    //public float GameVolume { get => gameVolume; set => gameVolume = value; }

    [SerializeField] private AudioClip bgClip;
    [SerializeField] private AudioSource audioSource;

    //public void SetVolume(float _volume)
    //{
    //    audioSource.volume = _volume;
    //}

    #endregion

    #region SceneManagement

    public void NextLevel()
    {
        switch (levelIndex)
        {
            case EnumLevelIndex._1: LoadGame2(); break;
            case EnumLevelIndex._2: LoadGame3(); break;
            case EnumLevelIndex._3: LoadGame4(); break;
            case EnumLevelIndex._4: LoadGame5(); break;
            case EnumLevelIndex._5: 
                if(questionID < totalRound-1)
                {
                    questionID += 1;
                    LoadGame1();
                }
                else
                {
                    LoadGameEnd();
                }
                break;
        }
    }

    public void LoadMainMenu()
    {
        levelIndex = EnumLevelIndex.None;
        ResetGame();
        SceneManager.LoadScene("Menu");
        
    }

    public void LoadGame1()
    {
        levelIndex = EnumLevelIndex._1;
        SceneManager.LoadScene("Game1");
    }

    public void LoadGame2()
    {
        levelIndex = EnumLevelIndex._2;
        SceneManager.LoadScene("Game2");
    }

    public void LoadGame3()
    {
        levelIndex = EnumLevelIndex._3;
        SceneManager.LoadScene("Game3");
    }

    public void LoadGame4()
    {
        levelIndex = EnumLevelIndex._4;
        SceneManager.LoadScene("Game4");
    }

    public void LoadGame5()
    {
        levelIndex = EnumLevelIndex._5;
        SceneManager.LoadScene("Game5");
    }

    public void LoadGameOver()
    {
        levelIndex = EnumLevelIndex.None;
        SceneManager.LoadScene("GameOver");
    }

    public void LoadGameEnd()
    {
        levelIndex = EnumLevelIndex.None;
        SceneManager.LoadScene("GameOver");
    }
    #endregion
}

public enum EnumLevelIndex
{
    None,
    _1,
    _2,
    _3,
    _4,
    _5
}

public enum EnumTouchPhase
{
    None,
    Start,
    Drag,
    End,
}
