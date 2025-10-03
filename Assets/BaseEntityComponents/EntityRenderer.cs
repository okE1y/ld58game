using UnityEngine;
using DG.Tweening;

public class EntityRenderer : MonoBehaviour
{
    [HideInInspector] public IntPos IntPos;
    public Sequence MoveSequance;
    public float moveTime = 0.3f;
    void Start()
    {
        IntPos = GetComponent<IntPos>();
        transform.localPosition = IntPos.pos * GamePlace.instance.cellSize;
    }

    public void ChangePosition()
    {
        var history = IntPos.ChangePosInvokesHistory;

        MoveSequance = DOTween.Sequence();

        if (history != null)
        {
            for (int i = 0; i < history.Count; i++)
            {
                if (i != history.Count - 1)
                    MoveSequance.Append(transform.DOLocalMove((Vector3)(history[i + 1].startPoint * GamePlace.instance.cellSize), moveTime)).SetEase(Ease.Linear);
                else
                    MoveSequance.Append(transform.DOLocalMove((Vector3)(history[i].endPoint * GamePlace.instance.cellSize), moveTime)).SetEase(Ease.Linear);
            }
        }
        Game.GameManager.sequences.Add(MoveSequance);

        MoveSequance.Play();
    }
}
