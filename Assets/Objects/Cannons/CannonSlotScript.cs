using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class CannonSlotScript : IdentifiableBehavior
{

    private ScriptableProjectile Projectile;
    private ScriptableCannon cannon;
    [SerializeField] Slot associatedSlot;

    public bool isSlotFilled = false;
    public bool isReadyToFire = false;

    float cannonDamageMult;
    float cannonVelocityMult;
    int cannonDamageMod;
    float cannonVelocityMod;
    float fireRate;

    public delegate void CannonEquip(ScriptableCannon c);
    public event CannonEquip OnCannonEquipped;

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
        EquipCannon(cannon);
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
            var t = Timer.AddTimer(fireRate);
            t.OnTimerEnd += () =>
            {
                isReadyToFire = true;
            };
        }
    }
    public void EquipCannon(ScriptableCannon newCannon)
    {
        cannon = newCannon;
        if(associatedSlot != null)
            PopulateSlotData();
        if (cannon != null)
            InvokeCannonEquip(newCannon);
    }
    public ScriptableCannon GetCannonData() { return cannon; }


    private void PopulateSlotData()
    {
        var textArr = associatedSlot.GetComponentsInChildren<TextMeshProUGUI>();
        if(cannon != null)
        {
            textArr[0].text = cannon.name;
            textArr[1].text = cannon.projectile.name;

        }
        else
        {
            textArr[0].text = "";
            textArr[1].text = "";

        }
    }
}
