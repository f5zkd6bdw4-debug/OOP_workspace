using OOPRpg.Structs;

namespace OOPRpg.Characters;

public class Mage : Player
{
    public Mage(string name) : base(name, new Stat(90, 24, 4, 7))
    {
    }

    public override void Attack(Character target)
    {
        Console.WriteLine("마법사가 지팡이로 마력탄을 발사합니다.");
        base.Attack(target);
    }

    public override void UseSpecialSkill(Character target)
    {
        int damage = stat.Attack + 18;
        target.TakeDamage(damage);
        Console.WriteLine($"{Name}의 파이어볼! {target.Name}에게 {damage} 피해!");
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine("직업: 마법사");
    }
}
