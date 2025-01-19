

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace game;

public class Cli
{
    public static void InitializeGame()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(@"Enter ""Start"", ""Quit"" or ""Fun fact""");
            var command = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(command))
            {
                command = command.Trim().ToLower();
            }

            if (command == "start")
            {
                Console.WriteLine("Game Started!\nEnter a coordinate to begin (e.g. 3 4)...");
                _play();
            }
            else if (command == "quit")
            {
                Console.WriteLine("Thank you for playing my game!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                break;
            }
            else if (command == "fun fact")
            {
                Console.WriteLine("Do you know I am actually a Turing complete game?");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    private static void _play()
    {
        var firstMoveString = Console.ReadLine();
        var firstMoveCoordinate = Parser.Input2Coordinate(firstMoveString);
                
        var grid = new Grid(row: 9, column: 9, mineNum: 10, firstMoveCoordinate: firstMoveCoordinate);
        var controller = new Controller();
                
        grid.Reveal(firstMoveCoordinate);
        var firstMoveCell = grid.Field[firstMoveCoordinate.Row][firstMoveCoordinate.Column];
        grid.CalculateAdjacent(firstMoveCell);
                
        while (true)
        {
            Console.Clear();
            grid.Display();
            
            Console.WriteLine($"Controller point at: {controller.GetCurrentLocation()}");
            Console.WriteLine(@"Enter two numbers to target a cell, ""R"" to reveal, ""F"" to flag: ");
            var input = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(@"Invalid input, must be 2 integers, ""R"" or ""F"" ");
                Console.Write("Press enter to continue...");
                Console.ReadLine();
                continue;
            }

            input = input.ToLower();
            if (input == "r")
            {
                var clickCoordinate = controller.Click();
                grid.Reveal(clickCoordinate);
                grid.CalculateAdjacent(grid.Field[clickCoordinate.Row][clickCoordinate.Column]);
                if (grid.HasMine(clickCoordinate))
                {
                    Console.WriteLine("You step on a mine, game over!");
                    Console.WriteLine("Another round? (Press Enter)");
                    Console.ReadLine();
                    break;
                }
            }
            else if (input == "f")
            {
                var clickCoordinate = controller.Click();
                grid.Flag(clickCoordinate);
            }
            else
            {
                var moveCoordinate = Parser.Input2Coordinate(input);
                controller.Move(moveCoordinate);
            }

            if (grid.Win())
            {
                Console.WriteLine("Congratulation! The field is save now!");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                break;
            }
        }
    }
}