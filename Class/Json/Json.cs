
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

    public string target_type { get; set; }
    public int target_id { get; set; }
    public int attack { get; set; }
}