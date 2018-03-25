using UnityEngine;

public class Player_Local : Parent
{
    public int user_id { get; set; }
    public Map map { get; set; }
    public Ship_Player ship { get; set; }
    public Ammunition ammunition { get; set; }
    public string nickname { get; set; }
    public int hitpoints { get; set; }
    public int shields { get; set; }
    public int level { get; set; }
    public Vector3 position { get; set; }
    public double scrap { get; set; }
    public double credits { get; set; }
    public double experience { get; set; }
    public double ranking_points { get; set; }

    public GameObject model { get; set; }

    public Player_Local(T_Player_Local player, Game_Data data)
    {
        user_id = player.user_id;
        map = data.maps[player.map_id];
        ship = data.ships_players[player.ship_id];
        ammunition = data.ammunitions[player.ammunition_id];
        nickname = player.nickname;
        hitpoints = player.hitpoints;
        shields = player.shields;
        level = player.level;
        position = new Vector3((float)player.position_x, (float)player.position_y, 0);
        scrap = player.scrap;
        credits = player.credits;
        experience = player.experience;
        ranking_points = player.ranking_points;
        model = null;
    }

    public void ChangeMap()
    {

    }
}