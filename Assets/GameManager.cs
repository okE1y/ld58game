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


        while (true) // ������� ����
        {
            yield return PlayerMovePhase();
            yield return GamePhase();
        }
    }

    private IEnumerator PlayerMovePhase()
    {
        // ����� ����������
        // �������� ���������� ��� �������

        yield return new WaitUntil(() => playerEndMove); // ���� ����� ����� �������� ���
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
            if (car.CellSelected) // ���� ������� ������� ������
            {
                IntPos ip = go.GetComponent<IntPos>();

                ip.CreateChangePosContext(ip.pos, car.SelectedCell); // ������ �������
            }
        }
    }

    private IEnumerator ApplyMovePhase()
    {
        // ��������� ����, �������� ������� � ���������� �������
        yield break;
    }

    private bool CheckFinishCondition()
    {
        // ��������� ������� ���������� ����
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
