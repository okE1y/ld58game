using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Sequence> sequences = new List<Sequence>();
    public List<GameObject> Cars = new List<GameObject>();

    private bool playerEndMove = false;
    private void Awake()
    {
        Game.GameManager = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);


        while (true) // игровой цикл
        {
            yield return PlayerMovePhase();
            yield return GamePhase();
        }
    }

    private IEnumerator PlayerMovePhase()
    {
        // Вызов интерфейса
        // передача управления над камерой

        yield return new WaitUntil(() => playerEndMove); // ждем когда игрок закончит ход
        yield break;
    }

    private IEnumerator GamePhase()
    {
        do
        {
            MakeRoutePhase();
            yield return ApplyMovePhase();
        }while (!CheckFinishCondition());
    }

    private void MakeRoutePhase()
    {
        foreach (GameObject go in Cars)
        {
            Car car = go.GetComponent<Car>();
            if (car.CellSelected) // если машинка выбрала клетку
            {
                IntPos ip = go.GetComponent<IntPos>();

                ip.CreateChangePosContext(ip.pos, car.SelectedCell); // Создаём маршрут
            }
        }
    }

    private IEnumerator ApplyMovePhase()
    {
        // Воплощяем ходы, собираем ловушки и активируем эффекты
        yield break;
    }

    private bool CheckFinishCondition()
    {
        // Проверяем условие завершения хода
        return true;
    }

    private IEnumerator FinishMove()
    {
        foreach (GameObject go in Cars)
        {
            Car car = go.GetComponent<Car>();
            car.CellSelected = false;
        }
        yield break;
    }

}
