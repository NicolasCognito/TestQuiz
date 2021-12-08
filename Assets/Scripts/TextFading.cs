using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TextFading : MonoBehaviour
{
    [SerializeField]
    private float duration;

    private void Start()
    {
        Text text = GetComponent<Text>();

        //Smoothly makes text visible 
        text.DOFade(1f, duration);
    }
}
