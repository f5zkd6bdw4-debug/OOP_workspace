using OOPRpg.Characters;
using OOPRpg.Interfaces;

namespace OOPRpg.Items;

public class Potion : Item, IUsable, ISellable
{
    public int HealAmount { get; private set; }

    public Potion(string name, int price, int healAmount) : base(name, price)
    {
        HealAmount = healAmount;
    }

    public void Use(Player player)
    {
        player.Heal(this);
        Console.WriteLine($"{player.Name}이/가 {Name}을 사용하여 HP {HealAmount} 회복!");
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"  회복량: {HealAmount}");
    }
}
