using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    private GameManager.ProjectileManager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the information about itself
        manager = GameManager.Instance.bulletDict[gameObject.GetInstanceID()];
        rb.SetRotation(manager.angle);
        // Shoot projectile and adjust for 90 degree angle
        var unitVector = Tools.getUnitVector3(manager.angle + 90);
        rb.AddForce(unitVector * manager.velocity);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (manager.owningTeam == GameManager.Team.Player && collision.gameObject.layer != 6)
        {
            Destroy(gameObject);
            //Debug.Log("Destroyed bullet");
        }
        else if (manager.owningTeam == GameManager.Team.Alien && collision.gameObject.layer != 7)
        {
            Destroy(gameObject);
            //Debug.Log("Destroyed bullet");
        }

    }
}
