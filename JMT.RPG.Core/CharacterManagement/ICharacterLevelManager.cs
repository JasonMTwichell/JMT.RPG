﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.CharacterManagement
{
    public interface ICharacterLevelManager
    {
        Task<CharacterLevelUpResult> PerformLevelUp(CharacterLevelUpContext ctx);
    }
}
