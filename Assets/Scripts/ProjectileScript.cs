using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static GameManager;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    ScriptableProjectile properties;
    
    private int damage;
    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        // Get the information about itself
        //// Shoot projectile and adjust for 90 degree angle
        rb.SetRotation(transform.rotation);
        var unitVector = Tools.GetUnitVector2(rb.rotation + 90);
        rb.AddForce(unitVector * velocity);
        //Debug.Log(velocity);
    }

    public void SetProperties(ScriptableProjectile prop)
    {
        properties = prop;
    }
    public void SetVelocity(float velocityMult, float velocityMod)
    {
        velocity = properties.baseVelocity + velocityMod;
        velocity *= velocityMult;
        //Debug.Log($"Velocity: {velocity}");
    }
    public void SetDamage(float damageMult, int damageMod)
    {
        damage = damageMod + properties.baseDamage;
        damage *= (int)(damageMult);
        //Debug.Log($"Damage Calculation: {damageMult} * ({properties.baseDamage} + {damageMod})\nTotal Damage: {damage}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<ShipScript>(out var ship))
        {
            if((int)ship.team != gameObject.layer - Constants.BULLET_TEAM_LAYER_NUM)
            {
                ship.TakeDamage(damage);
                Debug.Log($"{damage}");
                Destroy(gameObject);
            }

        }
        else if (collision.gameObject.TryGetComponent<ProjectileScript>(out var bullet))
        {
            if (bullet.gameObject.layer != gameObject.layer)
            {
                Destroy(gameObject);
            }
        }
        //Debug.Log("Destroyed bullet");
    }
}
