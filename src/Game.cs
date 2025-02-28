using System;

class Game
{
	// Private fields
	private Parser parser;
	private Room currentRoom;

	// Constructor
	public Game()
	{
		parser = new Parser();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		Room basement = new Room("in the basement");
		Room rooftop = new Room("on the rooftop of the university");

		// Initialise room exits
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

		// Start game outside
		currentRoom = outside;
	}

	// Main play routine. Loops until end of play.
	public void Play()
	{
  		PrintWelcome();

		// Enter the main command loop.
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

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(currentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
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
			case "quit":
				wantToQuit = true;
				break;
		}

		return wantToQuit;
	}

	// Print out some help information.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;
		Room nextRoom = currentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to " + direction + "!");
			return;
		}

		currentRoom = nextRoom;
		Console.WriteLine(currentRoom.GetLongDescription());
	}

	// Prints the current room 
	private void Look()
	{
		Console.WriteLine(currentRoom.GetLongDescription());
	}
}
