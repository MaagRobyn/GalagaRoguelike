using UnityEngine;

[CreateAssetMenu]
public class ScriptableCannon : ScriptableObject
{
    public Sprite sprite;
    public ScriptableProjectile projectile;
    [Min(0.001f)]
    public float cannonDamageMult;
    [Min(0.001f)]
    public float cannonVelocityMult;
    [Min(0)]
    public int cannonDamageMod;
    [Min(0)]
    public float cannonVelocityMod;
    [Min(0.001f)]
    public float fireRate;
}