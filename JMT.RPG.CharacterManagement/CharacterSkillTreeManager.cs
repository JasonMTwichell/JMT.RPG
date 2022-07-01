using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMT.RPG.Core.CharacterManagement;

namespace JMT.RPG.CharacterManagement
{
    public class CharacterSkillTreeManager : ICharacterSkillManager
    {
        private ICharacterSkillInputManager _inputHandler;
        public CharacterSkillTreeManager(ICharacterSkillInputManager inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public async Task<UnlockCharacterSkillResult> UnlockCharacterSkill(UnlockCharacterSkillContext ctx)
        {
            CharacterSkillInputContext req = new CharacterSkillInputContext()
            {
                CharacterID = ctx.CharacterID,
                AvailableCurrency = ctx.Currency,
                CharacterSkillCopse = ctx.CharacterSkillCopse,
            };

            CharacterSkillInputResult inputResult = await _inputHandler.GetInput(req);

            if(inputResult.PurchasedSkill == null)
            {
                return new UnlockCharacterSkillResult();
            }

            // verify that the player has the right learn that skill
            CharacterSkillTree tree = ctx.CharacterSkillCopse.SkillTrees.First(st => st.Skills.Contains(inputResult.PurchasedSkill));
            bool canPurchase = (tree.Skills.Where(skill => skill.Tier < inputResult.PurchasedSkill.Tier).All(skill => skill.IsLearned) 
                                    && inputResult.CurrencySpent <= ctx.Currency);
            if(!canPurchase)
            {
                throw new InvalidSkillSelectionException(String.Format("Cannot purchase selected skill."));
            }
            else
            {
                return new UnlockCharacterSkillResult()
                {
                    SkillWasPurchased = true,
                    CurrencySpent = inputResult.CurrencySpent,
                    LearnedSkill = inputResult.PurchasedSkill,
                };
            }
        }
    }
}
