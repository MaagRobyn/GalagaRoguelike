using System.Collections;
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
        public abstract void GiveReward();
    }
}