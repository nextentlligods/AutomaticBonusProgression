﻿using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Patches;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.ActivatableAbilities;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class BalancedArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(BalancedArmor));

    private const string BalancedArmorName = "LegendaryArmor.Balanced";
    private const string BuffName = "LegendaryArmor.Balanced.Buff";
    private const string AbilityName = "LegendaryArmor.Balanced.Ability";

    private const string DisplayName = "LegendaryArmor.Balanced.Name";
    private const int Enhancement = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Balanced Armor");

      var balancedFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ArcaneArmorBalancedFeature, EnhancementType.Armor, Enhancement);

      var enchant = ArmorEnchantmentRefs.ArcaneArmorBalancedEnchant.Reference.Get();
      var buff = BuffConfigurator.New(BuffName, Guids.BalancedArmorBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .AddComponent(balancedFeature.GetComponent<CMDBonusAgainstManeuvers>())
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, Enhancement))
        .Configure();

      var ability = ActivatableAbilityConfigurator.New(AbilityName, Guids.BalancedArmorAbility)
        .SetDisplayName(DisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .SetBuff(buff)
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .SetHiddenInUI()
        .AddComponent(new EnhancementEquivalentRestriction(EnhancementType.Armor, Enhancement))
        .Configure();

      return FeatureConfigurator.New(BalancedArmorName, Guids.BalancedArmor)
        .SetIsClassFeature()
        .SetDisplayName(DisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .AddFacts(new() { ability })
        .Configure();
    }
  }
}
