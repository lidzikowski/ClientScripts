using System;
using System.Collections.Generic;

public class JsonData
{
    public JsonLocalPlayer localPlayer { get; set; }
    public List<JsonPlayer> otherPlayers { get; set; }
    public List<JsonEnemy> otherEnemies { get; set; }
}

public abstract class Json
{
    public string ship_name { get; set; }
    public int hitpoints { get; set; }
    public int hitpoints_max { get; set; }
    public int shields { get; set; }
    public int shields_max { get; set; }
    public double position_x { get; set; }
    public double position_y { get; set; }
    public double new_position_x { get; set; }
    public double new_position_y { get; set; }
    public int speed { get; set; }
}

public class JsonPlayer : Json
{
    public int user_id { get; set; }
    public string username { get; set; }
}

public class JsonLocalPlayer : Json
{
    public string map_id { get; set; }
    public int scrap { get; set; }
    public int credits { get; set; }
    public int experience { get; set; }
    public int level { get; set; }
    public int ranking_points { get; set; }
}

public class JsonEnemy : Json
{
    public int id { get; set; }
    public string enemie_name { get; set; }
    public string enemie_type { get; set; }
}