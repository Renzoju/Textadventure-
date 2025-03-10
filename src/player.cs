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
        backpack = new Inventory(25); // 25kg max draaggewicht
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

    public bool TakeFromChest(string itemName)
    {
    

        return false;
    }

    
    public bool DropToChest(string itemName)
    {
     

        return false;
    }
}