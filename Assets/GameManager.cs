using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Sequence> sequences = new List<Sequence>();
    private void Awake()
    {
        Game.GameManager = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        GameObject car = GameObject.Find("FirstCar");
        car.GetComponent<IntPos>().CreateChangePosContext(car.GetComponent<IntPos>().pos, new Vector2Int(5, 1));
        car.GetComponent<IntPos>().CreateChangePosContext(new Vector2Int(5, 1), new Vector2Int(8, 3));


        car.GetComponent<EntityRenderer>().ChangePosition();
        car.GetComponent<IntPos>().ApplyMove();
    }
}
