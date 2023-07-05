﻿using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Deathless
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Deathless));

    private const string EffectName = "LA.Deathless.Effect";
    private const string BuffName = "LA.Deathless.Buff";

    private const string DisplayName = "LA.Deathless.Name";
    private const string Description = "LA.Deathless.Description";
    // Soulreaver
    private const string Icon = "0d2c99456e7521f4d8002162b445ee86";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Deathless");

      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, Icon, EnhancementCost);

      var effectBuff = BuffConfigurator.New(EffectName, Guids.DeathlessEffect)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(Icon)
        .AddComponent<DeathlessComponent>()
        .AddDamageResistanceEnergy(type: DamageEnergyType.NegativeEnergy, value: 10)
        .AddDamageResistanceEnergy(type: DamageEnergyType.PositiveEnergy, value: 10)
        .Configure();

      EnchantTool.CreateEnchantWithEffect(
        enchantInfo,
        effectBuff,
        parentBuff: new(BuffName, Guids.DeathlessBuff));
    }

    [TypeId("23ca83d5-128e-4937-9fd5-646a86a3ed0d")]
    private class DeathlessComponent : UnitBuffComponentDelegate, ITargetRulebookHandler<RuleDrainEnergy>
    {
      public void OnEventAboutToTrigger(RuleDrainEnergy evt)
      {
        try
        {
          if (UnityEngine.Random.Range(0, 4) == 0)
          {
            Logger.Verbose(() => "Negating energy drain");
            evt.TargetIsImmune = true;
          }
        }
        catch (Exception e)
        {
          Logger.LogException("DeathlessComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleDrainEnergy evt) { }
    }
  }
}
