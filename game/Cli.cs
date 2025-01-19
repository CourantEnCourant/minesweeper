

using System.Text.RegularExpressions;

namespace game;

public class Cli
{
    private int row;
    private int column;
    private int mines;
    
    public void InitializeGame()
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

                    Console.Write(@"Enter two numbers to target a cell, ""R"" to reveal, ""F"" to flag: ");
                    var input = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(input))
                    {
                        
                    } 
                    
                    if (input == "r")
                    {
                        var clickCoordinate = controller.Click();
                        grid.Reveal(clickCoordinate);
                        grid.CalculateAdjacent(grid.Field[clickCoordinate.Row][clickCoordinate.Column]);
                        if (grid.HasMine(clickCoordinate))
                        {
                            Console.WriteLine("You step on a mine, game over!");
                            Environment.Exit(0);
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
                }
            }
            else if (command == "quit")
            {
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
    

    public void SelectDifficulty()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Input number to difficulty:\n 0 - Easy\n1 - Normal\n2 - Hard\n3 - Impossible");
            var inputString = Console.ReadLine();

            if (int.TryParse(inputString, out int inputNum))
            {
                switch (inputNum)
                {
                    case 0:
                        mines = row * column / 10;
                        break;
                    case 1:
                        mines = row * column / 5;
                        break;
                    case 2:
                        mines = row * column / 4;
                        break;
                    case 3:
                        mines = row * column / 3;
                        break;
                }

                break;
            }
        }
    }
}