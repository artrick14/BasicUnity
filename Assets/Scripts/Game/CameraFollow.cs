using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;
    public Transform target;
    private Vector3 velocity;

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);
    }
}
