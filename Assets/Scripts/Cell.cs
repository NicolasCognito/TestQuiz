using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    //Fields
    public SpriteContainer container;

    private bool IsVictorious = false;

    public event Action OnClickedVictoriousCell;

    public event Action OnClickedWrongCell;

    public event Action<string> OnNewVictoriousCell;

    //Constructor
    Cell(SpriteContainer spriteContainer) 
    {
        container = spriteContainer;
    }


    //Methods

    //Update visual part
    public void UpdateSprite()
    {
        CellVisualEffect renderer = GetComponentInChildren<CellVisualEffect>();

        renderer.SetSprite(container.sprite);
    }


    //Set victorious and trigger event to update level goal
    public void MakeVictorious() 
    {
        IsVictorious = true;
        OnNewVictoriousCell?.Invoke(container.key);
    }

    public void OnMouseDown()
    {
        if (IsVictorious == true)
        {
            OnClickedVictoriousCell();
        }
        else
        {
            OnClickedWrongCell();
        }
        Debug.Log("Click!");
    }

    public void Destruction() 
    {
        Destroy(gameObject);
    }
}
