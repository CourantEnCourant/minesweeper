using System.Text.Json;

namespace game;

public class Leaderboard
{
    public static Dictionary<string, int> ReadFrom(string path="./leaderboard.json")
    {
        string jsonString = File.ReadAllText(path);
        var leaderboard = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonString);
        return leaderboard;
    }
    
    public static Dictionary<string, int> Record(string name, Dictionary<string, int> leaderboard)
    {
        if (leaderboard.ContainsKey(name))
        {
            leaderboard[name]++;
            return leaderboard;
        }

        leaderboard[name] = 1;
        return leaderboard;
    }
    
    public static void Output(Dictionary<string, int> leaderboard, string path="leaderboard.json")
    {
        string jsonString = JsonSerializer.Serialize(leaderboard);
        File.WriteAllText(path, jsonString);
        Console.WriteLine($"Record saved to {path}.");
        Console.Write("Press Enter to continue... ");
        Console.ReadLine();
    }
    
}