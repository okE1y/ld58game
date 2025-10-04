using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControll : MonoBehaviour
{
    private float rawDirection = 0f;
    private float preparedDirection = 0f;
    public float Inertia = 0.3f;
    public float Speed = 5f;

    private float playerOffset;

    public Transform cl1;
    public Transform cl2;

    public MultipleSwitcher MSwitcher = new MultipleSwitcher();

    private void Start()
    {
        playerOffset = Game.player.transform.position.x - transform.position.x;
    }

    public void MoveCamera(InputAction.CallbackContext context)
    {
        if (MSwitcher.GetSwitchState() && (context.started || context.canceled))
        {
            rawDirection = context.ReadValue<float>();
        }
    }


    private void LateUpdate()
    {
        if (rawDirection - preparedDirection != 0f)
            preparedDirection = Mathf.Lerp(preparedDirection, rawDirection, (1 / Inertia) * Time.deltaTime / Mathf.Abs(rawDirection - preparedDirection));
        else
            preparedDirection = Mathf.Lerp(preparedDirection, rawDirection, 1);

        transform.position = new Vector3 (transform.position.x + preparedDirection * Speed * Time.deltaTime, transform.position.y, transform.position.z);

        transform.position = new Vector3 (Mathf.Clamp(transform.position.x, cl1.position.x, cl2.position.x), transform.position.y, transform.position.z);
    }

    public IEnumerator CenterCameraOnPlayer()
    {
        Sequence cameraMove = DOTween.Sequence()
            .Append(transform.DOMoveX(Game.player.transform.position.x + Mathf.Abs(playerOffset), 0.4f))
            .Play();

        yield return new WaitUntil(() => cameraMove.IsComplete());

        yield break;
    }
}
