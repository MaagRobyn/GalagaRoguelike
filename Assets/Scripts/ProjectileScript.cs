using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static GameManager;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public ScriptableProjectile properties;
    public Team team;

    // Start is called before the first frame update
    void Start()
    {
        // Get the information about itself
        //// Shoot projectile and adjust for 90 degree angle
        rb.SetRotation(transform.rotation);
        var unitVector = Tools.GetUnitVector2(rb.rotation + 90);
        rb.AddForce(unitVector * properties.velocity);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<ShipScript>(out var ship))
        {
            if((int)ship.team != gameObject.layer - Constants.BULLET_TEAM_LAYER_NUM)
            {
                ship.TakeDamage(properties.damage);
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
