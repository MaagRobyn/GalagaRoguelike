using System.Collections;
using UnityEngine;

namespace Assets
{
    public abstract class Reward : ScriptableObject
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public abstract void GiveReward();
    }
}