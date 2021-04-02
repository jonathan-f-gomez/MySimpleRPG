using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySimpleRPG
{
    public partial class MySimpleRPG : Form
    {
        private Player _player;
        private Monster _currentMonster;
        private int locationCounter = 0;

        public MySimpleRPG()
        {
            InitializeComponent();

            _player = new Player(10, 10, 20, 0);
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));

            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToTheNorth);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToTheSouth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToTheEast);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToTheWest);
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            //Gets the weapon thats selected in the combobox
            Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;

            //Determines the amount of damage to do to the Monster
            int damageToMonster = RandomNumberGenerator.NumberBetween(currentWeapon.MinimumDamage, currentWeapon.MaximumDamage);

            //Applies the damage to the monster
            _currentMonster.CurrentHitPoints -= damageToMonster;

            //displays the message
            rtbMessages.Text += $"You hit the {_currentMonster.Name} for {damageToMonster} damage!{Environment.NewLine}";

            //checks to see if the monster is dead
            if (_currentMonster.CurrentHitPoints <= 0)
            {
                //Monster died
                rtbMessages.Text = $"{Environment.NewLine}You defeated the {_currentMonster.Name}!{Environment.NewLine}";

                //give the Player XP
                _player.ExperiencePoints += _currentMonster.RewardExperiencePoints;
                rtbMessages.Text = $"You gained {_currentMonster.RewardExperiencePoints} XP!{Environment.NewLine}";

                //Give player gold
                _player.Gold += _currentMonster.RewardGold;
                rtbMessages.Text = $"You found {_currentMonster.RewardGold} gold on the {_currentMonster.Name}.{Environment.NewLine}";


                //Get random loot items from the monster
                List<InventoryItem> lootedItems = new List<InventoryItem>();

                //Pulling up loot table and usin gthe number generator to see if player can get lucky
                foreach (LootItem lootItem in _currentMonster.LootTable)
                {
                    if (RandomNumberGenerator.NumberBetween(1, 100) <= lootItem.DropPercentage)
                    {
                        lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                    }
                }

                //if nothing is seleected add default items
                if (lootedItems.Count == 0)
                {
                    foreach (LootItem lootItem in _currentMonster.LootTable)
                    {
                        if (lootItem.IsDefaultItem)
                        {
                            lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                        }
                    }
                }

                //add looted items to players inventory
                foreach (InventoryItem inventoryItem in lootedItems)
                {
                    _player.AddItemToInventory(inventoryItem.Details);

                    if (inventoryItem.Quantity == 1)
                    {
                        rtbMessages.Text += $"You loot {inventoryItem.Quantity} {inventoryItem.Details.Name} from the {_currentMonster.Name}.{Environment.NewLine}";
                    }
                    else
                    {
                        rtbMessages.Text += $"You loot {inventoryItem.Quantity} {inventoryItem.Details.NamePlural} from the {_currentMonster.Name}.{Environment.NewLine}";
                    }
                }

                // Refreshes the player information
                UpdatePlayerStats();

                UpdateInventoryListInUI();
                UpdateWeaponListInUI();
                UpdatePotionListInUI();


                //This adds a blank line only for appearance
                rtbMessages.Text += Environment.NewLine;
                ScrollToBottomOfMessages();

                //moves player to current location
                MoveTo(_player.CurrentLocation);


            }

            else
            {
                //The Monster is still alive
                int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentMonster.MaximumDamage);//TODO: IF DAMAGE = MAX Add Message calling it critical hit

                //The Display message 
                //TODO: Add that Monster missed
                rtbMessages.Text += $"The {_currentMonster.Name} hit you for {damageToPlayer} damage.{Environment.NewLine}";

                //Player loses health
                _player.CurrentHitPoints -= damageToPlayer;

                //Update display for hitpoints
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();

                //player death
                //TODO: Add player losing gold for getting transfered to thier house.
                if (_player.CurrentHitPoints <=0)
                {
                    rtbMessages.Text += $"The {_currentMonster.Name} killed you. {Environment.NewLine}";
                    locationCounter = 0;
                    MoveTo(World.LocationByID(World.LOCATION_ID_HOME));

                }




            }
        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            //gets the currently selected potion
            HealingPotion potion = (HealingPotion)cboPotions.SelectedItem;

            //adds healing amount to player health
            //TODO: does this work?
            _player.CurrentHitPoints += potion.AmountToHeal;

            //Makes sure that the player can't have more then thier allowed hitpoints
            if (_player.CurrentHitPoints > _player.MaximumHitPoints)
            {
                _player.CurrentHitPoints = _player.MaximumHitPoints;
            }

            //Remove the potion from the players inventory
            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if(inventoryItem.Details.ID == potion.ID) inventoryItem.Quantity--; //didnt add a break
                
            }
            //Displays that you drank a potion
            rtbMessages.Text += $"You drink a {potion.Name}.{Environment.NewLine}";


            //The Monster is Gets to attack the player after they heal
            int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentMonster.MaximumDamage);//TODO: IF DAMAGE = MAX Add Message calling it critical hit

            //The Display message 
            //TODO: Add that Monster missed
            rtbMessages.Text += $"The {_currentMonster.Name} hit you for {damageToPlayer} damage.";

            //Player loses health
            _player.CurrentHitPoints -= damageToPlayer;

            //player death
            //TODO: Add player losing gold for getting transfered to thier house.
            if (_player.CurrentHitPoints <= 0)
            {
                rtbMessages.Text += $"The {_currentMonster.Name} killed you {Environment.NewLine}";
                locationCounter = 0;
                MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            }

            //Update player UI
            UpdatePlayerStats();

            UpdateInventoryListInUI();
            UpdatePotionListInUI();

            ScrollToBottomOfMessages();
        }

        private void MoveTo(Location newLocation)
        {
            //Does the location have any required items
            if (!_player.HasRequiredItemToEnterThisLocation(newLocation))
            {
                rtbMessages.Text += $"You must have a {newLocation.ItemRequiredToEnter.Name} to enter this location.{Environment.NewLine}";
                return;
            }

            // Update the player's current location
            _player.CurrentLocation = newLocation;

            // Show/hide available movement buttons
            btnNorth.Visible = (newLocation.LocationToTheNorth != null);
            btnEast.Visible = (newLocation.LocationToTheEast != null);
            btnSouth.Visible = (newLocation.LocationToTheSouth != null);
            btnWest.Visible = (newLocation.LocationToTheWest != null);

            // Display current location name and description
            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text += newLocation.Description + Environment.NewLine;

            

            if (newLocation.ID == World.LOCATION_ID_HOME && locationCounter == 0)
            {
                rtbMessages.Text += $"You woke up in your home feeling rested. {Environment.NewLine}";
                ScrollToBottomOfMessages();
                locationCounter++;
            }
            else
            {
                rtbMessages.Text += $"You head toward {newLocation.Name}. {Environment.NewLine}";
                ScrollToBottomOfMessages();
                locationCounter++;
            }
            

            // Completely heal the player
            //TODO: Take this out 
            _player.CurrentHitPoints = _player.MaximumHitPoints;

            // Update Hit Points in UI
                UpdatePlayerStats();

            // Does the location have a quest?
            if (newLocation.QuestAvailableHere != null)
            {
                // See if the player already has the quest, and if they've completed it
                bool playerAlreadyHasQuest = _player.HasThisQuest(newLocation.QuestAvailableHere);
                bool playerAlreadyCompletedQuest = _player.CompletedThisQuest(newLocation.QuestAvailableHere);

                // See if the player already has the quest
                if (playerAlreadyHasQuest)
                {
                    // If the player has not completed the quest yet
                    if (!playerAlreadyCompletedQuest)
                    {
                        // See if the player has all the items needed to complete the quest
                        bool playerHasAllItemsToCompleteQuest = _player.HasAllQuestCompletionItems(newLocation.QuestAvailableHere);

                        // The player has all items required to complete the quest
                        if (playerHasAllItemsToCompleteQuest)
                        {
                            // Display message
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += "You complete the '" + newLocation.QuestAvailableHere.Name + " quest." + Environment.NewLine;

                            // Remove quest items from inventory
                            _player.RemoveQuestCompletionItems(newLocation.QuestAvailableHere);

                            // Give quest rewards
                            rtbMessages.Text += "You receive: " + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardExperiencePoints.ToString() + " experience points" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardGold.ToString() + " gold" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                            rtbMessages.Text += Environment.NewLine;

                            _player.ExperiencePoints += newLocation.QuestAvailableHere.RewardExperiencePoints;
                            _player.Gold += newLocation.QuestAvailableHere.RewardGold;

                            // Add the reward item to the player's inventory
                            _player.AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);

                            // Mark the quest as completed
                            _player.MarkQuestCompleted(newLocation.QuestAvailableHere);
                        }
                    }
                }
                else
                {
                    // The player does not already have the quest

                    // Display the messages
                    rtbMessages.Text += "You receive the " + newLocation.QuestAvailableHere.Name + " quest." + Environment.NewLine;
                    rtbMessages.Text += newLocation.QuestAvailableHere.Description + Environment.NewLine;
                    rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;
                    foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                    {
                        if (qci.Quantity == 1)
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                        }
                        else
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                        }
                    }
                    rtbMessages.Text += Environment.NewLine;
                    ScrollToBottomOfMessages();

                    // Add the quest to the player's quest list
                    _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
                }
            }

            // Does the location have a monster?
            if (newLocation.MonsterLivingHere != null)
            {
                rtbMessages.Text += $"You see a {newLocation.MonsterLivingHere.Name}. {Environment.NewLine}";
                ScrollToBottomOfMessages();

                // Make a new monster, using the values from the standard monster in the World.Monster list
                Monster standardMonster = World.MonsterByID(newLocation.MonsterLivingHere.ID);

                _currentMonster = new Monster(standardMonster.ID, standardMonster.Name, standardMonster.MaximumDamage,
                    standardMonster.RewardExperiencePoints, standardMonster.RewardGold, standardMonster.CurrentHitPoints, standardMonster.MaximumHitPoints);

                foreach (LootItem lootItem in standardMonster.LootTable)
                {
                    _currentMonster.LootTable.Add(lootItem);
                }

                cboWeapons.Visible = true;
                cboPotions.Visible = true;
                btnUseWeapon.Visible = true;
                btnUsePotion.Visible = true;
            }
            else
            {
                _currentMonster = null;

                cboWeapons.Visible = false;
                cboPotions.Visible = false;
                btnUseWeapon.Visible = false;
                btnUsePotion.Visible = false;
            }

            // Refresh player's inventory list
            UpdateInventoryListInUI();

            // Refresh player's quest list
            UpdateQuestListInUI();

            // Refresh player's weapons combobox
            UpdateWeaponListInUI();

            // Refresh player's potions combobox
            UpdatePotionListInUI();

            //Updates Players UI
            UpdatePlayerStats();
        }

        private void UpdateInventoryListInUI()
        {
            dgvInventory.RowHeadersVisible = false;

            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";

            dgvInventory.Rows.Clear();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] { inventoryItem.Details.Name, inventoryItem.Quantity.ToString() });
                }
            }
        }

        private void UpdateQuestListInUI()
        {
            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";

            dgvQuests.Rows.Clear();

            foreach (PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }
        }

        private void UpdateWeaponListInUI()
        {
            List<Weapon> weapons = new List<Weapon>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Weapon)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        weapons.Add((Weapon)inventoryItem.Details);
                    }
                }
            }

            if (weapons.Count == 0)
            {
                // The player doesn't have any weapons, so hide the weapon combobox and "Use" button
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";

                cboWeapons.SelectedIndex = 0;
            }
        }

        private void UpdatePotionListInUI()
        {
            List<HealingPotion> healingPotions = new List<HealingPotion>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is HealingPotion)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        healingPotions.Add((HealingPotion)inventoryItem.Details);
                    }
                }
            }

            if (healingPotions.Count == 0)
            {
                // The player doesn't have any potions, so hide the potion combobox and "Use" button
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";

                cboPotions.SelectedIndex = 0;
            }
        }

        private void ScrollToBottomOfMessages()
        {
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }

        private void UpdatePlayerStats()
        {
            //Refreshes the players information on the screen
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblLevel.Text = _player.Level.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();



        }

    }
}

