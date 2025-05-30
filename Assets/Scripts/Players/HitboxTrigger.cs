using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitboxTrigger : MonoBehaviour
{
    public int damage = 10;
    private bool canDealDamage = false;

    void OnTriggerEnter(Collider other)
    {
        if (!canDealDamage) return;

        IDamageable target = other.GetComponentInParent<IDamageable>();
        
        if (target != null)
        {
            target.TakeDamage(damage);
            canDealDamage = false; // prevent multi-hit from 1 punch
        }
    }

    public void EnableDamage() {
        canDealDamage = true;
    }

    public void DisableDamage() {
        canDealDamage = false;
    }
}
