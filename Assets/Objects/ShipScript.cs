using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipScript : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected List<Transform> cannons = new();

    [SerializeField] protected float projectileTimer = 0;
    [SerializeField] protected float projectileSpeed = 10;
    [SerializeField] protected float projectileDamage = 1;

    [SerializeField] protected float health = 100f;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float speed;
    [SerializeField] public GameManager.Team team;

    protected void FireProjectile(GameManager.ProjectileType projectileType, float bulletDmg, float bulletVelocity)
    {
        foreach (Transform t in cannons)
        {
            GameManager.Instance.ShootProjectile(
                projectileType,
                t,
                team,
                bulletDmg,
                bulletVelocity,
                rb.rotation
            );

        }
        projectileTimer = fireRate;
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }
}
