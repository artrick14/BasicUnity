using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable
{
    private InputSubscription GetInput;
    private Transform playerTransform;
    private const float INTERACT_DISTANCE = 3f;
    const float COOLDOWN_TIME = 0.5f;
    float nextActionTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetInput = GetComponent<InputSubscription>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetInput.InteractionInput && IsWithinInteractableRange() && Time.time >= nextActionTime)
        {
            Interact();
            nextActionTime = Time.time + COOLDOWN_TIME;
        }
    }

    public abstract void Interact();

    private bool IsWithinInteractableRange()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) < INTERACT_DISTANCE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
