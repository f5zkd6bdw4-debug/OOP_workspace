using OOPRpg.Interfaces;

namespace OOPRpg.Items;

public abstract class Item : IComparable<Item>, IPrintable
{
    public string Name { get; protected set; }
    public int Price { get; protected set; }

    protected Item(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public int CompareTo(Item? other)
    {
        if (other is null)
        {
            return 1;
        }

        return Price.CompareTo(other.Price);
    }

    public virtual void PrintInfo()
    {
        Console.WriteLine($"{Name} / 가격 {Price}G");
    }
}
