/*
name: Check Roles
description: This script will give a popup telling you a bunch of information regarding your account.
tags: tool, evaluate, account, chrono, heromart, beta, founder, badges, enhancements, rare, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using System.Globalization;
using Newtonsoft.Json;
using Skua.Core.Models.Quests;

public class CheckRoles
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Evaluate();
    }

    public void Evaluate()
    {
        #region Items

        Bot.Bank.Open();
        Bot.Quests.Load(forgeEnhIDs.Concat(new[] { 793, 2937, 8042 }).ToArray());
        Bot.Wait.ForTrue(() => Bot.Bank.Items.Any(), 20);

        int dmgAll51Items = 0;
        (RacialGearBoost, bool)[] racial75Items = racialGears.Select(x => (x, false)).ToArray();


        processItems("world.myAvatar.items");
        processItems("world.bankinfo.items");
        processItems("world.myAvatar.houseitems");


        // The actual output
        Bot.ShowMessageBox(

            // Enhancements
            $"(Victor of War) Valiance:\t\t\t\t{Checkbox(Core.isCompletedBefore(8741))}\n" +
            $"(Conductor of War) Arcana's Concerto:\t\t{Checkbox(Core.isCompletedBefore(8742))}\n" +
            $"(Deliverance of War) Elysium:\t\t\t{Checkbox(Core.isCompletedBefore(8821))}\n" +
            $"(Reflectionist of War) Examen:\t\t\t{Checkbox(Core.isCompletedBefore(8825))}\n" +
            $"(Penitent of War) Pentience:\t\t\t{Checkbox(Core.isCompletedBefore(8822))}\n" +
            $"(Miltonious of War) Ravenous:\t\t\t{Checkbox(Core.isCompletedBefore(9560))}\n" +
            $"(Shadow of War) Dauntless:\t\t\t{Checkbox(Core.isCompletedBefore(9172))}\n" +

            // Classes
            importantItemCheckbox(4, "Necrotic Sword of Doom") +
            importantItemCheckbox(3, "Hollowborn Sword of Doom") +
            importantItemCheckbox(5, "Chaos Avenger") +
            importantItemCheckbox(5, "Providence") +
            importantItemCheckbox(5, "ArchMage") +
            importantItemCheckbox(5, "Legion Revenant") +
            importantItemCheckbox(5, "Void Highlord", "Void Highlord (IoDA)") +
            importantItemCheckbox(4, "Verus DoomKnight") +
            importantItemCheckbox(5, "Dragon of Time") +
            importantItemCheckbox(5, "Arcana Invoker") +

            // Weapons
            importantItemCheckbox(3, "Necrotic Blade of the Underworld") +
            importantItemCheckbox(3, "Necrotic Sword of the Abyss") +
            importantItemCheckbox(5, "Sin of the Abyss") +
            importantItemCheckbox(4, "Exalted Apotheosis") +
            importantItemCheckbox(4, "Dual Exalted Apotheosis") +
            importantItemCheckbox(3, "Hollowborn Reaper's Scythe") +
            importantItemCheckbox(3, "Greatblade of the Entwined Eclipse") +

            // Armor
            $"(Ascendant of War) Awescended:\t\t\t{Checkbox(Core.isCompletedBefore(8042))}\n" +
            importantItemCheckbox(4, "Radiant Goddess of War") +

           //Chrono Check
           OutPutOwnedChrono() +


            // OverAll Role Checks
            $"Apprentice of War:\t\t\t\t{Checkbox(ApprenticeOfWar())}\n" +
            $"Mastere of War:\t\t\t\t{Checkbox(MastereofWar())}\n" +
            $"Apostle of War:\t\t\t\t{Checkbox(Apostleofwar())}\n" +
            $"Bishop of War:\t\t\t\t{Checkbox(BishopofWar())}\n" +
            $"Cardinal of War:\t\t\t\t{Checkbox(CardinalofWar())}\n" +
            $"51% DMG All Weapons:\t\t{dmgAll51Items} out of 22\n\n" +
            GetQuestStatusReport(),

           "Evaluation Complete");

        #endregion

        void processItems(string prop)
        {
            var list = Bot.Flash.GetGameObject<List<dynamic>>(prop);
            if (list != null)
            {
                for (int i = 0; i < racial75Items.Length; i++)
                {
                    if (!racial75Items[i].Item2 &&
                        list.Any(item =>
                            item.sMeta != null &&
                            ((string)item.sMeta).Contains($"{racial75Items[i].Item1}:1.75")))
                        racial75Items[i].Item2 = true;
                }
                dmgAll51Items += list.Count(item => item.sMeta != null && ((string)item.sMeta).Contains("dmgAll:1.51"));
            }
        }

    }

    private bool ChronoOwned(out string ownedChronoClass)
    {
        // Filter for Chrono/Time classes
        string[] ChronoClasses = hmClasses.Where(x => x.Contains("Chrono") || x.Contains("Time")).ToArray();

        // Find the first owned Chrono class from Inventory or Bank
        var chronoClass = Bot.Inventory.Items
            .Concat(Bot.Bank.Items)
            .FirstOrDefault(x => ChronoClasses.Contains(x.Name));

        // Set the out parameter to the found class name or an empty string if not found
        ownedChronoClass = chronoClass?.Name ?? string.Empty;

        // Return true if a Chrono class was found, otherwise false
        return !string.IsNullOrEmpty(ownedChronoClass);
    }

    private bool ApprenticeOfWar()
    {
        return Bot.Player.Level >= 100 &&
                Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(item => dpsClasses.Contains(item.Name)) &&
                Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(item => farmerClasses.Contains(item.Name)) &&
                Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(item => supportClasses.Contains(item.Name));
    }

    private bool MastereofWar()
    {
        return ApprenticeOfWar()
               && Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(item => item != null && Core.GetBoostFloat(item, "dmgAll") > 1.3f && IsNonWeaponOrArmor(item))
               && Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(item => item != null && Core.GetBoostFloat(item, "dmgAll") > 1.3f && !IsNonWeaponOrArmor(item));
    }

    private bool Apostleofwar()
    {
        return MastereofWar()
       && Core.CheckInventory(ApostleWeapons, any: true, toInv: false) || Core.CheckInventory(Apostleinsignias, toInv: false);
    }

    private bool BishopofWar()
    {
        return Apostleofwar() && HasItemWithMinimalBoost(1.5f, false);
    }

    private bool CardinalofWar()
    {
        return BishopofWar() && HasItemWithMinimalBoost();
    }







    #region ArmyRoles
    /*
   
   Master of War:
    Have @Apprentice of War.
    Have a weapon with +30% damage to All monsters on 4 or more accounts.
    Have a non-enhance-able item with +30% damage to 4 or more monster types on 4 or more accounts.

    Apostle of War:
    Have @Master of War.
    Show Ultra Ezrajal, Ultra Warden, and Ultra Engineer insignias in army inventories, OR a recording of your army killing them.

    Bishop of War:
    Have @Apostle of War.
    Have 1 +51% weapon role, i.e. @Legatus of War.
    Have 1 Class-based role, i.e. @ArchMage of War.
    Show Ultra Dage/Nulgath insignias in army inventories, OR a recording of your army killing them.

    Cardinal of War:
    Have @Bishop of War.
    Have 1 Weapon, Helm, and Cape Forge Enhancement unlocked.
    Have 4 +51% weapon roles, i.e. @Legatus of War. (@Deacon of War counts as 2).
    Have 4 Class-based roles, i.e. @ArchMage of War.
    Show Dark Carnax char page badge, OR a recording of your army killing Dark Carnax.
    */
    #endregion
    #region ignore these
    // DPS Classes
    string[] dpsClasses = new[]
    {
    "Dragon of Time",
    "Glacial Berserker",
    "Guardian",
    "Legion DoomKnight",
    "Legion Revenant",
    "LightCaster",
    "Lycan",
    "Psionic MindBreaker",
    "Void HighLord"
};
    // Farmers
    string[] farmerClasses = new[]
    {
    "Abyssal Angel",
    "ArchMage",
    "Blaze Binder",
    "Daimon",
    "Dragon of Time",
    "Eternal Inversionist",
    "Firelord Summoner",
    "Legion Revenant",
    "Dark Master of Moglins",
    "Master of Moglins",
    "NCM",
    "Scarlet Sorceress",
    "ShadowScythe General",
    "Shaman"
};
    // Support Classes
    string[] supportClasses = new[]
    {
    "ArchFiend",
    "ArchPaladin",
    "Frostval Barbarian",
    "Infinity Titan",
    "Dark Legendary Hero",
    "Legendary Hero",
    "Legion Revenant",
    "LightCaster",
    "Lord of Order",
    "NorthLands Monk",
    "Quantum Chronomancer",
    "Continuum Chronomancer",
    "StoneCrusher"
};
    private int[] rareIDs =
    {
        21, // Limited Time Drop
        68, // New Collection Chest
        35, // Rare
        40, // Import Item
        55, // Sesaonal Rare
        60, // Event Item
        65, // Event Rare
        70, // Limited Rare
        75, // Collector's Rare
        80, // Promotional Item
        90, // Ultra Rare
        95, // Super Mega Ultra Rare
    };
    private string[] houseCat =
    {
        "Floor Item",
        "Wall Item",
        "House",
    };
    private int[] forgeEnhIDs =
    {
        8738,
        8739,
        8740,
        8741,
        8742,
        8743,
        8745,
        8758,
        8821,
        8820,
        8822,
        8823,
        8824,
        8825,
        8826,
        8827,
        9172,
        9171,
    };
    private (string Name, int ID)[] ForgeQuests = new (string Name, int ID)[]
      {
    ("Forge Weapon Enhancement", 8738),
    ("Lacerate", 8739),
    ("Smite", 8740),
    ("Hero's Valiance", 8741),
    ("Arcana's Concerto", 8742),
    ("Absolution", 8743),
    ("Avarice", 8745),
    ("Praxis", 9171),
    ("Acheron", 8820),
    ("Elysium", 8821),
    ("Penitence", 8822),
    ("Lament", 8823),
    ("Vim, Ether", 8824),
    ("Anima", 8826),
    ("Pneuma", 8827),
    ("Dauntless", 9172),
    ("Forge Cape Enhancement", 8758),
    ("Examen", 8825)
      };
    private string[] hmClasses =
    {
        "CardClasher",
        "Chrono Chaorruptor",
        "Chrono Commandant",
        "Chrono DataKnight",
        "Chrono DragonKnight",
        "ChronoCommander",
        "ChronoCorruptor",
        "Chronomancer",
        "Chronomancer Prime",
        "Classic Defender",
        "Classic Dragonlord",
        "Classic Guardian",
        "Continuum Chronomancer",
        "Corrupted Chronomancer",
        "Dark Master of Moglins",
        "Defender",
        "Dragonlord",
        "DoomKnight OverLord",
        "Dragon Knight",
        "Empyrean Chronomancer",
        "Eternal Chronomancer",
        "Flame Dragon Warrior",
        "Great Thief",
        "Guardian",
        "Heavy Metal Rockstar",
        "Heavy Metal Necro",
        "Immortal Chronomancer",
        "Infinity Knight",
        "Interstellar Knight",
        "Legion Paladin",
        "Master of Moglins",
        "Nechronomancer",
        "Necrotic Chronomancer",
        "NOT A MOD",
        "Nu Metal Necro",
        "Obsidian Paladin Chronomancer",
        "Overworld Chronomancer",
        "Paladin Chronomancer",
        "Paladin Highlord",
        "PaladinSlayer",
        "Quantum Chronomancer",
        "ShadowStalker of Time",
        "ShadowWalker of Time",
        "ShadowWeaver of Time",
        "Star Captain",
        "StarLord",
        "TimeKeeper",
        "TimeKiller",
        "Timeless Chronomancer",
        "Underworld Chronomancer",
        "Unchained Rocker",
    };
    string[] ApostleWeapons = new[]
 {
        "Apostate Alpha",
        "Thaumaturgus Alpha",
        "Apostate Omega",
        "Thaumaturgus Omega",
        "Apostate Ultima",
        "Thaumaturgus Ultima",
        "Exalted Penultima",
        "Exalted Unity",
        "Exalted Apotheosis"
    };
    string[] Apostleinsignias = new[]
    {
        "Ezrajal Insignia",
        "Warden Insignia",
        "Engineer Insignia"
    };
    #endregion

    #region Methods
    string importantItemCheckbox(int tabs, params string[] items)
    {
        string _tabs = string.Empty;
        for (int t = 0; t < tabs; t++)
            _tabs += '\t';
        return $"{items[0]}:{_tabs}{Checkbox(Core.CheckInventory(items, 1, true, false))}\n";
    }
    string Checkbox(bool check)
        => $"[ {(check ? "🗸" : "\u200AX\u200A")} ]";
    bool isCollectionChest(InventoryItem item)
        => item.Category == ItemCategory.Pet && (item.Name.Contains("Chest") || item.Name.Contains("Collection"));
    bool NoneEnhFilter(InventoryItem x)
    {
        return
         x.Category != ItemCategory.Sword
            && x.Category != ItemCategory.Axe
            && x.Category != ItemCategory.Dagger
            && x.Category != ItemCategory.Gun
            && x.Category != ItemCategory.HandGun
            && x.Category != ItemCategory.Rifle
            && x.Category != ItemCategory.Bow
            && x.Category != ItemCategory.Mace
            && x.Category != ItemCategory.Gauntlet
            && x.Category != ItemCategory.Polearm
            && x.Category != ItemCategory.Staff
            && x.Category != ItemCategory.Wand
            && x.Category != ItemCategory.Whip
            && x.Category != ItemCategory.Helm
            && x.Category != ItemCategory.Cape;
    }
    bool IsNonWeaponOrArmor(InventoryItem x)
    {
        return x.Category != ItemCategory.Sword
            && x.Category != ItemCategory.Axe
            && x.Category != ItemCategory.Dagger
            && x.Category != ItemCategory.Gun
            && x.Category != ItemCategory.HandGun
            && x.Category != ItemCategory.Rifle
            && x.Category != ItemCategory.Bow
            && x.Category != ItemCategory.Mace
            && x.Category != ItemCategory.Gauntlet
            && x.Category != ItemCategory.Polearm
            && x.Category != ItemCategory.Staff
            && x.Category != ItemCategory.Wand
            && x.Category != ItemCategory.Whip
            && x.Category != ItemCategory.Helm
            && x.Category != ItemCategory.Cape;
    }
    public string GetQuestStatusReport()
    {
        // Call CheckQuests to get the list of incomplete quests
        int unlockedQuests = CheckForgeQuests(out List<string> incompleteQuests);

        // Build the report string
        var reportBuilder = new System.Text.StringBuilder();
        reportBuilder.AppendLine($"Number of unlocked quests: {unlockedQuests}");

        if (incompleteQuests.Count > 0)
        {
            reportBuilder.AppendLine("Incomplete quests:");
            foreach (var quest in incompleteQuests)
            {
                reportBuilder.AppendLine(quest);
            }
        }
        else
        {
            reportBuilder.AppendLine("All Forge Quests are completed!");
        }

        return reportBuilder.ToString();
    }
    /// <summary>
    /// Checks if there is any item in the inventory or bank with a boost value of at least the specified minimum.
    /// The item boost is evaluated based on various racial gear boosts, and an optional filter can exclude non-weapon or non-armor items.
    /// If the <paramref name="NoneEnhanceAble"/> parameter is true, it will additionally check that at least 4 out of 5 specified racial gear boost types have the minimum boost.
    /// </summary>
    /// <param name="minimumBoost">The minimum boost value required. Default is 1.3 (30%).</param>
    /// <param name="NoneEnhanceAble">If true, excludes items that are neither weapons nor armor, and checks if at least 4 out of 5 specified racial boost types meet the requirement. Default is false.</param>
    /// <returns>
    /// Returns true if at least one item meets the minimum boost requirement and, if specified, is filtered according to the <paramref name="NoneEnhanceAble"/> parameter.
    /// Returns false otherwise.
    /// </returns>
    public bool HasItemWithMinimalBoost(float minimumBoost = 1.3f, bool NoneEnhanceAble = false)
    {
        // Calculate the percentage needed for the HasMinimalBoost method
        int requiredPercentage = (int)((minimumBoost - 1) * 100);

        if (NoneEnhanceAble)
        {
            // List of racial gear boosts to check
            RacialGearBoost[] racialBoosts = new[]
            {
                RacialGearBoost.Chaos,
                RacialGearBoost.Dragonkin,
                RacialGearBoost.Elemental,
                RacialGearBoost.Human,
                RacialGearBoost.Undead
            };

            // Count how many racial gear boosts meet the minimum boost requirement
            int boostCount = racialBoosts.Count(boost => Adv.HasMinimalBoost(boost, requiredPercentage));

            // Check if at least 4 out of 5 racial gear boosts meet the minimum boost requirement
            if (boostCount >= 4)
            {
                // Check if there is any item with the required boost, considering the filter
                return Bot.Inventory.Items.Concat(Bot.Bank.Items)
                    .Any(item => item != null
                                && (!IsNonWeaponOrArmor(item)));
            }

            return false;
        }
        else
        {
            // Check if there is any item with the required boost, considering the filter
            return Bot.Inventory.Items.Concat(Bot.Bank.Items)
                .Any(item => item != null
                            && (Adv.HasMinimalBoost(RacialGearBoost.Chaos, requiredPercentage)
                                || Adv.HasMinimalBoost(RacialGearBoost.Dragonkin, requiredPercentage)
                                || Adv.HasMinimalBoost(RacialGearBoost.Elemental, requiredPercentage)
                                || Adv.HasMinimalBoost(RacialGearBoost.Human, requiredPercentage)
                                || Adv.HasMinimalBoost(RacialGearBoost.Undead, requiredPercentage))
                            && (!NoneEnhanceAble || IsNonWeaponOrArmor(item)));
        }
    }
    private int CheckForgeQuests(out List<string> incompleteQuests)
    {
        incompleteQuests = new List<string>();
        int unlockedQuests = 0;

        foreach (var (Name, ID) in ForgeQuests)
        {
            var questID = Bot.Quests.EnsureLoad(ID).ID;
            if (Core.isCompletedBefore(questID))
            {
                unlockedQuests++;
            }
            else
            {
                incompleteQuests.Add($"Quest '{Name}' (ID: {ID}) is not completed.");
            }
        }

        return unlockedQuests;
    }
    public string OutPutOwnedChrono()
    {
        // Variables to store the results
        bool isChronoOwned = ChronoOwned(out string ownedChronoClass);

        // Format the report line with the checkmark after the owned Chrono class name
        string reportLine = $"ChronoMancer Owned? {(isChronoOwned ? $"[{ownedChronoClass}]\t{Checkbox(true)}" : Checkbox(false))}\n";

        return reportLine;
    }
    private RacialGearBoost[] racialGears =
         Enum.GetValues<RacialGearBoost>().Except(RacialGearBoost.None, RacialGearBoost.Drakath, RacialGearBoost.Orc);


    #endregion
}
