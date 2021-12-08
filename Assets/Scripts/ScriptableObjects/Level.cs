using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 3)]
public class Level : ScriptableObject
{
    public int sizeX;

    public int sizeY;

    //Order of level in game
    public int index;
}
