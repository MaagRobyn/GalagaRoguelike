using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class CannonSlotScript : MonoBehaviour
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

    public delegate void CannonEquip();
    public event CannonEquip OnCannonEquipped;
    private float fireTimer;

    protected virtual void InvokeCannonEquip()
    {
        OnCannonEquipped?.Invoke();
    }

    private void Start()
    {
        isSlotFilled = false;
        OnCannonEquipped += () =>
        {
            if(cannon != null && !isSlotFilled)
            {
                isSlotFilled = true;
                cannonDamageMult = cannon.cannonDamageMult;
                cannonVelocityMult = cannon.cannonVelocityMult;
                cannonDamageMod = cannon.cannonDamageMod;
                cannonVelocityMod = cannon.cannonVelocityMod;
                fireRate = cannon.fireRate;
                GetComponent<SpriteRenderer>().sprite = cannon.sprite;
                Projectile = cannon.projectile;
            }
        };
    }
    private void Update()
    {
        if (cannon != null)
        {
            if(!isSlotFilled)
            {
                InvokeCannonEquip();

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
            var projectileObj = Instantiate(Projectile.bulletPrefab);
            projectileObj.transform.SetPositionAndRotation(transform.position, transform.rotation);
            projectileObj.name = "Projectile";
            projectileObj.layer = Constants.BULLET_TEAM_LAYER_NUM + (int)team;
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
        InvokeCannonEquip();
    }
}
