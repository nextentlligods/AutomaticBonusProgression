﻿using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Enchantments;
using AutomaticBonusProgression.UI.Attunement;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using ModMenu.Window;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryArmor));

    private const string LegendaryArmorName = "LegendaryArmor";
    private const string LegendaryArmorDisplayName = "LegendaryArmor.Name";
    private const string LegendaryArmorDescription = "LegendaryArmor.Description";
    private const string LegendaryArmorAbility = "LegendaryArmor.Ability";
    internal const string LegendaryArmorAbilityDescription = "LegendaryArmor.Ability.Description";

    private const string LegendaryShieldName = "LegendaryShield";
    private const string LegendaryShieldDisplayName = "LegendaryShield.Name";
    internal const string LegendaryShieldDescription = "LegendaryShield.Description";
    private const string LegendaryShieldAbility = "LegendaryShield.Ability";

    internal static BlueprintFeature Configure()
    {
      Logger.Log("Configuring Legendary Armor");

      var ability = AbilityConfigurator.New(LegendaryArmorAbility, Guids.LegendaryArmorAbility)
        .SetDisplayName(LegendaryArmorDisplayName)
        .SetDescription(LegendaryArmorAbilityDescription)
        .AddAbilityEffectRunAction(ActionsBuilder.New().Add<ShowAttunement>())
        .Configure();
      //var ability = ActivatableAbilityConfigurator.New(LegendaryArmorAbility, Guids.LegendaryArmorAbility)
      //  .SetDisplayName(LegendaryArmorDisplayName)
      //  .SetDescription(LegendaryArmorAbilityDescription)
      //  //.SetIcon()
      //  .SetDeactivateImmediately()
      //  .SetActivationType(AbilityActivationType.Immediately)
      //  .SetActivateWithUnitCommand(CommandType.Free)
      //  .AddActivatableAbilityVariants(
      //    variants: 
      //      new()
      //      {
      //        Guids.BalancedArmorAbility,

      //        Guids.BolsteringAbility,

      //        Guids.BrawlingAbility,

      //        Guids.ChampionAbility,

      //        Guids.CreepingAbility,

      //        Guids.DastardAbility,

      //        Guids.DeathlessAbility,

      //        Guids.DeterminationAbility,

      //        Guids.ExpeditiousAbility,

      //        Guids.FortificationAbility,
      //        Guids.ImprovedFortificationAbility,
      //        Guids.GreaterFortificationAbility,

      //        Guids.GhostArmorAbility,

      //        Guids.InvulnerabilityAbility,

      //        Guids.MartyringAbility,

      //        Guids.RallyingAbility,

      //        Guids.RighteousAbility,

      //        Guids.ShadowArmorAbility,
      //        Guids.ImprovedShadowArmorAbility,
      //        Guids.GreaterShadowArmorAbility,

      //        Guids.SpellResistance13Ability,
      //        Guids.SpellResistance16Ability,
      //        Guids.SpellResistance19Ability,
      //        Guids.SpellResistance22Ability,
      //      })
      //  .AddActivationDisable()
      //  .Configure();

      var shieldAbility = ActivatableAbilityConfigurator.New(LegendaryShieldAbility, Guids.LegendaryShieldAbility)
        .SetDisplayName(LegendaryShieldDisplayName)
        .SetDescription(LegendaryShieldDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants: 
            new()
            {
              Guids.BashingAbility,

              Guids.BlindingAbility,

              Guids.BolsteringShieldAbility,

              Guids.FortificationShieldAbility,
              Guids.ImprovedFortificationShieldAbility,
              Guids.GreaterFortificationShieldAbility,

              Guids.GhostArmorShieldAbility,

              Guids.RallyingShieldAbility,

              Guids.ReflectingAbility,

              Guids.SpellResistance13ShieldAbility,
              Guids.SpellResistance16ShieldAbility,
              Guids.SpellResistance19ShieldAbility,
              Guids.SpellResistance22ShieldAbility,

              Guids.WyrmsbreathAbility,
            })
        .AddActivationDisable()
        .Configure();

      return FeatureConfigurator.New(LegendaryArmorName, Guids.LegendaryArmor)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryArmorDisplayName)
        .SetDescription(LegendaryArmorDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddFacts(new() { ability })
        .AddComponent(
          new AttunementBuffsComponent(
      #region Energy Resistance
            // 10 Armor
            Guids.AcidResist10Buff,
            Guids.ColdResist10Buff,
            Guids.ElectricityResist10Buff,
            Guids.FireResist10Buff,
            Guids.SonicResist10Buff,

            // 20 Armor
            Guids.AcidResist20Buff,
            Guids.ColdResist20Buff,
            Guids.ElectricityResist20Buff,
            Guids.FireResist20Buff,
            Guids.SonicResist20Buff,

            // 30 Armor
            Guids.AcidResist30Buff,
            Guids.ColdResist30Buff,
            Guids.ElectricityResist30Buff,
            Guids.FireResist30Buff,
            Guids.SonicResist30Buff
      #endregion
          ))
        .Configure();
    }
  }
}
