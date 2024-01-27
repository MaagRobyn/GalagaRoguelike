using UnityEngine;

[CreateAssetMenu]
public class ScriptableCannon : ScriptableObject
{
    public Sprite sprite;
    public ScriptableProjectile projectile;
    public float cannonDamageMult;
    public float cannonVelocityMult;
    public int cannonDamageMod;
    public float cannonVelocityMod;
    public float fireRate;
}