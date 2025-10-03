using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public partial class IntPos : MonoBehaviour
{
    public struct ChangePosContext
    {
        public GameObject _gameObject;

        public Vector2Int startPoint;
        public Vector2Int endPoint;

        public Vector2 LerpPos(float f)
        {
            Vector2 st = (Vector2)startPoint;
            Vector2 ed = (Vector2)endPoint;

            return Vector2.Lerp(st, ed, f);
        }
    }
    [field: SerializeField] public Vector2Int pos {  get; private set; }

    public UnityEvent<ChangePosContext> ChangePosEvent = new UnityEvent<ChangePosContext>();
    public List<ChangePosContext> ChangePosInvokesHistory = new List<ChangePosContext>();
    private Cell currentCell;

    private void Start()
    {
        GamePlace.instance.Place[pos.x, pos.y].AssignCell(gameObject); // ������������ ������ � �����
        currentCell = GamePlace.instance.Place[pos.x, pos.y];
    }

    public ChangePosContext CreateChangePosContext(Vector2Int startpos, Vector2Int resultPos) // �������� ���� � ����������� �������
    {
        ChangePosContext context = new ChangePosContext()
        {
            startPoint = startpos,
            endPoint = resultPos,
            _gameObject = gameObject
        };

        ChangePosEvent.Invoke(context); // 

        ChangePosInvokesHistory.Add(context);

        return context;
    }

    public void CancelMove() => ChangePosInvokesHistory.Clear();

    public Cell ApplyMove()
    {
        if (ChangePosInvokesHistory.Count != 0)
        {
            GamePlace.instance.Place[pos.x, pos.y].ResignCell(gameObject.name); // ������� ������ �� ������ �� ������
            pos = ChangePosInvokesHistory[ChangePosInvokesHistory.Count - 1].endPoint;
            GamePlace.instance.Place[pos.x, pos.y].AssignCell(gameObject); // ������� ������ �� ������ � ���� ������
            ChangePosInvokesHistory.Clear();

            return GamePlace.instance.Place[pos.x, pos.y];
        }
        else
        {
            throw new System.Exception("�� ������ ����� ���� � ����� ������");
        }
    }
}