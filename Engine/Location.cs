using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
     public class Location
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Item ItemRequiredToEnter { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public Monster MonsterLivingHere { get; set; }
        public Location LocationToTheNorth { get; set; }
        public Location LocationToTheWest { get; set; }
        public Location LocationToTheEast { get; set; }
        public Location LocationToTheSouth { get; set; }


        public Location (int id, string name, string description,Item itemRequiredToEnter = null,
            Quest questAvailableHere = null, Monster monsterLivingHere = null)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.ItemRequiredToEnter = itemRequiredToEnter;
            this.QuestAvailableHere = questAvailableHere;
            this.MonsterLivingHere = monsterLivingHere;
        }
    }
}
