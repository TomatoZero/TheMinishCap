using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private UiController _uiController;

    private void Update()
    {
        _playerInput.actions["FirstItem"].started += ChargeFirstItemStart;
        _playerInput.actions["FirstItem"].canceled += _ => ChargeFirstItemCancel();
    }

    private void OnEnable()
    {
        _playerInput.ActivateInput();
    }

    private void OnDisable()
    {
        _playerInput.DeactivateInput();
    }
    

    public void ChargeFirstItemStart(InputAction.CallbackContext context)
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
