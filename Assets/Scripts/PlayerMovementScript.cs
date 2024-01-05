using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public static float bulletrate = .3f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] ScriptableBullet bullet;
    [SerializeField] private float bulletTimer = 0;
    public float speed = 1f;
    float horizontal;
    float vertical;
    float jump;

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
        if(bulletTimer > 0)
        {
            bulletTimer -= Time.deltaTime;
        }
        if(jump > 0 && bulletTimer < 0)
        {
            ShootBullet();
            bulletTimer = bulletrate;
        }
    }

    private void ShootBullet()
    {
        var bulletTransform = new GameObject().transform;
        bulletTransform.position = rb.transform.position + new Vector3(0, 1);
        
        Instantiate(bullet, bulletTransform);
        print("bullet shot");
    }

    private void FixedUpdate()
    {
        var velocity = new Vector2 (horizontal, vertical);
        velocity.Normalize();
        rb.velocity = velocity * speed;
    }


}