using System;
using System.Collections;


class Game
{
    private Parser parser;
    private Player player;

    public Game()
    {
        parser = new Parser();
        player = new Player();
        CreateRooms();
    }

    private void CreateRooms()
    {
Room outside = new Room("lost from school camp... You stand in the Abandoned Churchyard, among broken gravestones. The cold wind howls, and a shadow moves quickly past you. Curious, you decide to follow it.");
Room hallway = new Room("in the Old Hallway, where dust fills the air, and the wooden floor creaks under your feet. The shadow moves ahead of you, just out of sight...");
Room chapel = new Room("in the Damaged Chapel, where the altar is dusty and candles flicker without a flame. Whispers echo through the air, but the shadow remains just out of reach...");
Room sanctuary = new Room("in the Crumbling Sanctuary. The walls are cracked, and faint light seeps through broken windows. The shadow grows longer, yet stays ahead...");
Room crypt = new Room("in the Forgotten Crypt, surrounded by cold stone. The damp air chills you. The shadow is close, but still you can't see it clearly...");
Room darkHallway = new Room("in the Dark Hallway. The walls are cracked, and flickering shadows dance around you. The shadow moves fast, always just ahead...");
Room basement = new Room("in the Basement. It’s cold and dark. The air smells musty, and the only sound is your own footsteps...");
Room storageRoom = new Room("in the Moldy Storage Room, filled with old boxes and broken furniture. The shadow moves between the piles of junk. You reach out, but it slips away...");
Room hiddenCellar = new Room("in the Hidden Cellar, where damp walls close in around you. The shadow finally stops, but as you reach out, it vanishes through a crack. Ahead, a Locked Door blocks your way.");
Room lockedGate = new Room("at the Locked Gate. The gate is rusty and closed. You push, but it won't move. You need a key to continue...");

outside.AddExit("north", hallway);  

hallway.AddExit("south", outside);  
hallway.AddExit("east", chapel);
hallway.AddExit("west", sanctuary);
hallway.AddExit("north", crypt);

chapel.AddExit("west", hallway);
chapel.AddExit("north", sanctuary);

sanctuary.AddExit("south", chapel);
sanctuary.AddExit("east", hallway);
sanctuary.AddExit("north", crypt);

crypt.AddExit("south", sanctuary);
crypt.AddExit("east", darkHallway);

darkHallway.AddExit("west", crypt);
darkHallway.AddExit("down", basement);  

basement.AddExit("up", darkHallway);  
basement.AddExit("east", storageRoom);

storageRoom.AddExit("west", basement);
storageRoom.AddExit("south", hiddenCellar);

hiddenCellar.AddExit("north", storageRoom);
hiddenCellar.AddExit("east", lockedGate);

lockedGate.AddExit("west", hiddenCellar);

        
        Item sword = new Item(25, "iron sword");
        Item shield = new Item(7, "iron shield");
        Item potion = new Item(1, "health potion");
        Item key = new Item(1, "key");

        outside.Chest.Put("sword", sword);
        outside.Chest.Put("shield", shield);
        outside.Chest.Put(" healthpotion", potion);


        storageRoom.Chest.Put("key", key);
        

        player.CurrentRoom = outside;
    }

    public void Play()
    {
        PrintWelcome();

        bool finished = false;
        while (!finished)
        {
            Command command = parser.GetCommand();
            finished = ProcessCommand(command);
        }
        Console.WriteLine("Thank you for playing.");
        Console.WriteLine("Press [Enter] to continue.");
        Console.ReadLine();
    }

    private void PrintWelcome()
    {
        Console.WriteLine();
        Console.WriteLine("Welcome to Zuul!");
        Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
        Console.WriteLine("Type 'help' if you need help.");
        Console.WriteLine();
        Console.WriteLine(player.CurrentRoom.GetLongDescription());
    }

    private bool ProcessCommand(Command command)
    {
        bool wantToQuit = false;

        if (command.IsUnknown())
        {
            Console.WriteLine("I don't know what you mean...");
            return wantToQuit;
        }

        switch (command.CommandWord)
        {
            case "help":
                PrintHelp();
                break;
            case "go":
                GoRoom(command);
                break;
            case "look":
                Look();
                break;
            case "status":
                Status();
                break;
            case "take":
                Take(command);
                break;
            case "drop":
                Drop(command);
                break;

            case "quit":
                wantToQuit = true;
                break;
        }

        return wantToQuit;
    }

    private void PrintHelp()
    {
        Console.WriteLine("You are lost. You are alone.");
        Console.WriteLine("You wander around at the university.");
        Console.WriteLine();
        parser.PrintValidCommands();
    }

    private void GoRoom(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Go where?");
            return;
        }

        string direction = command.SecondWord;
        Room nextRoom = player.CurrentRoom.GetExit(direction);

        if (nextRoom == null)
        {
            Console.WriteLine("There is no door to " + direction + "!");
            return;
        }

        player.CurrentRoom = nextRoom;
        player.Damage(10); 

        Console.WriteLine(player.CurrentRoom.GetLongDescription());

        if (!player.IsAlive())
        {
            Console.WriteLine("You have died.  thanks for playing.");
            Environment.Exit(0); 
        }
    }

    private void Look()
    {
        Console.WriteLine(player.CurrentRoom.GetLongDescription());

         Console.WriteLine("Items in the room:");
        Console.WriteLine(player.CurrentRoom.Chest.Show());
        Console.WriteLine();
    }

   private void Status()
{
    Console.WriteLine("Your health is: " + player.Health);
    Console.WriteLine();
    Console.WriteLine("Your inventory:");
    Console.WriteLine(player.ShowInventory());
    Console.WriteLine();
    Console.WriteLine($"Backpack weight: {player.Backpack.TotalWeight()}/{player.Backpack.MaxWeight} KG");
}

    private void Take(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Take what?");
            return;
        }
        string itemName = command.SecondWord;
        if (player.TakeFromChest(itemName))
        {
            Console.WriteLine($"You have taken the {itemName} from the ground.");
        }
        else
        {
            Console.WriteLine($"There is no {itemName} in the room or it is too heavy to carry.");
        }
    }

    private void Drop(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Drop what?");
            return;
        }
        string itemName = command.SecondWord;
        if (player.DropToChest(itemName))
        {
            Console.WriteLine($"You have dropped the {itemName} on the ground.");
        }
        else
        {
            Console.WriteLine($"You don't have a {itemName} in your backpack or the ground cannot hold it.");
        }
    }

}