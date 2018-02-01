using System;
using System.Collections.Generic;

public class JsonData
{
    public JsonLocalPlayer localPlayer { get; set; }
    public List<JsonPlayer> otherPlayers { get; set; }
    public List<JsonEnemy> otherEnemies { get; set; }
    public List<JsonDeath> objectDeaths { get; set; }
}