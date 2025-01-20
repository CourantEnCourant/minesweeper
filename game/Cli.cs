

using System.Net;
using System.Reflection;
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
            Console.Write(@"Enter ""start"", ""quit"", ""leaderboard"" or ""fun fact"": ");
            var command = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(command))
            {
                command = command.Trim().ToLower();
            }

            if (command == "start")
            {
                _play();
            }
            else if (command == "quit")
            {
                Console.WriteLine("Thank you for playing my game!");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                break;
            }
            else if (command == "leaderboard")
            {
                var leaderboard = Leaderboard.ReadFrom();
                foreach (var item in leaderboard)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                continue;
            }
            else if (command == "fun fact")
            {
                Console.WriteLine("Do you know I am actually a Turing complete game? Only if I have infinite grid!");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                continue;
            }
            else
            {
                Console.WriteLine(@"Invalid command. Only ""start"", ""quit"", ""fun fact"" are allowed");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    private static void _play()
    {
        Console.Clear();
        Console.WriteLine("WARNING");
        Console.WriteLine("This MVP version shows mine location explicitly (noted by \"M\"), to facilitate testing and grading.");
        Console.WriteLine("Grid set to 9*9, mine-count to 10.");
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
        
        var pattern = @"\b([0-9]+) ([0-9]+)\b";
        var firstMoveCoordinate = new Coordinate(3, 4);
        while (true)
        {
            Console.Clear();
            Console.Write("Game Started!\nEnter a coordinate to begin (e.g. 3 4): ");
            
            var regex = new Regex(pattern);
            var firstMoveString = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(firstMoveString) && regex.IsMatch(firstMoveString))
            {
                firstMoveCoordinate = Parser.Input2Coordinate(firstMoveString, pattern);
                break;
            }
            else
            {
                Console.WriteLine("Input format should be single digit \"num num\". (e.g. 3 4) ");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                continue;
            }
        }

        var grid = new Grid(row: 8, column: 8, mineNum: 10, firstMoveCoordinate: firstMoveCoordinate);
                
        grid.Reveal(firstMoveCoordinate);
        var firstMoveCell = grid.Field[firstMoveCoordinate.Row][firstMoveCoordinate.Column];
        grid.CalculateAdjacent(firstMoveCell);
        
        var controller = new Controller();
        controller.Move(firstMoveCoordinate);
                
        while (true)
        {
            Console.Clear();
            grid.Display();
            
            var aimingAt = controller.AimingAt();
            Console.WriteLine(aimingAt);
            Console.Write(@"Enter two numbers to target a cell, ""R"" to reveal, ""F"" to flag: ");
            var input = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(@"Invalid empty input. Must be 2 integers, ""R"" or ""F"" ");
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
            else if (Regex.IsMatch(input, pattern))
            {
                var targetCoordinate = Parser.Input2Coordinate(input, pattern);
                controller.Move(targetCoordinate);
            }
            else
            {
                Console.WriteLine(@"Invalid input. Must be 2 integers, ""R"" or ""F"" ");
                Console.Write("Press enter to continue... ");
                Console.ReadLine();
                continue;
            }

            if (grid.Win())
            {
                Console.WriteLine("Congratulation! You won the game!");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                while (true)
                {
                    Console.Clear();
                    Console.Write("Would you add your result to leaderboard ? [y/n]: ");
                    var choice = Console.ReadLine();

                    if (String.IsNullOrWhiteSpace(choice))
                    {
                        Console.WriteLine("Invalid input. Choose between [y/n]");
                        Console.Write("Press Enter to continue...");
                        Console.ReadLine();
                        continue;
                    }
                    
                    choice = choice.Trim().ToLower();
                    if (choice == "y")
                    {
                        Console.Write("Enter your name: ");
                        var name = Console.ReadLine();
                        var leaderboard = Leaderboard.ReadFrom();
                        leaderboard = Leaderboard.Record(name, leaderboard);
                        Leaderboard.Output(leaderboard);
                        break;
                    }
                    else if (choice == "n")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(@"Invalid input. Choose between [y/n].");
                        Console.Write("Press Enter to continue");
                        Console.ReadLine();
                        break;
                    }
                }
                break;
            }
        }
    }
}