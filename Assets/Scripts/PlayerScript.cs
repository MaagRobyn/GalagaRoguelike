using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : ShipScript
{
    public float rotationSpeed;
    float horizontal;
    float vertical;
    float jump;
    float rotation;

    [SerializeField] Slider healthbar;
    [SerializeField] SpriteRenderer playerSprite;
    readonly List<Reward> obtainedRewards = new();

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
        if (Input.GetAxisRaw("Close") == 1)
        {
            Debug.Log("Quitting Game");
            Application.Quit();
        }

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

        if (health > 0 && jump > 0)
        {
            FireCannons();
        }
    }


    private void FixedUpdate()
    {
        if (health > 0)
        {
            var verticalVector = Tools.GetUnitVector2(rb.rotation);
            var horizontalVector = Tools.GetUnitVector2(rb.rotation + 90);
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
            InvokePlayerDeath();
            healthbar.enabled = false;
            playerSprite.enabled = false;

        }
    }
    public void AddReward(Reward reward)
    {
        obtainedRewards.Add(reward);
    }

    #region Events

    public delegate void PlayerDeath();
    public event PlayerDeath OnPlayerDeath;
    protected virtual void InvokePlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }
    #endregion


}