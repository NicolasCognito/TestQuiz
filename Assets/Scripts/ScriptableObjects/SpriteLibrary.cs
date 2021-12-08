using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Set", menuName = "ScriptableObjects/Set", order = 1)]
public class SpriteLibrary : ScriptableObject
{
    //Name of the set (numbers, letters, e.g.)
    public string Name;


    //Containment of the set
    public List<SpriteContainer> sprites;
}

