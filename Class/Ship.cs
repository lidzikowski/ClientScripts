using UnityEngine;

public abstract class Ship
{
    public int ship_id { get; set; }
    public string ship_name { get; set; }
    public int base_hitpoints { get; set; }
    public int base_speed { get; set; }
    public Reward reward { get; set; }
    public GameObject model { get; set; }

    public Ship(T_Ship ship, GameObject _model, Reward _reward)
    {
        ship_id = ship.ship_id;
        ship_name = ship.ship_name;
        base_hitpoints = ship.base_hitpoints;
        base_speed = ship.base_speed;
        reward = _reward;
        model = _model;
    }
}