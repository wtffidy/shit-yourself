
/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class ShitYourSelf
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        ThisIswhatYouWanted();

        Core.SetOptions(false);
    }

    public void ThisIswhatYouWanted()
    {
        Bot.Options.LagKiller = false;
        Bot.Options.CustomName = "Why are we on this map";
        Bot.Options.CustomGuild = "you asked for this...";
        Random rand = new Random();
        int randomNumber = rand.Next(5); // generate a random number from 0 to 9

        switch (randomNumber)
        {
            case 0:
                Core.Join("yulgar-999999", "Bathroom", "Center");
                Core.Logger("Congrats you made it to the bathroom, dont worry about your guests they like to watch.");
                break;
            case 1:
                Core.Join("Tercessuinitlim-999999", "Boss", "Right");
                Bot.Options.DisableCollisions = true;
                Bot.Player.WalkTo(250, 354);
                Core.Logger($"Lets be honest here.. for all this farming he sorta deserves it");
                Bot.Options.DisableCollisions = false;
                break;
            case 2:
                Core.Join("Party-999999");
                Core.Logger($"its now a lemon party.");
                break;
            case 3:
                Core.Join("battleontown-999999", "r9", "Right");
                Core.Logger($"off a cliff? seems about right, go ahead");
                Bot.Player.WalkTo(364, 343);
                break;

            case 4:
                Core.Logger($"you just straight piss your pants man.. idk what to tell you.");
                break;
            case 5:
                Core.Join("shadowfall-999999", "Inside", "Right");
                Bot.Options.DisableCollisions = true;
                Bot.Player.WalkTo(748, 174);
                Core.Logger($"...a man of culture i see");
                Bot.Options.DisableCollisions = false;
                break;
            default:
                Core.Logger($"you managed to hold it :O congratulations!");
                break;
        }
        Bot.Sleep(10000);
    }
}
