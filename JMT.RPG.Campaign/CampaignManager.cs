using JMT.RPG.Core.Contracts.Campaign;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Campaign
{
    public class CampaignManager: ICampaignManager
    {
        private ICampaignInputHandler _inputHandler;
        private ICampaignLootManager _lootMgr;
        private ICombatManager _combatMgr;
        public CampaignManager(ICombatManager combatMgr, ICampaignInputHandler inputHandler, ICampaignLootManager lootMgr)
        {
            _inputHandler = inputHandler;       
            _lootMgr = lootMgr;
            _combatMgr = combatMgr;
        }

        public async Task<CampaignResult> PerformCampaign(CampaignContext campaignCtx)
        {
            int awardedCurrency = 0;
            List<CampaignPartyItem> playerPartyItems = new List<CampaignPartyItem>(campaignCtx.PlayerPartyItems);

            CampaignEvent[] sequencedEvents = campaignCtx.CampaignEvents.OrderBy(c => c.EventSequence).ToArray();           

            foreach (CampaignEvent campaignEvent in sequencedEvents)
            {
                // roll pre-combat dialog
                await OutputDialog(campaignEvent.CampaignDialog);
                
                if (campaignEvent.IsCombatEvent)
                {
                    CombatEncounterContext combatEncCtx = MapCombatEncounterContext(campaignCtx.PlayerParty.ToArray(), campaignEvent.EnemyParty.ToArray(), campaignCtx.PlayerPartyItems.ToArray());

                    CombatResult combatResult = await _combatMgr.PerformCombat(combatEncCtx);

                    if(combatResult.PlayerPartyWon)
                    {
                        CampaignPartyItem[] acquiredLoot = _lootMgr.RollForLoot(campaignEvent.LootTable, campaignEvent.NumberOfLootRolls).ToArray();
                        awardedCurrency += campaignEvent.AwardedCurrency;
                        // TODO: I DONT LIKE THIS MAPPING BACK, THE MAP FORWARD COULD HAVE ALTERED THINGS MARJORLY
                        playerPartyItems = combatResult.RemainingPlayerPartyCombatItems.Select(ci => MapToCampaignItem(ci)).ToList();
                        playerPartyItems.AddRange(acquiredLoot);
                    }
                    else
                    {
                        // trigger campaign fail event
                        return new CampaignResult()
                        {
                            CampaignCompleted = false,
                        };
                    }
                }
            }

            return new CampaignResult()
            {
                CampaignCompleted = true,
                PlayerPartyCurrency = awardedCurrency,
                PlayerPartyItems = playerPartyItems,
            };
        }


        private async Task OutputDialog(IEnumerable<CampaignEventDialog> dialogLines)
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

        private CombatEncounterContext MapCombatEncounterContext(IEnumerable<CampaignCharacter> playerParty, IEnumerable<CampaignCharacter> enemyParty, IEnumerable<CampaignPartyItem> playerPartyItems)
        {
            List<CombatantContext> combatantContexts = new List<CombatantContext>();
            CombatantContext[] playerPartyMapped = playerParty.Select(c => new CombatantContext()
            {
                CombatantID = c.CampaignCharacterID,
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
                CombatantID = e.CampaignCharacterID,
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

            combatantContexts.AddRange(playerPartyMapped);
            combatantContexts.AddRange(enemyParyMapped);

            CombatItem[] mappedCombatItems = playerPartyItems.Select(ppi => new CombatItem()
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
                PlayerPartyCombatItems = mappedCombatItems,
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

        private CampaignPartyItem MapToCampaignItem(CombatItem ci)
        {
            return new CampaignPartyItem()
            {
                ItemID = ci.CombatItemID,
                ItemName = ci.CombatItemName,
                ItemDescription = ci.CombatItemDescription,
                Effects = ci.Effects.Select(e => MapToItemEffect(e)),
            };
        }

        private ItemEffect MapToItemEffect(CombatEffect e)
        {
            return new ItemEffect()
            {
                EffectType = e.EffectType,
                EffectedAttribute = e.EffectedAttribute,
                Magnitude = e.Magnitude,
                MagnitudeFactor = e.MagnitudeFactor,
                ForwardEffect = e.ForwardEffect != null ? MapToItemEffect(e.ForwardEffect) : null,
            };
        }
    }
}