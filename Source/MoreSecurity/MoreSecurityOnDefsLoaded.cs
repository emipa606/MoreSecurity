using SquirtingElephant.Helpers;
using Verse;

namespace SquirtingElephant.MoreSecurity
{
    [StaticConstructorOnStartup] // This makes the static constructor get called AFTER defs are loaded.
    public class MoreSecurityOnDefsLoaded
    {
        static MoreSecurityOnDefsLoaded()
        {
            ApplySettingsToDefs();

#if debug
            Log.Message("MS_ModName".Translate() + ": Settings applied");
#endif
        }

        public static void ApplySettingsToDefs()
        {
            ApplyResearch();
            ApplySandbags();
            ApplyTurrets();
        }

        private static void ApplyResearch()
        {
            Utils.SetResearchBaseCost("MoreTrapsMKII", MoreSecuritySettings.Settings.ResearchCost_TrapsMKII);
            Utils.SetResearchBaseCost("MoreTurretsMKII", MoreSecuritySettings.Settings.ResearchCost_GunTurretMKII);
            Utils.SetResearchBaseCost("MoreTurretsMKIII", MoreSecuritySettings.Settings.ResearchCost_GunTurretMKIII);
            Utils.SetResearchBaseCost("MoreTurretsMKIV", MoreSecuritySettings.Settings.ResearchCost_GunTurretMKIV);
        }

        private static void ApplySandbags()
        {
            Utils.SetThingMaxHp("Sandbags_MKII", MoreSecuritySettings.Settings.SandbagMKII_MaxHp);
            Utils.SetThingMaxHp("Sandbags_MKIII", MoreSecuritySettings.Settings.SandbagMKIII_MaxHp);
        }

        private static void ApplyTurrets()
        {
            MoreSecuritySettings.Settings.AllTurretSettings.ForEach(ApplyTurretSettings);
        }

        private static void ApplyTurretSettings(TurretSettings turretSettings)
        {
            Utils.SetThingMaxHp(turretSettings.DefName, turretSettings.MaxHP);
            Utils.SetThingMaxBeauty(turretSettings.DefName, turretSettings.Beauty);
            Utils.SetThingTurretBurstCooldown(turretSettings.DefName, turretSettings.BurstCooldown);

            var bulletDef = Utils.GetDefByDefName<ThingDef>(turretSettings.BulletDefName);
            if (bulletDef != null)
            {
                bulletDef.projectile.stoppingPower = turretSettings.StoppingPower;
                bulletDef.projectile.speed = turretSettings.BulletSpeed;
            }

            Utils.SetThingSteelCost(turretSettings.DefName, turretSettings.CostSteel);
            Utils.SetThingComponentCost(turretSettings.DefName, turretSettings.CostComponent);
        }
    }
}