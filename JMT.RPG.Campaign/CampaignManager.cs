using JMT.RPG.Core.Contracts.Campaign;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Campaign
{
    public class CampaignManager
    {
        private ICampaignInputHandler _inputHandler;
        private ILootManager _lootMgr;
        private ICombatManager _combatMgr;
        public CampaignManager(ICombatManager combatMgr, ICampaignInputHandler inputHandler, ILootManager lootMgr)
        {
            _inputHandler = inputHandler;       
            _lootMgr = lootMgr;
            _combatMgr = combatMgr;
        }

        public async Task<CampaignResult> PerformCampaign(CampaignContext campaignCtx)
        {
            int awardedCurrency = 0;
            List<CampaignPartyItem> awardedItems = new List<CampaignPartyItem>();

            CampaignEvent[] sequencedEvents = campaignCtx.CampaignEvents.OrderBy(c => c.EventSequence).ToArray();           

            foreach (CampaignEvent campaignEvent in sequencedEvents)
            {
                // roll pre-combat dialog
                CampaignDialog[]? preCombatDialog = campaignEvent.CampaignDialog?.Where(d => d.IsPreCombat).ToArray();
                if (preCombatDialog != null) 
                {
                    await OutputDialog(preCombatDialog);
                }

                if(campaignEvent.IsCombatEvent)
                {
                    CombatEncounterContext combatEncCtx = MapCombatEncounterContext(campaignCtx, campaignEvent.EnemyParty.ToArray());

                    CombatResult combatResult = await _combatMgr.PerformCombat(combatEncCtx);

                    if(combatResult.PlayerPartyWon)
                    {
                        CampaignPartyItem[] acquiredLoot = _lootMgr.RollForLoot(campaignEvent.LootTable, campaignEvent.NumberOfLootRolls).ToArray();
                        awardedCurrency += campaignEvent.AwardedCurrency;
                        awardedItems.AddRange(acquiredLoot);
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
                    CampaignDialog[]? postCombatDialog = campaignEvent.CampaignDialog?.Where(d => !d.IsPreCombat).ToArray();
                    if(postCombatDialog != null)
                    {
                        await OutputDialog(postCombatDialog);
                    }
                }
            }

            return new CampaignResult()
            {
                CampaignCompleted = true,
                PlayerPartyCurrency = awardedCurrency,
                PlayerPartyItems = awardedItems,
            };
        }

        private async Task OutputDialog(IEnumerable<CampaignDialog> dialogLines)
        {
            CampaignInputContext[] inputEvents = dialogLines.OrderBy(d => d.DialogSequence).Select(ie => new CampaignInputContext()
            {
                Dialog = ie.Dialog
            }).ToArray();

            foreach(CampaignInputContext inputEvent in inputEvents)
            {
                await _inputHandler.GetInput(inputEvent);
            }
        }

        private CombatEncounterContext MapCombatEncounterContext(CampaignContext campCtx, IEnumerable<CampaignCharacter> enemyParty)
        {
            List<CombatantContext> combatantContexts = new List<CombatantContext>();
            CombatantContext[] playerParty = campCtx.PlayerParty.Select(c => new CombatantContext()
            {
                CombatantID = c.Id,
                Name = c.Name,
                Level = c.Level,
                IsEnemyCombatant = false,
                TotalHealth = c.TotalHealth,
                RemainingHealth = c.RemainingHealth,
                Strength = c.Strength,
                Speed = c.Speed,
                Intellect = c.Intellect,
                CombatAbilities = c.CombatAbilities.ToArray(),
            }).ToArray();

            CombatantContext[] enemyParyMapped = enemyParty.Select(e => new CombatantContext()
            {
                CombatantID = e.Id,
                Name = e.Name,
                Level = e.Level,
                IsEnemyCombatant = true,
                TotalHealth = e.TotalHealth,
                RemainingHealth = e.RemainingHealth,
                Strength = e.Strength,
                Speed = e.Speed,
                Intellect = e.Intellect,
                CombatAbilities = e.CombatAbilities.ToArray(),
            }).ToArray();

            combatantContexts.AddRange(playerParty);
            combatantContexts.AddRange(enemyParyMapped);

            CombatItem[] combatItems = campCtx.PlayerPartyItems.Select(ppi => new CombatItem()
            {
                CombatItemID = ppi.ItemID,
                CombatItemName = ppi.ItemName,
                CombatItemDescription = ppi.ItemDescription,
                CombatItemFlavorText = string.Empty,
                Effects = ppi.Effects.Select(ie => MapItemEffect(ie)).ToArray(),
            }).ToArray();

            CombatEncounterContext combatCtx = new CombatEncounterContext()
            {
                Combatants = combatantContexts,
                PlayerPartyCombatItems = combatItems,
            };

            return combatCtx;
        }

        private CombatEffect MapItemEffect(ItemEffect itemEffect)
        {
            return new CombatEffect()
            {
                EffectedAttribute = itemEffect.EffectedAttribute,
                EffectType = itemEffect.EffectType,
                Magnitude = itemEffect.Magnitude,
                MagnitudeFactor = itemEffect.MagnitudeFactor,
                ForwardEffect = itemEffect.ForwardEffect != null ? MapItemEffect(itemEffect.ForwardEffect) : null,
            };
        }
    }
}