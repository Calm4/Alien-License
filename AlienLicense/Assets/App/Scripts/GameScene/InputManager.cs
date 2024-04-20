using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    private PlayerController _playerControls;

    public delegate void StartTouch(Vector3 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector3 position, float time);
    public event EndTouch OnEndTouch;

    private Camera mainCamera;
    
    private void Awake()
    {
        _playerControls = new PlayerController();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    void Start()
    {
        _playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(Utils.ScreenToWorldDirection(mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()),(float)ctx.startTime);
        }
    }
    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(Utils.ScreenToWorldDirection(mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()),(float)ctx.time);
        }
    }
    public Vector3 PrimaryPosition()
    {
        return Utils.ScreenToWorldDirection(mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}