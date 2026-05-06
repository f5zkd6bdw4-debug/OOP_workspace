using OOPRpg.Structs;

namespace OOPRpg.Characters;

public class Archer : Player
{
    public Archer(string name) : base(name, new Stat(105, 16, 6, 14))
    {
    }

    public override void Attack(Character target)
    {
        Console.WriteLine("궁수가 빠르게 화살을 쏩니다.");
        base.Attack(target, stat.Speed / 3);
    }

    public override void UseSpecialSkill(Character target)
    {
        int damage = stat.Attack + stat.Speed;
        target.TakeDamage(damage);
        Console.WriteLine($"{Name}의 연속 사격! {target.Name}에게 {damage} 피해!");
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine("직업: 궁수");
    }
}
