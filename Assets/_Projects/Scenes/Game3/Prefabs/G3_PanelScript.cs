using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class G3_PanelScript : MonoBehaviour
{
    public TextMeshProUGUI textSubShell;

    [SerializeField] private List<G3_DiagramColumn> dc = new List<G3_DiagramColumn>();

    private string panelString = "0000000000";

    private string col1 = "00";
    private string col2 = "00";
    private string col3 = "00";
    private string col4 = "00";
    private string col5 = "00";

    public string Col1 { get => col1; set => col1 = value; }
    public string Col2 { get => col2; set => col2 = value; }
    public string Col3 { get => col3; set => col3 = value; }
    public string Col4 { get => col4; set => col4 = value; }
    public string Col5 { get => col5; set => col5 = value; }

    public void UpdatePanelString()
    {
        panelString = Col1+Col2+Col3+Col4+Col5;
        G3_GameManager.Instance.BuildAnswer();
    }

    public void SetTextQuest(string _text)
    {
        textSubShell.text = _text;
        string tx = _text.ToLower().Replace("<sup>","").Replace("</sup>", "");

        if (tx.Contains('s'))
        {
            SetRoom(1);
        }

        if (tx.Contains('p'))
        {
            SetRoom(3);
        }
    }

    private void SetRoom(int idx)
    {
        for (int i = idx; i < dc.Count; i++)
        {
            dc[i].GetComponent<Image>().enabled = false;
        }
    }

    public string GetPanelString()
    {
        return panelString;
    }
}

