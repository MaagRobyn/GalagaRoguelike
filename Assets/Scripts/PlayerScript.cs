using Assets;
using Assets.Objects.Reward;
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
    float fire1;
    float boost;
    float rotation;
    float zoom;

    [SerializeField] Slider healthbar;
    [SerializeField] SpriteRenderer playerSprite;
    public List<Reward> obtainedRewards = new();
    public Queue<Reward> newRewards = new();
    public List<CrewMember> crewMembers = new();
    [SerializeField] ScriptableCannon defaultCannon;

    float respawnTimer = 5;

    int[] testArr = new int[2];
    int a = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Static Camera
        healthbar.value = health;
        cannons[2].EquipCannon(defaultCannon);
        OnPlayerDeath += () =>
        {
            var loadTimer = Timer.AddTimer(respawnTimer);
            loadTimer.OnTimerEnd += () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            };
        };
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.Player = this;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        fire1 = Input.GetAxisRaw("Fire1");
        boost = Input.GetAxisRaw("Fire3");
        rotation = Input.GetAxis("Rotation");
        zoom = Input.GetAxisRaw("Zoom");
        if (Input.GetAxisRaw("Close") == 1)
        {
            Debug.Log("Quitting Game");
            Application.Quit();
        }

        CannonSlotSwap();

        foreach (var r in obtainedRewards)
        {
            if (r.isContinuous)
            {
                r.action.GiveReward(this);
            }
        }

        if (health > 0 && fire1 > 0 && boost <= 0)
        {
            FireCannons();
        }
        Reward reward;
        while (newRewards.TryDequeue(out reward))
        {
            obtainedRewards.Add(reward);
            reward.action.GiveReward(this);
        }
    }

    private void CannonSlotSwap()
    {
        if (Input.GetKeyUp(KeyCode.Alpha3) && testArr[(a + 1) % 2] != 3)
        {
            testArr[a % 2] = 3;
            if (a % 2 == 1)
            {
                SwapWeaponSlots(testArr[a % testArr.Length], testArr[(a + 1) % testArr.Length]);
            }
            a++;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2) && testArr[(a + 1) % 2] != 2)
        {
            testArr[a % 2] = 2;
            if (a % 2 == 1)
            {
                SwapWeaponSlots(testArr[a % testArr.Length], testArr[(a + 1) % testArr.Length]);
            }
            a++;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4) && testArr[(a + 1) % 2] != 4)
        {
            testArr[a % 2] = 4;
            if (a % 2 == 1)
            {
                SwapWeaponSlots(testArr[a % testArr.Length], testArr[(a + 1) % testArr.Length]);
            }
            a++;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1) && testArr[(a + 1) % 2] != 1)
        {
            testArr[a % 2] = 1;
            if (a % 2 == 1)
            {
                SwapWeaponSlots(testArr[a % testArr.Length], testArr[(a + 1) % testArr.Length]);
            }
            a++;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5) && testArr[(a + 1) % 2] != 5)
        {
            testArr[a % 2] = 5;
            if (a % 2 == 1)
            {
                SwapWeaponSlots(testArr[a % testArr.Length], testArr[(a + 1) % testArr.Length]);
            }
            a++;
        }
    }

    private void FixedUpdate()
    {
        if (health > 0)
        {
            //Non-static Camera
            var horizontalVector = Tools.GetUnitVector2(rb.rotation);
            var verticalVector = Tools.GetUnitVector2(rb.rotation + 90);
            rb.velocity = (boost + 1) * speed * (verticalVector * vertical + horizontal * horizontalVector);
            if ((zoom > 0 && Camera.main.orthographicSize < 20) || (zoom < 0 && Camera.main.orthographicSize > 5))
            {
                Camera.main.orthographicSize -= zoom * 5;
            }

            // Static Camera
            //var movement = new Vector2();
            //Check if player is in camera bounds
            //if (Camera.main.WorldToScreenPoint(transform.position).x < playerSprite.size.x && horizontal < 0 ||
            //    (Camera.main.WorldToScreenPoint(transform.position).x > Camera.main.scaledPixelWidth - playerSprite.size.y && horizontal > 0))
            //{
            //movement.x = horizontal * speed;
            //}
            //if (Camera.main.WorldToScreenPoint(transform.position).y < playerSprite.size.y && vertical < 0 ||
            //    (Camera.main.WorldToScreenPoint(transform.position).y > Camera.main.scaledPixelHeight - playerSprite.size.y && vertical > 0))
            //{
            //movement.y = vertical * speed;
            //}
            //rb.velocity = movement;


            //Non-static Camera
            rb.SetRotation(rb.rotation + -rotation * rotationSpeed);

            // Static Camera
            //var mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            //Debug.Log(mousePosition);

            //rb.SetRotation(Tools.FindAngleBetweenTwoPositions(transform.position, mousePosition));
        }

    }

    private void SwapWeaponSlots(int firstIndex, int secondIndex)
    {
        var tmp = cannons[firstIndex].GetCannonData();
        cannons[firstIndex].EquipCannon(cannons[secondIndex].GetCannonData());
        cannons[secondIndex].EquipCannon(tmp);
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
        newRewards.Enqueue(reward);
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