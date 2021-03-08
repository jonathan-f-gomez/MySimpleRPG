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
        public Item RewardItem { get; set; }
        public List<QuestCompletionItem> QuestCompletionItems { get; set; }




        public Quest(int id, string name, string description, int rewardExperiencePoints,int rewardGold)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.RewardExperiencePoints = rewardExperiencePoints;
            this.RewardGold = rewardGold;

            QuestCompletionItems = new List<QuestCompletionItem>();
        }
    }
}
