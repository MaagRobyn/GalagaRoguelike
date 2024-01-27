using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

[CreateAssetMenu]
public class ScriptableProjectile : ScriptableObject
{
    public ProjectileElement element;
    public GameObject bulletPrefab;
    public Sprite sprite;
    public int baseDamage;
    public float baseVelocity;
    public enum ProjectileElement
    {
        Kinetic = 0,
        Laser = 1,
        Plasma = 2,
        Acid = 3
    }
}
