using OOPRpg.Characters;
using OOPRpg.Interfaces;

namespace OOPRpg.Items;

public class Armor : Item, IUsable, ISellable
{
    public int DefenseBonus { get; private set; }

    public Armor(string name, int price, int defenseBonus) : base(name, price)
    {
        DefenseBonus = defenseBonus;
    }

    public void Use(Player player)
    {
        Console.WriteLine($"{Name}을 장비했습니다. 방어력 보너스 {DefenseBonus}는 시연용으로 표시됩니다.");
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"  방어력 보너스: {DefenseBonus}");
    }
}
