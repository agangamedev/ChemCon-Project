using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class G1_Node : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private TextMeshProUGUI txNode;

    [Header("Images")]
    [SerializeField] private Image circleImage;
    [SerializeField] private List<Sprite> spriteImages = new List<Sprite>();
    [SerializeField] private Transform spriteHover;
    private int subshellIndex;
    private int subShellElectron;
    private SubshellEnum subshellEnum;
    private bool choosen = false;

    private string subShell;

    public string SubShell { get => subShell; set => subShell = value; }

    #region Define Shell
    public void RandomNode()
    {
        subshellEnum = (SubshellEnum)(Random.Range(0, 3));
        switch (subshellEnum)
        {
            case SubshellEnum.S:
                subshellIndex = Random.Range(1, 8);
                subShellElectron = Random.Range(1, 2);
                break;
            case SubshellEnum.P:
                subshellIndex = Random.Range(2, 7);
                subShellElectron = Random.Range(1, 6);
                break;
            case SubshellEnum.D:
                subshellIndex = Random.Range(3, 6);
                subShellElectron = Random.Range(1, 10);
                break;
            case SubshellEnum.F:
                subshellIndex = Random.Range(4, 5);
                subShellElectron = Random.Range(1, 14);
                break;
        }

        SetSubShell(subshellIndex, subshellEnum, subShellElectron);
        
    }

    public void SetSubShell(int _index, SubshellEnum _enum, int _electron)
    {
        circleImage.sprite = spriteImages[Random.Range(0, spriteImages.Count)];

        subShell = _index.ToString() + _enum.ToString() + "<sup>" + _electron + "</sup>";
        txNode.text = subShell;
    }

    #endregion

    public void DebugAnswer()
    {
        this.GetComponent<Image>().color = Color.green;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(G1_GameManager.Instance.ETouchPhase == EnumTouchPhase.Drag)
        {
            if(!choosen)
                ChooseNode();
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (G1_GameManager.Instance.ETouchPhase == EnumTouchPhase.End)
        {
            if(!choosen)
                ChooseNode();
        }
    }
    private void ChooseNode()
    {
        //Debug.Log("Add Drag " + subShell);
        choosen = true;
        spriteHover.gameObject.SetActive(true);
        G1_GameManager.Instance.BuildAnswer(subShell, this.transform);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        G1_GameManager.Instance.CheckAnswer();
    }
}
