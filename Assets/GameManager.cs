using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Sequence> sequences = new List<Sequence>();

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
        //Строим маршруты всех машинок
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

}
