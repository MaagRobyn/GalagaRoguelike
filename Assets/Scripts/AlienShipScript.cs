using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShipScript : ShipScript
{
    [SerializeField] private GameObject Player;
    public AlienType type;
    private const int LERPFACTOR = 2000;
    private Vector3 destinationCoordinates;

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
        Drone,
        CirclePlayer
    }

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1;
        flightPattern = FlightPattern.Chase;
        GameManager.Instance.Player.PlayerDied += () =>
        {
            flightPattern = FlightPattern.Wander;
        };
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
        Transform target = new GameObject().transform;
        switch (flightPattern)
        {
            case FlightPattern.Chase:
                target.position = GameManager.Instance.Player.transform.position;
                destinationCoordinates = target.position;
                if (Vector3.Distance(transform.position, target.position) > 2)
                {
                    GoToTarget(target);
                }
                RotateTowardsTarget(target);
                if (Vector3.Distance(transform.position, target.position) <= 1)
                {
                    target.position = -target.position;
                    GoToTarget(target);
                }
                break;
            case FlightPattern.Wander:
                if (Vector3.Distance(transform.position, target.position) < 1)
                {
                    var x = UnityEngine.Random.Range(-1000, 1000);
                    var y = UnityEngine.Random.Range(-1000, 1000);
                    var z = UnityEngine.Random.Range(-1000, 1000);
                    destinationCoordinates = new Vector3(x, y, z);
                }
                target.position = destinationCoordinates;
                GoToTarget(target);
                break;
        }

    }

    private void RotateTowardsTarget(Transform target)
    {
        float angle = Tools.FindAngleBetweenTwoTransforms(transform, target);
        rb.SetRotation(angle);
    }

    private void GoToTarget(Transform target)
    {
        rb.position = Vector2.Lerp(transform.position, target.position, speed / LERPFACTOR);
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
