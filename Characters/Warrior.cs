using OOPRpg.Structs;

namespace OOPRpg.Characters;

public class Warrior : Player
{
    public Warrior(string name) : base(name, new Stat(130, 18, 10, 5))
    {
    }

    public override void Attack(Character target)
    {
        Console.WriteLine("전사가 검을 휘두릅니다.");
        base.Attack(target, 2);
    }

    public override void UseSpecialSkill(Character target)
    {
        int damage = stat.Attack * 2;
        target.TakeDamage(damage);
        Console.WriteLine($"{Name}의 방패 강타! {target.Name}에게 {damage} 피해!");
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine("직업: 전사");
    }
}
