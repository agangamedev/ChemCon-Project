using UnityEngine;

public class SceneControl : MonoBehaviour
{
    public void LoadMainMenu()
    {
        GameInstance.Instance.LoadMainMenu();
    }

    public void LoadGame1()
    {
        GameInstance.Instance.LoadGame1();
    }
}
