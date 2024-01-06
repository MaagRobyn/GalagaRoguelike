using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float firerate = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject bullet;
    [SerializeField] List<Transform> cannons = new List<Transform>();

    private float fireTimer = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= 0.5f * Time.deltaTime;
        if (fireTimer <= 0)
        {
            var projectileType = GameManager.ProjectileType.Basic;

            var dmg = 1;
            var angle = rb.rotation;
            var velocity = 10;

            foreach (Transform t in cannons)
            {
                GameManager.Instance.ShootProjectile(
                    projectileType,
                    t.position,
                    GameManager.Team.Alien,
                    dmg,
                    velocity,
                    angle);
                fireTimer = firerate;

            }
        }
        fireTimer -= 0.5f * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //var forceVector = -(gameObject.transform.position - StaticPlayerObj.transform.position + new Vector3(0, -5));

        //forceVector.Normalize();
        //rb.AddForce(forceVector * Time.deltaTime * speed);
        var target = Player.transform.position;
        print(target);
        rb.position = Vector2.Lerp(transform.position, target, speed);

    }
}
