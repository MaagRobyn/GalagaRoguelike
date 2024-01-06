using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public static float bulletrate = .3f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject bullet;
    [SerializeField] private float bulletTimer = 0;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float bulletDamage = 1;
    [SerializeField] List<Transform> cannons = new List<Transform>();

    public float speed;
    public float rotationSpeed;
    float horizontal;
    float vertical;
    float jump;
    float rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        jump = Input.GetAxisRaw("Jump");
        rotation = Input.GetAxisRaw("Rotation");

        if(bulletTimer > 0)
        {
            bulletTimer -= Time.deltaTime;
        }
        if(jump > 0 && bulletTimer < 0)
        {
            var projectileType = GameManager.ProjectileType.Basic;
            var bulletDmg = 1;
            var bulletVelocity = 10;

            foreach(Transform t in cannons)
            {
                GameManager.Instance.ShootProjectile(
                    projectileType, 
                    t.position, 
                    GameManager.Team.Player, 
                    bulletDmg, 
                    bulletVelocity, 
                    rb.rotation
                );

            }
            bulletTimer = bulletrate;
        }
    }

    private void FixedUpdate()
    {
        var velocity = new Vector2(horizontal, vertical);
        velocity.Normalize();
        rb.velocity = velocity * speed;
        rb.SetRotation(rb.rotation + rotation * rotationSpeed);
    }


}