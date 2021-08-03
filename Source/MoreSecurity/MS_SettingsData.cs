using System.Collections.Generic;
using Verse;

namespace SquirtingElephant.MoreSecurity
{
    public class MS_SettingsData : ModSettings
    {
        private List<TurretSettings> _AllTurretSettings;

        private readonly TurretSettings FlameThrowerTurretSettings = new TurretSettings(
            2,
            "MS_FlamethrowerTurret",
            "SE_Turret_FlamethrowerTurret",
            "SE_Bullet_Flamethrower",
            425,
            -5,
            3.0f,
            65,
            2.5f,
            225,
            3
        );

        private readonly TurretSettings HitechMinigunTurretMKIISettings = new TurretSettings(
            6,
            "MS_HitechMinigunTurretMKII",
            "HT_MinigunTurretMKII",
            "HT_MinigunTurret_BulletMKII",
            1600,
            -2,
            1.0f,
            130,
            2.0f,
            1200,
            15
        );

        private readonly TurretSettings HitechMinigunTurretMKISettings = new TurretSettings(
            5,
            "MS_HitechMinigunTurretMKI",
            "HT_MinigunTurret",
            "HT_MinigunTurret_Bullet",
            1100,
            -2,
            0.0f,
            130,
            2.0f,
            600,
            10
        );

        private readonly TurretSettings HitechUraniumTurretSettings = new TurretSettings(
            3,
            "MS_HitechUraniumTurret",
            "Turret_SniperMKII",
            "Bullet_TurretSniperMKII",
            825,
            -2,
            6.0f,
            165,
            5.0f,
            975,
            12
        );

        public int ResearchCost_GunTurretMKII = 1250;
        public int ResearchCost_GunTurretMKIII = 6000;
        public int ResearchCost_GunTurretMKIV = 22000;
        public int ResearchCost_TrapsMKII = 1250;

        public int SandbagMKII_MaxHp = 675;
        public int SandbagMKIII_MaxHp = 1000;

        private readonly TurretSettings ShotgunTurretSettings = new TurretSettings(
            1,
            "MS_ShotgunTurret",
            "SE_ShotgunTurret",
            "Bullet_SE_ShotgunTurret",
            425,
            -5,
            3.0f,
            65,
            2.5f,
            225,
            3
        );

        private readonly TurretSettings UltraUraniumTurretSettings = new TurretSettings(
            4,
            "MS_UltraUraniumTurret",
            "Turret_SniperMKIII",
            "Bullet_TurretSniperMKIII",
            1550,
            -2,
            12.0f,
            160,
            2.0f,
            3550,
            1
        );

        public List<TurretSettings> AllTurretSettings
        {
            get => _AllTurretSettings ?? new List<TurretSettings>
            {
                ShotgunTurretSettings, FlameThrowerTurretSettings, HitechUraniumTurretSettings,
                UltraUraniumTurretSettings, HitechMinigunTurretMKISettings, HitechMinigunTurretMKIISettings
            };
            set => _AllTurretSettings = value;
        }

        public override void ExposeData()
        {
            // Research
            Scribe_Values.Look(ref ResearchCost_TrapsMKII, "ResearchCost_TrapsMKII", 1250);
            Scribe_Values.Look(ref ResearchCost_GunTurretMKII, "ResearchCost_GunTurretMKII", 1250);
            Scribe_Values.Look(ref ResearchCost_GunTurretMKIII, "ResearchCost_GunTurretMKIII", 6000);
            Scribe_Values.Look(ref ResearchCost_GunTurretMKIV, "ResearchCost_GunTurretMKIV", 22000);

            // Sandbags
            Scribe_Values.Look(ref SandbagMKII_MaxHp, "SandbagMKII_MaxHP", 675);
            Scribe_Values.Look(ref SandbagMKIII_MaxHp, "SandbagMKIII_MaxHP", 1000);

            // Turrets
            AllTurretSettings.ForEach(s => s.ExposeData());

            base.ExposeData();
        }
    }
}