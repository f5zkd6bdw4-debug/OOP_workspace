using OOPRpg.Characters;
using OOPRpg.Exceptions;
using OOPRpg.Interfaces;
using OOPRpg.Items;

namespace OOPRpg.Systems;

public delegate void BattleEventHandler(string message);

public class BattleManager
{
    private readonly InputManager inputManager;
    private readonly Random random = new();

    public BattleEventHandler? OnBattleLog;

    public BattleManager(InputManager inputManager)
    {
        this.inputManager = inputManager;
    }

    public bool StartBattle(Player player, Monster monster)
    {
        OnBattleLog?.Invoke($"{monster.Name}이/가 나타났습니다!");
        Queue<Character> turnQueue = BuildTurnQueue(player, monster);

        while (!player.IsDead && !monster.IsDead)
        {
            Character actor = turnQueue.Dequeue();
            turnQueue.Enqueue(actor);

            if (actor.IsDead)
            {
                continue;
            }

            try
            {
                if (actor == player)
                {
                    ProcessPlayerTurn(player, monster);
                }
                else
                {
                    ProcessMonsterTurn(monster, player);
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidItemIndexException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DeadCharacterException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                PrintTurnState(player, monster);
            }
        }

        return EndBattle(player, monster);
    }

    private Queue<Character> BuildTurnQueue(Player player, Monster monster)
    {
        List<Character> ordered = new() { player, monster };
        ordered.Sort((left, right) => right.Stat.Speed.CompareTo(left.Stat.Speed));
        return new Queue<Character>(ordered);
    }

    private void ProcessPlayerTurn(Player player, Monster monster)
    {
        Console.WriteLine("\n[전투 메뉴] 1.공격 2.직업스킬 3.아이템 4.상태확인");
        int choice = inputManager.ReadMenuNumber("선택: ");

        switch (choice)
        {
            case 1:
                player.Attack(monster);
                break;
            case 2:
                player.UseSpecialSkill(monster);
                break;
            case 3:
                UsePlayerItem(player);
                break;
            case 4:
                player.PrintInfo();
                monster.PrintInfo();
                break;
            default:
                throw new FormatException("전투 메뉴 번호가 올바르지 않습니다.");
        }
    }

    private void ProcessMonsterTurn(Monster monster, Player player)
    {
        if (random.Next(0, 100) < 30)
        {
            monster.UseSpecialSkill(player);
        }
        else
        {
            monster.Attack(player);
        }
    }

    private void UsePlayerItem(Player player)
    {
        List<Item> usableItems = player.Inventory.GetUsableItems().Cast<Item>().ToList();
        if (usableItems.Count == 0)
        {
            Console.WriteLine("사용 가능한 아이템이 없습니다.");
            return;
        }

        usableItems.Select((item, index) => $"{index + 1}. {item.Name}")
                   .ToList()
                   .ForEach(Console.WriteLine);

        int itemNumber = inputManager.ReadMenuNumber("사용할 아이템 번호: ") - 1;
        if (itemNumber < 0 || itemNumber >= usableItems.Count)
        {
            throw new InvalidItemIndexException("사용할 수 없는 아이템 번호입니다.");
        }

        if (usableItems[itemNumber] is IUsable usable)
        {
            usable.Use(player);
            player.Inventory.RemoveItem(usableItems[itemNumber]);
        }
    }

    private bool EndBattle(Player player, Monster monster)
    {
        if (player.IsDead)
        {
            OnBattleLog?.Invoke("플레이어가 쓰러졌습니다. 게임 오버!");
            return false;
        }

        OnBattleLog?.Invoke($"{monster.Name} 처치! EXP {monster.ExpReward}, GOLD {monster.GoldReward} 획득!");
        player.GainExp(monster.ExpReward);
        player.AddGold(monster.GoldReward);
        return true;
    }

    private void PrintTurnState(Player player, Monster monster)
    {
        Console.WriteLine($"현재 HP: {player.Name} {player.CurrentHp}/{player.Stat.MaxHp} | {monster.Name} {monster.CurrentHp}/{monster.Stat.MaxHp}");
    }
}
