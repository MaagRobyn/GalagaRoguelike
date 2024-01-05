using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableBullet : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private float angle;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] public Collider2D collider;
    [SerializeField] public GameObject bulletPrefab;


    
}
