using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShipScript : ShipScript
{
    [SerializeField] private GameObject Player;
    public AlienType type;
    private const int LERPFACTOR = 2000;

    public enum AlienType
    {
        Basic = 0
    }
    private FlightPattern flightPattern;
    enum FlightPattern
    {
        Wander,
        Chase,
        Cruise,
        Drone
    }
    
    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1;
        flightPattern = FlightPattern.Chase;
    }

    // Update is called once per frame
    void Update()
    {
        projectileTimer -= 0.5f * Time.deltaTime;
        if (projectileTimer <= 0)
        {
            var projectileType = GameManager.ProjectileType.Basic;

            var dmg = projectileDamage;
            var velocity = projectileSpeed;
            ShootProjectile(projectileType, dmg, velocity);
        }
        projectileTimer -= 0.5f * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        switch (flightPattern)
        {
            case FlightPattern.Chase:
                var target = GameManager.Instance.PlayerTransform;
                rb.position = Vector2.Lerp(transform.position, target.position, speed / LERPFACTOR);

                float angle = Tools.FindAngleBetweenTwoTransforms(transform, target); 
                rb.SetRotation(angle);
                break;
        }

    }


    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
