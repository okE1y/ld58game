using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlace
{
    public static GamePlace instance = new GamePlace(); // Гейм плейс - это синглтон

    public Vector2 cellSize { get; } = new Vector2(1f, 1f);

    public Cell[,] Place = new Cell[200, 6];
    private GamePlace()
    {
        for (int i = 0; i < Place.GetLength(0); i++)
        {
            for(int j = 0; j < Place.GetLength(1); j++)
            {
                Place[i, j] = new Cell(i, j);
            }
        }
    }

}

public class Cell
{
    public Vector2Int pos { get; }

    public Dictionary<string, GameObject> Entities = new Dictionary<string, GameObject>();

    public Cell(int x, int y)
    {
        pos = new Vector2Int(x, y);
    }

    public bool AssignCell(GameObject entity) // Возвращает индекс
    {
        if (entity.TryGetComponent<IntPos>(out IntPos intPos))
        {
            if (Entities.ContainsKey(entity.name))
            {
                return false;
            }
            else
            {
                Entities.Add(entity.name, entity);
                return true;
            }
        }
        else
        {
            throw new System.Exception("неправильный объект в клету");
        }
    }

    public bool ResignCell(string name) => Entities.Remove(name);
}