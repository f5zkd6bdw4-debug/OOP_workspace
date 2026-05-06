using OOPRpg.Characters;
using OOPRpg.Interfaces;

namespace OOPRpg.Systems;

public class Skill : IPrintable
{
    public string Name { get; private set; }
    public int Power { get; private set; }
    public string Description { get; private set; }

    public Skill(string name, int power, string description)
    {
        Name = name;
        Power = power;
        Description = description;
    }

    public void Cast(Character caster, Character target)
    {
        int damage = Math.Max(1, caster.Stat.Attack + Power - target.Stat.Defense);
        target.TakeDamage(damage);
        Console.WriteLine($"{caster.Name}의 {Name}! {target.Name}에게 {damage} 피해!");
    }

    public void PrintInfo()
    {
        Console.WriteLine($"{Name} / 위력 {Power} / {Description}");
    }
}
