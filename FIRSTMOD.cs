using MelonLoader;
using BTD_Mod_Helper;
using FIRSTMOD;
using Il2CppAssets.Scripts.Models;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.Stats;
using BTD_Mod_Helper.Api.ModOptions;
using HarmonyLib;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Scenes;
using Il2CppAssets.Scripts.Unity.UI_New.Main;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Data;
using System.Collections.Generic;

[assembly: MelonInfo(typeof(FIRSTMOD.FIRSTMOD), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace FIRSTMOD;

public class FIRSTMOD : BloonsTD6Mod
{
    public override void OnApplicationStart() // runs when started
    {
        // prints to the console
        MelonLogger.Msg("FIRSTMOD loaded");
    }

    [HarmonyPatch(typeof(MainMenu), "Open")]
    public class MainMenuPatch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            // give 999999999 monkey money
            ModSettingInt Amount_monkey_money = new ModSettingInt(999999999);
            Game.instance.playerService.Player.Data.monkeyMoney.Value = Amount_monkey_money;
            // give 999999999 xp to every tower
            ModSettingInt xp = new ModSettingInt(999999999);
            for (int i = 0; i < Game.instance.model.towers.Count; i++)
            {
                Game.instance.playerService.Player.AddTowerXP(Game.instance.model.towers[i].name, 100);
            }
            foreach (var item in Game.instance.playerService.Player.Data.towerXp)
            {
                Game.instance.playerService.Player.Data.towerXp[item.key].Value = xp;
            // give 999999999 monkey knowledge
            ModSettingInt Amount_monkey_knowledge = new ModSettingInt(999999999);
            Game.instance.playerService.Player.Data.KnowledgePoints = Amount_monkey_knowledge;
            
            }
            // gives all items in trophy store
            var testList = new List<string>();
            foreach (var item in Game.instance.playerService.Player.Data.trophyStorePurchasedItems)
                testList.Add(item.Key);
            foreach (var item in GameData._instance.trophyStoreItems.storeItems)
            {
                if (testList.Contains(item.id)) continue;
                Game.instance.playerService.Player.AddTrophyStoreItem(item.id);
                MelonLogger.Msg($"Unlocked {item.id}");
            }
        }
    }
}
