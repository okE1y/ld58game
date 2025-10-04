using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(IntPos))]
public class Player : MonoBehaviour
{
    private IntPos intPos;
    private Car car;

    private List<PaternCell> visualizedPaternCells = new List<PaternCell>();

    private void Start()
    {
        car = GetComponent<Car>();
        intPos = GetComponent<IntPos>();

        VisualizePatern();
    }
    public void VisualizePatern()
    {
        var patern = car.GetCellPaternProjectionOnGamePlace();

        Debug.Log(intPos.pos);

        foreach (var p in patern)
        {
            visualizedPaternCells.Add(Game.PaternCellPool.SetCell(p).GetComponent<PaternCell>());
        }
    }
}
