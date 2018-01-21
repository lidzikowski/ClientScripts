using System.Collections.Generic;

public class ServerData
{
    public List<Item> items { get; set; }
    public List<Map> maps { get; set; }
    public List<PlayerShip> playerShips { get; set; }
    public List<EnemieShip> enemieShips { get; set; }
    public TempLocalPlayer playerData { get; set; }
}