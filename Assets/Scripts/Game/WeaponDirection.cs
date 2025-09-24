using System;
using UnityEngine;

public class WeaponDirection : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    InputSubscription GetInput;

    void Start()
    {
        GetInput = GetComponent<InputSubscription>();
    }


    private void Update()
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(GetInput.LookInput.x, GetInput.LookInput.y, mainCamera.nearClipPlane));

        Vector3 rotateDirection = (worldPosition - transform.position).normalized;
        rotateDirection.z = 0;

        float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg - 180f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}