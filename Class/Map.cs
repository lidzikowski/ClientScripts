using System.Collections.Generic;

public class Map
{
    public int map_id { get; set; }
    public string map_name { get; set; }
    public int required_level { get; set; }
    public bool is_pvp { get; set; }
    public List<Box> boxes_on_map { get; set; }
    public List<Ship_Enemie> enemies_on_map { get; set; }

    public Map(T_Map map)
    {
        map_id = map.map_id;
        map_name = map.map_name;
        required_level = map.required_level;
        if (map.is_pvp == 1)
            is_pvp = true;
        else
            is_pvp = false;
        boxes_on_map = new List<Box>();
        enemies_on_map = new List<Ship_Enemie>();
    }
}