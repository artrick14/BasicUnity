using UnityEngine;
using UnityEngine.InputSystem;

public class InputSubscription : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public Vector2 LookInput { get; private set; } = Vector2.zero;
    public bool MenuInput { get; private set; } = false;
    public bool AttackInput { get; private set; } = false;
    public bool InteractionInput { get; private set; } = false;
    public bool SprintInput { get; private set; } = false;

    InputSystemActions _Input = null;

    private void OnEnable()
    {
        _Input = new InputSystemActions();
        _Input.Player.Enable();

        _Input.Player.Move.performed += SetMovement;
        _Input.Player.Move.canceled += SetMovement;

        _Input.Player.Look.performed += SetLook;
        _Input.Player.Look.canceled += SetLook;

        _Input.Player.Menu.started += SetMenu;
        _Input.Player.Menu.canceled += SetMenu;

        _Input.Player.Attack.started += SetAttack;
        _Input.Player.Attack.canceled += SetAttack;

        _Input.Player.Interact.started += SetInteraction;
        _Input.Player.Interact.canceled += SetInteraction;

        _Input.Player.Sprint.started += SetSprint;
        _Input.Player.Sprint.canceled += SetSprint;
    }

    private void OnDisable()
    {
        _Input.Player.Move.performed -= SetMovement;
        _Input.Player.Move.canceled -= SetMovement;

        _Input.Player.Look.performed -= SetLook;
        _Input.Player.Look.canceled -= SetLook;

        _Input.Player.Menu.started -= SetMenu;
        _Input.Player.Menu.canceled -= SetMenu;

        _Input.Player.Attack.started -= SetAttack;
        _Input.Player.Attack.canceled -= SetAttack;

        _Input.Player.Attack.started -= SetInteraction;
        _Input.Player.Attack.canceled -= SetInteraction;

        _Input.Player.Sprint.started -= SetSprint;
        _Input.Player.Sprint.canceled -= SetSprint;

        _Input.Player.Disable();
    }

    void SetMovement(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    void SetLook(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }

    void SetMenu(InputAction.CallbackContext ctx)
    {
        MenuInput = ctx.started;
    }

    void SetAttack(InputAction.CallbackContext ctx)
    {
        AttackInput = ctx.started;
    }

    void SetInteraction(InputAction.CallbackContext ctx)
    {
        InteractionInput = ctx.started;
    }

    void SetSprint(InputAction.CallbackContext ctx)
    {
        SprintInput = ctx.started;
    }

}
