using System.Diagnostics;

namespace _15TextRPG.Source.State
{

    class TextRPG
    {
        private static void Main()
        {
            GameManager gameManager = new GameManager();

            gameManager.Run();
        }
    }



    public class GameManager
    {
        private IGameState currentState;
        private bool isRunning = true;
        public Player Player { get; private set; }
        public Enemy Enemy { get; set; }

        public GameManager()
        {
            Player = new Player("Agent Ahn");
            currentState = new MainMenuState();
        }

        public void Run()
        {
            while (isRunning)
            {
                currentState.DisplayMenu(this);
                Console.Write("\n원하시는 행동을 입력해주세요. >> ");
                string input = Console.ReadLine() ?? "";
                currentState.HandleInput(this, input);
            }
        }

        public void ChangeState(IGameState newState)
        {
            currentState = newState;
        }

        public void QuitGame()
        {
            Console.WriteLine("게임을 종료합니다.");
            isRunning = false;
        }

        public void MonsterSpawn(int monsternum)
        {
            Enemy = new Enemy();
            for (int i = 0; i < monsternum; i++)
            {
                Enemy.targetmonster.Add(new EnemyReal(i));
            }
        }

        public void AtkPhase()
        {
            Console.Clear();
            for (int i = 0; i < 4; i++)
            {
                Enemy.EnemyStat(i, 0 + i * 20);
            }
            Player.BattleStat();
            Console.SetCursorPosition(0, 14);
            Console.Write("대상을 지정해주세요.");
            string input = Console.ReadLine();
            Player.TargetEnemy(input, Enemy);

        }
        public void DefPhase()
        {

        }       
    }


