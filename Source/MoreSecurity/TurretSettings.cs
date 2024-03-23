using SquirtingElephant.Helpers;
using UnityEngine;
using Verse;

namespace SquirtingElephant.MoreSecurity;

public class TurretSettings
{
    private const float ICON_WIDTH = 32f;

    private const int MIN_TURRET_HP = 1;
    private const int MAX_TURRET_HP = 100000;
    private const int MIN_BEAUTY = -500;
    private const int MAX_BEAUTY = 500;
    private const float MIN_STOPPING_POWER = 0.0f;
    private const float MAX_STOPPING_POWER = 12.0f;
    private const int MIN_BULLET_SPEED = 1;
    private const int MAX_BULLET_SPEED = 165;
    private const float MIN_TURRET_SHOOT_COOLDOWN = 0.001f;
    private const float MAX_TURRET_SHOOT_COOLDOWN = 100.0f;
    private const int MIN_COST = 1;
    private const int MAX_COST = 100000;

    private static readonly string[] HEADER_TRANSLATION_KEYS =
    [
        "MS_MaxHP", "MS_Beauty", "MS_StoppingPower", "MS_BulletSpeed", "MS_TurretBurstCooldownTime", "MS_CostSteel",
        "MS_CostComponent"
    ];

    public readonly int Beauty_Default;
    public readonly string BulletDefName; // Def name of the bullet that this turret uses.
    public readonly int BulletSpeed_Default;
    public readonly float BurstCooldown_Default;
    public readonly int CostComponent_Default;
    public readonly int CostSteel_Default;
    public readonly string DefName; // Def name of Turret itself.
    public readonly int MaxHP_Default;

    private readonly int SettingsRowIdx;
    public readonly float StoppingPower_Default;
    public readonly string TranslationKey; // Keyed translation string.
    public int Beauty;
    public string BulletDefName_Default;
    public int BulletSpeed;
    public float BurstCooldown;
    public int CostComponent;
    public int CostSteel;
    public string DefName_Default;
    public int MaxHP;
    public float StoppingPower;

    public string TranslationKey_Default;

    public TurretSettings(int settingsRowIdx, string translationKey, string defName, string bulletDefName,
        int maxHP, int beauty, float stoppingPower, int bulletSpeed, float burstCooldown, int costSteel,
        int costComponent)
    {
        SettingsRowIdx = settingsRowIdx;

        // This could/should be refactored, but I have no time right now.
        TranslationKey = TranslationKey_Default = translationKey;
        DefName = DefName_Default = defName;
        BulletDefName = BulletDefName_Default = bulletDefName;
        MaxHP = MaxHP_Default = maxHP;
        Beauty = Beauty_Default = beauty;
        StoppingPower = StoppingPower_Default = stoppingPower;
        BulletSpeed = BulletSpeed_Default = bulletSpeed;
        BurstCooldown = BurstCooldown_Default = burstCooldown;
        CostSteel = CostSteel_Default = costSteel;
        CostComponent = CostComponent_Default = costComponent;
    }

    public void DoSettingsWindowContents(ref Listing_Standard ls)
    {
        var rowRect = MoreSecuritySettings.Table.GetRowRect(SettingsRowIdx);
        var labelRect = MoreSecuritySettings.Table.GetFieldRect(0, SettingsRowIdx);

        Widgets.ThingIcon(labelRect.Replace_Width(ICON_WIDTH), DefDatabase<ThingDef>.GetNamed(DefName));
        Widgets.Label(labelRect.Add_X(ICON_WIDTH + 2), TranslationKey.Translate().CapitalizeFirst());

        MakeTextFieldNumeric(MoreSecuritySettings.Table.GetFieldRect(1, SettingsRowIdx), ref MaxHP, MIN_TURRET_HP,
            MAX_TURRET_HP);
        MakeTextFieldNumeric(MoreSecuritySettings.Table.GetFieldRect(2, SettingsRowIdx), ref Beauty, MIN_BEAUTY,
            MAX_BEAUTY);
        MakeTextFieldNumericFloat(MoreSecuritySettings.Table.GetFieldRect(3, SettingsRowIdx), ref StoppingPower,
            MIN_STOPPING_POWER, MAX_STOPPING_POWER);
        MakeTextFieldNumeric(MoreSecuritySettings.Table.GetFieldRect(4, SettingsRowIdx), ref BulletSpeed,
            MIN_BULLET_SPEED, MAX_BULLET_SPEED);
        MakeTextFieldNumericFloat(MoreSecuritySettings.Table.GetFieldRect(5, SettingsRowIdx), ref BurstCooldown,
            MIN_TURRET_SHOOT_COOLDOWN, MAX_TURRET_SHOOT_COOLDOWN);
        MakeTextFieldNumeric(MoreSecuritySettings.Table.GetFieldRect(6, SettingsRowIdx), ref CostSteel, MIN_COST,
            MAX_COST);
        MakeTextFieldNumeric(MoreSecuritySettings.Table.GetFieldRect(7, SettingsRowIdx), ref CostComponent,
            MIN_COST, MAX_COST);

        if (Mouse.IsOver(rowRect))
        {
            Widgets.DrawHighlight(rowRect);
        }

        TooltipHandler.TipRegion(rowRect.LeftHalf(), TranslationKey.Translate().CapitalizeFirst());

        ls.Gap();
    }

    public static void CreateHeaders()
    {
        for (var i = 0; i < HEADER_TRANSLATION_KEYS.Length; i++)
        {
            Widgets.Label(MoreSecuritySettings.Table.GetHeaderRect(i + 1),
                HEADER_TRANSLATION_KEYS[i].Translate().CapitalizeFirst());
        }
    }

    private void MakeTextFieldNumeric(Rect rect, ref int setting, int min, int max)
    {
        var buffer = setting.ToString();
        Widgets.TextFieldNumeric(rect, ref setting, ref buffer, min, max);
    }

    private void MakeTextFieldNumericFloat(Rect rect, ref float setting, float min, float max)
    {
        var buffer = setting.ToString();
        Widgets.TextFieldNumeric(rect, ref setting, ref buffer, min, max);
    }

    public void ExposeData()
    {
        Scribe_Values.Look(ref MaxHP, DefName + "_MaxHP", MaxHP_Default);
        Scribe_Values.Look(ref Beauty, DefName + "_Beauty", Beauty_Default);
        Scribe_Values.Look(ref StoppingPower, DefName + "_StoppingPower", StoppingPower_Default);
        Scribe_Values.Look(ref BulletSpeed, DefName + "_BulletSpeed", BulletSpeed_Default);
        Scribe_Values.Look(ref BurstCooldown, DefName + "_BurstCooldown", BurstCooldown_Default);
        Scribe_Values.Look(ref CostSteel, DefName + "_CostSteel", CostSteel_Default);
        Scribe_Values.Look(ref CostComponent, DefName + "_CostComponent", CostComponent_Default);
    }
}