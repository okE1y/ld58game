using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(IntPos))]
public class Car : MonoBehaviour
{
    private IntPos intPos;

    public Vector2Int SelectedCell;
    public bool CellSelected = false;

    public List<Vector2Int> CellPatern = new List<Vector2Int>();

    private Vector2 Direction;
    private int Speed; // –ассто€ние селл патерна от машинки по направлению Direction

    private void Start()
    {
        intPos = GetComponent<IntPos>();
        Game.GameManager.Cars.Add(gameObject);
    }

    public List<Vector2Int> GetCellPaternProjectionOnGamePlace()
    {
        List<Vector2Int> temp = new List<Vector2Int>();
        temp.AddRange(CellPatern);

        temp.ForEach((x) => x += intPos.pos + new Vector2Int(Mathf.FloorToInt(Direction.x * Speed), Mathf.FloorToInt(Direction.y * Speed)));

        return temp.FindAll((x) => 0 < x.x + 1 && x.x + 1 < GamePlace.instance.Place.GetLength(0) &&
        0 < x.y + 1 && x.y + 1 < GamePlace.instance.Place.GetLength(1));

    }

    public void SelectOrDeselectCell(Vector2Int cell, bool selected)
    {
        SelectedCell = cell;
        CellSelected = selected;
    }
}
