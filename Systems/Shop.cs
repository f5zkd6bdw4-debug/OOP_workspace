using OOPRpg.Characters;
using OOPRpg.Exceptions;
using OOPRpg.Items;

namespace OOPRpg.Systems;

public class Shop
{
    private readonly List<Item> goods;
    private readonly InputManager inputManager;

    public Shop(InputManager inputManager)
    {
        this.inputManager = inputManager;
        goods = new List<Item>
        {
            new Potion("작은 포션", 20, 30),
            new Potion("큰 포션", 45, 70),
            new Weapon("훈련용 검", 80, 5),
            new Armor("가죽 갑옷", 75, 4)
        };
    }

    public void Open(Player player)
    {
        try
        {
            Console.WriteLine("\n[상점]");
            PrintGoods();
            int choice = inputManager.ReadMenuNumber("구매할 번호(0: 나가기): ");
            if (choice == 0)
            {
                return;
            }

            Buy(player, choice - 1);
        }
        catch (NotEnoughGoldException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("상점 이용을 종료합니다.");
        }
    }

    public void Buy(Player player, int goodsIndex)
    {
        if (goodsIndex < 0 || goodsIndex >= goods.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(goodsIndex), "존재하지 않는 상품 번호입니다.");
        }

        Item item = goods[goodsIndex];
        player.SpendGold(item.Price);
        player.Inventory.AddItem(item);
        Console.WriteLine($"{item.Name} 구매 완료!");
    }

    public void Sell(Player player, int inventoryIndex)
    {
        Item item = player.Inventory[inventoryIndex];
        if (player.Inventory.RemoveItem(item))
        {
            player.AddGold(item.Price / 2);
            Console.WriteLine($"{item.Name} 판매 완료!");
        }
    }

    public void PrintGoods()
    {
        goods.OrderBy(item => item.Price)
             .Select((item, index) => $"{index + 1}. {item.Name} - {item.Price}G")
             .ToList()
             .ForEach(Console.WriteLine);
    }
}
