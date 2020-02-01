using UnityEngine;

public class AnimateWindowsOff : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private float _rotationSpeed = -10f;

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(_moveSpeed, 0, 0), Space.World);
        transform.Rotate(new Vector3(0, 0, -_rotationSpeed), Space.Self);
    }

}
