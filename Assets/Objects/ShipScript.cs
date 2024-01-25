using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ShipScript : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected List<CannonScript> cannons = new();

    protected float projectileTimer;
    [InspectorLabel("Projectile Info")]
    [SerializeField] protected float fireRate;
    [SerializeField] protected float projectileVelocityMult;
    [SerializeField] protected float projectileDamageMult;
    [SerializeField] protected float projectileVelocityMod;
    [SerializeField] protected float projectileDamageMod;

    [SerializeField] protected float health;
    [SerializeField] protected float speed;

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
