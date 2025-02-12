using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.Combat;

namespace _15TextRPG.Source.State
{
    public class BattleMenuState : IGameState
    {

        public EnemyTrigger? Enemy { get; set; }

        public BattleMenuState()
        {

        }

        public BattleMenuState(EnemyTrigger enemy)
        {
            Enemy = enemy;
        }

        public void DisplayMenu()
        {
        }

        public void HandleInput()
        {
            GameManager.Instance.BattleManager.InBattle(Enemy);
            //GameManager.Instance.ChangeState(new ExploreState(GameManager.Instance.GameData.CurrentChapter.CurrentStage.Name));
        }        
    }
}