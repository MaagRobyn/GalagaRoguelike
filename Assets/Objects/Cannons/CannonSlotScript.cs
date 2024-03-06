using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class CannonSlotScript : IdentifiableBehavior
{

    private ScriptableProjectile Projectile;
    public ScriptableCannon cannon;

    public bool isSlotFilled = false;
    public bool isReadyToFire = false;

    float cannonDamageMult;
    float cannonVelocityMult;
    int cannonDamageMod;
    float cannonVelocityMod;
    float fireRate;

    public delegate void CannonEquip(ScriptableCannon c);
    public event CannonEquip OnCannonEquipped;
    private float fireTimer;

    protected virtual void InvokeCannonEquip(ScriptableCannon cannon)
    {
        OnCannonEquipped?.Invoke(cannon);
    }

    private void Awake()
    {
        Id = ++LastID;
    }

    private void Start()
    {
        isSlotFilled = false;
        OnCannonEquipped += c =>
        {
            if(c != null)
            {
                isSlotFilled = true;
                cannonDamageMult = c.cannonDamageMult;
                cannonVelocityMult = c.cannonVelocityMult;
                cannonDamageMod = c.cannonDamageMod;
                cannonVelocityMod = c.cannonVelocityMod;
                fireRate = c.fireRate;
                GetComponent<SpriteRenderer>().sprite = c.sprite;
                Projectile = c.projectile;
            }
            else
            {
                Debug.LogError("Attempted to equip a null cannon");
            }
        };
    }
    private void Update()
    {
        if (cannon != null)
        {
            if(!isSlotFilled )
            {
                EquipCannon(cannon);

            }
            else
            {
                if (fireTimer > 0)
                {
                    fireTimer -= Time.deltaTime;
                }
                else if(!isReadyToFire)
                {
                    isReadyToFire = true;
                }
            }
        }
    }

    /// <summary>
    /// Fires the projectile from the cannon
    /// </summary>
    /// <param name="damageMult">Damage multiplier</param>
    /// <param name="velocityMult">Velocity multiplier</param>
    /// <param name="damageMod">Damage Flat Bonus</param>
    /// <param name="velocityMod">Velocity Flat Bonus</param>
    public void ShootProjectile(Team team, float damageMult = 1, float velocityMult = 1, int damageMod = 0, float velocityMod = 0)
    {
        if (isSlotFilled && isReadyToFire)
        {
            var projectileObj = Instantiate(Projectile.bulletPrefab, transform);
            projectileObj.transform.SetPositionAndRotation(transform.position, transform.rotation);
            projectileObj.name = "Projectile";
            projectileObj.layer = Globals.BULLET_TEAM_LAYER_NUM + (int)team;
            var projectileScript = projectileObj.GetComponent<ProjectileScript>();
            projectileScript.SetProperties(Projectile);
            projectileScript.SetDamage(damageMult * cannonDamageMult, damageMod + cannonDamageMod);
            projectileScript.SetVelocity(velocityMult * cannonVelocityMult, velocityMod + cannonVelocityMod);
            isReadyToFire = false;
            fireTimer = fireRate;
        }
    }
    public void EquipCannon(ScriptableCannon newCannon)
    {
        cannon = newCannon;
        InvokeCannonEquip(newCannon);
    }
}
