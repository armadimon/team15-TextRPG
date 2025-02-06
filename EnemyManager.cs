using _15TextRPG.Source.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG
{
    public class EnemyManager
    {
        private static EnemyManager? instance;
        private List<Enemy> enemyList; // 적 목록

        private EnemyManager()
        {
            enemyList = new List<Enemy>()
            {
                new Enemy("옴닉a", 1, "옴닉a이다.", 5, 2, 30, new List<Item>()),
                new Enemy("옴닉b", 1, "옴닉b이다.", 5, 2, 30, new List<Item>()),
                new Enemy("옴닉c", 1, "옴닉c이다.", 5, 2, 30, new List<Item>()),
            };
        }

        public static EnemyManager Instance // 싱글톤
        {
            get
            {
                if (instance == null)
                    instance = new EnemyManager();
                return instance;
            }
        }

        public Enemy GetRandomEnemy()
        {
            Random random = new Random();
            int index = random.Next(enemyList.Count);
            return new Enemy(enemyList[index]);
        }
    }
}
