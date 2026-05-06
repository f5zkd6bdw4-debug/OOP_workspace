using OOPRpg.Interfaces;
using OOPRpg.Structs;

namespace OOPRpg.Characters;

public abstract class Monster : Character, IRewardable
{
    public int ExpReward { get; protected set; }
    public int GoldReward { get; protected set; }

    protected Monster(string name, Stat stat, int expReward, int goldReward) : base(name, stat)
    {
        ExpReward = expReward;
        GoldReward = goldReward;
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"보상: EXP {ExpReward}, GOLD {GoldReward}");
    }
}
