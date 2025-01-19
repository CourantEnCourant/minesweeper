
using System.Text.RegularExpressions;

namespace game;

public class Parser
{
    public static Coordinate Input2Coordinate(string input, string pattern)
    // Parse input to Coordinate
    {
        var regex = new Regex(pattern);
        if (String.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Invalid input. Press Enter to restart a session...");
        }
                        
        var match = regex.Match(input);
        var coordinateRow = Convert.ToInt32(match.Groups[1].Value);
        var coordinateColumn = Convert.ToInt32(match.Groups[2].Value);
        return new Coordinate(coordinateRow, coordinateColumn);
    }
}