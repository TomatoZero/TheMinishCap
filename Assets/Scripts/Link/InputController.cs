using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private UiController _uiController;
    [SerializeField] private CharacterController _characterController;

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
        // _characterController.Move(_moveDirection);
    }

    private void OnEnable()
    {
        _playerControl.Enable();

        _playerAction.FirstItem.started += ChargeFirstItemStart;
        _playerAction.FirstItem.canceled += ChargeFirstItemCancel;
        
        _playerAction.SecondItem.started += ChargeSecondItemStart;
        _playerAction.SecondItem.canceled += ChargeSecondItemCancel;

        _playerAction.Menu.performed += OpenMenu;
        _playerAction.Roll.performed += Roll;
        _playerAction.ConnectKinstonePiece.performed += ConnectPiece;

        _playerAction.Move.performed += MoveDirection;
        _playerAction.Move.canceled += MoveStop;
        
        _UiAction.Disable();

        _UiAction.Close.performed += CloseMenu;
        _UiAction.NextWindow.performed += NextWindowOnPerformed;
        _UiAction.PrevWindow.performed += PrevWindowOnPerformed;
    }

    private void OnDisable()
    {
        _playerControl.Disable();

        _playerAction.FirstItem.started -= ChargeFirstItemStart;
        _playerAction.FirstItem.canceled -= ChargeFirstItemCancel;
        
        _playerAction.SecondItem.started -= ChargeSecondItemStart;
        _playerAction.SecondItem.canceled -= ChargeSecondItemCancel;

        _playerAction.Menu.performed -= OpenMenu;
        _playerAction.Roll.performed -= Roll;
        _playerAction.ConnectKinstonePiece.performed -= ConnectPiece;

        _playerAction.Move.performed -= MoveDirection;
        _playerAction.Move.canceled -= MoveStop;
        
        
        _UiAction.Disable();

        _UiAction.Close.performed -= CloseMenu;
        _UiAction.NextWindow.performed -= NextWindowOnPerformed;
        _UiAction.PrevWindow.performed -= PrevWindowOnPerformed;
    }


    private void MoveDirection(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }

    private void MoveStop(InputAction.CallbackContext context)
    {
        _moveDirection = Vector2.zero;
    }

    private void ChargeFirstItemStart(InputAction.CallbackContext context)
    {
        _uiController.ChargeWeapon();
    }

    private void ChargeFirstItemCancel(InputAction.CallbackContext context)
    {
        _uiController.StopCharge();
    }

    private void ChargeSecondItemStart(InputAction.CallbackContext context)
    {
        _uiController.ChargeWeapon();
    }

    private void ChargeSecondItemCancel(InputAction.CallbackContext context)
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
}