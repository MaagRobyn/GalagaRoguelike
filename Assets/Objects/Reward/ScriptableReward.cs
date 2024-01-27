using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableReward : ScriptableObject
{
    public string title;
    public string description;
    public RewardRarity rarity;
    public enum RewardRarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary,
        Unique
    }
}
