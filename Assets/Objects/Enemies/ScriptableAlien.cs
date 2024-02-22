using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableAlien : ScriptableObject
{
    public Sprite sprite;
    public Color color;
    public List<ScriptableCannon> cannons;
    public Vector3 scale;
    public int speed;
    public int health;
    public int dangerLevel;
    [Min(0.001f)]
    public float velocityMult;
    [Min(0.001f)]
    public float damageMult;
    [Min(0)]
    public float velocityMod;
    [Min(0)]
    public int damageMod;
}
