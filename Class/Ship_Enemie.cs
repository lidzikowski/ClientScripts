using UnityEngine;

public class Ship_Enemie : Ship
{
    public int base_shields { get; set; }
    public int base_damage { get; set; }
    public bool is_aggresive { get; set; }
    public int shot_distance { get; set; }
    public Ammunition ammunition { get; set; }

    public Ship_Enemie(T_Ship_Enemie ship, GameObject _model, Reward _reward, Ammunition _ammunition) : base(ship, _model, _reward)
    {
        base_shields = ship.base_shields;
        base_damage = ship.base_damage;
        if (ship.is_aggresive == 1)
            is_aggresive = true;
        else
            is_aggresive = false;
        shot_distance = ship.shot_distance;
        ammunition = _ammunition;
    }
}