using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Robit_Game.Classes;
using Robit_Game.Properties;

namespace Robit_Game
{
    public partial class RobitGame : Form
    {
        readonly OverWorld World;
        readonly Battle Battle;
        readonly Inventory Inventory;
        bool IsBattle;
        int LevelUp;
        int Turn;
        new int Move = -1;
        bool AwaitingEquip = false;
        int SelectedItem = -1;
        int SelectedItemType = 2;
        int LastEquippedBadge;
        enum InvMode { Items, Abilities, Badges };
        enum Characters { Abel, K813, Sarge, Enemy1, Enemy2, Enemy3, Enemy4};
        enum Directions { North, East, South, West};
        public enum StatusEffects { Glitched, Melted, Rusted, ExtraTurn, HPRegen, AtkBonus, AtkPenalty, DefBonus, DefPenalty };
        public enum BadgeEffects { Buggernaut, MachineDatabase, Nanomachines, AdaptiveControlUnit, MatterAnnihilator, EmergencyOffenseReserve, EmergencyDefenseReserve, InductionCoil, MinimalistArchetecture, CapacitorSize, ItemAutoLoader, ParryDatabase };
        public RobitGame()
        {
            InitializeComponent();
            World = new OverWorld();
            Battle = new Battle();
            Inventory = new Inventory(Battle);
            IsBattle = false;
            BeginBattle(new int[] {14, 3, 3, 3});
            for (int z = 0; z < 20; z++)//DEBUG LOOP! DO NOT KEEP
            {
                Inventory.Badges.Add(Inventory.BadgePrototypes[z]);
            }
            for (int z = 0; z < 10; z++)//DEBUG LOOP! DO NOT KEEP
            {
                Inventory.ItemIDs.Add(z);
            }
            UpdateInventory((int) InvMode.Badges);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrintStory(World.UpdateStory());
        }
        private void DescriptionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DescriptionBox.ClearSelected();
        }


