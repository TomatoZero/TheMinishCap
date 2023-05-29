using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private Slider _attackCharge;

    private bool _isCharging = false;
    private bool _canCharge = true;

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
}