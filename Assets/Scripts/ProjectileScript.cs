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
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
        MoveProjectile();
    }
    void MoveProjectile()
    {
        MoveProjectile(velocity);
    }
    void MoveProjectile(float v)
    {
        rb.SetRotation(initialRotation);
        var unitVector = Tools.GetUnitVector2(rb.rotation + 90);
        rb.AddForce(unitVector * v);
    }
    private void Update()
    {
        if(rb.velocity.magnitude < velocity)
        {
            //Debug.Log($"{rb.velocity.magnitude} vs {velocity}");
            MoveProjectile(velocity - rb.velocity.magnitude);
        }
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
            if((int)ship.team != gameObject.layer - Globals.BULLET_TEAM_LAYER_NUM)
            {
                ship.TakeDamage(damage); 
                //Lasers pass through objects, but still damage them
                if (properties.element != ScriptableProjectile.ProjectileElement.Laser)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    MoveProjectile();
                }
            }

        }
        else if (collision.gameObject.TryGetComponent<ProjectileScript>(out var bullet))
        {
            if (bullet.gameObject.layer != gameObject.layer)
            {
                gameObject.SetActive(false);
            }
        }
        else if(collision.gameObject.TryGetComponent<CrateScript>(out var reward))
        {
            reward.TakeDamage(damage);
            //Lasers pass through objects, but still damage them
            if (properties.element != ScriptableProjectile.ProjectileElement.Laser)
            {
                gameObject.SetActive(false);
            }
            else
            {
                MoveProjectile();
            }
        }else
        {
            Debug.Log(collision.gameObject.name);
        }
        //Debug.Log("Destroyed bullet");
    }
}
