﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    class CollectItemQuest : IQuestObject
    {
        private string itemId;
        private int requiredAmount;
        private int currentAmount;

        public bool IsCompleted => currentAmount >= requiredAmount;

        public CollectItemQuest(string itemName, int requiredAmount)
        {
            this.itemId = itemName;
            this.requiredAmount = requiredAmount;
            this.currentAmount = 0;
        }

        public void UpdateProgress()
        {
            currentAmount++;
            Console.WriteLine($"아이템 {itemId} 수집 {currentAmount}/{requiredAmount}");
            Console.ReadLine();
        }
    }

    class KillEnemyQuest : IQuestObject
    {
        private string enemyName;
        private int requiredKills;
        private int currentKills;

        public bool IsCompleted => currentKills >= requiredKills;

        public KillEnemyQuest(string enemyName, int requiredKills)
        {
            this.enemyName = enemyName;
            this.requiredKills = requiredKills;
            this.currentKills = 0;
        }

        public void UpdateProgress()
        {
            currentKills++;
            Console.WriteLine($"{enemyName} 처치 {currentKills}/{requiredKills}");
            Console.ReadLine();
        }
    }
}
