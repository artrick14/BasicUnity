using UnityEngine;

public class GameManager : MonoBehaviour
{
    InputSubscription GetInput;

    private void Awake()
    {
        GetInput = GetComponent<InputSubscription>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
