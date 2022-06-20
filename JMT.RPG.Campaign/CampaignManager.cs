using JMT.RPG.Core.Contracts.Combat;
using JMT.RPG.Core.Interfaces;

namespace JMT.RPG.Campaign
{
    public class CampaignManager
    {
        private IInputHandler _inputHandler;
        private ILootManager _lootMgr;
        private ICombatManager _combatMgr;
        public CampaignManager(ICombatManager combatMgr, IInputHandler inputHandler, ILootManager lootMgr)
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
                    CombatResult combatResult = await _combatMgr.PerformCombat();

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
    }
}