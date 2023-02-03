using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G4_DiagramColumn : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject diagramUp;
    [SerializeField] private GameObject diagramDown;
    [SerializeField] private G4_PanelScript panelScript;
    [SerializeField] private EnumColumnType eColType;
    private enum EnumColumnType
    {
        col1, col2, col3, col4, col5
    }

    private void Awake()
    {
        DisableDiagram();
    }

    public void DisableDiagram()
    {
        diagramUp.SetActive(false);
        diagramDown.SetActive(false);
    }

    public void DiagramUpEnable()
    {
        diagramUp.SetActive(true);
    }

    public void DiagramDownEnable()
    {
        diagramDown.SetActive(true);
    }
}
