using UnityEngine;

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

    public void SetSelectedTrue()
    {
        Selected = true;
        ChangeState();
    }

    public void SetSelectedFalse()
    {
        Selected = false;
        ChangeState();
    }

    public void SwitchSelected()
    {
        Selected = !Selected;
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
    }
}
