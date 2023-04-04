using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using Robit_Game.Properties;
using System.Security.Cryptography;
using Robit_Game.Classes;

namespace Robit_Game
{
    public partial class RobitGame : Form
    {
        OverWorld World;
        Battle Battle;
        Inventory Inventory;
        bool IsBattle;
        int LevelUp;
        int Turn;
        new int Move;
        enum InvMode { Items, Abilities, Badges };
        public RobitGame()
        {
            InitializeComponent();
            World = new OverWorld();
            Battle = new Battle();
            Inventory = new Inventory(Battle);
            IsBattle = false;
            BeginBattle(new int[] {14, 3, 3, 3});
            for (int z = 0; z < 5; z++)//DEBUG LOOP! DO NOT KEEP
            {
                Inventory.Badges.Append(Inventory.BadgePrototypes[z]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrintStory(World.UpdateStory());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void InventoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void AddLoreBox(string phrase)
        {
            LoreBox.Items.Insert(0, phrase);
        }
        public void LoreBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void NorthButton_Click(object sender, EventArgs e)
        {
            if (IsBattle)
            {
                PrintStory(Battle.RunTurn(0, Turn, 0));//Runs the defend function.
                Turn++;
                UpdateBattle();
                if (!IsBattle && LevelUp != 1)
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
                    Translate(0);
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
                    Translate(1);
                }
            }
        }
        private void SouthButton_Click(object sender, EventArgs e)
        {
            if (IsBattle)
            {
                PrintStory(Battle.Flee());//Flee!
                Turn++;
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
                    Translate(2);
                }
            }
        }//Complete!
        private void WestButton_Click(object sender, EventArgs e)
        {
            if (IsBattle)
            {
                //Ability!
                UpdateInventory((int)InvMode.Abilities);
                PrintStory(new string[] { "Choose an ability." });
            }
            else
            {
                if (LevelUp == 1)
                {

                    LevelUp = 0;
                }
                else
                {
                    Translate(3);
                }
            }
        }
        private void BadgeButton_Click(object sender, EventArgs e)
        {
            UpdateInventory((int)InvMode.Badges);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            UpdateInventory((int)InvMode.Badges);
        }
        private void RunPlayerTurn(int Target)
        {
            if (Target == -1 || Move == -1 || Turn > 2)
            {
                return;
            }
            for (int x = 0; x < 2; x++)//This block of code may need to run twice, if both Abel and K-813 are dead. This will not need to be done a third time, as if this is the case, it is already game over.
            {
                if (Battle.Combatants[Turn].HP == 0)
                {
                    Turn++;
                    if (Turn == 3)
                    {
                        return;
                    }
                }
            }
            
            PrintStory(Battle.RunTurn(Move, Turn, Target));

            Move = -1;
            Target = -1;
            UpdateBattle();
            Turn++;
            if (!IsBattle && LevelUp != 1)
            {
                UpdateButtons(World.FetchNeighbors());
            }
            if (Turn == 3)
            {
                RunEnemyTurns();
            }
        }
        private void RunEnemyTurns()
        {
            Random RNG = new Random();
            int Target;
            while (Turn<7)
            {
                if (Battle.Combatants[Turn].HP > 0)
                {
                    do
                    {
                        Target = RNG.Next(0, 3);
                    } while (Battle.Combatants[Target].HP <= 0);
                    Move = Battle.Combatants[Turn].ValidAttacks[RNG.Next(0, Battle.Combatants[Turn].ValidAttacks.Length)];//Picks a random attack from the list of attacks.
                    PrintStory(Battle.RunTurn(Move, Turn, Target));
                    UpdateBattle();
                }
                Turn++;
            }
            Move = -1;
            Turn = 0;
            while (Battle.Combatants[Turn].HP <= 0)
            {
                Turn++;
            }
        }
        private void PrintStory(string[] phrases)
        {
            for(int x=phrases.Length-1;x>=0;x--)
            {
                LoreBox.Items.Insert(0, phrases[x]);
            }
        }
        private void UpdateInventory(int InvMode)
        {
            string[] Items = Inventory.GetInventory(InvMode);
            InventoryBox.Items.Clear();
            for (int x = Items.Length - 1; x >= 0; x--)
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
            int[] HPs = Battle.FetchHPs();
            int AlliesHP;
            int EnemiesHP;
            int x;
            UpdateButtons(Battle.UpdateButtons());
            UpdateNames(Battle.FetchNames());
            UpdateHPBars(Battle.FetchMaxHPs(), Battle.FetchHPs());
            UpdateMPBar(Battle.MaxMP, Battle.MP);
            HPs = Battle.FetchRealHPs();
            AlliesHP = 0;
            for (x = 0; x < 3; x++)
            {
                AlliesHP += HPs[x];
            }
            if(AlliesHP <= 0)
            {
                Battle.GameOver();
                IsBattle = false;
            }
            EnemiesHP = 0;
            for (x=3;x<7;x++)
            {
                EnemiesHP += HPs[x];
            }
            if(EnemiesHP <= 0)
            {
                PrintStory(Battle.EndBattle());
                IsBattle = false;
                Turn = 0;
                if (Battle.EXP/100 > Battle.Combatants[0].Level)
                {
                    PrintStory(new string[] { "Level up!", "Pick a stat to improve." });
                    UpdateButtons(new string[] { "+2 Max HP each", "+5 Max MP", " ", "+3 Max BP" });
                    LevelUp = 1;
                    Battle.Combatants[0].Level++;
                    for(int y = 0;y<3;y++)
                    {
                        Battle.Combatants[y].HP = Battle.Combatants[y].MaxHP;
                        Battle.MP = Battle.MaxMP;
                    }
                }
            }
            if(Turn>=7)
            {
                Turn = 0;
            }
            while (Battle.Combatants[Turn].HP == 0)//Skip dead characters' turns.
            {
                Turn++;
                if(Turn>=7)
                {
                    Turn = 0;
                }
            }
        }
        private void UpdateHPBars(int[] MaxHPs, int[] HPs)
        {
            AHPBar.Maximum = MaxHPs[0];
            AHPBar.Value = HPs[0];

            KHPBar.Maximum = MaxHPs[1];
            KHPBar.Value = HPs[1];

            SHPBar.Maximum = MaxHPs[2];
            SHPBar.Value = HPs[2];

            E1HPBar.Maximum = MaxHPs[3];
            E1HPBar.Value = HPs[3];
            
            E2HPBar.Maximum = MaxHPs[4];
            E2HPBar.Value = HPs[4];

            E3HPBar.Maximum = MaxHPs[5];
            E3HPBar.Value = HPs[5];

            E4HPBar.Maximum = MaxHPs[6];
            E4HPBar.Value = HPs[6];

        }
        private void UpdateNames(string[] names)
        {
            AButton.Text = names[0];
            KButton.Text = names[1];
            SButton.Text = names[2];
            E1Button.Text = names[3];
            E2Button.Text = names[4];
            E3Button.Text = names[5];
            E4Button.Text = names[6];
        }
        private void UpdateButtons(string[] FaceNames)
        {
            NorthButton.Text = FaceNames[0];
            EastButton.Text = FaceNames[1];
            SouthButton.Text = FaceNames[2];
            WestButton.Text = FaceNames[3];
        }
        private void UpdateMPBar(int Max, int Value)
        {
            MPBar.Maximum = Max;
            MPBar.Value = Value;
            MPLabel.Text = Value + "/" + Max;
        }
        private void AButton_Click(object sender, EventArgs e)
        {
            RunPlayerTurn(0);
        }
        private void KButton_Click(object sender, EventArgs e)
        {
            RunPlayerTurn(1);
        }
        private void SButton_Click(object sender, EventArgs e)
        {
            RunPlayerTurn(2);
        }
        private void E1Button_Click(object sender, EventArgs e)
        {
            RunPlayerTurn(3);
        }
        private void E2Button_Click(object sender, EventArgs e)
        {
            RunPlayerTurn(4);
        }
        private void E3Button_Click(object sender, EventArgs e)
        {
            RunPlayerTurn(5);
        }
        private void E4Button_Click(object sender, EventArgs e)
        {
            RunPlayerTurn(6);
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