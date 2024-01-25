using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AlienShip : ScriptableObject
{
    public Sprite sprite;
    public List<CannonScript> cannons;
    public int speed;
    public int health;
    public int fireRate;
    public int dangerLevel;
    public float velocityMod;
    public float damageMod;
    public float velocityMult;
    public float damageMult;
}
