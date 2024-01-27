using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableAlien : ScriptableObject
{
    public Sprite sprite;
    public List<ScriptableCannon> cannons;
    public Vector3 scale;
    public int speed;
    public int health;
    public int dangerLevel;
    public float velocityMod;
    public int damageMod;
    public float velocityMult;
    public float damageMult;
}
