using OOPRpg.Exceptions;
using OOPRpg.Items;
using OOPRpg.Structs;
using OOPRpg.Systems;

namespace OOPRpg.Characters;

public abstract class Player : Character
{
    public int Level { get; protected set; }
    public int Exp { get; protected set; }
    public int Gold { get; private set; }
    public Inventory<Item> Inventory { get; }
    public Dictionary<string, Skill> Skills { get; }

    protected Player(string name, Stat stat) : base(name, stat)
    {
        Level = 1;
        Exp = 0;
        Gold = 100;
        Inventory = new Inventory<Item>();
        Skills = new Dictionary<string, Skill>();
    }

    public void GainExp(int amount)
    {
        Exp += amount;
        while (Exp >= RequiredExp())
        {
            Exp -= RequiredExp();
            LevelUp();
        }
    }

    public virtual void LevelUp()
    {
        Level++;
        stat.Add(10, 2, 1, 1);
        currentHp = stat.MaxHp;
        Console.WriteLine($"레벨 업! {Name}의 현재 레벨: {Level}");
    }

    public void AddGold(int amount)
    {
        Gold += Math.Max(0, amount);
    }

    public void SpendGold(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "사용 골드는 음수가 될 수 없습니다.");
        }

        if (Gold < amount)
        {
            throw new NotEnoughGoldException($"골드가 부족합니다. 필요: {amount}, 보유: {Gold}");
        }

        Gold -= amount;
    }

    public void Heal(Potion potion)
    {
        Heal(potion.HealAmount);
    }

    public bool TryLearnSkill(Skill skill, out Skill learnedSkill)
    {
        learnedSkill = skill;
        if (Skills.ContainsKey(skill.Name))
        {
            return false;
        }

        Skills.Add(skill.Name, skill);
        return true;
    }

    public int RequiredExp()
    {
        return Level * 50;
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"LV {Level} EXP {Exp}/{RequiredExp()} GOLD {Gold}");
    }
}
