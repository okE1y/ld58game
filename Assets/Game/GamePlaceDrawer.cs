using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlaceDrawer : MonoBehaviour
{

    private GameObject[,] cellSprites;

    public Transform Ceils;

    void Start()
    {
        cellSprites = new GameObject[30, GamePlace.instance.Place.GetLength(1)];
        GameObject cellSpritePrefab = "Prefabs/Cell".load<GameObject>();


        for (int i = 0; i < cellSprites.GetLength(0); i++)
        {
            for (int j = 0; j < cellSprites.GetLength(1); j++)
            {
                cellSprites[i, j] = Instantiate(cellSpritePrefab, Ceils);
                cellSprites[i, j].transform.localPosition = GamePlace.instance.cellSize * new Vector3(i, j);
            }
        }
    }
}
