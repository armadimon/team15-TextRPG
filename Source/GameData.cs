using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{

    public class GameData
    {
        public Player Player { get; set; }
        public List<ChapterData> Chapters { get; set; }
        public ChapterData CurrentChapter { get; set; }
        public List<IMonster> monsters = new List<IMonster>();

        public GameData()
        {
            Player = new Player("Default");
            Chapters = new List<ChapterData>();


            ChapterData chapter1 = new ChapterData("Chapter1");
            chapter1.InitailizeChapter1();
            monsters.Add(new Hana());


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

