using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class MouseControll : MonoBehaviour
{
    private bool mouseOn = false;

    public UnityEvent MouseEnter = new UnityEvent();
    public UnityEvent MouseExit = new UnityEvent();
    public UnityEvent MouseDown = new UnityEvent();

    private void OnMouseEnter()
    {
        mouseOn = true;
        MouseEnter.Invoke();
    }

    private void OnMouseExit()
    {
        mouseOn = false;
        MouseExit.Invoke();
    }

    private void OnMouseDown()
    {
        MouseDown.Invoke();
    }

    public void ResetControll()
    {
        mouseOn = false;
    }
}
