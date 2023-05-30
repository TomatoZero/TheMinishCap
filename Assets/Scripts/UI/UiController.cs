using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private Slider _attackCharge;
    [SerializeField] private MenuController _menuController;
    [SerializeField] private List<ButtonHintController> _buttonsHint;
    
    private bool _isCharging = false;
    private bool _canCharge = true;

    public bool IsMenuOpen
    {
        get => _menuController.gameObject.activeSelf;
    }

    public void NextWindow()
    {
        _menuController.NextWindow();
    }

    public void PrevWindow()
    {
        _menuController.PrevWindow();
    }
    
    public void OpenMenu()
    {
        foreach (var hintController in _buttonsHint) hintController.Hide();
        _menuController.Show();
    }

    public void CloseMenu()
    {
        _menuController.Hide();
        foreach (var hintController in _buttonsHint) hintController.Show();
    }
    
    public void ChargeWeapon()
    {
        _isCharging = true;
        StartCoroutine(Charge());
    }

    public void StopCharge()
    {
        _isCharging = false;
        _canCharge = false;
        StartCoroutine(UnCharge());
    }

    private IEnumerator Charge()
    {
        _attackCharge.gameObject.SetActive(true);

        if(_canCharge)
            while (_isCharging)
            {
                _attackCharge.value += 1f * Time.deltaTime;
                yield return null;   
            }
    }

    private IEnumerator UnCharge()
    {
        while (!_canCharge)
        {
            _attackCharge.value -= 1f * Time.deltaTime;

            if (_attackCharge.value == 0) _canCharge = true;
            
            yield return null;
        }
        
        _attackCharge.gameObject.SetActive(false);
    }
    
    public void SetFirstButtonHint(IventoryItem item)
    {
        SetButtonHint(0, item.Item.Icon);
    }
    
    public void SetSecondButtonHint(IventoryItem item)
    {
        SetButtonHint(1, item.Item.Icon);
    }

    public void SetFirstButtonHint(string hint)
    {
        SetButtonHint(0, hint);
    }
    
    public void HideButtonHint(int id)
    {
        _buttonsHint[id].Hide();
    }

    private void SetButtonHint(int id, Sprite itemIco)
    {
        _buttonsHint[id].Show();
        _buttonsHint[id].ItemIco = itemIco;
    }

    private void SetButtonHint(int id, string hint)
    {
        _buttonsHint[id].Show();
        _buttonsHint[id].TextHint = hint;
    }
}