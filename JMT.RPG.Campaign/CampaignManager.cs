using JMT.RPG.Combat;
using JMT.RPG.Core.Game;

namespace JMT.RPG.Campaign
{
    public class CampaignManager
    {
        private IInputHandler _inputHandler;
        private ILootManager _lootMgr;
        public CampaignManager(IInputHandler inputHandler, ILootManager lootMgr)
        {
            _inputHandler = inputHandler;       
            _lootMgr = lootMgr;
        }
        public async Task<CampaignResult> PerformCampaign(CampaignContext campaignCtx)
        {
            CampaignEvent[] sequencedEvents = campaignCtx.CampaignEvents.OrderBy(c => c.EventSequence).ToArray();
            //Combatant[] playerParty = BuildPlayerCombatants(campaignCtx.PlayerParty.PlayerParty, campaignCtx.PlayerParty.CampaignPartyItems).ToArray();

            foreach (CampaignEvent campaignEvent in sequencedEvents)
            {
                // roll pre-combat dialog
                await OutputDialog(campaignEvent.CampaignDialog.Where(d => d.IsPreCombat).ToArray());

                if(campaignEvent.IsCombatEvent)
                {
                    ICombatManager combatMgr = new CombatManager(campaignCtx.PlayerParty, campaignEvent.EnemyParty);
                    CombatResult combatResult = await combatMgr.PerformCombat();

                    if(combatResult.PlayerPartyWon)
                    {
                        CampaignLoot[] acquiredLoot = _lootMgr.RollForLoot(campaignEvent.LootTable, campaignEvent.NumberOfLootRolls).ToArray();
                        campaignCtx.PlayerPartyItems.AddRange(acquiredLoot.SelectMany(al => al.CampaignPartyItems).ToArray());
                        campaignCtx.PlayerPartyCurrency += campaignEvent.AwardedCurrency;                        
                    }
                    else
                    {
                        // trigger campaign fail event
                        return new CampaignResult()
                        {
                            CampaignCompleted = false,
                        };
                    }

                    // roll post combat dialog
                    await OutputDialog(campaignEvent.CampaignDialog.Where(d => !d.IsPreCombat).ToArray());
                }
            }

            return new CampaignResult()
            {
                CampaignCompleted = true,
                PlayerPartyCurrency = campaignCtx.PlayerPartyCurrency,
                PlayerPartyItems = campaignCtx.PlayerPartyItems,
            };
        }

        private async Task OutputDialog(IEnumerable<CampaignDialog> dialogLines)
        {
            DialogInputEvent[] inputEvents = dialogLines.OrderBy(d => d.DialogSequence).Select(ie => new DialogInputEvent()
            {
                DialogLine = ie.Dialog
            }).ToArray();

            foreach(DialogInputEvent inputEvent in inputEvents)
            {
                await _inputHandler.GetInput(inputEvent);
            }
        }

        private IEnumerable<Combatant> BuildPlayerCombatants(IEnumerable<CampaignCharacter> characters, IEnumerable<CampaignPartyItem> campaignItems)
        {
            // this can be shared across all chars
            IInputHandler combatInputHandler = new CombatInputHandler();

            // accessible to all party members
            CombatAbility[] itemAbilities = campaignItems.Select(ci => new CombatAbility()
            {
                CombatAbilityId = string.Format("ITEM_{0}",ci.ItemID),
                Cooldown = 0,
                Name = ci.ItemName,
                Description = ci.ItemDescription,
                RemainingCooldown = 0,                
                Effects = ci.Effects.Select(e => BuildCombatEffect(e)),
            }).ToArray();

            foreach (CampaignCharacter character in characters)
            {
                CombatAbility[] combatAbilities = itemAbilities.Concat(character.CombatAbilities).ToArray();
                ICombatAbilityManager abilityMgr = new CombatAbilityManager(combatAbilities);
                ICombatantStateManager stateMgr = new CombatantStateManager()
                {
                    TotalHealth = character.TotalHealth,
                    RemainingHealth = character.RemainingHealth,
                    Strength = character.Strength,
                    Intellect = character.Intellect,
                    Speed = character.Speed,
                };

                Combatant combatant = new Combatant(combatInputHandler, abilityMgr, stateMgr)
                {
                    Id = character.Id,
                    Name = character.Name,
                };

                yield return combatant;
            }
        }

        private CombatEffect BuildCombatEffect(Effect effect)
        {
            CombatEffect combatEffect = new CombatEffect()
            {
                EffectedAttribute = effect.EffectedAttribute,
                EffectType = effect.EffectType,
                Magnitude = effect.Magnitude,
                MagnitudeFactor = effect.MagnitudeFactor,
                ForwardEffect = effect.ForwardEffect != null ? BuildCombatEffect(effect.ForwardEffect) : null,
            };

            return combatEffect;
        }
    }
}