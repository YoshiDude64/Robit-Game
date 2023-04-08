using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public List<int> ItemIDs = new List<int>();
        public Item[] ItemPrototypes;
        public Ability[] Abilities;
        readonly Dictionary <int, string> CharacterNames = new Dictionary <int, string>();
        public string LastItemUsed;
        int InvType = 0;
        enum InvMode { Items, Abilities, Badges };
        public enum BadgeEffects { Buggernaut, MachineDatabase, Nanomachines, AdaptiveControlUnit, MatterAnnihilator, EmergencyOffenseReserve, EmergencyDefenseReserve, InductionCoil, MinimalistArchetecture, CapacitorSize, ItemAutoLoader, ParryDatabase };
        public Inventory(Battle NewBattle)
        {
            Battle = NewBattle;
            MaxBP = 6;
            BP = MaxBP;
            InitializeBadges();
            InitializeItems();
            InitializeAbilities();
            CharacterNames.Add( -1, "X" );
            CharacterNames.Add( 0, "Abel" );
            CharacterNames.Add( 1, "K-813" );
            CharacterNames.Add( 2, "Sarge" );
            CharacterNames.Add( 3, "All");
        }
        public void InitializeAbilities()
        {
            Abilities = new Ability[12];
            Abilities[0]  = new Ability { Name = "Defend",          MPCost = 0, Description = new string[] { "Grants the defending status effect.", "Reduces all damage taken by 1, entirely nullifies certain attacks." } };
            Abilities[1]  = new Ability { Name = "Attack",          MPCost = 0, Description = new string[] { "Basic attack.", "Deals damage once to the selected target." } };
            Abilities[2]  = new Ability { Name = "Tattle",          MPCost = 0, Description = new string[] { "Permanently reveals the health of the targeted robot type.", "The robot's stats are also revealed." } };
            Abilities[3]  = new Ability { Name = "Reconstruct",     MPCost = 8, Description = new string[] { "Restores 10 HP to the selected robot." } };
            Abilities[4]  = new Ability { Name = "Heat Wave",       MPCost = 6, Description = new string[] { "Burns all targets of the selected team for the caster's base damage.", "Ignores all defense except for the defending status effect." } };
            Abilities[5]  = new Ability { Name = "Torch",           MPCost = 2, Description = new string[] { "Burns a single target for the caster's base damage.", "Ignores all defense except for the defending status effect." } };
            Abilities[6]  = new Ability { Name = "Magneto",         MPCost = 4, Description = new string[] { "Magnetizes all robots of the selected team for the caster's base damage.", "Has no effect on defending targets." } };
            Abilities[7]  = new Ability { Name = "Charge",          MPCost = 1, Description = new string[] { "Temporarily increases the caster's attack.", "Can stack up to 3 times." } };
            Abilities[8]  = new Ability { Name = "Blazing Combo",   MPCost = 3, Description = new string[] { "Attacks 3 times, with each hit dealing one less damage.", "Approaches a minimum of one damage." } };
            Abilities[9]  = new Ability { Name = "Lightning Combo", MPCost = 3, Description = new string[] { "Attacks 4 times, with each hit dealing one less damage.", "Approaches a minimum of zero damage." } };
            Abilities[10] = new Ability { Name = "Hard Charge",     MPCost = 0, Description = new string[] { "Maximizes the charge of the caster at the cost of 5 HP." } };
            Abilities[11] = new Ability { Name = "Summon",          MPCost = 0, Description = new string[] { "Has a 50% change to summon an enemy.", "You shouldn't have access to this." } };
        }
        public void InitializeBadges()
        {
            BadgePrototypes = new Badge[20];
            BadgePrototypes[0] = new OPAmplifier(Battle, this);
            BadgePrototypes[1] = new DPAmplifier(Battle, this);
            BadgePrototypes[2] = new ThickenedPlating(Battle, this);
            BadgePrototypes[3] = new BackupBattery(Battle, this);
            BadgePrototypes[4] = new OPDiverter(Battle, this);
            BadgePrototypes[5] = new DPDiverter(Battle, this);
            BadgePrototypes[6] = new Buggernaut(Battle, this);
            BadgePrototypes[7] = new MachineDatabase(Battle, this);
            BadgePrototypes[8] = new Nanomachines(Battle, this);
            BadgePrototypes[9] = new FissionReactor(Battle, this);
            BadgePrototypes[10] = new AdaptiveControlUnit(Battle, this);
            BadgePrototypes[11] = new MatterAnnihilator(Battle, this);
            BadgePrototypes[12] = new EmergencyOffenceReserve(Battle, this);
            BadgePrototypes[13] = new EmergencyDefenseReserve(Battle, this);
            BadgePrototypes[14] = new InductionCoil(Battle, this);
            BadgePrototypes[15] = new MinimalistArchetecture(Battle, this);
            BadgePrototypes[16] = new LargeCapacitor(Battle, this);
            BadgePrototypes[17] = new SmallCapacitor(Battle, this);
            BadgePrototypes[18] = new DebugBadge(Battle, this);
            BadgePrototypes[19] = new ParryDatabase(Battle, this);
        }
        public void InitializeItems()
        {
            ItemPrototypes = new Item[10];
            ItemPrototypes[0] = new Item { ID = 0, Name = "AA Battery", Description = new string[] { "Stores a tiny amount of electricity.", "Restores 5 MP to the team." }, MPRestore = 5, TeamHeal = true };
            ItemPrototypes[1] = new Item { ID = 1, Name = "D Battery", Description = new string[] { "Stores a modest amount of electricity.", "Restores 10 MP to the team." }, MPRestore = 10, TeamHeal = true };
            ItemPrototypes[2] = new Item { ID = 2, Name = "Car Battery", Description = new string[] { "Stores a significant amount of electricity.", "Restores 20 MP to the team." }, MPRestore = 20, TeamHeal = true };
            ItemPrototypes[3] = new Item { ID = 3, Name = "Scrap Metal", Description = new string[] { "Can be used to repair a chassis in a pinch.", "Restores 3 HP to one machine." }, HPRestore = 3 };
            ItemPrototypes[4] = new Item { ID = 4, Name = "Mech Repair Kit", Description = new string[] { "This repair kit wasn't made for humanoid machines, but the parts are of decent quality", "Restores 8 HP to one machine." }, HPRestore = 8 };
            ItemPrototypes[5] = new Item { ID = 5, Name = "Replacement Equipment", Description = new string[] { "Quality assured. These parts will have your machines running like new.", "Restores 15 HP to one machine." }, HPRestore = 15};
            ItemPrototypes[6] = new Item { ID = 6, Name = "Jumper Kit", Description = new string[] { "These cables can deliver enough watts to bring anything back to life.", "Revives a downed teammate, providing 5 HP in the process." }, HPRestore = 5, Revive = true };
            ItemPrototypes[7] = new Item { ID = 7, Name = "Plutonium Fuel Rod", Description = new string[] { "This seems really dangerous. Is it really worth the MP?", "Restores 99 MP to the team, damaging all members by 3HP in the process." }, HPRestore = -3, MPRestore = 99, TeamHeal = true };
            ItemPrototypes[8] = new Item { ID = 8, Name = "Nanomachine Swarm", Description = new string[] { "This fancy new technology can repair an army in a matter of seconds", "Restores 5 HP to all teammates" }, HPRestore = 5, TeamHeal = true };
            ItemPrototypes[9] = new Item { ID = 9, Name = "Debug Item", Description = new string[] { "How did you find this???", "Restores 99 HP and 99 MP to all team members. Revives downed team members." }, HPRestore = 99, MPRestore = 99, Revive = true, TeamHeal = true };
        }
        public string[] GetInventory(int type, int Turn)
        {
            List <string> Entries = new List<string> { };
            InvType = type;
            switch (type)
            {
                case (int)InvMode.Items:
                    ItemIDs.Sort();
                    for (int x = 0; x < ItemIDs.Count; x++)
                    {
                        Entries.Add($"{ItemPrototypes[ItemIDs[x]].Name}");
                    }
                    break;
                case (int)InvMode.Abilities:
                    Array.Sort(Battle.Combatants[Turn].ValidAttacks);
                    if (Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.MatterAnnihilator] < 1)
                    {
                        for (int x = 0; x < Battle.Combatants[Turn].ValidAttacks.Length; x++)
                        {
                            Entries.Add($"{Abilities[Battle.Combatants[Turn].ValidAttacks[x]].Name} - {Abilities[Battle.Combatants[Turn].ValidAttacks[x]].MPCost + Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.CapacitorSize]} MP");//FIX LATER
                        }
                    }
                    else
                    {
                        for (int x = 0; x < Battle.Combatants[Turn].ValidAttacks.Length; x++)
                        {
                            Entries.Add($"{Abilities[Battle.Combatants[Turn].ValidAttacks[x]].Name} - {Abilities[Battle.Combatants[Turn].ValidAttacks[x]].MPCost + Battle.Combatants[Turn].BadgeEffects[(int)BadgeEffects.CapacitorSize]} HP");//FIX LATER
                        }
                    }
                    break;
                case (int)InvMode.Badges:
                    Badges = Badges.OrderBy(Badges => Badges.Name).ToList();
                    for (int x = 0; x < Badges.Count; x++)
                    {
                        Entries.Add($"{Badges[x].Name} - {Badges[x].BPCost} BP - {CharacterNames[Badges[x].EquippedToCharacter]}");
                    }
                    break;
            }
            return Entries.ToArray();
        }
        public string[] PrintDescription(int ListIndex, int Turn)
        {
            List<string> Log = new List<string>();
            switch (InvType)
            {
                case (int)InvMode.Items:
                    Log = ItemPrototypes[ItemIDs[ListIndex]].Description.ToList();
                    break;
                case (int)InvMode.Abilities:
                    Log = Abilities[Battle.Combatants[Turn].ValidAttacks[ListIndex]].Description.ToList();
                    break;
                case (int)InvMode.Badges:
                    Log = Badges[ListIndex].Description.ToList();
                    Log.Add($"BP: {BP}/{MaxBP}");
                    break;
            }
            return Log.ToArray();
        }
        public bool UseItem(int ListIndex, int Type, int Character)
        {
            switch (Type)
            {
                case (int)InvMode.Items:
                    ConsumeItem(ItemIDs[ListIndex], Character);
                    LastItemUsed = ItemPrototypes[ItemIDs[ListIndex]].Name;
                    ItemIDs.RemoveAt(ListIndex);
                    return true;
                case (int)InvMode.Abilities:
                    break;
                case (int)InvMode.Badges:
                    if(Badges[ListIndex].EquippedToCharacter>=0)
                    {
                        
                        Badges[ListIndex].Unequip();
                        return true;
                    }
                    else
                    {
                        return Badges[ListIndex].Equip(Character);
                    }
            }
            return false;//Error!
        }
        public void ConsumeItem(int ItemID, int Character)
        {
            int TeamStart, TeamEnd;
            if (ItemPrototypes[ItemID].TeamHeal)//Affecting entire team?
            {
                if (Character <= 2)//Using on allies
                {
                    TeamStart = 0;
                    TeamEnd = 3;
                }
                else//using on enemies
                {
                    TeamStart = 3;
                    TeamEnd = 7;
                }
            }
            else//using on single character
            {
                TeamStart = Character;
                TeamEnd = Character + 1;
            }
            for (int x = TeamStart; x < TeamEnd; x++)//for each character affected
            {
                if (ItemPrototypes[ItemID].Revive || Battle.Combatants[x].HP > 0)//If Revive is true, combatants can be healed no matter what. Due to established code, enemies cannot be revived.
                {
                    Battle.Combatants[x].HP += ItemPrototypes[ItemID].HPRestore;
                    Battle.Combatants[x].HP = Math.Min(Battle.Combatants[x].MaxHP, Battle.Combatants[x].HP);//Cannot go over max HP.
                }
                Battle.MP += ItemPrototypes[ItemID].MPRestore;
                Battle.MP = Math.Min(Battle.MP, Battle.MaxMP);//Cannot go over max MP.

            }
        }//Must be changed later if status effects are introduced.
    }
    public class Ability
    {
        public int MPCost;
        public string[] Description;
        public string Name;
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
    public class Badge
    {
        public enum Characters { Any = -1, Abel = 0, K813 = 1, Sarge = 2, All = 3 };
        public enum BadgeEffects { Buggernaut, MachineDatabase, Nanomachines, AdaptiveControlUnit, MatterAnnihilator, EmergencyOffenseReserve, EmergencyDefenseReserve, InductionCoil, MinimalistArchetecture, CapacitorSize, ItemAutoLoader, ParryDatabase };
        public int BadgeEffectToChange = -1;
        public int EquippedToCharacter = -1;
        public int ForceEquip = -1;
        public string Name = string.Empty;
        public List<string> Description = new List<string> { };
        public int BPCost;
        public Battle Battle;
        public Inventory Inventory;
        public int ID;
        public bool TeamBadge;
        public Badge(Battle NewBattle, Inventory BadgeInventory)
        {
            Battle = NewBattle;
            Inventory = BadgeInventory;
        }
        public bool Equip(int Equipee)//Successful equip? T/F.
        {
            if (ForceEquip == -1 || ForceEquip == Equipee)
            {
                if (TeamBadge)
                {
                    Equipee = 3;
                }
                if (EquippedToCharacter >= 0)
                {
                    Unequip();
                }
                if (BPCost <= Inventory.BP)
                {
                    Inventory.BP -= BPCost;
                    EquippedToCharacter = Equipee;
                    Equip2(Equipee);
                    return true;
                }
            }
            return false;
        }
        public virtual void Equip2(int Equipee)
        {
            if (BadgeEffectToChange >= 0)
            {
                Battle.Combatants[EquippedToCharacter].BadgeEffects[BadgeEffectToChange] += 1;
            }//Otherwise, Error!
        }
        public void Unequip()
        {
            Inventory.BP += BPCost;
            Unequip2(EquippedToCharacter);
            EquippedToCharacter = -1;
        }
        public virtual void Unequip2(int Equipee) 
        {
            if (BadgeEffectToChange >= 0)
            {
                Battle.Combatants[EquippedToCharacter].BadgeEffects[BadgeEffectToChange] -= 1;
            }//Otherwise, Error!
        }
    }
    public class OPAmplifier : Badge
    {
        public OPAmplifier(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Offensive Power Amplifier";
            Description.Add("Increases base attack by one for the equipped character.");
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
            Description.Add("Increases base defense by one for the equipped character.");
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
            Description.Add("Stores an additional 5MP at the cost of 3BP.");
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
            Description.Add("Increases the max HP of a robot by 4, at the cost of 2 BP.");
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
            Description.Add("Trade away one defense for one attack.");
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
            Description.Add("Trade away one attack for one defense.");
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
    public class Buggernaut : Badge 
    {
        public Buggernaut(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Buggernaut";
            Description.Add("Gain an INSANE amount of defense while under the glitched status effect.");
            BPCost = 2;
            ID = 6;
            BadgeEffectToChange = (int) BadgeEffects.Buggernaut;
        }
    }
    public class MachineDatabase : Badge
    {
        public MachineDatabase(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory) 
        {
            Name = "Machine Database";
            Description.Add("Automatically reveals enemy health bars at the start of a battle");
            Description.Add("if they haven't been tattled yet. (K-813 only)");
            BPCost = 1;
            ID = 7;
            ForceEquip = (int) Characters.K813;
        }
        public override void Equip2(int Equipee)
        {
            Battle.AutoTattle = true;
        }
        public override void Unequip2(int Equipee)
        {
            Battle.AutoTattle = false;
        }
    }
    public class Nanomachines : Badge
    {
        public Nanomachines(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Nanomachines";
            Description.Add("Repairs a robot for 2HP at the end of every turn.");
            BPCost = 2;
            ID = 8;
            BadgeEffectToChange = (int)BadgeEffects.Nanomachines;
        }
    }
    public class FissionReactor : Badge
    {
        public FissionReactor(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory) 
        {
            Name = "Fission Reactor";
            Description.Add("Generates 1 MP every turn for free.");
            BPCost = 3;
            ID = 9;
            TeamBadge = true;
        }
        public override void Equip2(int Equipee)
        {
            Battle.MPRegen++;
        }
        public override void Unequip2(int Equipee)
        {
            Battle.MPRegen--;
        }
    }
    public class AdaptiveControlUnit : Badge
    {
        public AdaptiveControlUnit(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory) 
        {
            Name = "Adaptive Control Unit";
            Description.Add("Recover from all status effects at the end of every turn.");
            Description.Add("(Applies to ALL status effects, good or bad)");
            BPCost = 3;
            ID = 10;
            BadgeEffectToChange = (int)BadgeEffects.AdaptiveControlUnit;
        }
    }
    public class MatterAnnihilator : Badge
    {
        public MatterAnnihilator(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Matter Annihilator";
            Description.Add("Annihilates matter into pure energy.");
            Description.Add("Abilities cost HP instead of MP");
            BPCost = 1;
            ID = 11;
            BadgeEffectToChange = (int)BadgeEffects.MatterAnnihilator;
        }
    }
    public class EmergencyOffenceReserve : Badge
    {
        public EmergencyOffenceReserve(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory) 
        {
            Name = "Emergency Offence Reserve";
            Description.Add("Increases attack by one when under 20% HP.");
            BPCost = 1;
            ID = 12;
        }
        public override void Equip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].BadgeEffects[(int)BadgeEffects.EmergencyOffenseReserve] += 1;
        }
        public override void Unequip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].BadgeEffects[(int)BadgeEffects.EmergencyOffenseReserve] -= 1;
        }
    }
    public class EmergencyDefenseReserve : Badge
    {
        public EmergencyDefenseReserve(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Emergency Defense Reserve";
            Description.Add("Increases defense by one when under 20% HP.");
            BPCost = 1;
            ID = 13;
            BadgeEffectToChange = (int)BadgeEffects.EmergencyDefenseReserve;
        }
    }
    public class InductionCoil : Badge
    {
        public InductionCoil(Battle NewBattle, Inventory BadgeInventory) : base (NewBattle, BadgeInventory)
        {
            Name = "Induction Coil";
            Description.Add("Applies the melting effect for 2 turns to each target");
            BPCost = 1;
            ID = 14;
            BadgeEffectToChange = (int)BadgeEffects.InductionCoil;
        }
    }
    public class MinimalistArchetecture : Badge
    {
        public MinimalistArchetecture(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Minimalist Archetecture";
            Description.Add("Removes all moves except basic attack. Basic attack deals +1 damage.");
            Description.Add("Special attacks? That's bloat. Cut it out.");
            BPCost = 1;
            ID = 15;
            BadgeEffectToChange = (int)BadgeEffects.MinimalistArchetecture;
        }
        public override void Equip2(int Equipee)
        {
            if (Battle.Combatants[Equipee].AllAttacks.Length == 0)
            {
                Battle.Combatants[Equipee].AllAttacks = Battle.Combatants[Equipee].ValidAttacks;
                Battle.Combatants[Equipee].ValidAttacks = new int[] { 0, 1 };//Only attack and defend.
            }
        }
        public override void Unequip2(int Equipee)
        {
            if (Battle.Combatants[Equipee].AllAttacks.Length != 0)
            {
                Battle.Combatants[Equipee].ValidAttacks = Battle.Combatants[Equipee].AllAttacks;
                Battle.Combatants[Equipee].AllAttacks = new int[] { };
            }
        }
    }
    public class LargeCapacitor : Badge
    {
        public LargeCapacitor(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Large Capacitor";
            Description.Add("Takes more energy to charge, but unleashes greater power");
            Description.Add("All attacks cost +1 MP, but they all deal +1 damage.");
            BPCost = 4;
            ID = 16;
            BadgeEffectToChange = (int)BadgeEffects.CapacitorSize;
        }
    }
    public class SmallCapacitor : Badge
    {
        public SmallCapacitor(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Small Capacitor";
            Description.Add("Takes less energy to charge, but unleashes less power");
            Description.Add("All attacks cost -1 MP, but they all deal -1 damage.");
            BPCost = 2;
            ID = 17;
        }
        public override void Equip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].BadgeEffects[(int)BadgeEffects.CapacitorSize] -= 1;
        }
        public override void Unequip2(int Equipee)
        {
            Battle.Combatants[EquippedToCharacter].BadgeEffects[(int)BadgeEffects.CapacitorSize] += 1;
        }
    }
    public class DebugBadge : Badge
    {
        public DebugBadge(Battle NewBattle, Inventory BadgeInventory) : base(NewBattle, BadgeInventory)
        {
            Name = "Debug Badge";
            Description.Add("Grants access to every ability in the game. Mostly irreversible.");
            Description.Add("How did you find this???.");
            BPCost = 6;
            ID = 18;
        }
        public override void Equip2(int Equipee)
        {
                Battle.Combatants[Equipee].ValidAttacks = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        }
    }
    public class ParryDatabase : Badge
    {
        public ParryDatabase(Battle NewBattle, Inventory BadgeInventory) : base (NewBattle, BadgeInventory)
        {
            Name = "Parry Database";
            Description.Add("When hit, deals one damage to the attacker.");
            Description.Add("It's just 10,000 hours of swordfighting videos.");
            BPCost = 3;
            ID = 19;
            BadgeEffectToChange = (int)BadgeEffects.ParryDatabase;
        }
    }
}
