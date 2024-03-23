// Copyright 2019 Squirting Elephant.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using RimWorld;
using UnityEngine;
using Verse;

namespace SquirtingElephant.Helpers;

public static class Utils
{
    public enum ESide
    {
        LeftHalf,
        RightHalf,
        BothSides
    }

    private const string DEF_NOT_FOUND_FORMAT =
        "Unable to find {0}: {1}. Please ensure that this def exists in the database and that the database was loaded before trying to locate this.";

    private const string SINGLE_WHITE_SPACE = " ";

    public static void LogDefNotFoundWarning(string defName, string defType = "Def")
    {
        Log.Warning(string.Format(DEF_NOT_FOUND_FORMAT, defType, defName));
    }

    public static void LogDefNotFoundError(string defName, string defType = "Def")
    {
        Log.Error(string.Format(DEF_NOT_FOUND_FORMAT, defType, defName));
    }

    public static T GetDefByDefName<T>(string recipeDefName, bool errorOnNotFound = true) where T : Def
    {
        var def = DefDatabase<T>.GetNamed(recipeDefName);
        if (def != null)
        {
            return def;
        }

        if (errorOnNotFound)
        {
            LogDefNotFoundError(recipeDefName, typeof(T).Name);
        }

        return null;
    }

    public static void SetThingStat(string thingDefName, string statDefName, float newValue)
    {
        var def = GetDefByDefName<ThingDef>(thingDefName);
        if (def != null)
        {
            def.statBases.Find(s => s.stat.defName == statDefName).value = newValue;
        }
    }

