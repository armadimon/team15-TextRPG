﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15TextRPG.Source.Combat;

namespace _15TextRPG.Source
{
    public enum Job
    {
        Nomad = 1,
        Gutterchild,
        Enterprise
    }

    public class GameData
    {
        public Player Player { get; set; }
        public List<Enemy> enemies;
        public List<ChapterData> Chapters { get; set; }
        public ChapterData? CurrentChapter { get; set; }


        public static readonly Dictionary<Job, string> JobDescriptions = new Dictionary<Job, string>
        {
            { Job.Nomad, "노마드" },
            { Job.Gutterchild, "부랑아" },
            { Job.Enterprise, "기업" }
        };


        public List<Quest> Quests { get; set; }

        public GameData()
        {
            Player = new Player("Default",Job.Nomad);
            Chapters = new List<ChapterData>();

            ChapterData chapter1 = new ChapterData("Chapter1");
            chapter1.InitailizeChapter1();

            enemies = new List<Enemy>()
            {
                new Enemy("Omnic_A", 1, "abc d e f ", 5, 20, 30, new List<Item>()),
                new Enemy("Omnic_B", 1, "abc d e f ", 5, 20, 30, new List<Item>()),
                new Enemy("Omnic_C", 1, "abc d e f ", 5, 20, 30, new List<Item>()),
            };
            Chapters = new List<ChapterData> { chapter1 };
            CurrentChapter = chapter1;
        }

        public void ChangeChapter(string chapterName)
        {
            var newChapter = Chapters.Find(c => c.Name == chapterName);
            if (newChapter != null)
            {
                CurrentChapter = newChapter;
                Console.Clear();
                Console.WriteLine($"{chapterName} 챕터로 변경되었습니다.");
                Console.ReadLine();
            }
        }
    }
}

