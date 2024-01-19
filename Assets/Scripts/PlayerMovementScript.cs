using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovementScript : ShipScript
{
    public float rotationSpeed;
    float horizontal;
    float vertical;
    float jump;
    float rotation;

    [SerializeField] Slider healthbar;
    [SerializeField] SpriteRenderer playerSprite;

    public delegate void PlayerDeath();
    public event PlayerDeath PlayerDied;

    float respawnTimer = 5;

    // Start is called before the first frame update
    void Start()
    {
        healthbar.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.Player = this;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        jump = Input.GetAxisRaw("Jump");
        rotation = Input.GetAxisRaw("Rotation");

        if (!playerSprite.enabled)
        {
            if (respawnTimer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                respawnTimer -= Time.deltaTime;
            }
        }

        if (projectileTimer > 0)
        {
            projectileTimer -= Time.deltaTime;
        }
        if (jump > 0 && projectileTimer <= 0)
        {
            var projectileType = GameManager.ProjectileType.Basic;
            var bulletDmg = projectileDamage;
            var bulletVelocity = 10;
            if (health > 0)
            {
                ShootProjectile(projectileType, bulletDmg, bulletVelocity);
            }
        }
    }


    private void FixedUpdate()
    {
        if (health > 0)
        {
            var verticalVector = Tools.GetUnitVector3(rb.rotation);
            var horizontalVector = Tools.GetUnitVector3(rb.rotation + 90);
            rb.velocity = horizontal * speed * verticalVector + speed * vertical * horizontalVector;
            //var velocity = new Vector2(horizontal, vertical);
            //velocity.Normalize();
            //rb.velocity = velocity * speed;
            rb.SetRotation(rb.rotation + rotation * rotationSpeed);
        }

    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthbar.value = health;
        if (health <= 0)
        {
            OnPlayerDeath();
            healthbar.enabled = false;
            playerSprite.enabled = false;

        }
    }
    protected virtual void OnPlayerDeath()
    {
        PlayerDied?.Invoke();
    }


}