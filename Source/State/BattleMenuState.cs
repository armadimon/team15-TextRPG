using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.Combat;

namespace _15TextRPG.Source.State
{
    public class BattleMenuState() : IGameState
    {
        public void DisplayMenu(GameManager gameManager)
        {
            gameManager.BattleManager.InBattle(gameManager);
        }

        public void HandleInput(GameManager gameManager)
        {
            gameManager.ChangeState(new ExploreState(gameManager.GameData.CurrentChapter.CurrentStage.Name));
        }        
    }
}