using System.Collections.Generic;

public class ServerData
{
    public List<T_Ammunition> ammunitions { get; set; }
    public List<T_Box> boxes { get; set; }
    public List<T_Item> items { get; set; }
    public List<T_Map> maps { get; set; }
    public List<T_Map_Box> maps_boxes { get; set; }
    public List<T_Map_Enemie> maps_enemies { get; set; }
    public List<T_Reward> rewards { get; set; }
    public List<T_Reward_Item> rewards_items { get; set; }
    public List<T_Ship> ships { get; set; }
    public List<T_Ship_Enemie> ships_enemies { get; set; }
    public List<T_Ship_Player> ships_players { get; set; }
    public T_Player_Local player_data { get; set; }
}