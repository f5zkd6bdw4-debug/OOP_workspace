using OOPRpg.Exceptions;
using OOPRpg.Interfaces;
using OOPRpg.Structs;

namespace OOPRpg.Characters;

public abstract class Character : IAttackable, IPrintable
{
    protected int currentHp;
    protected Stat stat;

    public string Name { get; protected set; }
    public Stat Stat => stat;
    public int CurrentHp => currentHp;
    public bool IsDead => currentHp <= 0;

    protected Character(string name, Stat stat)
    {
        Name = name;
        this.stat = stat;
        currentHp = stat.MaxHp;
    }

    public virtual void Attack(Character target)
    {
        EnsureAlive();
        int damage = CalculateDamage(target);
        target.TakeDamage(damage);
        Console.WriteLine($"{Name}이/가 {target.Name}에게 {damage} 피해를 입혔습니다.");
    }

    public virtual void Attack(Character target, int bonusDamage)
    {
        EnsureAlive();
        int damage = Math.Max(1, CalculateDamage(target) + bonusDamage);
        target.TakeDamage(damage);
        Console.WriteLine($"{Name}이/가 추가 피해와 함께 {target.Name}에게 {damage} 피해를 입혔습니다.");
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage), "피해량은 음수가 될 수 없습니다.");
        }

        currentHp = Math.Max(0, currentHp - damage);
    }

    public virtual void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "회복량은 음수가 될 수 없습니다.");
        }

        currentHp = Math.Min(stat.MaxHp, currentHp + amount);
    }

    public virtual void PrintInfo()
    {
        Console.WriteLine($"{Name} HP {currentHp}/{stat.MaxHp} ATK {stat.Attack} DEF {stat.Defense} SPD {stat.Speed}");
    }

    public int CalculateDamage(Character target)
    {
        return Math.Max(1, stat.Attack - target.stat.Defense);
    }

    protected void EnsureAlive()
    {
        if (IsDead)
        {
            throw new DeadCharacterException($"{Name}은/는 이미 쓰러져 행동할 수 없습니다.");
        }
    }

    public abstract void UseSpecialSkill(Character target);
}
