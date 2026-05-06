using OOPRpg.Structs;

namespace OOPRpg.Characters;

public class Goblin : Monster
{
    public Goblin() : base("고블린", new Stat(55, 12, 3, 8), 25, 18)
    {
    }

    public override void Attack(Character target)
    {
        Console.WriteLine("고블린이 단검으로 찌릅니다.");
        base.Attack(target, 1);
    }

    public override void UseSpecialSkill(Character target)
    {
        int damage = stat.Attack + 5;
        target.TakeDamage(damage);
        Console.WriteLine($"고블린의 기습! {damage} 피해!");
    }
}
