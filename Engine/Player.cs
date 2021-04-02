using System.Linq;
using System.Collections.Generic;


namespace Engine
{
    public class Player : LivingCreature
    {
        public int Gold { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level 
        {
            get
            {
                return ((ExperiencePoints / 100) + 1);
            }
        }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }
        public Location CurrentLocation { get; set; }


        public Player(int currentHitPoints, int maximumHitPoints,int gold, int experiencePoints) : base(currentHitPoints, maximumHitPoints)
        {
            this.Gold = gold;            
            this.ExperiencePoints = experiencePoints;

            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if (location.ItemRequiredToEnter == null)
            {
                // There is no required item for this location, so return "true"
                return true;
            }

            return Inventory.Exists(inventoryItem => inventoryItem.Details.ID == location.ItemRequiredToEnter.ID);
        }

        public bool HasThisQuest(Quest quest)
        {
            return Quests.Exists(playerQuest => playerQuest.Details.ID == quest.ID);

        }

        public bool CompletedThisQuest(Quest quest)
        {
            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == quest.ID)
                {
                    return playerQuest.IsCompleted;
                }
            }

            return false;
        }

        public bool HasAllQuestCompletionItems(Quest quest)
        {   
            // Check each item in the player's inventory, to see if they have it, and enough of it
            foreach (QuestCompletionItem completionItem in quest.QuestCompletionItems)
            {
                if (!Inventory.Exists(inventoryItem => inventoryItem.Details.ID == completionItem.Details.ID && inventoryItem.Quantity >= completionItem.Quantity))
                {
                    return false;
                }
            }

            // If we got here, then the player must have all the required items, and enough of them, to complete the quest.
            return true;
        }

        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItem questCompletionItem in quest.QuestCompletionItems)
            {
                foreach (InventoryItem inventoryItem in Inventory)
                {
                    if (inventoryItem.Details.ID == questCompletionItem.Details.ID)
                    {
                        // Subtract the quantity from the player's inventory that was needed to complete the quest
                        inventoryItem.Quantity -= questCompletionItem.Quantity;
                        break;
                    }
                }
            }
        }

        public void AddItemToInventory(Item itemToAdd)
        {
            foreach (InventoryItem inventoryItem in Inventory)
            {
                if (inventoryItem.Details.ID == itemToAdd.ID)
                {
                    // They have the item in their inventory, so increase the quantity by one
                    inventoryItem.Quantity++;

                    return; // We added the item, and are done, so get out of this function
                }
            }

            // They didn't have the item, so add it to their inventory, with a quantity of 1
            Inventory.Add(new InventoryItem(itemToAdd, 1));
        }

        public void MarkQuestCompleted(Quest quest)
        {
            // Find the quest in the player's quest list
            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == quest.ID)
                {
                    // Mark it as completed
                    playerQuest.IsCompleted = true;

                    return; // We found the quest, and marked it complete, so get out of this function
                }
            }
        }

    }
}
