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
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;

public class DefaultTemplate
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreStory Story = new();
    private CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Bone Dust" });
        Core.SetOptions();

        Example("battleunderb", "Enter", "Spawn", "*", "Bone Dust", 500);

        Core.SetOptions(false);
    }

    public void Example(string? map = null, string? Cell = null, string? Pad = null, string? MobName = null, string? item = null, int quant = 0, bool isTemp = false)
    {
        // Core.DL_Enable();
        Core.DebugLogger(this);
        Core.EquipClass(ClassType.Solo);
        while (!Bot.ShouldExit && isTemp ? !Bot.TempInv.Contains(item, quant) : !Core.CheckInventory(item, quant))
        {
            CheckVariables(map, Cell, Pad);

            if (MobName == "*")
            {
                Core.DebugLogger(this);
                foreach (Monster mob in Bot.Monsters.CurrentAvailableMonsters
                    .Where(m => m != null))
                {
                    if (mob == null) continue;

                    Core.DebugLogger(this);
                    while (!Bot.ShouldExit && isTemp ? !Bot.TempInv.Contains(item, quant) : !Bot.Inventory.Contains(item, quant))
                    {
                        Core.DebugLogger(this);
                        // wait for play to be alive to negate null target on death
                        while (!Bot.ShouldExit && !Bot.Player.Alive) { }
                        Core.DebugLogger(this);
                        CheckVariables(map, Cell, Pad);
                        Core.DebugLogger(this);
                        if (mob != null && (!Bot.Player.HasTarget || (Bot.Player.Target != null && Bot.Player.Target.HP > 0)))
                        {
                            Core.DebugLogger(this);
                            Bot.Combat.Attack(mob);
                        }
                        Core.DebugLogger(this);
                        Bot.Sleep(500);
                        Core.DebugLogger(this);
                        if (Bot.Player.Target != null && Bot.Player.Target.HP <= 0)
                        {
                            Core.DebugLogger(this);
                            Bot.Combat.CancelTarget();
                            Core.DebugLogger(this);
                            break;
                        }
                        Core.DebugLogger(this);
                    }
                    // Early break if required.
                    if (isTemp ? Bot.TempInv.Contains(item, quant) : Bot.Inventory.Contains(item, quant))
                        break;
                }
            }
            else
            {
                Core.DebugLogger(this);
                foreach (Monster mob in Bot.Monsters.CurrentAvailableMonsters
                    .Where(m => m != null && m.Name == MobName))
                {
                    if (mob == null) continue;

                    Core.DebugLogger(this);
                    while (!Bot.ShouldExit && isTemp ? !Bot.TempInv.Contains(item, quant) : !Bot.Inventory.Contains(item, quant))
                    {
                        Core.DebugLogger(this);
                        // wait for play to be alive to negate null target on death
                        while (!Bot.ShouldExit && !Bot.Player.Alive) { }
                        Core.DebugLogger(this);
                        CheckVariables(map, Cell, Pad);
                        Core.DebugLogger(this);
                        if (mob != null && (!Bot.Player.HasTarget || (Bot.Player.Target != null && Bot.Player.Target.HP > 0)))
                        {
                            Core.DebugLogger(this);
                            Bot.Combat.Attack(mob);
                        }
                        Core.DebugLogger(this);
                        Bot.Sleep(500);
                        Core.DebugLogger(this);
                        if (Bot.Player.Target != null && Bot.Player.Target.HP <= 0)
                        {
                            Core.DebugLogger(this);
                            Bot.Combat.CancelTarget();
                            Core.DebugLogger(this);
                            break;
                        }
                        Core.DebugLogger(this);
                    }
                    // Early break if required.
                    if (isTemp ? Bot.TempInv.Contains(item, quant) : Bot.Inventory.Contains(item, quant))
                        break;
                }

            }
        }

        void CheckVariables(string? map = null, string? Cell = null, string? Pad = null)
        {
            if (Bot.Map.Name == map && Bot.Player.Cell == Cell)
                return;
            Core.DebugLogger(this);

            if (Bot.Map.Name != map)
            {
                Core.DebugLogger(this);
                Core.Join(map);
                Bot.Wait.ForMapLoad(map);
            }
            Core.DebugLogger(this);
            if (Bot.Player.Cell != Cell)
            {
                Core.DebugLogger(this);
                Core.Jump(Cell, Pad);
                Bot.Wait.ForCellChange(Cell);
            }
        }
    }
}



