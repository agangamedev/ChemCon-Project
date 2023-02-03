using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonfigurasiButton : MonoBehaviour
{
    public int id;
    public int sortList;
    public bool isCorrect = false;
    public KonfigurasiManager konfigurasiManager;

    public void Answer()
    {
        konfigurasiManager.Checking(id);
    }

    public void Sort()
    {
        gameObject.transform.SetAsLastSibling();
    }
}
