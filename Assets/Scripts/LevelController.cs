using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private List<Level> levelsToLoad;

    [SerializeField]
    private SpriteLibrary spriteLibrary;

    [SerializeField]
    private GridSpawner grid;

    //This two fields will help us to avoid repeats
    private List<SpriteContainer> poolOfWinCons;

    private SpriteContainer currentWincon;

    //Protection from rapid clicking during win animation
    //More adequate, but longer way is to make victory cell non-interactable
    private bool LevelCompleted = false;

    [SerializeField]
    private Text textfield;

    private int currentLevelIndex = 0;

    private void Start()
    {
        //Subscribe to the cell creation from the grid
        grid.OnCellCreation += WhenCellCreated;

        poolOfWinCons = new List<SpriteContainer>(spriteLibrary.sprites);

        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        if (poolOfWinCons.Count != 0) 
        {
            int randomObjFromPool = Random.Range(0, poolOfWinCons.Count);

            Debug.Log(randomObjFromPool);

            currentWincon = poolOfWinCons[randomObjFromPool];

            poolOfWinCons.Remove(currentWincon);
        }
        else
        {
            throw new System.Exception("All objects already were used as win conditions, restart the game to continue");
        }

        if (currentLevelIndex + 1 < levelsToLoad.Count)
        {
            grid.SpawnGrid(levelsToLoad[currentLevelIndex], spriteLibrary, currentWincon);
            currentLevelIndex += 1;
        }
        else
        {
            grid.SpawnGrid(levelsToLoad[currentLevelIndex], spriteLibrary, currentWincon);
        }

        LevelCompleted = false;
    }

    public void UpdateText(string newKey)
    {
        textfield.text = ("Press " + newKey + " to win");
    }

    private void WhenCellCreated(Cell newCell) 
    {
        //subscribe to the /click/ and /chosen victorious/ events
        newCell.OnNewVictoriousCell += UpdateText;
        newCell.OnClickedVictoriousCell += LevelVictory;
        //in most cases, I would write it differently and create unsubscriber, but not in the test case
    }


    //Call this function when you succesfully finished the level
    private void LevelVictory()
    {
        //This works as a protection from rapid clicking
        if (LevelCompleted == false)
        {
            //wait until bounce and particle animation is finished
            //in most cases, I would use coroutines
            Invoke("LoadNextLevel", 2f);
            LevelCompleted = true;
        }
    }


}
