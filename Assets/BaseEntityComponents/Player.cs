using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(IntPos))]
public class Player : MonoBehaviour
{
    [HideInInspector] public IntPos intPos;
    [HideInInspector] public Car car;

    private List<PaternCell> visualizedPaternCells = new List<PaternCell>();

    public bool PaternVizualized { get; private set; } = false;

    private void Awake()
    {
        Game.player = this;
    }

    private void Start()
    {
        car = GetComponent<Car>();
        intPos = GetComponent<IntPos>();
    }
    public void VisualizePatern()
    {
        var patern = car.GetCellPaternProjectionOnGamePlace();

        foreach (var p in patern)
        {
            visualizedPaternCells.Add(Game.PaternCellPool.SetCell(p).GetComponent<PaternCell>());
            visualizedPaternCells[visualizedPaternCells.Count - 1].OnCellSelect.AddListener(ApplySelection);
        }

        PaternVizualized = true;
    
    }

    public void ApplySelection(Vector2Int pos, bool selected)
    {
        if (selected)
        {
            foreach (var cell in visualizedPaternCells)
            {
                if (cell.pos != pos)
                {
                    cell.SetSelectedFalse(true);
                }
            }
            car.SelectedCell = pos;
            car.CellSelected = true;
        }
        else
        {
            car.CellSelected = false;
        }

    }



    public void HidePatern()
    {
        foreach (var p in visualizedPaternCells)
        {
            Game.PaternCellPool.RemoveCell(p.gameObject);
        }

        visualizedPaternCells.Clear();

        PaternVizualized = false;
    }

    public void UpdatePatern()
    {
        if (PaternVizualized)
        {
            foreach (var p in visualizedPaternCells)
            {
                Game.PaternCellPool.RemoveCell(p.gameObject);
            }

            visualizedPaternCells.Clear();

            var patern = car.GetCellPaternProjectionOnGamePlace();

            foreach (var p in patern)
            {
                visualizedPaternCells.Add(Game.PaternCellPool.SetCell(p).GetComponent<PaternCell>());
                visualizedPaternCells[visualizedPaternCells.Count - 1].OnCellSelect.AddListener(ApplySelection);
            }
        }
    }
}
