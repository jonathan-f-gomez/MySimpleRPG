using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Quest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RewardExperiencePoints { get; set; }
        public int RewardGold { get; set; }

        public Quest(int id, string name, string description, int rewardExperiencePoints,int rewardGold)
        {
            this.Description = description;
            this.ID = id;
            this.Name = name;
            this.RewardExperiencePoints = rewardExperiencePoints;
            this.RewardGold = rewardGold;
        }
    }
}
