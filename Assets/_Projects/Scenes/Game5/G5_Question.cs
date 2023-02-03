using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G5_Question : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private List<G4_DiagramColumn> diagramC = new List<G4_DiagramColumn>();

    public void BuildDiagram(string _stringSubShell, int _totalElektron, bool _isTrueAnswer)
    {
        int totalRoom = 0;
        int _subShell = 0;

        if (_stringSubShell.Contains('S'))
        {
            _subShell = 0;
        }
        else if (_stringSubShell.Contains('P'))
        {
            _subShell = 1;
        }
        else if (_stringSubShell.Contains('D'))
        {
            _subShell = 2;
        }

        switch (_subShell)
        {
            case 0:
                totalRoom = 1; // for S
                CheckElektron(_totalElektron, 2);
                break;
            case 1:
                totalRoom = 3; // for P
                CheckElektron(_totalElektron, 6);
                break;
            case 2:
                totalRoom = 5; // for D
                CheckElektron(_totalElektron, 10);
                break;
            case 3:
                totalRoom = 7; // for F
                CheckElektron(_totalElektron, 14);
                break;
            default:
                totalRoom = 0;
                break;
        }

        SetRoom(totalRoom);

        for (int i = 0; i < totalRoom; i++)
        {

            if (_totalElektron > 0)
            {
                diagramC[i].DiagramUpEnable();
                _totalElektron -= 1;
            }

        }

        for (int i = 0; i < totalRoom; i++)
        {
            if (_totalElektron > 0)
            {
                diagramC[i].DiagramDownEnable();
                _totalElektron -= 1;
            }
        }
    }

    private void SetRoom(int idx)
    {
        for (int i = idx; i < diagramC.Count; i++)
        {
            diagramC[i].gameObject.SetActive(false);
        }
    }



    private static void CheckElektron(int totalElektron, int maxElektron)
    {
        if (totalElektron > maxElektron)
        {
            Debug.Log("totalElektron kelebihan");
        }
    }
}
