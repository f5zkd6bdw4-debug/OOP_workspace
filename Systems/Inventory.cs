using OOPRpg.Exceptions;
using OOPRpg.Interfaces;
using OOPRpg.Items;

namespace OOPRpg.Systems;

public class Inventory<T> where T : Item
{
    private readonly List<T> items = new();

    public int Count => items.Count;

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= items.Count)
            {
                throw new InvalidItemIndexException($"잘못된 아이템 번호입니다: {index + 1}");
            }

            return items[index];
        }
    }

    public void AddItem(T item)
    {
        items.Add(item);
    }

    public void AddItem(T item, int count)
    {
        for (int i = 0; i < count; i++)
        {
            items.Add(item);
        }
    }

    public bool RemoveItem(T item)
    {
        return items.Remove(item);
    }

    public bool TryGetItem(string itemName, out T? foundItem)
    {
        foundItem = items.FirstOrDefault(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        return foundItem != null;
    }

    public void PrintAll()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("인벤토리가 비어 있습니다.");
            return;
        }

        items.Select((item, index) => new { item, index })
             .ToList()
             .ForEach(pair =>
             {
                 Console.Write($"[{pair.index + 1}] ");
                 pair.item.PrintInfo();
             });
    }

    public List<T> GetAll()
    {
        return items.ToList();
    }

    public List<T> GetUsableItems()
    {
        return items.Where(item => item is IUsable).ToList();
    }

    public List<T> FilterByPrice(int minPrice)
    {
        return items.Where(item => item.Price >= minPrice).ToList();
    }

    public void SortByPrice()
    {
        items.Sort((left, right) => left.Price.CompareTo(right.Price));
    }

    public void SortByName()
    {
        items.Sort((left, right) => string.Compare(left.Name, right.Name, StringComparison.Ordinal));
    }
}
