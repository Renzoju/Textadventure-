using System;
using System.Runtime.CompilerServices;

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
        Room outside = new Room("outside the main entrance of the university");
        Room theatre = new Room("in a lecture theatre");
        Room pub = new Room("in the campus pub");
        Room lab = new Room("in a computing lab");
        Room office = new Room("in the computing admin office");
        Room basement = new Room("in the basement");
        Room rooftop = new Room("on the rooftop of the university");


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
             player.Status();
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
        player.Damage(50);
        Console.WriteLine(player.CurrentRoom.GetLongDescription());

        if (!player.IsAlive())
    {
        Console.WriteLine("You have died.");
        Environment.Exit(0);
    }

        
    }

    private void Look()
    {
       
        Console.WriteLine(player.CurrentRoom.GetLongDescription());
    } 
}



