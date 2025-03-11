using System.Collections.Generic;

class Inventory
{
    private int maxWeight;
    private Dictionary<string, Item> items;

    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }

    public bool Put(string itemName, Item item)
    {
        if (TotalWeight() + item.Weight <= maxWeight)
        {
            items[itemName] = item;
            return true;
        }
        return false;
    }

    public Item Get(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            Item item = items[itemName];
            items.Remove(itemName);
            return item;
        }
        return null;
    }

    public int TotalWeight()
    {
        int total = 0;
        foreach (Item item in items.Values)
        {
            total += item.Weight;
        }
        return total;
    }

    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

    public Dictionary<string, Item> GetItems()
    {
        return items;
    }

    public string Show()
    {
        if (items.Count == 0)
        {
            return "No items.";
        }

        string itemList = "";
        foreach (var item in items)
        {
            itemList += $"\n- {item.Key} (Weight: {item.Value.Weight})";
        }
        return itemList;
    }
}