using System.Collections;
using UnityEngine;

namespace Assets.Objects.Reward
{
    public interface IRewardAction
    {
        public void GiveReward(PlayerScript player, float factor);
    }

    public class CrewAction : IRewardAction
    {
        public void GiveReward(PlayerScript player, float factor)
        {
            
        }
    }
    public class NewCannonAction : IRewardAction
    {
        public ScriptableCannon cannon;
        public void GiveReward(PlayerScript player, float factor)
        {
            player.cannons[(int)factor].cannon = cannon;
        }
    }
}