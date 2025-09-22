using UnityEngine;

public class WeaponDirection : MonoBehaviour
{
    InputSubscription GetInput;
    Transform tran;
    Vector2 aimDirection;

    void Start()
    {
        GetInput = GetComponent<InputSubscription>();
        tran = gameObject.GetComponent<Transform>();
    }


    private void Update()
    {
        aimDirection = new Vector2(GetInput.LookInput.x, GetInput.LookInput.y);
        float mouseAngle = Mathf.Atan2(aimDirection.y - transform.position.y, aimDirection.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        tran.localRotation = Quaternion.Euler(0, 0, mouseAngle);
    }
}