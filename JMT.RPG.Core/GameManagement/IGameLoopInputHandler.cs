namespace JMT.RPG.Core.GameManagement
{
    public interface IGameLoopInputHandler
    {
        Task<GameLoopInputResult> GetUserActionSelection(GameLoopInputContext gameLoopInputContext);
        Task<CampaignSelectionResult> GetUserCampaignSelection(CampaignSelectionContext campaignSelectionContext);
    }
}
