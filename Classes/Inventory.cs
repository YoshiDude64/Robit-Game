using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Robit_Game.Properties;

namespace Robit_Game.Classes
{
    public class Inventory
    {
        public Battle Battle;
        public int MaxBP;
        public int BP;
        public List<Badge> Badges = new List<Badge>();
        public Badge[] BadgePrototypes;
        public int[] ItemIDs;
        public Item[] ItemPrototypes;
        Dictionary <int, string> CharacterNames = new Dictionary <int, string>();
        enum InvMode { Items, Abilities, Badges }
        public Inventory(Battle NewBattle)
        {
            Battle = NewBattle;
            MaxBP = 3;
            BP = MaxBP;
            InitializeBadges();
            CharacterNames.Add( -1, "X" );
            CharacterNames.Add( 0, "Abel" );
            CharacterNames.Add( 1, "K-813" );
            CharacterNames.Add( 2, "Sarge" );
        }
        public void InitializeBadges()
        {
            BadgePrototypes = new Badge[20];//Do not use these. This is just so that the lookup table has valid space for writing actual badges.
            BadgePrototypes[0] = new OPAmplifier(Battle, this);
            BadgePrototypes[1] = new DPAmplifier(Battle, this);
            BadgePrototypes[2] = new ThickenedPlating(Battle, this);
            BadgePrototypes[3] = new BackupBattery(Battle, this);
            BadgePrototypes[4] = new OPDiverter(Battle, this);
            BadgePrototypes[5] = new DPDiverter(Battle, this);
        }
        public void InitializeItems()
        {
            ItemPrototypes = new Item[10];
            ItemPrototypes[0] = new Item { ID = 0, Name = "AA Battery", Description = new string[] { "Stores a tiny amount of electricity.", "Restores 5 MP to the team." }, MPRestore = 5 };
            ItemPrototypes[1] = new Item { ID = 1, Name = "D Battery", Description = new string[] { "Stores a modest amount of electricity.", "Restores 10 MP to the team." }, MPRestore = 10 };
            ItemPrototypes[2] = new Item { ID = 2, Name = "Car Battery", Description = new string[] { "Stores a significant amount of electricity.", "Restores 20 MP to the team." }, MPRestore = 20 };
            ItemPrototypes[3] = new Item { ID = 3, Name = "Scrap Metal", Description = new string[] { "Can be used to repair a chassis in a pinch.", "Restores 3 HP to one machine." }, HPRestore = 3 };
            ItemPrototypes[4] = new Item { ID = 4, Name = "Mech Repair Kit", Description = new string[] { "This repair kit wasn't made for humanoid machines, but the parts high quality", "Restores 8 HP to one machine." }, HPRestore = 8 };
            ItemPrototypes[5] = new Item { ID = 5, Name = "Replacement Equipment", Description = new string[] { "Quality assured. These parts will have your machines running like new.", "Restores 15 HP to one machine." }, HPRestore = 15};
            ItemPrototypes[6] = new Item { ID = 6, Name = "Jumper Kit", Description = new string[] { "These cables can deliver enough watts to bring anything back to life.", "Revives a downed teammate, providing 5 HP in the process." }, HPRestore = 5, Revive = true };
            ItemPrototypes[7] = new Item { ID = 7, Name = "Plutonium Fuel Rod", Description = new string[] { "This seems really dangerous. Is it really worth the MP?", "Restores 99 MP to the team, damaging all members by 3HP in the process." }, HPRestore = -3, MPRestore = 99, TeamHeal = true };
            ItemPrototypes[8] = new Item { ID = 8, Name = "Nanomachine Swarm", Description = new string[] { "This fancy new technology can repair an army in a matter of seconds", "Restores 5 HP to all teammates" }, HPRestore = 5, TeamHeal = true };
            ItemPrototypes[9] = new Item { ID = 9, Name = "Debug Item", Description = new string[] { "How did you find this???", "Restores 99 HP and 99 MP to all team members. Revives downed team members." }, HPRestore = 99, MPRestore = 99, Revive = true, TeamHeal = true };
        }
        public string[] GetInventory(int type)
        {
            List <string> Entries = new List<string> { };
            switch (type)
            {
                case (int)InvMode.Items:
                    //add all item names to Entries
                    break;
                case (int)InvMode.Abilities:
                    //add all ability names to Abilities
                    break;
                case (int)InvMode.Badges:
                    //Badges.OrderBy(Badges => Badges.ID);
                    for (int x = 0; x < Badges.Count; x++)
                    {
                        Entries.Add($"{Badges[x].Name} - {Badges[x].BPCost} BP - {CharacterNames[Badges[x].EquippedToCharacter]}");
                    }
                    break;
            }
            return Entries.ToArray();
        }
    }
    public class Badge
    {
        public enum Characters { Any = -1, Abel = 0, K = 1, Sarge = 2};

