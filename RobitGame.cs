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

namespace Robit_Game
{
    public partial class RobitGame : Form
    {
        OverWorld World;
        Battle Battle;
        bool IsBattle;
        int LevelUp;
        int Turn;
        public RobitGame()
        {
            InitializeComponent();
            World = new OverWorld();
            Battle = new Battle();
            IsBattle = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AHPBar.SetState(3);
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
        private void button2_Click(object sender, EventArgs e)
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
                //Defend Function!
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
                    LevelUp = 0;
                }
                else
                {
                    Translate(0);
                }
            }
        }
        private void EastButton_Click(object sender, EventArgs e)
        {
            if (IsBattle)
            {
                //Attack!
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
                if (LevelUp == 1)//Upgrade and refil MP.
                {
                    Battle.MaxMP += 5;
                    Battle.MP = Battle.MaxMP;
                    LevelUp = 0;
                    UpdateButtons(World.FetchNeighbors());
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
                //Flee!
                Turn++;
                UpdateBattle();
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
        }
        private void button4_Click(object sender, EventArgs e)//WestButton
        {
            if (IsBattle)
            {
                //Ability!
                Turn++;
                UpdateBattle();
                if (!IsBattle && LevelUp != 1)
                {
                    UpdateButtons(World.FetchNeighbors());
                }
                if (Turn==3)
                {
                    RunEnemyTurns();
                }
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
        private void PrintStory(string[] phrases)
        {
            for(int x=phrases.Length-1;x>=0;x--)
            {
                LoreBox.Items.Insert(0, phrases[x]);
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
            HPs = Battle.FetchHPs();
            AlliesHP = 0;
            EnemiesHP = 0;
            for (x = 0; x < 3; x++)
            {
                AlliesHP += HPs[x];
            }
            if(AlliesHP <= 0)
            {
                Battle.GameOver();
                IsBattle = false;
            }
            for (x=3;x<7;x++)
            {
                EnemiesHP += HPs[x];
            }
            if(EnemiesHP <= 0)
            {
                PrintStory(Battle.EndBattle());
                IsBattle = false;
                Turn = 0;
                if (Battle.EXP > Battle.Combatants[0].Level)
                {
                    PrintStory(new string[] { "Level up!", "Pick a stat to improve." });
                    UpdateButtons(new string[] { "+2 Max HP each", "+5 Max MP", " ", "+3 Max BP" });
                    LevelUp = 1;
                    for(int y = 0;y<3;y++)
                    {
                        Battle.Combatants[y].HP = Battle.Combatants[y].MaxHP;
                        Battle.MP = Battle.MaxMP;
                    }
                }
            }
        }
        private void RunEnemyTurns()
        {

        }
        private void UpdateHPBars(int[] MaxHPs, int[] HPs)
        {
            int[] BarStates = new int[7];
            for (int x=0;x<7;x++)
            {
                if(HPs[x] * 10 / MaxHPs[x]<=2)
                {
                    BarStates[x] = 2;
                }
                else if (HPs[x] * 10 / MaxHPs[x]>=5)
                {
                    BarStates[x] = 1;
                }
                else
                {
                    BarStates[x] = 3;
                }
            }
            AHPBar.Maximum = MaxHPs[0];
            AHPBar.Value = HPs[0];
            AHPBar.SetState(BarStates[0]);
            KHPBar.Maximum = MaxHPs[1];
            KHPBar.Value = HPs[1];
            KHPBar.SetState(BarStates[1]);
            SHPBar.Maximum = MaxHPs[2];
            SHPBar.Value = HPs[2];
            SHPBar.SetState(BarStates[2]);
            E1HPBar.Maximum = MaxHPs[3];
            E1HPBar.Value = HPs[3];
            E1HPBar.SetState(BarStates[3]);
            E2HPBar.Maximum = MaxHPs[4];
            E2HPBar.Value = HPs[4];
            E2HPBar.SetState(BarStates[4]);
            E3HPBar.Maximum = MaxHPs[5];
            E3HPBar.Value = HPs[5];
            E3HPBar.SetState(BarStates[5]);
            E4HPBar.Maximum = MaxHPs[6];
            E4HPBar.Value = HPs[6];
            E4HPBar.SetState(BarStates[6]);
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

        }
        private void KButton_Click(object sender, EventArgs e)
        {

        }
        private void SButton_Click(object sender, EventArgs e)
        {

        }
        private void E1Button_Click(object sender, EventArgs e)
        {

        }
        private void E2Button_Click(object sender, EventArgs e)
        {

        }
        private void E3Button_Click(object sender, EventArgs e)
        {

        }
        private void E4Button_Click(object sender, EventArgs e)
        {

        }
        private void AHPBar_Click(object sender, EventArgs e)
        {

        }
        private void progressBar4_Click(object sender, EventArgs e)
        {

        }//KHPBar
        private void SHPBar_Click(object sender, EventArgs e)
        {

        }
        private void progressBar5_Click(object sender, EventArgs e)
        {

        }//E1HPBar
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
public static class ModifyProgressBarColor//Borrowed from a stack overflow user (There is no built-in way to change progressbar color without ruining the entire form's style) https://stackoverflow.com/questions/778678/how-to-change-the-color-of-progressbar-in-c-sharp-net-3-5
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    public static void SetState(this System.Windows.Forms.ProgressBar pBar, int state)
    {
        SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
    }
}