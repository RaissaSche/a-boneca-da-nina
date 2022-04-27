using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    [Range(2, 10)]
    public float smoothFactor;

    private void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 offsettedTargetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, offsettedTargetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
