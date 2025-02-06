using System;

public interface IGameState
{
    void DisplayMenu(GameManager gameManager);
    void HandleInput(GameManager gameManager, string input);
}


class TextRPG
{
    private static void Main()
    {
        GameManager gameManager = new GameManager();

        gameManager.Run();
    }
}

public class MainMenuState : IGameState
{
    public void DisplayMenu(GameManager gameManager)
    {
        Console.Clear();
        Console.WriteLine("\n[캠프 입구]");
        Console.WriteLine("\n1. 상태 보기");
        Console.WriteLine("0. 종료");
    }

    public void HandleInput(GameManager gameManager, string input)
    {
        switch (input)
        {
            case "1":
                gameManager.ChangeState(new StatusMenuState());
                break;
            case "0":
                gameManager.QuitGame();
                break;
        }
    }
}

public class StatusMenuState : IGameState
{
    public void DisplayMenu(GameManager gameManager)
    {
        Console.Clear();
        gameManager.Player.ShowInventory();
    }

    public void HandleInput(GameManager gameManager, string input)
    {
        switch (input)
        {
            case "0":
                gameManager.ChangeState(new MainMenuState());
                break;
        }
    }
}

public class GameManager
{
    private IGameState currentState;
    private bool isRunning = true;
    public Player Player { get; private set; }

    public GameManager()
    {
        Player = new Player("Default");
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
}

public class Player
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double AttackDamage { get; set; }
    public int DefensePoint { get; set; }
    public int Health { get; set; }
    public int Gold { get; set; }
    public int Level { get; set; }
    public int ClearCount { get; set; }
    public Item? Weapon { get; set; }
    public Item? Armor { get; set; }

    public List<Item> inventory;

    public Player(string name)
    {
        Name = name;
        Level = 1;
        Description = "용병";
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
}
