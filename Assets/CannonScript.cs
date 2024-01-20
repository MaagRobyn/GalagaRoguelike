using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class CannonScript : MonoBehaviour
{
    public ScriptableProjectile Projectile;
    
    /// <summary>
    /// Fires the projectile from the cannon
    /// </summary>
    /// <param name="damageMult">Damage multiplier</param>
    /// <param name="velocityMult">Velocity multiplier</param>
    /// <param name="damageMod">Damage Flat Bonus</param>
    /// <param name="velocityMod">Velocity Flat Bonus</param>
    public void ShootProjectile(Team team, float damageMult = 1, float velocityMult = 1, float damageMod = 0, float velocityMod = 0)
    {
        var projectile = Instantiate(Projectile);
        projectile.velocity = damageMult * projectile.damage + damageMod;
        projectile.velocity = velocityMult * projectile.velocity + velocityMod;
        
        var projectileObj = Instantiate(Projectile.bulletPrefab);
        projectileObj.transform.SetPositionAndRotation(transform.position, transform.rotation);
        projectileObj.name = "Projectile";
        projectileObj.layer = Constants.BULLET_TEAM_LAYER_NUM + (int)team;
        projectileObj.GetComponent<ProjectileScript>().properties = projectile;
        projectileObj.GetComponent<ProjectileScript>().team = team;

        //Debug.Log("bullet shot");
    }
}
