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
        //������ �������� ���� �������
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

}
