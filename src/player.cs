// Player.cs
 class Player
{
    // Auto-implemented property for the player's current room
    public  Room CurrentRoom { get; set; }

    // Constructor that initializes the current room to null
    public Player()
    {
        CurrentRoom = null;
        
    }
}
