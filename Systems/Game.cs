using OOPRpg.Characters;
using OOPRpg.Exceptions;
using OOPRpg.Items;

namespace OOPRpg.Systems;

public class Game
{
    private readonly InputManager inputManager = new();
    private readonly Dungeon dungeon = new();
    private readonly BattleManager battleManager;
    private readonly Shop shop;
    private Player? player;

    public Game()
    {
        battleManager = new BattleManager(inputManager);
        battleManager.OnBattleLog += message => Console.WriteLine($"[전투 로그] {message}");
        shop = new Shop(inputManager);
    }

    public void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        PrintTitle();

        bool running = true;
        while (running)
        {
            try
            {
                PrintMainMenu();
                int choice = inputManager.ReadMenuNumber("선택: ");
                running = ProcessMainMenu(choice);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex) when (ex is NotEnoughGoldException or InvalidItemIndexException)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("메인 루프 1회 처리 완료\n");
            }
        }
    }

    private void PrintTitle()
    {
        Console.WriteLine("==============================");
        Console.WriteLine("   Dungeon of OOP - C# RPG");
        Console.WriteLine("==============================");
    }

    private void PrintMainMenu()
    {
        Console.WriteLine("1. 새 게임");
        Console.WriteLine("2. 던전 입장");
        Console.WriteLine("3. 인벤토리");
        Console.WriteLine("4. 상점");
        Console.WriteLine("5. 상태 확인");
        Console.WriteLine("0. 종료");
    }

    private bool ProcessMainMenu(int choice)
    {
        switch (choice)
        {
            case 1:
                StartNewGame();
                return true;
            case 2:
                EnterDungeon();
                return true;
            case 3:
                OpenInventory();
                return true;
            case 4:
                RequirePlayer().PrintInfo();
                shop.Open(RequirePlayer());
                return true;
            case 5:
                RequirePlayer().PrintInfo();
                return true;
            case 0:
                Console.WriteLine("게임을 종료합니다.");
                return false;
            default:
                throw new FormatException("메뉴 번호가 올바르지 않습니다.");
        }
    }

    private void StartNewGame()
    {
        string name = inputManager.ReadRequiredText("캐릭터 이름: ");
        Console.WriteLine("직업 선택: 1.전사 2.마법사 3.궁수");
        int job = inputManager.ReadMenuNumber("직업 번호: ");
        player = CreatePlayer(name, job);
        GiveStarterItems(player);
        dungeon.Reset();
        Console.WriteLine($"{player.Name} 생성 완료!");
    }

    private Player CreatePlayer(string name, int job)
    {
        return job switch
        {
            1 => new Warrior(name),
            2 => new Mage(name),
            3 => new Archer(name),
            _ => throw new FormatException("직업 번호가 올바르지 않습니다.")
        };
    }

    private void GiveStarterItems(Player newPlayer)
    {
        newPlayer.Inventory.AddItem(new Potion("초보자 포션", 0, 40), 3);
        newPlayer.TryLearnSkill(new Skill("집중 공격", 8, "기본 공격보다 강한 공용 스킬"), out Skill learnedSkill);
        Console.WriteLine($"기본 스킬 습득: {learnedSkill.Name}");
    }

    private void EnterDungeon()
    {
        Player currentPlayer = RequirePlayer();
        if (dungeon.IsCleared())
        {
            Console.WriteLine("이미 던전을 클리어했습니다. 새 게임으로 다시 도전하세요.");
            return;
        }

        Monster monster = dungeon.CreateNextMonster();
        bool survived = battleManager.StartBattle(currentPlayer, monster);
        if (survived && dungeon.IsCleared())
        {
            Console.WriteLine("축하합니다! 모든 몬스터를 물리치고 게임을 클리어했습니다!");
        }
    }

    private void OpenInventory()
    {
        Player currentPlayer = RequirePlayer();
        try
        {
            currentPlayer.Inventory.SortByName();
            currentPlayer.Inventory.PrintAll();
            List<Item> expensiveItems = currentPlayer.Inventory.FilterByPrice(30);
            Console.WriteLine($"30G 이상 아이템 수: {expensiveItems.Count}");
            if (currentPlayer.Inventory.TryGetItem("초보자 포션", out Item? foundItem))
            {
                Console.WriteLine($"검색 성공: {foundItem!.Name}");
            }
        }
        catch (InvalidItemIndexException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("인벤토리 확인 완료");
        }
    }

    private Player RequirePlayer()
    {
        if (player is null)
        {
            throw new InvalidOperationException("먼저 새 게임을 시작하세요.");
        }

        return player;
    }
}
