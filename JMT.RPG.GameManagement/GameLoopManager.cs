using JMT.RPG.Core.Campaign;
using JMT.RPG.Core.CharacterManagement;
using JMT.RPG.Core.GameManagement;

namespace JMT.RPG.GameManagement
{
    public class GameLoopManager : IGameLoopManager
    {
        private IGameLoopInputHandler _gameLoopInputHandler;
        private ICampaignManager _campMgr;
        private ICharacterLevelManager _levelMgr;
        private ICharacterSkillManager _skillMgr;
        private IShopManager _shopMgr;
        private IItemCraftingManager _craftingMgr;
        public GameLoopManager(IGameLoopInputHandler gameLoopInputHandler, ICampaignManager campMgr, ICharacterLevelManager levelMgr, 
            ICharacterSkillManager skillMgr, IShopManager shopMgr, IItemCraftingManager craftingMgr)
        {
            _gameLoopInputHandler = gameLoopInputHandler;
            _campMgr = campMgr;
            _levelMgr = levelMgr;
            _skillMgr = skillMgr;
            _shopMgr = shopMgr;
            _craftingMgr = craftingMgr;
        }

        public async Task<GameLoopResult> PerformGameLoop(GameLoopContext context)
        {
            bool performGameLoop = true;
            while(performGameLoop)
            {
                // create object used to save player game state then return to caller to save
                GameLoopInputContext gameLoopInputContext = GetGameLoopInputContext();

                GameLoopInputResult uInput = await _gameLoopInputHandler.GetUserActionSelection(gameLoopInputContext);
                if(uInput.SelectedAction == GameAction.QUIT)
                {
                    performGameLoop = false;
                }
                else if (uInput.SelectedAction == GameAction.SHOP)
                {
                    ShopTransactionContext shopCtx = new ShopTransactionContext()
                    {

                    };

                    ShopTransactionResult shopResult = await _shopMgr.PerformShopTransaction(shopCtx);
                }
                else if (uInput.SelectedAction == GameAction.CRAFT)
                {
                    ItemCraftingContext craftingCtx = new ItemCraftingContext()
                    {

                    };

                    ItemCraftingResult craftingResult = await _craftingMgr.CraftItem(craftingCtx);
                }
                else if (uInput.SelectedAction == GameAction.LEVELUP)
                {
                    CharacterLevelUpContext levelUpCtx = new CharacterLevelUpContext()
                    {

                    };

                    CharacterLevelUpResult levelUpResult = await _levelMgr.PerformLevelUp(levelUpCtx);
                }
                else if (uInput.SelectedAction == GameAction.LEARN_SKILL)
                {
                    UnlockCharacterSkillContext skillCtx = new UnlockCharacterSkillContext()
                    {

                    };

                    UnlockCharacterSkillResult skillResult = await _skillMgr.UnlockCharacterSkill(skillCtx);
                }
                else if (uInput.SelectedAction == GameAction.EQUIP_CHARACTER)
                {
                    // TODO: stub this out
                }
                else if (uInput.SelectedAction == GameAction.CAMPAIGN)
                {
                    CampaignSelectionContext campaignSelCtx = new CampaignSelectionContext()
                    {

                    };

                    CampaignSelectionResult campaignSelResult = await _gameLoopInputHandler.GetUserCampaignSelection(campaignSelCtx);

                    CampaignContext campCtx = new CampaignContext()
                    {

                    };

                    CampaignResult campResult = await _campMgr.PerformCampaign(campCtx);
                }

                throw new UnsupportedGameLoopActionException();
            }

            return new GameLoopResult();
        }

        private GameLoopInputContext GetGameLoopInputContext()
        {
            return new GameLoopInputContext()
            {
                UserActionOptions = new UserActionOption[]
                {
                    new UserActionOption()
                    {
                        OptionID = GameAction.QUIT,
                        OptionTitle = "SAVE AND QUIT",
                    },
                    new UserActionOption()
                    {
                        OptionID = GameAction.SHOP,
                        OptionTitle = "SHOP",
                    },
                    new UserActionOption()
                    {
                        OptionID = GameAction.CRAFT,
                        OptionTitle = "CRAFT",
                    },
                    new UserActionOption()
                    {
                        OptionID = GameAction.LEVELUP,
                        OptionTitle = "LEVEL UP",
                    },
                    new UserActionOption()
                    {
                        OptionID = GameAction.LEARN_SKILL,
                        OptionTitle = "LEARN SKILLS",
                    },
                    new UserActionOption()
                    {
                        OptionID = GameAction.EQUIP_CHARACTER,
                        OptionTitle = "EQUIP CHARACTER",
                    },
                    new UserActionOption()
                    {
                        OptionID = GameAction.CAMPAIGN,
                        OptionTitle = "CAMPAIGN",
                    },
                }
            };
        }
    }
}
