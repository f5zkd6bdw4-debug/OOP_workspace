using OOPRpg.Characters;
using OOPRpg.Interfaces;

namespace OOPRpg.Items;

public class Weapon : Item, IUsable, ISellable
{
    public int AttackBonus { get; private set; }

    public Weapon(string name, int price, int attackBonus) : base(name, price)
    {
        AttackBonus = attackBonus;
    }

    public void Use(Player player)
    {
        Console.WriteLine($"{Name}을 장비했습니다. 공격력 보너스 {AttackBonus}는 시연용으로 표시됩니다.");
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"  공격력 보너스: {AttackBonus}");
    }
}
