using System;

class Player
{
    public Room CurrentRoom { get; set; }
    public Inventory Inventory { get; private set; } 

    private int health = 100;

    public int Health
    {
        get { return health; }
        set 
        { 
            if (value < 0) health = 0;
            else if (value > 100) health = 100;
            else health = value;
        }
    }

    public Player() 
    {
        CurrentRoom = null;
        Inventory = new Inventory(10);
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
}
