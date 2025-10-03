using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControll : MonoBehaviour
{
    private float rawDirection = 0f;
    private float preparedDirection = 0f;
    public float Inertia = 0.3f;
    public float Speed = 5f;

    public Transform cl1;
    public Transform cl2;

    public void MoveCamera(InputAction.CallbackContext context)
    {
        if (context.started || context.canceled)
        {
            rawDirection = context.ReadValue<float>();
        }
    }


    private void Update()
    {
        if (rawDirection - preparedDirection != 0f)
            preparedDirection = Mathf.Lerp(preparedDirection, rawDirection, (1 / Inertia) * Time.deltaTime / Mathf.Abs(rawDirection - preparedDirection));
        else
            preparedDirection = Mathf.Lerp(preparedDirection, rawDirection, 1);

        transform.position = new Vector3 (transform.position.x + preparedDirection * Speed * Time.deltaTime, transform.position.y, transform.position.z);

        transform.position = new Vector3 (Mathf.Clamp(transform.position.x, cl1.position.x, cl2.position.x), transform.position.y, transform.position.z);
    }
}
