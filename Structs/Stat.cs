namespace OOPRpg.Structs;

public struct Stat
{
    public int MaxHp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }

    public Stat(int maxHp, int attack, int defense, int speed)
    {
        MaxHp = maxHp;
        Attack = attack;
        Defense = defense;
        Speed = speed;
    }

    public void Add(int maxHp, int attack, int defense, int speed)
    {
        MaxHp += maxHp;
        Attack += attack;
        Defense += defense;
        Speed += speed;
    }
}