        public int EquippedToCharacter = -1;
        public string Name = string.Empty;
        public string Description = string.Empty;
        public int BPCost;
        public Battle Battle;
        public Inventory Inventory;
        public int ID;
        public Badge(Battle NewBattle, Inventory BadgeInventory)
        {
            Battle = NewBattle;
            Inventory = BadgeInventory;
        }
        public void Equip(int Equipee)
        {
            if (EquippedToCharacter >= 0)
            {
                Unequip();
            }
            if(BPCost<=Inventory.BP)
            {
                Inventory.BP -= BPCost;
                EquippedToCharacter = Equipee;
                Equip2(Equipee);
            }
        }
        public virtual void Equip2(int Equipee)
        {
            //Should do nothing. If this ever runs, error!
        }
        public void Unequip()
        {
            Inventory.BP += BPCost;
            Unequip2(EquippedToCharacter);
            EquippedToCharacter = -1;
        }
        public virtual void Unequip2(int Equipee) 
        { 
            //Should do nothing. If this ever runs, error!
        }
    }
    public class Item
    {
        public int ID;
        public string Name;
        public string[] Description;
        public int HPRestore;
        public int MPRestore;
        public bool TeamHeal;
        public bool Revive;
        public int[] StatusEffects;
        public Item()
        {
            HPRestore = 0;
            MPRestore = 0;
            TeamHeal = false;
            Revive = false;
            StatusEffects = new int[0];
        }
    }
    public class OPAmplifier : Badge
    {
        public OPAmplifier(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Offensive Power Amplifier";
            Description = "Increases base attack by one for the equipped character.";
            BPCost = 6;
            ID = 0;
        }
        public override void Equip2(int Equipee)
        {   
            Battle.Combatants[EquippedToCharacter].Attack += 1;
        }
        public override void Unequip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].Attack -= 1;
        }
    }
    public class DPAmplifier : Badge
    {
        public DPAmplifier(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Defensive Power Amplifier";
            Description = "Increases base defense by one for the equipped character.";
            BPCost = 5;
            ID = 1;
        }
        public override void Equip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].Defense += 1;
        }
        public override void Unequip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].Defense -= 1;
        }
    }
    public class BackupBattery : Badge
    {
        public BackupBattery(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Backup Battery";
            Description = "Stores an additional 5MP at the cost of 3BP.";
            BPCost = 3;
            ID = 2;
        }
        public override void Equip2(int Equipee)
        {
            Battle.MaxMP += 5;
        }
        public override void Unequip2(int Equipee)
        {
            Battle.MaxMP -= 5;
            Battle.MP = Math.Min(Battle.MaxMP, Battle.MP);
        }
    }
    public class ThickenedPlating : Badge
    {
        public ThickenedPlating(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Thickened Plating";
            Description = "Increases the max HP of a robot by 4, at the cost of 2 BP.";
            BPCost = 2;
            ID = 3;
        }
        public override void Equip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].MaxHP += 4;
        }
        public override void Unequip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].MaxHP -= 4;
            Battle.Combatants[EquippedToCharacter].HP = Math.Min(Battle.Combatants[EquippedToCharacter].HP, Battle.Combatants[EquippedToCharacter].MaxHP);
        }
    }
    public class OPDiverter : Badge
    {
        public OPDiverter(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Offensive Power Diverter";
            Description = "Trade away one defense for one attack.";
            BPCost = 3;
            ID = 4;
        }
        public override void Equip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].Attack += 1;
            Battle.Combatants[EquippedToCharacter].Defense -= 1;
        }
        public override void Unequip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].Attack -= 1;
            Battle.Combatants[EquippedToCharacter].Defense += 1;
        }
    }
    public class DPDiverter : Badge
    {
        public DPDiverter(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Defensive Power Diverter";
            Description = "Trade away one attack for one defense.";
            BPCost = 2;
            ID = 5;
        }
        public override void Equip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].Attack -= 1;
            Battle.Combatants[EquippedToCharacter].Defense += 1;
        }
        public override void Unequip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].Attack += 1;
            Battle.Combatants[EquippedToCharacter].Defense -= 1;
        }
    }
}
