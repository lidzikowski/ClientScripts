using System.Collections.Generic;

public class GameData
{
    public Dictionary<int, Item> items { get; set; }
    public Dictionary<string, Map> maps { get; set; }
    public Dictionary<string, PlayerShip> playerShips { get; set; }
    public Dictionary<string, EnemieShip> enemieShips { get; set; }

    public GameData()
    {
        items = new Dictionary<int, Item>();
        maps = new Dictionary<string, Map>();
        playerShips = new Dictionary<string, PlayerShip>();
        enemieShips = new Dictionary<string, EnemieShip>();
    }
}