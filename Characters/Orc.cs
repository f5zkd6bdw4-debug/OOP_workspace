using OOPRpg.Structs;

namespace OOPRpg.Characters;

public class Orc : Monster
{
    public Orc() : base("오크", new Stat(80, 16, 6, 5), 40, 30)
    {
    }

    public override void Attack(Character target)
    {
        Console.WriteLine("오크가 도끼를 내려찍습니다.");
        base.Attack(target, 4);
    }

    public override void UseSpecialSkill(Character target)
    {
        int damage = stat.Attack * 2 - target.Stat.Defense;
        target.TakeDamage(Math.Max(1, damage));
        Console.WriteLine($"오크의 분노의 일격! {Math.Max(1, damage)} 피해!");
    }
}
