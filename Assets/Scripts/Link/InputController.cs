using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private UiController _uiController;
    [SerializeField] private CharacterController _characterController;
    // [SerializeField] private WeaponsController _weaponsController;
    
    private PlayerControll _playerControl;
    private PlayerControll.PlayerActions _playerAction;
    private PlayerControll.UIActions _UiAction;
    
    private Vector2 _moveDirection;

    private void Awake()
    {
        _playerControl = new PlayerControll();
        _playerAction = _playerControl.Player;
        _UiAction = _playerControl.UI;
    }

    private void Update()
    {
        _characterController.MoveDirection = _moveDirection.normalized;
    }

    private void OnEnable()
    {
        _playerControl.Enable();

        InputSystem.onDeviceChange += DeviceChanged;

        _playerAction.Enable();
        _playerAction.FirstItem.started += UseFirstWeapon;
        _playerAction.FirstItem.canceled += ReleaseFirstWeapon;
        
        _playerAction.SecondItem.started += UseSecondWeapon;
        _playerAction.SecondItem.canceled += ReleaseSecondWeapon;

        _playerAction.Menu.performed += OpenMenu;
        _playerAction.Roll.performed += Roll;
        _playerAction.ConnectKinstonePiece.performed += ConnectPiece;

        _playerAction.Move.performed += MoveDirection;
        _playerAction.Move.canceled += MoveStop;
        
        _UiAction.Disable();

        _UiAction.Close.performed += CloseMenu;
        _UiAction.NextWindow.performed += NextWindowOnPerformed;
        _UiAction.PrevWindow.performed += PrevWindowOnPerformed;
        _UiAction.Back.performed += HideSubWindow;
    }

    private void OnDisable()
    {
        _playerControl.Disable();
        _playerAction.Disable();
        
        _playerAction.FirstItem.started -= UseFirstWeapon;
        _playerAction.FirstItem.canceled -= ReleaseFirstWeapon;
        
        _playerAction.SecondItem.started -= UseSecondWeapon;
        _playerAction.SecondItem.canceled -= ReleaseSecondWeapon;

        _playerAction.Menu.performed -= OpenMenu;
        _playerAction.Roll.performed -= Roll;
        _playerAction.ConnectKinstonePiece.performed -= ConnectPiece;

        _playerAction.Move.performed -= MoveDirection;
        _playerAction.Move.canceled -= MoveStop;
        
        
        _UiAction.Disable();

        _UiAction.Close.performed -= CloseMenu;
        _UiAction.NextWindow.performed -= NextWindowOnPerformed;
        _UiAction.PrevWindow.performed -= PrevWindowOnPerformed;
        _UiAction.Back.performed -= HideSubWindow;
    }

    private void DeviceChanged(InputDevice device, InputDeviceChange change)
    {
        // Debug.Log($"device: {device} change: {change}");
    }
    
    private void MoveDirection(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }

    private void MoveStop(InputAction.CallbackContext context)
    {
        _moveDirection = Vector2.zero;
    }

    private void UseFirstWeapon(InputAction.CallbackContext context)
    {
        // Debug.Log($"Input controller use weapon");
        _characterController.UseFirstWeapon();
    }

    private void ReleaseFirstWeapon(InputAction.CallbackContext context)
    {
        // Debug.Log($"Input controller release weapon");
        _characterController.ReleaseFirstWeapon();
    }

    private void UseSecondWeapon(InputAction.CallbackContext context)
    {
        _uiController.ChargeWeapon();
    }

    private void ReleaseSecondWeapon(InputAction.CallbackContext context)
    {
        _uiController.StopCharge();
    }

    private void OpenMenu(InputAction.CallbackContext context)
    {
        _playerAction.Disable();
        _UiAction.Enable();
        
        _uiController.OpenMenu();
    }

    private void CloseMenu(InputAction.CallbackContext context)
    {
        _playerAction.Enable();
        _UiAction.Disable();
        
        _uiController.CloseMenu();
    }

    private void ConnectPiece(InputAction.CallbackContext context)
    {
        Debug.Log("Connect Kinstone Piece");
    }

    private void Roll(InputAction.CallbackContext context)
    {
        _characterController.Roll();
    }

    private void NextWindowOnPerformed(InputAction.CallbackContext obj)
    {
        if(_uiController.IsMenuOpen)
            _uiController.NextWindow();    
    }

    private void PrevWindowOnPerformed(InputAction.CallbackContext obj)
    {
        if(_uiController.IsMenuOpen)
            _uiController.PrevWindow();
    }

    private void HideSubWindow(InputAction.CallbackContext obj)
    {
        if(_uiController.IsMenuOpen)
            _uiController.SubWindowClose();
    }
}