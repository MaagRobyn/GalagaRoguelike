using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

[CreateAssetMenu]
public class ScriptableProjectile : ScriptableObject
{
    public ProjectileType type;
    public GameObject bulletPrefab;
    public float damage;
    public float velocity;
    public enum ProjectileType
    {
        Basic = 0
    }
}
