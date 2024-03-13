using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ShipScript : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] public List<CannonSlotScript> cannons = new();

    [SerializeField] protected float shipProjectileVelocityMult;
    [SerializeField] protected float shipProjectileDamageMult;
    [SerializeField] protected float shipProjectileVelocityMod;
    [SerializeField] protected int shipProjectileDamageMod;

    [SerializeField] protected float health;
    [SerializeField] protected float speed;

    public GameManager.Team team;

    protected void FireCannons()
    {
        foreach (CannonSlotScript cannon in cannons)
        {
            if (cannon != null && cannon.isSlotFilled && cannon.isReadyToFire)
                cannon.ShootProjectile(
                    team,
                    shipProjectileDamageMult,
                    shipProjectileVelocityMult,
                    shipProjectileDamageMod,
                    shipProjectileVelocityMod
                );
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }
}
