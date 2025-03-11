using System.Collections.Generic;

class Room
{
    // Private fields
    private string description;
    private Dictionary<string, Room> exits; // stores exits of this room.
    private Inventory chest; // Inventory for the room (chest)

    // Create a room described "description". Initially, it has no exits.
    // "description" is something like "in a kitchen" or "in a courtyard".
public Room(string desc)
{
    description = desc;
    exits = new Dictionary<string, Room>();
    chest = new Inventory(500); 
}

    // Define an exit for this room.
    public void AddExit(string direction, Room neighbor)
    {
        exits.Add(direction, neighbor);
    }

    // Getter for the chest inventory
    public Inventory Chest
    {
        get { return chest; }
    }

    // Return the description of the room.
    public string GetShortDescription()
    {
        return description;
    }

    // Return a long description of this room, in the form:
    //     You are in the kitchen.
    //     Exits: north, west
    public string GetLongDescription()
    {
        string str = "You are ";
        str += description;
        str += ".\n";
        str += GetExitString();
        return str;
    }

    // Return the room that is reached if we go from this room in direction
    // "direction". If there is no room in that direction, return null.
    public Room GetExit(string direction)
    {
        if (exits.ContainsKey(direction))
        {
            return exits[direction];
        }
        return null;
    }

    // Return a string describing the room's exits, for example
    // "Exits: north, west".
    private string GetExitString()
    {
        string str = "Exits: ";
        str += string.Join(", ", exits.Keys);

        return str;
    }
}