    public class Player
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AttackDamage { get; set; }
        public int DefensePoint { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int ClearCount { get; set; }
        public Item? Weapon { get; set; }
        public Item? Armor { get; set; }
        public string[] Skill { get; set; }

        public List<Item> inventory;

        Enemy Enemy { get; set; }

        public Player(string name)
        {
            Name = name;
            Level = 1;
            Description = "수습 해커";
            AttackDamage = 10;
            DefensePoint = 5;
            Health = 100;
            Gold = 1500;
            Weapon = null;
            Armor = null;
            inventory = new List<Item>();
        }

        public bool HasItem(string itemName)
        {
            for (int i = 0; i < inventory.Count(); i++)
            {
                if (inventory[i].Name == itemName)
                {
                    return (true);
                }
            }
            return (false);
        }

        public void ShowInventory()
        {
            Console.WriteLine("\n[인벤토리]");

            Console.WriteLine("\n[아이템 목록]");
            for (int i = 0; i < inventory.Count(); i++)
            {
                string equip = "";
                if (Weapon != null && (inventory[i].Name == Weapon.Name))
                {
                    equip = "[E]";
                }
                else if (Armor != null && (inventory[i].Name == Armor.Name))
                {
                    equip = "[E]";
                }
                string tag = (inventory[i].Tag == 1)
                    ? "공격력"
                    : "방어력";

                Console.WriteLine($"- {equip} {i + 1}. {inventory[i].Name} | {tag} {inventory[i].Stat:+#;-#;0} | {inventory[i].Desc}");
            }
            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("\n0. 나가기");
        }

        public void EquipdManagement()
        {

            Console.WriteLine("\n[인벤토리] - 장착 관리");
            Console.WriteLine("\n[아이템 목록]");
            for (int i = 0; i < inventory.Count(); i++)
            {
                string equip = "";
                if (Weapon != null && (inventory[i].Name == Weapon.Name))
                {
                    equip = "[E]";
                }
                else if (Armor != null && (inventory[i].Name == Armor.Name))
                {
                    equip = "[E]";
                }
                string tag = (inventory[i].Tag == 1)
                    ? "공격력"
                    : "방어력";
                Console.WriteLine($"- {equip} {i + 1}. {inventory[i].Name} | {tag} {inventory[i].Stat:+#;-#;0} | {inventory[i].Desc}");
            }
            Console.WriteLine("\n0. 나가기");
        }

        public void Equip(string input)
        {
            int ret;

            if (int.TryParse(input, out ret) && (ret <= inventory.Count() && ret > 0))
            {
                if (inventory[ret - 1].Tag == 1)
                {
                    if (Weapon == null)
                        Weapon = inventory[ret - 1];
                    else
                    {
                        if (Weapon.Name == inventory[ret - 1].Name)
                            Weapon = null;
                        else
                            Weapon = inventory[ret - 1];
                    }
                }
                else
                {
                    if (Armor == null)
                        Armor = inventory[ret - 1];
                    else
                    {
                        if (Armor.Name == inventory[ret - 1].Name)
                            Armor = null;
                        else
                            Armor = inventory[ret - 1];
                    }
                }
            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadLine();
            }
        }

        public void PutItemToInventory(Item newItem)
        {
            if (newItem != null)
            {
                inventory.Add(newItem);
            }
            else
            {
                Console.WriteLine("\n추가 가능한 아이템이 없습니다!");
                Console.ReadLine();
            }
        }

        public void RemoveItemFromInventory(Item newItem)
        {
            Item? targetItem = inventory.Find(item => item.Name == newItem.Name);
            if (targetItem != null)
            {
                inventory.Remove(targetItem);
            }
            else
            {
                Console.WriteLine("\n팔 수 있는 아이템이 없습니다!");
                Console.ReadLine();
            }
        }

        public void ShowStatus()
        {

            string ap = Weapon != null
                ? $"{Weapon.Stat:+#;-#;0}"
                : "0";

            string dp = Armor != null
                ? $"{Armor.Stat:+#;-#;0}"
                : "0";

            Console.WriteLine($"상태 보기");
            Console.WriteLine($"캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv . {Level:D2}");
            Console.WriteLine($"{Name} ({Description})");
            Console.WriteLine($"공격력 : {AttackDamage} ({ap})");
            Console.WriteLine($"방어력 : {DefensePoint} ({dp})");
            Console.WriteLine($"체력 : {Health}");
            Console.WriteLine($"Gold : {Gold} G");
        }
        public void BattleStat()
        {

            string ap = Weapon != null
                ? $"{Weapon.Stat:+#;-#;0}"
                : "0";

            string dp = Armor != null
                ? $"{Armor.Stat:+#;-#;0}"
                : "0";

            Console.SetCursorPosition(80, 0);
            Console.WriteLine($"Stat");
            Console.SetCursorPosition(80, 1);
            Console.WriteLine($"Lv . {Level:D2}");
            Console.SetCursorPosition(80, 2);
            Console.WriteLine($"{Name} ({Description})");
            Console.SetCursorPosition(80, 3);
            Console.WriteLine($"공격력 : {AttackDamage} ({ap})");
            Console.SetCursorPosition(80, 4);
            Console.WriteLine($"방어력 : {DefensePoint} ({dp})");
            Console.SetCursorPosition(80, 5);
            Console.WriteLine($"체력 : {Health}");
            Console.SetCursorPosition(80, 6);
            Console.WriteLine($"Gold : {Gold} G");
        }

        public void TargetEnemy(string enemynum, Enemy enemy)
        {
            switch (enemynum)
            {
                case "1":
                    Attack(enemy.targetmonster[0]);
                    break;
                case "2":
                    Attack(enemy.targetmonster[1]);
                    break;
                case "3":
                    Attack(enemy.targetmonster[2]);
                    break;
                case "4":
                    Attack(enemy.targetmonster[3]);
                    break;

            }

        }
        public void Attack(EnemyReal target)
        {
            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyz";
            char[] question = new char[8];
            for(int i = 0; i < 8; i++)
            {
                question[i] = chars[random.Next(chars.Length)];
                Console.Write(question[i]);
            }
            Console.WriteLine();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string input = Console.ReadLine();
            sw.Stop();
            if (target.health - AttackDamage * 5 - ((int)sw.ElapsedMilliseconds / 100) > 0)
            {
                target.health -= AttackDamage * 5 - ((int)sw.ElapsedMilliseconds / 100);
            }
            else
            {
                target.health = 0;
            }
        }

        public void UseSkill()
        {

        }


        public void Defense()
        {

        }

        public void Item()
        {

        }
    }

    public class EnemyReal
    {
        public string Name;
        public int Level;
        public int health;

        public EnemyReal(int i)
        {
            if (i == 0)
            {
                Name = "Defender";
                health = 100;
                Level = 1;
            }
            else if (i == 1)
            {
                Name = "Blocker";
                health = 120;
                Level = 2;
            }
            else
            {
                Name = "Shielder";
                health = 140;
                Level = 3;
            }
        }               

    }

    public class Enemy
    {
        public List<EnemyReal> targetmonster = new List<EnemyReal>();

        public void EnemyStat(int index, int x)
        {
            EnemyReal nowTarget = targetmonster[index]; 
            Console.SetCursorPosition(x, 10);
            Console.WriteLine(nowTarget.Name);
            Console.SetCursorPosition(x, 11);
            Console.WriteLine("Lv. " + nowTarget.Level + " " + nowTarget.health);
        }


        //public class Target : Enemy(0)
        //{
        //    public string Skill;
        //    public int Def;
        //}
    }
}

