using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator animator;
    InputSubscription GetInput;
    Rigidbody2D rb;
    Vector2 playerMovement;
    Vector2 playerLook;
    const float MOVEMENT_SPEED = 5f;
    const float ATTACK_FORCE = 2f;
    const float SPRINT_SPEED = 7.5f;
    const float COOLDOWN_TIME = 1f;
    float nextActionTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetInput = GetComponent<InputSubscription>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        playerMovement = new Vector2(GetInput.MoveInput.x, GetInput.MoveInput.y);
        rb.linearVelocity = new Vector2(playerMovement.x, playerMovement.y) * MOVEMENT_SPEED;
        animator.SetFloat("Horizontal", GetInput.MoveInput.x);
        animator.SetFloat("Vertical", GetInput.MoveInput.y);

        // LookFunctionality();

        if (GetInput.MenuInput && Time.time >= nextActionTime)
        {
            Debug.Log("Showing Menu");
            nextActionTime = Time.time + COOLDOWN_TIME;
        }

        if (GetInput.AttackInput && Time.time >= nextActionTime)
        {
            rb.AddForce(transform.right * ATTACK_FORCE);
            nextActionTime = Time.time + COOLDOWN_TIME;
        }

        if (GetInput.SprintInput)
        {
            rb.linearVelocity = new Vector2(playerMovement.x, playerMovement.y) * SPRINT_SPEED;
        }
    }

    void LookFunctionality()
    {
        playerLook = new Vector2(GetInput.LookInput.x, GetInput.LookInput.y);
        float mouseAngle = Mathf.Atan2(playerLook.y - transform.position.y, playerLook.x - transform.position.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, mouseAngle);
    }

}
