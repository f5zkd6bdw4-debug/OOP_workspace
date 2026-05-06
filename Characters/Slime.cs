using OOPRpg.Structs;

namespace OOPRpg.Characters;

public class Slime : Monster
{
    public Slime() : base("슬라임", new Stat(35, 7, 1, 4), 15, 10)
    {
    }

    public override void Attack(Character target)
    {
        Console.WriteLine("슬라임이 통통 튀어 공격합니다.");
        base.Attack(target);
    }

    public override void UseSpecialSkill(Character target)
    {
        base.Attack(target, 3);
    }
}
