using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background_Randomize : MonoBehaviour
{
    [SerializeField] private List<Sprite> bgSprite = new List<Sprite>();
    
    [SerializeField] private Image img;

    private void Awake()
    {
        if(img == null)
            img = GetComponent<Image>();
    }

    private void Start()
    {
        img.sprite = bgSprite[Random.Range(0, bgSprite.Count)];
    }
}
