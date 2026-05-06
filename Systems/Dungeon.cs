using OOPRpg.Characters;

namespace OOPRpg.Systems;

public class Dungeon
{
    private readonly List<Func<Monster>> monsterFactories;
    private int clearedCount;

    public Dungeon()
    {
        monsterFactories = new List<Func<Monster>>
        {
            () => new Slime(),
            () => new Goblin(),
            () => new Orc(),
            () => new BossMonster()
        };
    }

    public Monster CreateNextMonster()
    {
        int index = Math.Min(clearedCount, monsterFactories.Count - 1);
        clearedCount++;
        return monsterFactories[index]();
    }

    public bool IsCleared()
    {
        return clearedCount >= monsterFactories.Count;
    }

    public void Reset()
    {
        clearedCount = 0;
    }
}
