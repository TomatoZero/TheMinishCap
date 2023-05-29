using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    // [SerializeField] private Slider _attackCharge;
    [SerializeField] private UiController _uiController;
    
    private PlayerControll _playerControl;
    private CharacterController _characterController;
    // private bool _isCharging = false;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _characterController = this.GetComponent<CharacterController>();
        _playerControl = new PlayerControll();

        var player = _playerControl.Player;
        player.FirstItem.started += _ => ChargeFirstItemStart();
        player.FirstItem.canceled += _ => ChargeFirstItemCancel();
        
        player.SecondItem.started += _ => ChargeSecondItemStart();
        player.SecondItem.canceled += _ => ChargeSecondItemCancel();

        player.Menu.performed += _ => OpenMenu();
        player.Roll.performed += _ => Roll();
        player.ConnectKinstonePiece.performed += _ => ConnectPiece();

        player.Move.performed += ctx => _moveDirection = ctx.ReadValue<Vector2>();
        player.Move.canceled += ctx => _moveDirection = Vector2.zero;
    }

    private void Update()
    {
        _characterController.MoveDirection = _moveDirection.normalized;
        // _characterController.Move(_moveDirection);
    }

    private void OnEnable()
    {
        _playerControl.Enable();
    }

    private void OnDisable()
    {
        _playerControl.Disable();
    }
    

    private void ChargeFirstItemStart()
    {
        _uiController.ChargeWeapon();
    }

    private void ChargeFirstItemCancel()
    {
        _uiController.StopCharge();
    }
    
    private void ChargeSecondItemStart()
    {
        _uiController.ChargeWeapon();
    }

    private void ChargeSecondItemCancel()
    {
        _uiController.StopCharge();
    }
    
    private void OpenMenu()
    {
        var input = GetComponent<PlayerInput>();
        
    }

    private void ConnectPiece()
    {
        Debug.Log("Connect Kinstone Piece");
    }

    private void Roll()
    {
        _characterController.Roll();
    }
    
    
}