        private void InventoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InventoryBox.SelectedIndex == -1)
                return;
            UpdateDescriptionBox(InventoryBox.SelectedIndex);
            SelectedItem = InventoryBox.SelectedIndex;
            if (SelectedItemType != (int)InvMode.Abilities)
                AwaitingEquip = true;
            else
            {
                Move = Battle.Combatants[Turn].ValidAttacks[SelectedItem];
                if(Battle.MP < Inventory.Abilities[Battle.Combatants[Turn].ValidAttacks[SelectedItem]].MPCost + Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.CapacitorSize] && Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.MatterAnnihilator] < 1)
                {
                    Move = -1;
                }
                if (Battle.Combatants[Turn].HP <= Inventory.Abilities[Battle.Combatants[Turn].ValidAttacks[SelectedItem]].MPCost + Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.CapacitorSize] && Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.MatterAnnihilator] > 0)
                {
                    Move = -1;
                }
            }
        }
        private void UpdateDescriptionBox(int index)
        {
            if (index < 0 || index > InventoryBox.Items.Count)//If selected index is out of bounds.
            {
                return;
            }
            string[] Description = Inventory.PrintDescription(index, Turn);
            DescriptionBox.Items.Clear();
            foreach (string x in Description)
            {
                DescriptionBox.Items.Add(x);
            }
        }
        public void AddLoreBox(string phrase)
        {
            LoreBox.Items.Insert(0, phrase);
        }
        public void LoreBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoreBox.ClearSelected();
        }
        private void NorthButton_Click(object sender, EventArgs e)
        {
            if (IsBattle)
            {
                PrintStory(Battle.RunTurn(0, Turn, 0));//Runs the defend function.
                if (Battle.Combatants[Turn].StatusEffects[(int)StatusEffects.ExtraTurn] > 0)
                    Battle.Combatants[Turn].StatusEffects[(int)StatusEffects.ExtraTurn]--;
                else
                    Turn++;
                if (Turn <= 2)
                {
                    UpdateInventory(SelectedItemType);
                }
                UpdateBattle();
                if (!IsBattle && LevelUp != 1)
                {
                    UpdateButtons(World.FetchNeighbors());
                }
                if (Turn == (int) Characters.Enemy1)
                {
                    RunEnemyTurns();
                }
            }
            else
            {
                if (LevelUp == 1)
                {
                    for (int y = 0; y < 3; y++)//Upgrade Max HP. Full HP Refil.
                    {
                        Battle.Combatants[y].MaxHP += 2;
                        Battle.Combatants[y].HP = Battle.Combatants[y].MaxHP;
                    }
                    UpdateBattle();
                    UpdateButtons(World.FetchNeighbors());
                    LevelUp = 0;
                }
                else
                {
                    Translate((int) Directions.North);
                }
            }
        }//Complete!
        private void EastButton_Click(object sender, EventArgs e)
        {
            if (IsBattle)
            {
                Move = 1;
                PrintStory(new string[] { "Choose a target to attack." });
            }
            else
            {
                if (LevelUp == 1)//Upgrade and refil MP.
                {
                    Battle.MaxMP += 5;
                    Battle.MP = Battle.MaxMP;
                    UpdateBattle();
                    UpdateButtons(World.FetchNeighbors());
                    LevelUp = 0;
                }
                else
                {
                    Translate((int) Directions.East);
                }
            }
        }
        private void SouthButton_Click(object sender, EventArgs e)
        {
            if (IsBattle)
            {
                PrintStory(Battle.Flee());//Flee!
                UpdateBattle();
                IsBattle = false;
                if (!IsBattle&&LevelUp!=1)
                {
                    UpdateButtons(World.FetchNeighbors());
                }
                if (Turn == 3)
                {
                    RunEnemyTurns();
                }
            }
            else
            {
                if (LevelUp != 1)
                {
                    Translate((int) Directions.South);
                }
            }
        }//Complete!
        private void WestButton_Click(object sender, EventArgs e)
        {
            if (IsBattle)
            {
                //Ability!
                AwaitingEquip = false;
                SelectedItemType = (int)InvMode.Abilities;
                UpdateInventory((int)InvMode.Abilities);
                PrintStory(new string[] { "Choose an ability." });

            }
            else
            {
                if (LevelUp == 1)
                {
                    Inventory.MaxBP += 3;
                    Inventory.BP += 3;
                    LevelUp = 0;
                }
                else
                {
                    Translate((int) Directions.West);
                }
            }
        }
        private void BadgeButton_Click(object sender, EventArgs e)
        {
            UpdateInventory((int)InvMode.Badges);
            SelectedItemType = (int)InvMode.Badges;
        }
        private void ItemButton_Click(object sender, EventArgs e)
        {
            UpdateInventory((int)InvMode.Items);
            SelectedItemType = (int)InvMode.Items;
        }
        private void RunPlayerTurn(int Target)
        {
            if (Target == -1 || Move == -1 || Turn > (int) Characters.Sarge)
            {
                return;
            }
            for (int x = 0; x < 2; x++)//This block of code may need to run twice, if both Abel and K-813 are dead. This will not need to be done a third time, as if this is the case, it is already game over.
            {
                if (Battle.Combatants[Turn].HP == 0 || Battle.Combatants[Turn].StatusEffects[(int) StatusEffects.Glitched] > 0)
                {
                    Turn++;
                    if (Turn == (int) Characters.Enemy1)
                    {
                        return;
                    }
                }
            }
            
            PrintStory(Battle.RunTurn(Move, Turn, Target));

            Move = -1;
            UpdateBattle();
            if (Battle.Combatants[Turn].StatusEffects[(int)StatusEffects.ExtraTurn] > 0)
                Battle.Combatants[Turn].StatusEffects[(int)StatusEffects.ExtraTurn]--;
            else
                Turn++;
            if (!IsBattle && LevelUp != 1)
            {
                UpdateButtons(World.FetchNeighbors());
            }
            if (Turn == 3)
            {
                RunEnemyTurns();
            }
            if (SelectedItemType == (int)InvMode.Abilities)
            {
                UpdateInventory((int)InvMode.Abilities);
            }
        }
        private void RunEnemyTurns()
        {
            Random RNG = new Random();
            int Target;
            while (Turn < (int) Characters.Enemy4 + 1)
            {
                if (Battle.Combatants[Turn].HP > 0)
                {
                    do
                    {
                        Target = RNG.Next((int) Characters.Abel, (int) Characters.Sarge + 1);
                    } while (Battle.Combatants[Target].HP <= 0);
                    Move = Battle.Combatants[Turn].ValidAttacks[RNG.Next(0, Battle.Combatants[Turn].ValidAttacks.Length)];//Picks a random attack from the list of attacks.
                    PrintStory(Battle.RunTurn(Move, Turn, Target));
                    UpdateBattle();
                }
                if (Battle.Combatants[Turn].StatusEffects[(int)StatusEffects.ExtraTurn] > 0)
                    Battle.Combatants[Turn].StatusEffects[(int)StatusEffects.ExtraTurn]--;
                else
                    Turn++;
            }
            Move = -1;
            Turn = 0;
            Battle.RunRoundCalculations();
            while (Battle.Combatants[Turn].HP <= 0 || Battle.Combatants[Turn].StatusEffects[(int) StatusEffects.Glitched] > 0)
            {
                Turn++;
            }
        }
        private void PrintStory(string[] phrases)
        {
            for(int x = phrases.Length - 1 ; x >= 0 ; x--)
            {
                LoreBox.Items.Insert(0, phrases[x]);
            }
        }
        private void UpdateInventory(int InvMode)
        {
            string[] Items = Inventory.GetInventory(InvMode, Turn);
            InventoryBox.Items.Clear();
            for (int x = 0 ; x <  Items.Length ; x++)
            {
                InventoryBox.Items.Add(Items[x]);
            }
        }
        private void Translate(int Direction)
        {
            if (World.IsStoryTrigger())
            {
                PrintStory(World.UpdateStory());
            }
            World.Translate(Direction);
            LoreBox.Items.Insert(0, World.PrintDescription());
            UpdateButtons(World.FetchNeighbors());
        }
        private void BeginBattle(int[] IDs)
        {
            IsBattle = true;
            Battle.BeginBattle(IDs);
            Battle.Combatants[3].Tattled = true; //DEBUG LINE!
            UpdateBattle();
        }
        private void UpdateBattle()
        {
            int[] HPs = Battle.FetchRealHPs();
            int AlliesHP;
            int EnemiesHP;
            int x;
            UpdateButtons(Battle.UpdateButtons());
            UpdateNames(Battle.FetchNames());
            UpdateHPBars(Battle.FetchMaxHPs(), Battle.FetchHPs());
            UpdateMPBar(Battle.MaxMP, Battle.MP);
            AlliesHP = 0;
            for (x = (int) Characters.Abel ; x < (int) Characters.Sarge + 1 ; x++)
            {
                AlliesHP += HPs[x];
            }
            if(AlliesHP <= 0)
            {
                Battle.GameOver();
                IsBattle = false;
            }
            EnemiesHP = 0;
            for (x = (int) Characters.Enemy1 ; x < (int) Characters.Enemy4 + 1 ; x++ )
            {
                EnemiesHP += HPs[x];
            }
            if(EnemiesHP <= 0)
            {
                PrintStory(Battle.EndBattle());
                IsBattle = false;
                Turn = (int) Characters.Abel;
                if (Battle.EXP/100 > Battle.Combatants[(int) Characters.Abel].Level)
                {
                    PrintStory(new string[] { "Level up!", "Pick a stat to improve." });
                    UpdateButtons(new string[] { "+2 Max HP each", "+5 Max MP", " ", "+3 Max BP" });
                    LevelUp = 1;
                    Battle.Combatants[(int) Characters.Abel].Level++;
                    for(int y = (int) Characters.Abel ; y < (int) Characters.Sarge + 1 ; y++)
                    {
                        Battle.Combatants[y].HP = Battle.Combatants[y].MaxHP;
                        Battle.MP = Battle.MaxMP;
                    }
                }
            }
            if(Turn >= (int) Characters.Enemy4)
            {
                Turn = (int) Characters.Abel;
            }
            while (Battle.Combatants[Turn].HP == 0 || Battle.Combatants[Turn].StatusEffects[(int)StatusEffects.Glitched] > 0)//Skip dead characters' turns.
            {
                Turn++;
                if(Turn > (int) Characters.Enemy4)
                {
                    Turn = (int) Characters.Abel;
                }
            }
        }
        private void UpdateHPBars(int[] MaxHPs, int[] HPs)
        {
            AHPBar.Maximum = MaxHPs[(int) Characters.Abel];
            AHPBar.Value = HPs[(int)Characters.Abel];

            KHPBar.Maximum = MaxHPs[(int)Characters.K813];
            KHPBar.Value = HPs[(int)Characters.K813];

            SHPBar.Maximum = MaxHPs[(int)Characters.Sarge];
            SHPBar.Value = HPs[(int)Characters.Sarge];

            E1HPBar.Maximum = MaxHPs[(int)Characters.Enemy1];
            E1HPBar.Value = HPs[(int)Characters.Enemy1];
            
            E2HPBar.Maximum = MaxHPs[(int)Characters.Enemy2];
            E2HPBar.Value = HPs[(int)Characters.Enemy2];

            E3HPBar.Maximum = MaxHPs[(int)Characters.Enemy3];
            E3HPBar.Value = HPs[(int)Characters.Enemy3];

            E4HPBar.Maximum = MaxHPs[(int)Characters.Enemy4];
            E4HPBar.Value = HPs[(int)Characters.Enemy4];
        }
        private void UpdateNames(string[] names)
        {
            AButton.Text = names[(int)Characters.Abel];
            KButton.Text = names[(int)Characters.K813];
            SButton.Text = names[(int)Characters.Sarge];
            E1Button.Text = names[(int)Characters.Enemy1];
            E2Button.Text = names[(int)Characters.Enemy2];
            E3Button.Text = names[(int)Characters.Enemy3];
            E4Button.Text = names[(int)Characters.Enemy4];
        }
        private void UpdateButtons(string[] FaceNames)
        {
            NorthButton.Text = FaceNames[(int) Directions.North];
            EastButton.Text = FaceNames[(int)Directions.East];
            SouthButton.Text = FaceNames[(int)Directions.South];
            WestButton.Text = FaceNames[(int)Directions.West];
        }
        private void UpdateMPBar(int Max, int Value)
        {
            MPBar.Maximum = Max;
            MPBar.Value = Value;
            MPLabel.Text = Value + "/" + Max;
        }
        private void AButton_Click(object sender, EventArgs e)
        {
            RunButtonLogic((int) Characters.Abel);
        }
        private void KButton_Click(object sender, EventArgs e)
        {
            RunButtonLogic((int)Characters.K813);
        }
        private void SButton_Click(object sender, EventArgs e)
        {
            RunButtonLogic((int)Characters.Sarge);
        }
        private void E1Button_Click(object sender, EventArgs e)
        {
            RunButtonLogic((int)Characters.Enemy1);
        }
        private void E2Button_Click(object sender, EventArgs e)
        {
            RunButtonLogic((int)Characters.Enemy2);
        }
        private void E3Button_Click(object sender, EventArgs e)
        {
            RunButtonLogic((int)Characters.Enemy3);
        }
        private void E4Button_Click(object sender, EventArgs e)
        {
            RunButtonLogic((int)Characters.Enemy4);
        }
        private void RunButtonLogic(int Character)
        {
            LastEquippedBadge = SelectedItem;
            if (AwaitingEquip && SelectedItem >= 0)
            {
                if (Character>2&&SelectedItemType == (int)InvMode.Badges)
                {
                    PrintStory(new string[] { "Cannot equip badges to enemies!"});
                }
                else if (Inventory.UseItem(SelectedItem, SelectedItemType, Character))
                {
                    UpdateInventory(SelectedItemType);
                    PrintItemUsed(Character);
                }
                else
                    PrintStory(new string[] { $"{Battle.Combatants[Turn].Name} failed to equip the badge to {Battle.Combatants[Character].Name}. Do you have enough BP?" });
            }
            if (SelectedItemType == (int) InvMode.Badges)//If a badge is equipped, the text box must update BP total.
            {
                UpdateDescriptionBox(LastEquippedBadge);
            }
            if (Move >= 0)
            {
                if (Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.MatterAnnihilator] < 1)
                {
                    if (SelectedItemType == (int)InvMode.Abilities && Battle.MP >= Inventory.Abilities[Move].MPCost + Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.CapacitorSize])
                    {
                        Battle.MP -= Inventory.Abilities[SelectedItem].MPCost + Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.CapacitorSize];
                    }
                }
                else
                {
                    if (SelectedItemType == (int)InvMode.Abilities && Battle.Combatants[Turn].HP > Inventory.Abilities[Move].MPCost + Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.CapacitorSize])
                    {
                        Battle.Combatants[Turn].HP -= Inventory.Abilities[SelectedItem].MPCost + Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.CapacitorSize];
                    }
                }
            }
            InventoryBox.ClearSelected();
            RunPlayerTurn(Character);
        }
        private void PrintItemUsed(int target)
        {
            string TargetName;
            List<string> Log = new List<string>();
            switch(SelectedItemType)
            {
                case (int)InvMode.Items:
                    if (Turn == target && Inventory.ItemPrototypes[SelectedItem].TeamHeal == false)
                    {
                        if (Turn == (int) Characters.K813)
                            Log.Add($"{Battle.Combatants[Turn].Name} used {Inventory.LastItemUsed} on herself.");
                        else
                            Log.Add($"{Battle.Combatants[Turn].Name} used {Inventory.LastItemUsed} on himself.");
                    }
                    else
                    {
                        if (Inventory.ItemPrototypes[SelectedItem].TeamHeal == true)
                        {
                            if (target <= (int)Characters.Sarge)
                                TargetName = "all allies";
                            else
                                TargetName = "all enemies";
                        }
                        else
                            TargetName = Battle.Combatants[target].Name;
                        Log.Add($"{Battle.Combatants[Turn].Name} used {Inventory.LastItemUsed} on {TargetName}.");
                    }
                    break;
                case (int)InvMode.Abilities:
                    break;
                case (int)InvMode.Badges:
                    if (Turn == target)
                    {
                        if (Turn == (int) Characters.K813)
                            Log.Add($"{Battle.Combatants[Turn].Name} modified her own badges.");
                        else
                            Log.Add($"{Battle.Combatants[Turn].Name} modified his own badges.");
                    }
                    else
                        Log.Add($"{Battle.Combatants[Turn].Name} modified {Battle.Combatants[target].Name}'s badges.");
                    break;
            }
            PrintStory(Log.ToArray());
            if (Battle.Combatants[Turn].StatusEffects[(int)StatusEffects.ExtraTurn] > 0)
                Battle.Combatants[Turn].StatusEffects[(int)StatusEffects.ExtraTurn]--;
            else
                Turn++;
            UpdateBattle();
            if(Turn > (int) Characters.Sarge)
            {
                RunEnemyTurns();
            }
        }
        private void AHPBar_Click(object sender, EventArgs e)
        {

        }
        private void KHPBar_Click(object sender, EventArgs e)
        {

        }
        private void SHPBar_Click(object sender, EventArgs e)
        {

        }
        private void E1HPBar_Click(object sender, EventArgs e)
        {

        }
        private void E2HPBar_Click(object sender, EventArgs e)
        {

        }
        private void E3HPBar_Click(object sender, EventArgs e)
        {

        }
        private void E4HPBar_Click(object sender, EventArgs e)
        {

        }
        private void MPLabel_Click(object sender, EventArgs e)
        {
            
        }
    }
}