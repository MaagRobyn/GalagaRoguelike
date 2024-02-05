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
}