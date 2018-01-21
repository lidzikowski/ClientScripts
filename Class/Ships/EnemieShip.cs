
public class EnemieShip : Ship
{
    public string enemie_name { get; set; }
    public string enemie_type { get; set; }
    public int base_shields { get; set; }
    public int base_damage { get; set; }
    public int reward_scrap { get; set; }
    public int reward_credits { get; set; }
    public int is_aggresive { get; set; }
    public int shot_distance { get; set; }
}