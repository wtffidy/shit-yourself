/*
name: Blaze Beard Story
description: This will finish the Blaze Beard Story Quest.
tags: story, quest, blaze-beard, pirate
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class LegacyTemplate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        // Core.BankingBlackList.AddRange(RequiredItems);
        Core.SetOptions();

        TokenQuests();

        Core.SetOptions(false);
    }

    // string[] RequiredItems = { "Pirate Booty I", "Pirate Booty II", "Pirate Booty III", "Pirate Booty IV", "Pirate Booty V", "Pirate Booty VI", "Pirate Booty VII", "Pirate Booty VIII", "Pirate Booty IX", "Pirate Booty X", "Pirate Booty XI", "Pirate Booty XII", "Pirate Booty XIII", "Pirate Booty XIV", "Pirate Booty XV" };


    public void TokenQuests()
    {
        if (!Core.isSeasonalMapActive("BlazeBeard"))
            return;

        Story.PreLoad(this);

        Story.LegacyQuestManager(QuestLogic, 1, 2, 3);

        void QuestLogic()
        {
            //InsertQuestID from the LegacyQuestManager in order, per case
            switch (Story.LegacyQuestID)
            {
                case 1:
                    Core.HuntMonster("map", "mob", "item", quant);
                    break;

                case 2:
                    //ItemID - How to Get: Goto Quest Map > Tools > Grabber > GetMap Item Ids > ItemID is the first # in the bit lines that show up.
                    Core.GetMapItem(ItemID, quant, "map");
                    break;

                case 3:
                //There area  few ways to use Core.BuyItem (itemname, ItemID, or ShopItemID)
                //shop item id can be gotten through the Grabber, just open teh shop goto the shop items tab in grabber, and hit grab. the ShopItemID will be on the Right you may need to scroll
                Shop
                    Core.BuyItem("map", shopID, "Itename", quant);
                    Core.BuyItem("map", shopID, itemID, quant);
                    Core.BuyItem("map", shopID, "Itename", ShopItemID, quant);
                    Core.BuyItem("map", shopID, "Itename", quant);
                    break;

            }
        }
    }
}

{
    private IScriptInterface Bot => IScriptInterface.Instance;
private CoreBots Core => CoreBots.Instance;

public void ScriptMain(IScriptInterface Bot)
{
    Core.SetOptions();

    Example();
    Core.SetOptions(false);
}

public void Example()
{
    if (Core.CheckInventory("item"))
        return;

    //INSERT CODE HERE      
}
}