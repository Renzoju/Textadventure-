using System;

class Player
{
    public Room CurrentRoom { get; set; }
    
    
    
    private int health;

    public Player()
    {
        CurrentRoom = null;
        health = 100; 
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0; 
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > 100)
        {
            health = 100; 
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public bool IsAlive()
{
    return health > 0;
}

      public void Status()
    {
        Console.WriteLine($" health: {health}/100");
    } 
}


