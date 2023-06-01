using System.Collections;
using UnityEngine;


public class Sword : BaseWeapon
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"Enemy {other.name} hit");
        }
    }

    public override void UseWeapon()
    {
        gameObject.SetActive(true);

        StartCoroutine(DoAttack());
    }

    public override void ReleaseWeapon()
    {
        IsRelease = true;
    }
    
    private IEnumerator Hold()
    {
        base.Hold();
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            
            if(IsRelease) break;
        }
        
        gameObject.SetActive(false);
        IsHold = false;
    }

    private IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Hold());

    }
}