using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G3_DiagramColumn : MonoBehaviour
{
    [SerializeField] private GameObject diagramUp;
    [SerializeField] private GameObject diagramDown;
    [SerializeField] private G3_PanelScript panelScript;
    [SerializeField] private EnumColumnType eColType;

    private enum EnumColumnType
    {
        col1, col2, col3, col4, col5
    }

    private string up = "0";
    private string down = "0";

    private void Awake()
    {
        panelScript = GetComponentInParent<G3_PanelScript>();
    }

    private void Start()
    {
        DisableDiagram();
    }

    public void DiagramUpEnable()
    {
        diagramUp.SetActive(true);
        up = "1";
        GenerateCol();
    }
    public void DiagramDownEnable()
    {
        diagramDown.SetActive(true);
        down = "1";
        GenerateCol();
    }

    public void DisableDiagram()
    {
        diagramUp.SetActive(false);
        diagramDown.SetActive(false);
        up = "0";
        down = "0";
        GenerateCol();
    }

    private void GenerateCol()
    {
        string colString = up + down;

        switch(eColType)
        {
            case EnumColumnType.col1: panelScript.Col1 = colString; break;
            case EnumColumnType.col2: panelScript.Col2 = colString; break;
            case EnumColumnType.col3: panelScript.Col3 = colString; break;
            case EnumColumnType.col4: panelScript.Col4 = colString; break;
            case EnumColumnType.col5: panelScript.Col5 = colString; break;
        }

        panelScript.UpdatePanelString();
    }
}
