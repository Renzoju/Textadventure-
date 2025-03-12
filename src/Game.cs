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
        Room outside = new Room("Your lost from school camp..... you are  in the Abandoned Churchyard, standing among broken gravestones. The wind is cold, and you feel something strange. A shadow moves quickly past you, and you are curious. You decide to follow it.");
        Room theatre = new Room("You enter the Old Hallway, where dust fills the air, and the wooden floor creaks under your feet. The shadow moves ahead of you, just out of sight. You feel it there, but when you look, there’s nothing...");
        Room pub = new Room("Next, you move into the Damaged Chapel, where the altar is dirty and candles flicker without a flame. The shadow is still ahead, moving quickly. You hear whispers, but the shadow keeps just out of your reach.. ");
        Room lab = new Room("You follow the shadow into the Crumbling Sanctuary, where the walls are cracked, and only a little light comes through the broken windows. The shadow is growing longer, and you feel like you’re getting closer. But when you look, it’s always just ahead...");
        Room office = new Room("You hurry into the Forgotten Crypt, where cold stone surrounds you. The air smells damp. The shadow is still there, always moving ahead, never waiting for you to catch up. You feel like you’re getting realy  closer, but you can’t see it...");
        Room basement = new Room("You rush through the Dark Hallway, where the walls are cracked and shadows move quickly. You stop for a moment, but the shadow moves faster than you. It’s always just ahead, but you can’t catch it.");
        Room rooftop = new Room("Suddenly, you find a stairway leading down. You decide to follow the shadow into the Basement, where it’s cold and dark. The air smells musty, and you hear nothing except the sound of your own footsteps. The shadow is still ahead, flickering in and out of view.");
        Room room = new Room("You step into the Moldy Storage Room, where old boxes and broken furniture are piled everywhere. The shadow moves between the piles of junk. You reach out to grab it, but it slips through your fingers again.");
        Room room1 = new Room("Finally, you enter the Hidden Cellar, where the walls are damp and cold. The shadow stops, and for the first time, you get close. But when you reach out to touch it, the shadow suddenly disappears into a crack in the wall. You look around and realize there’s a Locked Door ahead.");
        Room room2 = new Room("You step into the Hidden Cellar, where the walls are damp and cold. The shadow stops, and for the first time, you get close. But when you reach out to touch it, the shadow suddenly disappears into a crack in the wall. You look around and realize there’s a Locked Door ahead.");
        Room room3 = new Room("Finally, you reach the Locked Gate. The gate is old and rusty. It’s closed, and you can’t get through. But then you realize—you need a key. Without the key, you can’t follow the shadow anymore.");
    

        outside.AddExit("east", theatre);
        outside.AddExit("south", lab);
        outside.AddExit("west", pub);
        outside.AddExit("down", basement);
        outside.AddExit("up", rooftop);

        theatre.AddExit("west", outside);
        pub.AddExit("east", outside);
        lab.AddExit("north", outside);
        lab.AddExit("east", office);
        office.AddExit("west", lab);
        
        basement.AddExit("up", outside);
        rooftop.AddExit("down", outside);

        // Voeg items toe aan de chest van een kamer
        Item sword = new Item(5, "iron sword");
        Item shield = new Item(7, "iron shield");
        Item potion = new Item(1, "health potion");

        outside.Chest.Put("sword", sword);
        outside.Chest.Put("shield", shield);
        outside.Chest.Put("potion", potion);

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
            Console.WriteLine("You have died.");
            Environment.Exit(0); // Beëindig het spel als de speler dood is
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