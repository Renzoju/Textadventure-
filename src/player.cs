using System;

class Player
{
    public Room CurrentRoom { get; set; }
    private Inventory backpack;
    private int health = 100;

    public int Health
    {
        get { return health; }
        private set
        {
            if (value < 0) health = 0;
            else if (value > 100) health = 100;
            else health = value;
        }
    }

    public Player()
    {
        CurrentRoom = null;
        backpack = new Inventory(25);
    }

    public void Damage(int amount)
    {
        Health -= amount;
    }

    public void Heal(int amount)
    {
        Health += amount;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    public Inventory Backpack
    {
        get { return backpack; }
    }

    public string ShowInventory()
    {
        return backpack.Show();
    }

    public bool TakeFromChest(string itemName)
    {
        Item item = CurrentRoom.Chest.Get(itemName);
        if (item != null)
        {
            if (backpack.Put(itemName, item))
            {
                return true;
            }
            else
            {
                CurrentRoom.Chest.Put(itemName, item);
                return false;
            }
        }
        return false;
    }

    public bool DropToChest(string itemName)
    {
        Item item = backpack.Get(itemName);
        if (item != null)
        {
            if (CurrentRoom.Chest.Put(itemName, item))
            {
                return true;
            }
            else
            {
                backpack.Put(itemName, item);
                return false;
            }
        }
        return false;
    }
}