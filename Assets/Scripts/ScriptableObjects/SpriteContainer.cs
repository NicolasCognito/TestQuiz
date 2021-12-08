using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprite w/ key", menuName = "ScriptableObjects/CellObject", order = 2)]
public class SpriteContainer : ScriptableObject
{
    //Line of text associated w/ image
    [SerializeField]
    public string key;


    //Image itself
    [SerializeField]
    public Sprite sprite;
}