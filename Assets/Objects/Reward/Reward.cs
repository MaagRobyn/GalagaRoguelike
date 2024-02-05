using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public abstract class Reward : ScriptableObject
    {
        public string title;
        public string description;
        public Sprite sprite;
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
}