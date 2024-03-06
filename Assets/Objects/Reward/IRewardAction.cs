using System.Collections;
using UnityEngine;

namespace Assets.Objects.Reward
{
    public interface IRewardAction
    {
        public void GiveReward(PlayerScript player, int factor = 0);
    }

    public class CrewAction : IRewardAction
    {
        public CrewMember crew;
        public void GiveReward(PlayerScript player, int factor = 0)
        {
            player.crewMembers.Add(crew);
        }
    }
    public class NewCannonAction : IRewardAction
    {
        public Canvas OptionMenu;
        public ScriptableCannon cannon;
        public void GiveReward(PlayerScript player, int factor = 0)
        {
            player.cannons[factor].EquipCannon(cannon);
        }
    }
}