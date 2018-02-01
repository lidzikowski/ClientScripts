
public abstract class Temp
{
    public int id { get; set; }
    public string username { get; set; }
    public string ship_name { get; set; }
    public int hitpoints { get; set; }
    public int hitpoints_max { get; set; }
    public int shields { get; set; }
    public int shields_max { get; set; }
    public int position_x { get; set; }
    public int position_y { get; set; }
    public int speed { get; set; }
    public string ammunition { get; set; }
}

public class TempPlayer : Temp
{
}

public class TempLocalPlayer : Temp
{
    public string map_id { get; set; }
    public int scrap { get; set; }
    public int credits { get; set; }
    public int experience { get; set; }
    public int level { get; set; }
    public int ranking_points { get; set; }
    public string equipment { get; set; }
    public string equip_lasers { get; set; }
    public string equip_shields { get; set; }
    public string equip_engines { get; set; }
    public string equip_extras { get; set; }
}