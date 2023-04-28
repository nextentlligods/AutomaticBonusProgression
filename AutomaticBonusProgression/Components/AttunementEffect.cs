﻿using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Components
{
  /// <summary>
  /// Base class for applying enchantment effects. Implementing classes indicate whether the effect buff should be applied,
  /// this handles actually applying and removing it.
  /// </summary>
  [TypeId("9f0122b2-7afe-4289-9104-164a6b6e3671")]
  internal abstract class AttunementEffect : UnitBuffComponentDelegate<AttunementEffect.ComponentData>, IUnitEquipmentHandler
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AttunementEffect));

    private readonly BlueprintBuffReference EffectBuff;

    protected AttunementEffect(BlueprintBuffReference effectBuff)
    {
      EffectBuff = effectBuff;
    }

    public override void OnActivate()
    {
      try
      {
        ApplyEffect();
      }
      catch (Exception e)
      {
        Logger.LogException("AttunementEffect.OnActivate", e);
      }
    }

    public override void OnDeactivate()
    {
      try
      {
        if (Data.AppliedBuff is not null)
        {
          Logger.Verbose(() => $"Deactivating {Data.AppliedBuff.Name}");
          Data.AppliedBuff.Remove();
          Data.AppliedBuff = null;
        }
      }
      catch (Exception e)
      {
        Logger.LogException("AttunementEffect.OnDeactivate", e);
      }
    }

    public void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem)
    {
      try
      {
        if (slot.Owner != Owner)
          return;

        if (!AffectsSlot(slot))
          return;

        ApplyEffect();
      }
      catch (Exception e)
      {
        Logger.LogException("ArmorAttunement.HandleEquipmentSlotUpdated", e);
      }
    }

    private void ApplyEffect()
    {
      var buffApplied = Data.AppliedBuff is not null;
      var shouldApply = IsAvailable(Owner);

      if (buffApplied && !shouldApply)
      {
        Logger.Verbose(() => $"Unsupported enchantment, removing {Data.AppliedBuff.Name}");
        Data.AppliedBuff.Remove();
        Data.AppliedBuff = null;
      }
      else if (!buffApplied && shouldApply)
      {
        var buff = EffectBuff.Get();
        Logger.Verbose(() => $"Applying enchantment {buff.Name}");
        Data.AppliedBuff = Owner.AddBuff(buff, Context);
      }
    }

    protected abstract bool AffectsSlot(ItemSlot slot);
    public abstract bool IsAvailable(UnitDescriptor unit);

    /// <summary>
    /// Returns a comma separated list of requirement strings, or an empty string if there are none
    /// </summary>
    public virtual string GetRequirements()
    {
      return string.Empty;
    }

    internal class ComponentData
    {
      internal Buff AppliedBuff;
    }
  }
}
