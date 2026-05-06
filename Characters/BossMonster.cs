using OOPRpg.Structs;

namespace OOPRpg.Characters;

public class BossMonster : Monster
{
    public BossMonster() : base("마왕", new Stat(160, 25, 10, 9), 120, 100)
    {
    }

    public override void Attack(Character target)
    {
        Console.WriteLine("마왕이 어둠의 검기를 날립니다.");
        base.Attack(target, 8);
    }

    public override void UseSpecialSkill(Character target)
    {
        int damage = stat.Attack + 25;
        target.TakeDamage(damage);
        Console.WriteLine($"마왕의 어둠 폭발! {target.Name}에게 {damage} 피해!");
    }
}