    /// <summary>
    ///     Note that this method assumes that windows is installed on the C-drive.
    /// </summary>
    public static string GetModSettingsFolderPath()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return
                $@"C:\Users\{Environment.UserName}\AppData\LocalLow\Ludeon Studios\RimWorld by Ludeon Studios\Config";
        }

        return RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
            ? "~/Library/Application Support/RimWorld/Config"
            : "~/.config/unity3d/Ludeon Studios/RimWorld by Ludeon Studios/Config"; // Unix
    }

    public static void OpenModSettingsFolder()
    {
        var path = GetModSettingsFolderPath();
        if (Directory.Exists(path))
        {
            Process.Start(path);
        }
        else
        {
            Log.Error($"Unable to open path: {path}. This error is not problematic and doesn't hurt your game.");
        }
    }

    public static void SetupTextureAndColorIt(ref Texture2D texture, int width, int height, Color color)
    {
        var paintTexture = false;
        if (texture == null)
        {
            texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
            paintTexture = true;
        }
        else if (texture.width != width || texture.height != height)
        {
            if (width == 0 || height == 0)
            {
                Log.Error("Received either a 0 for the texture width and/or height.");
            }

            paintTexture = texture.width < width || texture.height < height;
            texture.width = width;
            texture.height = height;
        }

        if (!paintTexture)
        {
            return;
        }

        var textureArray = texture.GetPixels();
        for (var i = 0; i < textureArray.Length; ++i)
        {
            textureArray[i] = color;
        }

        texture.SetPixels(textureArray);
        texture.Apply();
    }

    public static IEnumerable<Building> GetBuildingsByDefName(string defName)
    {
        if (Current.Game == null || Current.Game.CurrentMap == null)
        {
            return Enumerable.Empty<Building>();
        }

        return Current.Game.CurrentMap.listerBuildings.allBuildingsColonist.Where(b => b.def.defName == defName);
    }

    public static void AddRecipeUnique(ThingDef thingDef, RecipeDef recipe)
    {
        if (thingDef.recipes.Any(r => r.defName == recipe.defName))
        {
            thingDef.recipes.Add(recipe);
        }
    }

    /// <summary>
    ///     Looks up recipes in one ThingDef and adds references to them to another ThingDef.recipes.
    /// </summary>
    public static void CopyRecipesFromAToB(string sourceDefName, string destinationDefName)
    {
        var source = GetDefByDefName<ThingDef>(sourceDefName);
        var destination = GetDefByDefName<ThingDef>(destinationDefName);

        foreach (var recipe in source.recipes)
        {
            AddRecipeUnique(destination, recipe);
        }
    }

    public static void AddRecipesToDef(string thingDefName, bool errorOnRecipeNotFound,
        params string[] recipeDefNames)
    {
        if (recipeDefNames.Length == 0)
        {
            return;
        }

        var td = GetDefByDefName<ThingDef>(thingDefName, false);
        if (td == null)
        {
            return;
        }

        foreach (var recipeDefName in recipeDefNames)
        {
            var recipe = GetDefByDefName<RecipeDef>(recipeDefName, errorOnRecipeNotFound);
            if (recipe != null)
            {
                AddRecipeUnique(td, recipe);
            }
        }
    }

    private static Rect GetRectFor(Listing_Standard ls, ESide side, float rowHeight)
    {
        switch (side)
        {
            case ESide.LeftHalf:
                return ls.GetRect(rowHeight).LeftHalf();
            case ESide.RightHalf:
                return ls.GetRect(rowHeight).RightHalf();
            case ESide.BothSides:
                return ls.GetRect(rowHeight);
            default:
                throw new ArgumentException("Unexpected value", nameof(side));
        }
    }

    public static void MakeCheckboxLabeled(Listing_Standard ls, string translationKey, ref bool checkedSetting,
        ESide side = ESide.RightHalf, float rowHeight = 32f)
    {
        var boxRect = GetRectFor(ls, side, rowHeight);
        Widgets.CheckboxLabeled(boxRect, translationKey.Translate().CapitalizeFirst() + SINGLE_WHITE_SPACE,
            ref checkedSetting);
    }

    public static void MakeTextFieldNumericLabeled<T>(Listing_Standard ls, string translationKey, ref T setting,
        float min = 1, float max = 1000, ESide side = ESide.RightHalf, float rowHeight = 32f) where T : struct
    {
        var boxRect = GetRectFor(ls, side, rowHeight);
        var buffer = setting.ToString();
        Widgets.TextFieldNumericLabeled(boxRect, translationKey.Translate().CapitalizeFirst() + SINGLE_WHITE_SPACE,
            ref setting, ref buffer, min, max);
    }

    public static void EditPowerGenerationValue(string thingDefName, int newPowerGenerationAmount)
    {
        var thingDef = GetDefByDefName<ThingDef>(thingDefName);
        if (thingDef == null)
        {
            return;
        }

        var fieldInfo = typeof(CompProperties_Power).GetField("basePowerConsumption",
            BindingFlags.Instance | BindingFlags.NonPublic);
        if (fieldInfo != null)
        {
            fieldInfo.SetValue(thingDef.comps.OfType<CompProperties_Power>().First(),
                -Math.Abs(newPowerGenerationAmount));
        }
    }

    public static void SetWorkAmount(string recipeDefName, int newWorkAmount)
    {
        var rd = GetDefByDefName<RecipeDef>(recipeDefName);
        if (rd != null)
        {
            rd.workAmount = newWorkAmount;
        }
    }

    public static void SetYieldAmount(string recipeDefName, int newYieldAmount)
    {
        var def = GetDefByDefName<RecipeDef>(recipeDefName);
        def?.products.ForEach(p => p.count = newYieldAmount);
    }

    public static void SetResearchBaseCost(string researchDefName, int newResearchCost)
    {
        var rpd = GetDefByDefName<ResearchProjectDef>(researchDefName);
        if (rpd != null)
        {
            rpd.baseCost = newResearchCost;
        }
    }

    public static void SetThingMaxHp(string thingDefName, int newHP)
    {
        SetThingStat(thingDefName, "MaxHitPoints", newHP);
    }

    public static void SetThingMaxBeauty(string thingDefName, int newBeauty)
    {
        SetThingStat(thingDefName, "Beauty", newBeauty);
    }

    public static void SetThingTurretBurstCooldown(string thingDefName, float newTurretBurstCooldown)
    {
        var def = GetDefByDefName<ThingDef>(thingDefName);
        if (def != null)
        {
            def.building.turretBurstCooldownTime = newTurretBurstCooldown;
        }
    }

    public static void SetThingSteelCost(string thingDefName, int newSteelCost)
    {
        var def = GetDefByDefName<ThingDef>(thingDefName);

        var costDef = def?.costList.FirstOrDefault(c => c.thingDef == ThingDefOf.Steel);
        if (costDef != null)
        {
            costDef.count = newSteelCost;
        }
    }

    public static void SetThingComponentCost(string thingDefName, int newComponentCost)
    {
        var def = GetDefByDefName<ThingDef>(thingDefName);

        var costDef = def?.costList.FirstOrDefault(c => c.thingDef == ThingDefOf.ComponentIndustrial);
        if (costDef != null)
        {
            costDef.count = newComponentCost;
        }
    }

    public static void SetThingComponentSpacerCost(string thingDefName, int newComponentSpacerCost)
    {
        var def = GetDefByDefName<ThingDef>(thingDefName);

        var costDef = def?.costList.FirstOrDefault(c => c.thingDef == ThingDefOf.ComponentSpacer);
        if (costDef != null)
        {
            costDef.count = newComponentSpacerCost;
        }
    }
}