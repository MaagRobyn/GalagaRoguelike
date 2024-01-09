using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShipScript : ShipScript
{
    [SerializeField] private GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        projectileTimer -= 0.5f * Time.deltaTime;
        if (projectileTimer <= 0)
        {
            var projectileType = GameManager.ProjectileType.Basic;

            var dmg = 1;
            var velocity = 10;
            FireProjectile(projectileType, dmg, velocity);
        }
        projectileTimer -= 0.5f * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //var forceVector = -(gameObject.transform.position - StaticPlayerObj.transform.position + new Vector3(0, -5));

        //forceVector.Normalize();
        //rb.AddForce(forceVector * Time.deltaTime * speed);
        var target = GameManager.Instance.player.transform.position;
        //print(target);
        rb.position = Vector2.Lerp(transform.position, target, speed / 1000);

    }
}
