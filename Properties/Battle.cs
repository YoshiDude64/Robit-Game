using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robit_Game.Properties
{
    public class Battle
    {
        public Robit[] Combatants;
        Robit[] RobitPrototypes;
        public int MP;
        public int MaxMP;
        public int EXP;
        public Battle()
        {
            Combatants = new Robit[7];
            RobitPrototypes = new Robit[17];
            EXP = 0;
            InitializePrototypes();
            for (int x=0;x<3;x++)
            {
                Combatants[x] = RobitPrototypes[x];
            }//Initialize the 3 player characters.
            for (int x=3;x<7;x++)
            {
                Combatants[x] = RobitPrototypes[3];
            }//Initialize the 4 enemy slots to the empty robit prototype. This will prevent memory errors later on.
        }
        public string[] UpdateButtons()//returns the names of all buttons when a battle starts.
        {
            string[] NewButtons = new string[4];
            NewButtons[0] = "Defend";
            NewButtons[1] = "Attack";
            NewButtons[2] = "Flee";
            NewButtons[3] = "Ability";
            return NewButtons;
        }
        public int[] FetchMaxHPs()//returns the max HPs of all entities on the field.
        {
            int[] MaxHPs = new int[7];
            for(int x=0;x<7;x++)
            {
                if (Combatants[x].Tattled)
                {
                    MaxHPs[x] = Combatants[x].MaxHP;
                }
                else
                {
                    MaxHPs[x] = 1;
                }
            }
            return MaxHPs;
        }
        public int[] FetchHPs()//returns the HPs of all entities on the field.
        {
            int[] HPs= new int[7];
            for(int x=0;x<7;x++)
            {
                if (Combatants[x].Tattled)
                {
                    HPs[x] = Combatants[x].HP;
                }
                else
                {
                    HPs[x] = 1;
                }
            }
            return HPs;
        }
        public string[] FetchNames()//returns the names of all entities on the field.
        {
            string[] Names = new string[7];
            for(int x=0;x<7;x++)
            {
                if (Combatants[x].HP <= 0)
                {
                    Names[x] = " ";
                }
                else
                {
                    if (Combatants[x].Tattled)
                    {
                        Names[x] = Combatants[x].Name + " (" + Combatants[x].HP + "/" + Combatants[x].MaxHP + ")";
                    }
                    else
                    {
                        Names[x] = Combatants[x].Name + " (??/??)";
                    }
                }
            }
            return Names;
        }
        public string[] RunTurn(int move, int caster, int target)//returns strings to be printed to the LoreBox.
        {
            string[] Log = new string[] { "" };//temporary assignment so the log is at least one string long.
            int Damage = Combatants[caster].Attack;//Tinker with this line for badges
            switch (move)
            {
                case 0://Defend
                    Combatants[caster].Defending = true;
                    Log[0] = Combatants[caster].Name + " defends.";
                    break;
                case 1://Basic attack
                    Damage = Damage - Combatants[target].Defense - Convert.ToInt32(Combatants[target].Defending);
                    Damage = Math.Max(Damage, 0);//keeps enemies with high defense from being healed by attacks.
                    Combatants[target].HP -= Damage;
                    Log[0] = Combatants[caster].Name + " hits " + Combatants[target].Name + " for " + Damage + "Damage.";
                    break;
                case 2://Tattle
                    Log = Tattle(caster, target);
                    break;
                case 3://Reconstruct
                    int Healed = Combatants[target].HP - Math.Min(Combatants[target].HP + 10, Combatants[target].MaxHP);
                    Combatants[target].HP += Healed;
                    Log[0] = Combatants[caster].Name + " healed " + Combatants[target].Name + " for " + Healed + " HP.";
                    break;
                case 4://Group burn (penetrates defense)
                    int[] Damages = new int[4];
                    if(target>=3)
                    {

                    }
                    else
                    {

                    }

                    break;
            }
            for (int x = 0; x < 7; x++)
            {
                if (Combatants[x].HP < 0)
                {
                    Combatants[x].HP = 0;//Keeps enemies and allies from having negative HP, which may cause errors.
                }
            }
            return Log;
        }
        void Flee()
        {

        }
        public string[] EndBattle()
        {
            string[] log = new string[] { "We did it! All enemies defeated." };
            int CalculatedEXP = 0;
            for (int x = 3; x < 7; x++)
            {
                CalculatedEXP += Math.Max(Math.Min(Combatants[x].Level + Combatants[0].Level, 25), 0);//The Max function prevents the player from loosing EXP from fighting weak enemies. The Min function keeps the player from gaining more than 100 EXP in one fight, preventing the player from leveling up twice from the same fight.
            }
            EXP += Math.Max(CalculatedEXP, 1);//Fights will always yield at least 1 EXP.
            return log;
        }
        public void GameOver()
        {

        }
        public string[] Tattle(int caster, int target)//returns text for the LoreBox to write, marks enemies as tattled. Tattled enemies display thier max HP and HP.
        {
            for (int x = 3; x < 7; x++)
            {
                if (Combatants[x].ID == Combatants[target].ID)
                {
                    Combatants[x].Tattled = true;//Marks all 4 potential enemies on the battlefield of the same type as tattled
                }
            }
            RobitPrototypes[target].Tattled = true;//Marks the enemy prototype as tattled, so that when they are encountered again, they do not need to be tattled again.
            string[] Tattle = new string[] { Combatants[caster].Name + ": \"That's "  + Combatants[target].Name + ". Max HP is " + Combatants[target].MaxHP + ", Attack is " + Combatants[target].Attack + ", Defense is " + Combatants[target].Defense + ".\"" };
            for (int x = 0; x < Combatants[target].TattleLog.Length; x++)
            {
                Tattle.Append(Combatants[caster].Name + ": \"" + Combatants[target].TattleLog[x] + "\"");
            }
            return Tattle;
        }
        void InitializePrototypes()
        {
            RobitPrototypes[0]  = new Robit { Name = "Abel",              MaxHP = 8,   Attack = 2, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "He may be the last human alive. So facinating!" } };
            RobitPrototypes[1]  = new Robit { Name = "K-813",             MaxHP = 6,   Attack = 2, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "The data may be a little biased in my favor."} };
            RobitPrototypes[2]  = new Robit { Name = "Sarge",             MaxHP = 10,  Attack = 2, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "He used to be a great leader in the Emperial Army, but he retired long ago. He's rusty, but he's not rusty!"} };
            RobitPrototypes[3]  = new Robit { Name = " ",                 MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "" } };
            RobitPrototypes[4]  = new Robit { Name = "Capacitron",        MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "They would be cute if they weren't so hazardous.", "Try to put them out of their misery before they blow." } };
            RobitPrototypes[5]  = new Robit { Name = "Snooper",           MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "These gentle drones scan the earth for viable parts.", "When they find something they like, they call their masters, the scrap bandits.", "I feel bad fighting them, but if they stay, we'll be in a world of pain."} };
            RobitPrototypes[6]  = new Robit { Name = "Multibeater",       MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "The swiss army knife of the wastes.", "Originally intended as handymen, their adaptive nature made them highly unstable.", "Eventually all of these poor things were relegated to the wastes to prevent any further harm."} };
            RobitPrototypes[7]  = new Robit { Name = "Pitcher Mech",      MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "I really don't like these guys.", "Do I really have to talk about them?", "Fine. They're pitching machines on legs. They throw rocks", "Happy now?"} };
            RobitPrototypes[8]  = new Robit { Name = "Scrap Bandit",      MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "Ew! I've heard about these creeps.", "They like to kidnap robots who wander too far from town and sell them on the black market.", "Sometimes they break them apart just for the parts...", "We need to stop them before they hurt anybody else!"} };
            RobitPrototypes[9]  = new Robit { Name = "Surveilance Drone", MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "It's another drone! This one looks looks really sturdy.", "They're basically armored snoopers. Take them out before they alert their friends."} };
            RobitPrototypes[10] = new Robit { Name = "Toastee",           MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "Aww! It's a little toaster.", "Wait, it doesn't look very friendly. Look out for its heating coil!"} };
            RobitPrototypes[11] = new Robit { Name = "Polesaw Knight",    MaxHP = 0,   Attack = 0, Defense = 2, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "These knights are the backbone of the Forge's border patrol.", "They're pretty well armored. Maybe we should use special attacks on them?"} };
            RobitPrototypes[12] = new Robit { Name = "Scorch Trooper",    MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "These guys melt minerals and intruders alike", "Let's make sure we're using fire resistance badges."} };
            RobitPrototypes[13] = new Robit { Name = "Hacksmith",         MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "Hacksmiths can be difficult to deal with.", "They have access to a day-zero database, so they always find a way to debuff you", "Try to get rid of all the hacksmiths on the battlefield before you worry about the others."} };
            RobitPrototypes[14] = new Robit { Name = "Crane Mech",        MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "I never thought a magnet crane could be used as a weapon!", "When Frix charges the magnet, hold a defensive stance.", "If you let him pull you in, you could get stunned."} };
            RobitPrototypes[15] = new Robit { Name = "Sir Volk",          MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, TattleLog = new string[] { "Sir Volk is Smith's son and right hand.", "A jetpack, a sword, a flamethrower... What weapons does he not have?", "All of these tools give him a ton of attacking options.", "I hope we came well prepared."} };
            for(int x=0;x<15;x++)
            {
                RobitPrototypes[x].Tattled = false;
                RobitPrototypes[x].Defending = false;
                RobitPrototypes[x].ID = x;
                RobitPrototypes[x].HP = RobitPrototypes[x].MaxHP;
            }
        }
    }
    public class Robit
    {
        public int HP;
        public int MaxHP;
        public int Attack;
        public int Defense;
        public int[] ValidAttacks;
        public int Level;
        public string Name;
        public string[] TattleLog;
        public bool Tattled;
        public int ID;
        public bool Defending;
        public Robit()
        {
            ValidAttacks = new int[10];
            for(int x=0;x<10;x++)
            {
                ValidAttacks[x] = 0;
            }
        }
    }
}
