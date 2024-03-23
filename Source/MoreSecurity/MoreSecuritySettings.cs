using Mlie;
using SquirtingElephant.Helpers;
using UnityEngine;
using Verse;

namespace SquirtingElephant.MoreSecurity;

public class MoreSecuritySettings : Mod
{
    private const float ROW_HEIGHT = 32f;
    private const float COL_WIDTH = 85f;

    private const int MAX_RESEARCH_COST = 1000000;
    private const int MIN_SANDBAG_HP = 1;
    private const int MAX_SANDBAG_HP = 100000;
    public static MS_SettingsData Settings;
    private static string currentVersion;

    public static readonly TableData Table = new TableData(new Vector2(0f, 250f), new Vector2(10f, 10f),
        [150f, COL_WIDTH],
        [36f, ROW_HEIGHT],
        8, 7);

    private Vector2 ScrollPosition = Vector2.zero;
    private Rect ViewRect;

    /// <summary>
    ///     A mandatory constructor.
    /// </summary>
    public MoreSecuritySettings(ModContentPack content) : base(content)
    {
        Settings = GetSettings<MS_SettingsData>();
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    /// <summary>
    ///     Mod Settings GUI.
    /// </summary>
    public override void DoSettingsWindowContents(Rect inRect)
    {
        var ls = new Listing_Standard();
        ViewRect = inRect.ContractedBy(10f);
        ViewRect.height = 275f + (Settings.AllTurretSettings.Count * 50f);
        Widgets.BeginScrollView(inRect, ref ScrollPosition, ViewRect);
        ls.Begin(ViewRect);

        var lastRect = ls.Label("MS_ResearchCosts".Translate());

        if (currentVersion != null)
        {
            lastRect.y += 50f;
            GUI.contentColor = Color.gray;
            Widgets.Label(lastRect, "MS_CurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        ls.Gap(2);

        var bufferResearchCost_TrapsMKII = Settings.ResearchCost_TrapsMKII.ToString();
        ls.TextFieldNumericLabeled("MS_TrapMKIIResearchCost".Translate(), ref Settings.ResearchCost_TrapsMKII,
            ref bufferResearchCost_TrapsMKII, 0, MAX_RESEARCH_COST);

        var bufferResearchCost_MoreTurretsMKII = Settings.ResearchCost_GunTurretMKII.ToString();
        ls.TextFieldNumericLabeled("MS_TurretsMKIIResearchCost".Translate(),
            ref Settings.ResearchCost_GunTurretMKII, ref bufferResearchCost_MoreTurretsMKII, 0, MAX_RESEARCH_COST);

        var bufferResearchCost_MoreTurretsMKIII = Settings.ResearchCost_GunTurretMKIII.ToString();
        ls.TextFieldNumericLabeled("MS_TurretsMKIIIResearchCost".Translate(),
            ref Settings.ResearchCost_GunTurretMKIII, ref bufferResearchCost_MoreTurretsMKIII, 0,
            MAX_RESEARCH_COST);

        var bufferResearchCost_MoreTurretsMKIV = Settings.ResearchCost_GunTurretMKIV.ToString();
        ls.TextFieldNumericLabeled("MS_TurretsMKIVResearchCost".Translate(),
            ref Settings.ResearchCost_GunTurretMKIV, ref bufferResearchCost_MoreTurretsMKIV, 0, MAX_RESEARCH_COST);

        ls.GapLine();

        ls.Label("MS_Sandbags".Translate());
        ls.Gap(2);

        var bufferSandbagsMKII_Hp = SeMath.RoundToNearestMultiple(Settings.SandbagMKII_MaxHp, 5).ToString();
        ls.TextFieldNumericLabeled("MS_SandbagsMKII".Translate() + "MS_Sandbags_MaxHP".Translate(),
            ref Settings.SandbagMKII_MaxHp, ref bufferSandbagsMKII_Hp, MIN_SANDBAG_HP, MAX_SANDBAG_HP);

        var bufferSandbagsMKIII_Hp = SeMath.RoundToNearestMultiple(Settings.SandbagMKIII_MaxHp, 5).ToString();
        ls.TextFieldNumericLabeled("MS_SandbagsMKIII".Translate() + "MS_Sandbags_MaxHP".Translate(),
            ref Settings.SandbagMKIII_MaxHp, ref bufferSandbagsMKIII_Hp, MIN_SANDBAG_HP, MAX_SANDBAG_HP);

        // Note.
        ls.GapLine();
        ls.Label("MS_NeedRebuild".Translate());

        // Turrets.
        TurretSettings.CreateHeaders();
        Settings.AllTurretSettings.ForEach(s => s.DoSettingsWindowContents(ref ls));

        MoreSecurityOnDefsLoaded.ApplySettingsToDefs();


        ls.End();
        Widgets.EndScrollView();
        base.DoSettingsWindowContents(inRect);
    }


    /// <summary>
    ///     This is the name as how it'll appear ain the game's settings menu.
    /// </summary>
    public override string SettingsCategory()
    {
        return "MS_SettingsCategory".Translate();
    }
}