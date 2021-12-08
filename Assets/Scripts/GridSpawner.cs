using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GridSpawner : MonoBehaviour
{
    private Cell[,] cells;

    [SerializeField]
    private Cell cellPrefab;

    //True if win condition is presented
    private bool VictoriousCell = false;

    public event Action<Cell> OnCellCreation;

    //Check if level is able to be spawned
    private void CheckGrid(Level levelToSpawn, SpriteLibrary library)
    {
        //No zero/negative numbers
        if (levelToSpawn.sizeX <= 0 || levelToSpawn.sizeY <= 0)
        {
            throw new System.Exception("Wrong size of a level array");
        }

        //Total number of objects is greater or equal than level size
        if (levelToSpawn.sizeX * levelToSpawn.sizeY > library.sprites.Count)
        {
            throw new System.Exception("Not enough objects to fill the grid: " + levelToSpawn.sizeX * levelToSpawn.sizeY + " > " + library.sprites.Count);
        }
    }

    //Replace one of the cells with wincon if not spawned randomly
    public void CreateVictoriousCell(Level spawnedLevel, SpriteContainer winCon)
    {
        int randX = UnityEngine.Random.Range(0, spawnedLevel.sizeX);
        int randY = UnityEngine.Random.Range(0, spawnedLevel.sizeY);
        //Choose random cell in current array
        Cell chosenCell = cells[randX, randY];

        //Destroy it
        chosenCell.Destruction();


        //Replace it  (as function demands list, transform wincon into list with single object, another way was to overload function)
        Cell updatedCell = NewCellCreation(new[] { winCon }.ToList(), randX, randY, winCon);

        //Make new one victorious
        updatedCell.MakeVictorious();
    }

    public Cell NewCellCreation(List<SpriteContainer> pool, int x, int y, SpriteContainer wincon)
    {
        Cell newCell = Instantiate(cellPrefab);
        newCell.transform.parent = transform;

        //Update array
        cells[x, y] = newCell;

        //Magical numbers for visual part, otherwise would use base 
        newCell.transform.localPosition = new Vector3(x * 5, y * -5, 0f);

        //Now we will update visual part of the cell with random, not used before sprite form the pool
        int randomObjFromPool = UnityEngine.Random.Range(0, pool.Count);

        SpriteContainer obj = pool[randomObjFromPool];

        //Lets remove object to avoid duplicates
        pool.Remove(obj);
        newCell.container = obj;
        newCell.UpdateSprite();

        //Let the game know there is a new cell
        OnCellCreation?.Invoke(newCell);

        if (obj == wincon) 
        {
            newCell.MakeVictorious();
            VictoriousCell = true;
        }

        return newCell;
    }

    private void DestroyGrid()
    {
        foreach (Cell c in cells) 
            {
            c.Destruction();
            }
    }

    public void SpawnGrid(Level levelToSpawn, SpriteLibrary library, SpriteContainer winCon) 
    {
        //Destroy previous grid if any
        if (cells != null)
        {
            DestroyGrid();
        }
        //Check if able to spawn new level
        CheckGrid(levelToSpawn, library);

        //Nullify our previous win condition
        VictoriousCell = false;

        //Lets copy our library to a new list (this way we wouldn't affect our scriptable object at all)
        List<SpriteContainer> pool = new List<SpriteContainer>(library.sprites);

        cells = new Cell[levelToSpawn.sizeX, levelToSpawn.sizeY];

        for (int x = 0; x < levelToSpawn.sizeX; x++)
        {
            for (int y = 0; y < levelToSpawn.sizeY; y++)
            {
                //In most cases I would make a separate function or two here, but for a test - next lines will be enough
                NewCellCreation(pool, x, y, winCon);
            }
        }

        //Create victorious cell if none
        if (VictoriousCell == false)
        {
            CreateVictoriousCell(levelToSpawn, winCon);
        }
    }
}
