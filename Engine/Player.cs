using System;
using System.Collections.Generic;


namespace Engine
{
    public class Player : LivingCreature
    {
        
        public int Gold { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }

public Player(int currentHitPoints, int maximumHitPoints,int gold, int experiencePoints, int level) : base(currentHitPoints, maximumHitPoints)
        {
            this.Gold = gold;
            this.Level = level;
            this.ExperiencePoints = experiencePoints;

            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

    }
}
