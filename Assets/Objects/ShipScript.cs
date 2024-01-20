using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipScript : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected List<CannonScript> cannons = new();

    [SerializeField] protected float projectileTimer;
    [SerializeField] protected float projectileVelocityMod = 10;
    [SerializeField] protected float projectileDamageMod = 1;

    [SerializeField] protected float health = 100f;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float speed;

    public float dangerLevel;
    public GameManager.Team team;

    protected void FireCannons(float bulletDmgMult = 1, float bulletVelocityMult = 1, float bulletDmgMod = 0, float bulletVelocityMod = 0)
    {
        foreach (CannonScript cannon in cannons)
        {
            cannon.ShootProjectile(team, bulletDmgMult, bulletVelocityMult, bulletDmgMod, bulletVelocityMod);
        }
        projectileTimer = fireRate;
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }
}
