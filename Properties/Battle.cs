using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                Combatants[x].Tattled = true;
            }//Initialize the 3 player characters.
            for (int x=3;x<7;x++)
            {
                Combatants[x] = RobitPrototypes[3];
            }//Initialize the 4 enemy slots to the empty robit prototype. This will prevent memory errors later on.
            MaxMP = 10;
            MP = 10;
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
        public int[] FetchRealHPs()
        {
            int[] HPs = new int[7];
            for (int x = 0; x < 7; x++)
            {
                HPs[x] = Combatants[x].HP;
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
            int Damage = Combatants[caster].Attack + Combatants[caster].Charge;//Tinker with this line for badges
            int start, stop;
            string TargetTeam;
            Random RNG = new Random();

            if (target >= 3)//Sets up group attack iterators for attacks against enemies.
            {
                TargetTeam = "enemies";
                start = 3;
                stop = 7;
            }
            else//Sets up group attack iterators for attacks against allies.
            {
                TargetTeam = "allies";
                start = 0;
                stop = 3;
            }
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
                    Log[0] = Combatants[caster].Name + " hits " + Combatants[target].Name + " for " + Damage + " Damage.";
                    break;
                case 2://Tattle
                    Log = Tattle(caster, target);
                    break;
                case 3://Reconstruct
                    int Healed = Combatants[target].HP - Math.Min(Combatants[target].HP + 10, Combatants[target].MaxHP);
                    Combatants[target].HP += Healed;
                    Log[0] = Combatants[caster].Name + " healed " + Combatants[target].Name + " for " + Healed + " HP.";
                    break;
                case 4://Heat wave (Group burn) (penetrates normal defense, but a defensive stance will still reduce damage taken by 1)
                    Log[0] = Combatants[caster].Name + " uses \"Heat Wave\" on all " + TargetTeam + "!";
                    for (int x = start; x < stop; x++)
                    {
                        Damage = Damage - Convert.ToInt32(Combatants[target].Defending);
                        Damage = Math.Max(Damage, 0);//Prevents defending opponents from healing 1 HP.
                        Combatants[x].HP -= Damage;//May produce negative values. This issue is solved outside the switch.
                        Log.Append(Combatants[x].Name + " was burned for " + Damage + " damage.");
                    }
                    break;
                case 5://Torch (Single burn) (Penetrates normal defense, but defensive stance will still reduce damage taken by 1)
                    Damage = Damage - Convert.ToInt32(Combatants[target].Defending);
                    Damage = Math.Max(Damage, 0);
                    Combatants[target].HP -= Damage;
                    Log[0] = Combatants[caster].Name + " torches " + Combatants[target].Name + " for " + Damage + " damage.";
                    break;
                case 6://Magneto (Group. Ignores defense, but deals zero damage , only used by Frix.)
                    Log[0] = Combatants[caster].Name + " activates the electromagnet.";
                    for(int x = start; x < stop; x++)
                    {
                        if (Combatants[x].Defending)
                        {
                            Log.Append(Combatants[x].Name + " evaded the magnet!");
                        }
                        else
                        {
                            Combatants[x].HP -= Damage;
                            Log.Append(Combatants[x].Name + " was magnetized for " + Damage);
                        }
                    }
                    break;
                case 7://charge. Makes attacks deal +1 damage. Max of 3 charge.
                    Combatants[caster].Charge += 1;
                    if (Combatants[caster].Charge>3)
                    {
                        Combatants[caster].Charge = 3;//If overcharged, set back to 3.
                    }
                    Log[0] = Combatants[caster].Name + " charged up. (Total charge: " + Combatants[caster].Charge + ")";
                    break;
                case 8://Blazing Combo
                    Log[0] = Combatants[caster].Name + " begins a BLAZING COMBO!";
                    if(Damage<1)//Does zero damage total if first hit would deal less than one damage without the max function.
                    {
                        break;
                    }
                    for(int x=0;x<3;x++)//Strikes 3 times. If attack is 3, then deal {3, 2, 1} = 6 damage. If attack is 1, deal {1, 1, 1) = 3 damage. Basically, it's Vi's hurricaine toss.
                    {
                        Damage = Math.Max(Damage, 1);//deal at least one damage per hit.
                        Log.Append("Hit " + x + ": " + Damage + " Damage!");
                        Combatants[target].HP -= Damage;
                        Damage--;
                    }
                    break;
                case 9://Lightning Combo (Weaker than blazing combo for attack 1 and 2. Equal in power at 3 attack. Stronger with 4+ attack.)
                    Log[0] = Combatants[caster].Name + " begins a LIGHTNING COMBO!";
                    for(int x=0;x<4;x++)//Strikes 4 times. If attack is 3, then deal {3, 2, 1, 0} = 6 damage. If attack is 1, deal {1, 0, 0, 0} = 1 damage.
                    {
                        Damage = Math.Max(Damage, 0);//never heal on hit.
                        Log.Append("Hit " + x + ": " + Damage + " Damage!");
                        Combatants[target].HP -= Damage;
                        Damage--;
                    }
                    break;
                case 10://hard charge. Costs 5 HP to get 3 charge (ouch!)
                    Log[0] = Combatants[caster].Name + " hard charges! (Charge = 3)";
                    Combatants[caster].HP -= 5;
                    Combatants[caster].Charge += 3;
                    break;
                case 11://summon. Enemy needs unique data for a summon.
                    Log[0] = Combatants[caster].Name + " calls for help!";
                    if(RNG.Next(1, 100)>30)//30% change of summoning ally.
                    {
                        for(int x=3;x<7;x++)
                        {
                            if (Combatants[x].HP == 0)
                            {
                                Combatants[x] = RobitPrototypes[Combatants[caster].Summon];
                                Log.Append(Combatants[x].Name + "appeared!");
                                break;
                            }
                        }
                    }
                    else
                    {
                        Log.Append("...But nobody came");
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
            if(move!=7&&move!=10)//Resets charge after an attack, unless charge was used. Charge can build up infinitely, but takes one turn each time.
            {
                Log.Append(Combatants[caster].Name + "discharged!");
                Combatants[caster].Charge = 0;
            }
            return Log;
        }
        public string[] Flee()
        {
            string[] log = new string[] { "Escaped!" };
            for(int x=3;x<7;x++)
            {
                Combatants[x] = RobitPrototypes[3];//sets all enemies to empty enemies.
            }
            return log;
        }
        public string[] BeginBattle(int[] EnemyIDs)
        {
            string[] Log = new string[] { "You've encountered a team of enemies!"};
            for(int x=0;x<4;x++)
            {
                Combatants[x + 3] = RobitPrototypes[EnemyIDs[x]];
            }
            return Log;
        }
        public string[] EndBattle()
        {
            string[] log = new string[] { "We did it! All enemies defeated." };
            int CalculatedEXP = 0;
            for (int x = 3; x < 7; x++)
            {
                CalculatedEXP += Math.Max(Math.Min(Combatants[x].Level - Combatants[0].Level, 25), 0);//The Max function prevents the player from loosing EXP from fighting weak enemies. The Min function keeps the player from gaining more than 100 EXP in one fight, preventing the player from leveling up twice from the same fight.
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
            RobitPrototypes[0]  = new Robit { Name = "Abel",              MaxHP = 8,   Attack = 2, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 1, },         TattleLog = new string[] { "He may be the last human alive. So facinating!" } };
            RobitPrototypes[1]  = new Robit { Name = "K-813",             MaxHP = 6,   Attack = 2, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 1, 2, 3 },    TattleLog = new string[] { "The data may be a little biased in my favor."} };
            RobitPrototypes[2]  = new Robit { Name = "Sarge",             MaxHP = 10,  Attack = 2, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0, 1, },         TattleLog = new string[] { "He used to be a great leader in the Emperial Army, but he retired long ago. He's rusty, but he's not rusty!"} };
            RobitPrototypes[3]  = new Robit { Name = " ",                 MaxHP = 0,   Attack = 0, Defense = 0, Level = 0,  ValidAttacks = new int[] { 0,},             TattleLog = new string[] { "" } };
            RobitPrototypes[4]  = new Robit { Name = "Capacitron",        MaxHP = 4,   Attack = 1, Defense = 0, Level = 7,  ValidAttacks = new int[] { 1, 7, 10 },      TattleLog = new string[] { "They would be cute if they weren't so hazardous.", "Try to put them out of their misery before they blow." } };
            RobitPrototypes[5]  = new Robit { Name = "Snooper",           MaxHP = 4,   Attack = 1, Defense = 0, Level = 7,  ValidAttacks = new int[] { 0, 11 },         TattleLog = new string[] { "These gentle drones scan the earth for viable parts.", "When they find something they like, they call their masters, the scrap bandits.", "I feel bad fighting them, but if they stay, we'll be in a world of pain."} };
            RobitPrototypes[6]  = new Robit { Name = "Multibeater",       MaxHP = 7,   Attack = 2, Defense = 0, Level = 9,  ValidAttacks = new int[] { 1, 7, 9 },       TattleLog = new string[] { "The swiss army knife of the wastes.", "Originally intended as handymen, their adaptive nature made them highly unstable.", "Eventually all of these poor things were relegated to the wastes to prevent any further harm."} };
            RobitPrototypes[7]  = new Robit { Name = "Pitcher Mech",      MaxHP = 7,   Attack = 2, Defense = 0, Level = 9,  ValidAttacks = new int[] { 1, 8, 9 },       TattleLog = new string[] { "I really don't like these guys.", "Do I really have to talk about them?", "Fine. They're pitching machines on legs. They throw rocks", "Happy now?"} };
            RobitPrototypes[8]  = new Robit { Name = "Scrap Bandit",      MaxHP = 10,  Attack = 2, Defense = 1, Level = 11, ValidAttacks = new int[] { 1, 8, 9 },       TattleLog = new string[] { "Ew! I've heard about these creeps.", "They like to kidnap robots who wander too far from town and sell them on the black market.", "Sometimes they break them apart just for the parts...", "We need to stop them before they hurt anybody else!"} };
            RobitPrototypes[9]  = new Robit { Name = "Surveilance Drone", MaxHP = 8,   Attack = 1, Defense = 0, Level = 13, ValidAttacks = new int[] { 0, 11 },         TattleLog = new string[] { "It's another drone! This one looks looks really sturdy.", "They're basically armored snoopers. Take them out before they alert their friends."} };
            RobitPrototypes[10] = new Robit { Name = "Toastee",           MaxHP = 8,   Attack = 2, Defense = 0, Level = 13, ValidAttacks = new int[] { 1, 5 },          TattleLog = new string[] { "Aww! It's a little toaster.", "Wait, it doesn't look very friendly. Look out for its heating coil!"} };
            RobitPrototypes[11] = new Robit { Name = "Polesaw Knight",    MaxHP = 14,  Attack = 2, Defense = 2, Level = 15, ValidAttacks = new int[] { 1, 7, 8 },       TattleLog = new string[] { "These knights are the backbone of the Forge's border patrol.", "They're pretty well armored. Maybe we should use special attacks on them?"} };
            RobitPrototypes[12] = new Robit { Name = "Scorch Trooper",    MaxHP = 10,  Attack = 2, Defense = 0, Level = 15, ValidAttacks = new int[] { 1, 4, 5, 8 },    TattleLog = new string[] { "These guys melt minerals and intruders alike", "Let's make sure we're using fire resistance badges."} };
            RobitPrototypes[13] = new Robit { Name = "Hacksmith",         MaxHP = 15,  Attack = 2, Defense = 0, Level = 17, ValidAttacks = new int[] { 3, 4, 5, 7 },    TattleLog = new string[] { "Hacksmiths can be difficult to deal with.", "They have access to a day-zero database, so they always find a way to debuff you", "Try to get rid of all the hacksmiths on the battlefield before you worry about the others."} };
            RobitPrototypes[14] = new Robit { Name = "Crane Mech",        MaxHP = 30,  Attack = 2, Defense = 0, Level = 20, ValidAttacks = new int[] { 1, 6, 9 },       TattleLog = new string[] { "I never thought a magnet crane could be used as a weapon!", "When Frix charges the magnet, hold a defensive stance.", "If you let him pull you in, you could get stunned."} };
            RobitPrototypes[15] = new Robit { Name = "Sir Volk",          MaxHP = 50,  Attack = 3, Defense = 0, Level = 30, ValidAttacks = new int[] { 4, 5, 8},        TattleLog = new string[] { "Sir Volk is Smith's son and right hand.", "A jetpack, a sword, a flamethrower... What weapons does he not have?", "All of these tools give him a ton of attacking options.", "I hope we came well prepared."} };
            for(int x=0;x<15;x++)
            {
                RobitPrototypes[x].Tattled = false;
                RobitPrototypes[x].Defending = false;
                RobitPrototypes[x].ID = x;
                RobitPrototypes[x].HP = RobitPrototypes[x].MaxHP;
                RobitPrototypes[x].Charge = 0;
                RobitPrototypes[x].Summon = 3;
            }
            RobitPrototypes[5].Summon = 8;
            RobitPrototypes[9].Summon = 11;
            RobitPrototypes[3].Tattled = true;
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
        public int Charge;
        public int Summon;
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
