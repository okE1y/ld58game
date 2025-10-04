using UnityEngine;
using UnityEngine.Events;

public class PaternCell : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform spriteTransform;

    public Vector2Int pos;

    public bool InAttention = false;
    public bool Selected = false;

    private Color defaultColor;
    private Color attentionColor;
    private Color SelectedColor;

    public UnityEvent<Vector2Int, bool> OnCellSelect = new UnityEvent<Vector2Int, bool>();

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteTransform = spriteRenderer.GetComponent<Transform>();
        defaultColor = spriteRenderer.color;

        attentionColor = new Color(1f, 1f, 1f, defaultColor.a);
        SelectedColor = new Color(0f, 1f, 0f, defaultColor.a);
    }

    public void SetAttentionTrue()
    {
        InAttention = true;
        ChangeState();
    }

    public void SetAttentionFalse()
    {
        InAttention = false;
        ChangeState();
    }

    public void SetSelectedTrue(bool fast = false)
    {
        Selected = true;
        if (!fast) OnCellSelect.Invoke(pos, true);
        ChangeState();
    }

    public void SetSelectedFalse(bool fast = false)
    {
        Selected = false;
        if (!fast) OnCellSelect.Invoke(pos, false);
        ChangeState();
    }

    public void SwitchSelected()
    {
        Selected = !Selected;
        OnCellSelect.Invoke(pos, Selected);
        ChangeState();
    }

    private void ChangeState()
    {
        if (!Selected)
        {
            if (InAttention)
            {
                spriteRenderer.color = attentionColor;
            }
            else
            {
                spriteRenderer.color = defaultColor;
            }
        }
        else
        {
            spriteRenderer.color = SelectedColor;
        }
    }

    public void ResetControll()
    {
        InAttention = false;
        Selected = false;
        spriteRenderer.color = defaultColor;
        OnCellSelect.RemoveAllListeners();
    }
}
