/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;
using Skua.Core.Utils;
using System.Text;
using Skua.Core.Models;

public class BestGearAttempt
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreStory Story = new();
    private CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "item1", "Item2", "Etc" });
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example(bool TestMode = false)
    {
          EquipBestByMetaType("dmgAll");

        #region EquipBestByMetaType

        // Method to equip the best item based on specified meta type
        void EquipBestByMetaType(string metaType)
        {
            var excludedMetaTypes = new[] { "AutoAdd" }; // Meta types to exclude

            // Function to process items and extract meta information
            List<(ItemBase Item, string MetaType, double Value, ItemCategory Category)> ProcessItems(IEnumerable<ItemBase> items)
            {
                var result = new List<(ItemBase Item, string MetaType, double Value, ItemCategory Category)>();

                foreach (var item in items.Where(x => x != null))
                {
                    if (item?.Meta == null)
                        continue; // Skip items with no meta

                    // Process meta entries
                    var metaEntries = item.Meta.Split('\n')
                        .SelectMany(metaLine =>
                        {
                            var metaKeyValues = metaLine.Replace("AutoAdd,", "").Split(','); // Handle multiple meta values
                            return metaKeyValues
                                .Select(metaKeyValue =>
                                {
                                    var metaKeyValuePair = metaKeyValue.Split(':'); // Split each meta entry into key and value
                                    if (metaKeyValuePair.Length == 2 && double.TryParse(metaKeyValuePair[1], out double value) && value > 0)
                                    {
                                        var metaType = metaKeyValuePair[0];
                                        // Return a tuple only if meta type is not in excluded list
                                        if (!excludedMetaTypes.Contains(metaType))
                                            return (MetaType: metaType, Value: value);
                                    }
                                    return default((string MetaType, double Value)); // Return default value if invalid
                                })
                                .Where(entry => !string.IsNullOrEmpty(entry.MetaType)) // Filter out empty entries
                                .GroupBy(entry => entry.MetaType) // Group by meta type
                                .SelectMany(group => group
                                    .OrderByDescending(x => x.Value) // Sort by value
                                    .Select(x => (Item: item, MetaType: group.Key, x.Value, Category: item.Category))
                                );
                        });

                    result.AddRange(metaEntries);
                }

                return result;
            }

            // Concatenate inventory and bank items
            var items = Bot.Inventory.Items.OfType<ItemBase>()
                .Concat(Bot.Bank.Items.OfType<ItemBase>())
                .ToList();

            // Process items
            var processedItems = ProcessItems(items);

            // Separate lists based on categories
            var helmItems = processedItems.Where(x => x.Category == ItemCategory.Helm).ToList();
            var capeItems = processedItems.Where(x => x.Category == ItemCategory.Cape).ToList();
            var floorItemItems = processedItems.Where(x => x.Category == ItemCategory.FloorItem).ToList();
            var weaponItems = processedItems.Where(x => x.Category == ItemCategory.Sword
                                                      || x.Category == ItemCategory.Axe
                                                      || x.Category == ItemCategory.Dagger
                                                      || x.Category == ItemCategory.Gun
                                                      || x.Category == ItemCategory.HandGun
                                                      || x.Category == ItemCategory.Rifle
                                                      || x.Category == ItemCategory.Bow
                                                      || x.Category == ItemCategory.Mace
                                                      || x.Category == ItemCategory.Gauntlet
                                                      || x.Category == ItemCategory.Polearm
                                                      || x.Category == ItemCategory.Staff
                                                      || x.Category == ItemCategory.Wand
                                                      || x.Category == ItemCategory.Whip).ToList();
            var armorItems = processedItems.Where(x => x.Category == ItemCategory.Armor).ToList();

            // Equip the best item based on specified meta type for each category
            EquipBestItem(weaponItems, metaType);
            EquipBestItem(helmItems, metaType);
            EquipBestItem(capeItems, metaType);
            EquipBestItem(armorItems, metaType);
            EquipBestItem(floorItemItems, metaType);
        }

        // Helper method to equip the best item based on a specific meta type
        void EquipBestItem(IEnumerable<(ItemBase Item, string MetaType, double Value, ItemCategory Category)> items, string metaType)
        {
            var bestItem = items
                .Where(x => x.MetaType.Equals(metaType, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.Value)
                .FirstOrDefault();

            if (bestItem.Item != null)
            {
                Core.Equip(bestItem.Item.Name);
                Bot.Wait.ForTrue(() => Bot.Inventory.IsEquipped(bestItem.Item.Name), 20);
            }
        }

        #endregion
    }

    public enum MetaTypes
    {
dmgAll,
gold,
rep,
cp,
exp,
Dragonkin,
    };
}