using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class KonfigurasiManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject parrentPrefab;
    public Transform buttonPos;
    public Vector2 startPos;
    public Vector2 _ofset = new Vector2(1.5f, 1.5f);

    public List<GameObject> buttons;
    public List<GameObject> randomedButtons;
    [SerializeField] List<int> list;
    public int state;
    public int[] correnctState;

    // Start is called before the first frame update
    void Start()
    {
        SpawnButton(30);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Checking(int id)
    {
        if (correnctState[state] == id)
        {
            Debug.Log("Benar");
            state++;
        }
        else
        {
            Debug.Log("salah");
        }
    }

    public void SpawnButton(int colums)
    {
        for (int row = 0; row < colums; row++)
        {
            var tempButton = Instantiate(buttonPrefab, buttonPos.position, buttonPos.transform.rotation);
            
            tempButton.transform.parent = parrentPrefab.transform;
            tempButton.name = tempButton.name + 'r' + row;
            TextMeshProUGUI childText = tempButton.GetComponentInChildren<TextMeshProUGUI>();
            
            KonfigurasiButton konfigurasiButton = tempButton.GetComponent<KonfigurasiButton>();
            konfigurasiButton.id = row;
            konfigurasiButton.konfigurasiManager = this;

            childText.text = konfigurasiButton.id.ToString();

            buttons.Add(tempButton);
        }
        
        GenerateRandom();
        SetSortRandomedListButton();
        SortChildrenByIndex();
    }

    public void SetSortRandomedListButton()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            KonfigurasiButton konfigurasiButton = buttons[i].GetComponent<KonfigurasiButton>();
            konfigurasiButton.sortList = list[i];
        }                        
    }
    public void SortChildrenByIndex()
    {
        GameObject[] a = buttons.ToArray();
        a = a.OrderBy(x => x.GetComponent<KonfigurasiButton>().sortList).ToArray();
        randomedButtons = a.ToList();

        for (int i = 0; i < randomedButtons.Count; i++)
        {
            KonfigurasiButton konfigurasiButton = randomedButtons[i].GetComponent<KonfigurasiButton>();
            konfigurasiButton.Sort();
        }
    }

    void GenerateRandom()
    {
        for (int j = 0; j < buttons.Count; j++)
        {
            int Rand = Random.Range(0, buttons.Count);
            while (list.Contains(Rand))
            {
                Rand = Random.Range(0, buttons.Count);
            }
            list.Add(Rand);
        }
    }
}
