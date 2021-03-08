using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Monster : LivingCreature
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaximumDamage { get; set; }
        public int RewardExperiencePoints { get; set; }
        public int RewardGold { get; set; }
        public List<LootItem> LootTable { get; set; }



        public Monster(int id, string name, int maximumDamage, int rewardExperiencePoints, int rewardGold,int maximumHitPoints, int minimumHitPoints)
            : base (maximumHitPoints, minimumHitPoints)
        {
            this.ID = id;
            this.Name = name;
            this.MaximumDamage = maximumDamage;
            this.RewardExperiencePoints = rewardExperiencePoints;
            this.RewardGold = rewardGold;
            this.MaximumDamage = maximumDamage;

            LootTable = new List<LootItem>();
        }

    }
}
